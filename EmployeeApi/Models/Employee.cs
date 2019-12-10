using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeApi.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public DateTime HiredDate { get; set; }
        public List<int> TaskIdList { get; set; }
    }
    public class EmployeeTask
    {
        public int TaskId { get; set; }
        public String TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadLine { get; set; }
    }
}