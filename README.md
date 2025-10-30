# Employee Management

## Hướng dẫn chạy project (Sử dụng Visual Studio)

1. Chạy file `scripts.sql` ở trong thư mục `Database` bằng SSMS

2. Chỉnh chuỗi kết nối ở trong `App.config`

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <connectionStrings>
      <!-- Tại đây hãy đổi `Data Source` là tên máy chủ SQL của bạn-->
      <add name="EmployeeManagementDB" 
      connectionString="Data Source=DESKTOP-EM19H7N\SQLEXPRESS;Integrated Security=True;Connect Timeout=3;Encrypt=False;Database=ProjectManagementDB" 
      providerName="System.Data.SqlClient" />
    </connectionStrings>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>
</configuration>
```

3. Chạy project
