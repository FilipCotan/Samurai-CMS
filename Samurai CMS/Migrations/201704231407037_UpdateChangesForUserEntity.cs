namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChangesForUserEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "WebsiteUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "WebsiteUrl", c => c.String(nullable: false));
        }
    }
}
