using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCSpa.Models
{
    public class AcademicDetail
    {
        public int AcademicDetailId { get; set; }
       
        public int EmployeeId { get; set; }
        public string DegreeName { get; set; }
        public int PassingYear { get; set; }
        public decimal CGPA { get; set; }
       

        public virtual Employee Employee { get; set; }
    }
}