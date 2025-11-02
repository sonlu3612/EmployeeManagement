using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    public class TaskFileRepository : IRepository<TaskFile>
    {
        public List<TaskFile> GetAll()
        {
            List<TaskFile> files = new List<TaskFile>();

            try
            {
                string sql =
                    @"SELECT tf.TaskFileID, tf.TaskID, tf.Title, tf.FileName,
                    tf.CreatedBy, tf.CreatedAt,
                    t.TaskTitle,
                    creator.FullName AS CreatedByName
                    FROM TaskFiles tf
                    LEFT JOIN Tasks t ON tf.TaskID = t.TaskID
                    LEFT JOIN Employees creator ON tf.CreatedBy = creator.EmployeeID
                    ORDER BY tf.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToTaskFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskFileRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public TaskFile GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskFileID", id)
                };

                string sql =
                    @"SELECT tf.TaskFileID, tf.TaskID, tf.Title, tf.FileName,
                    tf.CreatedBy, tf.CreatedAt,
                    t.TaskTitle,
                    creator.FullName AS CreatedByName
                    FROM TaskFiles tf
                    LEFT JOIN Tasks t ON tf.TaskID = t.TaskID
                    LEFT JOIN Employees creator ON tf.CreatedBy = creator.EmployeeID
                    WHERE tf.TaskFileID = @TaskFileID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToTaskFile(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskFileRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }

        public List<TaskFile> GetByTask(int taskId)
        {
            List<TaskFile> files = new List<TaskFile>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", taskId)
                };

                string sql =
                    @"SELECT tf.TaskFileID, tf.TaskID, tf.Title, tf.FileName,
                    tf.CreatedBy, tf.CreatedAt,
                    t.TaskTitle,
                    creator.FullName AS CreatedByName
                    FROM TaskFiles tf
                    LEFT JOIN Tasks t ON tf.TaskID = t.TaskID
                    LEFT JOIN Employees creator ON tf.CreatedBy = creator.EmployeeID
                    WHERE tf.TaskID = @TaskID
                    ORDER BY tf.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToTaskFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskFileRepository.GetByTask] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public bool Insert(TaskFile entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskID", entity.TaskID),
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value),
                    new SqlParameter("@CreatedBy", entity.CreatedBy)
                };

                string sql =
                    @"INSERT INTO TaskFiles (TaskID, Title, FileName, CreatedBy)
                    VALUES (@TaskID, @Title, @FileName, @CreatedBy)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskFileRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Update(TaskFile entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskFileID", entity.TaskFileID),
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value)
                };

                string sql =
                    @"UPDATE TaskFiles
                    SET Title = @Title,
                    FileName = @FileName
                    WHERE TaskFileID = @TaskFileID";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskFileRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TaskFileID", id)
                };

                string sql = "DELETE FROM TaskFiles WHERE TaskFileID = @TaskFileID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TaskFileRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        private TaskFile MapDataRowToTaskFile(DataRow row)
        {
            return new TaskFile
            {
                TaskFileID = row["TaskFileID"] != DBNull.Value ? Convert.ToInt32(row["TaskFileID"]) : 0,
                TaskID = row["TaskID"] != DBNull.Value ? Convert.ToInt32(row["TaskID"]) : 0,
                Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : null,
                FileName = row["FileName"] != DBNull.Value ? row["FileName"].ToString() : null,
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : 0,
                CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : DateTime.MinValue,
                TaskTitle = row.Table.Columns.Contains("TaskTitle") && row["TaskTitle"] != DBNull.Value
                  ? row["TaskTitle"].ToString() : null,
                CreatedByName = row.Table.Columns.Contains("CreatedByName") && row["CreatedByName"] != DBNull.Value
             ? row["CreatedByName"].ToString() : null
            };
        }
    }
}