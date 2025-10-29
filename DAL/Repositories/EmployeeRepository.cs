using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Repositories
{
    /// <summary>
    /// Repository for Employee entity operations
    /// Implements IRepository pattern for data access
    /// </summary>
    public class EmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Retrieves all employees from database
        /// Calls sp_Employee_GetAll stored procedure
        /// </summary>
        /// <returns>List of all employees</returns>
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Employee_GetAll", null);

                // Map DataTable rows to Employee objects
                foreach (DataRow row in dt.Rows)
                {
                    employees.Add(MapDataRowToEmployee(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetAll] Error: {ex.Message}");
                throw;
            }

            return employees;
        }

        /// <summary>
        /// Retrieves a single employee by ID
        /// Calls sp_Employee_GetById stored procedure
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee object or null if not found</returns>
        public Employee GetById(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", id)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Employee_GetById", parameters);

                // Return null if no employee found
                if (dt.Rows.Count == 0)
                {
                    return null;
                }

                // Map first row to Employee object
                return MapDataRowToEmployee(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetById] Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Inserts a new employee
        /// Calls sp_Employee_Insert stored procedure
        /// </summary>
        /// <param name="entity">Employee object to insert</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool Insert(Employee entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FullName", entity.FullName ?? (object)DBNull.Value),
                    new SqlParameter("@Email", entity.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Phone", entity.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@DepartmentID", entity.DepartmentID ?? (object)DBNull.Value),
                    new SqlParameter("@ImagePath", entity.ImagePath ?? (object)DBNull.Value),
                    new SqlParameter("@Address", entity.Address ?? (object)DBNull.Value),
                    new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@IsActive", entity.IsActive)
                         };

                DatabaseHelper.ExecuteStoredProcedure("sp_Employee_Insert", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.Insert] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing employee
        /// Calls sp_Employee_Update stored procedure
        /// </summary>
        /// <param name="entity">Employee object with updated data</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool Update(Employee entity)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", entity.EmployeeID),
                    new SqlParameter("@FullName", entity.FullName ?? (object)DBNull.Value),
                    new SqlParameter("@Email", entity.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Phone", entity.Phone ?? (object)DBNull.Value),
                    new SqlParameter("@DepartmentID", entity.DepartmentID ?? (object)DBNull.Value),
                    new SqlParameter("@ImagePath", entity.ImagePath ?? (object)DBNull.Value),
                    new SqlParameter("@Address", entity.Address ?? (object)DBNull.Value),
                    new SqlParameter("@HireDate", entity.HireDate),
                    new SqlParameter("@IsActive", entity.IsActive)
                };

                DatabaseHelper.ExecuteStoredProcedure("sp_Employee_Update", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.Update] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Soft deletes an employee (sets IsActive = 0)
        /// Calls sp_Employee_Delete stored procedure
        /// </summary>
        /// <param name="id">Employee ID to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", id)
                };

                DatabaseHelper.ExecuteStoredProcedure("sp_Employee_Delete", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.Delete] Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Retrieves employees by department
        /// </summary>
        /// <param name="deptId">Department ID</param>
        /// <returns>List of employees in the department</returns>
        public List<Employee> GetByDepartment(int deptId)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", deptId)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Employee_GetByDepartment", parameters);

                // Map DataTable rows to Employee objects
                foreach (DataRow row in dt.Rows)
                {
                    employees.Add(MapDataRowToEmployee(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.GetByDepartment] Error: {ex.Message}");
                throw;
            }

            return employees;
        }

        /// <summary>
        /// Searches employees by name (partial match)
        /// </summary>
        /// <param name="keyword">Search keyword</param>
        /// <returns>List of matching employees</returns>
        public List<Employee> SearchByName(string keyword)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Keyword", keyword ?? (object)DBNull.Value)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_Employee_SearchByName", parameters);

                // Map DataTable rows to Employee objects
                foreach (DataRow row in dt.Rows)
                {
                    employees.Add(MapDataRowToEmployee(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmployeeRepository.SearchByName] Error: {ex.Message}");
                throw;
            }

            return employees;
        }

        /// <summary>
        /// Maps a DataRow to an Employee object
        /// Handles NULL values and type conversions
        /// </summary>
        /// <param name="row">DataRow from query result</param>
        /// <returns>Employee object</returns>
        private Employee MapDataRowToEmployee(DataRow row)
        {
            return new Employee
            {
                // Map EmployeeID (required field)
                EmployeeID = row["EmployeeID"] != DBNull.Value ? Convert.ToInt32(row["EmployeeID"]) : 0,

                // Map FullName (nullable string)
                FullName = row["FullName"] != DBNull.Value ? row["FullName"].ToString() : null,

                // Map Email (nullable string)
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,

                // Map Phone (nullable string)
                Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : null,

                // Map DepartmentID (nullable int)
                DepartmentID = row["DepartmentID"] != DBNull.Value ? (int?)Convert.ToInt32(row["DepartmentID"]) : null,

                // Map DepartmentName (for display, nullable string)
                DepartmentName = row.Table.Columns.Contains("DepartmentName") && row["DepartmentName"] != DBNull.Value
              ? row["DepartmentName"].ToString()
                : null,

                // Map ImagePath (nullable string)
                ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : null,

                // Map Address (nullable string)
                Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : null,

                // Map HireDate (required DateTime)
                HireDate = row["HireDate"] != DBNull.Value ? Convert.ToDateTime(row["HireDate"]) : DateTime.MinValue,

                // Map IsActive (required bool)
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false
            };
        }
    }
}
