using System.Data;
using WorkFlow.DAL;
namespace WorkFlow.WorkflowManagement.Controller
{
    public class StageReviewersController
    {
        DBManager dbMAnager;
        public StageReviewersController()
        {
            dbMAnager = new DBManager("sqldbConnection");
        }

        public void AddStageReviewers(int StageID, int ReviewerID, int DepartmentID, bool ActionCommentMandetory)
        {
            string sql = "INSERT INTO StageReviewers (StageID, ReviewerID, DepartmentID, ActionCommentMandetory) VALUES (@StageID, @ReviewerID, @DepartmentID, @ActionCommentMandetory)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@StageID",                   StageID, DbType.Int32),
                dbMAnager.CreateParameter("@ReviewerID",                ReviewerID, DbType.Int32),
                dbMAnager.CreateParameter("@DepartmentID",              DepartmentID, DbType.Int32),
                dbMAnager.CreateParameter("@ActionCommentMandetory",    ActionCommentMandetory, DbType.Boolean)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }

        public void UpdateStageReviewers(int StageReviewerID, int StageID, int ReviewerID, int DepartmentID)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@StageReviewerID",           StageReviewerID, DbType.Int32),
                dbMAnager.CreateParameter("@StageID",                   StageID, DbType.Int32),
                dbMAnager.CreateParameter("@ReviewerID",                ReviewerID, DbType.Int32),
                dbMAnager.CreateParameter("@DepartmentID",              DepartmentID, DbType.Int32)            
            };
            dbMAnager.Update("usp_UpdateStageReviewers", CommandType.StoredProcedure, parameters);
        }
    }
}
