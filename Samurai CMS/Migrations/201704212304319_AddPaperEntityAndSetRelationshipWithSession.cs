namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaperEntityAndSetRelationshipWithSession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorPapers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Authors = c.String(),
                        Keywords = c.String(),
                        IsAccepted = c.Boolean(),
                        AbstractFileType = c.String(),
                        Abstract = c.Binary(),
                        PaperFileType = c.String(),
                        Paper = c.Binary(),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorPapers", "SessionId", "dbo.Sessions");
            DropIndex("dbo.AuthorPapers", new[] { "SessionId" });
            DropTable("dbo.AuthorPapers");
        }
    }
}
