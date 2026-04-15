namespace MVCSpa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAuthTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        UserId = c.String(),
                        tblUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblUsers", t => t.tblUser_Id)
                .Index(t => t.tblUser_Id);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblRoles", "tblUser_Id", "dbo.tblUsers");
            DropIndex("dbo.tblRoles", new[] { "tblUser_Id" });
            DropTable("dbo.tblUsers");
            DropTable("dbo.tblRoles");
        }
    }
}
