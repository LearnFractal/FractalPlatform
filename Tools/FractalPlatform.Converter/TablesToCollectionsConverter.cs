using FractalPlatform.Converter.Models;
using FractalPlatform.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace FractalPlatform.Converter
{
    public class TablesToCollectionsConverter
    {
        #region Members

        public BaseRepository Repository { get; }

        private string _appName;

        private List<string> _schemas;

        private List<string> _recommendCollections;

        public List<Table> Tables { get; } = new List<Table>();

        public List<Table> Dictionaries { get; } = new List<Table>();

        public List<Collection> Collections { get; } = new List<Collection>();

        const string _deploymentPath = @"..\..\..\..";

        const string _assemblyName = "FractalPlatform.Converter.Examples";

        #endregion

        #region Constructors

        public TablesToCollectionsConverter(BaseRepository repository,
                                            string appName,
                                            List<string> schemas,
                                            List<string> recommendCollections)
        {
            Repository = repository;

            _appName = appName;

            _schemas = schemas;

            _recommendCollections = recommendCollections;
        }

        #endregion

        #region Virtual Methods

        public virtual string GetPrimaryIdColumn(Table table) => "Id";

        public virtual string GetDictionaryNameColumn(Table table) => "Name";

        public virtual string GetCreatedByColumn() => "CreatedBy";

        public virtual string GetUpdatedByColumn() => "UpdatedBy";

        public virtual string GetDeletedByColumn() => "DeletedBy";

        public virtual string GetCreatedOnColumn() => "CreatedOn";

        public virtual string GetUpdatedOnColumn() => "UpdatedOn";

        public virtual string GetDeletedOnColumn() => "DeletedOn";

        public bool IsPrimaryIdColumn(Table table, string columnName) => columnName == GetPrimaryIdColumn(table);

        public virtual bool IsIdColumn(string columnName)
        {
            return columnName.EndsWith("Id");
        }

        public virtual string GetChildColumnByTable(Table table)
        {
            return table.Name;
        }

        public virtual string GetDictionaryValueByColumn(Table table,
                                                        string columnName,
                                                        string columnValue)
        {
            foreach (var dictionary in Dictionaries)
            {
                if (columnName.Contains(dictionary.Name))
                {
                    var dt = dictionary.DataTable;

                    var drs = dt.Select($"{GetPrimaryIdColumn(table)} = '{columnValue}'");

                    if (drs.Length == 1)
                    {
                        return drs[0][GetDictionaryNameColumn(table)].ToString();
                    }
                    else
                    {
                        throw new InvalidOperationException($"Cannot extract value from {dictionary.Name} dictionary.");
                    }
                }
            }

            return null;
        }

        public virtual List<string> GetDictionaryValuesByColumn(Table table,
                                                        string columnName)
        {
            var result = new List<string>();

            foreach (var dictionary in Dictionaries)
            {
                if (columnName.Contains(dictionary.Name))
                {
                    var dt = dictionary.DataTable;

                    foreach (DataRow dr in dt.Rows)
                    {
                        var name = GetDictionaryNameColumn(dictionary);

                        result.Add("'" + dr[name].ToString() + "'");
                    }
                }
            }

            return result;
        }

        public virtual bool IsRealGuid(string id)
        {
            Guid g;

            if (!Guid.TryParse(id, out g))
                return false;

            var chrs = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

            foreach (var ch in chrs)
            {
                if (id.Replace(ch.ToString(), "").Length < id.Length / 3)
                {
                    return false;
                }
            }

            return true;
        }

        public virtual bool IsRealColumn(string columnName)
        {
            //Last access columns in relation database
            if (columnName.StartsWith("Create") ||
                columnName.StartsWith("Read") ||
                columnName.StartsWith("Update") ||
                columnName.StartsWith("Delete"))
            {
                return false;
            }

            return true;
        }

        public virtual bool IsRealTable(string tableName)
        {
            return !tableName.Contains("[_"); //ignore tables like [xx].[__TempLogs]
        }

        public virtual bool IsManyToManyTable(Table table)
        {
            //all columns ids
            foreach (var column in table.Columns)
            {
                if (column.Type != typeof(Guid))
                    return false;
            }

            //at least two parents
            if (table.Parents.Count < 2)
                return false;

            return true;
        }

        private bool Contains(List<Column> columns, string columnName) => columns.Any(x => x.Name == columnName);

        public virtual bool IsDictionaryTable(Table table)
        {
            return (table.Columns.Count == 2 &&
                    Contains(table.Columns, GetPrimaryIdColumn(table)) &&
                    Contains(table.Columns, GetDictionaryNameColumn(table)));
        }

        public virtual string GetCollectionName(Table table)
        {
            return table.Name;
        }

        public virtual DataTable GetTable(Table table)
        {
            return Repository.ExecuteReader($"SELECT * FROM {table.FullName}");
        }

        public virtual DataTable GetColumnsTable(Table table)
        {
            return Repository.ExecuteReader(@$"SELECT COLUMN_NAME ColumnName,
                                                      DATA_TYPE DataType,
                                                      CHARACTER_MAXIMUM_LENGTH MaxLen,
                                                      CASE WHEN IS_NULLABLE = 'YES' THEN CAST(0 as bit) ELSE CAST(1 as bit) END IsRequired
                                               FROM INFORMATION_SCHEMA.COLUMNS
                                               WHERE TABLE_SCHEMA = '{table.Schema}'
                                                AND TABLE_NAME = '{table.Name}'");
        }

        public virtual Table GetParentTableByChild(string tableName, string columnName)
        {
            var dt = Repository.ExecuteReader(@$"SELECT sch1.name AS ChildSchemaName,
                                                      tab1.name AS ChildTableName,
                                                      col1.name AS ChildColumn,
                                                      sch2.name AS ParentSchemaName,
                                                      tab2.name AS ParentTableName,
                                                      col2.name AS ParentColumnName
                                                FROM sys.foreign_key_columns fkc
                                                INNER JOIN sys.objects obj
                                                    ON obj.object_id = fkc.constraint_object_id
                                                INNER JOIN sys.tables tab1
                                                    ON tab1.object_id = fkc.parent_object_id
                                                INNER JOIN sys.schemas sch1
                                                    ON tab1.schema_id = sch1.schema_id
                                                INNER JOIN sys.columns col1
                                                    ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id
                                                INNER JOIN sys.tables tab2
                                                    ON tab2.object_id = fkc.referenced_object_id
                                                INNER JOIN sys.schemas sch2
                                                    ON tab2.schema_id = sch2.schema_id
                                                INNER JOIN sys.columns col2
                                                    ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id
	                                            WHERE tab1.name = '{tableName}' 
                                                    AND col1.name = '{columnName}'");

            if (dt.Rows.Count > 0)
            {
                var parentSchema = dt.Rows[0]["ParentSchema"].ToString();

                var parentTableName = dt.Rows[0]["ParentTableName"].ToString();

                return Tables.Find(x => x.Schema == parentSchema && x.Name == parentTableName);
            }
            else
            {
                return null;
            }
        }

        public virtual void CreateTables()
        {
            var dtTables = Repository.ExecuteReader(@"SELECT TABLE_SCHEMA SchemaName,
                                                             TABLE_NAME TableName
                                                      FROM INFORMATION_SCHEMA.TABLES");

            foreach (DataRow drTable in dtTables.Rows)
            {
                var schema = (string)drTable["SchemaName"];

                if (_schemas == null ||
                    _schemas.Contains(schema))
                {
                    var name = (string)drTable["TableName"];

                    var fullName = $"[{schema}].[{name}]";

                    Console.WriteLine($"Table {fullName}: {Tables.Count + 1} of {dtTables.Rows.Count}");

                    if (IsRealTable(fullName))
                    {
                        var table = new Table(this, schema, name);

                        table.Init();

                        Tables.Add(table);
                    }
                }
            }
        }

        #endregion

        #region Create Methods

        private void CreateReferences()
        {
            //set references
            foreach (var table in Tables)
            {
                table.FindChilds(Tables);
            }
        }

        private void AddManyToManyRelation(Parent parentTable, Table childTable)
        {
            var manyToManyTable = new DataTable();

            var parentRefColumn = parentTable.RefColumnName;
            var childRefColumn = childTable.Name + GetPrimaryIdColumn(childTable);

            manyToManyTable.Columns.Add(parentRefColumn);
            manyToManyTable.Columns.Add(childRefColumn);

            var parentDt = parentTable.Table.DataTable;
            var childDt = childTable.DataTable;

            foreach (DataRow parentDr in parentDt.Rows)
            {
                var parentId = parentDr[GetPrimaryIdColumn(parentTable.Table)].ToString();

                var drs = childDt.Select($"{parentRefColumn} = '{parentId}'");

                foreach (DataRow childDr in drs)
                {
                    var childId = childDr[GetPrimaryIdColumn(childTable)].ToString();

                    var dr = manyToManyTable.NewRow();

                    dr[parentRefColumn] = parentId;
                    dr[childRefColumn] = childId;

                    manyToManyTable.Rows.Add(dr);
                }
            }

            var relation = new ManyToManyRelation
            {
                ManyToManyTable = manyToManyTable,
                RelatedToTable = parentTable.Table
            };

            childTable.ManyToManyRelations.Add(relation);

            //remove other relations
            childTable.Parents.RemoveAll(x => x == parentTable);

            parentTable.Table.Childs.RemoveAll(x => x.Table == childTable);
        }

        private void AddManyToManyRelation(Parent leftTable,
                                       Table rightTable,
                                       Table manyToManyTable)
        {
            var relation = new ManyToManyRelation
            {
                ManyToManyTable = manyToManyTable.DataTable,
                RelatedToTable = rightTable
            };

            leftTable.Table.ManyToManyRelations.Add(relation);

            //remove other relations
            leftTable.Table.Childs.RemoveAll(x => x.Table == manyToManyTable);

            rightTable.Childs.RemoveAll(x => x.Table == manyToManyTable);

            Tables.Remove(manyToManyTable);
        }

        private void HandleSharedTables()
        {
            //Childs that related to two parents, it is shared collections
            foreach (var child in Tables)
            {
                if (child.Parents.Count > 1)
                {
                    var parents = child.Parents.ToList();

                    foreach (var parent in parents)
                    {
                        AddManyToManyRelation(parent, child);
                    }
                }
            }

            //Childs that participate in references, it is shared collections
            foreach (var child in Tables)
            {
                if (child.Parents.Count > 0)
                {
                    bool bFind = false;

                    foreach(var refTable in Tables)
                    {
                        if (child != refTable)
                        {
                            foreach (var relation in refTable.ManyToManyRelations)
                            {
                                if (relation.RelatedToTable == child) //table can't be related to child, only to collection
                                {
                                    bFind = true;

                                    break;
                                }
                            }
                        }

                        if (bFind)
                            break;
                    }

                    if (bFind)
                    {
                        var parents = child.Parents.ToList();

                        foreach (var parent in parents)
                        {
                            AddManyToManyRelation(parent, child);
                        }
                    }
                }
            }
        }

        private void HandleRecommendedCollections()
        {
            //set shared flag, for recommended collections
            foreach (var child in Tables)
            {
                if (_recommendCollections != null &&
                   _recommendCollections.Contains(child.Name) &&
                   child.HasParents)
                {
                    var parents = child.Parents.ToList();

                    foreach (var parent in parents)
                    {
                        AddManyToManyRelation(parent, child);
                    }
                }
            }
        }

        private void CreateCollectionsBasedOnRelations()
        {
            int i = 0;

            while (i < Tables.Count)
            {
                var table = Tables[i];

                if (table.HasNoIDs)
                {
                    Collections.Add(new Collection { RootTable = table });

                    Tables.Remove(table);
                }
                else
                {
                    i++;
                }
            }

            foreach (var table in Tables)
            {
                if (table.IsRootTable)
                {
                    Collections.Add(new Collection { RootTable = table });
                }
            }
        }

        private void CheckCycleRefs()
        {
            foreach (var table in Tables)
            {
                //if (table.FindSelfRefs())
                //    throw new InvalidOperationException($"Self reference in table: {table.Name}");

                var result = table.FindCycleRefs(Tables);

                if (!string.IsNullOrEmpty(result))
                    throw new InvalidOperationException($"Cycle reference: {result}");
            }
        }

        private void CreateDictionaries()
        {
            //create dictionaries
            var i = 0;

            while (i < Tables.Count)
            {
                var table = Tables[i];

                if (IsDictionaryTable(table))
                {
                    //remove relation between table and dictionary
                    foreach (var child in table.Childs)
                    {
                        child.Table.Parents.RemoveAll(x => x.Table == table);
                    }

                    Dictionaries.Add(table);

                    Tables.Remove(table);
                }
                else
                {
                    i++;
                }
            }

            //remove dictionaries from parents
            foreach (var table in Tables)
            {
                foreach (var dictionary in Dictionaries)
                {
                    table.Parents.RemoveAll(x => x.Table == dictionary);
                }
            }
        }

        private void CreateManyToManyRelations()
        {
            var i = 0;

            while (i < Tables.Count)
            {
                var manyToManyTable = Tables[i];

                if (IsManyToManyTable(manyToManyTable))
                {
                    //TODO think about direction of relation. Do not add manyToMany relations to both tables
                    foreach (var parent in manyToManyTable.Parents)
                    {
                        foreach (var relatedToParent in manyToManyTable.Parents.Where(x => x != parent))
                        {
                            AddManyToManyRelation(parent, relatedToParent.Table, manyToManyTable);
                        }
                    }
                }
                else
                {
                    i++;
                }
            }
        }

        #endregion

        #region Jsons Methods

        private void SaveJsonFile(string fileName, string json)
        {
            SaveFile(fileName, JsonConvert.FormatJson(json));
        } 

        private void SaveFile(string fileName, string content)
        {
            var directory = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(fileName))
                File.Delete(fileName);

            File.WriteAllText(fileName, content);
        }

        private void CreateDocuments(string collName, List<string> jsons)
        {
            var docNumber = 1;

            foreach (var json in jsons)
            {
                var documentPath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}\{collName}\Document\Extract\Json\{docNumber.ToString("0000000000")}.json";

                SaveJsonFile(documentPath, json);

                docNumber++;
            }
        }

        private void CreateDimensionDocuments(string collName, string dimension, List<string> jsons)
        {
            var docNumber = 1;

            foreach (var json in jsons)
            {
                var documentPath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}\{collName}\{dimension}\Document\Extract\Json\{docNumber.ToString("0000000000")}.json";

                SaveJsonFile(documentPath, json);

                docNumber++;
            }
        }

        private void CreateDimension(string collName, string dimension, string json)
        {
            var documentPath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}\{collName}\{dimension}\Document\Extract\Json\0000000000.json";

            SaveJsonFile(documentPath, json);
        }

        private void CreateCollections()
        {
            foreach (var collection in Collections)
            {
                var collInfo = collection.RootTable.ToCollectionJsons();

                var collName = collection.RootTable.Name;

                CreateDocuments(collName, collInfo.Documents);

                if (collInfo.HasEnum)
                {
                    CreateDimension(collName, "Enum", collInfo.Enum);
                }

                if (collInfo.HasUI)
                {
                    CreateDimension(collName, "UI", collInfo.UI);
                }

                if (collInfo.HasValidation)
                {
                    CreateDimension(collName, "Validation", collInfo.Validation);
                }

                if (collInfo.HasRelation)
                {
                    CreateDimension(collName, "Relation", collInfo.Relation);
                }

                if (collInfo.HasLastAccess)
                {
                    CreateDimension(collName, "LastAccess", collInfo.LastAccess);

                    CreateDimensionDocuments(collName, "LastAccess", collInfo.LastAccessDocuments);
                }
            }
        }

        private void CreateDashboard()
        {
            var document = new StringBuilder();
            var ui = new StringBuilder();
            var evnt = new StringBuilder();

            document.Append('{');
            ui.Append('{');
            evnt.Append('{');

            var isFirst = true;

            foreach (var collection in Collections)
            {
                if (!isFirst)
                {
                    document.Append(',');
                    ui.Append(',');
                    evnt.Append(',');
                }

                var collName = collection.RootTable.Name;

                document.Append("\"").Append(collName).Append("\":\"").Append(collName).Append("\"");
                ui.Append("\"").Append(collName).Append("\":{\"ControlType\":\"Button\"}");
                evnt.Append("\"").Append(collName).Append("\":{\"OnClick\":\"").Append(collName).Append("\"}");

                isFirst = false;
            }

            document.Append('}');
            ui.Append('}');
            evnt.Append('}');

            //Document
            var documentPath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}\Dashboard\Document\Extract\Json\0000000001.json";

            SaveJsonFile(documentPath, document.ToString());

            var uiPath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}\Dashboard\UI\Document\Extract\Json\0000000000.json";

            SaveJsonFile(uiPath, ui.ToString());

            var evntPath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}\Dashboard\Event\Document\Extract\Json\0000000000.json";

            SaveJsonFile(evntPath, evnt.ToString());
        }

        private void ChangeApplication()
        {
            var oldAppPath = $@"{_deploymentPath}\{_assemblyName}\Applications\{_appName}\TemplateApplication.cs";

            var content = File.ReadAllText(oldAppPath);

            File.Delete(oldAppPath);

            content = content.Replace("Template", _appName);

            var newAppPath = $@"{_deploymentPath}\{_assemblyName}\Applications\{_appName}\{_appName}Application.cs";

            SaveFile(newAppPath, content);
        }

        #endregion

        private void PrepareCollections()
        {
            Console.WriteLine($"Create tables ...");

            CreateTables();

            Console.WriteLine($"Create dictionaries ...");

            CreateDictionaries();

            Console.WriteLine($"Create references ...");

            CreateReferences();

            Console.WriteLine($"Create many-to-many relations ...");

            CreateManyToManyRelations();

            Console.WriteLine($"Find shared tables ...");

            HandleSharedTables();

            Console.WriteLine($"Find recommended collections ...");

            HandleRecommendedCollections();

            Console.WriteLine($"Check cycle/self references ...");

            CheckCycleRefs();

            Console.WriteLine($"Create collections based on relations ...");

            CreateCollectionsBasedOnRelations();
        }

        private void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Delete old target directories
            if (Directory.Exists(targetPath))
            {
                foreach (string dirPath in Directory.GetDirectories(targetPath, "*", SearchOption.TopDirectoryOnly))
                {
                    Directory.Delete(dirPath, true);
                }
            }
            else
            {
                Directory.CreateDirectory(targetPath);
            }

            //Create new target directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        private void CreateApplication()
        {
            //create app
            var sourceAppPath = $@"{_deploymentPath}\{_assemblyName}\Applications\Template";

            var targetAppPath = $@"{_deploymentPath}\{_assemblyName}\Applications\{_appName}";

            CopyFilesRecursively(sourceAppPath, targetAppPath);

            //copy database
            var sourceDatabasePath = $@"{_deploymentPath}\{_assemblyName}\Databases\Template";

            var targetDatabasePath = $@"{_deploymentPath}\{_assemblyName}\Databases\{_appName}";

            CopyFilesRecursively(sourceDatabasePath, targetDatabasePath);

            //create dashboard
            CreateDashboard();

            //change application to new name
            ChangeApplication();
        }

        public void Run()
        {
            Console.WriteLine($"Prepare collections ...");

            PrepareCollections();

            Console.WriteLine($"Create application ...");

            CreateApplication();

            Console.WriteLine($"Create collections ...");

            CreateCollections();

            Console.WriteLine($"Done.");
        }
    }
}
