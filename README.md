# Employee Management

## Hướng dẫn chạy project (Sử dụng Visual Studio)

1. Chạy file ở trong `Database/scripts.sql` bằng phần mềm SSMS

2. Dựa vào `App.config.template`, clone file `App.config` và chỉnh chuỗi kết nối tại `[DB_SERVER_NAME]`

```xml
<!-- App.config -->

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <connectionStrings>
      <add name="EmployeeManagementDB" 
      connectionString="Data Source=[DB_SERVER_NAME];Integrated Security=True;Connect Timeout=3;Encrypt=False;Database=EmployeeManagementDB" 
      providerName="System.Data.SqlClient" />
    </connectionStrings>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>
</configuration>
```

3. Chạy project
