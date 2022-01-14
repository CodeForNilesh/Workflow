USE [WorkFlow1]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 15-01-2022 01:52:51 ******/
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
/****** Object:  Table [dbo].[Persons]    Script Date: 15-01-2022 01:52:51 ******/
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
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage]    Script Date: 15-01-2022 01:52:51 ******/
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
PRIMARY KEY CLUSTERED 
(
	[StageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StageEscalators]    Script Date: 15-01-2022 01:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StageEscalators](
	[StageEscalatorID] [int] NOT NULL,
	[StageID] [int] NULL,
	[EscalatorID] [int] NULL,
	[DepartmentID] [int] NULL,
	[ActionCommentMandetory] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[StageEscalatorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StageReviewers]    Script Date: 15-01-2022 01:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StageReviewers](
	[StageReviewerID] [int] NOT NULL,
	[StageID] [int] NULL,
	[ReviewerID] [int] NULL,
	[DepartmentID] [int] NULL,
	[ActionCommentMandetory] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[StageReviewerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workflow]    Script Date: 15-01-2022 01:52:51 ******/
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
/****** Object:  Table [dbo].[WorkflowStage]    Script Date: 15-01-2022 01:52:51 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_GetEntireWorkflow]    Script Date: 15-01-2022 01:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_GetEntireWorkflow]
(
@WorkflowID int
)
AS
BEGIN
    SET NOCOUNT ON
	
	SELECT * FROM WORKFLOW WHERE WorkflowID = @WorkflowID

	SELECT * FROM STAGE WHERE WorkflowID = @WorkflowID

	SELECT * FROM STAGE ST LEFT JOIN STAGEREVIEWERS SR ON ST.StageID = SR.StageID
	LEFT JOIN PERSONS PS ON SR.ReviewerID = PS.UserID 
	WHERE ST.WorkflowID = @WorkflowID

	SELECT * FROM STAGE ST LEFT JOIN STAGEREVIEWERS SR ON ST.StageID = SR.StageID
	LEFT JOIN DEPARTMENT DT ON SR.DepartmentID = DT.DepartmentID 
	WHERE ST.WorkflowID = @WorkflowID

	SELECT * FROM STAGE ST LEFT JOIN STAGEESCALATORS SE ON ST.StageID = SE.StageID
	LEFT JOIN PERSONS PS ON SE.EscalatorID = PS.UserID 
	WHERE ST.WorkflowID = @WorkflowID

	SELECT * FROM STAGE ST LEFT JOIN STAGEESCALATORS SE ON ST.StageID = SE.StageID
	LEFT JOIN DEPARTMENT DT ON SE.DepartmentID = DT.DepartmentID 
	WHERE ST.WorkflowID = @WorkflowID

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetWorkflowMeta]    Script Date: 15-01-2022 01:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetWorkflowMeta]
(
@WorkflowID int
)
AS
BEGIN
    SET NOCOUNT ON
	SELECT * FROM Workflow WHERE (@WorkflowID IS NULL OR WorkflowID = @WorkflowID)
END
GO
