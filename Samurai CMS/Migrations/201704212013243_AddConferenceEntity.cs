namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConferenceEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Theme = c.String(maxLength: 40),
                        PaperIsRequired = c.Boolean(),
                        ReviewersCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Conferences");
        }
    }
}
