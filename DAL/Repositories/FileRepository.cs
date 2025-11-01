using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    public class FileRepository : IRepository<File>
    {
        public List<File> GetAll()
        {
            List<File> files = new List<File>();

            try
            {
                string sql =
                    @"SELECT f.FileID, f.Title, f.FileName, f.CreatedBy, f.CreatedAt,
                    e.FullName AS CreatedByName
                    FROM Files f
                    LEFT JOIN Employees e ON f.CreatedBy = e.EmployeeID
                    ORDER BY f.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public File GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FileID", id)
                };

                string sql =
                    @"SELECT f.FileID, f.Title, f.FileName, f.CreatedBy, f.CreatedAt,
                    e.FullName AS CreatedByName
                    FROM Files f
                    LEFT JOIN Employees e ON f.CreatedBy = e.EmployeeID
                    WHERE f.FileID = @FileID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToFile(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }

        public bool Insert(File entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value),
                    new SqlParameter("@CreatedBy", entity.CreatedBy)
                };

                string sql =
                    @"INSERT INTO Files (Title, FileName, CreatedBy)
                    VALUES (@Title, @FileName, @CreatedBy)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Update(File entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FileID", entity.FileID),
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value)
            };

                string sql =
                    @"UPDATE Files
                    SET Title = @Title,
                    FileName = @FileName
                    WHERE FileID = @FileID";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FileID", id)
                };

                string sql = "DELETE FROM Files WHERE FileID = @FileID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        public List<File> SearchByTitle(string keyword)
        {
            List<File> files = new List<File>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Keyword", $"%{keyword}%")
                };

                string sql =
                    @"SELECT f.FileID, f.Title, f.FileName, f.CreatedBy, f.CreatedAt,
                    e.FullName AS CreatedByName
                    FROM Files f
                    LEFT JOIN Employees e ON f.CreatedBy = e.EmployeeID
                    WHERE f.Title LIKE @Keyword OR f.FileName LIKE @Keyword
                    ORDER BY f.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileRepository.SearchByTitle] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public List<File> GetByEmployee(int employeeId)
        {
            List<File> files = new List<File>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                string sql =
                    @"SELECT f.FileID, f.Title, f.FileName, f.CreatedBy, f.CreatedAt,
                    e.FullName AS CreatedByName
                    FROM Files f
                    LEFT JOIN Employees e ON f.CreatedBy = e.EmployeeID
                    WHERE f.CreatedBy = @EmployeeID
                    ORDER BY f.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileRepository.GetByEmployee] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        private File MapDataRowToFile(DataRow row)
        {
            return new File
            {
                FileID = row["FileID"] != DBNull.Value ? Convert.ToInt32(row["FileID"]) : 0,
                Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : null,
                FileName = row["FileName"] != DBNull.Value ? row["FileName"].ToString() : null,
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : 0,
                CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : DateTime.MinValue,
                CreatedByName = row.Table.Columns.Contains("CreatedByName") && row["CreatedByName"] != DBNull.Value
         ? row["CreatedByName"].ToString()
     : null
            };
        }
    }
}