-- =============================================
-- Script: Setup ProjectManagementDB
-- Mô tả: Tạo database, tables, indexes và sample data
-- Lưu ý: Script sẽ XÓA database cũ nếu tồn tại
-- =============================================

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

