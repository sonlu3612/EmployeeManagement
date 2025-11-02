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
    ImagePath NVARCHAR(500),
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
    Budget DECIMAL(18,2) NULL,
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
    CONSTRAINT FK_Tasks_Employees_Assigned FOREIGN KEY (AssignedTo) 
   REFERENCES dbo.Employees(EmployeeID) ON DELETE SET NULL,
    CONSTRAINT FK_Tasks_Employees_Created FOREIGN KEY (CreatedBy) 
 REFERENCES dbo.Employees(EmployeeID) ON DELETE NO ACTION
);
PRINT 'Table [Tasks] created.';
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
CREATE NONCLUSTERED INDEX IX_TaskComments_EmployeeID ON dbo.TaskComments(EmployeeID);

-- Phone và Email Indexes (để tối ưu tìm kiếm)
CREATE NONCLUSTERED INDEX IX_Users_Phone ON dbo.Users(Phone) WHERE Phone IS NOT NULL;
CREATE NONCLUSTERED INDEX IX_Users_Email ON dbo.Users(Email) WHERE Email IS NOT NULL;

PRINT '==> All 10 indexes created successfully!';
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
-- LƯU Ý: EmployeeID phải khớp với UserID đã tạo ở bước 1
-- UserID=1: admin (không tạo Employee vì là Admin thuần túy)
-- UserID=2: nva
-- UserID=3: ttb
-- UserID=4: lmc
-- UserID=5: ptd
-- UserID=6: hqh
INSERT INTO dbo.Employees (EmployeeID, FullName, Position, DepartmentID, ImagePath, Address, HireDate)
VALUES
(2, N'Nguyễn Văn An', N'Lập trình viên', 1, '/images/employees/nva.jpg', N'123 Láng Hạ, Hà Nội', '2020-03-15'),
(3, N'Trần Thị Bình', N'Trưởng phòng Nhân sự', 2, '/images/employees/ttb.jpg', N'456 Giải Phóng, Hà Nội', '2019-07-20'),
(4, N'Lê Minh Cường', N'Trưởng phòng IT', 1, '/images/employees/lmc.jpg', N'789 Trường Chinh, Hà Nội', '2018-11-10'),
(5, N'Phạm Thu Dung', N'Kế toán trưởng', 3, '/images/employees/ptd.jpg', N'101 Cầu Giấy, Hà Nội', '2021-02-01'),
(6, N'Hoàng Quốc Huy', N'Trưởng phòng Marketing', 4, '/images/employees/hqh.jpg', N'202 Nguyễn Trãi, Hà Nội', '2019-09-25');
PRINT '==> 5 Employees inserted (EmployeeID = UserID).';
GO

-- 4. Update ManagerID cho Departments
UPDATE dbo.Departments SET ManagerID = 4 WHERE DepartmentID = 1; -- IT: Lê Minh Cường (EmployeeID=4)
UPDATE dbo.Departments SET ManagerID = 3 WHERE DepartmentID = 2; -- HR: Trần Thị Bình (EmployeeID=3)
UPDATE dbo.Departments SET ManagerID = 5 WHERE DepartmentID = 3; -- Finance: Phạm Thu Dung (EmployeeID=5)
UPDATE dbo.Departments SET ManagerID = 6 WHERE DepartmentID = 4; -- Marketing: Hoàng Quốc Huy (EmployeeID=6)
-- Operations: No manager (NULL)
PRINT '==> Department managers assigned.';
GO

-- 5. Projects (5 records) - CreatedBy dùng EmployeeID
INSERT INTO dbo.Projects (ProjectName, Description, StartDate, EndDate, Status, Budget, CreatedBy)
VALUES
(N'Hệ thống quản lý nhân sự', N'Xây dựng hệ thống quản lý nhân sự tập trung', '2024-01-15', '2024-07-31', 'InProgress', 800000000, 3), -- CreatedBy = Employee ttb (EmployeeID=3)
(N'Website thương mại điện tử', N'Phát triển nền tảng bán hàng online', '2024-02-01', NULL, 'Planning', 1200000000, 3),
(N'Ứng dụng mobile CRM', N'App quản lý quan hệ khách hàng trên di động', '2023-09-01', '2024-03-31', 'Completed', 500000000, 4), -- lmc (EmployeeID=4)
(N'Tối ưu hệ thống báo cáo', N'Nâng cấp hệ thống báo cáo tài chính', '2024-04-01', '2024-12-31', 'Planning', 300000000, 4), -- lmc
(N'Nâng cấp hạ tầng mạng', N'Modernize network infrastructure', '2024-01-10', '2024-06-30', 'InProgress', 600000000, 3);
PRINT '==> 5 Projects inserted.';
GO

-- 6. Tasks (5 records) - AssignedTo và CreatedBy dùng EmployeeID
INSERT INTO dbo.Tasks (ProjectID, TaskTitle, Description, AssignedTo, CreatedBy, Deadline, Status, Priority, Progress)
VALUES
(1, N'Thiết kế database schema', N'Phân tích và thiết kế cấu trúc CSDL', 4, 3, '2024-02-15', 'Done', 'High', 100), -- Assigned to lmc (EmployeeID=4), Created by ttb (EmployeeID=3)
(1, N'Phát triển API Backend', N'Xây dựng REST API với .NET', 2, 3, '2024-04-30', 'InProgress', 'High', 60), -- nva (EmployeeID=2)
(2, N'Nghiên cứu công nghệ', N'Đánh giá các framework frontend phù hợp', NULL, 3, '2024-03-15', 'Todo', 'Medium', 0),
(3, N'Kiểm thử chức năng', N'Test toàn bộ tính năng trước khi release', 5, 4, '2024-03-20', 'Done', 'Critical', 100), -- ptd (EmployeeID=5), Created by lmc (EmployeeID=4)
(5, N'Cấu hình firewall', N'Setup firewall rules cho hệ thống mới', 6, 3, '2024-05-31', 'InProgress', 'High', 30); -- hqh (EmployeeID=6)
PRINT '==> 5 Tasks inserted.';
GO

-- 7. TaskComments (5 records) - EmployeeID
INSERT INTO dbo.TaskComments (TaskID, EmployeeID, Comment)
VALUES
(1, 3, N'Schema đã được review và approve bởi team lead.'),
(2, 5, N'Cần optimize performance cho API lấy danh sách users.'),
(2, 3, N'Đã áp dụng caching, response time giảm từ 800ms xuống 120ms.'),
(4, 4, N'Tất cả 45 test cases đã PASSED. Ready for production.'),
(5, 3, N'Cần phối hợp với team Network để test connection.');
PRINT '==> 5 TaskComments inserted.';
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
PRINT 'Tables Created: 6';
PRINT '  - Users';
PRINT '  - Departments';
PRINT '  - Employees (Shared PK with Users)';
PRINT '  - Projects';
PRINT '- Tasks';
PRINT '  - TaskComments';
PRINT '';
PRINT 'Indexes Created: 9';
PRINT 'Sample Records:';
PRINT '  - Users: 6 (1 Admin, 5 Employees)';
PRINT '  - Employees: 5 (EmployeeID = UserID)';
PRINT '  - Departments: 5';
PRINT '  - Projects: 5';
PRINT '  - Tasks: 5';
PRINT '  - Comments: 5';
PRINT '';
PRINT 'Default Login Credentials:';
PRINT '  Phone: 0900000001 | Email: admin@company.com - Password: Admin@123! (Admin)';
PRINT '  Phone: 0901234567 | Email: nguyen.van.an@company.com - Password: Employee@123 (Employee)';
PRINT '  Phone: 0912345678 | Email: tran.thi.binh@company.com - Password: Manager@123 (Manager)';
PRINT '';
PRINT '========================================';
PRINT 'QUAN HỆ 1-1 GIỮA USERS VÀ EMPLOYEES:';
PRINT '- Employees.EmployeeID = Users.UserID (Shared PK)';
PRINT '- Workflow: Tạo User trước → Tạo Employee với ID giống User';
PRINT '- ON DELETE CASCADE: Xóa User → tự động xóa Employee';
PRINT '- Đảm bảo tuyệt đối 1-1 (không cần UNIQUE constraint)';
PRINT '';
PRINT 'THAY ĐỔI CẤU TRÚC:';
PRINT '- Users: Bỏ Username; Thêm Phone và Email';
PRINT '- Employees: Bỏ Email, Phone; Thêm Position';
PRINT '- Projects.CreatedBy: Giờ liên kết với Employees';
PRINT '- Tasks.CreatedBy: Giờ liên kết với Employees';
PRINT '- TaskComments.EmployeeID: Giờ liên kết với Employees';
PRINT '';
PRINT 'ĐĂNG NHẬP:';
PRINT '- Sử dụng Phone hoặc Email thay cho Username';
PRINT '- Phone và Email được lưu trong bảng Users';
PRINT '========================================';

PRINT 'SECURITY NOTES:';
PRINT '- Passwords are hashed using SHA2_256';
PRINT '- In production, use BCrypt/Argon2 with salt';
PRINT '- Always use parameterized queries';
PRINT '- Enable SQL Server audit logs';
PRINT '========================================';
GO

