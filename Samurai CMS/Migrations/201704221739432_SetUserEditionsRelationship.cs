namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetUserEditionsRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        EditionId = c.Int(nullable: false),
                        PaperId = c.Int(),
                        Affiliation = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.EditionId })
                .ForeignKey("dbo.Editions", t => t.EditionId, cascadeDelete: true)
                .ForeignKey("dbo.AuthorPapers", t => t.PaperId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EditionId)
                .Index(t => t.PaperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "PaperId", "dbo.AuthorPapers");
            DropForeignKey("dbo.Enrollments", "EditionId", "dbo.Editions");
            DropIndex("dbo.Enrollments", new[] { "PaperId" });
            DropIndex("dbo.Enrollments", new[] { "EditionId" });
            DropIndex("dbo.Enrollments", new[] { "UserId" });
            DropTable("dbo.Enrollments");
        }
    }
}
