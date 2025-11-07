using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        public List<Department> GetAll()
        {
            List<Department> departments = new List<Department>();
            try
            {
                string sql =
                    @"SELECT d.DepartmentID, d.DepartmentName, d.Description, d.ManagerID,
                      e.FullName AS ManagerName
                      FROM Departments d
                      LEFT JOIN Employees e ON d.ManagerID = e.EmployeeID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    departments.Add(MapDataRowToDepartment(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }
            return departments;
        }
        public Department GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", id)
                };
                string sql =
                    @"SELECT d.DepartmentID, d.DepartmentName, d.Description, d.ManagerID,
                      e.FullName AS ManagerName
                      FROM Departments d
                      LEFT JOIN Employees e ON d.ManagerID = e.EmployeeID
                      WHERE d.DepartmentID = @DepartmentID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                return MapDataRowToDepartment(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }
        public bool Insert(Department entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@DepartmentName", entity.DepartmentName ?? (object)DBNull.Value),
            new SqlParameter("@Description", entity.Description ?? (object)DBNull.Value),
            new SqlParameter("@ManagerID", entity.ManagerID ?? (object)DBNull.Value)
                };

                string sql = @"
            INSERT INTO Departments (DepartmentName, Description, ManagerID)
            VALUES (@DepartmentName, @Description, @ManagerID)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                // Nếu có ManagerID, gán thêm role "Trưởng phòng"
                if (entity.ManagerID.HasValue && entity.ManagerID.Value > 0)
                {
                    int managerId = entity.ManagerID.Value;

                    // ⚠️ Giả định ManagerID là UserID.
                    // Nếu ManagerID là EmployeeID, bạn cần map sang UserID:
                    // string getUserSql = "SELECT TOP 1 UserID FROM Users WHERE EmployeeID = @EmpID";
                    // var dt = DatabaseHelper.ExecuteQuery(getUserSql, new SqlParameter("@EmpID", managerId));
                    // if (dt.Rows.Count > 0) managerId = Convert.ToInt32(dt.Rows[0]["UserID"]);

                    // Kiểm tra đã có role "Trưởng phòng" hay chưa
                    SqlParameter[] checkParams = new SqlParameter[]
                    {
                new SqlParameter("@UserID", managerId),
                new SqlParameter("@Role", "Trưởng phòng")
                    };
                    string checkSql = @"SELECT 1 FROM UserRoles WHERE UserID = @UserID AND Role = @Role";

                    var checkDt = DatabaseHelper.ExecuteQuery(checkSql, checkParams);
                    bool exists = (checkDt != null && checkDt.Rows.Count > 0);

                    if (!exists)
                    {
                        SqlParameter[] insertRoleParams = new SqlParameter[]
                        {
                    new SqlParameter("@UserID", managerId),
                    new SqlParameter("@Role", "Quản lý phòng ban")
                        };
                        string insertRoleSql = @"INSERT INTO UserRoles (UserID, Role, AssignedDate)
                                         VALUES (@UserID, @Role, GETDATE())";

                        DatabaseHelper.ExecuteNonQuery(insertRoleSql, insertRoleParams);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Update(Department entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", entity.DepartmentID),
                    new SqlParameter("@DepartmentName", entity.DepartmentName ?? (object)DBNull.Value),
                    new SqlParameter("@Description", entity.Description ?? (object)DBNull.Value),
                    new SqlParameter("@ManagerID", entity.ManagerID ?? (object)DBNull.Value)
                };
                string sql =
                    @"UPDATE Departments
                      SET DepartmentName = @DepartmentName,
                          Description = @Description,
                          ManagerID = @ManagerID
                      WHERE DepartmentID = @DepartmentID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", id)
                };
                string sql = "DELETE FROM Departments WHERE DepartmentID = @DepartmentID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }
        public List<Department> SearchByName(string keyword)
        {
            List<Department> departments = new List<Department>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Keyword", $"%{keyword}%")
                };
                string sql =
                    @"SELECT d.DepartmentID, d.DepartmentName, d.Description, d.ManagerID,
                      e.FullName AS ManagerName
                      FROM Departments d
                      LEFT JOIN Employees e ON d.ManagerID = e.EmployeeID
                      WHERE d.DepartmentName LIKE @Keyword";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    departments.Add(MapDataRowToDepartment(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.SearchByName] Lỗi: {ex.Message}");
                throw;
            }
            return departments;
        }
        public int GetEmployeeCount(int departmentId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", departmentId)
                };
                string sql =
                    @"SELECT COUNT(*) AS EmployeeCount
                      FROM Employees
                      WHERE DepartmentID = @DepartmentID AND IsActive = 1";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count > 0 && dt.Rows[0]["EmployeeCount"] != DBNull.Value)
                {
                    return Convert.ToInt32(dt.Rows[0]["EmployeeCount"]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.GetEmployeeCount] Lỗi: {ex.Message}");
                throw;
            }
        }
        public List<Department> GetAllWithEmployeeCount()
        {
            var departments = new List<Department>();
            try
            {
                string sql = @"
                SELECT
                    d.DepartmentID,
                    d.DepartmentName,
                    d.Description,
                    d.ManagerID,
                    e.FullName AS ManagerName,
                    COUNT(emp.EmployeeID) AS EmployeeCount
                FROM Departments d
                LEFT JOIN Employees e ON d.ManagerID = e.EmployeeID
                LEFT JOIN Employees emp ON emp.DepartmentID = d.DepartmentID AND emp.IsActive = 1
                GROUP BY
                    d.DepartmentID, d.DepartmentName, d.Description, d.ManagerID, e.FullName
                ORDER BY d.DepartmentID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    departments.Add(new Department
                    {
                        DepartmentID = row["DepartmentID"] != DBNull.Value ? Convert.ToInt32(row["DepartmentID"]) : 0,
                        DepartmentName = row["DepartmentName"]?.ToString(),
                        Description = row["Description"]?.ToString(),
                        ManagerID = row["ManagerID"] != DBNull.Value ? (int?)Convert.ToInt32(row["ManagerID"]) : null,
                        ManagerName = row["ManagerName"]?.ToString(),
                        EmployeeCount = row["EmployeeCount"] != DBNull.Value ? Convert.ToInt32(row["EmployeeCount"]) : 0
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.GetAllWithEmployeeCount] Lỗi: {ex.Message}");
                throw;
            }
            return departments;
        }
        public bool AssignManager(int departmentId, int employeeId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", departmentId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                string sql =
                    @"UPDATE Departments
                      SET ManagerID = @EmployeeID
                      WHERE DepartmentID = @DepartmentID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.AssignManager] Lỗi: {ex.Message}");
                return false;
            }
        }
        public bool RemoveManager(int departmentId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", departmentId)
                };
                string sql =
                    @"UPDATE Departments
                      SET ManagerID = NULL
                      WHERE DepartmentID = @DepartmentID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.RemoveManager] Lỗi: {ex.Message}");
                return false;
            }
        }
        private Department MapDataRowToDepartment(DataRow row)
        {
            return new Department
            {
                DepartmentID = row["DepartmentID"] != DBNull.Value ? Convert.ToInt32(row["DepartmentID"]) : 0,
                DepartmentName = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : null,
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                ManagerID = row["ManagerID"] != DBNull.Value ? (int?)Convert.ToInt32(row["ManagerID"]) : null,
                ManagerName = row.Table.Columns.Contains("ManagerName") && row["ManagerName"] != DBNull.Value
                    ? row["ManagerName"].ToString()
                    : null
            };
        }
    }
}