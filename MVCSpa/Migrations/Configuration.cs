namespace MVCSpa.Migrations
{
    using MVCSpa.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCSpa.DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCSpa.DAL.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var Departments = new List<Department>
            {
                new Department {DepartmentName="IT" },
                new Department {DepartmentName="HR" },
                new Department {DepartmentName="Accounts" }
            };
            Departments.ForEach(e => context.Departments.AddOrUpdate(p => p.DepartmentName, e));
            context.SaveChanges();
        }
    }
}
