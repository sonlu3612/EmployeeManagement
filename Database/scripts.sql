﻿-- =============================================
-- Script: Setup ProjectManagementDB (Sửa lỗi vòng FK & trigger)
-- Mô tả: Tạo database, tables, indexes và sample data
-- Lưu ý: Script sẽ XÓA database cũ nếu tồn tại
-- =============================================

USE master;
GO

IF DB_ID(N'ProjectManagementDB') IS NOT NULL
BEGIN
    PRINT 'Closing all connections to ProjectManagementDB...';
    ALTER DATABASE [ProjectManagementDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [ProjectManagementDB];
    PRINT 'Database "ProjectManagementDB" has been dropped.';
END
ELSE
BEGIN
    PRINT 'Database "ProjectManagementDB" does not exist.';
END
GO

CREATE DATABASE [ProjectManagementDB];
PRINT 'Database "ProjectManagementDB" created successfully!';
GO

USE [ProjectManagementDB];
PRINT 'Switched to database: ProjectManagementDB';
GO

-- =============================================
-- TABLES CREATION (đã sắp xếp để tránh circular FK)
-- =============================================

-- Users
CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Phone VARCHAR(20),
    Email NVARCHAR(100),
    PasswordHash NVARCHAR(256) NOT NULL,
    IsActive BIT NOT NULL DEFAULT (1),
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE())
);
PRINT 'Table [Users] created.';
GO

-- UserRoles
CREATE TABLE dbo.UserRoles (
    UserRoleID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Role NVARCHAR(50) NOT NULL CHECK (Role IN ('Admin', N'Quản lý phòng ban', N'Nhân viên', N'Quản lý dự án')),
    AssignedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserID)
        REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
    CONSTRAINT UQ_UserRoles_UserRole UNIQUE (UserID, Role)
);
PRINT 'Table [UserRoles] created.';
GO

-- Departments (tạo trước, nhưng **chưa gán FK Manager -> Employees** để tránh vòng tham chiếu)
CREATE TABLE dbo.Departments (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    ManagerID INT NULL -- FK sẽ được thêm sau khi Employees đã được tạo
);
PRINT 'Table [Departments] created.';
GO

-- Employees (tham chiếu tới Users và Departments)
-- Lưu ý: EmployeeID là PK (shared PK với Users.UserID) – không dùng IDENTITY để cho phép dùng UserID làm EmployeeID
CREATE TABLE dbo.Employees (
    EmployeeID INT PRIMARY KEY, -- sẽ bằng UserID khi thêm
    FullName NVARCHAR(100) NOT NULL,
    Position NVARCHAR(100),
    DepartmentID INT NULL,
    AvatarPath NVARCHAR(500),
    Address NVARCHAR(500),
    HireDate DATE NOT NULL DEFAULT (GETDATE()),
    IsActive BIT NOT NULL DEFAULT (1),
    Gender NVARCHAR(20) NOT NULL DEFAULT ('NotSpecified'),
    CONSTRAINT FK_Employees_Users FOREIGN KEY (EmployeeID)
        REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentID)
        REFERENCES dbo.Departments(DepartmentID) ON DELETE SET NULL
);
PRINT 'Table [Employees] created.';
GO

-- Now add FK from Departments.ManagerID -> Employees.EmployeeID
ALTER TABLE dbo.Departments
    ADD CONSTRAINT FK_Departments_Manager FOREIGN KEY (ManagerID)
        REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL;
PRINT 'FK [FK_Departments_Manager] added (deferred until Employees exist).';
GO

-- Projects (tạo sau Employees để FK CreatedBy/ManagerBy hợp lệ)
CREATE TABLE dbo.Projects (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN (N'Lập kế hoạch',N'Đang thực hiện',N'Hoàn thành',N'Đã hủy')),
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    ManagerBy INT NULL,
    CONSTRAINT CHK_Project_Dates CHECK (EndDate IS NULL OR EndDate >= StartDate),
    CONSTRAINT FK_Projects_Employees FOREIGN KEY (CreatedBy)
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION,
    CONSTRAINT FK_Projects_ManagerBy FOREIGN KEY (ManagerBy)
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [Projects] created.';
GO

-- Tasks
CREATE TABLE dbo.Tasks (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    TaskTitle NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    CreatedBy INT NOT NULL,
    Deadline DATE NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN (N'Cần làm',N'Đang thực hiện',N'Chờ duyệt',N'Hoàn thành')),
    Priority NVARCHAR(20) NOT NULL CHECK (Priority IN (N'Thấp',N'Trung bình',N'Cao',N'Ưu tiên cao')),
    CreatedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    UpdatedDate DATETIME NULL,
    CONSTRAINT FK_Tasks_Projects FOREIGN KEY (ProjectID)
        REFERENCES dbo.Projects(ProjectID) ON DELETE CASCADE,
    CONSTRAINT FK_Tasks_Employees_Created FOREIGN KEY (CreatedBy)
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [Tasks] created.';
GO

-- TaskAssignments
CREATE TABLE dbo.TaskAssignments (
    TaskAssignmentID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
    EmployeeID INT NOT NULL,
    AssignedBy INT NULL,
    AssignedDate DATETIME NOT NULL DEFAULT (GETDATE()),
    CompletionStatus NVARCHAR(20) NOT NULL DEFAULT (N'Pending') CHECK (CompletionStatus IN (N'Pending', N'In Progress', N'Completed')),
    CompletedDate DATETIME NULL,
    CONSTRAINT FK_TaskAssignments_Tasks FOREIGN KEY (TaskID)
        REFERENCES dbo.Tasks(TaskID) ON DELETE CASCADE,
    CONSTRAINT FK_TaskAssignments_Employees FOREIGN KEY (EmployeeID)
        REFERENCES dbo.Employees(EmployeeID) ON DELETE CASCADE,
    CONSTRAINT FK_TaskAssignments_AssignedBy FOREIGN KEY (AssignedBy)
        REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION,
    CONSTRAINT UQ_TaskAssignments_TaskEmployee UNIQUE (TaskID, EmployeeID)
);
PRINT 'Table [TaskAssignments] created.';
GO

-- Subtasks
CREATE TABLE dbo.Subtasks (
    SubtaskID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
    SubtaskTitle NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(20) NOT NULL DEFAULT (N'Cần làm') CHECK (Status IN (N'Cần làm',N'Đang thực hiện',N'Hoàn thành')),
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

-- TaskComments
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

-- EmployeeFiles
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

-- ProjectFiles
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

-- TaskFiles
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

PRINT '==> All tables created successfully!';
GO

-- =============================================
-- TRIGGERS CREATION (giữ trigger quan trọng, tránh trigger gây rollback trên Departments)
-- =============================================

-- TR_Projects_Insert: yêu cầu ManagerBy không null (sample data có ManagerBy nên an toàn)
CREATE TRIGGER TR_Projects_Insert
ON dbo.Projects
AFTER INSERT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE ManagerBy IS NULL)
    BEGIN
        RAISERROR ('ManagerBy must be assigned when creating a project.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
PRINT 'Trigger [TR_Projects_Insert] created.';
GO

-- Trigger cập nhật trạng thái Task khi TaskAssignments thay đổi
CREATE OR ALTER TRIGGER TR_TaskAssignments_UpdateStatus
ON dbo.TaskAssignments
AFTER UPDATE, INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Update CompletedDate for newly completed assignments
    UPDATE ta
    SET CompletedDate = GETDATE()
    FROM dbo.TaskAssignments ta
    INNER JOIN inserted i ON ta.TaskAssignmentID = i.TaskAssignmentID
    WHERE i.CompletionStatus = N'Completed' AND ta.CompletedDate IS NULL;

    -- Cập nhật trạng thái Task: nếu tất cả assignment đã Completed => Task.Status = 'Hoàn thành', else 'Đang thực hiện'
    DECLARE @TaskID INT;
    DECLARE task_cursor CURSOR LOCAL FAST_FORWARD FOR
        SELECT DISTINCT TaskID FROM inserted;
    OPEN task_cursor;
    FETCH NEXT FROM task_cursor INTO @TaskID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @AllCompleted BIT = 1;
        IF EXISTS (
            SELECT 1 FROM TaskAssignments WHERE TaskID = @TaskID AND CompletionStatus != N'Completed'
        )
            SET @AllCompleted = 0;

        UPDATE dbo.Tasks
        SET Status = CASE WHEN @AllCompleted = 1 THEN N'Hoàn thành' ELSE N'Đang thực hiện' END
        WHERE TaskID = @TaskID;

        FETCH NEXT FROM task_cursor INTO @TaskID;
    END
    CLOSE task_cursor;
    DEALLOCATE task_cursor;
END;
PRINT 'Trigger [TR_TaskAssignments_UpdateStatus] created.';
GO

-- Prevent updating completed assignment
CREATE TRIGGER TR_TaskAssignments_PreventUpdateIfCompleted
ON dbo.TaskAssignments
INSTEAD OF UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted WHERE CompletionStatus = N'Completed')
    BEGIN
        RAISERROR ('Cannot update completed assignment.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        UPDATE ta
        SET CompletionStatus = i.CompletionStatus,
            AssignedDate = i.AssignedDate
        FROM dbo.TaskAssignments ta
        INNER JOIN inserted i ON ta.TaskAssignmentID = i.TaskAssignmentID;
    END
END;
PRINT 'Trigger [TR_TaskAssignments_PreventUpdateIfCompleted] created.';
GO

-- =============================================
-- INDEXES CREATION
-- =============================================
PRINT 'Creating indexes...';
CREATE NONCLUSTERED INDEX IX_Employees_DepartmentID ON dbo.Employees(DepartmentID);
CREATE NONCLUSTERED INDEX IX_Departments_ManagerID ON dbo.Departments(ManagerID);
CREATE NONCLUSTERED INDEX IX_Projects_CreatedBy ON dbo.Projects(CreatedBy);
CREATE NONCLUSTERED INDEX IX_Tasks_ProjectID ON dbo.Tasks(ProjectID);
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
CREATE NONCLUSTERED INDEX IX_TaskAssignments_TaskID ON dbo.TaskAssignments(TaskID);
CREATE NONCLUSTERED INDEX IX_TaskAssignments_EmployeeID ON dbo.TaskAssignments(EmployeeID);
CREATE NONCLUSTERED INDEX IX_UserRoles_UserID ON dbo.UserRoles(UserID);
PRINT '==> All indexes created successfully!';
GO

<3

-- =============================================
-- SAMPLE DATA (chèn theo thứ tự đúng)
-- =============================================

PRINT 'Inserting sample data...';
GO

-- Users
INSERT INTO dbo.Users (Phone, Email, PasswordHash)
VALUES
('0900000101', '1', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', '1'), 2)),
('0900000001', 'admin@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Admin@123!'), 2)),
('0901234567', 'nguyen.van.an@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee@123'), 2)),
('0912345678', 'tran.thi.binh@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Manager@123'), 2)),
('0923456789', 'le.minh.cuong@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Manager@123'), 2)),
('0934567890', 'pham.thu.dung@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee@123'), 2)),
('0945678901', 'hoang.quoc.huy@company.com', CONVERT(NVARCHAR(256), HASHBYTES('SHA2_256', 'Employee@123'), 2));
PRINT '==> 6 Users inserted.';
GO

-- UserRoles
INSERT INTO dbo.UserRoles (UserID, Role)
VALUES
(1, 'Admin'),
(2, N'Nhân viên'),
(3, N'Quản lý phòng ban'),
(4, N'Quản lý phòng ban'),
(5, N'Nhân viên'),
(6, N'Nhân viên');
PRINT '==> 6 UserRoles inserted.';
GO

-- Departments (ManagerID NULL ban đầu — sẽ cập nhật sau khi chèn Employees)
INSERT INTO dbo.Departments (DepartmentName, Description, ManagerID)
VALUES
(N'Công nghệ thông tin', N'Phòng phát triển phần mềm và hạ tầng IT', NULL),
(N'Nhân sự', N'Phòng quản lý nguồn nhân lực', NULL),
(N'Tài chính', N'Phòng kế toán và tài chính', NULL),
(N'Marketing', N'Phòng marketing và truyền thông', NULL),
(N'Vận hành', N'Phòng vận hành và logistics', NULL);
PRINT '==> 5 Departments inserted.';
GO

-- Employees (EmployeeID phải tương ứng với UserID khi dùng shared PK)
INSERT INTO dbo.Employees (EmployeeID, FullName, Position, DepartmentID, AvatarPath, Address, HireDate)
VALUES
(2, N'Nguyễn Văn An', N'Lập trình viên', 1, '/Uploads/Avatars/20240101120000_avatar_nva.jpg', N'123 Láng Hạ, Hà Nội', '2020-03-15'),
(3, N'Trần Thị Bình', N'Trưởng phòng Nhân sự', 2, '/Uploads/Avatars/20240101120100_avatar_ttb.png', N'456 Giải Phóng, Hà Nội', '2019-07-20'),
(4, N'Lê Minh Cường', N'Trưởng phòng IT', 1, '/Uploads/Avatars/20240101120200_avatar_lmc.jpg', N'789 Trường Chinh, Hà Nội', '2018-11-10'),
(5, N'Phạm Thu Dung', N'Kế toán trưởng', 3, NULL, N'101 Cầu Giấy, Hà Nội', '2021-02-01'),
(6, N'Hoàng Quốc Huy', N'Trưởng phòng Marketing', 4, NULL, N'202 Nguyễn Trãi, Hà Nội', '2019-09-25');
PRINT '==> 5 Employees inserted with AvatarPath.';
GO

-- Cập nhật Managers cho Departments (bây giờ FK Manager -> Employees đã tồn tại và EmployeeIDs hợp lệ)
UPDATE dbo.Departments SET ManagerID = 4 WHERE DepartmentID = 1; -- Lê Minh Cường (EmployeeID=4)
UPDATE dbo.Departments SET ManagerID = 3 WHERE DepartmentID = 2; -- Trần Thị Bình (3)
UPDATE dbo.Departments SET ManagerID = 5 WHERE DepartmentID = 3; -- Phạm Thu Dung (5)
UPDATE dbo.Departments SET ManagerID = 6 WHERE DepartmentID = 4; -- Hoàng Quốc Huy (6)
PRINT '==> Managers assigned.';
GO

-- Projects (Employees đã tồn tại => FK CreatedBy/ManagerBy hợp lệ)
INSERT INTO dbo.Projects (ProjectName, Description, StartDate, EndDate, Status, CreatedBy, ManagerBy)
VALUES
(N'Hệ thống quản lý nhân sự', N'Xây dựng hệ thống quản lý nhân sự tập trung', '2024-01-15', '2024-07-31', N'Đang thực hiện', 3, 3),
(N'Website thương mại điện tử', N'Phát triển nền tảng bán hàng online', '2024-02-01', NULL, N'Lập kế hoạch', 3, 3),
(N'Ứng dụng mobile CRM', N'App quản lý quan hệ khách hàng trên di động', '2023-09-01', '2024-03-31', N'Hoàn thành', 4, 4),
(N'Tối ưu hệ thống báo cáo', N'Nâng cấp hệ thống báo cáo tài chính', '2024-04-01', '2024-12-31', N'Lập kế hoạch', 4, 4),
(N'Nâng cấp hạ tầng mạng', N'Modernize network infrastructure', '2024-01-10', '2024-06-30', N'Đang thực hiện', 3, 3);
PRINT '==> 5 Projects inserted.';
GO

-- Tasks
-- ===========================
-- Tasks (Status hợp lệ)
-- ===========================
INSERT INTO dbo.Tasks (ProjectID, TaskTitle, Description, CreatedBy, Deadline, Status, Priority)
VALUES
(1, N'Thiết kế database schema', N'Phân tích và thiết kế cấu trúc CSDL', 3, '2024-02-15', N'Hoàn thành', N'Cao'),
(1, N'Phát triển API Backend', N'Xây dựng REST API với .NET', 3, '2024-04-30', N'Cần làm', N'Cao'),
(2, N'Nghiên cứu công nghệ', N'Đánh giá các framework frontend phù hợp', 3, '2024-03-15', N'Cần làm', N'Trung bình'),
(3, N'Kiểm thử chức năng', N'Test toàn bộ tính năng trước khi release', 4, '2024-03-20', N'Hoàn thành', N'Ưu tiên cao'),
(5, N'Cấu hình firewall', N'Setup firewall rules cho hệ thống mới', 3, '2024-05-31', N'Đang thực hiện', N'Cao');
PRINT '==> 5 Tasks inserted.';

-- ===========================
-- TaskAssignments
-- ===========================
INSERT INTO dbo.TaskAssignments (TaskID, EmployeeID, AssignedBy, AssignedDate, CompletionStatus)
VALUES
(1, 4, 3, '2024-01-15', N'Completed'),
(2, 2, 3, '2024-01-15', N'Pending'),
(3, 5, 4, '2024-01-15', N'Pending'),
(4, 6, 3, '2024-01-15', N'Completed'),
(5, 2, 3, '2024-01-15', N'In Progress');
PRINT '==> 5 TaskAssignments inserted.';

-- ===========================
-- Subtasks
-- ===========================
INSERT INTO dbo.Subtasks (TaskID, SubtaskTitle, Description, Status, Progress, AssignedTo, Deadline)
VALUES
(1, N'Thiết kế bảng Users', N'Tạo ERD cho bảng Users', N'Hoàn thành', 100, 4, '2024-02-05'),
(1, N'Thiết kế bảng Employees', N'Tạo ERD cho bảng Employees', N'Hoàn thành', 100, 4, '2024-02-08'),
(2, N'Setup project structure', N'Tạo project ASP.NET Core', N'Cần làm', 0, 2, '2024-03-20'),
(2, N'Implement User API', N'Tạo CRUD API cho Users', N'Đang thực hiện', 70, 2, '2024-04-15');
PRINT '==> 4 Subtasks inserted.';

-- ===========================
-- TaskComments
-- ===========================
INSERT INTO dbo.TaskComments (TaskID, EmployeeID, Comment)
VALUES
(1, 3, N'Schema đã được review và approve.'),
(2, 5, N'Cần optimize performance cho API.'),
(4, 4, N'Tất cả test cases đã PASSED.');
PRINT '==> 3 TaskComments inserted.';

-- ===========================
-- TaskFiles
-- ===========================
INSERT INTO dbo.TaskFiles (TaskID, Title, FileName, CreatedBy, CreatedAt)
VALUES
(1, N'Sơ đồ ERD database', '20240205150000_database_diagram.png', 4, '2024-02-05 15:00:00'),
(2, N'Kết quả test API', '20240410160000_api_test_results.xlsx', 2, '2024-04-10 16:00:00');
PRINT '==> 2 TaskFiles inserted.';


-- EmployeeFiles
INSERT INTO dbo.EmployeeFiles (EmployeeID, Title, FileName, CreatedBy, CreatedAt)
VALUES
(4, N'CV Lê Minh Cường', '20240115100000_cv_lmc.pdf', 4, '2024-01-15 10:00:00'),
(4, N'Chứng chỉ AWS', '20240120150000_aws_cert.pdf', 4, '2024-01-20 15:00:00');
PRINT '==> 2 EmployeeFiles inserted.';
GO

-- ProjectFiles
INSERT INTO dbo.ProjectFiles (ProjectID, Title, FileName, CreatedBy, CreatedAt)
VALUES
(1, N'Tài liệu yêu cầu dự án', '20240115100000_requirements.pdf', 3, '2024-01-15 10:00:00'),
(1, N'Mockup thiết kế giao diện', '20240120140000_design_mockup.png', 4, '2024-01-20 14:00:00');
PRINT '==> 2 ProjectFiles inserted.';
GO

GO

-- =============================================
-- SUMMARY
-- =============================================
PRINT '';
PRINT '========================================';
PRINT ' DATABASE SETUP COMPLETED!';
PRINT '========================================';
PRINT 'Tables: 12';
PRINT ' - Users, UserRoles, Departments, Employees (with AvatarPath)';
PRINT ' - Projects, Tasks, TaskAssignments, Subtasks, TaskComments';
PRINT ' - EmployeeFiles (CV, certificates)';
PRINT ' - ProjectFiles (Requirements, designs)';
PRINT ' - TaskFiles (Screenshots, test results)';
PRINT '';
PRINT 'AVATAR HANDLING:';
PRINT ' - Avatar stored in Employees.AvatarPath';
PRINT ' - Path format: /Uploads/Avatars/{timestamp}_{fileName}';
PRINT ' - Other employee docs in EmployeeFiles table';
PRINT '';
PRINT 'FOREIGN KEYS:';
PRINT ' - All file tables have FK to parent table';
PRINT ' - CASCADE DELETE enabled (where appropriate)';
PRINT '';
PRINT 'Login: 0900000001 / Admin@123!';
PRINT '========================================';
GO