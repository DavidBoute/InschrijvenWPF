namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Toestemming_code : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToestemmingSoorts", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToestemmingSoorts", "Code", c => c.String());
        }
    }
}
