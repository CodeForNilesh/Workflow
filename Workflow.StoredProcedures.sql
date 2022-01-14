



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
