using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace EmployeeManagement.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// Validates user login credentials
        /// Password must be pre-hashed before calling this method
        /// Only returns active users (IsActive = 1)
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="passwordHash">Pre-hashed password</param>
        /// <returns>User object if valid, null if invalid</returns>

        private readonly HashPassword hp = new HashPassword();
        public User ValidateLogin(string email, string passwordHash)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email ?? (object)DBNull.Value),
                    new SqlParameter("@PasswordHash", passwordHash ?? (object)DBNull.Value)
                };

                // Query with username, password hash, and check IsActive = 1
                string sql = 
                    @"SELECT UserID,Phone, Email, Role, IsActive, CreatedDate 
                    FROM Users 
                    WHERE Email = @Email
                    AND PasswordHash = @PasswordHash 
                    AND IsActive = 1";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetByPhone] Lỗi: {ex.Message}");
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
                    @"SELECT UserID, Phone, Email, Role, IsActive, CreatedDate
                    FROM Users
                    WHERE Email = @Email";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
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
                    @"SELECT UserID, Phone, Email, Role, IsActive, CreatedDate
                    FROM Users";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

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

        public User GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@UserID", id)
                };

                string sql =
                    @"SELECT UserID, Phone, Email, Role, IsActive, CreatedDate
                    FROM Users
                    WHERE UserID = @UserID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
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
                    new SqlParameter("@Role", entity.Role ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", entity.IsActive)
                };

                string sql =
                    @"INSERT INTO Users (Phone, Email, PasswordHash, Role, IsActive)
                    VALUES (@Phone, @Email, @PasswordHash, @Role, @IsActive)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
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
                    new SqlParameter("@Role", entity.Role ?? (object)DBNull.Value),
                    new SqlParameter("@IsActive", entity.IsActive)
                };

                string sql =
                    @"UPDATE Users
                    SET Phone = @Phone,
                    Email = @Email,
                    Role = @Role,
                    IsActive = @IsActive
                    WHERE UserID = @UserID";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
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
                Role = row["Role"] != DBNull.Value ? row["Role"].ToString() : null,
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.MinValue
            };
        }

        public int InsertAndReturnId(User user)
        {
            string sql = @"
                INSERT INTO Users (Phone, Email, PasswordHash, Role, IsActive, CreatedDate)
                OUTPUT INSERTED.UserID
                VALUES (@Phone, @Email, @PasswordHash, @Role, 1, GETDATE());
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value),
            new SqlParameter("@Email", user.Email ?? (object)DBNull.Value),
            new SqlParameter("@PasswordHash", hp.Hash(user.PasswordHash)),
            new SqlParameter("@Role", user.Role)
            };

            object result = DatabaseHelper.ExecuteScalar(sql, parameters);
            return Convert.ToInt32(result);
        }

    }
}