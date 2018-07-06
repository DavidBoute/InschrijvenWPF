namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LerenKennen : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MarketingLerenKennenSoorts", newName: "MarketingLerenKennens");
            RenameColumn(table: "dbo.MarketingLerenKennens", name: "LerenKennenSoort_LerenKennenSoortId", newName: "LerenKennen_LerenKennenId");
            RenameIndex(table: "dbo.MarketingLerenKennens", name: "IX_LerenKennenSoort_LerenKennenSoortId", newName: "IX_LerenKennen_LerenKennenId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MarketingLerenKennens", name: "IX_LerenKennen_LerenKennenId", newName: "IX_LerenKennenSoort_LerenKennenSoortId");
            RenameColumn(table: "dbo.MarketingLerenKennens", name: "LerenKennen_LerenKennenId", newName: "LerenKennenSoort_LerenKennenSoortId");
            RenameTable(name: "dbo.MarketingLerenKennens", newName: "MarketingLerenKennenSoorts");
        }
    }
}
