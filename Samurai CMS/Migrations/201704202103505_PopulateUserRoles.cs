namespace Samurai_CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUserRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UserRoles VALUES('Chair', 0)");
            Sql("INSERT INTO UserRoles VALUES('Co-Chair', 1)");
            Sql("INSERT INTO UserRoles VALUES('Program Committee Member', 1)");
            Sql("INSERT INTO UserRoles VALUES('Author', 0)");
            Sql("INSERT INTO UserRoles VALUES('Listener', 0)");
            Sql("INSERT INTO UserRoles VALUES('Admin', NULL)");
        }
        
        public override void Down()
        {
        }
    }
}
