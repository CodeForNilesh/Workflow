USE [WorkFlow1]
GO
ALTER TABLE [dbo].[RequestWorkflow] DROP CONSTRAINT [DF__RequestWo__Reque__6CD828CA]
GO
ALTER TABLE [dbo].[RequestStageReviewer] DROP CONSTRAINT [DF__RequestSt__Appro__72910220]
GO
ALTER TABLE [dbo].[RequestStageEscalator] DROP CONSTRAINT [DF__RequestSt__Appro__756D6ECB]
GO
ALTER TABLE [dbo].[RequestStage] DROP CONSTRAINT [DF__RequestSt__Reque__6FB49575]
GO
/****** Object:  Table [dbo].[WorkflowStage]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowStage]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowStage]
GO
/****** Object:  Table [dbo].[Workflow]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Workflow]') AND type in (N'U'))
DROP TABLE [dbo].[Workflow]
GO
/****** Object:  Table [dbo].[StageReviewers]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StageReviewers]') AND type in (N'U'))
DROP TABLE [dbo].[StageReviewers]
GO
/****** Object:  Table [dbo].[StageEscalators]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StageEscalators]') AND type in (N'U'))
DROP TABLE [dbo].[StageEscalators]
GO
/****** Object:  Table [dbo].[Stage]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stage]') AND type in (N'U'))
DROP TABLE [dbo].[Stage]
GO
/****** Object:  Table [dbo].[RequestWorkflow]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestWorkflow]') AND type in (N'U'))
DROP TABLE [dbo].[RequestWorkflow]
GO
/****** Object:  Table [dbo].[RequestStageReviewer]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestStageReviewer]') AND type in (N'U'))
DROP TABLE [dbo].[RequestStageReviewer]
GO
/****** Object:  Table [dbo].[RequestStageEscalator]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestStageEscalator]') AND type in (N'U'))
DROP TABLE [dbo].[RequestStageEscalator]
GO
/****** Object:  Table [dbo].[RequestStage]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequestStage]') AND type in (N'U'))
DROP TABLE [dbo].[RequestStage]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
DROP TABLE [dbo].[Persons]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 22-01-2022 16:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[ParentDepartmentID] [int] NULL,
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
/****** Object:  Table [dbo].[Persons]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[RequestStage]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestStage](
	[RequestStageID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [int] NOT NULL,
	[StageID] [int] NOT NULL,
	[StageOrder] [int] NOT NULL,
	[ReviewTime] [int] NOT NULL,
	[RequestStageStatus] [int] NULL,
	[IsAnyApprover] [bit] NULL,
	[IsStageApprovalMandetory] [bit] NULL,
	[ActionCommentMandetory] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestStageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestStageEscalator]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestStageEscalator](
	[RequestStageEscalatorID] [int] IDENTITY(1,1) NOT NULL,
	[RequestStageID] [int] NOT NULL,
	[EscalatorID] [int] NOT NULL,
	[DepartmentID] [int] NULL,
	[ApprovalStatus] [int] NULL,
	[Comment] [varchar](255) NULL,
	[CreatedOn] [date] NULL,
	[EscalatedOn] [date] NULL,
	[EscalatorInstance] [varchar](500) NULL,
	[DepartmentInstance] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestStageEscalatorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestStageReviewer]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestStageReviewer](
	[RequestStageReviewerID] [int] IDENTITY(1,1) NOT NULL,
	[RequestStageID] [int] NOT NULL,
	[ReviewerID] [int] NOT NULL,
	[DepartmentID] [int] NULL,
	[ApprovalStatus] [int] NULL,
	[Comment] [varchar](255) NULL,
	[CreatedOn] [date] NULL,
	[ReviewedOn] [date] NULL,
	[ReviewerInstance] [varchar](500) NULL,
	[DepartmentInstance] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestStageReviewerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestWorkflow]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestWorkflow](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[WorkflowID] [int] NOT NULL,
	[CurrentStageID] [int] NULL,
	[Desciption] [varchar](500) NULL,
	[RequestStatus] [int] NULL,
	[WorkflowInstance] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage](
	[StageID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NULL,
	[Description] [varchar](255) NULL,
	[WorkflowID] [int] NULL,
	[ReviewTime] [int] NOT NULL,
	[EscalationTime] [int] NOT NULL,
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
/****** Object:  Table [dbo].[StageEscalators]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StageEscalators](
	[StageEscalatorID] [int] IDENTITY(1,1) NOT NULL,
	[StageID] [int] NULL,
	[EscalatorID] [int] NULL,
	[DepartmentID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StageEscalatorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StageReviewers]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StageReviewers](
	[StageReviewerID] [int] IDENTITY(1,1) NOT NULL,
	[StageID] [int] NULL,
	[ReviewerID] [int] NULL,
	[DepartmentID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StageReviewerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workflow]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workflow](
	[WorkflowID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NULL,
	[Details] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkflowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkflowStage]    Script Date: 22-01-2022 16:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkflowStage](
	[WorkflowStageID] [int] IDENTITY(1,1) NOT NULL,
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
ALTER TABLE [dbo].[RequestStage] ADD  DEFAULT ((0)) FOR [RequestStageStatus]
GO
ALTER TABLE [dbo].[RequestStageEscalator] ADD  DEFAULT ((0)) FOR [ApprovalStatus]
GO
ALTER TABLE [dbo].[RequestStageReviewer] ADD  DEFAULT ((0)) FOR [ApprovalStatus]
GO
ALTER TABLE [dbo].[RequestWorkflow] ADD  DEFAULT ((0)) FOR [RequestStatus]
GO
