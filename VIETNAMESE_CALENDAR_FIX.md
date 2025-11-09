# Fix Lỗi Hiển Thị Lịch Tiếng Việt

## Vấn Đề
Lịch (Calendar/DatePicker) trong ứng dụng đang hiển thị chữ Hán (一, 二, 三, 四, 五, 六, 日) thay vì tiếng Việt hoặc tiếng Anh.

## Nguyên Nhân
Thư viện AntdUI có hạn chế thiết kế trong việc xử lý ngôn ngữ:
- Thư viện kiểm tra nếu Culture ID bắt đầu bằng "en" → hiển thị tiếng Anh (Mon, Tue, Wed...)
- Nếu không → hiển thị chữ Hán (一, 二, 三...)
- Thư viện **KHÔNG** sử dụng localization provider để lấy tên các ngày trong tuần
- Các giá trị này được hard-code trực tiếp trong source code của thư viện

## Giải Pháp
Đã thay đổi Culture ID từ "vi-VN" thành "en-VN" trong file `Localizer.cs`:
```csharp
case "ID":
    return "en-VN";  // Thay vì "vi-VN"
```

Điều này "lừa" thư viện nghĩ rằng đang sử dụng tiếng Anh, nên sẽ hiển thị các chữ viết tắt tiếng Anh.

## Kết Quả
- **Trước khi fix:** Lịch hiển thị chữ Hán (一, 二, 三, 四, 五, 六, 日)
- **Sau khi fix:** Lịch hiển thị tiếng Anh (Mon, Tue, Wed, Thu, Fri, Sat, Sun)

## Hạn Chế
- Không thể hiển thị tiếng Việt (Th2, Th3, Th4...) do giới hạn của thư viện AntdUI
- Các bản dịch tiếng Việt đã được chuẩn bị trong `Localizer.cs` cho trường hợp thư viện hỗ trợ trong tương lai

## Kiểm Tra (Chỉ chạy được trên Windows)
1. Mở solution trong Visual Studio
2. Build project
3. Chạy ứng dụng
4. Mở bất kỳ form nào có DatePicker hoặc Calendar
5. Xác nhận rằng lịch hiển thị "Mon, Tue, Wed..." thay vì chữ Hán

## Tương Lai
Nếu thư viện AntdUI cập nhật và hỗ trợ localization đúng cách, các bản dịch tiếng Việt (Th2, Th3...) đã sẵn sàng trong file `Localizer.cs` (dòng 45-77).
