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
    ManagerID INT NULL
);
PRINT 'Table [Departments] created.';
GO

/*
 Table: Employees
 Mô tả: Quản lý nhân viên
 Relationships:
   - 1 Employee CÓ ĐÚNG 1 User (Shared PK: EmployeeID = UserID)
   - Nhiều Employees thuộc 1 Department (FK: DepartmentID)
   - 1 Employee có nhiều EmployeeFiles (EmployeeFiles.EmployeeID)
*/
CREATE TABLE dbo.Employees (
    EmployeeID INT PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Position NVARCHAR(100),
    DepartmentID INT NULL,
    AvatarPath NVARCHAR(500),
    Address NVARCHAR(500),
    HireDate DATE NOT NULL DEFAULT (GETDATE()),
    IsActive BIT NOT NULL DEFAULT (1),
  
    CONSTRAINT FK_Employees_Users FOREIGN KEY (EmployeeID) 
  REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentID) 
        REFERENCES dbo.Departments(DepartmentID) ON DELETE SET NULL
);
PRINT 'Table [Employees] created with AvatarPath.';
GO

ALTER TABLE dbo.Employees
    ADD GENDER NVARCHAR(20) NOT NULL DEFAULT ('NotSpecified')


ALTER TABLE dbo.Departments
    ADD CONSTRAINT FK_Departments_Manager FOREIGN KEY (ManagerID) 
   REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL;
PRINT 'FK [FK_Departments_Manager] added.';
GO

/*
 Table: Projects
 Mô tả: Quản lý dự án
 Relationships:
 - 1 Project có nhiều Tasks (Tasks.ProjectID)
   - 1 Project có nhiều ProjectFiles (ProjectFiles.ProjectID)
*/
CREATE TABLE dbo.Projects (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN (N'Lập kế hoạch',N'Đang thực hiện',N'Hoàn thành',N'Đã hủy')),
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
   - 1 Task có nhiều Subtasks (Subtasks.TaskID)
   - 1 Task có nhiều TaskFiles (TaskFiles.TaskID)
*/
CREATE TABLE dbo.Tasks (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    TaskTitle NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    AssignedTo INT NULL,
    CreatedBy INT NOT NULL,
    Deadline DATE NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN (N'Cần làm',N'Đang thực hiện',N'Chờ duyệt',N'Hoàn thành')),
    Priority NVARCHAR(20) NOT NULL CHECK (Priority IN (N'Thấp',N'Trung bình',N'Cao',N'Ưu tiên cao')),
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
*/
CREATE TABLE dbo.Subtasks (
    SubtaskID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
    SubtaskTitle NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(20) NOT NULL CHECK (Status IN (N'Cần làm',N'Đang thực hiện',N'Hoàn thành')) DEFAULT (N'Cần làm'),
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
 Table: EmployeeFiles
 Mô tả: Files của nhân viên (CV, documents, certificates)
 Storage: /Uploads/{timestamp}_{fileName}
 Relationships:
   - Nhiều EmployeeFiles thuộc 1 Employee (FK: EmployeeID) - CASCADE DELETE
 Note: Avatar được lưu trực tiếp trong Employees.AvatarPath
*/
CREATE TABLE dbo.EmployeeFiles (
    EmployeeFileID INT IDENTITY(1,1) PRIMARY KEY,
  EmployeeID INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
CreatedBy INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT (GETDATE()),
    
    CONSTRAINT FK_EmployeeFiles_Employees FOREIGN KEY (EmployeeID) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE CASCADE,
    CONSTRAINT FK_EmployeeFiles_CreatedBy FOREIGN KEY (CreatedBy) 
 REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [EmployeeFiles] created.';
GO

/*
 Table: ProjectFiles
 Mô tả: Files đính kèm dự án (Requirements, Design, Reports)
 Storage: /Uploads/{timestamp}_{fileName}
 Relationships:
   - Nhiều ProjectFiles thuộc 1 Project (FK: ProjectID) - CASCADE DELETE
*/
CREATE TABLE dbo.ProjectFiles (
    ProjectFileID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
Title NVARCHAR(200) NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    CreatedBy INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT (GETDATE()),
    
    CONSTRAINT FK_ProjectFiles_Projects FOREIGN KEY (ProjectID) 
      REFERENCES dbo.Projects(ProjectID) ON DELETE CASCADE,
    CONSTRAINT FK_ProjectFiles_CreatedBy FOREIGN KEY (CreatedBy) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [ProjectFiles] created.';
GO

/*
 Table: TaskFiles
 Mô tả: Files đính kèm công việc (Screenshots, Test Results)
 Storage: /Uploads/{timestamp}_{fileName}
 Relationships:
   - Nhiều TaskFiles thuộc 1 Task (FK: TaskID) - CASCADE DELETE
*/
CREATE TABLE dbo.TaskFiles (
  TaskFileID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
  Title NVARCHAR(200) NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    CreatedBy INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT (GETDATE()),
    
    CONSTRAINT FK_TaskFiles_Tasks FOREIGN KEY (TaskID) 
     REFERENCES dbo.Tasks(TaskID) ON DELETE CASCADE,
    CONSTRAINT FK_TaskFiles_CreatedBy FOREIGN KEY (CreatedBy) 
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [TaskFiles] created.';
GO

PRINT '==> All 10 tables created successfully!';
GO

-- Bước 1: Tạo bảng trung gian TaskAssignments (nếu chưa tồn tại)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaskAssignments]') AND type in (N'U'))
BEGIN
    /*
    Table: TaskAssignments
    Mô tả: Quản lý việc giao task cho nhiều nhân viên
    Relationships:
      - Nhiều TaskAssignments thuộc 1 Task (FK: TaskID) - CASCADE DELETE
      - Nhiều TaskAssignments thuộc 1 Employee (FK: EmployeeID) - CASCADE DELETE
    */
    CREATE TABLE dbo.TaskAssignments (
        TaskAssignmentID INT IDENTITY(1,1) PRIMARY KEY,
        TaskID INT NOT NULL,
        EmployeeID INT NOT NULL,
        AssignedBy INT NULL,  -- Optional: Ai giao task (FK đến Employees)
        AssignedDate DATETIME NOT NULL DEFAULT (GETDATE()),
        
        CONSTRAINT FK_TaskAssignments_Tasks FOREIGN KEY (TaskID)
            REFERENCES dbo.Tasks(TaskID) ON DELETE CASCADE,
        CONSTRAINT FK_TaskAssignments_Employees FOREIGN KEY (EmployeeID)
            REFERENCES dbo.Employees(EmployeeID) ON DELETE CASCADE,
        CONSTRAINT FK_TaskAssignments_AssignedBy FOREIGN KEY (AssignedBy)
            REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION,  -- Đổi thành NO ACTION để tránh multiple cascade paths
        
        -- Tránh duplicate: Một employee không được assign task 2 lần
        CONSTRAINT UQ_TaskAssignments_TaskEmployee UNIQUE (TaskID, EmployeeID)
    );
    PRINT 'Table [TaskAssignments] created.';
END
GO

-- Bước 2: Migrate dữ liệu từ Tasks.AssignedTo sang TaskAssignments
-- (Chỉ chạy nếu cột AssignedTo còn tồn tại và có data)
IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'AssignedTo' AND Object_ID = Object_ID(N'dbo.Tasks'))
BEGIN
    INSERT INTO dbo.TaskAssignments (TaskID, EmployeeID, AssignedBy, AssignedDate)
    SELECT t.TaskID, t.AssignedTo, t.CreatedBy, t.CreatedDate
    FROM dbo.Tasks t
    WHERE t.AssignedTo IS NOT NULL;
    PRINT 'Data migrated from [Tasks.AssignedTo] to [TaskAssignments].';
END
GO

-- Bước 3: Xóa constraint FK_Tasks_Employees_Assigned (nếu tồn tại)
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tasks_Employees_Assigned]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
BEGIN
    ALTER TABLE dbo.Tasks
        DROP CONSTRAINT FK_Tasks_Employees_Assigned;
    PRINT 'Constraint [FK_Tasks_Employees_Assigned] dropped.';
END
GO

-- Bước 4: Xóa index IX_Tasks_AssignedTo (nếu tồn tại)
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND name = N'IX_Tasks_AssignedTo')
BEGIN
    DROP INDEX IX_Tasks_AssignedTo ON dbo.Tasks;
    PRINT 'Index [IX_Tasks_AssignedTo] dropped from [Tasks].';
END
GO

-- Bước 5: Xóa cột AssignedTo khỏi Tasks (nếu tồn tại)
IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'AssignedTo' AND Object_ID = Object_ID(N'dbo.Tasks'))
BEGIN
    ALTER TABLE dbo.Tasks
        DROP COLUMN AssignedTo;
    PRINT 'Column [AssignedTo] dropped from [Tasks].';
END
GO

-- Giả sử bạn đã chạy script tạo bảng gốc thành công. Nếu chưa, chạy Phần 1 từ trước.

-- 1. Thêm bảng UserRoles (nếu chưa tồn tại)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UserRoles' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.UserRoles (
        UserRoleID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT NOT NULL,
        Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', N'Quản lý phòng ban', N'Nhân viên', N'Quản lý dự án')),
        AssignedDate DATETIME NOT NULL DEFAULT (GETDATE()),
        
        CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserID)
            REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
        CONSTRAINT UQ_UserRoles_UserRole UNIQUE (UserID, Role)  -- Ngăn duplicate roles
    );
    PRINT 'Table [UserRoles] created.';
END
GO

-- 2. Migrate dữ liệu cũ từ Users.Role sang UserRoles (chỉ insert nếu role hợp lệ)
-- Xóa data cũ nếu có conflict, hoặc điều chỉnh role thủ công nếu cần
DELETE FROM dbo.UserRoles;  -- Xóa nếu có data cũ gây conflict
INSERT INTO dbo.UserRoles (UserID, Role)
SELECT UserID, Role
FROM dbo.Users
WHERE Role IN ('Admin', N'Quản lý phòng ban', N'Nhân viên', N'Quản lý dự án');  -- Chỉ insert role hợp lệ
PRINT 'Data migrated from Users.Role to UserRoles (only valid roles).';
GO

-- 3. Xóa CHECK constraint trên Users.Role (sử dụng tên từ lỗi của bạn)
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK__Users__Role__37A5467C')
BEGIN
    ALTER TABLE dbo.Users
        DROP CONSTRAINT CK__Users__Role__37A5467C;
    PRINT 'CHECK constraint on [Users.Role] dropped.';
END
GO

-- 4. Xóa trường Role khỏi Users (bây giờ có thể drop vì constraint đã xóa)
ALTER TABLE dbo.Users
    DROP COLUMN Role;
PRINT 'Column [Role] dropped from [Users].';
GO

-- 5. Thêm ProjectManagerID vào Projects (nếu chưa có)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'ProjectManagerID' AND object_id = OBJECT_ID('dbo.Projects'))
BEGIN
    ALTER TABLE dbo.Projects
        ADD ProjectManagerID INT NULL;
    GO

    ALTER TABLE dbo.Projects
        ADD CONSTRAINT FK_Projects_Manager FOREIGN KEY (ProjectManagerID)
            REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL;
    PRINT 'Column [ProjectManagerID] and FK added to [Projects].';
END
GO

-- 6. Thêm trigger để enforce gán manager khi tạo (nếu chưa có)
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_Departments_Insert')
BEGIN
    CREATE TRIGGER TR_Departments_Insert
    ON dbo.Departments
    AFTER INSERT
    AS
    BEGIN
        IF EXISTS (SELECT 1 FROM inserted WHERE ManagerID IS NULL)
        BEGIN
            RAISERROR ('ManagerID must be assigned when creating a department.', 16, 1);
            ROLLBACK TRANSACTION;
        END
    END;
    PRINT 'Trigger [TR_Departments_Insert] created.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_Projects_Insert')
BEGIN
    CREATE TRIGGER TR_Projects_Insert
    ON dbo.Projects
    AFTER INSERT
    AS
    BEGIN
        IF EXISTS (SELECT 1 FROM inserted WHERE ProjectManagerID IS NULL)
        BEGIN
            RAISERROR ('ProjectManagerID must be assigned when creating a project.', 16, 1);
            ROLLBACK TRANSACTION;
        END
    END;
    PRINT 'Trigger [TR_Projects_Insert] created.';
END
GO

PRINT '==> Database schema updated successfully for multiple roles and project manager!';
GO
-- =============================================
-- INDEXES CREATION
-- =============================================

PRINT 'Creating indexes...';

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
CREATE NONCLUSTERED INDEX IX_EmployeeFiles_EmployeeID ON dbo.EmployeeFiles(EmployeeID);
CREATE NONCLUSTERED INDEX IX_EmployeeFiles_CreatedBy ON dbo.EmployeeFiles(CreatedBy);
CREATE NONCLUSTERED INDEX IX_ProjectFiles_ProjectID ON dbo.ProjectFiles(ProjectID);
CREATE NONCLUSTERED INDEX IX_ProjectFiles_CreatedBy ON dbo.ProjectFiles(CreatedBy);
CREATE NONCLUSTERED INDEX IX_TaskFiles_TaskID ON dbo.TaskFiles(TaskID);
CREATE NONCLUSTERED INDEX IX_TaskFiles_CreatedBy ON dbo.TaskFiles(CreatedBy);
CREATE NONCLUSTERED INDEX IX_Users_Phone ON dbo.Users(Phone) WHERE Phone IS NOT NULL;
CREATE NONCLUSTERED INDEX IX_Users_Email ON dbo.Users(Email) WHERE Email IS NOT NULL;

PRINT '==> All 18 indexes created successfully!';
GO

-- =============================================
-- SAMPLE DATA
-- =============================================

PRINT 'Inserting sample data...';
GO

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

INSERT INTO dbo.Departments (DepartmentName, Description)
VALUES
(N'Công nghệ thông tin', N'Phòng phát triển phần mềm và hạ tầng IT'),
(N'Nhân sự', N'Phòng quản lý nguồn nhân lực'),
(N'Tài chính', N'Phòng kế toán và tài chính'),
(N'Marketing', N'Phòng marketing và truyền thông'),
(N'Vận hành', N'Phòng vận hành và logistics');
PRINT '==> 5 Departments inserted.';
GO

INSERT INTO dbo.Employees (EmployeeID, FullName, Position, DepartmentID, AvatarPath, Address, HireDate)
VALUES
(2, N'Nguyễn Văn An', N'Lập trình viên', 1, '/Uploads/Avatars/20240101120000_avatar_nva.jpg', N'123 Láng Hạ, Hà Nội', '2020-03-15'),
(3, N'Trần Thị Bình', N'Trưởng phòng Nhân sự', 2, '/Uploads/Avatars/20240101120100_avatar_ttb.png', N'456 Giải Phóng, Hà Nội', '2019-07-20'),
(4, N'Lê Minh Cường', N'Trưởng phòng IT', 1, '/Uploads/Avatars/20240101120200_avatar_lmc.jpg', N'789 Trường Chinh, Hà Nội', '2018-11-10'),
(5, N'Phạm Thu Dung', N'Kế toán trưởng', 3, NULL, N'101 Cầu Giấy, Hà Nội', '2021-02-01'),
(6, N'Hoàng Quốc Huy', N'Trưởng phòng Marketing', 4, NULL, N'202 Nguyễn Trãi, Hà Nội', '2019-09-25');
PRINT '==> 5 Employees inserted with AvatarPath.';
GO

UPDATE dbo.Departments SET ManagerID = 4 WHERE DepartmentID = 1;
UPDATE dbo.Departments SET ManagerID = 3 WHERE DepartmentID = 2;
UPDATE dbo.Departments SET ManagerID = 5 WHERE DepartmentID = 3;
UPDATE dbo.Departments SET ManagerID = 6 WHERE DepartmentID = 4;
PRINT '==> Managers assigned.';
GO

INSERT INTO dbo.Projects (ProjectName, Description, StartDate, EndDate, Status, CreatedBy)
VALUES
(N'Hệ thống quản lý nhân sự', N'Xây dựng hệ thống quản lý nhân sự tập trung', '2024-01-15', '2024-07-31', N'Đang thực hiện', 3),
(N'Website thương mại điện tử', N'Phát triển nền tảng bán hàng online', '2024-02-01', NULL, N'Lập kế hoạch', 3),
(N'Ứng dụng mobile CRM', N'App quản lý quan hệ khách hàng trên di động', '2023-09-01', '2024-03-31', N'Hoàn thành', 4),
(N'Tối ưu hệ thống báo cáo', N'Nâng cấp hệ thống báo cáo tài chính', '2024-04-01', '2024-12-31', N'Lập kế hoạch', 4),
(N'Nâng cấp hạ tầng mạng', N'Modernize network infrastructure', '2024-01-10', '2024-06-30', N'Đang thực hiện', 3);
PRINT '==> 5 Projects inserted.';
GO

INSERT INTO dbo.Tasks (ProjectID, TaskTitle, Description, AssignedTo, CreatedBy, Deadline, Status, Priority)
VALUES
(1, N'Thiết kế database schema', N'Phân tích và thiết kế cấu trúc CSDL', 4, 3, '2024-02-15', N'Hoàn thành', N'Cao'),
(1, N'Phát triển API Backend', N'Xây dựng REST API với .NET', 2, 3, '2024-04-30', N'Lập kế hoạch', N'Cao'),
(2, N'Nghiên cứu công nghệ', N'Đánh giá các framework frontend phù hợp', NULL, 3, '2024-03-15', N'Cần làm', N'Trung bình'),
(3, N'Kiểm thử chức năng', N'Test toàn bộ tính năng trước khi release', 5, 4, '2024-03-20', N'Hoàn thành', N'Ưu tiên cao'),
(5, N'Cấu hình firewall', N'Setup firewall rules cho hệ thống mới', 6, 3, '2024-05-31', N'Đang thực hiện', N'Cao');
PRINT '==> 5 Tasks inserted.';
GO

INSERT INTO dbo.Subtasks (TaskID, SubtaskTitle, Description, Status, Progress, AssignedTo, Deadline)
VALUES
(1, N'Thiết kế bảng Users', N'Tạo ERD cho bảng Users', N'Hoàn thành', 100, 4, '2024-02-05'),
(1, N'Thiết kế bảng Employees', N'Tạo ERD cho bảng Employees', N'Hoàn thành', 100, 4, '2024-02-08'),
(2, N'Setup project structure', N'Tạo project ASP.NET Core', N'Hoàn thành', 100, 2, '2024-03-20'),
(2, N'Implement User API', N'Tạo CRUD API cho Users', N'Đang thực hiện', 70, 2, '2024-04-15');
PRINT '==> 4 Subtasks inserted.';
GO

INSERT INTO dbo.TaskComments (TaskID, EmployeeID, Comment)
VALUES
(1, 3, N'Schema đã được review và approve.'),
(2, 5, N'Cần optimize performance cho API.'),
(4, 4, N'Tất cả test cases đã PASSED.');
PRINT '==> 3 Comments inserted.';
GO

-- EmployeeFiles (CV, certificates - không bao gồm avatar)
INSERT INTO dbo.EmployeeFiles (EmployeeID, Title, FileName, CreatedBy, CreatedAt)
VALUES
(4, N'CV Lê Minh Cường', '20240115100000_cv_lmc.pdf', 4, '2024-01-15 10:00:00'),
(4, N'Chứng chỉ AWS', '20240120150000_aws_cert.pdf', 4, '2024-01-20 15:00:00');
PRINT '==> 2 EmployeeFiles inserted.';
GO

-- ProjectFiles (2 records)
INSERT INTO dbo.ProjectFiles (ProjectID, Title, FileName, CreatedBy, CreatedAt)
VALUES
(1, N'Tài liệu yêu cầu dự án', '20240115100000_requirements.pdf', 3, '2024-01-15 10:00:00'),
(1, N'Mockup thiết kế giao diện', '20240120140000_design_mockup.png', 4, '2024-01-20 14:00:00');
PRINT '==> 2 ProjectFiles inserted.';
GO

-- TaskFiles (2 records)
INSERT INTO dbo.TaskFiles (TaskID, Title, FileName, CreatedBy, CreatedAt)
VALUES
(1, N'Sơ đồ ERD database', '20240205150000_database_diagram.png', 4, '2024-02-05 15:00:00'),
(2, N'Kết quả test API', '20240410160000_api_test_results.xlsx', 2, '2024-04-10 16:00:00');
PRINT '==> 2 TaskFiles inserted.';
GO

PRINT '==> Sample data inserted!';
GO

-- =============================================
-- SUMMARY
-- =============================================

PRINT '';
PRINT '========================================';
PRINT '  DATABASE SETUP COMPLETED!';
PRINT '========================================';
PRINT 'Tables: 10';
PRINT '  - Users, Departments, Employees (with AvatarPath)';
PRINT '  - Projects, Tasks, Subtasks, TaskComments';
PRINT '  - EmployeeFiles (CV, certificates)';
PRINT '  - ProjectFiles (Requirements, designs)';
PRINT '  - TaskFiles (Screenshots, test results)';
PRINT '';
PRINT 'AVATAR HANDLING:';
PRINT '  - Avatar stored in Employees.AvatarPath';
PRINT '  - Path format: /Uploads/Avatars/{timestamp}_{fileName}';
PRINT '  - Other employee docs in EmployeeFiles table';
PRINT '';
PRINT 'FOREIGN KEYS:';
PRINT '  - All file tables have FK to parent table';
PRINT '  - CASCADE DELETE enabled';
PRINT '';
PRINT 'Login: 0900000001 / Admin@123!';
PRINT '========================================';
GO

