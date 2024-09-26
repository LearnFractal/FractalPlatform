using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FractalPlatform.Converter
{
    public class BaseRepository
    {
        public string ConnectionString { get; set; }

        private static int _paramNumber = 0;

        public BaseRepository(string connString)
        {
            ConnectionString = connString;
        }

        private string GetParameters(SqlParameterCollection parameters)
        {
            var sb = new StringBuilder();

            foreach (SqlParameter parameter in parameters)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                else
                {
                    if (!Convert.IsDBNull(parameter.Value))
                    {
                        sb.Append($"@{parameter.ParameterName} = '{parameter.Value?.ToString()}'");
                    }
                    else
                    {
                        sb.Append($"@{parameter.ParameterName} = NULL");
                    }
                }
            }

            return sb.ToString();
        }

        protected string GenerateParamName() => $"@p{++_paramNumber}";

        protected T Execute<T>(string query,
                               Func<SqlCommand, T> func,
                               CommandType commandType = CommandType.Text,
                               SqlParameter[] sqlParameters = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (var comm = new SqlCommand(query, conn))
                {
                    try
                    {
                        comm.CommandType = commandType;

                        if (sqlParameters != null)
                        {
                            comm.Parameters.AddRange(sqlParameters);

                            var parameters = GetParameters(comm.Parameters);

                            //LogHelper.LogQuery($"{query} {parameters}");
                        }
                        else
                        {
                            //LogHelper.LogQuery($"{query}");
                        }

                        return func(comm);
                    }
                    catch (Exception)
                    {
                        //LogHelper.LogErrorQuery(ex.Message, query);

                        throw;
                    }
                }
            }
        }

        public void ExecuteNonQuery(string query) => Execute(query, comm => comm.ExecuteNonQuery());

        public long ExecuteLongScalar(string query) => Execute(query, comm =>
        {
            var value = comm.ExecuteScalar();
            if (!Convert.IsDBNull(value))
            {
                return Convert.ToInt64(value);
            }
            else
            {
                return 0;
            }
        });

        public void ExecuteNonQueryProcedure(string procName, SqlParameter[] parameters)
        {
            Execute(procName,
                    comm =>
                    {
                        return comm.ExecuteNonQuery();
                    },
                    CommandType.StoredProcedure,
                    parameters);
        }

        public DateTime ExecuteDateTimeScalar(string query) => Execute(query, comm => Convert.ToDateTime(comm.ExecuteScalar()));

        public object ExecuteScalar(string query) => Execute(query, comm => comm.ExecuteScalar());

        public DataTable ExecuteReader(string query)
        {
            return Execute(query, comm =>
            {
                var dt = new DataTable();

                using (var reader = comm.ExecuteReader())
                {
                    dt.Load(reader);
                }

                return dt;
            });
        }
    }
}
