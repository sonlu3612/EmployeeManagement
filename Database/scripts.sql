USE [ProjectManagementDB]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ManagerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeFiles]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeFiles](
	[EmployeeFileID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Position] [nvarchar](100) NULL,
	[DepartmentID] [int] NULL,
	[AvatarPath] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[HireDate] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[GENDER] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectFiles]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectFiles](
	[ProjectFileID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ManagerBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subtasks]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subtasks](
	[SubtaskID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[SubtaskTitle] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Progress] [int] NOT NULL,
	[AssignedTo] [int] NULL,
	[Deadline] [date] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[SubtaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskAssignments]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskAssignments](
	[TaskAssignmentID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[AssignedBy] [int] NULL,
	[AssignedDate] [datetime] NOT NULL,
	[CompletionStatus] [nvarchar](20) NOT NULL,
	[CompletedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskAssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskComments]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskComments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[CommentDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskFiles]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskFiles](
	[TaskFileID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[TaskTitle] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[Deadline] [date] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Priority] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[AssignedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/7/2025 4:56:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [varchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[PasswordHash] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName], [Description], [ManagerID]) VALUES (1, N'Công nghệ thông tin', N'Phòng phát triển phần mềm và hạ tầng IT', 4)
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName], [Description], [ManagerID]) VALUES (2, N'Nhân sự', N'Phòng quản lý nguồn nhân lực', 3)
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName], [Description], [ManagerID]) VALUES (3, N'Tài chính', N'Phòng kế toán, tài chính', 5)
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName], [Description], [ManagerID]) VALUES (4, N'Marketing', N'Phòng marketing và truyền thông', 6)
INSERT [dbo].[Departments] ([DepartmentID], [DepartmentName], [Description], [ManagerID]) VALUES (1003, N'Nghiên cứu', N'Nghiên cứu cơ sở hạ tầng', 6)
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeFiles] ON 

INSERT [dbo].[EmployeeFiles] ([EmployeeFileID], [EmployeeID], [Title], [FileName], [CreatedBy], [CreatedAt]) VALUES (1, 4, N'CV Lê Minh Cường', N'20240115100000_cv_lmc.pdf', 4, CAST(N'2024-01-15T10:00:00.000' AS DateTime))
INSERT [dbo].[EmployeeFiles] ([EmployeeFileID], [EmployeeID], [Title], [FileName], [CreatedBy], [CreatedAt]) VALUES (2, 4, N'Chứng chỉ AWS', N'20240120150000_aws_cert.pdf', 4, CAST(N'2024-01-20T15:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[EmployeeFiles] OFF
GO
INSERT [dbo].[Employees] ([EmployeeID], [FullName], [Position], [DepartmentID], [AvatarPath], [Address], [HireDate], [IsActive], [GENDER]) VALUES (2, N'Nguyễn Văn An', N'Lập trình viên', 1, N'Assets/Avatars/20251106113813_Không có tiêu đề.jpg', N'123 Láng Hạ, Hà Nội', CAST(N'2020-03-15' AS Date), 0, N'Nam')
INSERT [dbo].[Employees] ([EmployeeID], [FullName], [Position], [DepartmentID], [AvatarPath], [Address], [HireDate], [IsActive], [GENDER]) VALUES (3, N'Trần Thị Bình', N'Trưởng phòng Nhân sự', 2, N'Assets/Avatars/20251106113813_Không có tiêu đề.jpg', N'456 Giải Phóng, Hà Nội', CAST(N'2019-07-20' AS Date), 1, N'Nữ')
INSERT [dbo].[Employees] ([EmployeeID], [FullName], [Position], [DepartmentID], [AvatarPath], [Address], [HireDate], [IsActive], [GENDER]) VALUES (4, N'Lê Minh Cường', N'Trưởng phòng IT', 1, N'Assets/Avatars/20251106113813_Không có tiêu đề.jpg', N'789 Trường Chinh, Hà Nội', CAST(N'2018-11-10' AS Date), 1, N'Nam')
INSERT [dbo].[Employees] ([EmployeeID], [FullName], [Position], [DepartmentID], [AvatarPath], [Address], [HireDate], [IsActive], [GENDER]) VALUES (5, N'Phạm Thu Dung', N'Kế toán trưởng', 3, N'Assets/Avatars/20251106113813_Không có tiêu đề.jpg', N'101 Cầu Giấy, Hà Nội', CAST(N'2021-02-01' AS Date), 1, N'Nữ')
INSERT [dbo].[Employees] ([EmployeeID], [FullName], [Position], [DepartmentID], [AvatarPath], [Address], [HireDate], [IsActive], [GENDER]) VALUES (6, N'Hoàng Quốc Huy', N'Trưởng phòng Marketing', NULL, N'Assets/Avatars/20251107151753_nv1.jpg', N'202 Nguyễn Trãi, Hà Nội', CAST(N'2019-09-25' AS Date), 1, N'Nam')
INSERT [dbo].[Employees] ([EmployeeID], [FullName], [Position], [DepartmentID], [AvatarPath], [Address], [HireDate], [IsActive], [GENDER]) VALUES (1016, N'Hoàng Văn Quyết', N'Nhân viên', 2, N'Assets/Avatars/20251106113813_Không có tiêu đề.jpg', N'HCM', CAST(N'2025-11-06' AS Date), 1, N'Nam')
INSERT [dbo].[Employees] ([EmployeeID], [FullName], [Position], [DepartmentID], [AvatarPath], [Address], [HireDate], [IsActive], [GENDER]) VALUES (1018, N'Sl', N'l', 1003, N'Assets/Avatars/20251106134107_cute-hollow-knight-hornet-pack.png', N'l', CAST(N'2025-11-13' AS Date), 1, N'Nam')
GO
SET IDENTITY_INSERT [dbo].[ProjectFiles] ON 

INSERT [dbo].[ProjectFiles] ([ProjectFileID], [ProjectID], [Title], [FileName], [CreatedBy], [CreatedAt]) VALUES (1, 1, N'Tài liệu yêu cầu dự án', N'20240115100000_requirements.pdf', 3, CAST(N'2024-01-15T10:00:00.000' AS DateTime))
INSERT [dbo].[ProjectFiles] ([ProjectFileID], [ProjectID], [Title], [FileName], [CreatedBy], [CreatedAt]) VALUES (2, 1, N'Mockup thiết kế giao diện', N'20240120140000_design_mockup.png', 4, CAST(N'2024-01-20T14:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[ProjectFiles] OFF
GO
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [Description], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedDate], [ManagerBy]) VALUES (1, N'Hệ thống quản lý nhân sự', N'Xây dựng hệ thống quản lý nhân sự tập trung', CAST(N'2024-01-15' AS Date), CAST(N'2024-07-31' AS Date), N'Đang thực hiện', 3, CAST(N'2025-11-02T15:53:42.523' AS DateTime), 3)
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [Description], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedDate], [ManagerBy]) VALUES (2, N'Website thương mại điện tử', N'Phát triển nền tảng bán hàng online', CAST(N'2024-02-01' AS Date), NULL, N'Lên kế hoạch', 3, CAST(N'2025-11-02T15:53:42.523' AS DateTime), 4)
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [Description], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedDate], [ManagerBy]) VALUES (3, N'Ứng dụng mobile CRM', N'App quản lý quan hệ khách hàng trên di động', CAST(N'2023-09-01' AS Date), CAST(N'2024-03-31' AS Date), N'Hoàn thành', 4, CAST(N'2025-11-02T15:53:42.523' AS DateTime), 6)
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [Description], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedDate], [ManagerBy]) VALUES (4, N'Tối ưu hệ thống báo cáo', N'Nâng cấp hệ thống báo cáo tài chính', CAST(N'2024-04-01' AS Date), CAST(N'2024-12-31' AS Date), N'Lên kế hoạch', 4, CAST(N'2025-11-02T15:53:42.523' AS DateTime), 5)
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [Description], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedDate], [ManagerBy]) VALUES (5, N'Nâng cấp hạ tầng mạng', N'Modernize network infrastructure', CAST(N'2024-01-10' AS Date), CAST(N'2024-06-30' AS Date), N'Đang thực hiện', 3, CAST(N'2025-11-02T15:53:42.523' AS DateTime), 3)
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [Description], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedDate], [ManagerBy]) VALUES (1002, N'Ứng dụng quản lý kho hàng', N'Xây dựng hệ thống quản lý kho hàng tự động', CAST(N'2025-11-01' AS Date), CAST(N'2026-05-31' AS Date), N'Lên kế hoạch', 3, CAST(N'2025-11-05T23:14:43.950' AS DateTime), NULL)
INSERT [dbo].[Projects] ([ProjectID], [ProjectName], [Description], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedDate], [ManagerBy]) VALUES (1006, N'1', N'1', CAST(N'2025-11-06' AS Date), CAST(N'2025-11-06' AS Date), N'Lên kế hoạch', 6, CAST(N'2025-11-06T19:25:56.613' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[Subtasks] ON 

INSERT [dbo].[Subtasks] ([SubtaskID], [TaskID], [SubtaskTitle], [Description], [Status], [Progress], [AssignedTo], [Deadline], [CreatedDate], [UpdatedDate]) VALUES (3, 2, N'Setup project structure', N'Tạo project ASP.NET Core', N'Done', 100, 2, CAST(N'2024-03-20' AS Date), CAST(N'2025-11-02T15:53:42.543' AS DateTime), NULL)
INSERT [dbo].[Subtasks] ([SubtaskID], [TaskID], [SubtaskTitle], [Description], [Status], [Progress], [AssignedTo], [Deadline], [CreatedDate], [UpdatedDate]) VALUES (4, 2, N'Implement User API', N'Tạo CRUD API cho Users', N'InProgress', 70, 2, CAST(N'2024-04-15' AS Date), CAST(N'2025-11-02T15:53:42.543' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Subtasks] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskAssignments] ON 

INSERT [dbo].[TaskAssignments] ([TaskAssignmentID], [TaskID], [EmployeeID], [AssignedBy], [AssignedDate], [CompletionStatus], [CompletedDate]) VALUES (55, 2, 3, 3, CAST(N'2025-11-06T16:23:11.670' AS DateTime), N'Pending', NULL)
INSERT [dbo].[TaskAssignments] ([TaskAssignmentID], [TaskID], [EmployeeID], [AssignedBy], [AssignedDate], [CompletionStatus], [CompletedDate]) VALUES (56, 2, 6, 3, CAST(N'2025-11-06T16:23:11.670' AS DateTime), N'Completed', CAST(N'2025-11-06T18:14:18.370' AS DateTime))
INSERT [dbo].[TaskAssignments] ([TaskAssignmentID], [TaskID], [EmployeeID], [AssignedBy], [AssignedDate], [CompletionStatus], [CompletedDate]) VALUES (57, 4, 6, 4, CAST(N'2025-11-06T18:17:40.757' AS DateTime), N'Pending', NULL)
INSERT [dbo].[TaskAssignments] ([TaskAssignmentID], [TaskID], [EmployeeID], [AssignedBy], [AssignedDate], [CompletionStatus], [CompletedDate]) VALUES (58, 3, 6, 3, CAST(N'2025-11-06T18:17:57.897' AS DateTime), N'Completed', CAST(N'2025-11-06T18:18:03.463' AS DateTime))
INSERT [dbo].[TaskAssignments] ([TaskAssignmentID], [TaskID], [EmployeeID], [AssignedBy], [AssignedDate], [CompletionStatus], [CompletedDate]) VALUES (60, 1002, 3, 3, CAST(N'2025-11-06T21:03:41.307' AS DateTime), N'Pending', NULL)
SET IDENTITY_INSERT [dbo].[TaskAssignments] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskComments] ON 

INSERT [dbo].[TaskComments] ([CommentID], [TaskID], [EmployeeID], [Comment], [CommentDate]) VALUES (2, 2, 5, N'Cần optimize performance cho API.', CAST(N'2025-11-02T15:53:42.550' AS DateTime))
INSERT [dbo].[TaskComments] ([CommentID], [TaskID], [EmployeeID], [Comment], [CommentDate]) VALUES (3, 4, 4, N'Tất cả test cases đã PASSED.', CAST(N'2025-11-02T15:53:42.550' AS DateTime))
SET IDENTITY_INSERT [dbo].[TaskComments] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskFiles] ON 

INSERT [dbo].[TaskFiles] ([TaskFileID], [TaskID], [Title], [FileName], [CreatedBy], [CreatedAt]) VALUES (2, 2, N'Kết quả test API', N'20240410160000_api_test_results.xlsx', 2, CAST(N'2024-04-10T16:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TaskFiles] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([TaskID], [ProjectID], [TaskTitle], [Description], [CreatedBy], [Deadline], [Status], [Priority], [CreatedDate], [UpdatedDate]) VALUES (2, 1, N'Phát triển API Backend', N'Xây dựng REST API với .NET', 3, CAST(N'2024-04-30' AS Date), N'Đang thực hiện', N'High', CAST(N'2025-11-02T15:53:42.533' AS DateTime), NULL)
INSERT [dbo].[Tasks] ([TaskID], [ProjectID], [TaskTitle], [Description], [CreatedBy], [Deadline], [Status], [Priority], [CreatedDate], [UpdatedDate]) VALUES (3, 2, N'Nghiên cứu công nghệ', N'Đánh giá các framework frontend phù hợp', 3, CAST(N'2025-11-07' AS Date), N'Hoàn thành', N'Medium', CAST(N'2025-11-02T15:53:42.533' AS DateTime), CAST(N'2025-11-06T18:14:38.843' AS DateTime))
INSERT [dbo].[Tasks] ([TaskID], [ProjectID], [TaskTitle], [Description], [CreatedBy], [Deadline], [Status], [Priority], [CreatedDate], [UpdatedDate]) VALUES (4, 3, N'Kiểm thử chức năng', N'Test toàn bộ tính năng trước khi release', 4, NULL, N'Hoàn thành', N'Critical', CAST(N'2025-11-02T15:53:42.533' AS DateTime), CAST(N'2025-11-07T16:52:40.090' AS DateTime))
INSERT [dbo].[Tasks] ([TaskID], [ProjectID], [TaskTitle], [Description], [CreatedBy], [Deadline], [Status], [Priority], [CreatedDate], [UpdatedDate]) VALUES (1002, 1002, N'Thiết kế database', N'Tạo schema cho kho hàng', 3, CAST(N'2025-11-07' AS Date), N'Đang thực hiện', N'High', CAST(N'2025-11-05T23:14:43.953' AS DateTime), CAST(N'2025-11-06T18:17:16.470' AS DateTime))
INSERT [dbo].[Tasks] ([TaskID], [ProjectID], [TaskTitle], [Description], [CreatedBy], [Deadline], [Status], [Priority], [CreatedDate], [UpdatedDate]) VALUES (1004, 3, N'Lập trình giao diện', N'UI', 6, NULL, N'Hoàn thành', N'Trung bình', CAST(N'2025-11-07T16:50:59.193' AS DateTime), CAST(N'2025-11-07T16:52:04.540' AS DateTime))
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (3, 1, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (4, 6, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (5, 1002, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (6, 1003, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (7, 1004, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (8, 1005, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (9, 1009, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (10, 1010, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (11, 1012, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (12, 1013, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (13, 1015, N'Admin', CAST(N'2025-11-06T12:51:02.883' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (14, 1018, N'Nhân viên', CAST(N'2025-11-06T13:41:23.207' AS DateTime))
INSERT [dbo].[UserRoles] ([UserRoleID], [UserID], [Role], [AssignedDate]) VALUES (15, 3, N'Quản lý phòng ban', CAST(N'2025-11-06T12:51:02.883' AS DateTime))

SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1, N'0900000001', N'admin@company.com', N'6CF0EA55E5FD5E692E007B16339A83F4319370CDB8B6193C1630820119CBBA50', 1, CAST(N'2025-11-02T15:53:42.480' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (2, N'0901234567', N'nguyen.van.an@company.com', N'B4BD29480AB196FAA782E0D4ECD10C2F4212814105227E5F7992F5BF4B212A64', 1, CAST(N'2025-11-02T15:53:42.480' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (3, N'0912345678', N'tran.thi.binh@company.com', N'E8392925A98C9C22795D1FC5D0DFEE5B9A6943F6B768EC5A2A0C077E5ED119CF', 1, CAST(N'2025-11-02T15:53:42.480' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (4, N'0923456789', N'le.minh.cuong@company.com', N'E8392925A98C9C22795D1FC5D0DFEE5B9A6943F6B768EC5A2A0C077E5ED119CF', 1, CAST(N'2025-11-02T15:53:42.480' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (5, N'0934567890', N'pham.thu.dung@company.com', N'B4BD29480AB196FAA782E0D4ECD10C2F4212814105227E5F7992F5BF4B212A64', 1, CAST(N'2025-11-02T15:53:42.480' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (6, N'0934567891', N'1', N'EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F', 1, CAST(N'2025-11-02T15:53:42.480' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (7, N'0945678901', N'hoang.quoc.huy@company.com', N'B4BD29480AB196FAA782E0D4ECD10C2F4212814105227E5F7992F5BF4B212A64', 1, CAST(N'2025-11-02T15:53:42.480' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1002, NULL, N'sonlu1207@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T13:19:05.333' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1003, NULL, N'sonlu@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T13:24:01.207' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1004, NULL, N'sonlu@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T13:26:48.440' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1005, NULL, N'sonlu@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T13:29:21.033' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1008, NULL, N's@d.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T13:47:33.380' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1009, NULL, N'ư@q.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T13:56:58.957' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1010, NULL, N'e@e.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T14:06:38.717' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1011, NULL, N's@s.s', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T14:35:39.957' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1012, NULL, N'a@a.n', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T14:36:26.527' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1013, NULL, N'son@m.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T19:34:28.177' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1014, NULL, N'son@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T19:35:50.620' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1015, N'036487541', N'sonlu@gmai.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-05T20:02:27.040' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1016, N'0964235998', N'quyet@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-06T11:38:52.973' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1017, N'12213234', N's@s.s', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-06T13:34:52.443' AS DateTime))
INSERT [dbo].[Users] ([UserID], [Phone], [Email], [PasswordHash], [IsActive], [CreatedDate]) VALUES (1018, N'123', N'g@g.g', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, CAST(N'2025-11-06T13:41:23.203' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Departme__D949CC34A122B9AD]    Script Date: 11/7/2025 4:56:46 PM ******/
ALTER TABLE [dbo].[Departments] ADD UNIQUE NONCLUSTERED 
(
	[DepartmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_TaskAssignments_TaskEmployee]    Script Date: 11/7/2025 4:56:46 PM ******/
ALTER TABLE [dbo].[TaskAssignments] ADD  CONSTRAINT [UQ_TaskAssignments_TaskEmployee] UNIQUE NONCLUSTERED 
(
	[TaskID] ASC,
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_UserRoles_UserRole]    Script Date: 11/7/2025 4:56:46 PM ******/
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [UQ_UserRoles_UserRole] UNIQUE NONCLUSTERED 
(
	[UserID] ASC,
	[Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmployeeFiles] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT (getdate()) FOR [HireDate]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ('NotSpecified') FOR [GENDER]
GO
ALTER TABLE [dbo].[ProjectFiles] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Subtasks] ADD  DEFAULT ('Todo') FOR [Status]
GO
ALTER TABLE [dbo].[Subtasks] ADD  DEFAULT ((0)) FOR [Progress]
GO
ALTER TABLE [dbo].[Subtasks] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TaskAssignments] ADD  DEFAULT (getdate()) FOR [AssignedDate]
GO
ALTER TABLE [dbo].[TaskAssignments] ADD  DEFAULT (N'Pending') FOR [CompletionStatus]
GO
ALTER TABLE [dbo].[TaskComments] ADD  DEFAULT (getdate()) FOR [CommentDate]
GO
ALTER TABLE [dbo].[TaskFiles] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT (getdate()) FOR [AssignedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Manager] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Employees] ([EmployeeID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_Manager]
GO
ALTER TABLE [dbo].[EmployeeFiles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeFiles_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeFiles] CHECK CONSTRAINT [FK_EmployeeFiles_CreatedBy]
GO
ALTER TABLE [dbo].[EmployeeFiles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeFiles_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeFiles] CHECK CONSTRAINT [FK_EmployeeFiles_Employees]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([DepartmentID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Users] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Users]
GO
ALTER TABLE [dbo].[ProjectFiles]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFiles_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[ProjectFiles] CHECK CONSTRAINT [FK_ProjectFiles_CreatedBy]
GO
ALTER TABLE [dbo].[ProjectFiles]  WITH CHECK ADD  CONSTRAINT [FK_ProjectFiles_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([ProjectID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectFiles] CHECK CONSTRAINT [FK_ProjectFiles_Projects]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Employees] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Employees]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ManagerBy] FOREIGN KEY([ManagerBy])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ManagerBy]
GO
ALTER TABLE [dbo].[Subtasks]  WITH CHECK ADD  CONSTRAINT [FK_Subtasks_Employees] FOREIGN KEY([AssignedTo])
REFERENCES [dbo].[Employees] ([EmployeeID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Subtasks] CHECK CONSTRAINT [FK_Subtasks_Employees]
GO
ALTER TABLE [dbo].[Subtasks]  WITH CHECK ADD  CONSTRAINT [FK_Subtasks_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Subtasks] CHECK CONSTRAINT [FK_Subtasks_Tasks]
GO
ALTER TABLE [dbo].[TaskAssignments]  WITH CHECK ADD  CONSTRAINT [FK_TaskAssignments_AssignedBy] FOREIGN KEY([AssignedBy])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[TaskAssignments] CHECK CONSTRAINT [FK_TaskAssignments_AssignedBy]
GO
ALTER TABLE [dbo].[TaskAssignments]  WITH CHECK ADD  CONSTRAINT [FK_TaskAssignments_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskAssignments] CHECK CONSTRAINT [FK_TaskAssignments_Employees]
GO
ALTER TABLE [dbo].[TaskAssignments]  WITH CHECK ADD  CONSTRAINT [FK_TaskAssignments_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskAssignments] CHECK CONSTRAINT [FK_TaskAssignments_Tasks]
GO
ALTER TABLE [dbo].[TaskComments]  WITH CHECK ADD  CONSTRAINT [FK_TaskComments_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[TaskComments] CHECK CONSTRAINT [FK_TaskComments_Employees]
GO
ALTER TABLE [dbo].[TaskComments]  WITH CHECK ADD  CONSTRAINT [FK_TaskComments_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskComments] CHECK CONSTRAINT [FK_TaskComments_Tasks]
GO
ALTER TABLE [dbo].[TaskFiles]  WITH CHECK ADD  CONSTRAINT [FK_TaskFiles_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[TaskFiles] CHECK CONSTRAINT [FK_TaskFiles_CreatedBy]
GO
ALTER TABLE [dbo].[TaskFiles]  WITH CHECK ADD  CONSTRAINT [FK_TaskFiles_Tasks] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskFiles] CHECK CONSTRAINT [FK_TaskFiles_Tasks]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Employees_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Employees_Created]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([ProjectID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projects]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [CHK_Project_Dates] CHECK  (([EndDate] IS NULL OR [EndDate]>=[StartDate]))
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [CHK_Project_Dates]
GO
ALTER TABLE [dbo].[Subtasks]  WITH CHECK ADD CHECK  (([Progress]>=(0) AND [Progress]<=(100)))
GO
ALTER TABLE [dbo].[Subtasks]  WITH CHECK ADD CHECK  (([Status]='Done' OR [Status]='InProgress' OR [Status]='Todo'))
GO
ALTER TABLE [dbo].[TaskAssignments]  WITH CHECK ADD CHECK  (([CompletionStatus]=N'Completed' OR [CompletionStatus]=N'In Progress' OR [CompletionStatus]=N'Pending'))
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD CHECK  (([Role]=N'Quản lý dự án' OR [Role]=N'Nhân viên' OR [Role]=N'Quản lý phòng ban' OR [Role]='Admin'))
GO

select * from UserRoles
join UserRoles ur on ur.UserID = Users.UserID