-- =============================================
-- Script: Setup ProjectManagementDB
-- Mô tả: Tạo database, tables, indexes và sample data
-- Lưu ý: Script sẽ XÓA database cũ nếu tồn tại
-- =============================================

-- 1. Tạo schema

USE master;
GO

-- Kiểm tra và xóa database nếu đã tồn tại
IF DB_ID(N'ProjectManagementDB') IS NOT NULL
BEGIN
PRINT 'Closing all connections to ProjectManagementDB...';
    
    -- Ngắt tất cả connections hiện tại
    ALTER DATABASE [ProjectManagementDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    
    -- Xóa database
    DROP DATABASE [ProjectManagementDB];
    PRINT 'Database "ProjectManagementDB" has been dropped.';
END
ELSE
BEGIN
    PRINT 'Database "ProjectManagementDB" does not exist.';
END
GO

-- Tạo database mới
CREATE DATABASE [ProjectManagementDB];
PRINT 'Database "ProjectManagementDB" created successfully!';
GO

-- Chuyển sang database mới tạo
USE [ProjectManagementDB];
PRINT 'Switched to database: ProjectManagementDB';
GO

-- =============================================
-- TABLES CREATION
-- =============================================

/*
 Table: Users
 Mô tả: Lưu thông tin accounts đăng nhập hệ thống
 Relationships: 
   - 1 User có thể tạo nhiều Projects (Projects.CreatedBy)
   - 1 User có thể tạo nhiều Tasks (Tasks.CreatedBy)
   - 1 User có thể comment nhiều TaskComments (TaskComments.UserID)
*/
CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
 PasswordHash NVARCHAR(256) NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin','Manager','Employee')),
  IsActive BIT NOT NULL DEFAULT (1),
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE())
);
PRINT 'Table [Users] created.';
GO

/*
 Table: Departments
 Mô tả: Quản lý phòng ban
 Relationships:
   - 1 Department có nhiều Employees (Employees.DepartmentID)
   - 1 Department có 1 Manager (là 1 Employee) (Departments.ManagerID)
*/
CREATE TABLE dbo.Departments (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    ManagerID INT NULL -- Sẽ thêm FK sau khi Employees được tạo
);
PRINT 'Table [Departments] created.';
GO

/*
 Table: Employees
 Mô tả: Quản lý nhân viên
 Relationships:
   - Nhiều Employees thuộc 1 Department (FK: DepartmentID)
   - 1 Employee có thể được gán nhiều Tasks (Tasks.AssignedTo)
*/
CREATE TABLE dbo.Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    Phone VARCHAR(20),
    DepartmentID INT NULL,
    ImagePath NVARCHAR(500),
    Address NVARCHAR(500),
    HireDate DATE NOT NULL DEFAULT (GETDATE()),
 IsActive BIT NOT NULL DEFAULT (1),
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentID) 
        REFERENCES dbo.Departments(DepartmentID) ON DELETE SET NULL
);
PRINT 'Table [Employees] created.';
GO

-- Thêm FK từ Departments.ManagerID -> Employees.EmployeeID (sau khi Employees đã tạo)
ALTER TABLE dbo.Departments
    ADD CONSTRAINT FK_Departments_Manager FOREIGN KEY (ManagerID) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL;
PRINT 'Foreign Key [FK_Departments_Manager] added.';
GO

/*
 Table: Projects
 Mô tả: Quản lý dự án
 Relationships:
   - 1 Project được tạo bởi 1 User (FK: CreatedBy)
   - 1 Project có nhiều Tasks (Tasks.ProjectID)
*/
CREATE TABLE dbo.Projects (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
  ProjectName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Planning','InProgress','Completed','Cancelled')),
    Budget DECIMAL(18,2) NULL,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT CHK_Project_Dates CHECK (EndDate IS NULL OR EndDate >= StartDate),
    CONSTRAINT FK_Projects_Users FOREIGN KEY (CreatedBy) 
        REFERENCES dbo.Users(UserID) ON DELETE NO ACTION
);
PRINT 'Table [Projects] created.';
GO

/*
 Table: Tasks
 Mô tả: Quản lý công việc trong dự án
 Relationships:
   - Nhiều Tasks thuộc 1 Project (FK: ProjectID) - CASCADE DELETE
   - 1 Task được gán cho 1 Employee (FK: AssignedTo)
   - 1 Task được tạo bởi 1 User (FK: CreatedBy)
   - 1 Task có nhiều TaskComments (TaskComments.TaskID)
*/
CREATE TABLE dbo.Tasks (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    TaskTitle NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    AssignedTo INT NULL,
    CreatedBy INT NOT NULL,
    Deadline DATE NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Todo','InProgress','Review','Done')),
    Priority NVARCHAR(20) NOT NULL CHECK (Priority IN ('Low','Medium','High','Critical')),
    Progress INT NOT NULL DEFAULT (0) CHECK (Progress BETWEEN 0 AND 100),
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    UpdatedDate DATETIME NULL,
    CONSTRAINT FK_Tasks_Projects FOREIGN KEY (ProjectID) 
        REFERENCES dbo.Projects(ProjectID) ON DELETE CASCADE,
    CONSTRAINT FK_Tasks_Employees FOREIGN KEY (AssignedTo) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL,
    CONSTRAINT FK_Tasks_Users FOREIGN KEY (CreatedBy) 
        REFERENCES dbo.Users(UserID) ON DELETE NO ACTION
);
PRINT 'Table [Tasks] created.';
GO

/*
 Table: TaskComments
 Mô tả: Lưu comments/ghi chú cho Tasks
 Relationships:
   - Nhiều Comments thuộc 1 Task (FK: TaskID) - CASCADE DELETE
   - 1 Comment được tạo bởi 1 User (FK: UserID)
*/
CREATE TABLE dbo.TaskComments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
    UserID INT NOT NULL,
    Comment NVARCHAR(MAX) NOT NULL,
    CommentDate DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT FK_TaskComments_Tasks FOREIGN KEY (TaskID) 
        REFERENCES dbo.Tasks(TaskID) ON DELETE CASCADE,
    CONSTRAINT FK_TaskComments_Users FOREIGN KEY (UserID) 
        REFERENCES dbo.Users(UserID) ON DELETE NO ACTION
);
PRINT 'Table [TaskComments] created.';
GO

PRINT '==> All 6 tables created successfully!';
GO

-- =============================================
-- INDEXES CREATION
-- =============================================

PRINT 'Creating indexes...';

-- Foreign Key Indexes (để tối ưu JOIN operations)
CREATE NONCLUSTERED INDEX IX_Employees_DepartmentID ON dbo.Employees(DepartmentID);
CREATE NONCLUSTERED INDEX IX_Departments_ManagerID ON dbo.Departments(ManagerID);
CREATE NONCLUSTERED INDEX IX_Projects_CreatedBy ON dbo.Projects(CreatedBy);
CREATE NONCLUSTERED INDEX IX_Tasks_ProjectID ON dbo.Tasks(ProjectID);
CREATE NONCLUSTERED INDEX IX_Tasks_AssignedTo ON dbo.Tasks(AssignedTo);
CREATE NONCLUSTERED INDEX IX_Tasks_CreatedBy ON dbo.Tasks(CreatedBy);
CREATE NONCLUSTERED INDEX IX_TaskComments_TaskID ON dbo.TaskComments(TaskID);
CREATE NONCLUSTERED INDEX IX_TaskComments_UserID ON dbo.TaskComments(UserID);

-- Email Indexes (để tối ưu tìm kiếm theo email)
CREATE NONCLUSTERED INDEX IX_Users_Email ON dbo.Users(Email) WHERE Email IS NOT NULL;
CREATE NONCLUSTERED INDEX IX_Employees_Email ON dbo.Employees(Email) WHERE Email IS NOT NULL;

PRINT '==> All 10 indexes created successfully!';
GO

-- =============================================
-- SAMPLE DATA INSERTION
-- =============================================

PRINT 'Inserting sample data...';
GO

-- 1. Users (5 records)
-- Password format: CONVERT to hex string for NVARCHAR storage
INSERT INTO dbo.Users (Username, PasswordHash, Email, Role)
VALUES
('admin', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Admin@123!'), 2), 'admin@company.com', 'Admin'),
('manager1', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Manager1@123'), 2), 'manager1@company.com', 'Manager'),
('manager2', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Manager2@123'), 2), 'manager2@company.com', 'Manager'),
('employee1', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee1@123'), 2), 'emp1@company.com', 'Employee'),
('employee2', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee2@123'), 2), 'emp2@company.com', 'Employee');
PRINT '==> 5 Users inserted.';
GO

-- 2. Departments (5 records) - ManagerID sẽ update sau
INSERT INTO dbo.Departments (DepartmentName, Description)
VALUES
(N'Công nghệ thông tin', N'Phòng phát triển phần mềm và hạ tầng IT'),
(N'Nhân sự', N'Phòng quản lý nguồn nhân lực'),
(N'Tài chính', N'Phòng kế toán và tài chính'),
(N'Marketing', N'Phòng marketing và truyền thông'),
(N'Vận hành', N'Phòng vận hành và logistics');
PRINT '==> 5 Departments inserted.';
GO

-- 3. Employees (5 records)
INSERT INTO dbo.Employees (FullName, Email, Phone, DepartmentID, ImagePath, Address, HireDate)
VALUES
(N'Nguyễn Văn An', 'nguyen.van.an@company.com', '0901234567', 1, '/images/employees/nva.jpg', N'123 Láng Hạ, Hà Nội', '2020-03-15'),
(N'Trần Thị Bình', 'tran.thi.binh@company.com', '0912345678', 2, '/images/employees/ttb.jpg', N'456 Giải Phóng, Hà Nội', '2019-07-20'),
(N'Lê Minh Cường', 'le.minh.cuong@company.com', '0923456789', 1, '/images/employees/lmc.jpg', N'789 Trường Chinh, Hà Nội', '2018-11-10'),
(N'Phạm Thu Dung', 'pham.thu.dung@company.com', '0934567890', 3, '/images/employees/ptd.jpg', N'101 Cầu Giấy, Hà Nội', '2021-02-01'),
(N'Hoàng Quốc Huy', 'hoang.quoc.huy@company.com', '0945678901', 4, '/images/employees/hqh.jpg', N'202 Nguyễn Trãi, Hà Nội', '2019-09-25');
PRINT '==> 5 Employees inserted.';
GO

-- 4. Update ManagerID cho Departments
UPDATE dbo.Departments SET ManagerID = 3 WHERE DepartmentID = 1; -- IT: Lê Minh Cường
UPDATE dbo.Departments SET ManagerID = 2 WHERE DepartmentID = 2; -- HR: Trần Thị Bình
UPDATE dbo.Departments SET ManagerID = 4 WHERE DepartmentID = 3; -- Finance: Phạm Thu Dung
UPDATE dbo.Departments SET ManagerID = 5 WHERE DepartmentID = 4; -- Marketing: Hoàng Quốc Huy
-- Operations: No manager (NULL)
PRINT '==> Department managers assigned.';
GO

-- 5. Projects (5 records)
INSERT INTO dbo.Projects (ProjectName, Description, StartDate, EndDate, Status, Budget, CreatedBy)
VALUES
(N'Hệ thống quản lý nhân sự', N'Xây dựng hệ thống quản lý nhân sự tập trung', '2024-01-15', '2024-07-31', 'InProgress', 800000000, 2),
(N'Website thương mại điện tử', N'Phát triển nền tảng bán hàng online', '2024-02-01', NULL, 'Planning', 1200000000, 2),
(N'Ứng dụng mobile CRM', N'App quản lý quan hệ khách hàng trên di động', '2023-09-01', '2024-03-31', 'Completed', 500000000, 1),
(N'Tối ưu hệ thống báo cáo', N'Nâng cấp hệ thống báo cáo tài chính', '2024-04-01', '2024-12-31', 'Planning', 300000000, 3),
(N'Nâng cấp hạ tầng mạng', N'Modernize network infrastructure', '2024-01-10', '2024-06-30', 'InProgress', 600000000, 2);
PRINT '==> 5 Projects inserted.';
GO

-- 6. Tasks (5 records)
INSERT INTO dbo.Tasks (ProjectID, TaskTitle, Description, AssignedTo, CreatedBy, Deadline, Status, Priority, Progress)
VALUES
(1, N'Thiết kế database schema', N'Phân tích và thiết kế cấu trúc CSDL', 3, 2, '2024-02-15', 'Done', 'High', 100),
(1, N'Phát triển API Backend', N'Xây dựng REST API với .NET', 1, 2, '2024-04-30', 'InProgress', 'High', 60),
(2, N'Nghiên cứu công nghệ', N'Đánh giá các framework frontend phù hợp', NULL, 2, '2024-03-15', 'Todo', 'Medium', 0),
(3, N'Kiểm thử chức năng', N'Test toàn bộ tính năng trước khi release', 4, 1, '2024-03-20', 'Done', 'Critical', 100),
(5, N'Cấu hình firewall', N'Setup firewall rules cho hệ thống mới', 5, 2, '2024-05-31', 'InProgress', 'High', 30);
PRINT '==> 5 Tasks inserted.';
GO

-- 7. TaskComments (5 records)
INSERT INTO dbo.TaskComments (TaskID, UserID, Comment)
VALUES
(1, 2, N'Schema đã được review và approve bởi team lead.'),
(2, 4, N'Cần optimize performance cho API lấy danh sách users.'),
(2, 2, N'Đã áp dụng caching, response time giảm từ 800ms xuống 120ms.'),
(4, 1, N'Tất cả 45 test cases đã PASSED. Ready for production.'),
(5, 2, N'Cần phối hợp với team Network để test connection.');
PRINT '==> 5 TaskComments inserted.';
GO

-- =============================================
-- COMPLETION SUMMARY
-- =============================================

PRINT '';
PRINT '========================================';
PRINT '  DATABASE SETUP COMPLETED!';
PRINT '========================================';
PRINT 'Database Name: ProjectManagementDB';
PRINT 'Tables Created: 6';
PRINT '  - Users';
PRINT '  - Departments';
PRINT '  - Employees';
PRINT '  - Projects';
PRINT '  - Tasks';
PRINT '  - TaskComments';
PRINT '';
PRINT 'Indexes Created: 10';
PRINT 'Sample Records: 5 per table';
PRINT '';
PRINT 'Default Login Credentials:';
PRINT '  Admin    - Username: admin   Password: Admin@123!';
PRINT '  Manager  - Username: manager1  Password: Manager1@123';
PRINT '  Employee - Username: employee1 Password: Employee1@123';
PRINT '';
PRINT '========================================';
PRINT 'SECURITY NOTES:';
PRINT '- Passwords are hashed using SHA2_256';
PRINT '- In production, use BCrypt/Argon2 with salt';
PRINT '- Always use parameterized queries';
PRINT '- Enable SQL Server audit logs';
PRINT '========================================';
GO

-- 2. Tạo Stored Procedures

-- =============================================
-- Script: Stored Procedures cho ProjectManagementDB
-- Mô tả: Các thủ tục CRUD và báo cáo
-- Tạo ngày: 2024
-- =============================================

USE [ProjectManagementDB];
GO

PRINT 'Đang tạo Stored Procedures...';
GO

-- =============================================
-- PHẦN 1: CÁC THỦ TUC CHO EMPLOYEE (NHÂN VIÊN)
-- =============================================

-- ---------------------------------------------
-- sp_Employee_GetAll
-- Mô tả: Lấy tất cả nhân viên đang hoạt động với thông tin phòng ban
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Employee_GetAll', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Employee_GetAll;
GO

CREATE PROCEDURE dbo.sp_Employee_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
  BEGIN TRY
        SELECT 
       e.EmployeeID,
       e.FullName,
      e.Email,
       e.Phone,
  e.DepartmentID,
            d.DepartmentName,
    e.ImagePath,
         e.Address,
       e.HireDate,
e.IsActive
  FROM dbo.Employees e
 LEFT JOIN dbo.Departments d ON e.DepartmentID = d.DepartmentID
  WHERE e.IsActive = 1
   ORDER BY e.FullName;
    
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Employee_GetAll';
GO

-- ---------------------------------------------
-- sp_Employee_GetById
-- Mô tả: Lấy thông tin nhân viên theo ID với thông tin phòng ban
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Employee_GetById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Employee_GetById;
GO

CREATE PROCEDURE dbo.sp_Employee_GetById
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra nhân viên có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
        BEGIN
      PRINT 'Không tìm thấy nhân viên';
  RETURN -1;
   END
    
        SELECT 
    e.EmployeeID,
 e.FullName,
            e.Email,
        e.Phone,
            e.DepartmentID,
   d.DepartmentName,
   e.ImagePath,
   e.Address,
    e.HireDate,
            e.IsActive
        FROM dbo.Employees e
   LEFT JOIN dbo.Departments d ON e.DepartmentID = d.DepartmentID
 WHERE e.EmployeeID = @EmployeeID;
   
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
      RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Employee_GetById';
GO

-- ---------------------------------------------
-- sp_Employee_Insert
-- Mô tả: Thêm nhân viên mới
-- Trả về: EmployeeID mới hoặc -1 nếu thất bại
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Employee_Insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Employee_Insert;
GO

CREATE PROCEDURE dbo.sp_Employee_Insert
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone VARCHAR(20),
    @DepartmentID INT = NULL,
    @Address NVARCHAR(500) = NULL,
    @ImagePath NVARCHAR(500) = NULL,
    @HireDate DATETIME = NULL,
    @IsActive BIT = 1,
  @NewEmployeeID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
  
    -- Kiểm tra các trường bắt buộc
        IF @FullName IS NULL OR LTRIM(RTRIM(@FullName)) = ''
        BEGIN
 PRINT 'FullName là bắt buộc';
  ROLLBACK TRANSACTION;
            RETURN -1;
        END
     
    -- Kiểm tra email đã tồn tại chưa
  IF @Email IS NOT NULL AND EXISTS (SELECT 1 FROM dbo.Employees WHERE Email = @Email)
      BEGIN
        PRINT 'Email đã tồn tại';
    ROLLBACK TRANSACTION;
   RETURN -1;
        END
    
        -- Kiểm tra phòng ban có tồn tại không (nếu được cung cấp)
 IF @DepartmentID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Departments WHERE DepartmentID = @DepartmentID)
  BEGIN
    PRINT 'Phòng ban không tồn tại';
   ROLLBACK TRANSACTION;
      RETURN -1;
      END
   
        -- Đặt HireDate là ngày hiện tại nếu không được cung cấp
        IF @HireDate IS NULL
         SET @HireDate = GETDATE();
    
  -- Thêm nhân viên
    INSERT INTO dbo.Employees (FullName, Email, Phone, DepartmentID, ImagePath, Address, HireDate, IsActive)
  VALUES (@FullName, @Email, @Phone, @DepartmentID, @ImagePath, @Address, @HireDate, @IsActive);
 
      -- Lấy ID mới
        SET @NewEmployeeID = SCOPE_IDENTITY();
        
COMMIT TRANSACTION;
   PRINT 'Tạo nhân viên thành công với ID: ' + CAST(@NewEmployeeID AS NVARCHAR(10));
        RETURN @NewEmployeeID;
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
  ROLLBACK TRANSACTION;
  
  PRINT ERROR_MESSAGE();
   SET @NewEmployeeID = -1;
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Employee_Insert';
GO

-- ---------------------------------------------
-- sp_Employee_Update
-- Mô tả: Cập nhật thông tin nhân viên
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Employee_Update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Employee_Update;
GO

CREATE PROCEDURE dbo.sp_Employee_Update
    @EmployeeID INT,
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
  @Phone VARCHAR(20),
    @DepartmentID INT = NULL,
    @Address NVARCHAR(500) = NULL,
    @ImagePath NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
  
    BEGIN TRY
        BEGIN TRANSACTION;
        
  -- Kiểm tra nhân viên có tồn tại không
 IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
    BEGIN
    PRINT 'Không tìm thấy nhân viên';
    ROLLBACK TRANSACTION;
    RETURN -1;
  END
   
        -- Kiểm tra các trường bắt buộc
    IF @FullName IS NULL OR LTRIM(RTRIM(@FullName)) = ''
        BEGIN
    PRINT 'FullName là bắt buộc';
    ROLLBACK TRANSACTION;
  RETURN -1;
  END
  
   -- Kiểm tra email đã tồn tại cho nhân viên khác chưa
      IF @Email IS NOT NULL AND EXISTS (
  SELECT 1 FROM dbo.Employees 
WHERE Email = @Email AND EmployeeID != @EmployeeID
        )
    BEGIN
 PRINT 'Email đã tồn tại cho nhân viên khác';
          ROLLBACK TRANSACTION;
  RETURN -1;
   END
    
        -- Kiểm tra phòng ban có tồn tại không (nếu được cung cấp)
IF @DepartmentID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Departments WHERE DepartmentID = @DepartmentID)
        BEGIN
   PRINT 'Phòng ban không tồn tại';
   ROLLBACK TRANSACTION;
         RETURN -1;
     END
   
        -- Cập nhật nhân viên
        UPDATE dbo.Employees
    SET 
    FullName = @FullName,
      Email = @Email,
    Phone = @Phone,
      DepartmentID = @DepartmentID,
     Address = @Address,
        ImagePath = @ImagePath
        WHERE EmployeeID = @EmployeeID;
  
    COMMIT TRANSACTION;
   PRINT 'Cập nhật nhân viên thành công';
RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
  IF @@TRANCOUNT > 0
     ROLLBACK TRANSACTION;

     PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Employee_Update';
GO

-- ---------------------------------------------
-- sp_Employee_Delete
-- Mô tả: Xóa mềm nhân viên (đặt IsActive = 0)
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Employee_Delete', 'P') IS NOT NULL
  DROP PROCEDURE dbo.sp_Employee_Delete;
GO

CREATE PROCEDURE dbo.sp_Employee_Delete
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
    BEGIN TRANSACTION;
      
        -- Kiểm tra nhân viên có tồn tại không
 IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
 BEGIN
        PRINT 'Không tìm thấy nhân viên';
ROLLBACK TRANSACTION;
      RETURN -1;
     END
        
     -- Kiểm tra nhân viên có phải là quản lý phòng ban không
      IF EXISTS (SELECT 1 FROM dbo.Departments WHERE ManagerID = @EmployeeID)
  BEGIN
       PRINT 'Không thể xóa nhân viên đang là quản lý phòng ban';
    ROLLBACK TRANSACTION;
       RETURN -1;
     END
    
  -- Xóa mềm
        UPDATE dbo.Employees
  SET IsActive = 0
  WHERE EmployeeID = @EmployeeID;
        
   COMMIT TRANSACTION;
     PRINT 'Xóa nhân viên thành công';
     RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
 IF @@TRANCOUNT > 0
          ROLLBACK TRANSACTION;
  
        PRINT ERROR_MESSAGE();
RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Employee_Delete';
GO

-- ---------------------------------------------
-- sp_Employee_GetByDepartment
-- Mô tả: Lấy tất cả nhân viên trong một phòng ban cụ thể
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Employee_GetByDepartment', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Employee_GetByDepartment;
GO

CREATE PROCEDURE dbo.sp_Employee_GetByDepartment
    @DepartmentID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
    -- Kiểm tra phòng ban có tồn tại không
  IF NOT EXISTS (SELECT 1 FROM dbo.Departments WHERE DepartmentID = @DepartmentID)
        BEGIN
   PRINT 'Không tìm thấy phòng ban';
 RETURN -1;
     END
        
      SELECT 
       e.EmployeeID,
       e.FullName,
e.Email,
 e.Phone,
   e.DepartmentID,
        d.DepartmentName,
    e.ImagePath,
    e.Address,
  e.HireDate,
        e.IsActive
        FROM dbo.Employees e
   LEFT JOIN dbo.Departments d ON e.DepartmentID = d.DepartmentID
   WHERE e.DepartmentID = @DepartmentID AND e.IsActive = 1
   ORDER BY e.FullName;
        
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
 PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Employee_GetByDepartment';
GO

-- =============================================
-- PHẦN 2: CÁC THỦ TỤC CHO PROJECT (DỰ ÁN)
-- =============================================

-- ---------------------------------------------
-- sp_Project_GetAll
-- Mô tả: Lấy tất cả dự án với thông tin người tạo
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Project_GetAll', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Project_GetAll;
GO

CREATE PROCEDURE dbo.sp_Project_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
   p.ProjectID,
     p.ProjectName,
p.Description,
      p.StartDate,
 p.EndDate,
 p.Status,
  p.Budget,
          p.CreatedBy,
     u.Username AS CreatedByUsername,
 p.CreatedDate
    FROM dbo.Projects p
        INNER JOIN dbo.Users u ON p.CreatedBy = u.UserID
ORDER BY p.CreatedDate DESC;
 
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
    PRINT ERROR_MESSAGE();
    RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Project_GetAll';
GO

-- ---------------------------------------------
-- sp_Project_GetById
-- Mô tả: Lấy thông tin dự án theo ID với thông tin người tạo
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Project_GetById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Project_GetById;
GO

CREATE PROCEDURE dbo.sp_Project_GetById
    @ProjectID INT
AS
BEGIN
    SET NOCOUNT ON;
  
    BEGIN TRY
        -- Kiểm tra dự án có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
       PRINT 'Không tìm thấy dự án';
      RETURN -1;
    END
        
        SELECT 
       p.ProjectID,
    p.ProjectName,
     p.Description,
 p.StartDate,
          p.EndDate,
        p.Status,
   p.Budget,
   p.CreatedBy,
       u.Username AS CreatedByUsername,
   p.CreatedDate
 FROM dbo.Projects p
    INNER JOIN dbo.Users u ON p.CreatedBy = u.UserID
        WHERE p.ProjectID = @ProjectID;
 
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
 PRINT ERROR_MESSAGE();
  RETURN -1; -- Lỗi
  END CATCH
END
GO

PRINT 'Đã tạo: sp_Project_GetById';
GO

-- ---------------------------------------------
-- sp_Project_Insert
-- Mô tả: Thêm dự án mới
-- Trả về: ProjectID mới hoặc -1 nếu thất bại
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Project_Insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Project_Insert;
GO

CREATE PROCEDURE dbo.sp_Project_Insert
    @ProjectName NVARCHAR(200),
    @Description NVARCHAR(MAX) = NULL,
  @StartDate DATE,
    @EndDate DATE = NULL,
    @Status NVARCHAR(20),
    @Budget DECIMAL(18,2) = NULL,
    @CreatedBy INT,
    @NewProjectID INT OUTPUT
AS
BEGIN
SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Kiểm tra các trường bắt buộc
 IF @ProjectName IS NULL OR LTRIM(RTRIM(@ProjectName)) = ''
BEGIN
      PRINT 'ProjectName là bắt buộc';
  ROLLBACK TRANSACTION;
    RETURN -1;
        END
        
   -- Kiểm tra trạng thái hợp lệ
  IF @Status NOT IN ('Planning', 'InProgress', 'Completed', 'Cancelled')
      BEGIN
        PRINT 'Trạng thái không hợp lệ. Phải là: Planning, InProgress, Completed, hoặc Cancelled';
  ROLLBACK TRANSACTION;
  RETURN -1;
   END
        
        -- Kiểm tra ngày tháng
      IF @EndDate IS NOT NULL AND @EndDate < @StartDate
 BEGIN
   PRINT 'EndDate không thể trước StartDate';
  ROLLBACK TRANSACTION;
     RETURN -1;
   END
    
 -- Kiểm tra người tạo có tồn tại không
     IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @CreatedBy)
        BEGIN
 PRINT 'Người tạo không tồn tại';
          ROLLBACK TRANSACTION;
 RETURN -1;
  END
   
    -- Thêm dự án
      INSERT INTO dbo.Projects (ProjectName, Description, StartDate, EndDate, Status, Budget, CreatedBy, CreatedDate)
     VALUES (@ProjectName, @Description, @StartDate, @EndDate, @Status, @Budget, @CreatedBy, GETDATE());
        
   -- Lấy ID mới
  SET @NewProjectID = SCOPE_IDENTITY();
   
COMMIT TRANSACTION;
        PRINT 'Tạo dự án thành công với ID: ' + CAST(@NewProjectID AS NVARCHAR(10));
        RETURN @NewProjectID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
   ROLLBACK TRANSACTION;
        
  PRINT ERROR_MESSAGE();
   SET @NewProjectID = -1;
    RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Project_Insert';
GO

-- ---------------------------------------------
-- sp_Project_Update
-- Mô tả: Cập nhật thông tin dự án
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Project_Update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Project_Update;
GO

CREATE PROCEDURE dbo.sp_Project_Update
    @ProjectID INT,
    @ProjectName NVARCHAR(200),
    @Description NVARCHAR(MAX) = NULL,
  @StartDate DATE,
    @EndDate DATE = NULL,
    @Status NVARCHAR(20),
 @Budget DECIMAL(18,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
  -- Kiểm tra dự án có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
 BEGIN
  PRINT 'Không tìm thấy dự án';
ROLLBACK TRANSACTION;
 RETURN -1;
        END
        
        -- Kiểm tra các trường bắt buộc
        IF @ProjectName IS NULL OR LTRIM(RTRIM(@ProjectName)) = ''
 BEGIN
    PRINT 'ProjectName là bắt buộc';
     ROLLBACK TRANSACTION;
   RETURN -1;
  END
   
    -- Kiểm tra trạng thái hợp lệ
    IF @Status NOT IN ('Planning', 'InProgress', 'Completed', 'Cancelled')
   BEGIN
 PRINT 'Trạng thái không hợp lệ. Phải là: Planning, InProgress, Completed, hoặc Cancelled';
      ROLLBACK TRANSACTION;
  RETURN -1;
     END

-- Kiểm tra ngày tháng
        IF @EndDate IS NOT NULL AND @EndDate < @StartDate
  BEGIN
          PRINT 'EndDate không thể trước StartDate';
     ROLLBACK TRANSACTION;
    RETURN -1;
 END
        
   -- Cập nhật dự án
 UPDATE dbo.Projects
SET 
    ProjectName = @ProjectName,
       Description = @Description,
 StartDate = @StartDate,
       EndDate = @EndDate,
            Status = @Status,
   Budget = @Budget
   WHERE ProjectID = @ProjectID;
   
   COMMIT TRANSACTION;
   PRINT 'Cập nhật dự án thành công';
RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
  IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
   
        PRINT ERROR_MESSAGE();
      RETURN -1; -- Lỗi
END CATCH
END
GO

PRINT 'Đã tạo: sp_Project_Update';
GO

-- ---------------------------------------------
-- sp_Project_Delete
-- Mô tả: Xóa dự án (CASCADE sẽ xóa các task liên quan)
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Project_Delete', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Project_Delete;
GO

CREATE PROCEDURE dbo.sp_Project_Delete
    @ProjectID INT
AS
BEGIN
    SET NOCOUNT ON;
  
    BEGIN TRY
   BEGIN TRANSACTION;
        
        -- Kiểm tra dự án có tồn tại không
 IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
 PRINT 'Không tìm thấy dự án';
 ROLLBACK TRANSACTION;
     RETURN -1;
        END
   
  -- Xóa dự án (CASCADE sẽ xử lý tasks và comments)
DELETE FROM dbo.Projects WHERE ProjectID = @ProjectID;
      
  COMMIT TRANSACTION;
   PRINT 'Xóa dự án thành công';
  RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
   IF @@TRANCOUNT > 0
      ROLLBACK TRANSACTION;
        
   PRINT ERROR_MESSAGE();
    RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Project_Delete';
GO

-- ---------------------------------------------
-- sp_Project_GetByStatus
-- Mô tả: Lấy tất cả dự án theo trạng thái
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Project_GetByStatus', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Project_GetByStatus;
GO

CREATE PROCEDURE dbo.sp_Project_GetByStatus
    @Status NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
  -- Kiểm tra trạng thái hợp lệ
   IF @Status NOT IN ('Planning', 'InProgress', 'Completed', 'Cancelled')
 BEGIN
 PRINT 'Trạng thái không hợp lệ. Phải là: Planning, InProgress, Completed, hoặc Cancelled';
     RETURN -1;
  END
        
     SELECT 
    p.ProjectID,
p.ProjectName,
   p.Description,
 p.StartDate,
     p.EndDate,
p.Status,
    p.Budget,
    p.CreatedBy,
  u.Username AS CreatedByUsername,
  p.CreatedDate
     FROM dbo.Projects p
   INNER JOIN dbo.Users u ON p.CreatedBy = u.UserID
   WHERE p.Status = @Status
      ORDER BY p.CreatedDate DESC;

  RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
   PRINT ERROR_MESSAGE();
    RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Project_GetByStatus';
GO

-- =============================================
-- PHẦN 3: CÁC THỦ TỤC CHO TASK (CÔNG VIỆC)
-- =============================================

-- ---------------------------------------------
-- sp_Task_GetAll
-- Mô tả: Lấy tất cả task với thông tin liên quan
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_GetAll', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_GetAll;
GO

CREATE PROCEDURE dbo.sp_Task_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
    SELECT 
         t.TaskID,
      t.ProjectID,
        p.ProjectName,
       t.TaskTitle,
      t.Description,
     t.AssignedTo,
      e.FullName AS AssignedToName,
 t.CreatedBy,
 u.Username AS CreatedByUsername,
    t.Deadline,
     t.Status,
 t.Priority,
       t.Progress,
      t.CreatedDate,
  t.UpdatedDate
 FROM dbo.Tasks t
  INNER JOIN dbo.Projects p ON t.ProjectID = p.ProjectID
   LEFT JOIN dbo.Employees e ON t.AssignedTo = e.EmployeeID
   INNER JOIN dbo.Users u ON t.CreatedBy = u.UserID
     ORDER BY t.CreatedDate DESC;
  
 RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
     PRINT ERROR_MESSAGE();
   RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_GetAll';
GO

-- ---------------------------------------------
-- sp_Task_GetById
-- Mô tả: Lấy thông tin task theo ID với thông tin liên quan
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_GetById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_GetById;
GO

CREATE PROCEDURE dbo.sp_Task_GetById
    @TaskID INT
AS
BEGIN
    SET NOCOUNT ON;
  
    BEGIN TRY
        -- Kiểm tra task có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
        BEGIN
    PRINT 'Không tìm thấy task';
 RETURN -1;
   END
        
   SELECT 
    t.TaskID,
  t.ProjectID,
  p.ProjectName,
   t.TaskTitle,
   t.Description,
   t.AssignedTo,
   e.FullName AS AssignedToName,
       t.CreatedBy,
 u.Username AS CreatedByUsername,
          t.Deadline,
       t.Status,
 t.Priority,
        t.Progress,
     t.CreatedDate,
    t.UpdatedDate
FROM dbo.Tasks t
   INNER JOIN dbo.Projects p ON t.ProjectID = p.ProjectID
  LEFT JOIN dbo.Employees e ON t.AssignedTo = e.EmployeeID
  INNER JOIN dbo.Users u ON t.CreatedBy = u.UserID
   WHERE t.TaskID = @TaskID;
     
        RETURN 0; -- Thành công
  END TRY
    BEGIN CATCH
   PRINT ERROR_MESSAGE();
    RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_GetById';
GO

-- ---------------------------------------------
-- sp_Task_GetByProject
-- Mô tả: Lấy tất cả task của một dự án cụ thể
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_GetByProject', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_GetByProject;
GO

CREATE PROCEDURE dbo.sp_Task_GetByProject
  @ProjectID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra dự án có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
            PRINT 'Không tìm thấy dự án';
  RETURN -1;
 END

      SELECT 
     t.TaskID,
       t.ProjectID,
     p.ProjectName,
  t.TaskTitle,
  t.Description,
        t.AssignedTo,
e.FullName AS AssignedToName,
            t.CreatedBy,
     u.Username AS CreatedByUsername,
t.Deadline,
    t.Status,
      t.Priority,
    t.Progress,
      t.CreatedDate,
   t.UpdatedDate
        FROM dbo.Tasks t
 INNER JOIN dbo.Projects p ON t.ProjectID = p.ProjectID
LEFT JOIN dbo.Employees e ON t.AssignedTo = e.EmployeeID
   INNER JOIN dbo.Users u ON t.CreatedBy = u.UserID
    WHERE t.ProjectID = @ProjectID
        ORDER BY t.Deadline ASC, t.Priority DESC;
 
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
      PRINT ERROR_MESSAGE();
     RETURN -1; -- Lỗi
END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_GetByProject';
GO

-- ---------------------------------------------
-- sp_Task_GetByEmployee
-- Mô tả: Lấy tất cả task được giao cho một nhân viên
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_GetByEmployee', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_GetByEmployee;
GO

CREATE PROCEDURE dbo.sp_Task_GetByEmployee
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
      -- Kiểm tra nhân viên có tồn tại không
   IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
    BEGIN
 PRINT 'Không tìm thấy nhân viên';
         RETURN -1;
        END

        SELECT 
t.TaskID,
    t.ProjectID,
            p.ProjectName,
  t.TaskTitle,
        t.Description,
        t.AssignedTo,
   e.FullName AS AssignedToName,
    t.CreatedBy,
   u.Username AS CreatedByUsername,
     t.Deadline,
          t.Status,
         t.Priority,
      t.Progress,
    t.CreatedDate,
      t.UpdatedDate
   FROM dbo.Tasks t
        INNER JOIN dbo.Projects p ON t.ProjectID = p.ProjectID
        LEFT JOIN dbo.Employees e ON t.AssignedTo = e.EmployeeID
        INNER JOIN dbo.Users u ON t.CreatedBy = u.UserID
 WHERE t.AssignedTo = @EmployeeID
    ORDER BY t.Deadline ASC, t.Priority DESC;
     
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
    PRINT ERROR_MESSAGE();
  RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_GetByEmployee';
GO

-- ---------------------------------------------
-- sp_Task_Insert
-- Mô tả: Thêm task mới
-- Trả về: TaskID mới hoặc -1 nếu thất bại
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_Insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_Insert;
GO

CREATE PROCEDURE dbo.sp_Task_Insert
    @ProjectID INT,
    @TaskTitle NVARCHAR(200),
@Description NVARCHAR(MAX) = NULL,
  @AssignedTo INT = NULL,
    @CreatedBy INT,
    @Deadline DATE = NULL,
    @Status NVARCHAR(20),
    @Priority NVARCHAR(20),
    @Progress INT = 0,
    @NewTaskID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
BEGIN TRY
        BEGIN TRANSACTION;
        
   -- Kiểm tra các trường bắt buộc
  IF @TaskTitle IS NULL OR LTRIM(RTRIM(@TaskTitle)) = ''
     BEGIN
       PRINT 'TaskTitle là bắt buộc';
    ROLLBACK TRANSACTION;
        RETURN -1;
   END
      
   -- Kiểm tra dự án có tồn tại không
     IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
    PRINT 'Dự án không tồn tại';
   ROLLBACK TRANSACTION;
       RETURN -1;
        END
        
-- Kiểm tra trạng thái hợp lệ
        IF @Status NOT IN ('Todo', 'InProgress', 'Review', 'Done')
     BEGIN
      PRINT 'Trạng thái không hợp lệ. Phải là: Todo, InProgress, Review, hoặc Done';
         ROLLBACK TRANSACTION;
            RETURN -1;
  END
        
    -- Kiểm tra độ ưu tiên hợp lệ
      IF @Priority NOT IN ('Low', 'Medium', 'High', 'Critical')
   BEGIN
          PRINT 'Độ ưu tiên không hợp lệ. Phải là: Low, Medium, High, hoặc Critical';
    ROLLBACK TRANSACTION;
    RETURN -1;
 END
    
        -- Kiểm tra phạm vi tiến độ
        IF @Progress < 0 OR @Progress > 100
   BEGIN
        PRINT 'Tiến độ phải từ 0 đến 100';
 ROLLBACK TRANSACTION;
       RETURN -1;
        END
     
        -- Kiểm tra nhân viên được giao có tồn tại không (nếu được cung cấp)
    IF @AssignedTo IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @AssignedTo AND IsActive = 1)
    BEGIN
    PRINT 'Nhân viên được giao không tồn tại hoặc không hoạt động';
            ROLLBACK TRANSACTION;
  RETURN -1;
        END
     
    -- Kiểm tra người tạo có tồn tại không
 IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @CreatedBy)
        BEGIN
     PRINT 'Người tạo không tồn tại';
 ROLLBACK TRANSACTION;
     RETURN -1;
    END
        
    -- Thêm task
 INSERT INTO dbo.Tasks (ProjectID, TaskTitle, Description, AssignedTo, CreatedBy, Deadline, Status, Priority, Progress, CreatedDate)
  VALUES (@ProjectID, @TaskTitle, @Description, @AssignedTo, @CreatedBy, @Deadline, @Status, @Priority, @Progress, GETDATE());
      
      -- Lấy ID mới
        SET @NewTaskID = SCOPE_IDENTITY();
     
        COMMIT TRANSACTION;
        PRINT 'Tạo task thành công với ID: ' + CAST(@NewTaskID AS NVARCHAR(10));
   RETURN @NewTaskID;
  END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
      ROLLBACK TRANSACTION;
   
 PRINT ERROR_MESSAGE();
        SET @NewTaskID = -1;
        RETURN -1; -- Lỗi
  END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_Insert';
GO

-- ---------------------------------------------
-- sp_Task_Update
-- Mô tả: Cập nhật thông tin task
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_Update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_Update;
GO

CREATE PROCEDURE dbo.sp_Task_Update
    @TaskID INT,
    @TaskTitle NVARCHAR(200),
    @Description NVARCHAR(MAX) = NULL,
    @AssignedTo INT = NULL,
    @Deadline DATE = NULL,
    @Status NVARCHAR(20),
    @Priority NVARCHAR(20),
    @Progress INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
      BEGIN TRANSACTION;
        
        -- Kiểm tra task có tồn tại không
     IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
  BEGIN
       PRINT 'Không tìm thấy task';
 ROLLBACK TRANSACTION;
     RETURN -1;
        END
        
 -- Kiểm tra các trường bắt buộc
        IF @TaskTitle IS NULL OR LTRIM(RTRIM(@TaskTitle)) = ''
 BEGIN
    PRINT 'TaskTitle là bắt buộc';
ROLLBACK TRANSACTION;
  RETURN -1;
    END
        
     -- Kiểm tra trạng thái hợp lệ
IF @Status NOT IN ('Todo', 'InProgress', 'Review', 'Done')
 BEGIN
    PRINT 'Trạng thái không hợp lệ. Phải là: Todo, InProgress, Review, hoặc Done';
  ROLLBACK TRANSACTION;
            RETURN -1;
        END
        
 -- Kiểm tra độ ưu tiên hợp lệ
        IF @Priority NOT IN ('Low', 'Medium', 'High', 'Critical')
   BEGIN
          PRINT 'Độ ưu tiên không hợp lệ. Phải là: Low, Medium, High, hoặc Critical';
 ROLLBACK TRANSACTION;
            RETURN -1;
   END
      
      -- Kiểm tra phạm vi tiến độ
   IF @Progress < 0 OR @Progress > 100
      BEGIN
PRINT 'Tiến độ phải từ 0 đến 100';
  ROLLBACK TRANSACTION;
       RETURN -1;
        END
  
        -- Kiểm tra nhân viên được giao có tồn tại không (nếu được cung cấp)
        IF @AssignedTo IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @AssignedTo AND IsActive = 1)
        BEGIN
            PRINT 'Nhân viên được giao không tồn tại hoặc không hoạt động';
       ROLLBACK TRANSACTION;
  RETURN -1;
        END
 
 -- Cập nhật task
UPDATE dbo.Tasks
   SET 
       TaskTitle = @TaskTitle,
        Description = @Description,
  AssignedTo = @AssignedTo,
      Deadline = @Deadline,
    Status = @Status,
       Priority = @Priority,
     Progress = @Progress,
            UpdatedDate = GETDATE()
WHERE TaskID = @TaskID;
        
     COMMIT TRANSACTION;
      PRINT 'Cập nhật task thành công';
 RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
   IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
    
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_Update';
GO

-- ---------------------------------------------
-- sp_Task_Delete
-- Mô tả: Xóa task (CASCADE sẽ xóa các comment liên quan)
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_Delete', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_Delete;
GO

CREATE PROCEDURE dbo.sp_Task_Delete
    @TaskID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
      
  -- Kiểm tra task có tồn tại không
  IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
     BEGIN
 PRINT 'Không tìm thấy task';
    ROLLBACK TRANSACTION;
   RETURN -1;
        END
      
        -- Xóa task (CASCADE sẽ xử lý comments)
        DELETE FROM dbo.Tasks WHERE TaskID = @TaskID;
 
        COMMIT TRANSACTION;
        PRINT 'Xóa task thành công';
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
      IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
   
      PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_Delete';
GO

-- ---------------------------------------------
-- sp_Task_UpdateProgress
-- Mô tả: Chỉ cập nhật tiến độ task
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Task_UpdateProgress', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Task_UpdateProgress;
GO

CREATE PROCEDURE dbo.sp_Task_UpdateProgress
    @TaskID INT,
    @Progress INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra task có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
 BEGIN
       PRINT 'Không tìm thấy task';
            ROLLBACK TRANSACTION;
       RETURN -1;
        END

   -- Kiểm tra phạm vi tiến độ
      IF @Progress < 0 OR @Progress > 100
   BEGIN
  PRINT 'Tiến độ phải từ 0 đến 100';
   ROLLBACK TRANSACTION;
     RETURN -1;
        END
    
        -- Cập nhật tiến độ
 UPDATE dbo.Tasks
        SET 
  Progress = @Progress,
            UpdatedDate = GETDATE()
    WHERE TaskID = @TaskID;
        
        COMMIT TRANSACTION;
        PRINT 'Cập nhật tiến độ task thành công';
      RETURN 0; -- Thành công
  END TRY
    BEGIN CATCH
      IF @@TRANCOUNT > 0
       ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Task_UpdateProgress';
GO

-- =============================================
-- PHẦN 4: CÁC THỦ TỤC BÁO CÁO
-- =============================================

-- ---------------------------------------------
-- sp_Report_TasksByStatus
-- Mô tả: Nhóm task theo trạng thái với số lượng
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Report_TasksByStatus', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Report_TasksByStatus;
GO

CREATE PROCEDURE dbo.sp_Report_TasksByStatus
AS
BEGIN
    SET NOCOUNT ON;
 
    BEGIN TRY
        SELECT 
       Status,
      COUNT(*) AS TaskCount,
       AVG(Progress) AS AvgProgress,
      COUNT(CASE WHEN Deadline < GETDATE() AND Status NOT IN ('Done') THEN 1 END) AS OverdueCount
        FROM dbo.Tasks
        GROUP BY Status
        ORDER BY 
      CASE Status
   WHEN 'Todo' THEN 1
    WHEN 'InProgress' THEN 2
WHEN 'Review' THEN 3
     WHEN 'Done' THEN 4
  END;
  
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Report_TasksByStatus';
GO

-- ---------------------------------------------
-- sp_Report_EmployeeWorkload
-- Mô tả: Lấy số lượng task được giao cho mỗi nhân viên
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Report_EmployeeWorkload', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Report_EmployeeWorkload;
GO

CREATE PROCEDURE dbo.sp_Report_EmployeeWorkload
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
     SELECT 
     e.EmployeeID,
    e.FullName,
       d.DepartmentName,
 COUNT(t.TaskID) AS TotalTasks,
        SUM(CASE WHEN t.Status = 'Todo' THEN 1 ELSE 0 END) AS TodoTasks,
      SUM(CASE WHEN t.Status = 'InProgress' THEN 1 ELSE 0 END) AS InProgressTasks,
            SUM(CASE WHEN t.Status = 'Review' THEN 1 ELSE 0 END) AS ReviewTasks,
          SUM(CASE WHEN t.Status = 'Done' THEN 1 ELSE 0 END) AS DoneTasks,
         SUM(CASE WHEN t.Deadline < GETDATE() AND t.Status NOT IN ('Done') THEN 1 ELSE 0 END) AS OverdueTasks,
 AVG(CAST(t.Progress AS FLOAT)) AS AvgProgress
        FROM dbo.Employees e
 LEFT JOIN dbo.Departments d ON e.DepartmentID = d.DepartmentID
        LEFT JOIN dbo.Tasks t ON e.EmployeeID = t.AssignedTo
     WHERE e.IsActive = 1
 GROUP BY e.EmployeeID, e.FullName, d.DepartmentName
 ORDER BY TotalTasks DESC, e.FullName;
        
   RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
      PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Report_EmployeeWorkload';
GO

-- ---------------------------------------------
-- sp_Report_ProjectProgress
-- Mô tả: Lấy phần trăm hoàn thành cho mỗi dự án
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Report_ProjectProgress', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Report_ProjectProgress;
GO

CREATE PROCEDURE dbo.sp_Report_ProjectProgress
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
            p.ProjectID,
    p.ProjectName,
     p.Status AS ProjectStatus,
p.StartDate,
      p.EndDate,
    p.Budget,
     COUNT(t.TaskID) AS TotalTasks,
SUM(CASE WHEN t.Status = 'Done' THEN 1 ELSE 0 END) AS CompletedTasks,
     CASE 
    WHEN COUNT(t.TaskID) > 0 
   THEN CAST(SUM(CASE WHEN t.Status = 'Done' THEN 1 ELSE 0 END) * 100.0 / COUNT(t.TaskID) AS DECIMAL(5,2))
    ELSE 0 
   END AS CompletionPercentage,
         AVG(CAST(t.Progress AS FLOAT)) AS AvgTaskProgress,
SUM(CASE WHEN t.Deadline < GETDATE() AND t.Status NOT IN ('Done') THEN 1 ELSE 0 END) AS OverdueTasks,
    CASE 
     WHEN p.EndDate < GETDATE() AND p.Status NOT IN ('Completed', 'Cancelled') THEN 'Overdue'
                WHEN p.EndDate IS NOT NULL AND DATEDIFF(DAY, GETDATE(), p.EndDate) <= 7 AND p.Status NOT IN ('Completed', 'Cancelled') THEN 'Due Soon'
            ELSE 'On Track'
            END AS ProjectHealth
        FROM dbo.Projects p
        LEFT JOIN dbo.Tasks t ON p.ProjectID = t.ProjectID
        GROUP BY p.ProjectID, p.ProjectName, p.Status, p.StartDate, p.EndDate, p.Budget
        ORDER BY p.Status, p.ProjectName;
        
        RETURN 0; -- Thành công
  END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
     RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Report_ProjectProgress';
GO

-- ---------------------------------------------
-- sp_Report_OverdueTasks
-- Mô tả: Lấy tất cả task quá hạn và chưa hoàn thành
-- ---------------------------------------------
IF OBJECT_ID('dbo.sp_Report_OverdueTasks', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_Report_OverdueTasks;
GO

CREATE PROCEDURE dbo.sp_Report_OverdueTasks
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        SELECT 
          t.TaskID,
t.TaskTitle,
p.ProjectName,
t.Status,
       t.Priority,
      t.Progress,
      t.Deadline,
        DATEDIFF(DAY, t.Deadline, GETDATE()) AS DaysOverdue,
e.FullName AS AssignedToName,
  e.Email AS AssignedToEmail,
  d.DepartmentName,
    u.Username AS CreatedBy
 FROM dbo.Tasks t
        INNER JOIN dbo.Projects p ON t.ProjectID = p.ProjectID
        LEFT JOIN dbo.Employees e ON t.AssignedTo = e.EmployeeID
        LEFT JOIN dbo.Departments d ON e.DepartmentID = d.DepartmentID
   INNER JOIN dbo.Users u ON t.CreatedBy = u.UserID
   WHERE t.Deadline < GETDATE() 
          AND t.Status NOT IN ('Done')
        ORDER BY t.Deadline ASC, t.Priority DESC;
        
        RETURN 0; -- Thành công
    END TRY
    BEGIN CATCH
     PRINT ERROR_MESSAGE();
        RETURN -1; -- Lỗi
    END CATCH
END
GO

PRINT 'Đã tạo: sp_Report_OverdueTasks';
GO

-- =============================================
-- TỔNG KẾT HOÀN THÀNH
-- =============================================

PRINT '';
PRINT '========================================';
PRINT '  ĐÃ TẠO STORED PROCEDURES!';
PRINT '========================================';
PRINT 'Tổng số Procedures: 28';
PRINT '';
PRINT 'Procedures cho EMPLOYEE: 6';
PRINT '  - sp_Employee_GetAll';
PRINT '  - sp_Employee_GetById';
PRINT '  - sp_Employee_Insert';
PRINT '  - sp_Employee_Update';
PRINT '  - sp_Employee_Delete';
PRINT '  - sp_Employee_GetByDepartment';
PRINT '';
PRINT 'Procedures cho PROJECT: 6';
PRINT '  - sp_Project_GetAll';
PRINT '  - sp_Project_GetById';
PRINT '  - sp_Project_Insert';
PRINT '  - sp_Project_Update';
PRINT '  - sp_Project_Delete';
PRINT '  - sp_Project_GetByStatus';
PRINT '';
PRINT 'Procedures cho TASK: 8';
PRINT '  - sp_Task_GetAll';
PRINT '  - sp_Task_GetById';
PRINT '  - sp_Task_GetByProject';
PRINT '  - sp_Task_GetByEmployee';
PRINT '  - sp_Task_Insert';
PRINT '  - sp_Task_Update';
PRINT '  - sp_Task_Delete';
PRINT '  - sp_Task_UpdateProgress';
PRINT '';
PRINT 'Procedures cho REPORT: 4';
PRINT '  - sp_Report_TasksByStatus';
PRINT '  - sp_Report_EmployeeWorkload';
PRINT '  - sp_Report_ProjectProgress';
PRINT '  - sp_Report_OverdueTasks';
PRINT '========================================';
GO

-- =============================================
-- PHẦN 5: CÁC LỆNH TEST MẪU
-- =============================================

PRINT '';
PRINT '========================================';
PRINT '  ĐANG CHẠY CÁC LỆNH TEST MẪU';
PRINT '========================================';
GO

-- Test 1: Lấy tất cả nhân viên
PRINT '';
PRINT '--- TEST 1: Lấy tất cả nhân viên ---';
EXEC dbo.sp_Employee_GetAll;
GO

-- Test 2: Lấy nhân viên theo ID
PRINT '';
PRINT '--- TEST 2: Lấy nhân viên theo ID (EmployeeID=1) ---';
EXEC dbo.sp_Employee_GetById @EmployeeID = 1;
GO

-- Test 3: Lấy nhân viên theo phòng ban
PRINT '';
PRINT '--- TEST 3: Lấy nhân viên theo phòng ban (DepartmentID=1) ---';
EXEC dbo.sp_Employee_GetByDepartment @DepartmentID = 1;
GO

-- Test 4: Thêm nhân viên mới
PRINT '';
PRINT '--- TEST 4: Thêm nhân viên mới ---';
DECLARE @NewEmpID INT;
EXEC dbo.sp_Employee_Insert 
    @FullName = N'Nguyễn Thị Mai',
    @Email = 'nguyen.thi.mai@company.com',
    @Phone = '0956789012',
    @DepartmentID = 2,
    @Address = N'303 Hai Bà Trưng, Hà Nội',
    @ImagePath = '/images/employees/ntm.jpg',
    @NewEmployeeID = @NewEmpID OUTPUT;
SELECT @NewEmpID AS NewEmployeeID;
GO

-- Test 5: Lấy tất cả dự án
PRINT '';
PRINT '--- TEST 5: Lấy tất cả dự án ---';
EXEC dbo.sp_Project_GetAll;
GO

-- Test 6: Lấy dự án theo trạng thái
PRINT '';
PRINT '--- TEST 6: Lấy dự án theo trạng thái (InProgress) ---';
EXEC dbo.sp_Project_GetByStatus @Status = 'InProgress';
GO

-- Test 7: Lấy task theo dự án
PRINT '';
PRINT '--- TEST 7: Lấy task theo dự án (ProjectID=1) ---';
EXEC dbo.sp_Task_GetByProject @ProjectID = 1;
GO

-- Test 8: Lấy task theo nhân viên
PRINT '';
PRINT '--- TEST 8: Lấy task theo nhân viên (EmployeeID=3) ---';
EXEC dbo.sp_Task_GetByEmployee @EmployeeID = 3;
GO

-- Test 9: Cập nhật tiến độ task
PRINT '';
PRINT '--- TEST 9: Cập nhật tiến độ task (TaskID=2, Progress=75) ---';
EXEC dbo.sp_Task_UpdateProgress @TaskID = 2, @Progress = 75;
GO

-- Test 10: Báo cáo - Task theo trạng thái
PRINT '';
PRINT '--- TEST 10: Báo cáo - Task theo trạng thái ---';
EXEC dbo.sp_Report_TasksByStatus;
GO

-- Test 11: Báo cáo - Khối lượng công việc nhân viên
PRINT '';
PRINT '--- TEST 11: Báo cáo - Khối lượng công việc nhân viên ---';
EXEC dbo.sp_Report_EmployeeWorkload;
GO

-- Test 12: Báo cáo - Tiến độ dự án
PRINT '';
PRINT '--- TEST 12: Báo cáo - Tiến độ dự án ---';
EXEC dbo.sp_Report_ProjectProgress;
GO

-- Test 13: Báo cáo - Task quá hạn
PRINT '';
PRINT '--- TEST 13: Báo cáo - Task quá hạn ---';
EXEC dbo.sp_Report_OverdueTasks;
GO

PRINT '';
PRINT '========================================';
PRINT '  HOÀN THÀNH TẤT CẢ CÁC TEST!';
PRINT '========================================';
PRINT '';
PRINT 'GHI CHÚ:';
PRINT '- Tất cả procedures sử dụng TRY-CATCH để xử lý lỗi';
PRINT '- Transactions được sử dụng cho INSERT/UPDATE/DELETE';
PRINT '- Mã trả về: 0 = Thành công, -1 = Lỗi';
PRINT '- Tất cả validations về ngày tháng đã được thực hiện';
PRINT '- Các validations về Foreign key đã được triển khai';
PRINT '- Xóa mềm được sử dụng cho Employees';
PRINT '- Xóa CASCADE được sử dụng cho Projects/Tasks';
PRINT '========================================';
GO
