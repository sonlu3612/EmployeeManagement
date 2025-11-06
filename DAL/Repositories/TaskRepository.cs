using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository cho các thao tác dữ liệu Task
    /// Triển khai IRepository pattern cho data access
    /// </summary>
    public class TaskRepository : IRepository<Task>
    {
        /// <summary>
        /// Lấy danh sách tất cả task từ database
        /// Sử dụng STRING_AGG để aggregate list employee names (cho multiple assignees)
        /// </summary>
        /// <returns>Danh sách tất cả task</returns>
        public List<Task> GetAll()
        {
            List<Task> tasks = new List<Task>();
            try
            {
                string sql =
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description,
                      t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate,
                      p.ProjectName,
                      ec.FullName AS CreatedByName,
                      STRING_AGG(e.FullName, ', ') AS AssignedEmployeeNames,
                      STRING_AGG(CONVERT(varchar(10), ta.EmployeeID), ',') AS AssignedEmployeeIDs
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees ec ON t.CreatedBy = ec.EmployeeID
                    LEFT JOIN TaskAssignments ta ON t.TaskID = ta.TaskID
                    LEFT JOIN Employees e ON ta.EmployeeID = e.EmployeeID
                    GROUP BY t.TaskID, t.ProjectID, t.TaskTitle, t.Description, t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate, p.ProjectName, ec.FullName";
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
        /// Lấy thông tin một task theo ID
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
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description,
                      t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate,
                      p.ProjectName,
                      ec.FullName AS CreatedByName,
                      STRING_AGG(e.FullName, ', ') AS AssignedEmployeeNames,
                      STRING_AGG(CONVERT(varchar(10), ta.EmployeeID), ',') AS AssignedEmployeeIDs
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees ec ON t.CreatedBy = ec.EmployeeID
                    LEFT JOIN TaskAssignments ta ON t.TaskID = ta.TaskID
                    LEFT JOIN Employees e ON ta.EmployeeID = e.EmployeeID
                    WHERE t.TaskID = @TaskID
                    GROUP BY t.TaskID, t.ProjectID, t.TaskTitle, t.Description, t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate, p.ProjectName, ec.FullName";
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
        /// Insert task trước, sau đó insert assignments nếu có (cần gọi riêng method AssignEmployees)
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
                    new SqlParameter("@CreatedBy", entity.CreatedBy),
                    new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
                    new SqlParameter("@Priority", entity.Priority ?? (object)DBNull.Value),
                    new SqlParameter("@Deadline", entity.Deadline ?? (object)DBNull.Value)
                };
                string sql =
                    @"INSERT INTO Tasks (ProjectID, TaskTitle, Description, CreatedBy, Status, Priority, Deadline)
                    VALUES (@ProjectID, @TaskName, @Description, @CreatedBy, @Status, @Priority, @Deadline)";
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
        /// Assignments cập nhật riêng (gọi method AssignEmployees)
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
                    new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
                    new SqlParameter("@Priority", entity.Priority ?? (object)DBNull.Value),
                    new SqlParameter("@Deadline", entity.Deadline ?? (object)DBNull.Value)
                };
                string sql =
                    @"UPDATE Tasks
                    SET ProjectID = @ProjectID,
                        TaskTitle = @TaskName,
                        Description = @Description,
                        Status = @Status,
                        Priority = @Priority,
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
        /// Xóa một task (cascade xóa assignments)
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
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description,
                      t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate,
                      p.ProjectName,
                      ec.FullName AS CreatedByName,
                      STRING_AGG(e.FullName, ', ') AS AssignedEmployeeNames,
                      STRING_AGG(CONVERT(varchar(10), ta.EmployeeID), ',') AS AssignedEmployeeIDs
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees ec ON t.CreatedBy = ec.EmployeeID
                    LEFT JOIN TaskAssignments ta ON t.TaskID = ta.TaskID
                    LEFT JOIN Employees e ON ta.EmployeeID = e.EmployeeID
                    WHERE t.ProjectID = @ProjectID
                    GROUP BY t.TaskID, t.ProjectID, t.TaskTitle, t.Description, t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate, p.ProjectName, ec.FullName";
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
        /// Lấy danh sách task được giao cho một nhân viên cụ thể (qua TaskAssignments)
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
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description,
                      t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate,
                      p.ProjectName,
                      ec.FullName AS CreatedByName,
                      STRING_AGG(e.FullName, ', ') AS AssignedEmployeeNames,
                      STRING_AGG(CONVERT(varchar(10), ta.EmployeeID), ',') AS AssignedEmployeeIDs
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees ec ON t.CreatedBy = ec.EmployeeID
                    LEFT JOIN TaskAssignments ta ON t.TaskID = ta.TaskID
                    LEFT JOIN Employees e ON ta.EmployeeID = e.EmployeeID
                    WHERE ta.EmployeeID = @EmployeeID
                    GROUP BY t.TaskID, t.ProjectID, t.TaskTitle, t.Description, t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate, p.ProjectName, ec.FullName";
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
                    @"SELECT t.TaskID, t.ProjectID, t.TaskTitle AS TaskName, t.Description,
                      t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate,
                      p.ProjectName,
                      ec.FullName AS CreatedByName,
                      STRING_AGG(e.FullName, ', ') AS AssignedEmployeeNames,
                      STRING_AGG(CONVERT(varchar(10), ta.EmployeeID), ',') AS AssignedEmployeeIDs
                    FROM Tasks t
                    LEFT JOIN Projects p ON t.ProjectID = p.ProjectID
                    LEFT JOIN Employees ec ON t.CreatedBy = ec.EmployeeID
                    LEFT JOIN TaskAssignments ta ON t.TaskID = ta.TaskID
                    LEFT JOIN Employees e ON ta.EmployeeID = e.EmployeeID
                    WHERE t.Deadline < GETDATE() AND t.Status != 'Done'
                    GROUP BY t.TaskID, t.ProjectID, t.TaskTitle, t.Description, t.CreatedBy, t.Deadline, t.Status, t.Priority, t.CreatedDate, t.UpdatedDate, p.ProjectName, ec.FullName
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
        /// </summary>
        /// <param name="row">DataRow từ kết quả query</param>
        /// <returns>Đối tượng Task</returns>
        private Task MapDataRowToTask(DataRow row)
        {
            var task = new Task
            {
                // Mapping TaskID (trường bắt buộc)
                TaskID = row.Table.Columns.Contains("TaskID") && row["TaskID"] != DBNull.Value ? Convert.ToInt32(row["TaskID"]) : 0,
                // Mapping ProjectID (trường bắt buộc)
                ProjectID = row.Table.Columns.Contains("ProjectID") && row["ProjectID"] != DBNull.Value ? Convert.ToInt32(row["ProjectID"]) : 0,
                // Mapping ProjectName (cho hiển thị)
                ProjectName = row.Table.Columns.Contains("ProjectName") && row["ProjectName"] != DBNull.Value
                    ? row["ProjectName"].ToString()
                    : null,
                // Mapping TaskName (nullable string)
                TaskName = row.Table.Columns.Contains("TaskName") && row["TaskName"] != DBNull.Value ? row["TaskName"].ToString() : null,
                // Mapping Description (nullable string)
                Description = row.Table.Columns.Contains("Description") && row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                // **Mapping CreatedBy (ID người tạo)**
                CreatedBy = row.Table.Columns.Contains("CreatedBy") && row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : 0,
                // Mapping tên người tạo (nếu SELECT ec.FullName AS CreatedByName)
                // UI của bạn dùng "EmployeeName" column, nên map vào property EmployeeName để bind trực tiếp
                EmployeeName = row.Table.Columns.Contains("CreatedByName") && row["CreatedByName"] != DBNull.Value
                    ? row["CreatedByName"].ToString()
                    : null,
                // Mapping AssignedEmployeeNames (aggregate string -> list)
                AssignedEmployeeNames = row.Table.Columns.Contains("AssignedEmployeeNames") && row["AssignedEmployeeNames"] != DBNull.Value
                    ? row["AssignedEmployeeNames"].ToString().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    : new List<string>(),
                // Mapping Status (nullable string)
                Status = row.Table.Columns.Contains("Status") && row["Status"] != DBNull.Value ? row["Status"].ToString() : null,
                // Mapping Priority (nullable string)
                Priority = row.Table.Columns.Contains("Priority") && row["Priority"] != DBNull.Value ? row["Priority"].ToString() : null,
                // Mapping Deadline (nullable DateTime)
                Deadline = row.Table.Columns.Contains("Deadline") && row["Deadline"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["Deadline"]) : null,
                // Mapping CreatedDate (DateTime bắt buộc)
                CreatedDate = row.Table.Columns.Contains("CreatedDate") && row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.MinValue,
                // Mapping LastUpdatedDate
                LastUpdatedDate = row.Table.Columns.Contains("UpdatedDate") && row["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(row["UpdatedDate"]) : DateTime.MinValue
            };

            // Mapping AssignedEmployeeIds (từ AssignedEmployeeIDs - "1,2,3")
            if (row.Table.Columns.Contains("AssignedEmployeeIDs") && row["AssignedEmployeeIDs"] != DBNull.Value)
            {
                var idsStr = row["AssignedEmployeeIDs"].ToString();
                var ids = idsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => { int.TryParse(s.Trim(), out var n); return n; })
                                .Where(n => n > 0)
                                .ToList();
                task.AssignedEmployeeIds = ids;
            }
            else
            {
                task.AssignedEmployeeIds = new List<int>();
            }

            return task;
        }

        // Method mới: Assign employees to task (gọi sau Insert/Update)
        public bool AssignEmployees(int taskId, List<int> employeeIds, int assignedBy)
        {
            try
            {
                // Xóa assignments cũ trước (nếu update)
                string deleteSql = "DELETE FROM TaskAssignments WHERE TaskID = @TaskID";
                DatabaseHelper.ExecuteNonQuery(deleteSql, new SqlParameter("@TaskID", taskId));
                // Insert mới
                foreach (int empId in employeeIds)
                {
                    SqlParameter[] paramsAssign = new SqlParameter[]
                    {
                        new SqlParameter("@TaskID", taskId),
                        new SqlParameter("@EmployeeID", empId),
                        new SqlParameter("@AssignedBy", assignedBy)
                    };
                    string insertSql =
                        @"INSERT INTO TaskAssignments (TaskID, EmployeeID, AssignedBy)
                        VALUES (@TASKID, @EMPLOYEEID, @ASSIGNEDBY)".Replace("@TASKID", "@TaskID").Replace("@EMPLOYEEID", "@EmployeeID").Replace("@ASSIGNEDBY", "@AssignedBy");
                    DatabaseHelper.ExecuteNonQuery(insertSql, paramsAssign);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.AssignEmployees] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool UpdateAssignmentStatus(int taskId, int employeeId, string newStatus)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", taskId),
                    new SqlParameter("@EmployeeID", employeeId),
                    new SqlParameter("@CompletionStatus", newStatus)
                };
                string sql =
                    @"UPDATE TaskAssignments
                      SET CompletionStatus = @CompletionStatus
                      WHERE TaskID = @TaskID AND EmployeeID = @EmployeeID";
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.UpdateAssignmentStatus] Lỗi: {ex.Message}");
                return false;
            }
        }

        public string GetAssignmentStatus(int taskId, int employeeId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", taskId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                string sql =
                    @"SELECT CompletionStatus
                      FROM TaskAssignments
                      WHERE TASKID = @TaskID AND EmployeeID = @EmployeeID";
                object result = DatabaseHelper.ExecuteScalar(sql, parameters);
                return result != null && result != DBNull.Value ? result.ToString() : "Pending";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetAssignmentStatus] Lỗi: {ex.Message}");
                return "Pending";
            }
        }

        /// <summary>
        /// Kiểm tra user có được phân công vào task này hay không
        /// </summary>
        public bool IsUserAssignedToTask(int taskId, int userId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", taskId),
                    new SqlParameter("@EmployeeID", userId)
                };
                string sql = @"SELECT 1 FROM TaskAssignments WHERE TaskID = @TaskID AND EmployeeID = @EmployeeID";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.IsUserAssignedToTask] Lỗi: {ex.Message}");
                return false;
            }
        }
    }
}
