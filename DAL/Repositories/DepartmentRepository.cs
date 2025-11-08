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
                // Lấy manager cũ (EmployeeID) trước khi cập nhật
                int? oldManagerEmpId = null;
                try
                {
                    SqlParameter[] getOldParams = new SqlParameter[]
                    {
                new SqlParameter("@DepartmentID", entity.DepartmentID)
                    };
                    var dtOld = DatabaseHelper.ExecuteQuery(
                        "SELECT ManagerID FROM Departments WHERE DepartmentID = @DepartmentID",
                        getOldParams);
                    if (dtOld != null && dtOld.Rows.Count > 0 && dtOld.Rows[0]["ManagerID"] != DBNull.Value)
                    {
                        oldManagerEmpId = Convert.ToInt32(dtOld.Rows[0]["ManagerID"]);
                    }
                }
                catch
                {
                    // ignore, sẽ log trong catch tổng nếu cần
                }

                // Thực hiện update Departments
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

                const string roleName = "Quản lý phòng ban";

                // Nếu manager mới được gán -> đảm bảo họ có role
                if (entity.ManagerID.HasValue)
                {
                    int newManagerEmpId = entity.ManagerID.Value;

                    // Map EmployeeID -> UserID (nếu có)
                    int? newManagerUserId = null;
                    try
                    {
                        var dtMap = DatabaseHelper.ExecuteQuery(
                            "SELECT TOP 1 UserID FROM Users WHERE EmployeeID = @EmpID",
                            new SqlParameter[] { new SqlParameter("@EmpID", newManagerEmpId) });
                        if (dtMap != null && dtMap.Rows.Count > 0 && dtMap.Rows[0]["UserID"] != DBNull.Value)
                        {
                            newManagerUserId = Convert.ToInt32(dtMap.Rows[0]["UserID"]);
                        }
                    }
                    catch { /* ignore mapping failure */ }

                    if (newManagerUserId.HasValue)
                    {
                        SqlParameter[] checkParams = new SqlParameter[]
                        {
                    new SqlParameter("@UserID", newManagerUserId.Value),
                    new SqlParameter("@Role", roleName)
                        };
                        var checkDt = DatabaseHelper.ExecuteQuery("SELECT 1 FROM UserRoles WHERE UserID = @UserID AND Role = @Role", checkParams);
                        bool exists = (checkDt != null && checkDt.Rows.Count > 0);
                        if (!exists)
                        {
                            SqlParameter[] insertRoleParams = new SqlParameter[]
                            {
                        new SqlParameter("@UserID", newManagerUserId.Value),
                        new SqlParameter("@Role", roleName)
                            };
                            string insertRoleSql = @"INSERT INTO UserRoles (UserID, Role, AssignedDate) VALUES (@UserID, @Role, GETDATE())";
                            DatabaseHelper.ExecuteNonQuery(insertRoleSql, insertRoleParams);
                        }
                    }
                }

                // Nếu có manager cũ và khác manager mới -> kiểm tra xem manager cũ còn quản lý dept nào không
                if (oldManagerEmpId.HasValue)
                {
                    bool managerChanged = !entity.ManagerID.HasValue || (entity.ManagerID.HasValue && oldManagerEmpId.Value != entity.ManagerID.Value);

                    if (managerChanged)
                    {
                        SqlParameter[] countParams = new SqlParameter[]
                        {
                    new SqlParameter("@OldManagerID", oldManagerEmpId.Value),
                    new SqlParameter("@ThisDepartmentID", entity.DepartmentID)
                        };
                        string countSql = @"SELECT COUNT(1) AS C FROM Departments WHERE ManagerID = @OldManagerID AND DepartmentID <> @ThisDepartmentID";
                        var countDt = DatabaseHelper.ExecuteQuery(countSql, countParams);
                        int remaining = 0;
                        if (countDt != null && countDt.Rows.Count > 0 && countDt.Rows[0]["C"] != DBNull.Value)
                            remaining = Convert.ToInt32(countDt.Rows[0]["C"]);

                        if (remaining == 0)
                        {
                            // Nếu không còn dept nào do manager cũ quản lý -> xoá role (nếu có)
                            // Map old EmployeeID -> UserID
                            int? oldManagerUserId = null;
                            try
                            {
                                var dtMapOld = DatabaseHelper.ExecuteQuery(
                                    "SELECT TOP 1 UserID FROM Users WHERE EmployeeID = @EmpID",
                                    new SqlParameter[] { new SqlParameter("@EmpID", oldManagerEmpId.Value) });
                                if (dtMapOld != null && dtMapOld.Rows.Count > 0 && dtMapOld.Rows[0]["UserID"] != DBNull.Value)
                                {
                                    oldManagerUserId = Convert.ToInt32(dtMapOld.Rows[0]["UserID"]);
                                }
                            }
                            catch { /* ignore */ }

                            if (oldManagerUserId.HasValue)
                            {
                                SqlParameter[] deleteRoleParams = new SqlParameter[]
                                {
                            new SqlParameter("@UserID", oldManagerUserId.Value),
                            new SqlParameter("@Role", roleName)
                                };
                                string deleteRoleSql = @"DELETE FROM UserRoles WHERE UserID = @UserID AND Role = @Role";
                                DatabaseHelper.ExecuteNonQuery(deleteRoleSql, deleteRoleParams);
                            }
                        }
                    }
                }

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
                // Lấy manager (EmployeeID) trước khi xóa
                int? managerEmpId = null;
                try
                {
                    var dt = DatabaseHelper.ExecuteQuery(
                        "SELECT ManagerID FROM Departments WHERE DepartmentID = @DepartmentID",
                        new SqlParameter[] { new SqlParameter("@DepartmentID", id) });
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ManagerID"] != DBNull.Value)
                        managerEmpId = Convert.ToInt32(dt.Rows[0]["ManagerID"]);
                }
                catch
                {
                    // ignore mapping failure
                }

                // Xóa department
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@DepartmentID", id)
                };
                string sql = "DELETE FROM Departments WHERE DepartmentID = @DepartmentID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                // Nếu có manager trước đó, kiểm tra họ còn quản lý dept nào không
                if (managerEmpId.HasValue)
                {
                    SqlParameter[] countParams = new SqlParameter[]
                    {
                new SqlParameter("@ManagerID", managerEmpId.Value)
                    };
                    string countSql = @"SELECT COUNT(1) AS C FROM Departments WHERE ManagerID = @ManagerID";
                    var countDt = DatabaseHelper.ExecuteQuery(countSql, countParams);
                    int remaining = 0;
                    if (countDt != null && countDt.Rows.Count > 0 && countDt.Rows[0]["C"] != DBNull.Value)
                        remaining = Convert.ToInt32(countDt.Rows[0]["C"]);

                    if (remaining == 0)
                    {
                        // Map EmployeeID -> UserID
                        int? managerUserId = null;
                        try
                        {
                            var dtMap = DatabaseHelper.ExecuteQuery(
                                "SELECT TOP 1 UserID FROM Users WHERE EmployeeID = @EmpID",
                                new SqlParameter[] { new SqlParameter("@EmpID", managerEmpId.Value) });
                            if (dtMap != null && dtMap.Rows.Count > 0 && dtMap.Rows[0]["UserID"] != DBNull.Value)
                                managerUserId = Convert.ToInt32(dtMap.Rows[0]["UserID"]);
                        }
                        catch { /* ignore */ }

                        if (managerUserId.HasValue)
                        {
                            const string roleName = "Quản lý phòng ban";
                            SqlParameter[] deleteRoleParams = new SqlParameter[]
                            {
                        new SqlParameter("@UserID", managerUserId.Value),
                        new SqlParameter("@Role", roleName)
                            };
                            string deleteRoleSql = @"DELETE FROM UserRoles WHERE UserID = @UserID AND Role = @Role";
                            DatabaseHelper.ExecuteNonQuery(deleteRoleSql, deleteRoleParams);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DepartmentRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
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
                    COUNT(emp.EmployeeID) 
                    + CASE WHEN d.ManagerID IS NOT NULL AND d.ManagerID IN (
                        SELECT EmployeeID FROM Employees WHERE IsActive = 1
                      ) THEN 1 ELSE 0 END AS EmployeeCount
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