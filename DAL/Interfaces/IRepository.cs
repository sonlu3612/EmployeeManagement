using System.Collections.Generic;

namespace EmployeeManagement.DAL.Interfaces
{
    /// <summary>
    /// Interface generic cho Repository pattern
    /// Định nghĩa các phương thức CRUD cơ bản cho tất cả các entity
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu entity (class)</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Lấy danh sách tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách các entity</returns>
        List<T> GetAll();

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi</param>
        /// <returns>Entity hoặc null nếu không tìm thấy</returns>
        T GetById(int id);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Entity cần thêm</param>
        /// <returns>True nếu thành công, False nếu thất bại</returns>
        bool Insert(T entity);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">Entity cần cập nhật</param>
        /// <returns>True nếu thành công, False nếu thất bại</returns>
        bool Update(T entity);

        /// <summary>
        /// Xóa một bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>True nếu thành công, False nếu thất bại</returns>
        bool Delete(int id);
    }
}
