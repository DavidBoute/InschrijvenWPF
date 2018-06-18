namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schoolreglement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inschrijvings", "IsAkkoordSchoolreglement", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inschrijvings", "IsAkkoordSchoolreglement");
        }
    }
}
