using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository for Task entity operations
    /// Implements IRepository pattern for data access
    /// </summary>
    public class TaskRepository : IRepository<Task>
    {
        /// <summary>
        /// Retrieves all tasks from database using vw_TaskDetails view
        /// View provides full information including ProjectName and EmployeeName
        /// </summary>
        /// <returns>List of all tasks</returns>
        public List<Task> GetAll()
        {
            List<Task> tasks = new List<Task>();

            try
            {
                // Query vw_TaskDetails view for full information
                string sql = "SELECT * FROM vw_TaskDetails";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                // Map DataTable rows to Task objects
                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetAll] Error: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Retrieves a single task by ID using vw_TaskDetails view
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Task object or null if not found</returns>
        public Task GetById(int id)
        {
            try
            {
                // Query vw_TaskDetails view for full information
                string sql = "SELECT * FROM vw_TaskDetails WHERE TaskID = @TaskID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", id)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Return null if no task found
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Map first row to Task object
                return MapDataRowToTask(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetById] Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Inserts a new task
        /// Calls sp_Task_Insert stored procedure
        /// </summary>
        /// <param name="entity">Task object to insert</param>
        /// <returns>True if successful, false otherwise</returns>
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
                    new SqlParameter("@Status", entity.Status ?? (object)DBNull.Value),
                    new SqlParameter("@Progress", entity.Progress),
                    new SqlParameter("@Deadline", entity.Deadline ?? (object)DBNull.Value)
             };

                DatabaseHelper.ExecuteStoredProcedure("sp_Task_Insert", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.Insert] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing task
        /// Calls sp_Task_Update stored procedure
        /// </summary>
        /// <param name="entity">Task object with updated data</param>
        /// <returns>True if successful, false otherwise</returns>
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
                    new SqlParameter("@Progress", entity.Progress),
                    new SqlParameter("@Deadline", entity.Deadline ?? (object)DBNull.Value)
            };

                DatabaseHelper.ExecuteStoredProcedure("sp_Task_Update", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.Update] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deletes a task
        /// Calls sp_Task_Delete stored procedure
        /// </summary>
        /// <param name="id">Task ID to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", id)
                };

                DatabaseHelper.ExecuteStoredProcedure("sp_Task_Delete", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.Delete] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Retrieves tasks by project ID
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <returns>List of tasks for the project</returns>
        public List<Task> GetByProject(int projectId)
        {
            List<Task> tasks = new List<Task>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", projectId)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Task_GetByProject", parameters);

                // Map DataTable rows to Task objects
                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetByProject] Error: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Retrieves tasks assigned to a specific employee
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>List of tasks assigned to the employee</returns>
        public List<Task> GetByEmployee(int employeeId)
        {
            List<Task> tasks = new List<Task>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Task_GetByEmployee", parameters);

                // Map DataTable rows to Task objects
                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetByEmployee] Error: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Retrieves overdue tasks (Deadline < current date AND Status != 'Done')
        /// </summary>
        /// <returns>List of overdue tasks</returns>
        public List<Task> GetOverdueTasks()
        {
            List<Task> tasks = new List<Task>();

            try
            {
                // Query vw_TaskDetails for overdue tasks
                string sql =
                    @"SELECT * FROM vw_TaskDetails 
                    WHERE Deadline < GETDATE() AND Status != 'Done'
                    ORDER BY Deadline ASC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                // Map DataTable rows to Task objects
                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(MapDataRowToTask(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetOverdueTasks] Error: {ex.Message}");
                throw;
            }

            return tasks;
        }

        /// <summary>
        /// Updates task progress and automatically sets Status to 'Done' if Progress = 100
        /// Calls sp_Task_UpdateProgress stored procedure
        /// </summary>
        /// <param name="taskId">Task ID</param>
        /// <param name="progress">Progress percentage (0-100)</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateProgress(int taskId, int progress)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", taskId),
                    new SqlParameter("@Progress", progress)
                };

                DatabaseHelper.ExecuteStoredProcedure("sp_Task_UpdateProgress", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.UpdateProgress] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets task count grouped by status
        /// Returns Dictionary for easy chart binding
        /// </summary>
        /// <returns>Dictionary with Status as key and Count as value</returns>
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

                // Map DataTable rows to Dictionary
                foreach (DataRow row in dt.Rows)
                {
                    string status = row["Status"] != DBNull.Value ? row["Status"].ToString() : "Unknown";
                    int count = row["TaskCount"] != DBNull.Value ? Convert.ToInt32(row["TaskCount"]) : 0;
                    statusCounts[status] = count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskRepository.GetTaskCountByStatus] Error: {ex.Message}");
                throw;
            }

            return statusCounts;
        }

        /// <summary>
        /// Maps a DataRow to a Task object
        /// Handles NULL values and type conversions
        /// Supports both vw_TaskDetails and sp_Task results
        /// </summary>
        /// <param name="row">DataRow from query result</param>
        /// <returns>Task object</returns>
        private Task MapDataRowToTask(DataRow row)
        {
            return new Task
            {
                // Map TaskID (required field)
                TaskID = row["TaskID"] != DBNull.Value ? Convert.ToInt32(row["TaskID"]) : 0,

                // Map ProjectID (required field)
                ProjectID = row["ProjectID"] != DBNull.Value ? Convert.ToInt32(row["ProjectID"]) : 0,

                // Map ProjectName (for display, from view)
                ProjectName = row.Table.Columns.Contains("ProjectName") && row["ProjectName"] != DBNull.Value
                  ? row["ProjectName"].ToString() : null,

                // Map TaskName (nullable string)
                TaskName = row["TaskName"] != DBNull.Value ? row["TaskName"].ToString() : null,

                // Map Description (nullable string)
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,

                // Map AssignedTo (nullable int)
                AssignedTo = row["AssignedTo"] != DBNull.Value ? (int?)Convert.ToInt32(row["AssignedTo"]) : null,

                // Map EmployeeName (for display, from view)
                EmployeeName = row.Table.Columns.Contains("EmployeeName") && row["EmployeeName"] != DBNull.Value
                    ? row["EmployeeName"].ToString() : null,

                // Map Status (nullable string)
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,

                // Map Progress (required int)
                Progress = row["Progress"] != DBNull.Value ? Convert.ToInt32(row["Progress"]) : 0,

                // Map Deadline (nullable DateTime)
                Deadline = row["Deadline"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["Deadline"]) : null,

                // Map CreatedDate (required DateTime)
                CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.MinValue
            };
        }
    }
}
