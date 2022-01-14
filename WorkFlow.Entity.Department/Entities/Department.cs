using System.Collections.Generic;

namespace WorkFlow.UserManagement.Entities
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Pincode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int DeptOwner { get; set; }
        public string ContactDetails { get; set; }
        public List<Person> Persons { get; set; }
    }
}
