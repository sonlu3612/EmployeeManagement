using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement.DAL.Helpers
{
    /// <summary>
    /// Class helper tĩnh cho các thao tác database sử dụng ADO.NET
    /// Hỗ trợ parameterized queries và stored procedures
    /// Đảm bảo connection pooling và proper resource disposal
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Lấy connection string từ app.config
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ProjectManagementDB"].ConnectionString;
            }
        }

        /// <summary>
        /// Tạo và trả về SqlConnection mới với connection pooling được kích hoạt
        /// </summary>
        /// <returns>Đối tượng SqlConnection</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Thực thi câu lệnh SQL query và trả về kết quả dạng DataTable
        /// </summary>
        /// <param name="sql">Câu lệnh SQL query</param>
        /// <param name="parameters">Mảng các SqlParameter (tùy chọn)</param>
        /// <returns>DataTable chứa kết quả query</returns>
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

                // Thêm parameters nếu có
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
                // Ghi log lỗi ra console (chế độ development)
                Console.WriteLine($"[DatabaseHelper.ExecuteQuery] Lỗi: {ex.Message}");
                Console.WriteLine($"SQL: {sql}");
                throw;
            }
            finally
            {
                // Giải phóng tài nguyên đúng cách
                adapter?.Dispose();
                command?.Dispose();
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Thực thi câu lệnh SQL không trả về dữ liệu (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="sql">Câu lệnh SQL</param>
        /// <param name="parameters">Mảng các SqlParameter (tùy chọn)</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
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

                // Thêm parameters nếu có
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ra console (chế độ development)
                Console.WriteLine($"[DatabaseHelper.ExecuteNonQuery] Lỗi: {ex.Message}");
                Console.WriteLine($"SQL: {sql}");
                throw;
            }
            finally
            {
                // Giải phóng tài nguyên đúng cách
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
        /// Thực thi scalar query và trả về giá trị cột đầu tiên của dòng đầu tiên
        /// </summary>
        /// <param name="sql">Câu lệnh SQL query</param>
        /// <param name="parameters">Mảng các SqlParameter (tùy chọn)</param>
        /// <returns>Object chứa giá trị scalar</returns>
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

                // Thêm parameters nếu có
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                object result = command.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ra console (chế độ development)
                Console.WriteLine($"[DatabaseHelper.ExecuteScalar] Lỗi: {ex.Message}");
                Console.WriteLine($"SQL: {sql}");
                throw;
            }
            finally
            {
                // Giải phóng tài nguyên đúng cách
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
        /// Thực thi stored procedure và trả về kết quả dạng DataTable
        /// </summary>
        /// <param name="spName">Tên stored procedure</param>
        /// <param name="parameters">Mảng các SqlParameter (tùy chọn)</param>
        /// <returns>DataTable chứa kết quả từ stored procedure</returns>
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

                // Thêm parameters nếu có
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
                // Ghi log lỗi ra console (chế độ development)
                Console.WriteLine($"[DatabaseHelper.ExecuteStoredProcedure] Lỗi: {ex.Message}");
                Console.WriteLine($"Stored Procedure: {spName}");
                throw;
            }
            finally
            {
                // Giải phóng tài nguyên đúng cách
                adapter?.Dispose();
                command?.Dispose();
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        internal static void ExecuteNonQuery(string deleteSql, SqlParameter sqlParameter)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = GetConnection();
                connection.Open();
                command = new SqlCommand(deleteSql, connection);
                command.CommandType = CommandType.Text;
                // Thêm single parameter nếu có (không null)
                if (sqlParameter != null)
                {
                    command.Parameters.Add(sqlParameter);
                }
                command.ExecuteNonQuery();  // Không cần return rowsAffected vì void
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ra console (chế độ development)
                Console.WriteLine($"[DatabaseHelper.ExecuteNonQuery (single param)] Lỗi: {ex.Message}");
                Console.WriteLine($"SQL: {deleteSql}");
                throw;
            }
            finally
            {
                // Giải phóng tài nguyên đúng cách
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