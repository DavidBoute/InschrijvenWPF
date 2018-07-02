namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toestemming : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Toestemmings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropIndex("dbo.Toestemmings", new[] { "Inschrijving_InschrijvingId" });
            AlterColumn("dbo.Toestemmings", "Inschrijving_InschrijvingId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Toestemmings", "Inschrijving_InschrijvingId");
            AddForeignKey("dbo.Toestemmings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings", "InschrijvingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toestemmings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropIndex("dbo.Toestemmings", new[] { "Inschrijving_InschrijvingId" });
            AlterColumn("dbo.Toestemmings", "Inschrijving_InschrijvingId", c => c.Guid());
            CreateIndex("dbo.Toestemmings", "Inschrijving_InschrijvingId");
            AddForeignKey("dbo.Toestemmings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings", "InschrijvingId");
        }
    }
}
