using MVCSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSpa.ViewModels
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<tblUser> UserList { get; set; }
    }
}