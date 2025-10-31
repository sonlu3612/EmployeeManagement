using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository for User entity operations
    /// Handles authentication and user management
    /// </summary>
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
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users 
                    WHERE Email = @Email
                    AND PasswordHash = @PasswordHash 
                    AND IsActive = 1";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Return null if no user found or credentials invalid
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Map first row to User object (without PasswordHash for security)
                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.ValidateLogin] Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Retrieves a user by username
        /// Does NOT return PasswordHash for security
        /// </summary>
        /// <param name="username">Username to search</param>
        /// <returns>User object or null if not found</returns>
        public User GetByEmail(string email)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email ?? (object)DBNull.Value)
                };

                // Query without PasswordHash field for security
                string sql =
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users 
                    WHERE Email = @Email";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Return null if no user found
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Map first row to User object (without PasswordHash)
                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetByEmail] Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves all users from database
        /// Does NOT return PasswordHash for security
        /// Calls sp_User_GetAll stored procedure
        /// </summary>
        /// <returns>List of all users</returns>
        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            try
            {
                // Query without PasswordHash field for security
                string sql =
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                // Map DataTable rows to User objects (without PasswordHash)
                foreach (DataRow row in dt.Rows)
                {
                    users.Add(MapDataRowToUserWithoutPassword(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetAll] Error: {ex.Message}");
                throw;
            }

            return users;
        }

        /// <summary>
        /// Retrieves a single user by ID
        /// Does NOT return PasswordHash for security
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User object or null if not found</returns>
        public User GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", id)
                };

                // Query without PasswordHash field for security
                string sql =
                    @"SELECT UserID, Username, Email, Role, IsActive, CreatedDate 
                    FROM Users 
                    WHERE UserID = @UserID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Return null if no user found
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Map first row to User object (without PasswordHash)
                return MapDataRowToUserWithoutPassword(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserRepository.GetById] Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Inserts a new user
        /// Password must be pre-hashed before calling this method
        /// Calls sp_User_Insert stored procedure
        /// </summary>
        /// <param name="entity">User object to insert</param>
        /// <returns>True if successful, false otherwise</returns>
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
                Console.WriteLine($"[UserRepository.Insert] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing user
        /// Does NOT update password (use ChangePassword instead)
        /// Calls sp_User_Update stored procedure
        /// </summary>
        /// <param name="entity">User object with updated data</param>
        /// <returns>True if successful, false otherwise</returns>
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
                Console.WriteLine($"[UserRepository.Update] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deletes a user (soft delete by setting IsActive = 0)
        /// </summary>
        /// <param name="id">User ID to delete</param>
        /// <returns>True if successful, false otherwise</returns>
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
                Console.WriteLine($"[UserRepository.Delete] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Changes user password
        /// New password must be pre-hashed before calling this method
        /// Calls sp_User_ChangePassword stored procedure
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="newPasswordHash">New pre-hashed password</param>
        /// <returns>True if successful, false otherwise</returns>
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
                Console.WriteLine($"[UserRepository.ChangePassword] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Maps a DataRow to a User object WITHOUT PasswordHash for security
        /// Handles NULL values and type conversions
        /// </summary>
        /// <param name="row">DataRow from query result</param>
        /// <returns>User object without PasswordHash</returns>
        private User MapDataRowToUserWithoutPassword(DataRow row)
        {
            return new User
            {
                // Map UserID (required field)
                UserID = row["UserID"] != DBNull.Value ? Convert.ToInt32(row["UserID"]) : 0,

                // Map Username (nullable string)
                Username = row["Username"] != DBNull.Value ? row["Username"].ToString() : null,

                // PasswordHash is NOT included for security
                PasswordHash = null,

                // Map Email (nullable string)
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,

                // Map Role (nullable string)
                Role = row["Role"] != DBNull.Value ? row["Role"].ToString() : null,

                // Map IsActive (required bool)
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,

                // Map CreatedDate (required DateTime)
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.MinValue
            };
        }
    }
}
