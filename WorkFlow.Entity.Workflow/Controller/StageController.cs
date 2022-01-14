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

        public void AddStage(string Title, string Description, DateTime ReviewTime, DateTime EscalationTime, bool IsAnyApprover)
        {
            string sql = "INSERT INTO Stage (Title, Description, ReviewTime, EscalationTime, IsAnyApprover) VALUES (@Title, @Description, @ReviewTime, @EscalationTime, @IsAnyApprover)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@Title",                 Title, DbType.String),
                dbMAnager.CreateParameter("@Description",           Description, DbType.String),
                dbMAnager.CreateParameter("@ReviewTime",            ReviewTime, DbType.DateTime),
                dbMAnager.CreateParameter("@EscalationTime",        EscalationTime, DbType.DateTime),
                dbMAnager.CreateParameter("@IsAnyApprover",         IsAnyApprover, DbType.Boolean)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }
        //public List<Stage> GetStages(int StageID, string Title, string Description, DateTime ReviewTime, DateTime EscalationTime, bool IsAnyApprover)
        //{
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@StageID",               StageID, DbType.Int32),
        //        dbMAnager.CreateParameter("@Title",                 Title, DbType.String),
        //        dbMAnager.CreateParameter("@Description",           Description, DbType.String),
        //        dbMAnager.CreateParameter("@ReviewTime",            ReviewTime, DbType.DateTime),
        //        dbMAnager.CreateParameter("@EscalationTime",        EscalationTime, DbType.DateTime),
        //        dbMAnager.CreateParameter("@IsAnyApprover",         IsAnyApprover, DbType.Boolean)
        //    };

        //    DataTable dt = dbMAnager.GetDataTable("usp_GetStages", CommandType.StoredProcedure, parameters);
        //    List<Stage> Stages = new List<Stage>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        Stages = (from c in dt.AsEnumerable()
        //                   select new Stage
        //                   {
        //                       StageID = c.Field<int>("StageID"),
        //                       Title = c.Field<string>("Title"),
        //                       Description = c.Field<string>("Description"),
        //                       ReviewTime = c.Field<DateTime>("ReviewTime"),
        //                       EscalationTime = c.Field<DateTime>("EscalationTime"),
        //                       IsAnyApprover = c.Field<Boolean>("IsAnyApprover")
        //                   }).ToList();
        //    }
        //    return Stages;
        //}

        //public void UpdateStage(int StageID, string Title, string Description, DateTime ReviewTime, DateTime EscalationTime, bool IsAnyApprover)
        //{
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@StageID",               StageID, DbType.Int32),
        //        dbMAnager.CreateParameter("@Title",                 Title, DbType.String),
        //        dbMAnager.CreateParameter("@Description",           Description, DbType.String),
        //        dbMAnager.CreateParameter("@ReviewTime",            ReviewTime, DbType.DateTime),
        //        dbMAnager.CreateParameter("@EscalationTime",        EscalationTime, DbType.DateTime),
        //        dbMAnager.CreateParameter("@IsAnyApprover",         IsAnyApprover, DbType.Boolean)
        //    };
        //    dbMAnager.Update("usp_UpdateStage", CommandType.StoredProcedure, parameters);
        //}

    }
}
