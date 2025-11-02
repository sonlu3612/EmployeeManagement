using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    public class ProjectFileRepository : IRepository<ProjectFile>
    {
        public List<ProjectFile> GetAll()
        {
            List<ProjectFile> files = new List<ProjectFile>();

            try
            {
                string sql =
                    @"SELECT pf.ProjectFileID, pf.ProjectID, pf.Title, pf.FileName,
                    pf.CreatedBy, pf.CreatedAt,
                    p.ProjectName,
                    creator.FullName AS CreatedByName
                    FROM ProjectFiles pf
                    LEFT JOIN Projects p ON pf.ProjectID = p.ProjectID
                    LEFT JOIN Employees creator ON pf.CreatedBy = creator.EmployeeID
                    ORDER BY pf.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToProjectFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectFileRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public ProjectFile GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectFileID", id)
                };

                string sql =
                    @"SELECT pf.ProjectFileID, pf.ProjectID, pf.Title, pf.FileName,
                    pf.CreatedBy, pf.CreatedAt,
                    p.ProjectName,
                    creator.FullName AS CreatedByName
                    FROM ProjectFiles pf
                    LEFT JOIN Projects p ON pf.ProjectID = p.ProjectID
                    LEFT JOIN Employees creator ON pf.CreatedBy = creator.EmployeeID
                    WHERE pf.ProjectFileID = @ProjectFileID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToProjectFile(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectFileRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }

        public List<ProjectFile> GetByProject(int projectId)
        {
            List<ProjectFile> files = new List<ProjectFile>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", projectId)
                };

                string sql =
                    @"SELECT pf.ProjectFileID, pf.ProjectID, pf.Title, pf.FileName,
                    pf.CreatedBy, pf.CreatedAt,
                    p.ProjectName,
                    creator.FullName AS CreatedByName
                    FROM ProjectFiles pf
                    LEFT JOIN Projects p ON pf.ProjectID = p.ProjectID
                    LEFT JOIN Employees creator ON pf.CreatedBy = creator.EmployeeID
                    WHERE pf.ProjectID = @ProjectID
                    ORDER BY pf.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToProjectFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectFileRepository.GetByProject] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public bool Insert(ProjectFile entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectID", entity.ProjectID),
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value),
                    new SqlParameter("@CreatedBy", entity.CreatedBy)
                };

                string sql =
                    @"INSERT INTO ProjectFiles (ProjectID, Title, FileName, CreatedBy)
                    VALUES (@ProjectID, @Title, @FileName, @CreatedBy)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectFileRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Update(ProjectFile entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectFileID", entity.ProjectFileID),
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value)
                };

                string sql =
                    @"UPDATE ProjectFiles
                    SET Title = @Title,
                    FileName = @FileName
                    WHERE ProjectFileID = @ProjectFileID";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectFileRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProjectFileID", id)
                };

                string sql = "DELETE FROM ProjectFiles WHERE ProjectFileID = @ProjectFileID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectFileRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        private ProjectFile MapDataRowToProjectFile(DataRow row)
        {
            return new ProjectFile
            {
                ProjectFileID = row["ProjectFileID"] != DBNull.Value ? Convert.ToInt32(row["ProjectFileID"]) : 0,
                ProjectID = row["ProjectID"] != DBNull.Value ? Convert.ToInt32(row["ProjectID"]) : 0,
                Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : null,
                FileName = row["FileName"] != DBNull.Value ? row["FileName"].ToString() : null,
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : 0,
                CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : DateTime.MinValue,
                ProjectName = row.Table.Columns.Contains("ProjectName") && row["ProjectName"] != DBNull.Value
                     ? row["ProjectName"].ToString() : null,
                CreatedByName = row.Table.Columns.Contains("CreatedByName") && row["CreatedByName"] != DBNull.Value
                  ? row["CreatedByName"].ToString() : null
            };
        }
    }
}