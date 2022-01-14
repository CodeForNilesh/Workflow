using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorkFlow.DAL;
using WorkFlow.WorkflowManagement.Entities;

namespace WorkFlow.WorkflowManagement.Controller
{
    public class StageEscalatorsController
    {
        DBManager dbMAnager;
        public StageEscalatorsController()
        {
            dbMAnager = new DBManager("sqldbConnection");
        }

        public void AddStageEscalators(int StageID, int EscalatorID, int DepartmentID, bool ActionCommentMandetory)
        {
            string sql = "INSERT INTO StageEscalators (StageID, EscalatorID, DepartmentID, ActionCommentMandetory) VALUES (@StageID, @EscalatorID, @DepartmentID, @ActionCommentMandetory)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@StageID",                   StageID, DbType.Int32),
                dbMAnager.CreateParameter("@EscalatorID",               EscalatorID, DbType.Int32),
                dbMAnager.CreateParameter("@DepartmentID",              DepartmentID, DbType.Int32),
                dbMAnager.CreateParameter("@ActionCommentMandetory",    ActionCommentMandetory, DbType.Boolean)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }
        //public List<StageEscalators> GetStageEscalators(int StageEscalatorID, int StageID, int EscalatorID, int DepartmentID, bool ActionCommentMandetory)
        //{
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@StageEscalatorID",          StageEscalatorID, DbType.Int32),
        //        dbMAnager.CreateParameter("@StageID",                   StageID, DbType.Int32),
        //        dbMAnager.CreateParameter("@EscalatorID",               EscalatorID, DbType.Int32),
        //        dbMAnager.CreateParameter("@DepartmentID",              DepartmentID, DbType.Int32),
        //        dbMAnager.CreateParameter("@ActionCommentMandetory",    ActionCommentMandetory, DbType.Boolean)
        //    };

        //    DataTable dt = dbMAnager.GetDataTable("usp_GetStageEscalators", CommandType.StoredProcedure, parameters);
        //    List<StageEscalators> stageEscalators = new List<StageEscalators>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        stageEscalators = (from c in dt.AsEnumerable()
        //                          select new StageEscalators
        //                          {
        //                              StageEscalatorID = c.Field<int>("StageEscalatorID"),
        //                              StageID = c.Field<int>("StageID"),
        //                              EscalatorID = c.Field<int>("EscalatorID"),
        //                              DepartmentID = c.Field<int>("DepartmentID"),
        //                              ActionCommentMandetory = c.Field<Boolean>("ActionCommentMandetory")
        //                          }).ToList();
        //    }
        //    return stageEscalators;
        //}

        //public void UpdateStageEscalators(int StageEscalatorID, int StageID, int EscalatorID, int DepartmentID, bool ActionCommentMandetory)
        //{
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@StageEscalatorID",          StageEscalatorID, DbType.Int32),
        //        dbMAnager.CreateParameter("@StageID",                   StageID, DbType.Int32),
        //        dbMAnager.CreateParameter("@EscalatorID",               EscalatorID, DbType.Int32),
        //        dbMAnager.CreateParameter("@DepartmentID",              DepartmentID, DbType.Int32),
        //        dbMAnager.CreateParameter("@ActionCommentMandetory",    ActionCommentMandetory, DbType.Boolean)
        //    };
        //    dbMAnager.Update("usp_UpdateStageEscalators", CommandType.StoredProcedure, parameters);
        //}
    }
}
