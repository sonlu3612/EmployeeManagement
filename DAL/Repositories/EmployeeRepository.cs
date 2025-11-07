using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository cho các thao tác dữ liệu Employee
    /// Triển khai IRepository pattern cho data access
    /// </summary>
    public class EmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Lấy danh sách tất cả nhân viên từ database
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                string sql =
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.Gender, e.DepartmentID,
                      e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                      d.DepartmentName
                      FROM Employees e
                      LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                      WHERE e.IsActive = 1";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    employees.Add(MapDataRowToEmployee(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }
            return employees;
        }
        /// <summary>
        /// Lấy thông tin một nhân viên theo ID
        /// </summary>
        /// <param name="id">ID nhân viên</param>
        /// <returns>Đối tượng Employee hoặc null nếu không tìm thấy</returns>
        public Employee GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", id)
                };
                string sql =
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.Gender, e.DepartmentID,
                      e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                      d.DepartmentName
                      FROM Employees e
                      LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                      WHERE e.EmployeeID = @EmployeeID AND e.IsActive = 1";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                // Trả về null nếu không tìm thấy nhân viên
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                // Chuyển đổi dòng đầu tiên thành đối tượng Employee
                return MapDataRowToEmployee(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        /// <param name="entity">Đối tượng Employee cần thêm</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Insert(Employee entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", entity.EmployeeID),
                    new SqlParameter("@FullName", entity.FullName ?? (object)DBNull.Value),
                    new SqlParameter("@Position", entity.Position ?? (object)DBNull.Value),
                    new SqlParameter("@Gender", entity.Gender ?? (object)DBNull.Value),
                    new SqlParameter("@DepartmentID", entity.DepartmentID ?? (object)DBNull.Value),
                    new SqlParameter("@AvatarPath", entity.AvatarPath ?? (object)DBNull.Value),
                    new SqlParameter("@Address", entity.Address ?? (object)DBNull.Value),
                    new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@IsActive", entity.IsActive)
                };
                string sql =
                    @"INSERT INTO Employees (EmployeeID, FullName, Position, Gender, DepartmentID, AvatarPath, Address, HireDate, IsActive)
                      VALUES (@EmployeeID, @FullName, @Position, @Gender, @DepartmentID, @AvatarPath, @Address, @HireDate, @IsActive)";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="entity">Đối tượng Employee với dữ liệu cập nhật</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Update(Employee entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", entity.EmployeeID),
                    new SqlParameter("@FullName", entity.FullName ?? (object)DBNull.Value),
                    new SqlParameter("@Position", entity.Position ?? (object)DBNull.Value),
                    new SqlParameter("@Gender", entity.Gender ?? (object)DBNull.Value),
                    new SqlParameter("@DepartmentID", entity.DepartmentID ?? (object)DBNull.Value),
                    new SqlParameter("@AvatarPath", entity.AvatarPath ?? (object)DBNull.Value),
                    new SqlParameter("@Address", entity.Address ?? (object)DBNull.Value),
                    //new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@IsActive", entity.IsActive)

                    //new SqlParameter("@AvatarData", SqlDbType.VarBinary)
                    //{
                    //    Value = entity.AvatarData ?? (object)DBNull.Value
                    //}
                };
                string sql =
                    @"UPDATE Employees
                    SET FullName = @FullName,
                    Position = @Position,
                    Gender = @Gender,
                    DepartmentID = @DepartmentID,
                    AvatarPath = @AvatarPath,
                    Address = @Address,
                    
                    IsActive = @IsActive
                    WHERE EmployeeID = @EmployeeID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Xóa nhân viên khỏi database (xóa cứng)
        /// </summary>
        /// <param name="id">ID nhân viên cần xóa</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", id)
                };
                string sql = "UPDATE Employees SET IsActive = 0 WHERE EmployeeID = @EmployeeID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Lấy danh sách nhân viên theo phòng ban
        /// </summary>
        /// <param name="deptId">ID phòng ban</param>
        /// <returns>Danh sách nhân viên trong phòng ban</returns>
        public List<Employee> GetByDepartment(int deptId)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", deptId)
                };
                string sql =
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.Gender, e.DepartmentID,
                      e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                      d.DepartmentName
                      FROM Employees e
                      LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                      WHERE e.DepartmentID = @DepartmentID AND e.IsActive = 1";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                // Chuyển đổi các dòng DataTable thành đối tượng Employee
                foreach (DataRow row in dt.Rows)
                {
                    employees.Add(MapDataRowToEmployee(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetByDepartment] Lỗi: {ex.Message}");
                throw;
            }
            return employees;
        }
        /// <summary>
        /// Tìm kiếm nhân viên theo tên (khớp một phần)
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách nhân viên phù hợp</returns>
        public List<Employee> SearchByName(string keyword)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Keyword", $"%{keyword}%")
                };
                string sql =
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.Gender, e.DepartmentID,
                      e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                      d.DepartmentName
                      FROM Employees e
                      LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                      WHERE e.FullName LIKE @Keyword AND e.IsActive = 1";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                // Chuyển đổi các dòng DataTable thành đối tượng Employee
                foreach (DataRow row in dt.Rows)
                {
                    employees.Add(MapDataRowToEmployee(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.SearchByName] Lỗi: {ex.Message}");
                throw;
            }
            return employees;
        }
        /// <summary>
        /// Chuyển đổi một DataRow thành đối tượng Employee
        /// Xử lý các giá trị NULL và chuyển đổi kiểu dữ liệu
        /// </summary>
        /// <param name="row">DataRow từ kết quả query</param>
        /// <returns>Đối tượng Employee</returns>
        private Employee MapDataRowToEmployee(DataRow row)
        {
            return new Employee
            {
                // Mapping EmployeeID (trường bắt buộc)
                EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : 0,
                // Mapping FullName (nullable string)
                FullName = row["FullName"] != DBNull.Value ? row["FullName"].ToString() : null,
                // Mapping Position (nullable string)
                Position = row["Position"] != DBNull.Value ? row["Position"].ToString() : null,
                Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : null,
                // Mapping DepartmentID (nullable int)
                DepartmentID = row["DepartmentID"] != DBNull.Value ? (int?)Convert.ToInt32(row["DepartmentID"]) : null,
                // Mapping DepartmentName (cho hiển thị, nullable string)
                DepartmentName = row.Table.Columns.Contains("DepartmentName") && row["DepartmentName"] != DBNull.Value
                    ? row["DepartmentName"].ToString()
                    : null,
                // Mapping AvatarPath (nullable string)
                AvatarPath = row["AvatarPath"] != DBNull.Value ? row["AvatarPath"].ToString() : null,
                // Mapping Address (nullable string)
                Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : null,
                // Mapping HireDate (DateTime bắt buộc)
                HireDate = row["HireDate"] != DBNull.Value ? Convert.ToDateTime(row["HireDate"]) : DateTime.MinValue,
                // Mapping IsActive (bool bắt buộc)
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false
            };
        }
        public List<Employee> GetForGrid()
        {
            List<Employee> list = new List<Employee>();
            try
            {
                string sql = @"
                SELECT e.EmployeeID, e.FullName, e.Gender, e.AvatarPath,
                       u.Email, u.Phone,
                       STRING_AGG(ur.Role, ', ') AS Roles,
                       ISNULL((
                           SELECT COUNT(DISTINCT p.ProjectID)
                           FROM Projects p
                           LEFT JOIN Tasks t ON t.ProjectID = p.ProjectID
                           LEFT JOIN TaskAssignments ta ON ta.TaskID = t.TaskID
                           WHERE p.CreatedBy = e.EmployeeID OR ta.EmployeeID = e.EmployeeID
                       ), 0) AS TotalProjects,
                       ISNULL((
                           SELECT COUNT(DISTINCT p.ProjectID)
                           FROM Projects p
                           LEFT JOIN Tasks t ON t.ProjectID = p.ProjectID
                           LEFT JOIN TaskAssignments ta ON ta.TaskID = t.TaskID
                           WHERE (p.CreatedBy = e.EmployeeID OR ta.EmployeeID = e.EmployeeID)
                             AND p.Status = 'Completed'
                       ), 0) AS CompletedProjects,
                       ISNULL((
                           SELECT COUNT(*)
                           FROM Tasks t2
                           INNER JOIN TaskAssignments ta2 ON ta2.TaskID = t2.TaskID
                           WHERE ta2.EmployeeID = e.EmployeeID
                       ), 0) AS TotalTasks,
                       ISNULL((
                           SELECT COUNT(*)
                           FROM Tasks t3
                           INNER JOIN TaskAssignments ta3 ON ta3.TaskID = t3.TaskID
                           WHERE ta3.EmployeeID = e.EmployeeID AND t3.Status = 'Done'
                       ), 0) AS CompletedTasks
                FROM Employees e
                LEFT JOIN Users u ON u.UserID = e.EmployeeID
                LEFT JOIN UserRoles ur ON ur.UserID = e.EmployeeID
                WHERE e.IsActive = 1
                GROUP BY e.EmployeeID, e.FullName, e.Gender, e.AvatarPath, u.Email, u.Phone;
                ";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    Employee emp = new Employee
                    {
                        EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : 0,
                        FullName = row["FullName"]?.ToString(),
                        Gender = row["Gender"]?.ToString(),
                        Email = row["Email"]?.ToString(),
                        Phone = row["Phone"]?.ToString(),
                        AvatarPath = row["AvatarPath"]?.ToString()
                    };
                    int totalPrj = Convert.ToInt32(row["TotalProjects"]);
                    int completedPrj = Convert.ToInt32(row["CompletedProjects"]);
                    int totalTask = Convert.ToInt32(row["TotalTasks"]);
                    int completedTask = Convert.ToInt32(row["CompletedTasks"]);
                    emp.ProjectSummary = $"{completedPrj}/{totalPrj}";
                    emp.TaskSummary = $"{completedTask}/{totalTask}";
                    // Đọc ảnh từ đường dẫn
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(emp.AvatarPath) && File.Exists(emp.AvatarPath))
                            emp.AvatarData = File.ReadAllBytes(emp.AvatarPath);
                        else
                            emp.AvatarData = null;
                    }
                    catch
                    {
                        emp.AvatarData = null;
                    }
                    list.Add(emp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetForGrid] Lỗi: {ex.Message}");
                throw;
            }
            return list;
        }
        public List<Employee> GetForGrid2(int id)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", id)
                };
                string sql = @"
                SELECT
                    e.EmployeeID,
                    e.FullName,
                    e.Position,
                    e.Gender,
                    e.Address,
                    e.HireDate,
                    e.AvatarPath,
                    u.Email,
                    u.Phone,
                    STRING_AGG(ur.Role, ', ') AS Roles
                FROM Employees e
                INNER JOIN Users u ON e.EmployeeID = u.UserID
                LEFT JOIN UserRoles ur ON ur.UserID = e.EmployeeID
                WHERE e.DepartmentID = @DepartmentID AND e.IsActive = 1
                GROUP BY e.EmployeeID, e.FullName, e.Position, e.Gender, e.Address, e.HireDate, e.AvatarPath, u.Email, u.Phone
                ORDER BY e.FullName";
                DataTable table = DatabaseHelper.ExecuteQuery(sql, parameters);
                foreach (DataRow row in table.Rows)
                {
                    Employee emp = new Employee
                    {
                        EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : 0,
                        FullName = row["FullName"]?.ToString(),
                        Position = row["Position"]?.ToString(),
                        Gender = row["Gender"]?.ToString(),
                        Address = row["Address"]?.ToString(),
                        HireDate = row["HireDate"] != DBNull.Value ? Convert.ToDateTime(row["HireDate"]) : DateTime.MinValue,
                        AvatarPath = row["AvatarPath"]?.ToString(),
                        Email = row["Email"]?.ToString(),
                        Phone = row["Phone"]?.ToString()
                    };
                    string rolesString = row["Roles"]?.ToString();
                    employees.Add(emp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetForGrid2] Lỗi: {ex.Message}");
            }
            return employees;
        }
        public List<Employee> GetByTask(int taskId)
        {
            List<Employee> list = new List<Employee>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@TaskID", taskId)
                };
                string sql = @"
            SELECT
                e.EmployeeID,
                e.FullName,
                e.Position,
                e.Gender,
                e.DepartmentID,
                e.AvatarPath,
                e.Address,
                e.HireDate,
                e.IsActive,
                u.Email,
                u.Phone,
                d.DepartmentName
            FROM Employees e
            INNER JOIN TaskAssignments ta ON ta.EmployeeID = e.EmployeeID
            LEFT JOIN Users u ON u.UserID = e.EmployeeID
            LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
            WHERE ta.TaskID = @TaskID AND e.IsActive = 1
            ORDER BY e.FullName";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    Employee emp = new Employee
                    {
                        EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : 0,
                        FullName = row["FullName"]?.ToString(),
                        Position = row["Position"]?.ToString(),
                        Gender = row["Gender"]?.ToString(),
                        DepartmentID = row["DepartmentID"] != DBNull.Value ? (int?)Convert.ToInt32(row["DepartmentID"]) : null,
                        DepartmentName = row["DepartmentName"] != DBNull.Value ? row["DepartmentName"].ToString() : null,
                        AvatarPath = row["AvatarPath"]?.ToString(),
                        Address = row["Address"]?.ToString(),
                        HireDate = row["HireDate"] != DBNull.Value ? Convert.ToDateTime(row["HireDate"]) : DateTime.MinValue,
                        IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                        Email = row["Email"]?.ToString(),
                        Phone = row["Phone"]?.ToString(),
                    };
                    // Đọc ảnh từ đường dẫn (nếu có)
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(emp.AvatarPath) && File.Exists(emp.AvatarPath))
                            emp.AvatarData = File.ReadAllBytes(emp.AvatarPath);
                        else
                            emp.AvatarData = null;
                    }
                    catch
                    {
                        emp.AvatarData = null;
                    }
                    list.Add(emp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetByTask] Lỗi: {ex.Message}");
                throw;
            }
            return list;
        }

        public Employee GetFromIdUser(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@EmployeeID", id)
                };
                string sql = @"
            SELECT 
                e.EmployeeID, e.FullName, e.Position, e.Gender, e.DepartmentID,
                e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                d.DepartmentName,
                u.Email, u.Phone, u.Role,
                ISNULL((
                    SELECT COUNT(DISTINCT p.ProjectID)
                    FROM Projects p
                    LEFT JOIN Tasks t ON t.ProjectID = p.ProjectID
                    LEFT JOIN TaskAssignments ta ON ta.TaskID = t.TaskID
                    WHERE p.CreatedBy = e.EmployeeID OR ta.EmployeeID = e.EmployeeID
                ), 0) AS TotalProjects,
                ISNULL((
                    SELECT COUNT(DISTINCT p.ProjectID)
                    FROM Projects p
                    LEFT JOIN Tasks t ON t.ProjectID = p.ProjectID
                    LEFT JOIN TaskAssignments ta ON ta.TaskID = t.TaskID
                    WHERE (p.CreatedBy = e.EmployeeID OR ta.EmployeeID = e.EmployeeID)
                      AND p.Status = N'Hoàn thành'
                ), 0) AS CompletedProjects,
                ISNULL((
                    SELECT COUNT(*) 
                    FROM Tasks t2 
                    INNER JOIN TaskAssignments ta2 ON ta2.TaskID = t2.TaskID 
                    WHERE ta2.EmployeeID = e.EmployeeID
                ), 0) AS TotalTasks,
                ISNULL((
                    SELECT COUNT(*) 
                    FROM Tasks t3 
                    INNER JOIN TaskAssignments ta3 ON ta3.TaskID = t3.TaskID 
                    WHERE ta3.EmployeeID = e.EmployeeID AND t3.Status = N'Hoàn thành'
                ), 0) AS CompletedTasks
            FROM Employees e
            LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
            LEFT JOIN Users u ON u.UserID = e.EmployeeID
            WHERE e.EmployeeID = @EmployeeID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = dt.Rows[0];
                Employee employee = MapDataRowToEmployee(row);

                // Mapping for summary
                int totalPrj = row["TotalProjects"] != DBNull.Value ? Convert.ToInt32(row["TotalProjects"]) : 0;
                int completedPrj = row["CompletedProjects"] != DBNull.Value ? Convert.ToInt32(row["CompletedProjects"]) : 0;
                int totalTask = row["TotalTasks"] != DBNull.Value ? Convert.ToInt32(row["TotalTasks"]) : 0;
                int completedTask = row["CompletedTasks"] != DBNull.Value ? Convert.ToInt32(row["CompletedTasks"]) : 0;
                employee.ProjectSummary = $"{completedPrj}/{totalPrj}";
                employee.TaskSummary = $"{completedTask}/{totalTask}";

                // Mapping for Users
                employee.Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null;
                employee.Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : null;

                // Load AvatarData
                try
                {
                    if (!string.IsNullOrWhiteSpace(employee.AvatarPath) && File.Exists(employee.AvatarPath))
                        employee.AvatarData = File.ReadAllBytes(employee.AvatarPath);
                    else
                        employee.AvatarData = null;
                }
                catch
                {
                    employee.AvatarData = null;
                }

                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }
    }
}