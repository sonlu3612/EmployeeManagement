using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository cho các thao tác dữ liệu Task
    /// Triển khai IRepository pattern cho data access
    /// </summary>
    public class TaskRepository : IRepository<Task>
    {
        /// <summary>
        /// Lấy danh sách tất cả task từ database sử dụng view vw_TaskDetails
        /// View cung cấp đầy đủ thông tin bao gồm ProjectName và EmployeeName
        /// </summary>
        /// <returns>Danh sách tất cả task</returns>
        public List<Task> GetAll()
        {
            List<Task> tasks = new List<Task>();

            try
            {
                string sql =
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description, t.AssignedTo,
                    t.CreatedBy, t.Deadline, t.Status, t.Priority, t.Progress, t.CreatedDate, t.UpdatedDate,
                    p.ProjectName,
                    e.FullName AS EmployeeName
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees e ON t.AssignedTo = e.EmployeeID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Lấy thông tin một task theo ID sử dụng view vw_TaskDetails
        /// </summary>
        /// <param name="id">ID của task</param>
        /// <returns>Đối tượng Task hoặc null nếu không tìm thấy</returns>
        public Task GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", id)
                };

                string sql =
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description, t.AssignedTo,
                    t.CreatedBy, t.Deadline, t.Status, t.Priority, t.Progress, t.CreatedDate, t.UpdatedDate,
                    p.ProjectName,
                    e.FullName AS EmployeeName
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees e ON t.AssignedTo = e.EmployeeID
                    WHERE t.TaskID = @TaskID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToTask(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Thêm mới một task
        /// Gọi stored procedure sp_Task_Insert
        /// </summary>
        /// <param name="entity">Đối tượng Task cần thêm</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Insert(Task entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", entity.ProjectID),
                    new SqlParameter("@TaskName", entity.TaskName ?? (object)DBNull.Value),
                    new SqlParameter("@Description", entity.Description ?? (object)DBNull.Value),
                    new SqlParameter("@AssignedTo", entity.AssignedTo ?? (object)DBNull.Value),
                    new SqlParameter("@CreatedBy", entity.CreatedBy),
                    new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
                    new SqlParameter("@Priority", entity.Priority ?? (object)DBNull.Value),
                    new SqlParameter("@Progress", entity.Progress),
                    new SqlParameter("@Deadline", entity.Deadline ?? (object)DBNull.Value)
                };

                string sql =
                    @"INSERT INTO Tasks (ProjectID, TaskTitle, Description, AssignedTo, CreatedBy, Status, Priority, Progress, Deadline)
                    VALUES (@ProjectID, @TaskName, @Description, @AssignedTo, @CreatedBy, @Status, @Priority, @Progress, @Deadline)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin task
        /// Gọi stored procedure sp_Task_Update
        /// </summary>
        /// <param name="entity">Đối tượng Task với dữ liệu cập nhật</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Update(Task entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", entity.TaskID),
                    new SqlParameter("@ProjectID", entity.ProjectID),
                    new SqlParameter("@TaskName", entity.TaskName ?? (object)DBNull.Value),
                    new SqlParameter("@Description", entity.Description ?? (object)DBNull.Value),
                    new SqlParameter("@AssignedTo", entity.AssignedTo ?? (object)DBNull.Value),
                    new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
                    new SqlParameter("@Priority", entity.Priority ?? (object)DBNull.Value),
                    new SqlParameter("@Progress", entity.Progress),
                    new SqlParameter("@Deadline", entity.Deadline ?? (object)DBNull.Value)
                };

                string sql =
                    @"UPDATE Tasks
                    SET ProjectID = @ProjectID,
                    TaskTitle = @TaskName,
                    Description = @Description,
                    AssignedTo = @AssignedTo,
                    Status = @Status,
                    Priority = @Priority,
                    Progress = @Progress,
                    Deadline = @Deadline,
                    UpdatedDate = GETDATE()
                    WHERE TaskID = @TaskID";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa một task
        /// Gọi stored procedure sp_Task_Delete
        /// </summary>
        /// <param name="id">ID của task cần xóa</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", id)
                };

                string sql = "DELETE FROM Tasks WHERE TaskID = @TaskID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách task theo ID dự án
        /// </summary>
        /// <param name="projectId">ID dự án</param>
        /// <returns>Danh sách task của dự án</returns>
        public List<Task> GetByProject(int projectId)
        {
            List<Task> tasks = new List<Task>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", projectId)
                };

                string sql =
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description, t.AssignedTo,
                    t.CreatedBy, t.Deadline, t.Status, t.Priority, t.Progress, t.CreatedDate, t.UpdatedDate,
                    p.ProjectName,
                    e.FullName AS EmployeeName
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees e ON t.AssignedTo = e.EmployeeID
                    WHERE t.ProjectID = @ProjectID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetByProject] Lỗi: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Lấy danh sách task được giao cho một nhân viên cụ thể
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <returns>Danh sách task được giao cho nhân viên</returns>
        public List<Task> GetByEmployee(int employeeId)
        {
            List<Task> tasks = new List<Task>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                string sql =
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description, t.AssignedTo,
                    t.CreatedBy, t.Deadline, t.Status, t.Priority, t.Progress, t.CreatedDate, t.UpdatedDate,
                    p.ProjectName,
                    e.FullName AS EmployeeName
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees e ON t.AssignedTo = e.EmployeeID
                    WHERE t.AssignedTo = @EmployeeID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetByEmployee] Lỗi: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Lấy danh sách task quá hạn (Deadline < ngày hiện tại VÀ Status != 'Done')
        /// </summary>
        /// <returns>Danh sách task quá hạn</returns>
        public List<Task> GetOverdueTasks()
        {
            List<Task> tasks = new List<Task>();

            try
            {
                string sql =
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description, t.AssignedTo,
                    t.CreatedBy, t.Deadline, t.Status, t.Priority, t.Progress, t.CreatedDate, t.UpdatedDate,
                    p.ProjectName,
                    e.FullName AS EmployeeName
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees e ON t.AssignedTo = e.EmployeeID
                    WHERE t.Deadline < GETDATE() AND t.Status != 'Done'
                    ORDER BY t.Deadline ASC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetOverdueTasks] Lỗi: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Cập nhật tiến độ task và tự động đặt Status = 'Done' nếu Progress = 100
        /// Gọi stored procedure sp_Task_UpdateProgress
        /// </summary>
        /// <param name="taskId">ID task</param>
        /// <param name="progress">Phần trăm tiến độ (0-100)</param>
        /// <returns>True nếu thành công, false nếu thất bại</returns>
        public bool UpdateProgress(int taskId, int progress)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", taskId),
                    new SqlParameter("@Progress", progress)
                };

                string sql =
                    @"UPDATE Tasks
                    SET Progress = @Progress,
                    Status = CASE WHEN @Progress = 100 THEN 'Done' ELSE Status END,
                    UpdatedDate = GETDATE()
                    WHERE TaskID = @TaskID";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.UpdateProgress] Lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy số lượng task được nhóm theo trạng thái
        /// Trả về Dictionary để dễ dàng binding với Chart
        /// </summary>
        /// <returns>Dictionary với Status là key và Count là value</returns>
        public Dictionary<string, int> GetTaskCountByStatus()
        {
            Dictionary<string, int> statusCounts = new Dictionary<string, int>();

            try
            {
                string sql =
                    @"SELECT Status, COUNT(*) AS TaskCount
                    FROM Tasks
                    GROUP BY Status";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    string status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Unknown";
                    int count = row["TaskCount"] != DBNull.Value ? Convert.ToInt32(row["TaskCount"]) : 0;
                    statusCounts[status] = count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetTaskCountByStatus] Lỗi: {ex.Message}");
                throw;
            }

            return statusCounts;
        }

        /// <summary>
        /// Chuyển đổi một DataRow thành đối tượng Task
        /// Xử lý các giá trị NULL và chuyển đổi kiểu dữ liệu
        /// Hỗ trợ cả view vw_TaskDetails và kết quả từ sp_Task
        /// </summary>
        /// <param name="row">DataRow từ kết quả query</param>
        /// <returns>Đối tượng Task</returns>
        private Task MapDataRowToTask(DataRow row)
        {
            return new Task
            {
                // Mapping TaskID (trường bắt buộc)
                TaskID = row["TaskID"] != DBNull.Value ? Convert.ToInt32(row["TaskID"]) : 0,

                // Mapping ProjectID (trường bắt buộc)
                ProjectID = row["ProjectID"] != DBNull.Value ? Convert.ToInt32(row["ProjectID"]) : 0,

                // Mapping ProjectName (cho hiển thị, từ view)
                ProjectName = row.Table.Columns.Contains("ProjectName") && row["ProjectName"] != DBNull.Value
                  ? row["ProjectName"].ToString()
                 : null,

                // Mapping TaskName (nullable string)
                TaskName = row["TaskName"] != DBNull.Value ? row["TaskName"].ToString() : null,

                // Mapping Description (nullable string)
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,

                // Mapping AssignedTo (nullable int)
                AssignedTo = row["AssignedTo"] != DBNull.Value ? (int?)Convert.ToInt32(row["AssignedTo"]) : null,

                // Mapping EmployeeName (cho hiển thị, từ view)
                EmployeeName = row.Table.Columns.Contains("EmployeeName") && row["EmployeeName"] != DBNull.Value
                    ? row["EmployeeName"].ToString() : null,

                // Mapping Status (nullable string)
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,

                // Mapping Progress (int bắt buộc)
                Progress = row["Progress"] != DBNull.Value ? Convert.ToInt32(row["Progress"]) : 0,

                // Mapping Deadline (nullable DateTime)
                Deadline = row["Deadline"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["Deadline"]) : null,

                // Mapping CreatedDate (DateTime bắt buộc)
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.MinValue
            };
        }
    }
}