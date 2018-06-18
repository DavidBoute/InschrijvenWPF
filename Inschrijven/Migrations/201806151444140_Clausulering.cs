namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clausulering : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttestSoorts", "IsClausuleringVereist", c => c.Boolean(nullable: false));
            AlterColumn("dbo.VoorgaandeInschrijvings", "Clausulering", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VoorgaandeInschrijvings", "Clausulering", c => c.String(nullable: false));
            DropColumn("dbo.AttestSoorts", "IsClausuleringVereist");
        }
    }
}
