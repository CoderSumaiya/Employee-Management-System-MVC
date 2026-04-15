using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSpa.Models
{
    public class Employee
    {
        public Employee()
        {
            this.AcademicDetails = new HashSet<AcademicDetail>();
        }

        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]

        public string EmployeeName { get; set; }
        [Required, Display(Name = "Joining Date"), DataType(DataType.Date),
                  DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime JoiningDate { get; set; }
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "IsPermanent?")]
        public bool IsPermanent { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<AcademicDetail> AcademicDetails { get; set; }
    }
}