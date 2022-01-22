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

        public void AddStageEscalators(int StageID, int EscalatorID, int DepartmentID)
        {
            string sql = "INSERT INTO StageEscalators (STAGEID, ESCALATORID, DEPARTMENTID) VALUES (@STAGEID, @ESCALATORID, @DEPARTMENTID)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@STAGEID",                   StageID, DbType.Int32),
                dbMAnager.CreateParameter("@ESCALATORID",               EscalatorID, DbType.Int32),
                dbMAnager.CreateParameter("@DEPARTMENTID",              DepartmentID, DbType.Int32)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }
        public void UpdateStageEscalators(int StageEscalatorID, int StageID, int EscalatorID, int DepartmentID)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@STAGEESCALATORID",          StageEscalatorID, DbType.Int32),
                dbMAnager.CreateParameter("@STAGEID",                   StageID, DbType.Int32),
                dbMAnager.CreateParameter("@ESCALATORID",               EscalatorID, DbType.Int32),
                dbMAnager.CreateParameter("@DEPARTMENTID",              DepartmentID, DbType.Int32)
            };
            dbMAnager.Update("usp_UpdateStageEscalators", CommandType.StoredProcedure, parameters);
        }
    }
}
