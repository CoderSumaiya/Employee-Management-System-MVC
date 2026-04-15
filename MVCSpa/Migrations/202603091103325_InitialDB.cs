namespace MVCSpa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicDetails",
                c => new
                    {
                        AcademicDetailId = c.Int(nullable: false, identity: true),
                        DegreeName = c.String(),
                        PassingYear = c.Int(nullable: false),
                        CGPA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AcademicDetailId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        MobileNo = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPermanent = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcademicDetails", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.AcademicDetails", new[] { "EmployeeId" });
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.AcademicDetails");
        }
    }
}
