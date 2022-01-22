USE [WorkFlow1]
GO
/****** Object:  StoredProcedure [dbo].[usp_ApproveStage]    Script Date: 21-01-2022 09:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ApproveStage]
@RequestStageID int,
@ReviewerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE RequestStageReviewer 
	SET
		Approval = 'APPROVED'
	WHERE RequestStageID = @RequestStageID AND ReviewerID = @ReviewerID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateWorkflowRequest]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_CreateWorkflowRequest]
@WorkflowID int,
@Desciption VARCHAR(500),
@WorkflowInstance VARCHAR(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @RequestID as int;
	DECLARE @RequestStageID as int;
	DECLARE @ReviewTime as int;
	
	SET @RequestID  = 0;

	INSERT INTO RequestWorkflow (WorkflowID, Desciption, WorkflowInstance) VALUES (@WorkflowID, @Desciption, @WorkflowInstance);
	SET @RequestID = @@IDENTITY;

	IF @RequestID <> 0
	BEGIN
		CREATE TABLE #CurrentRequestStage(RequestWorkflow INT, StageID INT, StageOrder INT, ReviewTime INT, IsAnyApprover BIT, IsStageApprovalMandetory BIT, ActionCommentMandetory BIT);

		INSERT INTO #CurrentRequestStage 
		SELECT @RequestID, StageID, StageOrder, ReviewTime, IsAnyApprover, IsStageApprovalMandetory, ActionCommentMandetory FROM STAGE 
		WHERE StageOrder = 1 AND WorkflowID = @WorkflowID

		INSERT INTO RequestStage (RequestID, StageID, StageOrder, IsAnyApprover, IsStageApprovalMandetory, ActionCommentMandetory)
		SELECT @RequestID, StageID, StageOrder, IsAnyApprover, IsStageApprovalMandetory, ActionCommentMandetory FROM #CurrentRequestStage
		WHERE StageOrder = 1
	
		SELECT @ReviewTime = ReviewTime FROM #CurrentRequestStage

		SET @RequestStageID = @@IDENTITY;

		UPDATE RequestWorkflow
		SET CurrentStageID = @RequestStageID
		WHERE RequestID = @RequestID

		CREATE TABLE #RequestStageReviewer (RequestStageID int, ReviewerID int, ReviewTime Date, Approval bit, Comment VARCHAR(255), CreatedOn Date)

		INSERT INTO RequestStageReviewer (RequestStageID, ReviewerID, ReviewTime, Approval, Comment, CreatedOn)
		SELECT @RequestStageID, ReviewerID, @ReviewTime, 'PENDING', '', GETDATE() FROM StageReviewers
		WHERE StageID in (SELECT StageID from Stage WHERE WorkflowID = @WorkflowID)
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetEntireWorkflow]    Script Date: 21-01-2022 09:06:58 ******/
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
	
	SELECT WorkflowID, Title, Details FROM WORKFLOW WHERE WorkflowID = @WorkflowID

	SELECT StageID, Title, Description, ActionCommentMandetory, ReviewTime, EscalationTime, IsAnyApprover FROM STAGE WHERE WorkflowID = @WorkflowID

	SELECT PS.UserID, PS.FirstName, PS.MiddleName, PS.LastName, PS.Email, PS.Mobile, PS.City, PS.[State], PS.District, PS.Pincode FROM STAGE ST INNER JOIN STAGEREVIEWERS SR ON ST.StageID = SR.StageID
	INNER JOIN PERSONS PS ON SR.ReviewerID = PS.UserID 
	WHERE ST.WorkflowID = @WorkflowID AND PS.IsActive = 1

	SELECT DT.DepartmentID, DT.DepartmentName, DT.City, DT.[State], DT.District, DT.Pincode, DT.Latitude, DT.Longitude, DT.DeptOwner, DT.ContactDetails FROM STAGE ST INNER JOIN STAGEREVIEWERS SR ON ST.StageID = SR.StageID
	INNER JOIN DEPARTMENT DT ON SR.DepartmentID = DT.DepartmentID 
	WHERE ST.WorkflowID = @WorkflowID

	SELECT PS.UserID, PS.FirstName, PS.MiddleName, PS.LastName, PS.Email, PS.Mobile, PS.City, PS.[State], PS.District, PS.Pincode FROM STAGE ST INNER JOIN STAGEESCALATORS SE ON ST.StageID = SE.StageID
	INNER JOIN PERSONS PS ON SE.EscalatorID = PS.UserID 
	WHERE ST.WorkflowID = @WorkflowID AND PS.IsActive = 1

	SELECT DT.DepartmentID, DT.DepartmentName, DT.City, DT.[State], DT.District, DT.Pincode, DT.Latitude, DT.Longitude, DT.DeptOwner, DT.ContactDetails FROM STAGE ST INNER JOIN STAGEESCALATORS SE ON ST.StageID = SE.StageID
	INNER JOIN DEPARTMENT DT ON SE.DepartmentID = DT.DepartmentID 
	WHERE ST.WorkflowID = @WorkflowID

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRequests]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetRequests] 
@ReviewerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- WorkflowRequest
	SELECT RW.WorkflowID, RW.RequestID, RW.CurrentStageID, RW.Desciption, RW.WorkflowInstance FROM RequestStage RS LEFT OUTER JOIN RequestStageReviewer RSR ON RS.RequestStageID = RSR.RequestStageID
	INNER JOIN RequestWorkflow RW ON RW.RequestID = RS.RequestID
	WHERE RSR.ReviewerID = @ReviewerID

	-- Stage
	--SELECT RS.RequestStageID, RS.RequestID, RS.StageID, RS.StageOrder, RS.IsStageApprovalMandetory, RS.IsAnyApprover, RS.ActionCommentMandetory FROM RequestStage RS LEFT OUTER JOIN RequestStageReviewer RSR ON RS.RequestStageID = RSR.RequestStageID
	--INNER JOIN RequestWorkflow RW ON RW.RequestID = RS.RequestID
	--WHERE RSR.ReviewerID = @ReviewerID

	-- Reviewer
	--SELECT RSR.ReviewerID, RSR.RequestStageID, RSR.RequestStageReviewerID, RSR.Approval, RSR.Comment, RSR.ReviewTime FROM RequestStage RS LEFT OUTER JOIN RequestStageReviewer RSR ON RS.RequestStageID = RSR.RequestStageID
	--INNER JOIN RequestWorkflow RW ON RW.RequestID = RS.RequestID
	--WHERE RSR.ReviewerID = @ReviewerID

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetWorkflowMeta]    Script Date: 21-01-2022 09:06:58 ******/
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
	SELECT WorkflowID, Title, Details FROM Workflow WHERE (@WorkflowID IS NULL OR WorkflowID = @WorkflowID)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetWorkflowStage]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetWorkflowStage]
(
@WorkflowStageID int,
@WorkflowID int,
@StageID int,
@StageOrder int,
@IsStageApprovalMandetory BIT
)
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @SQLQuery AS NVARCHAR(4000)  
	DECLARE @ParamDefinition AS NVARCHAR(2000) 

	Set @SQLQuery ='SELECT * FROM WorkflowStage WHERE true ' 
	IF (@WorkflowStageID Is Not Null) and (@WorkflowStageID <> 0)
		SET @SQLQuery = 'AND WorkflowStageID = @WorkflowStageID '

	IF (@WorkflowID Is Not Null) and (@WorkflowID <> 0)
		SET @SQLQuery = @SQLQuery + ' AND WorkflowID = @WorkflowID'

	IF (@StageID Is Not Null) and (@StageID <> 0)
		SET @SQLQuery = @SQLQuery + ' AND StageID = @StageID'

	IF (@StageOrder Is Not Null) and (@StageOrder <> 0)
		SET @SQLQuery = @SQLQuery + ' AND StageOrder = @StageOrder'		

	IF (@IsStageApprovalMandetory Is Not Null)
		SET @SQLQuery = @SQLQuery + ' AND IsStageApprovalMandetory = @IsStageApprovalMandetory'	
		
	SET @ParamDefinition = '@WorkflowStageID int, @WorkflowID int, @StageID int, @StageOrder int, @IsStageApprovalMandetory BIT'

	EXECUTE sp_Executesql @SQLQuery, @ParamDefinition, @WorkflowStageID, @WorkflowID, @StageID, @StageOrder, @IsStageApprovalMandetory

END
GO
/****** Object:  StoredProcedure [dbo].[usp_RejectStage]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RejectStage]
@RequestStageID int,
@ReviewerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE RequestStageReviewer 
	SET
		Approval = 'REJECTED'
	WHERE RequestStageID = @RequestStageID AND ReviewerID = @ReviewerID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_RouteStage]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RouteStage]
@RequestID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @WorkflowID as int;
	DECLARE @CurrentRequestStageOrder as int;
	DECLARE @MaxStageOrder as int;
	DECLARE @RequestStageID as int;
	DECLARE @ReviewTime as int;
	
	SET @WorkflowID  = 0;

	
	--Check if it was last stage
	SELECT @CurrentRequestStageOrder = MAX(StageOrder) from RequestStage WHERE RequestID = @RequestID
		
	SELECT @MaxStageOrder = MAX(S.StageOrder) from Stage S INNER JOIN RequestWorkflow RW ON S.WorkflowID = RW.WorkflowID
	WHERE RW.RequestID = @RequestID

	--if Current Request Stage is not last stage for workflow
	IF @CurrentRequestStageOrder < @MaxStageOrder
	BEGIN
		
		CREATE TABLE #CurrentRequestStage(RequestWorkflow INT, StageID INT, StageOrder INT, ReviewTime INT, IsAnyApprover BIT, IsStageApprovalMandetory BIT, ActionCommentMandetory BIT);

		INSERT INTO #CurrentRequestStage 
		SELECT @RequestID, StageID, StageOrder, ReviewTime, IsAnyApprover, IsStageApprovalMandetory, ActionCommentMandetory FROM STAGE 
		WHERE WorkflowID = @WorkflowID AND StageOrder = @CurrentRequestStageOrder + 1

		INSERT INTO RequestStage (RequestID, StageID, StageOrder, IsAnyApprover, IsStageApprovalMandetory, ActionCommentMandetory)
		SELECT @RequestID, StageID, StageOrder, IsAnyApprover, IsStageApprovalMandetory, ActionCommentMandetory FROM #CurrentRequestStage
	
		SELECT @ReviewTime = ReviewTime FROM #CurrentRequestStage

		SET @RequestStageID = @@IDENTITY;

		UPDATE RequestWorkflow 
		SET CurrentStageID = @RequestStageID
		WHERE RequestID = @RequestID

		CREATE TABLE #RequestStageReviewer (RequestStageID int, ReviewerID int, ReviewTime Date, Approval bit, Comment VARCHAR(255), CreatedOn Date)

		INSERT INTO RequestStageReviewer (RequestStageID, ReviewerID, ReviewTime, Approval, Comment, CreatedOn)
		SELECT @RequestStageID, ReviewerID, @ReviewTime, 'PENDING', '', GETDATE() FROM StageReviewers
		WHERE StageID in (SELECT StageID from Stage WHERE WorkflowID = @WorkflowID)
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateDepartment]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateDepartment]
(
@DepartmentID int,
@ParentDepartmentID int,
@DepartmentName VARCHAR(255),
@City VARCHAR(255),
@State VARCHAR(255),
@District VARCHAR(255),
@Pincode VARCHAR(6),
@Latitude VARCHAR(255),
@Longitude VARCHAR(255),
@DeptOwner int,
@ContactDetails VARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON
	UPDATE DEPARTMENT 
	SET ParentDepartmentID = IsNull(@ParentDepartmentID, ParentDepartmentID),
	DepartmentName = IsNull(@DepartmentName, DepartmentName),
	City = IsNull(@City, City),
	State = IsNull(@State, State),
	District = IsNull(@District, District),
	Pincode = IsNull(@Pincode, Pincode),
	Latitude = IsNull(@Latitude, Latitude),
	Longitude = IsNull(@Longitude, Longitude),
	DeptOwner = IsNull(@DeptOwner, DeptOwner),
	ContactDetails = IsNull(@ContactDetails, ContactDetails)
	WHERE DEPARTMENTID = @DepartmentID
END

GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePerson]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdatePerson]
(
@UserID int,
@FirstName VARCHAR(255), 
@MiddleName VARCHAR(255),
@LastName VARCHAR(255),
@Email VARCHAR(100),
@Mobile VARCHAR(15),
@City VARCHAR(255),
@State VARCHAR(255),
@District VARCHAR(255),
@Pincode VARCHAR(6)
)
AS
BEGIN
    SET NOCOUNT ON
	UPDATE PERSONS
		SET FirstName = ISNULL(@FirstName, FirstName),
		MiddleName = ISNULL(@MiddleName, MiddleName),
		LastName = ISNULL(@LastName, LastName),
		Email = ISNULL(@Email, Email),
		Mobile = ISNULL(@Mobile, Mobile),
		City = ISNULL(@City, City),
		State = ISNULL(@State, State),
		District = ISNULL(@District, District),
		Pincode = ISNULL(@Pincode, Pincode)
	WHERE UserID = @UserID
END


GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateStage]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateStage]
(
@StageID int,
@Title VARCHAR(255),
@Description VARCHAR(255),
@WorkflowID int,
@ReviewTime DATE,
@EscalationTime DATE,
@IsAnyApprover BIT,
@StageOrder int,
@IsStageApprovalMandetory bit,
@ActionCommentMandetory bit
)
AS
BEGIN
    SET NOCOUNT ON
	UPDATE STAGE
		SET Title = ISNULL(@Title, Title),
			Description = ISNULL(@Description, Description),
			WorkflowID = ISNULL(@WorkflowID, WorkflowID),
			ReviewTime = ISNULL(@ReviewTime, ReviewTime),
			EscalationTime = ISNULL(@EscalationTime, EscalationTime),
			IsAnyApprover = ISNULL(@IsAnyApprover, IsAnyApprover),
			StageOrder = ISNULL(@StageOrder, StageOrder),
			IsStageApprovalMandetory = ISNULL(@IsStageApprovalMandetory, IsStageApprovalMandetory),
			ActionCommentMandetory = ISNULL(@ActionCommentMandetory, ActionCommentMandetory)
	WHERE StageID = @StageID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateStageEscalators]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateStageEscalators]
(
@StageEscalatorID int,
@StageID int,
@EscalatorID int,
@DepartmentID int
)
AS
BEGIN
    SET NOCOUNT ON
	UPDATE StageEscalators
	SET
		StageID = ISNULL(@StageID, StageID),
		EscalatorID = ISNULL(@EscalatorID, EscalatorID),
		DepartmentID = ISNULL(@DepartmentID, DepartmentID)
	WHERE @StageEscalatorID = StageEscalatorID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateStageReviewers]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateStageReviewers]
(
@StageReviewerID int,
@StageID int,
@ReviewerID int,
@DepartmentID int,
@ActionCommentMandetory BIT
)
AS
BEGIN
    SET NOCOUNT ON
	UPDATE StageReviewers
	SET
		StageID = ISNULL(@StageID, StageID),
		ReviewerID = ISNULL(@ReviewerID, ReviewerID),
		DepartmentID = ISNULL(@DepartmentID, DepartmentID)
	WHERE StageReviewerID = @StageReviewerID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateWorkflow]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateWorkflow]
(
@WorkflowID int,
@Title VARCHAR(255),
@Details VARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON
	UPDATE Workflow
	SET
		Title = ISNULL(@Title, Title),
		Details = ISNULL(@Details, Details)
	WHERE WorkflowID = @WorkflowID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateWorkflowStage]    Script Date: 21-01-2022 09:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateWorkflowStage]
(
@WorkflowStageID int,
@WorkflowID int,
@StageID int,
@StageOrder int,
@IsStageApprovalMandetory BIT
)
AS
BEGIN
    SET NOCOUNT ON
	UPDATE WorkflowStage
	SET
		WorkflowID = ISNULL(@WorkflowID, WorkflowID),
		StageID = ISNULL(@StageID, StageID),
		StageOrder = ISNULL(@StageOrder, StageOrder),
		IsStageApprovalMandetory = ISNULL(@IsStageApprovalMandetory, IsStageApprovalMandetory)
	WHERE WorkflowStageID = @WorkflowStageID
END
GO
