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
                p.CreatedDate
            FROM Projects p
            LEFT JOIN Employees e ON p.CreatedBy = e.EmployeeID
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
                    @"SELECT ProjectID, ProjectName, Description, StartDate, EndDate, Status, CreatedBy, CreatedDate
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
                    p.CreatedDate
                    FROM Projects p
                    LEFT JOIN Employees e ON p.CreatedBy = e.EmployeeID
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
                    new SqlParameter("@CreatedBy", entity.CreatedBy)
                 };
                string sql =
                    @"INSERT INTO Projects (ProjectName, Description, StartDate, EndDate, Status, CreatedBy)
                    VALUES (@ProjectName, @Description, @StartDate, @EndDate, @Status, @CreatedBy)";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
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
                // Kiểm tra StartDate <= EndDate
                if (entity.EndDate.HasValue && entity.StartDate > entity.EndDate.Value)
                {
                    Console.WriteLine("[ProjectRepository.Update] Lỗi validation: StartDate phải <= EndDate");
                    return false;
                }
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", entity.ProjectID),
                    new SqlParameter("@ProjectName", entity.ProjectName ?? (object)DBNull.Value),
                    new SqlParameter("@Description", entity.Description ?? (object)DBNull.Value),
                    new SqlParameter("@StartDate", entity.StartDate),
                    new SqlParameter("@EndDate", entity.EndDate ?? (object)DBNull.Value),
                    new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value)
                 };
                string sql =
                    @"UPDATE Projects
                    SET ProjectName = @ProjectName,
                    Description = @Description,
                    StartDate = @StartDate,
                    EndDate = @EndDate,
                    Status = @Status
                    WHERE ProjectID = @ProjectID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Xóa một dự án
        /// </summary>
        /// <param name="id">ID dự án cần xóa</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", id)
                };
                string sql = "DELETE FROM Projects WHERE ProjectID = @ProjectID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }
        public List<Project> GetByStatus(string status)
        {
            List<Project> projects = new List<Project>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                           {
                    new SqlParameter("@Status", status ?? (object)DBNull.Value)
                };
                string sql =
                    @"SELECT ProjectID, ProjectName, Description, StartDate, EndDate, Status, CreatedBy, CreatedDate
                    FROM Projects
                    WHERE Status = @Status";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    projects.Add(MapDataRowToProject(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetByStatus] Lỗi: {ex.Message}");
                throw;
            }
            return projects;
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
                    SUM(CASE WHEN t.Status = 'Done' THEN 1 ELSE 0 END) AS CompletedTasks,
                    CASE
                    WHEN COUNT(t.TaskID) > 0 THEN
                    CAST(SUM(CASE WHEN t.Status = 'Done' THEN 1 ELSE 0 END) AS DECIMAL(10,2)) / COUNT(t.TaskID) * 100
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
                CreatedByName = row.Table.Columns.Contains("CreatedByName") && row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null
            };
        }
    }
}