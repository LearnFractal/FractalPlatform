using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FractalPlatform.Converter.Models
{
    public class Table
    {
        private TablesToCollectionsConverter _converter;

        public Table(TablesToCollectionsConverter converter, string schema, string name)
        {
            _converter = converter;

            Schema = schema;

            Name = name;
        }

        public bool IsRootTable => Parents.Count == 0;

        public bool HasNoIDs => IDs.Count == 0 && RefIDs.Count == 0;

        public string FullName => $"[{Schema}].[{Name}]";

        public string Schema { get; set; }

        public string Name { get; set; }

        public bool HasParents => Parents.Count > 0;

        public HashSet<string> IDs { get; set; } = new HashSet<string>();

        public List<RefInfo> RefIDs { get; set; } = new List<RefInfo>();

        public List<Parent> Parents { get; set; } = new List<Parent>();

        public List<Child> Childs { get; set; } = new List<Child>();

        public List<Column> Columns { get; set; } = new List<Column>();

        public List<Table> Dictionaries { get; set; } = new List<Table>();

        public List<ManyToManyRelation> ManyToManyRelations { get; set; } = new List<ManyToManyRelation>();

        private DataTable _dt;

        public DataTable DataTable
        {
            get
            {
                if (_dt == null)
                {
                    _dt = _converter.GetTable(this);
                }

                return _dt;
            }

            set
            {
                _dt = value;
            }
        }

        public void Init()
        {
            var dt = DataTable;

            foreach (DataColumn dc in dt.Columns)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (_converter.IsRealColumn(dc.ColumnName) &&
                        _converter.IsIdColumn(dc.ColumnName))
                    {
                        if (dc.DataType == typeof(Guid))
                        {
                            var id = dr[dc].ToString();

                            if (_converter.IsRealGuid(id))
                            {
                                if (dc.ColumnName == _converter.GetPrimaryIdColumn(this))
                                {
                                    IDs.Add(id);
                                }
                                else
                                {
                                    RefIDs.Add(new RefInfo { Id = id, RefcolumnName = dc.ColumnName });
                                }
                            }
                        }
                        else if (dc.DataType == typeof(int))
                        {
                            var id = dr[dc].ToString();

                            if (dc.ColumnName == _converter.GetPrimaryIdColumn(this))
                            {
                                IDs.Add(id);
                            }
                            else
                            {
                                var parentTable = _converter.GetParentTableByChild(this.Name, dc.ColumnName);

                                if (parentTable != null)
                                {
                                    RefIDs.Add(new RefInfo { Id = id, RefcolumnName = dc.ColumnName, Table = parentTable });
                                }
                            }
                        }
                    }
                }

                Columns.Add(new Column
                {
                    Name = dc.ColumnName,
                    Type = dc.DataType
                });
            }

            DataTable = null;
        }

        public void FindChilds(List<Table> tables)
        {
            foreach (var table in tables)
            {
                if (this != table)
                {
                    var found = 0;
                    string refColumnName = null;

                    foreach (var childId in table.RefIDs)
                    {
                        if (IDs.Contains(childId.Id) &&
                            childId.IsReferencedTo(this))
                        {
                            if (refColumnName == null)
                            {
                                refColumnName = childId.RefcolumnName;
                            }
                            else if(refColumnName != childId.RefcolumnName)
                            {
                                throw new InvalidOperationException("Double reference between tables");
                            }

                            found++;

                            if (found > 1)
                            {
                                break;
                            }
                        }
                    }

                    if (found > 0)
                    {
                        Childs.Add(new Child
                        {
                            Table = table,
                            Relation = found == 1 ? Relation.OneToOne : Relation.OneToMany
                        });

                        table.Parents.Add(new Parent { Table = this, RefColumnName = refColumnName });
                    }
                }
            }
        }

        public bool FindSelfRefs()
        {
            foreach (var childId in RefIDs)
            {
                if (IDs.Contains(childId.Id) &&
                    childId.IsReferencedTo(this))
                {
                    return true;
                }
            }

            return false;
        }

        public string FindCycleRefs(List<Table> tables)
        {
            return FindCycleRefs(tables, Childs, FullName, FullName);
        }

        public string FindCycleRefs(List<Table> tables,
                                  List<Child> childs,
                                  string rootTableName,
                                  string cycleRef)
        {
            foreach (var child in childs)
            {
                if (child.Table.FullName == rootTableName)
                {
                    return $"{cycleRef} <= {rootTableName}";
                }

                var result = child.Table.FindCycleRefs(tables, child.Table.Childs, rootTableName, $"{cycleRef} <= {child.Table.FullName}");

                if (!string.IsNullOrEmpty(result))
                    return result;
            }

            return null;
        }

        public CollectionInfo ToCollectionJsons()
        {
            var collInfo = new CollectionInfo();

            foreach (DataRow dr in DataTable.Rows)
            {
                var json = ToDocumentJson(dr);

                collInfo.Documents.Add(json);
            }

            collInfo.Enum = ToEnumJson();

            collInfo.UI = ToUIJson();

            collInfo.Validation = ToValidationJson();

            collInfo.Relation = ToRelationJson();

            collInfo.LastAccess = ToLastAccessJson();

            if (collInfo.HasLastAccess)
            {
                foreach (DataRow dr in DataTable.Rows)
                {
                    var json = ToLastAccessDocumentJson(dr);

                    collInfo.LastAccessDocuments.Add(json);
                }
            }

            DataTable = null;

            return collInfo;
        }

        private string GetRelatedToColumn(DataTable manyToManyTable, string tableName)
        {
            var columns = new List<string>();

            foreach(DataColumn dc in manyToManyTable.Columns)
            {
                columns.Add(dc.ColumnName);
            }

            foreach(string column in columns.OrderByDescending(x => x.Length))
            {
                if (column.Contains(tableName))
                    return column;
            }

            throw new InvalidOperationException($"Column for {tableName} table is not found");
        }

        private string ToDocumentJson(string id, ManyToManyRelation relation)
        {
            var dt = relation.ManyToManyTable;

            var relatedFromColumn = GetRelatedToColumn(dt, this.Name);

            var relatedToColumn = GetRelatedToColumn(dt, relation.RelatedToTable.Name);

            var drs = dt.Select($"{relatedFromColumn} = '{id}'");

            var sb = new StringBuilder();

            sb.Append('[');

            bool isFirst = true;

            foreach (DataRow dr in drs)
            {
                if (!isFirst)
                    sb.Append(',');

                sb.Append(GetValue(dr[relatedToColumn]));

                isFirst = false;
            }

            sb.Append(']');

            return sb.ToString();
        }

        private string ToDocumentJson(string columName, string parentId, bool isArray)
        {
            var drs = DataTable.Select($"{columName} = '{parentId}'");

            if (drs.Length == 1)
            {
                if (isArray)
                {
                    return $"[{ToDocumentJson(drs[0])}]";
                }
                else
                {
                    return ToDocumentJson(drs[0]);
                }
            }
            else if (drs.Length > 1)
            {
                var sb = new StringBuilder();

                sb.Append('[');

                bool isFirst = true;

                foreach (DataRow dr in drs)
                {
                    if (!isFirst)
                        sb.Append(',');

                    sb.Append(ToDocumentJson(dr));

                    isFirst = false;
                }

                sb.Append(']');

                return sb.ToString();
            }
            else
            {
                if (isArray)
                {
                    return "[]";
                }
                else
                {
                    return "null";
                }
            }
        }

        private string GetValue(object val)
        {
            if (Convert.IsDBNull(val) ||
                val is byte[]) //cannot be serialized
            {
                return "null";
            }
            else if (val is Guid ||
                    val is string ||
                    val is DateTime)
            {
                return $"\"{val}\"";
            }
            else if(val is bool)
            {
                return val.ToString().ToLower();
            }
            else
            {
                return val.ToString();
            }
        }

        private string GetRelatedToColumn(Table parent)
        {
            foreach(var currParent in Parents)
            {
                if (currParent.Table == parent)
                    return currParent.RefColumnName;
            }

            throw new InvalidOperationException("Ref column is not found");
        }

        private string ToDocumentJson(DataRow dr)
        {
            var json = new StringBuilder();

            json.Append('{');

            bool isFirst = true;

            var primaryIdColumn = _converter.GetPrimaryIdColumn(this);

            //add columns
            foreach (DataColumn dc in dr.Table.Columns)
            {
                if (_converter.IsRealColumn(dc.ColumnName) &&
                    (dc.ColumnName == primaryIdColumn || //regular columns
                    !_converter.IsIdColumn(dc.ColumnName)))
                {
                    if (!isFirst)
                        json.Append(",");

                    var val = GetValue(dr[dc]);

                    json.Append("\"").Append(dc.ColumnName).Append("\":").Append(val);

                    isFirst = false;
                }
                else if (!Convert.IsDBNull(dr[dc])) //dictionary columns
                {
                    var dictionaryValue = _converter.GetDictionaryValueByColumn(this, dc.ColumnName, dr[dc].ToString());

                    if (dictionaryValue != null)
                    {
                        if (!isFirst)
                            json.Append(",");

                        json.Append("\"").Append(dc.ColumnName).Append("\":\"").Append(dictionaryValue).Append("\"");

                        isFirst = false;
                    }
                }
            }

            //add childs
            if (dr.Table.Columns.Contains(primaryIdColumn))
            {
                //Add not shared childs
                var id = dr[primaryIdColumn].ToString();

                foreach (var child in Childs)
                {
                    if (!isFirst)
                        json.Append(",");

                    var isArray = (child.Relation == Relation.OneToMany ||
                                   child.Relation == Relation.ManyToMany);

                    var relatedToColumn = child.Table.GetRelatedToColumn(this);

                    var childJson = child.Table.ToDocumentJson(relatedToColumn, id, isArray);

                    var childName = _converter.GetChildColumnByTable(child.Table);

                    json.Append("\"").Append(childName).Append("\":").Append(childJson);

                    isFirst = false;
                }

                //Add many-to-many childs
                foreach (var relation in ManyToManyRelations)
                {
                    if (!isFirst)
                        json.Append(",");

                    var columnName = relation.RelatedToTable.Name;

                    var relationJson = ToDocumentJson(id, relation);

                    json.Append("\"").Append(columnName).Append("\":").Append(relationJson);

                    isFirst = false;
                }
            }

            json.Append('}');

            return json.ToString();
        }

        private string ToLastAccessDocumentJson(DataRow dr)
        {
            var json = new StringBuilder();

            json.Append('{');

            bool isFirst = true;

            //add columns
            var createdBy = string.Empty;
            var createdOn = string.Empty;

            var updatedBy = string.Empty;
            var updatedOn = string.Empty;

            var deletedBy = string.Empty;
            var deletedOn = string.Empty;

            foreach (DataColumn dc in DataTable.Columns)
            {
                if (dc.ColumnName == _converter.GetCreatedByColumn())
                {
                    createdBy = GetValue(dr[dc.ColumnName]);
                }
                else if (dc.ColumnName == _converter.GetCreatedOnColumn())
                {
                    createdOn = GetValue(dr[dc.ColumnName]);
                }
                else if (dc.ColumnName == _converter.GetUpdatedByColumn())
                {
                    updatedBy = GetValue(dr[dc.ColumnName]);
                }
                else if (dc.ColumnName == _converter.GetUpdatedOnColumn())
                {
                    updatedOn = GetValue(dr[dc.ColumnName]);
                }
                else if (dc.ColumnName == _converter.GetDeletedByColumn())
                {
                    deletedBy = GetValue(dr[dc.ColumnName]);
                }
                else if (dc.ColumnName == _converter.GetDeletedOnColumn())
                {
                    deletedOn = GetValue(dr[dc.ColumnName]);
                }
            }

            if (!string.IsNullOrEmpty(createdBy) || !string.IsNullOrEmpty(createdOn) ||
                !string.IsNullOrEmpty(updatedBy) || !string.IsNullOrEmpty(updatedOn) ||
                !string.IsNullOrEmpty(deletedBy) || !string.IsNullOrEmpty(deletedOn))
            {
                json.Append("\"Track\":{");

                var isFirstRow = true;

                if (!string.IsNullOrEmpty(createdBy) || !string.IsNullOrEmpty(createdOn))
                {
                    json.Append("\"Create\":{")
                        .Append("\"Who\":")
                        .Append(createdBy)
                        .Append(",\"OnDate\":")
                        .Append(createdOn)
                        .Append("}");

                    isFirstRow = false;
                }

                if (!string.IsNullOrEmpty(updatedBy) || !string.IsNullOrEmpty(updatedOn))
                {
                    if (!isFirstRow)
                        json.Append(",");

                    json.Append("\"Update\":{")
                        .Append("\"Who\":")
                        .Append(createdBy)
                        .Append(",\"OnDate\":")
                        .Append(createdOn)
                        .Append("}");

                    isFirstRow = false;
                }

                if (!string.IsNullOrEmpty(deletedBy) || !string.IsNullOrEmpty(deletedOn))
                {
                    if (!isFirstRow)
                        json.Append(",");

                    json.Append("\"Delete\":{")
                        .Append("\"Who\":")
                        .Append(createdBy)
                        .Append(",\"OnDate\":")
                        .Append(createdOn)
                        .Append("}");
                }

                json.Append("}");
            }

            //add childs
            foreach (var child in Childs)
            {
                var isArray = (child.Relation == Relation.OneToMany ||
                               child.Relation == Relation.ManyToMany);

                var relatedToColumn = child.Table.GetRelatedToColumn(this);

                var childJson = child.Table.ToLastAccessJson();

                if (!string.IsNullOrEmpty(childJson))
                {
                    if (!isFirst)
                        json.Append(",");

                    var childName = _converter.GetChildColumnByTable(child.Table);

                    if (!isArray)
                    {
                        json.Append("\"").Append(childName).Append("\":").Append(childJson);
                    }
                    else
                    {
                        json.Append("\"").Append(childName).Append("\":[").Append(childJson).Append("]");
                    }

                    isFirst = false;
                }
            }

            json.Append('}');

            return ReturnJson(json);
        }

        private string ToEnumJson()
        {
            var json = new StringBuilder();

            json.Append('{');

            bool isFirst = true;

            //add columns
            foreach (DataColumn dc in DataTable.Columns)
            {
                if (_converter.IsRealColumn(dc.ColumnName))
                {
                    //dictionary column
                    var values = _converter.GetDictionaryValuesByColumn(this, dc.ColumnName);

                    if (values.Count > 0)
                    {
                        if (!isFirst)
                            json.Append(",");

                        json.Append("\"")
                            .Append(dc.ColumnName)
                            .Append("\":{\"Items\":[")
                            .Append(String.Join(',', values))
                            .Append("]}");

                        isFirst = false;
                    }
                }
            }

            //add childs
            foreach (var child in Childs)
            {
                var isArray = (child.Relation == Relation.OneToMany ||
                               child.Relation == Relation.ManyToMany);

                var relatedToColumn = child.Table.GetRelatedToColumn(this);

                var childJson = child.Table.ToEnumJson();

                if (!string.IsNullOrEmpty(childJson))
                {
                    if (!isFirst)
                        json.Append(",");

                    var childName = _converter.GetChildColumnByTable(child.Table);

                    if (!isArray)
                    {
                        json.Append("\"").Append(childName).Append("\":").Append(childJson);
                    }
                    else
                    {
                        json.Append("\"").Append(childName).Append("\":[").Append(childJson).Append("]");
                    }

                    isFirst = false;
                }
            }

            json.Append('}');

            return ReturnJson(json);
        }

        private string ToUIJson()
        {
            var json = new StringBuilder();

            json.Append('{');

            bool isFirst = true;

            var primaryIdColumn = _converter.GetPrimaryIdColumn(this);

            //add columns
            foreach (DataColumn dc in DataTable.Columns)
            {
                if (_converter.IsRealColumn(dc.ColumnName))
                {
                    if (dc.ColumnName == primaryIdColumn)
                    {
                        if (!isFirst)
                            json.Append(",");

                        json.Append("\"").Append(dc.ColumnName).Append("\":{\"Visible\":false}");

                        isFirst = false;
                    }
                    else //dictionary columns
                    {
                        var values = _converter.GetDictionaryValuesByColumn(this, dc.ColumnName);

                        if (values.Count > 0)
                        {
                            if (!isFirst)
                                json.Append(",");

                            json.Append("\"")
                                .Append(dc.ColumnName)
                                .Append("\":{\"ControlType\":\"ComboBox\"}}");

                            isFirst = false;
                        }
                    }
                }
            }

            //add childs
            foreach (var child in Childs)
            {
                var isArray = (child.Relation == Relation.OneToMany ||
                               child.Relation == Relation.ManyToMany);

                var relatedToColumn = child.Table.GetRelatedToColumn(this);

                var childJson = child.Table.ToUIJson();

                if (!string.IsNullOrEmpty(childJson))
                {
                    if (!isFirst)
                        json.Append(",");

                    var childName = _converter.GetChildColumnByTable(child.Table);

                    if (!isArray)
                    {
                        json.Append("\"").Append(childName).Append("\":").Append(childJson);
                    }
                    else
                    {
                        json.Append("\"").Append(childName).Append("\":[").Append(childJson).Append("]");
                    }

                    isFirst = false;
                }
            }

            json.Append('}');

            return ReturnJson(json);
        }

        private string GetValidationJson(DataTable dt, string columnName)
        {
            var dr = dt.Select($"ColumnName = '{columnName}'")[0];

            var dataType = dr["DataType"].ToString();

            var isRequired = ((bool)dr["IsRequired"]).ToString().ToLower();

            var sb = new StringBuilder();

            sb.Append('{');

            switch (dataType)
            {
                case "bigint":
                    {
                        sb.Append($"\"Type\":\"string\",\"IsRequired\":{isRequired}");

                        break;
                    }
                case "binary":
                    {
                        //no validation

                        break;
                    }
                case "bit":
                    {
                        sb.Append($"\"Type\":\"bool\",\"IsRequired\":{isRequired}");

                        break;
                    }
                case "date":
                case "datetime2":
                case "timestamp":
                    {
                        sb.Append($"\"Type\":\"date\",\"IsRequired\":{isRequired}");

                        break;
                    }
                case "decimal":
                    {
                        sb.Append($"\"Type\":\"float\",\"IsRequired\":{isRequired}");

                        break;
                    }
                case "int":
                    {
                        sb.Append($"\"Type\":\"int\",\"MinValue\":0,\"MaxValue\":2147483647,\"IsRequired\":{isRequired}");

                        break;
                    }
                case "smallint":
                    {
                        sb.Append($"\"Type\":\"int\",\"MinValue\":0,\"MaxValue\":65536,\"IsRequired\":{isRequired}");

                        break;
                    }
                case "nvarchar":
                case "varchar":
                    {
                        var maxLen = (int)dr["MaxLen"];

                        if (maxLen > 0)
                        {
                            sb.Append($"\"Type\":\"string\",\"MinLen\":0,\"MaxLen\":{maxLen},\"IsRequired\":{isRequired}");
                        }
                        else
                        {
                            sb.Append($"\"Type\":\"string\",\"IsRequired\":{isRequired}");
                        }

                        break;
                    }
                case "uniqueidentifier":
                    {
                        sb.Append($"\"Type\":\"guid\",\"IsRequired\":{isRequired}");

                        break;
                    }
                default:
                    throw new InvalidOperationException();
            }

            sb.Append('}');

            return sb.ToString();
        }

        private string ToValidationJson()
        {
            var json = new StringBuilder();

            json.Append('{');

            bool isFirst = true;

            var validation = _converter.GetColumnsTable(this);

            //add columns
            foreach (DataColumn dc in DataTable.Columns)
            {
                if (_converter.IsRealColumn(dc.ColumnName) &&
                    !_converter.IsIdColumn(dc.ColumnName))
                {
                    if (!isFirst)
                        json.Append(",");

                    var validationJson = GetValidationJson(validation, dc.ColumnName);

                    json.Append("\"").Append(dc.ColumnName).Append("\":").Append(validationJson);

                    isFirst = false;
                }
            }

            //add childs
            foreach (var child in Childs)
            {
                var isArray = (child.Relation == Relation.OneToMany ||
                               child.Relation == Relation.ManyToMany);

                var relatedToColumn = child.Table.GetRelatedToColumn(this);

                var childJson = child.Table.ToValidationJson();

                if (!string.IsNullOrEmpty(childJson))
                {
                    if (!isFirst)
                        json.Append(",");

                    var childName = _converter.GetChildColumnByTable(child.Table);

                    if (!isArray)
                    {
                        json.Append("\"").Append(childName).Append("\":").Append(childJson);
                    }
                    else
                    {
                        json.Append("\"").Append(childName).Append("\":[").Append(childJson).Append("]");
                    }

                    isFirst = false;
                }
            }

            json.Append('}');

            return ReturnJson(json);
        }

        private string ToRelationJson(Table relatedToTable, bool isArray)
        {
            var idColumn = _converter.GetPrimaryIdColumn(relatedToTable);

            var sb = new StringBuilder();

            sb.Append("{\"CollectionName\":\"")
              .Append(relatedToTable.Name)
              .Append("\",\"Attribute\":\"")
              .Append(idColumn)
              .Append("\",\"IsArray\":").Append(isArray.ToString().ToLower()).Append("}");

            return sb.ToString();
        }

        private string ToRelationJson()
        {
            var json = new StringBuilder();

            json.Append('{');

            bool isFirst = true;

            //add childs
            foreach (var child in Childs)
            {
                var isArray = (child.Relation == Relation.OneToMany ||
                               child.Relation == Relation.ManyToMany);

                var relatedToColumn = child.Table.GetRelatedToColumn(this);

                var childJson = child.Table.ToRelationJson();

                if (!string.IsNullOrEmpty(childJson))
                {
                    if (!isFirst)
                        json.Append(",");

                    var childName = _converter.GetChildColumnByTable(child.Table);

                    if (!isArray)
                    {
                        json.Append("\"").Append(childName).Append("\":").Append(childJson);
                    }
                    else
                    {
                        json.Append("\"").Append(childName).Append("\":[").Append(childJson).Append("]");
                    }

                    isFirst = false;
                }
            }

            //Add many-to-many childs
            foreach (var relation in ManyToManyRelations)
            {
                if (!isFirst)
                    json.Append(",");

                var columnName = relation.RelatedToTable.Name;

                var relationJson = ToRelationJson(relation.RelatedToTable, true);

                json.Append("\"").Append(columnName).Append("\":").Append(relationJson);

                isFirst = false;
            }

            json.Append('}');

            return ReturnJson(json);
        }

        private string ToLastAccessJson()
        {
            var json = new StringBuilder();

            json.Append('{');

            bool isFirst = true;

            //add columns
            var hasCreate = false;
            var hasUpdate = false;
            var hasDelete = false;

            foreach (DataColumn dc in DataTable.Columns)
            {
                if (dc.ColumnName == _converter.GetCreatedByColumn() ||
                    dc.ColumnName == _converter.GetCreatedOnColumn())
                    hasCreate = true;

                if (dc.ColumnName == _converter.GetUpdatedByColumn() ||
                    dc.ColumnName == _converter.GetUpdatedOnColumn())
                    hasUpdate = true;

                if (dc.ColumnName == _converter.GetDeletedByColumn() ||
                    dc.ColumnName == _converter.GetDeletedOnColumn())
                    hasDelete = true;
            }

            if (hasCreate || hasUpdate || hasDelete)
            {
                json.Append("\"Track\":{").Append($"\"Create\":{hasCreate.ToString().ToLower()},\"Read\":false,\"Update\":{hasUpdate.ToString().ToLower()},\"Delete\":{hasDelete.ToString().ToLower()}").Append("}");
            }

            //add childs
            foreach (var child in Childs)
            {
                var isArray = (child.Relation == Relation.OneToMany ||
                               child.Relation == Relation.ManyToMany);

                var relatedToColumn = child.Table.GetRelatedToColumn(this);

                var childJson = child.Table.ToLastAccessJson();

                if (!string.IsNullOrEmpty(childJson))
                {
                    if (!isFirst)
                        json.Append(",");

                    var childName = _converter.GetChildColumnByTable(child.Table);

                    if (!isArray)
                    {
                        json.Append("\"").Append(childName).Append("\":").Append(childJson);
                    }
                    else
                    {
                        json.Append("\"").Append(childName).Append("\":[").Append(childJson).Append("]");
                    }

                    isFirst = false;
                }
            }

            json.Append('}');

            return ReturnJson(json);
        }

        private string ReturnJson(StringBuilder json)
        {
            if (json.Length > 2)
            {
                return json.ToString();
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return $"{FullName} => Childs: {Childs.Count}, Parents: {Parents.Count}";
        }
    }
}