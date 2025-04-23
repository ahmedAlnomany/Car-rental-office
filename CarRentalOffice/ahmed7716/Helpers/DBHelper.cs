using System.Data;
using System.Data.SqlClient;

namespace ahmed7716.Helpers
{

    public class DBHelper
    {
        private static string connectionString = @"Data Source=AHMED;Initial Catalog=CarRentalOffice;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public static DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                CloseConnection(connection);
            }

            return dataTable;
        }

        public static DataTable ExecuteTableFunction(string functionName, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                SqlCommand command = new SqlCommand($"SELECT * FROM {functionName}()", connection);

                if (parameters != null)
                {
                    command.CommandText = $"SELECT * FROM {functionName}({string.Join(",", parameters.Select(p => p.ParameterName))})";
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                CloseConnection(connection);
            }

            return dataTable;
        }

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
          
                throw;
            }
            finally
            {
                CloseConnection(connection);
            }

            return dataTable;
        }
    }
}

