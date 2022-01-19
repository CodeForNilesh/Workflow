using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorkFlow.DAL;
using WorkFlow.UserManagement.Entities;
using WorkFlow.WorkflowManagement.Entities;

namespace WorkFlow.WorkflowManagement.Controller
{
    public class WorkflowController
    {
        DBManager dbMAnager;
        public WorkflowController()
        {
            dbMAnager = new DBManager("sqldbConnection");
        }
        public void AddWorkflow(string Title, string Details)
        {
            string sql = "INSERT INTO Workflow (Title, Details) VALUES (@Title, @Details)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@Title",     Title, DbType.String),
                dbMAnager.CreateParameter("@Details",   Details, DbType.String)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }

        public List<Workflow> GetWorkflows(int WorkflowID)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@WorkflowID",                WorkflowID, DbType.Int32),
            };
            DataTable dt = dbMAnager.GetDataTable("usp_GetWorkflowMeta", CommandType.StoredProcedure, parameters);
            List<Workflow> workflows = new List<Workflow>();
            if (dt.Rows.Count > 0)
            {
                workflows = (from c in dt.AsEnumerable()
                             select new Workflow
                             {
                                 WorkflowID = c.Field<int>("WorkflowID"),
                                 Title = c.Field<string>("Title"),
                                 Details = c.Field<string>("Details")
                             }).ToList();
            }
            return workflows;
        }
        public Workflow GetEntireWorkflow(int WorkflowID)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@WorkflowID",                WorkflowID, DbType.Int32),
            };
            DataSet ds = dbMAnager.GetDataSet("usp_GetEntireWorkflow", CommandType.StoredProcedure, parameters);
            Workflow workFlow = null;
            if (ds.Tables.Count > 0)
            {
                DataTable table = new DataTable();
                if (ds.Tables[0]?.Rows.Count > 0)
                {
                    table = ds.Tables[0];
                    workFlow = new Workflow()
                    {
                        WorkflowID = int.Parse(table.Rows[0]["WorkflowID"].ToString()),
                        Title = table.Rows[0]["Title"].ToString(),
                        Details = table.Rows[0]["Details"].ToString()
                    };
                    workFlow.Stages = GetStages(ds);
                }
            }
            return workFlow;
        }
        private List<Stage> GetStages(DataSet ds)
        {
            List<Stage> Stages = new List<Stage>();
            if (ds.Tables[1]?.Rows.Count > 0)
            {
                Stages = (from c in ds.Tables[1].AsEnumerable()
                          select new Stage
                          {

                              StageID = c.Field<int>("StageID"),
                              Title = c.Field<string>("Title"),
                              Description = c.Field<string>("Description"),
                              ReviewTime = c.Field<int>("ReviewTime"),
                              EscalationTime = c.Field<int>("EscalationTime"),
                              Reviewers = GetPersons(c.Field<int>("StageID"), ds.Tables[2]),
                              ReviewerDepartments = GetDepartments(c.Field<int>("StageID"), ds.Tables[3]),
                              Escalators = GetPersons(c.Field<int>("StageID"), ds.Tables[4]),
                              EscalatorDepartments = GetDepartments(c.Field<int>("StageID"), ds.Tables[5]),
                              IsAnyApprover = c.Field<Boolean>("IsAnyApprover"),
                              ActionCommentMandetory = c.Field<Boolean>("ActionCommentMandetory")
                          }).ToList();
            }
            return Stages;
        }
        private List<Department> GetDepartments(int StageID, DataTable departmentTable)
        {
            List<Department> department = new List<Department>();
            if (departmentTable?.Rows.Count > 0)
            {
                department = (from c in departmentTable.Select("StageID=" + StageID).AsEnumerable()
                              select new Department
                              {
                                  DepartmentID = c.Field<int>("DepartmentID"),
                                  DepartmentName = c.Field<string>("DepartmentName"),
                                  City = c.Field<string>("City"),
                                  State = c.Field<string>("State"),
                                  District = c.Field<string>("District"),
                                  Pincode = c.Field<string>("Pincode"),
                                  Latitude = c.Field<string>("Latitude"),
                                  Longitude = c.Field<string>("Longitude"),
                                  DeptOwner = c.Field<int>("DeptOwner"),
                                  ContactDetails = c.Field<string>("ContactDetails")
                              }).ToList();
            }
            return department;
        }

        private List<Person> GetPersons(int StageID, DataTable personTable)
        {
            List<Person> Persons = new List<Person>();
            if (personTable?.Rows.Count > 0)
            {
                Persons = (from c in personTable.Select("StageID=" + StageID).AsEnumerable()
                           select new Person
                           {
                               UserID = c.Field<int>("UserID"),
                               FirstName = c.Field<string>("FirstName"),
                               MiddleName = c.Field<string>("MiddleName"),
                               LastName = c.Field<string>("LastName"),
                               Email = c.Field<string>("Email"),
                               Mobile = c.Field<string>("Mobile"),
                               City = c.Field<string>("City"),
                               State = c.Field<string>("State"),
                               District = c.Field<string>("District"),
                               Pincode = c.Field<string>("Pincode")
                           }).ToList();
            }
            return Persons;
        }
    }
}