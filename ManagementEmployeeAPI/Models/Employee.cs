using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudAngular.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string DateJoin { get; set; }
        public int PaymentDolar { get; set; }
        public string PhotoFileName { get; set; }
    }
}