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
            string sql = "INSERT INTO Department (DepartmentName, City, State, District, Pincode, Latitude, Longitude, DeptOwner, ContactDetails) VALUES (@DepartmentName, @City, @State, @District, @Pincode, @Latitude, @Longitude, @DeptOwner, @ContactDetails)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@DepartmentName",    DepartmentName, DbType.String),
                dbMAnager.CreateParameter("@City",              City, DbType.String),
                dbMAnager.CreateParameter("@State",             State, DbType.String),
                dbMAnager.CreateParameter("@District",          District, DbType.String),
                dbMAnager.CreateParameter("@Pincode",           Pincode, DbType.String),
                dbMAnager.CreateParameter("@Latitude",          Latitude, DbType.String),
                dbMAnager.CreateParameter("@Longitude",         Longitude, DbType.String),
                dbMAnager.CreateParameter("@DeptOwner",         DeptOwner, DbType.Int32),
                dbMAnager.CreateParameter("@ContactDetails",    ContactDetails, DbType.String)

            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }

        public List<Department> GetDepartment(int DepartmentID, string DepartmentName, string City, string State, string District, string Pincode,
            string Latitude, string Longitude, int DeptOwner, string ContactDetails)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@DepartmentID",            DepartmentID, DbType.Int32),
                dbMAnager.CreateParameter("@DepartmentName",    DepartmentName, DbType.String),
                dbMAnager.CreateParameter("@City",              City, DbType.String),
                dbMAnager.CreateParameter("@State",             State, DbType.String),
                dbMAnager.CreateParameter("@District",          District, DbType.String),
                dbMAnager.CreateParameter("@Pincode",           Pincode, DbType.String),
                dbMAnager.CreateParameter("@Latitude",          Latitude, DbType.String),
                dbMAnager.CreateParameter("@Longitude",         Longitude, DbType.String),
                dbMAnager.CreateParameter("@DeptOwner",         DeptOwner, DbType.Int32),
                dbMAnager.CreateParameter("@ContactDetails",    ContactDetails, DbType.String)
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
                dbMAnager.CreateParameter("@DepartmentID",      DepartmentID, DbType.Int32),
                dbMAnager.CreateParameter("@DepartmentName",    DepartmentName, DbType.String),
                dbMAnager.CreateParameter("@City",              City, DbType.String),
                dbMAnager.CreateParameter("@State",             State, DbType.String),
                dbMAnager.CreateParameter("@District",          District, DbType.String),
                dbMAnager.CreateParameter("@Pincode",           Pincode, DbType.String),
                dbMAnager.CreateParameter("@Latitude",          Latitude, DbType.String),
                dbMAnager.CreateParameter("@Longitude",         Longitude, DbType.String),
                dbMAnager.CreateParameter("@DeptOwner",         DeptOwner, DbType.Int32),
                dbMAnager.CreateParameter("@ContactDetails",    ContactDetails, DbType.String)
            };
            dbMAnager.Update("usp_UpdateDepartment", CommandType.StoredProcedure, parameters);
        }

        //public List<Department> GetDepartment(string DepartmentName)
        //{
        //    return GetDepartments(DepartmentName, null, null, null, null, null, null, 0);
        //}

        //public List<Department> GetDepartment(string State, string City)
        //{
        //    return GetDepartments("", State, City, null, null, null, null, 0);
        //}

        //public List<Department> GetDepartments(string City, string District, string Pincode)
        //{
        //    return GetDepartments("", "", City, District, Pincode, null, null, 0);
        //}

        //public List<Department> GetDepartments(string Latitude, string Longitude)
        //{
        //    return GetDepartments(null, null, null, null, null, Latitude, Longitude, 0);
        //}
        //public List<Department> GetDepartments(string DepartmentName, string State, string City,  string District, string Pincode, string Latitude, string Longitude, int DeptOwner) 
        //{
            
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@DepartmentName",    DepartmentName, DbType.String),
        //        dbMAnager.CreateParameter("@City",              City, DbType.String),
        //        dbMAnager.CreateParameter("@State",             State, DbType.String),
        //        dbMAnager.CreateParameter("@District",          District, DbType.String),
        //        dbMAnager.CreateParameter("@Pincode",           Pincode, DbType.String),
        //        dbMAnager.CreateParameter("@Latitude",          Latitude, DbType.String),
        //        dbMAnager.CreateParameter("@Longitude",         Longitude, DbType.String),
        //        dbMAnager.CreateParameter("@DeptOwner",         DeptOwner == 0 ? null: DeptOwner, DbType.Int32)
        //    };
        //    DataTable dt = dbMAnager.GetDataTable("usp_GetDepartments", CommandType.StoredProcedure, parameters);
        //    List<Department> department = new List<Department>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        department = (from c in dt.AsEnumerable()
        //                      select new Department
        //                      {
        //                          DepartmentID = c.Field<int>("DepartmentID"),
        //                          DepartmentName = c.Field<string>("DepartmentName"),
        //                          City = c.Field<string>("City"),
        //                          State = c.Field<string>("State"),
        //                          District = c.Field<string>("District"),
        //                          Pincode = c.Field<string>("Pincode"),
        //                          Latitude = c.Field<string>("Latitude"),
        //                          Longitude = c.Field<string>("Longitude"),
        //                          DeptOwner = c.Field<int>("DeptOwner"),
        //                          ContactDetails = c.Field<string>("ContactDetails")
        //                      }).ToList();
        //    }
        //    return department;
        //}
    }
}
