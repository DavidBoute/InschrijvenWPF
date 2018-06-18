namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToestemmingSoort : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToestemmingSoorts",
                c => new
                    {
                        ToestemmingSoortId = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false),
                        IsEnkelVoorEersteGraad = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ToestemmingSoortId);
            
            CreateIndex("dbo.Toestemmings", "ToestemmingId");
            AddForeignKey("dbo.Toestemmings", "ToestemmingId", "dbo.ToestemmingSoorts", "ToestemmingSoortId");
            DropColumn("dbo.Toestemmings", "ToestemmingOmschrijving");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toestemmings", "ToestemmingOmschrijving", c => c.String(nullable: false));
            DropForeignKey("dbo.Toestemmings", "ToestemmingId", "dbo.ToestemmingSoorts");
            DropIndex("dbo.Toestemmings", new[] { "ToestemmingId" });
            DropTable("dbo.ToestemmingSoorts");
        }
    }
}
