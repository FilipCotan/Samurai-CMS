namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetConferenceEditionRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Editions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 600),
                        Year = c.Int(nullable: false),
                        AbstractDeadline = c.DateTime(),
                        PaperDeadline = c.DateTime(),
                        ResultsDeadline = c.DateTime(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        ConferenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.ConferenceId, cascadeDelete: true)
                .Index(t => t.ConferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Editions", "ConferenceId", "dbo.Conferences");
            DropIndex("dbo.Editions", new[] { "ConferenceId" });
            DropTable("dbo.Editions");
        }
    }
}
