using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudAngular.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string CityName { get; set; }
        public int PostalCode { get; set; }
    }
}