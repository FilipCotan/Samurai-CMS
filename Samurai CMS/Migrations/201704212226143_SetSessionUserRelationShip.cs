namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetSessionUserRelationShip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Sessions", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sessions", "User_Id");
            AddForeignKey("dbo.Sessions", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Sessions", new[] { "User_Id" });
            DropColumn("dbo.Sessions", "User_Id");
            DropColumn("dbo.Sessions", "UserId");
        }
    }
}
