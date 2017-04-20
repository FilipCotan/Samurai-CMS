namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserRoles", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.UserRoles", "IsReviewer", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRoles", "IsReviewer", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserRoles", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
