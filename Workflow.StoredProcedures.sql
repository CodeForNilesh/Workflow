

/****** Object:  StoredProcedure [dbo].[usp_GetWorkflowMeta]    Script Date: 15-01-2022 12:07:23 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_GetWorkflowMeta]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetEntireWorkflow]    Script Date: 15-01-2022 12:07:23 ******/
DROP PROCEDURE IF EXISTS [dbo].[usp_GetEntireWorkflow]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetEntireWorkflow]    Script Date: 15-01-2022 12:07:23 ******/
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
	WHERE ST.WorkflowID = @WorkflowID

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
/****** Object:  StoredProcedure [dbo].[usp_GetWorkflowMeta]    Script Date: 15-01-2022 12:07:23 ******/
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
