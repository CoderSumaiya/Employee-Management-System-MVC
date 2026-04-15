namespace MVCSpa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAuthTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblRoles", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblRoles", "UserId", c => c.String());
        }
    }
}
