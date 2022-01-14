using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorkFlow.DAL;
using WorkFlow.WorkflowManagement.Entities;

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
        //public List<StageReviewers> GetStageReviewers(int StageReviewerID, int StageID, int ReviewerID, int DepartmentID, bool ActionCommentMandetory)
        //{
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@StageReviewerID",           StageReviewerID, DbType.Int32),
        //        dbMAnager.CreateParameter("@StageID",                   StageID, DbType.Int32),
        //        dbMAnager.CreateParameter("@ReviewerID",                ReviewerID, DbType.Int32),
        //        dbMAnager.CreateParameter("@DepartmentID",              DepartmentID, DbType.Int32),
        //        dbMAnager.CreateParameter("@ActionCommentMandetory",    ActionCommentMandetory, DbType.Boolean)
        //    };

        //    DataTable dt = dbMAnager.GetDataTable("usp_GetStageReviewers", CommandType.StoredProcedure, parameters);
        //    List<StageReviewers> stageReviewers = new List<StageReviewers>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        stageReviewers = (from c in dt.AsEnumerable()
        //                  select new StageReviewers
        //                  {
        //                      StageReviewerID = c.Field<int>("StageReviewerID"),
        //                      StageID = c.Field<int>("StageID"),
        //                      ReviewerID = c.Field<int>("ReviewerID"),
        //                      DepartmentID = c.Field<int>("DepartmentID"),
        //                      ActionCommentMandetory = c.Field<Boolean>("ActionCommentMandetory")
        //                  }).ToList();
        //    }
        //    return stageReviewers;
        //}

        //public void UpdateStageReviewers(int StageReviewerID, int StageID, int ReviewerID, int DepartmentID, bool ActionCommentMandetory)
        //{
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@StageReviewerID",           StageReviewerID, DbType.Int32),
        //        dbMAnager.CreateParameter("@StageID",                   StageID, DbType.Int32),
        //        dbMAnager.CreateParameter("@ReviewerID",                ReviewerID, DbType.Int32),
        //        dbMAnager.CreateParameter("@DepartmentID",              DepartmentID, DbType.Int32),
        //        dbMAnager.CreateParameter("@ActionCommentMandetory",    ActionCommentMandetory, DbType.Boolean)
        //    };
        //    dbMAnager.Update("usp_UpdateStageReviewers", CommandType.StoredProcedure, parameters);
        //}
    }
}
