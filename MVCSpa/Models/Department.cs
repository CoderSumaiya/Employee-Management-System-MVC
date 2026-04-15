using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSpa.Models
{
    public class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }


        public virtual ICollection<Employee> Employees { get; set; }
    }
}