namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWebsiteUrlFieldToUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "RoleId", "dbo.UserRoles");
            DropIndex("dbo.AspNetUsers", new[] { "RoleId" });
            AddColumn("dbo.AspNetUsers", "WebsiteUrl", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "RoleId");
            AddForeignKey("dbo.AspNetUsers", "RoleId", "dbo.UserRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RoleId", "dbo.UserRoles");
            DropIndex("dbo.AspNetUsers", new[] { "RoleId" });
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "WebsiteUrl");
            CreateIndex("dbo.AspNetUsers", "RoleId");
            AddForeignKey("dbo.AspNetUsers", "RoleId", "dbo.UserRoles", "Id", cascadeDelete: true);
        }
    }
}
