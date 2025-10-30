using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

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
        /// Gọi stored procedure sp_Project_GetAll
        /// </summary>
        /// <returns>Danh sách tất cả dự án</returns>
        public List<Project> GetAll()
        {
            List<Project> projects = new List<Project>();

            try
            {
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Project_GetAll", null);

                // Chuyển đổi các dòng DataTable thành đối tượng Project
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
        /// Gọi stored procedure sp_Project_GetById
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

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Project_GetById", parameters);

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
        /// Thêm mới một dự án
        /// Kiểm tra StartDate <= EndDate trước khi thêm
        /// Gọi stored procedure sp_Project_Insert
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
                    new SqlParameter("@Budget", entity.Budget)
                 };

                DatabaseHelper.ExecuteStoredProcedure("sp_Project_Insert", parameters);
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
        /// Gọi stored procedure sp_Project_Update
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
                    new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
                    new SqlParameter("@Budget", entity.Budget)
                 };

                DatabaseHelper.ExecuteStoredProcedure("sp_Project_Update", parameters);
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
        /// Gọi stored procedure sp_Project_Delete
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

                DatabaseHelper.ExecuteStoredProcedure("sp_Project_Delete", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách dự án theo trạng thái
        /// </summary>
        /// <param name="status">Trạng thái dự án (ví dụ: "Planning", "InProgress", "Completed")</param>
        /// <returns>Danh sách dự án có trạng thái phù hợp</returns>
        public List<Project> GetByStatus(string status)
        {
            List<Project> projects = new List<Project>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Status", status ?? (object)DBNull.Value)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Project_GetByStatus", parameters);

                // Chuyển đổi các dòng DataTable thành đối tượng Project
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
        /// Truy vấn view vw_ProjectSummary
        /// </summary>
        /// <param name="projectId">ID dự án</param>
        /// <returns>Đối tượng ProjectStats hoặc null nếu không tìm thấy</returns>
        public ProjectStats GetProjectStats(int projectId)
        {
            try
            {
                // Truy vấn view vw_ProjectSummary
                string sql = "SELECT * FROM vw_ProjectSummary WHERE ProjectID = @ProjectID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", projectId)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Trả về null nếu không tìm thấy thống kê
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Chuyển đổi dòng đầu tiên thành đối tượng ProjectStats
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

                // Mapping Status (nullable string)
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,

                // Mapping Budget (decimal bắt buộc)
                Budget = row["Budget"] != DBNull.Value ? Convert.ToDecimal(row["Budget"]) : 0m
            };
        }
    }
}
