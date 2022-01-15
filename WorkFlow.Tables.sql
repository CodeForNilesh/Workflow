USE [WorkFlow1]
GO
/****** Object:  Table [dbo].[WorkflowStage]    Script Date: 15-01-2022 12:07:23 ******/
DROP TABLE IF EXISTS [dbo].[WorkflowStage]
GO
/****** Object:  Table [dbo].[Workflow]    Script Date: 15-01-2022 12:07:23 ******/
DROP TABLE IF EXISTS [dbo].[Workflow]
GO
/****** Object:  Table [dbo].[StageReviewers]    Script Date: 15-01-2022 12:07:23 ******/
DROP TABLE IF EXISTS [dbo].[StageReviewers]
GO
/****** Object:  Table [dbo].[StageEscalators]    Script Date: 15-01-2022 12:07:23 ******/
DROP TABLE IF EXISTS [dbo].[StageEscalators]
GO
/****** Object:  Table [dbo].[Stage]    Script Date: 15-01-2022 12:07:23 ******/
DROP TABLE IF EXISTS [dbo].[Stage]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 15-01-2022 12:07:23 ******/
DROP TABLE IF EXISTS [dbo].[Persons]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 15-01-2022 12:07:23 ******/
DROP TABLE IF EXISTS [dbo].[Department]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 15-01-2022 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] NOT NULL,
	[DepartmentName] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[State] [varchar](255) NULL,
	[District] [varchar](255) NULL,
	[Pincode] [varchar](6) NULL,
	[Latitude] [varchar](255) NULL,
	[Longitude] [varchar](255) NULL,
	[DeptOwner] [int] NULL,
	[ContactDetails] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 15-01-2022 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[UserID] [int] NOT NULL,
	[FirstName] [varchar](255) NULL,
	[MiddleName] [varchar](255) NULL,
	[LastName] [varchar](255) NULL,
	[Email] [varchar](100) NULL,
	[Mobile] [varchar](15) NULL,
	[City] [varchar](255) NULL,
	[State] [varchar](255) NULL,
	[District] [varchar](255) NULL,
	[Pincode] [varchar](6) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage]    Script Date: 15-01-2022 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage](
	[StageID] [int] NOT NULL,
	[Title] [varchar](255) NULL,
	[Description] [varchar](255) NULL,
	[WorkflowID] [int] NULL,
	[ReviewTime] [date] NULL,
	[EscalationTime] [date] NULL,
	[IsAnyApprover] [bit] NULL,
	[StageOrder] [int] NULL,
	[IsStageApprovalMandetory] [bit] NULL,
	[ActionCommentMandetory] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[StageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StageEscalators]    Script Date: 15-01-2022 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StageEscalators](
	[StageEscalatorID] [int] NOT NULL,
	[StageID] [int] NULL,
	[EscalatorID] [int] NULL,
	[DepartmentID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StageEscalatorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StageReviewers]    Script Date: 15-01-2022 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StageReviewers](
	[StageReviewerID] [int] NOT NULL,
	[StageID] [int] NULL,
	[ReviewerID] [int] NULL,
	[DepartmentID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StageReviewerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workflow]    Script Date: 15-01-2022 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workflow](
	[WorkflowID] [int] NOT NULL,
	[Title] [varchar](255) NULL,
	[Details] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkflowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkflowStage]    Script Date: 15-01-2022 12:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkflowStage](
	[WorkflowStageID] [int] NOT NULL,
	[WorkflowID] [int] NULL,
	[StageID] [int] NULL,
	[StageOrder] [int] NULL,
	[IsStageApprovalMandetory] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkflowStageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
