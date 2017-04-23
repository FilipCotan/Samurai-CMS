namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewAssignemntsAndSetItsRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReviewAssignments",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        PaperId = c.Int(nullable: false),
                        BiddingResult = c.String(),
                        ReviewQualifier = c.String(),
                        Recommendations = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => new { t.UserId, t.PaperId })
                .ForeignKey("dbo.AuthorPapers", t => t.PaperId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PaperId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewAssignments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReviewAssignments", "PaperId", "dbo.AuthorPapers");
            DropIndex("dbo.ReviewAssignments", new[] { "PaperId" });
            DropIndex("dbo.ReviewAssignments", new[] { "UserId" });
            DropTable("dbo.ReviewAssignments");
        }
    }
}
