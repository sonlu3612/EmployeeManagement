using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    public class EmployeeFileRepository : IRepository<EmployeeFile>
    {
        public List<EmployeeFile> GetAll()
        {
            List<EmployeeFile> files = new List<EmployeeFile>();

            try
            {
                string sql =
                    @"SELECT ef.EmployeeFileID, ef.EmployeeID, ef.Title, ef.FileName,
                    ef.CreatedBy, ef.CreatedAt,
                    e.FullName AS EmployeeName,
                    creator.FullName AS CreatedByName
                    FROM EmployeeFiles ef
                    LEFT JOIN Employees e ON ef.EmployeeID = e.EmployeeID
                    LEFT JOIN Employees creator ON ef.CreatedBy = creator.EmployeeID
                    ORDER BY ef.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToEmployeeFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeFileRepository.GetAll] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public EmployeeFile GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeFileID", id)
                };

                string sql =
                    @"SELECT ef.EmployeeFileID, ef.EmployeeID, ef.Title, ef.FileName,
                    ef.CreatedBy, ef.CreatedAt,
                    e.FullName AS EmployeeName,
                    creator.FullName AS CreatedByName
                    FROM EmployeeFiles ef
                    LEFT JOIN Employees e ON ef.EmployeeID = e.EmployeeID
                    LEFT JOIN Employees creator ON ef.CreatedBy = creator.EmployeeID
                    WHERE ef.EmployeeFileID = @EmployeeFileID";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                return MapDataRowToEmployeeFile(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeFileRepository.GetById] Lỗi: {ex.Message}");
                throw;
            }
        }

        public List<EmployeeFile> GetByEmployee(int employeeId)
        {
            List<EmployeeFile> files = new List<EmployeeFile>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", employeeId)
                };

                string sql =
                    @"SELECT ef.EmployeeFileID, ef.EmployeeID, ef.Title, ef.FileName,
                    ef.CreatedBy, ef.CreatedAt,
                    e.FullName AS EmployeeName,
                    creator.FullName AS CreatedByName
                    FROM EmployeeFiles ef
                    LEFT JOIN Employees e ON ef.EmployeeID = e.EmployeeID
                    LEFT JOIN Employees creator ON ef.CreatedBy = creator.EmployeeID
                    WHERE ef.EmployeeID = @EmployeeID
                    ORDER BY ef.CreatedAt DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    files.Add(MapDataRowToEmployeeFile(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeFileRepository.GetByEmployee] Lỗi: {ex.Message}");
                throw;
            }

            return files;
        }

        public bool Insert(EmployeeFile entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", entity.EmployeeID),
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value),
                    new SqlParameter("@CreatedBy", entity.CreatedBy)
                };

                string sql =
                    @"INSERT INTO EmployeeFiles (EmployeeID, Title, FileName, CreatedBy)
                    VALUES (@EmployeeID, @Title, @FileName, @CreatedBy)";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeFileRepository.Insert] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Update(EmployeeFile entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeFileID", entity.EmployeeFileID),
                    new SqlParameter("@Title", entity.Title ?? (object)DBNull.Value),
                    new SqlParameter("@FileName", entity.FileName ?? (object)DBNull.Value)
                };

                string sql =
                    @"UPDATE EmployeeFiles
                    SET Title = @Title,
                    FileName = @FileName
                    WHERE EmployeeFileID = @EmployeeFileID";

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeFileRepository.Update] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@EmployeeFileID", id)
                };

                string sql = "DELETE FROM EmployeeFiles WHERE EmployeeFileID = @EmployeeFileID";
                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeFileRepository.Delete] Lỗi: {ex.Message}");
                return false;
            }
        }

        private EmployeeFile MapDataRowToEmployeeFile(DataRow row)
        {
            return new EmployeeFile
            {
                EmployeeFileID = row["EmployeeFileID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeFileID"]) : 0,
                EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : 0,
                Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : null,
                FileName = row["FileName"] != DBNull.Value ? row["FileName"].ToString() : null,
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : 0,
                CreatedAt = row["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(row["CreatedAt"]) : DateTime.MinValue,
                EmployeeName = row.Table.Columns.Contains("EmployeeName") && row["EmployeeName"] != DBNull.Value
                ? row["EmployeeName"].ToString() : null,
                CreatedByName = row.Table.Columns.Contains("CreatedByName") && row["CreatedByName"] != DBNull.Value
                  ? row["CreatedByName"].ToString() : null
            };
        }
    }
}