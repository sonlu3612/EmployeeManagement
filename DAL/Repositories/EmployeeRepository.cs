using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.DepartmentID,
                    e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                    d.DepartmentName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                // Chuyển đổi các dòng DataTable thành đối tượng Employee
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
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.DepartmentID,
                    e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                    d.DepartmentName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                    WHERE e.EmployeeID = @EmployeeID";

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
                    new SqlParameter("@DepartmentID", entity.DepartmentID ?? (object)DBNull.Value),
                    new SqlParameter("@AvatarPath", entity.AvatarPath ?? (object)DBNull.Value),
                    new SqlParameter("@Address", entity.Address ?? (object)DBNull.Value),
                    new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@IsActive", entity.IsActive)
                };

                string sql =
                    @"INSERT INTO Employees (EmployeeID, FullName, Position, DepartmentID, AvatarPath, Address, HireDate, IsActive)
                    VALUES (@EmployeeID, @FullName, @Position, @DepartmentID, @AvatarPath, @Address, @HireDate, @IsActive)";

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
                    new SqlParameter("@DepartmentID", entity.DepartmentID ?? (object)DBNull.Value),
                    new SqlParameter("@AvatarPath", entity.AvatarPath ?? (object)DBNull.Value),
                    new SqlParameter("@Address", entity.Address ?? (object)DBNull.Value),
                    new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@IsActive", entity.IsActive)
                };

                string sql =
                    @"UPDATE Employees
                    SET FullName = @FullName,
                    Position = @Position,
                    DepartmentID = @DepartmentID,
                    AvatarPath = @AvatarPath,
                    Address = @Address,
                    HireDate = @HireDate,
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
        /// Xóa mềm nhân viên (đặt IsActive = 0)
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
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.DepartmentID,
                    e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                    d.DepartmentName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                    WHERE e.DepartmentID = @DepartmentID";

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
                    @"SELECT e.EmployeeID, e.FullName, e.Position, e.DepartmentID,
                    e.AvatarPath, e.Address, e.HireDate, e.IsActive,
                    d.DepartmentName
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                    WHERE e.FullName LIKE @Keyword";

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
    }
}