USE [WorkFlow1]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetEntireWorkflow]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_GetWorkflowMeta]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdateDepartment]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdatePerson]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdateStage]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdateStageEscalators]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdateStageReviewers]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdateWorkflow]    Script Date: 19-01-2022 14:11:03 ******/
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
/****** Object:  StoredProcedure [dbo].[usp_UpdateWorkflowStage]    Script Date: 19-01-2022 14:11:03 ******/
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
