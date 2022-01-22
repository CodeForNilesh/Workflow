using System.Collections.Generic;
using WorkFlow.DAL;
using WorkFlow.UserManagement.Entities;
using System.Data;
using System.Linq;

namespace WorkFlow.UserManagement.Controller
{
    public class PersonsController
    {
        DBManager dbMAnager;
        public PersonsController()
        {
            dbMAnager = new DBManager("sqldbConnection");
        }
        public void AddPerson(string FirstName, string MiddleName, string LastName, string Email,
            string Mobile, string City, string State, string District, string Pincode)
        {
            string sql = "INSERT INTO PERSONS (FIRSTNAME, MIDDLENAME, LASTNAME ,EMAIL, MOBILE, CITY, STATE, DISTRICT, PINCODE) VALUES ( @FIRSTNAME, @MIDDLENAME, @LASTNAME ,@EMAIL, @MOBILE, @CITY, @STATE, @DISTRICT, @PINCODE)";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@FIRSTNAME",     FirstName, DbType.String),
                dbMAnager.CreateParameter("@MIDDLENAME",    MiddleName, DbType.String),
                dbMAnager.CreateParameter("@LASTNAME",      LastName, DbType.String),
                dbMAnager.CreateParameter("@EMAIL",         Email, DbType.String),
                dbMAnager.CreateParameter("@MOBILE",        Mobile, DbType.String),
                dbMAnager.CreateParameter("@CITY",          City, DbType.String),
                dbMAnager.CreateParameter("@STATE",         State, DbType.String),
                dbMAnager.CreateParameter("@DISTRICT",      District, DbType.String),
                dbMAnager.CreateParameter("@PINCODE",       Pincode, DbType.String)
            };
            dbMAnager.Insert(sql, CommandType.Text, parameters);
        }
        //public List<Person> GetPersons(int UserID, string FirstName, string MiddleName, string LastName, string Email, 
        //    string Mobile, string City, string State, string District, string Pincode)
        //{
        //    IDbDataParameter[] parameters = new IDbDataParameter[]
        //    {
        //        dbMAnager.CreateParameter("@UserID",        UserID, DbType.Int32),
        //        dbMAnager.CreateParameter("@FirstName",     FirstName, DbType.String),
        //        dbMAnager.CreateParameter("@MiddleName",    MiddleName, DbType.String),
        //        dbMAnager.CreateParameter("@LastName",      LastName, DbType.String),
        //        dbMAnager.CreateParameter("@Email",         Email, DbType.String),
        //        dbMAnager.CreateParameter("@Mobile",        Mobile, DbType.String),
        //        dbMAnager.CreateParameter("@City",          City, DbType.String),
        //        dbMAnager.CreateParameter("@State",         State, DbType.String),
        //        dbMAnager.CreateParameter("@District",      District, DbType.String),
        //        dbMAnager.CreateParameter("@Pincode",       Pincode, DbType.String)
        //    };

        //    DataTable dt = dbMAnager.GetDataTable("usp_GetPersons",CommandType.StoredProcedure,parameters);
        //    List<Person> persons = new List<Person>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        persons = (from c in dt.AsEnumerable()
        //                       select new Person
        //                       {
        //                           UserID = c.Field<int>("UserID"),
        //                           FirstName = c.Field<string>("FirstName"),
        //                           MiddleName = c.Field<string>("MiddleName"),
        //                           LastName = c.Field<string>("LastName"),
        //                           Email = c.Field<string>("Email"),
        //                           Mobile = c.Field<string>("Mobile"),
        //                           City = c.Field<string>("City"),
        //                           State = c.Field<string>("State"),
        //                           District = c.Field<string>("District"),
        //                           Pincode = c.Field<string>("Pincode")
        //                       }).ToList();
        //    }
        //    return persons;
        //}

        public void UpdatePerson(int UserID, string FirstName, string MiddleName, string LastName, string Email,
            string Mobile, string City, string State, string District, string Pincode)
        {
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                dbMAnager.CreateParameter("@USERID",        UserID, DbType.Int32),
                dbMAnager.CreateParameter("@FIRSTNAME",     FirstName, DbType.String),
                dbMAnager.CreateParameter("@MIDDLENAME",    MiddleName, DbType.String),
                dbMAnager.CreateParameter("@LASTNAME",      LastName, DbType.String),
                dbMAnager.CreateParameter("@EMAIL",         Email, DbType.String),
                dbMAnager.CreateParameter("@MOBILE",        Mobile, DbType.String),
                dbMAnager.CreateParameter("@CITY",          City, DbType.String),
                dbMAnager.CreateParameter("@STATE",         State, DbType.String),
                dbMAnager.CreateParameter("@DISTRICT",      District, DbType.String),
                dbMAnager.CreateParameter("@PINCODE",       Pincode, DbType.String)
            };
            dbMAnager.Update("usp_UpdatePerson", CommandType.StoredProcedure, parameters);
        }
    }
}
