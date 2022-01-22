using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorkFlow.DAL;
using WorkFlow.WorkflowManagement.Entities;

namespace WorkFlow.WorkflowManagement.Controller
{
    public class StageController
    {
        DBManager dbMAnager;
        public StageController()
        {
            dbMAnager = new DBManager("sqldbConnection");
        }

        public void AddStage(string Title, string Description, int WorkflowID, DateTime ReviewTime, DateTime EscalationTime, bool IsAnyApprover, int StageOrder,
                        bool IsStageApprovalMandetory, bool ActionCommentMandetory)
        {
            string sql = "INSERT INTO STAGE (TITLE, DESCRIPTION, WORKFLOWID, REVIEWTIME, ESCALATIONTIME, ISANYAPPROVER, STAGEORDER, ISSTAGEAPPROVALMANDETORY, ACTIONCOMMENTMANDETORY) VALUES " +
                         "(@TITLE, @DESCRIPTION, @WORKFLOWID, @REVIEWTIME, @ESCALATIONTIME, @ISANYAPPROVER, @STAGEORDER, @ISSTAGEAPPROVALMANDETORY, @ACTIONCOMMENTMANDETORY)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@TITLE",                         Title, DbType.String),
                dbMAnager.CreateParameter("@DESCRIPTION",                   Description, DbType.String),
                dbMAnager.CreateParameter("@WORKFLOWID",                    WorkflowID, DbType.Int32),
                dbMAnager.CreateParameter("@REVIEWTIME",                    ReviewTime, DbType.DateTime),
                dbMAnager.CreateParameter("@ESCALATIONTIME",                EscalationTime, DbType.DateTime),
                dbMAnager.CreateParameter("@ISANYAPPROVER",                 IsAnyApprover, DbType.Boolean),
                dbMAnager.CreateParameter("@STAGEORDER",                    StageOrder, DbType.Int32),
                dbMAnager.CreateParameter("@ISSTAGEAPPROVALMANDETORY",      IsStageApprovalMandetory, DbType.Boolean),
                dbMAnager.CreateParameter("@ACTIONCOMMENTMANDETORY",        ActionCommentMandetory, DbType.Boolean)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }

        public void UpdateStage(int StageID, string Title, string Description, int WorkflowID, DateTime ReviewTime, DateTime EscalationTime, bool IsAnyApprover, int StageOrder,
            bool IsStageApprovalMandetory, bool ActionCommentMandetory)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@STAGEID",                       StageID, DbType.Int32),
                dbMAnager.CreateParameter("@TITLE",                         Title, DbType.String),
                dbMAnager.CreateParameter("@DESCRIPTION",                   Description, DbType.String),
                dbMAnager.CreateParameter("@WORKFLOWID",                    WorkflowID, DbType.Int32),
                dbMAnager.CreateParameter("@REVIEWTIME",                    ReviewTime, DbType.DateTime),
                dbMAnager.CreateParameter("@ESCALATIONTIME",                EscalationTime, DbType.DateTime),
                dbMAnager.CreateParameter("@ISANYAPPROVER",                 IsAnyApprover, DbType.Boolean),
                dbMAnager.CreateParameter("@STAGEORDER",                    StageOrder, DbType.Int32),
                dbMAnager.CreateParameter("@ISSTAGEAPPROVALMANDETORY",      IsStageApprovalMandetory, DbType.Boolean),
                dbMAnager.CreateParameter("@ACTIONCOMMENTMANDETORY",        ActionCommentMandetory, DbType.Boolean)
            };
            dbMAnager.Update("usp_UpdateStage", CommandType.StoredProcedure, parameters);
        }
    }
}
