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
 - 1 User CÓ ĐÚNG 1 Employee (Shared Primary Key: EmployeeID = UserID)
*/
CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Phone VARCHAR(20),
    Email NVARCHAR(100),
    PasswordHash NVARCHAR(256) NOT NULL,
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
   - 1 Employee CÓ ĐÚNG 1 User (Shared PK: EmployeeID = UserID)
   - Nhiều Employees thuộc 1 Department (FK: DepartmentID)
   - 1 Employee có thể được gán nhiều Tasks (Tasks.AssignedTo)
   - 1 Employee có thể tạo nhiều Projects (Projects.CreatedBy)
   - 1 Employee có thể tạo nhiều Tasks (Tasks.CreatedBy)
   - 1 Employee có thể comment nhiều TaskComments (TaskComments.EmployeeID)
 
 LƯU Ý: EmployeeID KHÔNG DÙNG IDENTITY vì phải khớp với UserID
        Workflow: Tạo User trước → Tạo Employee với EmployeeID = UserID vừa tạo
*/
CREATE TABLE dbo.Employees (
    EmployeeID INT PRIMARY KEY, -- KHÔNG DÙNG IDENTITY, khớp với Users.UserID
    FullName NVARCHAR(100) NOT NULL,
    Position NVARCHAR(100),
    DepartmentID INT NULL,
    Address NVARCHAR(500),
    HireDate DATE NOT NULL DEFAULT (GETDATE()),
    IsActive BIT NOT NULL DEFAULT (1),
    
    -- Foreign Key: Đảm bảo EmployeeID phải tồn tại trong Users.UserID
    CONSTRAINT FK_Employees_Users FOREIGN KEY (EmployeeID) 
        REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
    
    -- Foreign Key: Department (nullable)
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentID) 
        REFERENCES dbo.Departments(DepartmentID) ON DELETE SET NULL
);
PRINT 'Table [Employees] created with Shared PK (EmployeeID = UserID).';
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
   - 1 Project được tạo bởi 1 Employee (FK: CreatedBy)
   - 1 Project có nhiều Tasks (Tasks.ProjectID)
*/
CREATE TABLE dbo.Projects (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Planning','InProgress','Completed','Cancelled')),
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    
    CONSTRAINT CHK_Project_Dates CHECK (EndDate IS NULL OR EndDate >= StartDate),
    CONSTRAINT FK_Projects_Employees FOREIGN KEY (CreatedBy) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [Projects] created.';
GO

/*
 Table: Tasks
 Mô tả: Quản lý công việc trong dự án
 Relationships:
   - Nhiều Tasks thuộc 1 Project (FK: ProjectID) - CASCADE DELETE
   - 1 Task được gán cho 1 Employee (FK: AssignedTo)
   - 1 Task được tạo bởi 1 Employee (FK: CreatedBy)
   - 1 Task có nhiều TaskComments (TaskComments.TaskID)
   - 1 Task có nhiều Subtasks (Subtasks.TaskID)
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
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    UpdatedDate DATETIME NULL,

    CONSTRAINT FK_Tasks_Projects FOREIGN KEY (ProjectID) 
    REFERENCES dbo.Projects(ProjectID) ON DELETE CASCADE,
    CONSTRAINT FK_Tasks_Employees_Assigned FOREIGN KEY (AssignedTo) 
    REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL,
    CONSTRAINT FK_Tasks_Employees_Created FOREIGN KEY (CreatedBy) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [Tasks] created.';
GO

/*
 Table: Subtasks
 Mô tả: Quản lý công việc con (subtask) của Tasks
 Relationships:
   - Nhiều Subtasks thuộc 1 Task (FK: TaskID) - CASCADE DELETE
   - 1 Subtask có thể được gán cho 1 Employee (FK: AssignedTo)
*/
CREATE TABLE dbo.Subtasks (
    SubtaskID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
    SubtaskTitle NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Todo','InProgress','Done')) DEFAULT ('Todo'),
    Progress INT NOT NULL DEFAULT (0) CHECK (Progress BETWEEN 0 AND 100),
    AssignedTo INT NULL,
    Deadline DATE NULL,
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    UpdatedDate DATETIME NULL,
    
  CONSTRAINT FK_Subtasks_Tasks FOREIGN KEY (TaskID) 
        REFERENCES dbo.Tasks(TaskID) ON DELETE CASCADE,
  CONSTRAINT FK_Subtasks_Employees FOREIGN KEY (AssignedTo) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL
);
PRINT 'Table [Subtasks] created.';
GO

/*
 Table: TaskComments
 Mô tả: Lưu comments/ghi chú cho Tasks
 Relationships:
   - Nhiều Comments thuộc 1 Task (FK: TaskID) - CASCADE DELETE
   - 1 Comment được tạo bởi 1 Employee (FK: EmployeeID)
*/
CREATE TABLE dbo.TaskComments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
    EmployeeID INT NOT NULL,
    Comment NVARCHAR(MAX) NOT NULL,
    CommentDate DATETIME NOT NULL DEFAULT (GETDATE()),
    
    CONSTRAINT FK_TaskComments_Tasks FOREIGN KEY (TaskID) 
    REFERENCES dbo.Tasks(TaskID) ON DELETE CASCADE,
    CONSTRAINT FK_TaskComments_Employees FOREIGN KEY (EmployeeID) 
    REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [TaskComments] created.';
GO

/*
 Table: Files
 Mô tả: Quản lý files đính kèm (CỰC KỲ ĐƠN GIẢN)
 Storage: Tất cả files lưu trong /Uploads/ với tên timestamp
 Naming Convention: {timestamp}_{originalFileName}
 Example: 20250601123045_document.pdf
*/
CREATE TABLE dbo.Files (
    FileID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL, -- Tiêu đề/mô tả file (do user nhập)
    FileName NVARCHAR(255) NOT NULL, -- Tên file gốc: document.pdf
    CreatedBy INT NOT NULL, -- Employee đã upload
    CreatedAt DATETIME NOT NULL DEFAULT (GETDATE()),
    
    CONSTRAINT FK_Files_CreatedBy FOREIGN KEY (CreatedBy) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [Files] created (simplified version).';
GO

PRINT '==> All 8 tables created successfully!';
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
CREATE NONCLUSTERED INDEX IX_Subtasks_TaskID ON dbo.Subtasks(TaskID);
CREATE NONCLUSTERED INDEX IX_Subtasks_AssignedTo ON dbo.Subtasks(AssignedTo);
CREATE NONCLUSTERED INDEX IX_TaskComments_TaskID ON dbo.TaskComments(TaskID);
CREATE NONCLUSTERED INDEX IX_TaskComments_EmployeeID ON dbo.TaskComments(EmployeeID);
CREATE NONCLUSTERED INDEX IX_Files_CreatedBy ON dbo.Files(CreatedBy);

-- Phone và Email Indexes (để tối ưu tìm kiếm)
CREATE NONCLUSTERED INDEX IX_Users_Phone ON dbo.Users(Phone) WHERE Phone IS NOT NULL;
CREATE NONCLUSTERED INDEX IX_Users_Email ON dbo.Users(Email) WHERE Email IS NOT NULL;

PRINT '==> All 13 indexes created successfully!';
GO

-- =============================================
-- SAMPLE DATA INSERTION (WITH SHARED PRIMARY KEY)
-- =============================================

PRINT 'Inserting sample data...';
GO

-- 1. Users (6 records) - TẠO TRƯỚC
-- Password format: CONVERT to hex string for NVARCHAR storage
INSERT INTO dbo.Users (Phone, Email, PasswordHash, Role)
VALUES
('0900000001', 'admin@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Admin@123!'), 2), 'Admin'),
('0901234567', 'nguyen.van.an@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee@123'), 2), 'Employee'),
('0912345678', 'tran.thi.binh@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Manager@123'), 2), 'Manager'),
('0923456789', 'le.minh.cuong@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Manager@123'), 2), 'Manager'),
('0934567890', 'pham.thu.dung@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee@123'), 2), 'Employee'),
('0945678901', 'hoang.quoc.huy@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee@123'), 2), 'Employee');
PRINT '==> 6 Users inserted.';
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

-- 3. Employees (5 records) - DÙNG UserID LÀM EmployeeID
INSERT INTO dbo.Employees (EmployeeID, FullName, Position, DepartmentID, Address, HireDate)
VALUES
(2, N'Nguyễn Văn An', N'Lập trình viên', 1, N'123 Láng Hạ, Hà Nội', '2020-03-15'),
(3, N'Trần Thị Bình', N'Trưởng phòng Nhân sự', 2, N'456 Giải Phóng, Hà Nội', '2019-07-20'),
(4, N'Lê Minh Cường', N'Trưởng phòng IT', 1, N'789 Trường Chinh, Hà Nội', '2018-11-10'),
(5, N'Phạm Thu Dung', N'Kế toán trưởng', 3, N'101 Cầu Giấy, Hà Nội', '2021-02-01'),
(6, N'Hoàng Quốc Huy', N'Trưởng phòng Marketing', 4, N'202 Nguyễn Trãi, Hà Nội', '2019-09-25');
PRINT '==> 5 Employees inserted (EmployeeID = UserID).';
GO

-- 4. Update ManagerID cho Departments
UPDATE dbo.Departments SET ManagerID = 4 WHERE DepartmentID = 1;
UPDATE dbo.Departments SET ManagerID = 3 WHERE DepartmentID = 2;
UPDATE dbo.Departments SET ManagerID = 5 WHERE DepartmentID = 3;
UPDATE dbo.Departments SET ManagerID = 6 WHERE DepartmentID = 4;
PRINT '==> Department managers assigned.';
GO

-- 5. Projects (5 records) - KHÔNG CÓ Budget
INSERT INTO dbo.Projects (ProjectName, Description, StartDate, EndDate, Status, CreatedBy)
VALUES
(N'Hệ thống quản lý nhân sự', N'Xây dựng hệ thống quản lý nhân sự tập trung', '2024-01-15', '2024-07-31', 'InProgress', 3),
(N'Website thương mại điện tử', N'Phát triển nền tảng bán hàng online', '2024-02-01', NULL, 'Planning', 3),
(N'Ứng dụng mobile CRM', N'App quản lý quan hệ khách hàng trên di động', '2023-09-01', '2024-03-31', 'Completed', 4),
(N'Tối ưu hệ thống báo cáo', N'Nâng cấp hệ thống báo cáo tài chính', '2024-04-01', '2024-12-31', 'Planning', 4),
(N'Nâng cấp hạ tầng mạng', N'Modernize network infrastructure', '2024-01-10', '2024-06-30', 'InProgress', 3);
PRINT '==> 5 Projects inserted.';
GO

-- 6. Tasks (5 records) - KHÔNG CÓ Progress
INSERT INTO dbo.Tasks (ProjectID, TaskTitle, Description, AssignedTo, CreatedBy, Deadline, Status, Priority)
VALUES
(1, N'Thiết kế database schema', N'Phân tích và thiết kế cấu trúc CSDL', 4, 3, '2024-02-15', 'Done', 'High'),
(1, N'Phát triển API Backend', N'Xây dựng REST API với .NET', 2, 3, '2024-04-30', 'InProgress', 'High'),
(2, N'Nghiên cứu công nghệ', N'Đánh giá các framework frontend phù hợp', NULL, 3, '2024-03-15', 'Todo', 'Medium'),
(3, N'Kiểm thử chức năng', N'Test toàn bộ tính năng trước khi release', 5, 4, '2024-03-20', 'Done', 'Critical'),
(5, N'Cấu hình firewall', N'Setup firewall rules cho hệ thống mới', 6, 3, '2024-05-31', 'InProgress', 'High');
PRINT '==> 5 Tasks inserted.';
GO

-- 7. Subtasks (7 records)
INSERT INTO dbo.Subtasks (TaskID, SubtaskTitle, Description, Status, Progress, AssignedTo, Deadline)
VALUES
(1, N'Thiết kế bảng Users', N'Tạo ERD cho bảng Users và relationships', 'Done', 100, 4, '2024-02-05'),
(1, N'Thiết kế bảng Employees', N'Tạo ERD cho bảng Employees', 'Done', 100, 4, '2024-02-08'),
(1, N'Review schema với team', N'Họp review thiết kế database', 'Done', 100, 4, '2024-02-12'),
(2, N'Setup project structure', N'Tạo project ASP.NET Core', 'Done', 100, 2, '2024-03-20'),
(2, N'Implement User API', N'Tạo CRUD API cho Users', 'InProgress', 70, 2, '2024-04-15'),
(2, N'Implement Employee API', N'Tạo CRUD API cho Employees', 'InProgress', 50, 2, '2024-04-25'),
(5, N'Cấu hình firewall rules', N'Setup inbound/outbound rules', 'InProgress', 40, 6, '2024-05-25');
PRINT '==> 7 Subtasks inserted.';
GO

-- 8. TaskComments (5 records)
INSERT INTO dbo.TaskComments (TaskID, EmployeeID, Comment)
VALUES
(1, 3, N'Schema đã được review và approve bởi team lead.'),
(2, 5, N'Cần optimize performance cho API lấy danh sách users.'),
(2, 3, N'Đã áp dụng caching, response time giảm từ 800ms xuống 120ms.'),
(4, 4, N'Tất cả 45 test cases đã PASSED. Ready for production.'),
(5, 3, N'Cần phối hợp với team Network để test connection.');
PRINT '==> 5 TaskComments inserted.';
GO

-- 9. Files (5 records) - ĐƠN GIẢN, chỉ có Title, FileName, CreatedBy, CreatedAt
-- Files sẽ được lưu trong /Uploads/ với tên: {timestamp}_{fileName}
INSERT INTO dbo.Files (Title, FileName, CreatedBy, CreatedAt)
VALUES
(N'Tài liệu yêu cầu dự án HR', 'requirements.pdf', 3, '2024-01-15 10:30:00'),
(N'Mockup thiết kế giao diện', 'design_mockup.png', 4, '2024-01-20 14:00:00'),
(N'Sơ đồ ERD database', 'database_diagram.png', 4, '2024-02-05 15:00:00'),
(N'Kết quả test API', 'api_test_results.xlsx', 2, '2024-04-10 16:00:00'),
(N'Cấu hình firewall chi tiết', 'firewall_config.docx', 6, '2024-05-20 17:00:00');
PRINT '==> 5 Files inserted (simplified).';
GO

PRINT '==> Sample data inserted successfully!';
GO

-- =============================================
-- COMPLETION SUMMARY
-- =============================================

PRINT '';
PRINT '========================================';
PRINT '  DATABASE SETUP COMPLETED!';
PRINT '========================================';
PRINT 'Database Name: ProjectManagementDB';
PRINT 'Tables Created: 8';
PRINT '  - Users';
PRINT '  - Departments';
PRINT '  - Employees';
PRINT '  - Projects';
PRINT '  - Tasks';
PRINT '  - Subtasks';
PRINT '  - TaskComments';
PRINT '  - Files (SIMPLIFIED)';
PRINT '';
PRINT 'Indexes Created: 13';
PRINT 'Sample Records:';
PRINT '  - Users: 6';
PRINT '  - Employees: 5';
PRINT '  - Departments: 5';
PRINT '  - Projects: 5';
PRINT '  - Tasks: 5';
PRINT '  - Subtasks: 7';
PRINT '  - Comments: 5';
PRINT '  - Files: 5 (SIMPLIFIED - no polymorphic)';
PRINT '';
PRINT '========================================';
PRINT 'THAY ĐỔI CẤU TRÚC:';
PRINT '- Employees: BỎ ImagePath';
PRINT '- Projects: BỎ Budget';
PRINT '- Tasks: BỎ Progress (chỉ dùng Status)';
PRINT '- Subtasks: THÊM MỚI (1-to-Many với Tasks)';
PRINT '- Files: THÊM MỚI (CỰC KỲ ĐƠN GIẢN)';
PRINT '';
PRINT 'FILES TABLE - CỰC KỲ ĐƠN GIẢN:';
PRINT '- Chỉ 5 cột: FileID, Title, FileName, CreatedBy, CreatedAt';
PRINT '- Không có polymorphic relationship';
PRINT '- Tất cả files lưu trong /Uploads/';
PRINT '- Tên file: {timestamp}_{originalFileName}';
PRINT '- VD: 20250601123045_document.pdf';
PRINT '';
PRINT 'Default Login Credentials:';
PRINT '  Phone: 0900000001 | Email: admin@company.com - Password: Admin@123! (Admin)';
PRINT '  Phone: 0901234567 | Email: nguyen.van.an@company.com - Password: Employee@123';
PRINT '  Phone: 0912345678 | Email: tran.thi.binh@company.com - Password: Manager@123';
PRINT '========================================';
GO