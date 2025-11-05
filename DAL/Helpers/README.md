# DAL/Helpers

Không nên gọi các hàm trong thư mục Helpers trực tiếp từ các lớp khác trong DAL. Thay vào đó, hãy sử dụng các hàm này thông qua các lớp dịch vụ (Services) hoặc kho lưu trữ (Repositories) để đảm bảo tính nhất quán và dễ bảo trì của mã nguồn.
