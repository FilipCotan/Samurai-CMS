namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionEntitySetRealtionshipWithEdition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(nullable: false, maxLength: 300),
                        IsMorningSession = c.Boolean(),
                        EditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Editions", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "EditionId", "dbo.Editions");
            DropIndex("dbo.Sessions", new[] { "EditionId" });
            DropTable("dbo.Sessions");
        }
    }
}
