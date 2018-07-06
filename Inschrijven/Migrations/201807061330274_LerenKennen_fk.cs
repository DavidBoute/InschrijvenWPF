namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LerenKennen_fk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LerenKennens", "LerenKennenId", "dbo.LerenKennenSoorts");
            DropIndex("dbo.LerenKennens", new[] { "LerenKennenId" });
            AddColumn("dbo.LerenKennens", "LerenKennenSoort_id", c => c.Int(nullable: false));
            CreateIndex("dbo.LerenKennens", "LerenKennenSoort_id");
            AddForeignKey("dbo.LerenKennens", "LerenKennenSoort_id", "dbo.LerenKennenSoorts", "LerenKennenSoortId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LerenKennens", "LerenKennenSoort_id", "dbo.LerenKennenSoorts");
            DropIndex("dbo.LerenKennens", new[] { "LerenKennenSoort_id" });
            DropColumn("dbo.LerenKennens", "LerenKennenSoort_id");
            CreateIndex("dbo.LerenKennens", "LerenKennenId");
            AddForeignKey("dbo.LerenKennens", "LerenKennenId", "dbo.LerenKennenSoorts", "LerenKennenSoortId");
        }
    }
}
