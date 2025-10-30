using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement.DAL
{
    /// <summary>
    /// Static helper class for database operations using ADO.NET
    /// Supports parameterized queries and stored procedures
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Gets the connection string from app.config
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ProjectManagementDB"].ConnectionString;
            }
        }

        /// <summary>
        /// Creates and returns a new SqlConnection with connection pooling enabled
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Executes a SQL query and returns results as DataTable
        /// </summary>
        /// <param name="sql">SQL query string</param>
        /// <param name="parameters">Array of SqlParameter objects</param>
        /// <returns>DataTable containing query results</returns>
        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            try
            {
                connection = GetConnection();
                connection.Open();

                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                // Add parameters if provided
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                // Log error to console (development mode)
                Console.WriteLine($"[DatabaseHelper.ExecuteQuery] Error: {ex.Message}");
                Console.WriteLine($"SQL: {sql}");
                throw;
            }
            finally
            {
                // Proper disposal of resources
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes a non-query SQL command (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="sql">SQL command string</param>
        /// <param name="parameters">Array of SqlParameter objects</param>
        /// <returns>Number of rows affected</returns>
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = GetConnection();
                connection.Open();

                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                // Add parameters if provided
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Log error to console (development mode)
                Console.WriteLine($"[DatabaseHelper.ExecuteNonQuery] Error: {ex.Message}");
                Console.WriteLine($"SQL: {sql}");
                throw;
            }
            finally
            {
                // Proper disposal of resources
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes a scalar query and returns the first column of the first row
        /// </summary>
        /// <param name="sql">SQL query string</param>
        /// <param name="parameters">Array of SqlParameter objects</param>
        /// <returns>Object containing the scalar value</returns>
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = GetConnection();
                connection.Open();

                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                // Add parameters if provided
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                object result = command.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                // Log error to console (development mode)
                Console.WriteLine($"[DatabaseHelper.ExecuteScalar] Error: {ex.Message}");
                Console.WriteLine($"SQL: {sql}");
                throw;
            }
            finally
            {
                // Proper disposal of resources
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns results as DataTable
        /// </summary>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="parameters">Array of SqlParameter objects</param>
        /// <returns>DataTable containing stored procedure results</returns>
        public static DataTable ExecuteStoredProcedure(string spName, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            try
            {
                connection = GetConnection();
                connection.Open();

                command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters if provided
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                // Log error to console (development mode)
                Console.WriteLine($"[DatabaseHelper.ExecuteStoredProcedure] Error: {ex.Message}");
                Console.WriteLine($"Stored Procedure: {spName}");
                throw;
            }
            finally
            {
                // Proper disposal of resources
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
