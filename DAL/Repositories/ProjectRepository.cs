using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository cho các thao tác dữ liệu Project
    /// Triển khai IRepository pattern cho data access
    /// </summary>
    public class ProjectRepository : IRepository<Project>
    {
        /// <summary>
        /// Lấy danh sách tất cả dự án từ database
        /// </summary>
        /// <returns>Danh sách tất cả dự án</returns>
        public List<Project> GetAll()
        {
            List<Project> projects = new List<Project>();
            try
            {
                string sql = @"
            SELECT
                p.ProjectID,
                p.ProjectName,
                p.Description,
                p.StartDate,
                p.EndDate,
                p.Status,
                p.CreatedBy,
                e.FullName AS CreatedByName,
                p.ManagerBy,
                m.FullName AS ManagerName,
                p.CreatedDate
            FROM Projects p
            LEFT JOIN Employees e ON p.CreatedBy = e.EmployeeID
            LEFT JOIN Employees m ON p.ManagerBy = m.EmployeeID
            ORDER BY p.ProjectID DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    projects.Add(MapDataRowToProject(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }
            return projects;
        }
        /// <summary>
        /// Lấy thông tin một dự án theo ID
        /// </summary>
        /// <param name="id">ID dự án</param>
        /// <returns>Đối tượng Project hoặc null nếu không tìm thấy</returns>
        public Project GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", id)
                };
                string sql =
                    @"SELECT ProjectID, ProjectName, Description, StartDate, EndDate, Status, CreatedBy, ManagerBy, CreatedDate
                    FROM Projects
                    WHERE ProjectID = @ProjectID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                // Trả về null nếu không tìm thấy dự án
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                // Chuyển đổi dòng đầu tiên thành đối tượng Project
                return MapDataRowToProject(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Lấy danh sách project được giao cho một nhân viên cụ thể
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Danh sách project được giao cho nhân viên</returns>
        public List<Project> GetByEmployee(int employeeId)
        {
            List<Project> projects = new List<Project>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };
                string sql =
                    @"SELECT p.ProjectID,p.ProjectName,p.Description,p.StartDate,p.EndDate, p.Status,
                    p.CreatedBy,
                    e.FullName AS CreatedByName,
                    p.ManagerBy,
                    m.FullName AS ManagerName,
                    p.CreatedDate
                    FROM Projects p
                    LEFT JOIN Employees e ON p.CreatedBy = e.EmployeeID
                    LEFT JOIN Employees m ON p.ManagerBy = m.EmployeeID
                    WHERE p.CreatedBy = @EmployeeID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    projects.Add(MapDataRowToProject(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRespository.GetByEmployee] Lỗi: {ex.Message}");
                throw;
            }
            return projects;
        }
        /// <summary>
        /// Thêm mới một dự án
        /// Kiểm tra StartDate <= EndDate trước khi thêm
        /// </summary>
        /// <param name="entity">Đối tượng Project cần thêm</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Insert(Project entity)
        {
            try
            {
                // Kiểm tra StartDate <= EndDate
                if (entity.EndDate.HasValue && entity.StartDate > entity.EndDate.Value)
                {
                    Console.WriteLine("[ProjectRepository.Insert] Lỗi validation: StartDate phải <= EndDate");
                    return false;
                }

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ProjectName", entity.ProjectName ?? (object)DBNull.Value),
            new SqlParameter("@Description", entity.Description ?? (object)DBNull.Value),
            new SqlParameter("@StartDate", entity.StartDate),
            new SqlParameter("@EndDate", entity.EndDate ?? (object)DBNull.Value),
            new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
            new SqlParameter("@CreatedBy", entity.CreatedBy),
            new SqlParameter("@ManagerBy", entity.ManagerBy ?? (object)DBNull.Value)
                };

                string sql =
                    @"INSERT INTO Projects (ProjectName, Description, StartDate, EndDate, Status, CreatedBy, ManagerBy)
              VALUES (@ProjectName, @Description, @StartDate, @EndDate, @Status, @CreatedBy, @ManagerBy)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                if (entity.ManagerBy.HasValue && entity.ManagerBy.Value > 0)
                {
                    int managerId = entity.ManagerBy.Value;

                    SqlParameter[] checkParams = new SqlParameter[]
                    {
                new SqlParameter("@UserID", managerId),
                new SqlParameter("@Role", "Quản lý dự án")
                    };
                    string checkSql = @"SELECT 1 FROM UserRoles WHERE UserID = @UserID AND Role = @Role";

                    var checkDt = DatabaseHelper.ExecuteQuery(checkSql, checkParams);
                    bool exists = (checkDt != null && checkDt.Rows.Count > 0);

                    if (!exists)
                    {
                        // Chèn role mới
                        SqlParameter[] insertRoleParams = new SqlParameter[]
                        {
                    new SqlParameter("@UserID", managerId),
                    new SqlParameter("@Role", "Quản lý dự án")
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
                Console.WriteLine($"[ProjectRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin dự án
        /// Kiểm tra StartDate <= EndDate trước khi cập nhật
        /// </summary>
        /// <param name="entity">Đối tượng Project với dữ liệu cập nhật</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Update(Project entity)
        {
            try
            {
                // Validation: StartDate <= EndDate
                if (entity.EndDate.HasValue && entity.StartDate > entity.EndDate.Value)
                {
                    Console.WriteLine("[ProjectRepository.Update] Lỗi validation: StartDate phải <= EndDate");
                    return false;
                }

                // Lấy manager cũ trước khi cập nhật
                int? oldManagerId = null;
                try
                {
                    SqlParameter[] getOldParams = new SqlParameter[]
                    {
                new SqlParameter("@ProjectID", entity.ProjectID)
                    };
                    DataTable dtOld = DatabaseHelper.ExecuteQuery(
                        "SELECT ManagerBy FROM Projects WHERE ProjectID = @ProjectID",
                        getOldParams);
                    if (dtOld.Rows.Count > 0 && dtOld.Rows[0]["ManagerBy"] != DBNull.Value)
                        oldManagerId = Convert.ToInt32(dtOld.Rows[0]["ManagerBy"]);
                }
                catch
                {
                    // nếu không lấy được manager cũ thì vẫn tiếp tục (sẽ log trong catch tổng)
                }

                // Thực hiện update project
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ProjectID", entity.ProjectID),
            new SqlParameter("@ProjectName", entity.ProjectName ?? (object)DBNull.Value),
            new SqlParameter("@Description", entity.Description ?? (object)DBNull.Value),
            new SqlParameter("@StartDate", entity.StartDate),
            new SqlParameter("@EndDate", entity.EndDate ?? (object)DBNull.Value),
            new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
            new SqlParameter("@ManagerBy", entity.ManagerBy ?? (object)DBNull.Value)
                 };
                string sql =
                    @"UPDATE Projects
              SET ProjectName = @ProjectName,
                  Description = @Description,
                  StartDate = @StartDate,
                  EndDate = @EndDate,
                  Status = @Status,
                  ManagerBy = @ManagerBy
              WHERE ProjectID = @ProjectID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                // Nếu manager mới được gán (khác null) thì đảm bảo họ có role "Quản lý dự án"
                if (entity.ManagerBy.HasValue && entity.ManagerBy.Value > 0)
                {
                    SqlParameter[] checkParams = new SqlParameter[]
                    {
                new SqlParameter("@UserID", entity.ManagerBy.Value),
                new SqlParameter("@Role", "Quản lý dự án")
                    };
                    string checkSql = @"SELECT 1 FROM UserRoles WHERE UserID = @UserID AND Role = @Role";
                    var checkDt = DatabaseHelper.ExecuteQuery(checkSql, checkParams);
                    bool exists = (checkDt != null && checkDt.Rows.Count > 0);
                    if (!exists)
                    {
                        SqlParameter[] insertRoleParams = new SqlParameter[]
                        {
                    new SqlParameter("@UserID", entity.ManagerBy.Value),
                    new SqlParameter("@Role", "Quản lý dự án")
                        };
                        string insertRoleSql = @"INSERT INTO UserRoles (UserID, Role, AssignedDate)
                                         VALUES (@UserID, @Role, GETDATE())";
                        DatabaseHelper.ExecuteNonQuery(insertRoleSql, insertRoleParams);
                    }
                }

                // Nếu manager cũ tồn tại và khác manager mới => kiểm tra xem có còn project nào do họ quản lý không
                if (oldManagerId.HasValue && (!entity.ManagerBy.HasValue || oldManagerId.Value != entity.ManagerBy.Value))
                {
                    SqlParameter[] countParams = new SqlParameter[]
                    {
                new SqlParameter("@OldManagerID", oldManagerId.Value),
                new SqlParameter("@ThisProjectID", entity.ProjectID)
                    };
                    string countSql = @"SELECT COUNT(1) AS C FROM Projects WHERE ManagerBy = @OldManagerID AND ProjectID <> @ThisProjectID";
                    var countDt = DatabaseHelper.ExecuteQuery(countSql, countParams);
                    int remaining = 0;
                    if (countDt != null && countDt.Rows.Count > 0 && countDt.Rows[0]["C"] != DBNull.Value)
                        remaining = Convert.ToInt32(countDt.Rows[0]["C"]);

                    if (remaining == 0)
                    {
                        // Xóa role "Quản lý dự án" của manager cũ (nếu có)
                        SqlParameter[] deleteRoleParams = new SqlParameter[]
                        {
                    new SqlParameter("@UserID", oldManagerId.Value),
                    new SqlParameter("@Role", "Quản lý dự án")
                        };
                        string deleteRoleSql = @"DELETE FROM UserRoles WHERE UserID = @UserID AND Role = @Role";
                        DatabaseHelper.ExecuteNonQuery(deleteRoleSql, deleteRoleParams);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                // Lấy manager của project trước khi xóa
                int? managerId = null;
                try
                {
                    SqlParameter[] getParams = new SqlParameter[]
                    {
                new SqlParameter("@ProjectID", id)
                    };
                    var dt = DatabaseHelper.ExecuteQuery("SELECT ManagerBy FROM Projects WHERE ProjectID = @ProjectID", getParams);
                    if (dt.Rows.Count > 0 && dt.Rows[0]["ManagerBy"] != DBNull.Value)
                        managerId = Convert.ToInt32(dt.Rows[0]["ManagerBy"]);
                }
                catch
                {
                    // nếu không lấy được manager thì vẫn tiếp tục xóa; sẽ log ở catch tổng
                }

                // Xóa project
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ProjectID", id)
                };
                string sql = "DELETE FROM Projects WHERE ProjectID = @ProjectID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                // Nếu có manager, kiểm tra họ còn quản lý project nào nữa không
                if (managerId.HasValue)
                {
                    SqlParameter[] countParams = new SqlParameter[]
                    {
                new SqlParameter("@ManagerID", managerId.Value)
                    };
                    string countSql = @"SELECT COUNT(1) AS C FROM Projects WHERE ManagerBy = @ManagerID";
                    var countDt = DatabaseHelper.ExecuteQuery(countSql, countParams);
                    int remaining = 0;
                    if (countDt != null && countDt.Rows.Count > 0 && countDt.Rows[0]["C"] != DBNull.Value)
                        remaining = Convert.ToInt32(countDt.Rows[0]["C"]);

                    if (remaining == 0)
                    {
                        // Xóa role "Quản lý dự án" nếu họ không còn quản lý project nào
                        SqlParameter[] deleteRoleParams = new SqlParameter[]
                        {
                    new SqlParameter("@UserID", managerId.Value),
                    new SqlParameter("@Role", "Quản lý dự án")
                        };
                        string deleteRoleSql = @"DELETE FROM UserRoles WHERE UserID = @UserID AND Role = @Role";
                        DatabaseHelper.ExecuteNonQuery(deleteRoleSql, deleteRoleParams);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy thống kê dự án bao gồm tiến độ hoàn thành task
        /// </summary>
        /// <param name="projectId">ID dự án</param>
        /// <returns>Đối tượng ProjectStats hoặc null nếu không tìm thấy</returns>
        public ProjectStats GetProjectStats(int projectId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", projectId)
                };
                string sql =
                    @"SELECT
                    p.ProjectID,
                    p.ProjectName,
                    COUNT(t.TaskID) AS TotalTasks,
                    SUM(CASE WHEN t.Status = N'Hoàn thành' THEN 1 ELSE 0 END) AS CompletedTasks,
                    CASE
                    WHEN COUNT(t.TaskID) > 0 THEN
                    CAST(SUM(CASE WHEN t.Status = N'Hoàn thành' THEN 1 ELSE 0 END) AS DECIMAL(10,2)) / COUNT(t.TaskID) * 100
                    ELSE 0
                    END AS CompletionPercentage
                    FROM Projects p
                    LEFT JOIN Tasks t ON p.ProjectID = t.ProjectID
                    WHERE p.ProjectID = @ProjectID
                    GROUP BY p.ProjectID, p.ProjectName";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                DataRow row = dt.Rows[0];
                return new ProjectStats
                {
                    ProjectID = row["ProjectID"] != DBNull.Value ? Convert.ToInt32(row["ProjectID"]) : 0,
                    ProjectName = row["ProjectName"] != DBNull.Value ? row["ProjectName"].ToString() : null,
                    TotalTasks = row["TotalTasks"] != DBNull.Value ? Convert.ToInt32(row["TotalTasks"]) : 0,
                    CompletedTasks = row["CompletedTasks"] != DBNull.Value ? Convert.ToInt32(row["CompletedTasks"]) : 0,
                    CompletionPercentage = row["CompletionPercentage"] != DBNull.Value ? Convert.ToDecimal(row["CompletionPercentage"]) : 0m
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetProjectStats] Lỗi: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Chuyển đổi một DataRow thành đối tượng Project
        /// Xử lý các giá trị NULL và chuyển đổi kiểu dữ liệu
        /// </summary>
        /// <param name="row">DataRow từ kết quả query</param>
        /// <returns>Đối tượng Project</returns>
        private Project MapDataRowToProject(DataRow row)
        {
            return new Project
            {
                // Mapping ProjectID (trường bắt buộc)
                ProjectID = row["ProjectID"] != DBNull.Value ? Convert.ToInt32(row["ProjectID"]) : 0,
                // Mapping ProjectName (nullable string)
                ProjectName = row["ProjectName"] != DBNull.Value ? row["ProjectName"].ToString() : null,
                // Mapping Description (nullable string)
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                // Mapping StartDate (DateTime bắt buộc)
                StartDate = row["StartDate"] != DBNull.Value ? Convert.ToDateTime(row["StartDate"]) : DateTime.MinValue,
                // Mapping EndDate (nullable DateTime)
                EndDate = row["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["EndDate"]) : null,
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : 0,
                CreatedByName = row.Table.Columns.Contains("CreatedByName") && row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                ManagerBy = row.Table.Columns.Contains("ManagerBy") && row["ManagerBy"] != DBNull.Value ? (int?)Convert.ToInt32(row["ManagerBy"]) : null,
                ManagerName = row.Table.Columns.Contains("ManagerName") && row["ManagerName"] != DBNull.Value ? row["ManagerName"].ToString() : null
            };
        }
        /// <summary>
        /// Lấy danh sách tất cả dự án với số lượng file đính kèm
        /// </summary>
        /// <returns>Danh sách dynamic với thông tin project và FileCount</returns>
        public List<dynamic> GetAllWithFileCount()
        {
            List<dynamic> projects = new List<dynamic>();
            try
            {
                string sql = @"
                    SELECT 
                        p.ProjectID,
                        p.ProjectName,
                        p.Description,
                        p.StartDate,
                        p.EndDate,
                        p.Status,
                        p.CreatedBy,
                        e.FullName AS CreatedByName,
                        p.ManagerBy,
                        m.FullName AS ManagerName,
                        p.CreatedDate,
                        COUNT(pf.ProjectFileID) AS FileCount
                    FROM Projects p
                    LEFT JOIN Employees e ON p.CreatedBy = e.EmployeeID
                    LEFT JOIN Employees m ON p.ManagerBy = m.EmployeeID
                    LEFT JOIN ProjectFiles pf ON p.ProjectID = pf.ProjectID
                    GROUP BY 
                        p.ProjectID,
                        p.ProjectName,
                        p.Description,
                        p.StartDate,
                        p.EndDate,
                        p.Status,
                        p.CreatedBy,
                        e.FullName,
                        p.ManagerBy,
                        m.FullName,
                        p.CreatedDate
                    ORDER BY p.ProjectID DESC";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);
                foreach (DataRow row in dt.Rows)
                {
                    projects.Add(new
                    {
                        ProjectID = Convert.ToInt32(row["ProjectID"]),
                        ProjectName = row["ProjectName"].ToString(),
                        Description = row["Description"].ToString(),
                        StartDate = Convert.ToDateTime(row["StartDate"]),
                        EndDate = row["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["EndDate"]) : null,
                        Status = row["Status"].ToString(),
                        CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                        CreatedByName = row["CreatedByName"].ToString(),
                        ManagerBy = row["ManagerBy"] != DBNull.Value ? (int?)Convert.ToInt32(row["ManagerBy"]) : null,
                        ManagerName = row["ManagerName"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        FileCount = Convert.ToInt32(row["FileCount"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetAllWithFileCount] Lỗi: {ex.Message}");
                throw;
            }
            return projects;
        }
    }
}