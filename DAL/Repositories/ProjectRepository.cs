using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository for Project entity operations
    /// Implements IRepository pattern for data access
    /// </summary>
    public class ProjectRepository : IRepository<Project>
    {
        /// <summary>
        /// Retrieves all projects from database
        /// Calls sp_Project_GetAll stored procedure
        /// </summary>
        /// <returns>List of all projects</returns>
        public List<Project> GetAll()
        {
            List<Project> projects = new List<Project>();

            try
            {
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Project_GetAll", null);

                // Map DataTable rows to Project objects
                foreach (DataRow row in dt.Rows)
                {
                    projects.Add(MapDataRowToProject(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetAll] Error: {ex.Message}");
                throw;
            }

            return projects;
        }

        /// <summary>
        /// Retrieves a single project by ID
        /// Calls sp_Project_GetById stored procedure
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns>Project object or null if not found</returns>
        public Project GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", id)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Project_GetById", parameters);

                // Return null if no project found
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Map first row to Project object
                return MapDataRowToProject(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetById] Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Inserts a new project
        /// Validates StartDate <= EndDate before insertion
        /// Calls sp_Project_Insert stored procedure
        /// </summary>
        /// <param name="entity">Project object to insert</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool Insert(Project entity)
        {
            try
            {
                // Validate StartDate <= EndDate
                if (entity.EndDate.HasValue && entity.StartDate > entity.EndDate.Value)
                {
                    Console.WriteLine("[ProjectRepository.Insert] Validation Error: StartDate must be <= EndDate");
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
                Console.WriteLine($"[ProjectRepository.Insert] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing project
        /// Validates StartDate <= EndDate before update
        /// Calls sp_Project_Update stored procedure
        /// </summary>
        /// <param name="entity">Project object with updated data</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool Update(Project entity)
        {
            try
            {
                // Validate StartDate <= EndDate
                if (entity.EndDate.HasValue && entity.StartDate > entity.EndDate.Value)
                {
                    Console.WriteLine("[ProjectRepository.Update] Validation Error: StartDate must be <= EndDate");
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
                Console.WriteLine($"[ProjectRepository.Update] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deletes a project
        /// Calls sp_Project_Delete stored procedure
        /// </summary>
        /// <param name="id">Project ID to delete</param>
        /// <returns>True if successful, false otherwise</returns>
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
                Console.WriteLine($"[ProjectRepository.Delete] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Retrieves projects by status
        /// </summary>
        /// <param name="status">Project status (e.g., "Planning", "In Progress", "Completed")</param>
        /// <returns>List of projects with matching status</returns>
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

                // Map DataTable rows to Project objects
                foreach (DataRow row in dt.Rows)
                {
                    projects.Add(MapDataRowToProject(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectRepository.GetByStatus] Error: {ex.Message}");
                throw;
            }

            return projects;
        }

        /// <summary>
        /// Retrieves project statistics including task completion
        /// Queries vw_ProjectSummary view
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <returns>ProjectStats object or null if not found</returns>
        public ProjectStats GetProjectStats(int projectId)
        {
            try
            {
                // Query vw_ProjectSummary view
                string sql = "SELECT * FROM vw_ProjectSummary WHERE ProjectID = @ProjectID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", projectId)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                // Return null if no stats found
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Map first row to ProjectStats object
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
                Console.WriteLine($"[ProjectRepository.GetProjectStats] Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Maps a DataRow to a Project object
        /// Handles NULL values and type conversions
        /// </summary>
        /// <param name="row">DataRow from query result</param>
        /// <returns>Project object</returns>
        private Project MapDataRowToProject(DataRow row)
        {
            return new Project
            {
                // Map ProjectID (required field)
                ProjectID = row["ProjectID"] != DBNull.Value ? Convert.ToInt32(row["ProjectID"]) : 0,

                // Map ProjectName (nullable string)
                ProjectName = row["ProjectName"] != DBNull.Value ? row["ProjectName"].ToString() : null,

                // Map Description (nullable string)
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,

                // Map StartDate (required DateTime)
                StartDate = row["StartDate"] != DBNull.Value ? Convert.ToDateTime(row["StartDate"]) : DateTime.MinValue,

                // Map EndDate (nullable DateTime)
                EndDate = row["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["EndDate"]) : null,

                // Map Status (nullable string)
                Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,

                // Map Budget (required decimal)
                Budget = row["Budget"] != DBNull.Value ? Convert.ToDecimal(row["Budget"]) : 0m
            };
        }
    }
}
