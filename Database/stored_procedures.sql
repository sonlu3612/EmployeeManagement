-- =============================================
-- Script: Stored Procedures for ProjectManagementDB
-- Description: CRUD operations and reporting procedures
-- Created: 2024
-- =============================================

USE [ProjectManagementDB];
GO

PRINT 'Creating Stored Procedures...';
GO

-- =============================================
-- SECTION 1: EMPLOYEE PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_Employee_GetAll
-- Description: Get all active employees with department info
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
    
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Employee_GetAll';
GO

-- ---------------------------------------------
-- sp_Employee_GetById
-- Description: Get employee by ID with department info
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
        IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
        BEGIN
      PRINT 'Employee not found';
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
        
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Employee_GetById';
GO

-- ---------------------------------------------
-- sp_Employee_Insert
-- Description: Insert new employee
-- Returns: New EmployeeID or -1 if failed
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
    @NewEmployeeID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
  
        -- Validate required fields
        IF @FullName IS NULL OR LTRIM(RTRIM(@FullName)) = ''
    BEGIN
          PRINT 'FullName is required';
            ROLLBACK TRANSACTION;
            RETURN -1;
    END
     
        -- Check if email already exists
        IF @Email IS NOT NULL AND EXISTS (SELECT 1 FROM dbo.Employees WHERE Email = @Email)
        BEGIN
            PRINT 'Email already exists';
       ROLLBACK TRANSACTION;
      RETURN -1;
   END
    
        -- Validate Department exists if provided
        IF @DepartmentID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Departments WHERE DepartmentID = @DepartmentID)
        BEGIN
  PRINT 'Department does not exist';
         ROLLBACK TRANSACTION;
            RETURN -1;
END
        
        -- Insert employee
        INSERT INTO dbo.Employees (FullName, Email, Phone, DepartmentID, ImagePath, Address, HireDate, IsActive)
 VALUES (@FullName, @Email, @Phone, @DepartmentID, @ImagePath, @Address, GETDATE(), 1);
     
        -- Get new ID
        SET @NewEmployeeID = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        PRINT 'Employee created successfully with ID: ' + CAST(@NewEmployeeID AS NVARCHAR(10));
        RETURN @NewEmployeeID;
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
     ROLLBACK TRANSACTION;
  
        PRINT ERROR_MESSAGE();
        SET @NewEmployeeID = -1;
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Employee_Insert';
GO

-- ---------------------------------------------
-- sp_Employee_Update
-- Description: Update existing employee
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
        
     -- Check if employee exists
        IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
    BEGIN
            PRINT 'Employee not found';
    ROLLBACK TRANSACTION;
    RETURN -1;
        END
   
        -- Validate required fields
    IF @FullName IS NULL OR LTRIM(RTRIM(@FullName)) = ''
        BEGIN
    PRINT 'FullName is required';
    ROLLBACK TRANSACTION;
  RETURN -1;
  END
  
        -- Check if email already exists for another employee
      IF @Email IS NOT NULL AND EXISTS (
          SELECT 1 FROM dbo.Employees 
WHERE Email = @Email AND EmployeeID != @EmployeeID
        )
        BEGIN
            PRINT 'Email already exists for another employee';
            ROLLBACK TRANSACTION;
       RETURN -1;
        END
      
        -- Validate Department exists if provided
IF @DepartmentID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Departments WHERE DepartmentID = @DepartmentID)
        BEGIN
            PRINT 'Department does not exist';
            ROLLBACK TRANSACTION;
         RETURN -1;
        END
   
        -- Update employee
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
        PRINT 'Employee updated successfully';
RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
         ROLLBACK TRANSACTION;

     PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Employee_Update';
GO

-- ---------------------------------------------
-- sp_Employee_Delete
-- Description: Soft delete employee (set IsActive = 0)
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
      
        -- Check if employee exists
 IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
        BEGIN
        PRINT 'Employee not found';
         ROLLBACK TRANSACTION;
            RETURN -1;
     END
        
     -- Check if employee is a department manager
      IF EXISTS (SELECT 1 FROM dbo.Departments WHERE ManagerID = @EmployeeID)
  BEGIN
            PRINT 'Cannot delete employee who is a department manager';
            ROLLBACK TRANSACTION;
            RETURN -1;
     END
        
        -- Soft delete
        UPDATE dbo.Employees
  SET IsActive = 0
  WHERE EmployeeID = @EmployeeID;
        
        COMMIT TRANSACTION;
     PRINT 'Employee deleted successfully';
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
 IF @@TRANCOUNT > 0
          ROLLBACK TRANSACTION;
  
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Employee_Delete';
GO

-- ---------------------------------------------
-- sp_Employee_GetByDepartment
-- Description: Get all employees in a specific department
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
    -- Validate department exists
        IF NOT EXISTS (SELECT 1 FROM dbo.Departments WHERE DepartmentID = @DepartmentID)
        BEGIN
   PRINT 'Department not found';
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
        
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Employee_GetByDepartment';
GO

-- =============================================
-- SECTION 2: PROJECT PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_Project_GetAll
-- Description: Get all projects with creator info
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
        
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
    RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Project_GetAll';
GO

-- ---------------------------------------------
-- sp_Project_GetById
-- Description: Get project by ID with creator info
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
        IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
            PRINT 'Project not found';
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
 
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
      PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
  END CATCH
END
GO

PRINT 'Created: sp_Project_GetById';
GO

-- ---------------------------------------------
-- sp_Project_Insert
-- Description: Insert new project
-- Returns: New ProjectID or -1 if failed
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

        -- Validate required fields
 IF @ProjectName IS NULL OR LTRIM(RTRIM(@ProjectName)) = ''
BEGIN
      PRINT 'ProjectName is required';
            ROLLBACK TRANSACTION;
    RETURN -1;
        END
        
        -- Validate status
        IF @Status NOT IN ('Planning', 'InProgress', 'Completed', 'Cancelled')
      BEGIN
            PRINT 'Invalid status. Must be: Planning, InProgress, Completed, or Cancelled';
  ROLLBACK TRANSACTION;
  RETURN -1;
        END
        
        -- Validate dates
      IF @EndDate IS NOT NULL AND @EndDate < @StartDate
 BEGIN
            PRINT 'EndDate cannot be before StartDate';
       ROLLBACK TRANSACTION;
     RETURN -1;
        END
      
 -- Validate creator exists
     IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @CreatedBy)
        BEGIN
            PRINT 'Creator user does not exist';
          ROLLBACK TRANSACTION;
   RETURN -1;
  END
   
    -- Insert project
        INSERT INTO dbo.Projects (ProjectName, Description, StartDate, EndDate, Status, Budget, CreatedBy, CreatedDate)
     VALUES (@ProjectName, @Description, @StartDate, @EndDate, @Status, @Budget, @CreatedBy, GETDATE());
        
        -- Get new ID
        SET @NewProjectID = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        PRINT 'Project created successfully with ID: ' + CAST(@NewProjectID AS NVARCHAR(10));
        RETURN @NewProjectID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
        SET @NewProjectID = -1;
    RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Project_Insert';
GO

-- ---------------------------------------------
-- sp_Project_Update
-- Description: Update existing project
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
        
        -- Check if project exists
        IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
  PRINT 'Project not found';
      ROLLBACK TRANSACTION;
 RETURN -1;
        END
        
        -- Validate required fields
        IF @ProjectName IS NULL OR LTRIM(RTRIM(@ProjectName)) = ''
        BEGIN
    PRINT 'ProjectName is required';
            ROLLBACK TRANSACTION;
          RETURN -1;
        END
        
        -- Validate status
        IF @Status NOT IN ('Planning', 'InProgress', 'Completed', 'Cancelled')
        BEGIN
 PRINT 'Invalid status. Must be: Planning, InProgress, Completed, or Cancelled';
      ROLLBACK TRANSACTION;
          RETURN -1;
     END

   -- Validate dates
        IF @EndDate IS NOT NULL AND @EndDate < @StartDate
        BEGIN
          PRINT 'EndDate cannot be before StartDate';
     ROLLBACK TRANSACTION;
       RETURN -1;
 END
        
   -- Update project
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
   PRINT 'Project updated successfully';
RETURN 0; -- Success
    END TRY
    BEGIN CATCH
  IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
   
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
END CATCH
END
GO

PRINT 'Created: sp_Project_Update';
GO

-- ---------------------------------------------
-- sp_Project_Delete
-- Description: Delete project (CASCADE will delete related tasks)
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
        
        -- Check if project exists
 IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
 PRINT 'Project not found';
            ROLLBACK TRANSACTION;
     RETURN -1;
        END
        
        -- Delete project (CASCADE will handle tasks and comments)
DELETE FROM dbo.Projects WHERE ProjectID = @ProjectID;
      
        COMMIT TRANSACTION;
        PRINT 'Project deleted successfully';
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
      PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Project_Delete';
GO

-- ---------------------------------------------
-- sp_Project_GetByStatus
-- Description: Get all projects by status
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
        -- Validate status
        IF @Status NOT IN ('Planning', 'InProgress', 'Completed', 'Cancelled')
        BEGIN
   PRINT 'Invalid status. Must be: Planning, InProgress, Completed, or Cancelled';
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

  RETURN 0; -- Success
    END TRY
    BEGIN CATCH
   PRINT ERROR_MESSAGE();
    RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Project_GetByStatus';
GO

-- =============================================
-- SECTION 3: TASK PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_Task_GetAll
-- Description: Get all tasks with related info
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
  
 RETURN 0; -- Success
    END TRY
    BEGIN CATCH
     PRINT ERROR_MESSAGE();
      RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Task_GetAll';
GO

-- ---------------------------------------------
-- sp_Task_GetById
-- Description: Get task by ID with related info
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
        IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
        BEGIN
            PRINT 'Task not found';
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
     
        RETURN 0; -- Success
  END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Task_GetById';
GO

-- ---------------------------------------------
-- sp_Task_GetByProject
-- Description: Get all tasks for a specific project
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
        -- Validate project exists
        IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
            PRINT 'Project not found';
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
        
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Task_GetByProject';
GO

-- ---------------------------------------------
-- sp_Task_GetByEmployee
-- Description: Get all tasks assigned to an employee
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
      -- Validate employee exists
   IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
    BEGIN
 PRINT 'Employee not found';
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
        
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
     RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Task_GetByEmployee';
GO

-- ---------------------------------------------
-- sp_Task_Insert
-- Description: Insert new task
-- Returns: New TaskID or -1 if failed
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
        
   -- Validate required fields
        IF @TaskTitle IS NULL OR LTRIM(RTRIM(@TaskTitle)) = ''
     BEGIN
       PRINT 'TaskTitle is required';
       ROLLBACK TRANSACTION;
        RETURN -1;
        END
        
        -- Validate project exists
        IF NOT EXISTS (SELECT 1 FROM dbo.Projects WHERE ProjectID = @ProjectID)
        BEGIN
         PRINT 'Project does not exist';
        ROLLBACK TRANSACTION;
       RETURN -1;
        END
        
-- Validate status
        IF @Status NOT IN ('Todo', 'InProgress', 'Review', 'Done')
        BEGIN
      PRINT 'Invalid status. Must be: Todo, InProgress, Review, or Done';
         ROLLBACK TRANSACTION;
            RETURN -1;
  END
        
        -- Validate priority
      IF @Priority NOT IN ('Low', 'Medium', 'High', 'Critical')
   BEGIN
          PRINT 'Invalid priority. Must be: Low, Medium, High, or Critical';
            ROLLBACK TRANSACTION;
    RETURN -1;
 END
        
        -- Validate progress range
        IF @Progress < 0 OR @Progress > 100
   BEGIN
        PRINT 'Progress must be between 0 and 100';
 ROLLBACK TRANSACTION;
       RETURN -1;
        END
     
        -- Validate assigned employee exists if provided
        IF @AssignedTo IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @AssignedTo AND IsActive = 1)
        BEGIN
            PRINT 'Assigned employee does not exist or is inactive';
            ROLLBACK TRANSACTION;
      RETURN -1;
        END
     
      -- Validate creator exists
 IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserID = @CreatedBy)
        BEGIN
     PRINT 'Creator user does not exist';
 ROLLBACK TRANSACTION;
     RETURN -1;
    END
        
        -- Insert task
 INSERT INTO dbo.Tasks (ProjectID, TaskTitle, Description, AssignedTo, CreatedBy, Deadline, Status, Priority, Progress, CreatedDate)
  VALUES (@ProjectID, @TaskTitle, @Description, @AssignedTo, @CreatedBy, @Deadline, @Status, @Priority, @Progress, GETDATE());
      
        -- Get new ID
        SET @NewTaskID = SCOPE_IDENTITY();
     
        COMMIT TRANSACTION;
        PRINT 'Task created successfully with ID: ' + CAST(@NewTaskID AS NVARCHAR(10));
   RETURN @NewTaskID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
      ROLLBACK TRANSACTION;
   
        PRINT ERROR_MESSAGE();
        SET @NewTaskID = -1;
        RETURN -1; -- Error
  END CATCH
END
GO

PRINT 'Created: sp_Task_Insert';
GO

-- ---------------------------------------------
-- sp_Task_Update
-- Description: Update existing task
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
        
        -- Check if task exists
     IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
  BEGIN
       PRINT 'Task not found';
 ROLLBACK TRANSACTION;
     RETURN -1;
        END
        
 -- Validate required fields
        IF @TaskTitle IS NULL OR LTRIM(RTRIM(@TaskTitle)) = ''
        BEGIN
    PRINT 'TaskTitle is required';
ROLLBACK TRANSACTION;
  RETURN -1;
    END
        
        -- Validate status
        IF @Status NOT IN ('Todo', 'InProgress', 'Review', 'Done')
        BEGIN
         PRINT 'Invalid status. Must be: Todo, InProgress, Review, or Done';
  ROLLBACK TRANSACTION;
            RETURN -1;
        END
        
 -- Validate priority
        IF @Priority NOT IN ('Low', 'Medium', 'High', 'Critical')
   BEGIN
            PRINT 'Invalid priority. Must be: Low, Medium, High, or Critical';
   ROLLBACK TRANSACTION;
            RETURN -1;
   END
      
        -- Validate progress range
   IF @Progress < 0 OR @Progress > 100
        BEGIN
            PRINT 'Progress must be between 0 and 100';
        ROLLBACK TRANSACTION;
            RETURN -1;
        END
        
        -- Validate assigned employee exists if provided
        IF @AssignedTo IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @AssignedTo AND IsActive = 1)
        BEGIN
            PRINT 'Assigned employee does not exist or is inactive';
       ROLLBACK TRANSACTION;
            RETURN -1;
        END
 
 -- Update task
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
      PRINT 'Task updated successfully';
 RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
    
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Task_Update';
GO

-- ---------------------------------------------
-- sp_Task_Delete
-- Description: Delete task (CASCADE will delete related comments)
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
      
     -- Check if task exists
  IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
     BEGIN
            PRINT 'Task not found';
            ROLLBACK TRANSACTION;
   RETURN -1;
        END
        
        -- Delete task (CASCADE will handle comments)
        DELETE FROM dbo.Tasks WHERE TaskID = @TaskID;
 
        COMMIT TRANSACTION;
        PRINT 'Task deleted successfully';
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
      IF @@TRANCOUNT > 0
    ROLLBACK TRANSACTION;
   
      PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Task_Delete';
GO

-- ---------------------------------------------
-- sp_Task_UpdateProgress
-- Description: Update task progress only
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
        
        -- Check if task exists
        IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE TaskID = @TaskID)
 BEGIN
            PRINT 'Task not found';
            ROLLBACK TRANSACTION;
       RETURN -1;
        END
   
        -- Validate progress range
      IF @Progress < 0 OR @Progress > 100
   BEGIN
  PRINT 'Progress must be between 0 and 100';
            ROLLBACK TRANSACTION;
     RETURN -1;
        END
        
        -- Update progress
 UPDATE dbo.Tasks
        SET 
            Progress = @Progress,
            UpdatedDate = GETDATE()
        WHERE TaskID = @TaskID;
        
        COMMIT TRANSACTION;
        PRINT 'Task progress updated successfully';
      RETURN 0; -- Success
  END TRY
    BEGIN CATCH
      IF @@TRANCOUNT > 0
       ROLLBACK TRANSACTION;
        
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Task_UpdateProgress';
GO

-- =============================================
-- SECTION 4: REPORT PROCEDURES
-- =============================================

-- ---------------------------------------------
-- sp_Report_TasksByStatus
-- Description: Group tasks by status with counts
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
  
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Report_TasksByStatus';
GO

-- ---------------------------------------------
-- sp_Report_EmployeeWorkload
-- Description: Get number of tasks assigned to each employee
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
        
   RETURN 0; -- Success
    END TRY
    BEGIN CATCH
      PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Report_EmployeeWorkload';
GO

-- ---------------------------------------------
-- sp_Report_ProjectProgress
-- Description: Get completion percentage for each project
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
        
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
     RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Report_ProjectProgress';
GO

-- ---------------------------------------------
-- sp_Report_OverdueTasks
-- Description: Get all tasks that are overdue and not done
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
        
        RETURN 0; -- Success
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        RETURN -1; -- Error
    END CATCH
END
GO

PRINT 'Created: sp_Report_OverdueTasks';
GO

-- =============================================
-- COMPLETION SUMMARY
-- =============================================

PRINT '';
PRINT '========================================';
PRINT '  STORED PROCEDURES CREATED!';
PRINT '========================================';
PRINT 'Total Procedures: 28';
PRINT '';
PRINT 'EMPLOYEE Procedures: 6';
PRINT '  - sp_Employee_GetAll';
PRINT '  - sp_Employee_GetById';
PRINT '  - sp_Employee_Insert';
PRINT '  - sp_Employee_Update';
PRINT '  - sp_Employee_Delete';
PRINT '  - sp_Employee_GetByDepartment';
PRINT '';
PRINT 'PROJECT Procedures: 6';
PRINT '  - sp_Project_GetAll';
PRINT '  - sp_Project_GetById';
PRINT '  - sp_Project_Insert';
PRINT '  - sp_Project_Update';
PRINT '  - sp_Project_Delete';
PRINT '  - sp_Project_GetByStatus';
PRINT '';
PRINT 'TASK Procedures: 8';
PRINT '  - sp_Task_GetAll';
PRINT '  - sp_Task_GetById';
PRINT '  - sp_Task_GetByProject';
PRINT '  - sp_Task_GetByEmployee';
PRINT '  - sp_Task_Insert';
PRINT '  - sp_Task_Update';
PRINT '  - sp_Task_Delete';
PRINT '  - sp_Task_UpdateProgress';
PRINT '';
PRINT 'REPORT Procedures: 4';
PRINT '  - sp_Report_TasksByStatus';
PRINT '  - sp_Report_EmployeeWorkload';
PRINT '  - sp_Report_ProjectProgress';
PRINT '  - sp_Report_OverdueTasks';
PRINT '========================================';
GO

-- =============================================
-- SECTION 5: SAMPLE TEST CALLS
-- =============================================

PRINT '';
PRINT '========================================';
PRINT '  RUNNING SAMPLE TEST CALLS';
PRINT '========================================';
GO

-- Test 1: Get all employees
PRINT '';
PRINT '--- TEST 1: Get All Employees ---';
EXEC dbo.sp_Employee_GetAll;
GO

-- Test 2: Get employee by ID
PRINT '';
PRINT '--- TEST 2: Get Employee By ID (EmployeeID=1) ---';
EXEC dbo.sp_Employee_GetById @EmployeeID = 1;
GO

-- Test 3: Get employees by department
PRINT '';
PRINT '--- TEST 3: Get Employees By Department (DepartmentID=1) ---';
EXEC dbo.sp_Employee_GetByDepartment @DepartmentID = 1;
GO

-- Test 4: Insert new employee
PRINT '';
PRINT '--- TEST 4: Insert New Employee ---';
DECLARE @NewEmpID INT;
EXEC dbo.sp_Employee_Insert 
    @FullName = N'Nguy?n Th? Mai',
    @Email = 'nguyen.thi.mai@company.com',
    @Phone = '0956789012',
    @DepartmentID = 2,
    @Address = N'303 Hai Bà Tr?ng, Hà N?i',
    @ImagePath = '/images/employees/ntm.jpg',
    @NewEmployeeID = @NewEmpID OUTPUT;
SELECT @NewEmpID AS NewEmployeeID;
GO

-- Test 5: Get all projects
PRINT '';
PRINT '--- TEST 5: Get All Projects ---';
EXEC dbo.sp_Project_GetAll;
GO

-- Test 6: Get projects by status
PRINT '';
PRINT '--- TEST 6: Get Projects By Status (InProgress) ---';
EXEC dbo.sp_Project_GetByStatus @Status = 'InProgress';
GO

-- Test 7: Get tasks by project
PRINT '';
PRINT '--- TEST 7: Get Tasks By Project (ProjectID=1) ---';
EXEC dbo.sp_Task_GetByProject @ProjectID = 1;
GO

-- Test 8: Get tasks by employee
PRINT '';
PRINT '--- TEST 8: Get Tasks By Employee (EmployeeID=3) ---';
EXEC dbo.sp_Task_GetByEmployee @EmployeeID = 3;
GO

-- Test 9: Update task progress
PRINT '';
PRINT '--- TEST 9: Update Task Progress (TaskID=2, Progress=75) ---';
EXEC dbo.sp_Task_UpdateProgress @TaskID = 2, @Progress = 75;
GO

-- Test 10: Report - Tasks by Status
PRINT '';
PRINT '--- TEST 10: Report - Tasks By Status ---';
EXEC dbo.sp_Report_TasksByStatus;
GO

-- Test 11: Report - Employee Workload
PRINT '';
PRINT '--- TEST 11: Report - Employee Workload ---';
EXEC dbo.sp_Report_EmployeeWorkload;
GO

-- Test 12: Report - Project Progress
PRINT '';
PRINT '--- TEST 12: Report - Project Progress ---';
EXEC dbo.sp_Report_ProjectProgress;
GO

-- Test 13: Report - Overdue Tasks
PRINT '';
PRINT '--- TEST 13: Report - Overdue Tasks ---';
EXEC dbo.sp_Report_OverdueTasks;
GO

PRINT '';
PRINT '========================================';
PRINT '  ALL TESTS COMPLETED!';
PRINT '========================================';
PRINT '';
PRINT 'NOTES:';
PRINT '- All procedures use TRY-CATCH for error handling';
PRINT '- Transactions are used for INSERT/UPDATE/DELETE';
PRINT '- Return codes: 0 = Success, -1 = Error';
PRINT '- All date validations are in place';
PRINT '- Foreign key validations are implemented';
PRINT '- Soft delete is used for Employees';
PRINT '- CASCADE delete is used for Projects/Tasks';
PRINT '========================================';
GO
