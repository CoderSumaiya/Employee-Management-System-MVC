using MVCSpa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSpa.ViewModels
{
    public class EmployeeViewModel
    {

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee  name is required")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
               
        [Required]
        [DataType(DataType.Date)]
        [PastDate(ErrorMessage = "Joining date cannot be a future date")]
        [Display(Name = "Joining Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime JoiningDate { get; set; } = DateTime.Now;

        [Remote("CheckMobileExists", "Employees", ErrorMessage = "Mobile number already exists!")]
        [Required(ErrorMessage = "Mobile number is required")]

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Please enter a valid mobile number (10-11 digits)")]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Required]
        [Range(0, 999999.99, ErrorMessage = "Salary fee must be a positive value")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
        [Display(Name = "IsPermanent?")]
        public bool IsPermanent { get; set; }


        [Required(ErrorMessage = "Please select a Department")]


        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Display(Name = " Image")]
        public string ImageUrl { get; set; }



        [Display(Name = "Upload Picture")]
       
        public HttpPostedFileBase ProfileFile { get; set; }
       
        public virtual IList<AcademicDetail> AcademicDetails { get; set; } = new List<AcademicDetail>();
       
        public virtual IList<Department> Departments { get; set; }
        public virtual IList<Employee> Employees { get; set; }
    }
}