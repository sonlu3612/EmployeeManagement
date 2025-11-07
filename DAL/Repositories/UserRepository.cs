using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace EmployeeManagement.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly HashPassword hp = new HashPassword();
        /// <summary>
        /// Validates user login credentials
        /// Password must be pre-hashed before calling this method
        /// Only returns active users (IsActive = 1)
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="passwordHash">Pre-hashed password</param>
        /// <returns>User object if valid, null if invalid</returns>
        public User ValidateLogin(string email, string passwordHash)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email ?? (object)DBNull.Value),
                    new SqlParameter("@PasswordHash", passwordHash ?? (object)DBNull.Value)
                };
                // Query with email, password hash, and check IsActive = 1
                string sql =
                    @"SELECT UserID, Phone, Email, IsActive, CreatedDate
                      FROM Users
                      WHERE Email = @Email
                      AND PasswordHash = @PasswordHash
                      AND IsActive = 1";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                User user = MapDataRowToUserWithoutPassword(dt.Rows[0]);
                user.Roles = GetUserRoles(user.UserID);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.ValidateLogin] Lỗi: {ex.Message}");
                throw;
            }
        }
        public User GetByEmail(string email)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email ?? (object)DBNull.Value)
                };
                string sql =
                    @"SELECT UserID, Phone, Email, IsActive, CreatedDate
                      FROM Users
                      WHERE Email = @Email";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                User user = MapDataRowToUserWithoutPassword(dt.Rows[0]);
                user.Roles = GetUserRoles(user.UserID);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetByEmail] Lỗi: {ex.Message}");
                throw;
            }
        }
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                string sql =
                    @"SELECT UserID, Phone, Email, IsActive, CreatedDate
                      FROM Users";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    User user = MapDataRowToUserWithoutPassword(row);
                    user.Roles = GetUserRoles(user.UserID);
                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }
            return users;
        }
        public User GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", id)
                };
                string sql =
                    @"SELECT UserID, Phone, Email, IsActive, CreatedDate
                      FROM Users
                      WHERE UserID = @UserID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                User user = MapDataRowToUserWithoutPassword(dt.Rows[0]);
                user.Roles = GetUserRoles(user.UserID);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }
        public bool Insert(User entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Phone", entity.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@Email", entity.Email ?? (object)DBNull.Value),
                    new SqlParameter("@PasswordHash", entity.PasswordHash ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", entity.IsActive)
                };
                string sql =
                    @"INSERT INTO Users (Phone, Email, PasswordHash, IsActive)
                      VALUES (@Phone, @Email, @PasswordHash, @IsActive)";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                // Insert roles into UserRoles if any
                if (entity.Roles != null && entity.Roles.Count > 0)
                {
                    int userId = GetLastInsertedUserId(); // Helper to get last ID, or use OUTPUT in insert
                    InsertUserRoles(userId, entity.Roles);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }
        public bool Update(User entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", entity.UserID),
                    new SqlParameter("@Phone", entity.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@Email", entity.Email ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", entity.IsActive)
                };
                string sql =
                    @"UPDATE Users
                      SET Phone = @Phone,
                          Email = @Email,
                          IsActive = @IsActive
                      WHERE UserID = @UserID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                // Update roles: Delete existing and insert new
                DeleteUserRoles(entity.UserID);
                if (entity.Roles != null && entity.Roles.Count > 0)
                {
                    InsertUserRoles(entity.UserID, entity.Roles);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", id)
                };
                string sql = "DELETE FROM Users WHERE UserID = @UserID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                // UserRoles will be cascaded deleted if configured
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }
        public bool ChangePassword(int userId, string newPasswordHash)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@NewPasswordHash", newPasswordHash ?? (object)DBNull.Value)
                };
                string sql =
                    @"UPDATE Users
                      SET PasswordHash = @NewPasswordHash
                      WHERE UserID = @UserID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.ChangePassword] Lỗi: {ex.Message}");
                return false;
            }
        }
        private User MapDataRowToUserWithoutPassword(DataRow row)
        {
            return new User
            {
                UserID = row["UserID"] != DBNull.Value ? Convert.ToInt32(row["UserID"]) : 0,
                Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : null,
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                PasswordHash = null,
                Roles = new List<string>(), // Will be populated separately
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.Now
            };
        }

        private User MapDataRowToUser(DataRow row)
        {
            return new User
            {
                UserID = row["UserID"] != DBNull.Value ? Convert.ToInt32(row["UserID"]) : 0,
                Phone = row["Phone"]?.ToString(),
                Email = row["Email"]?.ToString(),
                PasswordHash = row["PasswordHash"]?.ToString(),
                Role = row["Role"]?.ToString(),
                IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]),
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.Now
            };
        }
        public int InsertAndReturnId(User user)
        {
            string sql = @"
                INSERT INTO Users (Phone, Email, PasswordHash, IsActive, CreatedDate)
                OUTPUT INSERTED.UserID
                VALUES (@Phone, @Email, @PasswordHash, 1, GETDATE());
            ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value),
                new SqlParameter("@Email", user.Email ?? (object)DBNull.Value),
                new SqlParameter("@PasswordHash", hp.Hash(user.PasswordHash))
            };
            object result = DatabaseHelper.ExecuteScalar(sql, parameters);
            int userId = Convert.ToInt32(result);
            // Insert roles if any
            if (user.Roles != null && user.Roles.Count > 0)
            {
                InsertUserRoles(userId, user.Roles);
            }
            return userId;
        }
        // Helper method to get roles for a user
        private List<string> GetUserRoles(int userId)
        {
            List<string> roles = new List<string>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId)
                };
                string sql =
                    @"SELECT Role
                      FROM UserRoles
                      WHERE UserID = @UserID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Role"] != DBNull.Value)
                    {
                        roles.Add(row["Role"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetUserRoles] Lỗi: {ex.Message}");
            }
            return roles;
        }
        // Helper method to insert roles for a user
        private void InsertUserRoles(int userId, List<string> roles)
        {
            foreach (string role in roles)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@Role", role ?? (object)DBNull.Value)
                };
                string sql =
                    @"INSERT INTO UserRoles (UserID, Role)
                      VALUES (@UserID, @Role)";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
            }
        }
        // Helper method to delete roles for a user
        private void DeleteUserRoles(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            string sql = "DELETE FROM UserRoles WHERE UserID = @UserID";
            DatabaseHelper.ExecuteNonQuery(sql, parameters);
        }
        // Helper to get last inserted UserID (if not using OUTPUT)
        private int GetLastInsertedUserId()
        {
            string sql = "SELECT SCOPE_IDENTITY()";
            object result = DatabaseHelper.ExecuteScalar(sql, null);
            return Convert.ToInt32(result);
        }
    }
}