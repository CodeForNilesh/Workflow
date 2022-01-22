using System.Collections.Generic;
using WorkFlow.DAL;
using WorkFlow.UserManagement.Entities;
using System.Data;
using System.Linq;
using System;

namespace WorkFlow.UserManagement.Controller
{
    public class DepartmentController
    {
        DBManager dbMAnager;

        public DepartmentController()
        {
            dbMAnager = new DBManager("sqldbConnection");
        }

        public void AddDepartment(string DepartmentName, string City, string State, string District, string Pincode, 
            string Latitude, string Longitude, int DeptOwner, string ContactDetails)
        {
            string sql = "INSERT INTO DEPARTMENT (DEPARTMENTNAME, CITY, STATE, DISTRICT, PINCODE, LATITUDE, LONGITUDE, DEPTOWNER, CONTACTDETAILS) VALUES (@DEPARTMENTNAME, @CITY, @STATE, @DISTRICT, @PINCODE, @LATITUDE, @LONGITUDE, @DEPTOWNER, @CONTACTDETAILS)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@DEPARTMENTNAME",    DepartmentName, DbType.String),
                dbMAnager.CreateParameter("@CITY",              City, DbType.String),
                dbMAnager.CreateParameter("@STATE",             State, DbType.String),
                dbMAnager.CreateParameter("@DISTRICT",          District, DbType.String),
                dbMAnager.CreateParameter("@PINCODE",           Pincode, DbType.String),
                dbMAnager.CreateParameter("@LATITUDE",          Latitude, DbType.String),
                dbMAnager.CreateParameter("@LONGITUDE",         Longitude, DbType.String),
                dbMAnager.CreateParameter("@DEPTOWNER",         DeptOwner, DbType.Int32),
                dbMAnager.CreateParameter("@CONTACTDETAILS",    ContactDetails, DbType.String)

            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }

        public List<Department> GetDepartment(int DepartmentID, string DepartmentName, string City, string State, string District, string Pincode,
            string Latitude, string Longitude, int DeptOwner, string ContactDetails)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@DEPARTMENTID",            DepartmentID, DbType.Int32),
                dbMAnager.CreateParameter("@DEPARTMENTNAME",    DepartmentName, DbType.String),
                dbMAnager.CreateParameter("@CITY",              City, DbType.String),
                dbMAnager.CreateParameter("@STATE",             State, DbType.String),
                dbMAnager.CreateParameter("@DISTRICT",          District, DbType.String),
                dbMAnager.CreateParameter("@PINCODE",           Pincode, DbType.String),
                dbMAnager.CreateParameter("@LATITUDE",          Latitude, DbType.String),
                dbMAnager.CreateParameter("@LONGITUDE",         Longitude, DbType.String),
                dbMAnager.CreateParameter("@DEPTOWNER",         DeptOwner, DbType.Int32),
                dbMAnager.CreateParameter("@CONTACTDETAILS",    ContactDetails, DbType.String)
            };

            DataTable dt = dbMAnager.GetDataTable("usp_GetDepartments", CommandType.StoredProcedure, parameters);
            List<Department> department = new List<Department>();
            if (dt.Rows.Count > 0)
            {
                department = (from c in dt.AsEnumerable()
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

        public void UpdateDepartment(int DepartmentID, string DepartmentName, string City, string State, string District, string Pincode,
            string Latitude, string Longitude, int DeptOwner, string ContactDetails)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@DEPARTMENTID",      DepartmentID, DbType.Int32),
                dbMAnager.CreateParameter("@DEPARTMENTNAME",    DepartmentName, DbType.String),
                dbMAnager.CreateParameter("@CITY",              City, DbType.String),
                dbMAnager.CreateParameter("@STATE",             State, DbType.String),
                dbMAnager.CreateParameter("@DISTRICT",          District, DbType.String),
                dbMAnager.CreateParameter("@PINCODE",           Pincode, DbType.String),
                dbMAnager.CreateParameter("@LATITUDE",          Latitude, DbType.String),
                dbMAnager.CreateParameter("@LONGITUDE",         Longitude, DbType.String),
                dbMAnager.CreateParameter("@DEPTOWNER",         DeptOwner, DbType.Int32),
                dbMAnager.CreateParameter("@CONTACTDETAILS",    ContactDetails, DbType.String)
            };
            dbMAnager.Update("usp_UpdateDepartment", CommandType.StoredProcedure, parameters);
        }
    }
}
