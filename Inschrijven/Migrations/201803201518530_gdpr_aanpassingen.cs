namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gdpr_aanpassingen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Beperkings", "BeperkingSoortId", "dbo.BeperkingSoorts");
            DropForeignKey("dbo.Beperkings", "LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId", "dbo.Avondstudies");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId1", "dbo.Avondstudies");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId2", "dbo.Avondstudies");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId3", "dbo.Avondstudies");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingId", "dbo.Avondstudies");
            DropForeignKey("dbo.BijkomendeInfoBeperkings", "BijkomendeInfo_BijkomendeInfoId", "dbo.BijkomendeInfoes");
            DropForeignKey("dbo.BijkomendeInfoBeperkings", "Beperking_BeperkingId", "dbo.Beperkings");
            DropForeignKey("dbo.BijkomendeInfoes", "TaalMoederTaalSoortId", "dbo.TaalSoorts");
            DropForeignKey("dbo.Emails", "EmailId", "dbo.Leerlings");
            DropIndex("dbo.Emails", new[] { "EmailId" });
            DropIndex("dbo.Beperkings", new[] { "LeerlingId" });
            DropIndex("dbo.Beperkings", new[] { "BeperkingSoortId" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId1" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId2" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId3" });
            DropIndex("dbo.BijkomendeInfoes", new[] { "TaalMoederTaalSoortId" });
            DropIndex("dbo.BijkomendeInfoBeperkings", new[] { "BijkomendeInfo_BijkomendeInfoId" });
            DropIndex("dbo.BijkomendeInfoBeperkings", new[] { "Beperking_BeperkingId" });
            AddColumn("dbo.Leerlings", "BijkomendeInfoId", c => c.Guid(nullable: false));
            AddColumn("dbo.Inschrijvings", "IsAvondstudie", c => c.Boolean(nullable: false));
            AddColumn("dbo.BijkomendeInfoes", "Taalproblemen", c => c.String());
            CreateIndex("dbo.Leerlings", "LeerlingId");
            AddForeignKey("dbo.Leerlings", "LeerlingId", "dbo.BijkomendeInfoes", "BijkomendeInfoId");
            AddForeignKey("dbo.Leerlings", "LeerlingId", "dbo.Emails", "EmailId");
            DropColumn("dbo.Inschrijvings", "AvonstudieId");
            DropColumn("dbo.BijkomendeInfoes", "FamilialeSituatie");
            DropColumn("dbo.BijkomendeInfoes", "TaalMoederTaalSoortId");
            DropTable("dbo.Beperkings");
            DropTable("dbo.BeperkingSoorts");
            DropTable("dbo.Avondstudies");
            DropTable("dbo.AvondstudieSoorts");
            DropTable("dbo.BijkomendeInfoBeperkings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BijkomendeInfoBeperkings",
                c => new
                    {
                        BijkomendeInfo_BijkomendeInfoId = c.Guid(nullable: false),
                        Beperking_BeperkingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BijkomendeInfo_BijkomendeInfoId, t.Beperking_BeperkingId });
            
            CreateTable(
                "dbo.AvondstudieSoorts",
                c => new
                    {
                        AvondstudieSoortId = c.Int(nullable: false, identity: true),
                        AvondstudieSoortNaam = c.String(nullable: false),
                        IsVoorzienOpVrijdag = c.Boolean(nullable: false),
                        Avondstudie_AvondstudieId = c.Guid(),
                        Avondstudie_AvondstudieId1 = c.Guid(),
                        Avondstudie_AvondstudieId2 = c.Guid(),
                        Avondstudie_AvondstudieId3 = c.Guid(),
                    })
                .PrimaryKey(t => t.AvondstudieSoortId);
            
            CreateTable(
                "dbo.Avondstudies",
                c => new
                    {
                        AvondstudieId = c.Guid(nullable: false),
                        MaandagAvondstudieSoortId = c.Int(nullable: false),
                        DinsdagAvondstudieSoortId = c.Int(nullable: false),
                        DonderdagAvondstudieSoortId = c.Int(nullable: false),
                        VrijdagAvondstudieSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AvondstudieId);
            
            CreateTable(
                "dbo.BeperkingSoorts",
                c => new
                    {
                        BeperkingSoortId = c.Int(nullable: false, identity: true),
                        BeperkingSoortNaam = c.String(nullable: false),
                        IsVerwittigDirectie = c.Boolean(nullable: false),
                        IsVerslagNodig = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BeperkingSoortId);
            
            CreateTable(
                "dbo.Beperkings",
                c => new
                    {
                        BeperkingId = c.Guid(nullable: false),
                        NaamAlternatief = c.String(),
                        HeeftAttest = c.Boolean(nullable: false),
                        IsAttestIngediend = c.Boolean(),
                        IsVerslagIngediend = c.Boolean(),
                        IsMDecreet = c.Boolean(),
                        MDecreetMaatregelen = c.String(),
                        LeerlingId = c.Guid(nullable: false),
                        BeperkingSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BeperkingId);
            
            AddColumn("dbo.BijkomendeInfoes", "TaalMoederTaalSoortId", c => c.Int(nullable: false));
            AddColumn("dbo.BijkomendeInfoes", "FamilialeSituatie", c => c.String());
            AddColumn("dbo.Inschrijvings", "AvonstudieId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Leerlings", "LeerlingId", "dbo.Emails");
            DropForeignKey("dbo.Leerlings", "LeerlingId", "dbo.BijkomendeInfoes");
            DropIndex("dbo.Leerlings", new[] { "LeerlingId" });
            DropColumn("dbo.BijkomendeInfoes", "Taalproblemen");
            DropColumn("dbo.Inschrijvings", "IsAvondstudie");
            DropColumn("dbo.Leerlings", "BijkomendeInfoId");
            CreateIndex("dbo.BijkomendeInfoBeperkings", "Beperking_BeperkingId");
            CreateIndex("dbo.BijkomendeInfoBeperkings", "BijkomendeInfo_BijkomendeInfoId");
            CreateIndex("dbo.BijkomendeInfoes", "TaalMoederTaalSoortId");
            CreateIndex("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId3");
            CreateIndex("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId2");
            CreateIndex("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId1");
            CreateIndex("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId");
            CreateIndex("dbo.Beperkings", "BeperkingSoortId");
            CreateIndex("dbo.Beperkings", "LeerlingId");
            CreateIndex("dbo.Emails", "EmailId");
            AddForeignKey("dbo.Emails", "EmailId", "dbo.Leerlings", "LeerlingId");
            AddForeignKey("dbo.BijkomendeInfoes", "TaalMoederTaalSoortId", "dbo.TaalSoorts", "TaalSoortId", cascadeDelete: true);
            AddForeignKey("dbo.BijkomendeInfoBeperkings", "Beperking_BeperkingId", "dbo.Beperkings", "BeperkingId", cascadeDelete: true);
            AddForeignKey("dbo.BijkomendeInfoBeperkings", "BijkomendeInfo_BijkomendeInfoId", "dbo.BijkomendeInfoes", "BijkomendeInfoId", cascadeDelete: true);
            AddForeignKey("dbo.Inschrijvings", "InschrijvingId", "dbo.Avondstudies", "AvondstudieId");
            AddForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId3", "dbo.Avondstudies", "AvondstudieId");
            AddForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId2", "dbo.Avondstudies", "AvondstudieId");
            AddForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId1", "dbo.Avondstudies", "AvondstudieId");
            AddForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId", "dbo.Avondstudies", "AvondstudieId");
            AddForeignKey("dbo.Beperkings", "LeerlingId", "dbo.Leerlings", "LeerlingId", cascadeDelete: true);
            AddForeignKey("dbo.Beperkings", "BeperkingSoortId", "dbo.BeperkingSoorts", "BeperkingSoortId", cascadeDelete: true);
        }
    }
}
