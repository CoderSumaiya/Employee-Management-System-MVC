using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCSpa.Models
{
    public class tblRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }
     
        public virtual tblUser tblUser { get; set; }
       
    }
}