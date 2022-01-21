using System.Collections.Generic;
using System.Data;
using System.Linq;
using Workflow.Request.Entities;
using WorkFlow.DAL;

namespace Workflow.Request
{
    public class RequestController
    {
        DBManager dbMAnager;
        public RequestController()
        {
            dbMAnager = new DBManager("sqldbConnection");
        }
        public void CreateWorkflowRequest(int WorkflowID, string Desciption, string WorkflowInstance)
        { 
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@WorkflowID",         WorkflowID, DbType.Int32),
                dbMAnager.CreateParameter("@Desciption",         Desciption, DbType.String),
                dbMAnager.CreateParameter("@WorkflowInstance",   WorkflowInstance, DbType.String)
            };
            dbMAnager.Update("usp_CreateWorkflowRequest", CommandType.StoredProcedure, parameters);
        }

        public List<WorkflowRequest> GetRequests(int ReviewerID)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@ReviewerID",         ReviewerID, DbType.Int32),

            };
            DataTable dt = dbMAnager.GetDataTable("usp_GetRequests", CommandType.StoredProcedure, parameters);

            List<WorkflowRequest> WorkflowRequests = new List<WorkflowRequest>();
            if (dt?.Rows.Count > 0)
            {
                WorkflowRequests = (from c in dt.AsEnumerable()
                              select new WorkflowRequest
                              {
                                  RequestID = c.Field<int>("RequestID"),
                                  WorkflowID = c.Field<int>("WorkflowID"),
                                  CurrentStageID = c.Field<int>("CurrentStageID"),
                                  Desciption = c.Field<string>("Desciption")
                              }).ToList();
            }
            return WorkflowRequests;
        }

        public void ApproveStage(int RequestStageID, int ReviewerID)
        {

            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@RequestStageID",     RequestStageID, DbType.Int32),
                dbMAnager.CreateParameter("@ReviewerID",         ReviewerID, DbType.Int32)
            };
            dbMAnager.Update("usp_ApproveStage", CommandType.StoredProcedure, parameters);
        }
        public void RejectStage(int RequestStageID, int ReviewerID)
        {

            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@RequestStageID",     RequestStageID, DbType.Int32),
                dbMAnager.CreateParameter("@ReviewerID",         ReviewerID, DbType.Int32)
            };
            dbMAnager.Update("usp_RejectStage", CommandType.StoredProcedure, parameters);
        }
    }


}
