namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterUserIdFromSessionEntity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sessions", new[] { "User_Id" });
            DropColumn("dbo.Sessions", "UserId");
            RenameColumn(table: "dbo.Sessions", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Sessions", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sessions", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sessions", new[] { "UserId" });
            AlterColumn("dbo.Sessions", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Sessions", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Sessions", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sessions", "User_Id");
        }
    }
}
