using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCSpa.Models
{
    public class SecurityContext : DbContext
    {
        public SecurityContext() : base("name=AppDbContext")
        {

        }
        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblRole> tblRoles { get; set; }
    }
}