namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toestemmingsoort_code_opt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToestemmingSoorts", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToestemmingSoorts", "Code", c => c.String(nullable: false));
        }
    }
}
