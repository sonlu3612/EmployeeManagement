using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository cho các thao tác dữ liệu User
    /// Xử lý authentication và quản lý user
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// Xác thực thông tin đăng nhập của user
        /// Password phải được hash trước khi gọi method này
        /// Chỉ trả về user đang active (IsActive = 1)
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="passwordHash">Mật khẩu đã được hash</param>
        /// <returns>Đối tượng User nếu hợp lệ, null nếu không hợp lệ</returns>
        public User ValidateLogin(string username, string passwordHash)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username ?? (object)DBNull.Value),
                    new SqlParameter("@PasswordHash", passwordHash ?? (object)DBNull.Value)
                };

                // Truy vấn với username, password hash và kiểm tra IsActive = 1
                string sql =
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users 
                    WHERE Username = @Username 
                    AND PasswordHash = @PasswordHash 
                    AND IsActive = 1";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Trả về null nếu không tìm thấy user hoặc thông tin không hợp lệ
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Chuyển đổi dòng đầu tiên thành đối tượng User (không bao gồm PasswordHash vì lý do bảo mật)
                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.ValidateLogin] Lỗi: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lấy thông tin user theo username
        /// KHÔNG trả về PasswordHash vì lý do bảo mật
        /// </summary>
        /// <param name="username">Tên đăng nhập cần tìm</param>
        /// <returns>Đối tượng User hoặc null nếu không tìm thấy</returns>
        public User GetByUsername(string username)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username ?? (object)DBNull.Value)
                };

                // Truy vấn không bao gồm PasswordHash vì lý do bảo mật
                string sql =
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users 
                    WHERE Username = @Username";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Trả về null nếu không tìm thấy user
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Chuyển đổi dòng đầu tiên thành đối tượng User (không bao gồm PasswordHash)
                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetByUsername] Lỗi: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả user từ database
        /// KHÔNG trả về PasswordHash vì lý do bảo mật
        /// Gọi stored procedure sp_User_GetAll
        /// </summary>
        /// <returns>Danh sách tất cả user</returns>
        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            try
            {
                // Truy vấn không bao gồm PasswordHash vì lý do bảo mật
                string sql =
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                // Chuyển đổi các dòng DataTable thành đối tượng User (không bao gồm PasswordHash)
                foreach (DataRow row in dt.Rows)
                {
                    users.Add(MapDataRowToUserWithoutPassword(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }

            return users;
        }

        /// <summary>
        /// Lấy thông tin một user theo ID
        /// KHÔNG trả về PasswordHash vì lý do bảo mật
        /// </summary>
        /// <param name="id">ID user</param>
        /// <returns>Đối tượng User hoặc null nếu không tìm thấy</returns>
        public User GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", id)
                };

                // Truy vấn không bao gồm PasswordHash vì lý do bảo mật
                string sql =
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users 
                    WHERE UserID = @UserID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Trả về null nếu không tìm thấy user
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Chuyển đổi dòng đầu tiên thành đối tượng User (không bao gồm PasswordHash)
                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Thêm một user mới
        /// Password phải được hash trước khi gọi method này
        /// Gọi stored procedure sp_User_Insert
        /// </summary>
        /// <param name="entity">Đối tượng User cần thêm</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Insert(User entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", entity.Username ?? (object)DBNull.Value),
                    new SqlParameter("@PasswordHash", entity.PasswordHash ?? (object)DBNull.Value),
                    new SqlParameter("@Email", entity.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Role", entity.Role ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", entity.IsActive)
                };

                DatabaseHelper.ExecuteStoredProcedure("sp_User_Insert", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin một user đã tồn tại
        /// KHÔNG cập nhật mật khẩu (sử dụng ChangePassword để thay đổi mật khẩu)
        /// Gọi stored procedure sp_User_Update
        /// </summary>
        /// <param name="entity">Đối tượng User với dữ liệu đã cập nhật</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Update(User entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", entity.UserID),
                    new SqlParameter("@Username", entity.Username ?? (object)DBNull.Value),
                    new SqlParameter("@Email", entity.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Role", entity.Role ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", entity.IsActive)
                };

                DatabaseHelper.ExecuteStoredProcedure("sp_User_Update", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa một user (xóa mềm bằng cách đặt IsActive = 0)
        /// </summary>
        /// <param name="id">ID user cần xóa</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", id)
                };

                string sql = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Thay đổi mật khẩu cho user
        /// Mật khẩu mới phải được hash trước khi gọi method này
        /// Gọi stored procedure sp_User_ChangePassword
        /// </summary>
        /// <param name="userId">ID user</param>
        /// <param name="newPasswordHash">Mật khẩu mới đã được hash</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool ChangePassword(int userId, string newPasswordHash)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@NewPasswordHash", newPasswordHash ?? (object)DBNull.Value)
                };

                DatabaseHelper.ExecuteStoredProcedure("sp_User_ChangePassword", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.ChangePassword] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Chuyển đổi một DataRow thành đối tượng User KHÔNG có PasswordHash vì lý do bảo mật
        /// Xử lý giá trị NULL và chuyển đổi kiểu dữ liệu
        /// </summary>
        /// <param name="row">DataRow từ kết quả truy vấn</param>
        /// <returns>Đối tượng User không có PasswordHash</returns>
        private User MapDataRowToUserWithoutPassword(DataRow row)
        {
            return new User
            {
                // Map UserID (trường bắt buộc)
                UserID = row["UserID"] != DBNull.Value ? Convert.ToInt32(row["UserID"]) : 0,

                // Map Username (chuỗi có thể null)
                Username = row["Username"] != DBNull.Value ? row["Username"].ToString() : null,

                // PasswordHash KHÔNG được bao gồm vì lý do bảo mật
                PasswordHash = null,

                // Map Email (chuỗi có thể null)
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,

                // Map Role (chuỗi có thể null)
                Role = row["Role"] != DBNull.Value ? row["Role"].ToString() : null,

                // Map IsActive (boolean bắt buộc)
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,

                // Map CreatedDate (DateTime bắt buộc)
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.MinValue
            };
        }
    }
}
