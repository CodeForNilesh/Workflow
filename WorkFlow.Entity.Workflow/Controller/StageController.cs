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
            string sql = "INSERT INTO Stage (Title, Description, WorkflowID, ReviewTime, EscalationTime, IsAnyApprover, StageOrder, IsStageApprovalMandetory, ActionCommentMandetory) VALUES " +
                         "(@Title, @Description, @WorkflowID, @ReviewTime, @EscalationTime, @IsAnyApprover, @StageOrder, @IsStageApprovalMandetory, @ActionCommentMandetory)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@Title",                         Title, DbType.String),
                dbMAnager.CreateParameter("@Description",                   Description, DbType.String),
                dbMAnager.CreateParameter("@WorkflowID",                    WorkflowID, DbType.Int32),
                dbMAnager.CreateParameter("@ReviewTime",                    ReviewTime, DbType.DateTime),
                dbMAnager.CreateParameter("@EscalationTime",                EscalationTime, DbType.DateTime),
                dbMAnager.CreateParameter("@IsAnyApprover",                 IsAnyApprover, DbType.Boolean),
                dbMAnager.CreateParameter("@StageOrder",                    StageOrder, DbType.Int32),
                dbMAnager.CreateParameter("@IsStageApprovalMandetory",      IsStageApprovalMandetory, DbType.Boolean),
                dbMAnager.CreateParameter("@ActionCommentMandetory",        ActionCommentMandetory, DbType.Boolean)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }

        public void UpdateStage(int StageID, string Title, string Description, int WorkflowID, DateTime ReviewTime, DateTime EscalationTime, bool IsAnyApprover, int StageOrder,
            bool IsStageApprovalMandetory, bool ActionCommentMandetory)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@StageID",                       StageID, DbType.Int32),
                dbMAnager.CreateParameter("@Title",                         Title, DbType.String),
                dbMAnager.CreateParameter("@Description",                   Description, DbType.String),
                dbMAnager.CreateParameter("@WorkflowID",                    WorkflowID, DbType.Int32),
                dbMAnager.CreateParameter("@ReviewTime",                    ReviewTime, DbType.DateTime),
                dbMAnager.CreateParameter("@EscalationTime",                EscalationTime, DbType.DateTime),
                dbMAnager.CreateParameter("@IsAnyApprover",                 IsAnyApprover, DbType.Boolean),
                dbMAnager.CreateParameter("@StageOrder",                    StageOrder, DbType.Int32),
                dbMAnager.CreateParameter("@IsStageApprovalMandetory",      IsStageApprovalMandetory, DbType.Boolean),
                dbMAnager.CreateParameter("@ActionCommentMandetory",        ActionCommentMandetory, DbType.Boolean)
            };
            dbMAnager.Update("usp_UpdateStage", CommandType.StoredProcedure, parameters);
        }
    }
}
