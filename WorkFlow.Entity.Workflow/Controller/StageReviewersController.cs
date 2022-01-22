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
            string sql = "INSERT INTO StageReviewers (StageID, ReviewerID, DepartmentID, ActionCommentMandetory) VALUES (@STAGEID, @REVIEWERID, @DEPARTMENTID, @ACTIONCOMMENTMANDETORY)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@STAGEID",                   StageID, DbType.Int32),
                dbMAnager.CreateParameter("@REVIEWERID",                ReviewerID, DbType.Int32),
                dbMAnager.CreateParameter("@DEPARTMENTID",              DepartmentID, DbType.Int32),
                dbMAnager.CreateParameter("@ACTIONCOMMENTMANDETORY",    ActionCommentMandetory, DbType.Boolean)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }

        public void UpdateStageReviewers(int StageReviewerID, int StageID, int ReviewerID, int DepartmentID)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@STAGEREVIEWERID",           StageReviewerID, DbType.Int32),
                dbMAnager.CreateParameter("@STAGEID",                   StageID, DbType.Int32),
                dbMAnager.CreateParameter("@REVIEWERID",                ReviewerID, DbType.Int32),
                dbMAnager.CreateParameter("@DEPARTMENTID",              DepartmentID, DbType.Int32)            
            };
            dbMAnager.Update("usp_UpdateStageReviewers", CommandType.StoredProcedure, parameters);
        }
    }
}
