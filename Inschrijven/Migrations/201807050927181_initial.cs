namespace Inschrijven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AanschrijvingSoorts",
                c => new
                    {
                        AanschrijvingSoortId = c.Int(nullable: false, identity: true),
                        Aanspreking = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AanschrijvingSoortId);
            
            CreateTable(
                "dbo.Adres",
                c => new
                    {
                        AdresId = c.Guid(nullable: false),
                        Straat = c.String(nullable: false),
                        Huisnummer = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Gemeente = c.String(nullable: false),
                        Deelgemeente = c.String(),
                        IsDomicilie = c.Boolean(nullable: false),
                        IsAanschrijf = c.Boolean(nullable: false),
                        IsInternaat = c.Boolean(nullable: false),
                        Aanschrijving_AanschrijvingSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdresId)
                .ForeignKey("dbo.AanschrijvingSoorts", t => t.Aanschrijving_AanschrijvingSoortId, cascadeDelete: true)
                .Index(t => t.Aanschrijving_AanschrijvingSoortId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Guid(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Familienaam = c.String(nullable: false),
                        Opmerking = c.String(),
                        IsOverleden = c.Boolean(),
                        Adres_AdresId = c.Guid(nullable: false),
                        Email_EmailId = c.Guid(nullable: false),
                        Relatie_RelatieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Adres", t => t.Adres_AdresId)
                .ForeignKey("dbo.Emails", t => t.Email_EmailId)
                .ForeignKey("dbo.RelatieSoorts", t => t.Relatie_RelatieId, cascadeDelete: true)
                .Index(t => t.Adres_AdresId)
                .Index(t => t.Email_EmailId)
                .Index(t => t.Relatie_RelatieId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Guid(nullable: false),
                        EmailAdres = c.String(),
                    })
                .PrimaryKey(t => t.EmailId);
            
            CreateTable(
                "dbo.Leerlings",
                c => new
                    {
                        LeerlingId = c.Guid(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Familienaam = c.String(nullable: false),
                        Geboortedatum = c.DateTime(nullable: false),
                        Geboorteplaats = c.String(nullable: false),
                        Geboorteland = c.String(),
                        Nationaliteit = c.String(nullable: false),
                        RijksregisterNummer = c.String(),
                        Foto = c.Binary(),
                        Email_EmailId = c.Guid(nullable: false),
                        Geslacht_GeslachtId = c.Int(nullable: false),
                        Contact_ContactId = c.Guid(),
                        Adres_AdresId = c.Guid(),
                    })
                .PrimaryKey(t => t.LeerlingId)
                .ForeignKey("dbo.Emails", t => t.Email_EmailId, cascadeDelete: true)
                .ForeignKey("dbo.Geslachts", t => t.Geslacht_GeslachtId, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId)
                .ForeignKey("dbo.Adres", t => t.Adres_AdresId)
                .Index(t => t.Email_EmailId)
                .Index(t => t.Geslacht_GeslachtId)
                .Index(t => t.Contact_ContactId)
                .Index(t => t.Adres_AdresId);
            
            CreateTable(
                "dbo.BijkomendeInfoes",
                c => new
                    {
                        BijkomendeInfoId = c.Guid(nullable: false),
                        Moedertaal = c.String(),
                        MedischeProblemen = c.String(),
                        TaalProblemen = c.String(),
                        LeerProblemen = c.String(),
                        VerhoogdeZorgVraag = c.Boolean(),
                        VerslagBuitengewoonOnderwijs = c.Boolean(),
                        GemotiveerdVerslag = c.Boolean(),
                        OndersteuningsUur = c.Boolean(),
                    })
                .PrimaryKey(t => t.BijkomendeInfoId)
                .ForeignKey("dbo.Leerlings", t => t.BijkomendeInfoId)
                .Index(t => t.BijkomendeInfoId);
            
            CreateTable(
                "dbo.Geslachts",
                c => new
                    {
                        GeslachtId = c.Int(nullable: false, identity: true),
                        GeslachtNaam = c.String(nullable: false),
                        GeslachtAfkorting = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GeslachtId);
            
            CreateTable(
                "dbo.Inschrijvings",
                c => new
                    {
                        InschrijvingId = c.Guid(nullable: false),
                        StartTijd = c.DateTime(nullable: false),
                        IsHerinschrijving = c.Boolean(nullable: false),
                        IsAvondstudie = c.Boolean(nullable: false),
                        IsAkkoordSchoolreglement = c.Boolean(nullable: false),
                        InschrijvingStatus_InschrijvingStatusId = c.Int(nullable: false),
                        Leerkracht_LeerkrachtId = c.Guid(nullable: false),
                        Leerling_LeerlingId = c.Guid(),
                        Maaltijden_MaaltijdenId = c.Guid(),
                        Marketing_MarketingId = c.Guid(),
                        Optie_OptieId = c.Int(),
                        Richting_RichtingId = c.Int(nullable: false),
                        Schooljaar_SchooljaarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InschrijvingId)
                .ForeignKey("dbo.InschrijvingStatus", t => t.InschrijvingStatus_InschrijvingStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Leerkrachts", t => t.Leerkracht_LeerkrachtId, cascadeDelete: true)
                .ForeignKey("dbo.Leerlings", t => t.Leerling_LeerlingId)
                .ForeignKey("dbo.Maaltijdens", t => t.Maaltijden_MaaltijdenId)
                .ForeignKey("dbo.Marketings", t => t.Marketing_MarketingId)
                .ForeignKey("dbo.Opties", t => t.Optie_OptieId)
                .ForeignKey("dbo.Richtings", t => t.Richting_RichtingId, cascadeDelete: true)
                .ForeignKey("dbo.Schooljaars", t => t.Schooljaar_SchooljaarId, cascadeDelete: true)
                .Index(t => t.InschrijvingStatus_InschrijvingStatusId)
                .Index(t => t.Leerkracht_LeerkrachtId)
                .Index(t => t.Leerling_LeerlingId)
                .Index(t => t.Maaltijden_MaaltijdenId)
                .Index(t => t.Marketing_MarketingId)
                .Index(t => t.Optie_OptieId)
                .Index(t => t.Richting_RichtingId)
                .Index(t => t.Schooljaar_SchooljaarId);
            
            CreateTable(
                "dbo.InschrijvingStatus",
                c => new
                    {
                        InschrijvingStatusId = c.Int(nullable: false, identity: true),
                        InschrijvingStatusNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.InschrijvingStatusId);
            
            CreateTable(
                "dbo.Leerkrachts",
                c => new
                    {
                        LeerkrachtId = c.Guid(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Familienaam = c.String(nullable: false),
                        Nummer = c.Int(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.LeerkrachtId);
            
            CreateTable(
                "dbo.Maaltijdens",
                c => new
                    {
                        MaaltijdenId = c.Guid(nullable: false),
                        HeeftMoneySafeAccount = c.Boolean(nullable: false),
                        HeeftMoneySafeKaart = c.Boolean(nullable: false),
                        DinsdagMaaltijdSoort_MaaltijdSoortId = c.Int(nullable: false),
                        DonderdagMaaltijdSoort_MaaltijdSoortId = c.Int(nullable: false),
                        Inschrijving_InschrijvingId = c.Guid(),
                        MaandagMaaltijdSoort_MaaltijdSoortId = c.Int(nullable: false),
                        VrijdagMaaltijdSoort_MaaltijdSoortId = c.Int(nullable: false),
                        WoensdagMaaltijdSoort_MaaltijdSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaaltijdenId)
                .ForeignKey("dbo.MaaltijdSoorts", t => t.DinsdagMaaltijdSoort_MaaltijdSoortId)
                .ForeignKey("dbo.MaaltijdSoorts", t => t.DonderdagMaaltijdSoort_MaaltijdSoortId)
                .ForeignKey("dbo.Inschrijvings", t => t.Inschrijving_InschrijvingId)
                .ForeignKey("dbo.MaaltijdSoorts", t => t.MaandagMaaltijdSoort_MaaltijdSoortId)
                .ForeignKey("dbo.MaaltijdSoorts", t => t.VrijdagMaaltijdSoort_MaaltijdSoortId)
                .ForeignKey("dbo.MaaltijdSoorts", t => t.WoensdagMaaltijdSoort_MaaltijdSoortId)
                .Index(t => t.DinsdagMaaltijdSoort_MaaltijdSoortId)
                .Index(t => t.DonderdagMaaltijdSoort_MaaltijdSoortId)
                .Index(t => t.Inschrijving_InschrijvingId)
                .Index(t => t.MaandagMaaltijdSoort_MaaltijdSoortId)
                .Index(t => t.VrijdagMaaltijdSoort_MaaltijdSoortId)
                .Index(t => t.WoensdagMaaltijdSoort_MaaltijdSoortId);
            
            CreateTable(
                "dbo.MaaltijdSoorts",
                c => new
                    {
                        MaaltijdSoortId = c.Int(nullable: false, identity: true),
                        MaaltijdSoortNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaaltijdSoortId);
            
            CreateTable(
                "dbo.Marketings",
                c => new
                    {
                        MarketingId = c.Guid(nullable: false),
                        LerenKennenSchoolVaria = c.String(),
                        WaaromGekozenSchool = c.String(),
                    })
                .PrimaryKey(t => t.MarketingId);
            
            CreateTable(
                "dbo.LerenKennenSoorts",
                c => new
                    {
                        LerenKennenSoortId = c.Int(nullable: false, identity: true),
                        LerenKennenSoortOmschrijving = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LerenKennenSoortId);
            
            CreateTable(
                "dbo.Opties",
                c => new
                    {
                        OptieId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptieId);
            
            CreateTable(
                "dbo.Richtings",
                c => new
                    {
                        RichtingId = c.Int(nullable: false, identity: true),
                        Jaar = c.Int(nullable: false),
                        Naam = c.String(nullable: false),
                        Capaciteit = c.Int(),
                        AantalEigenLeerlingenIngeschreven = c.Int(),
                    })
                .PrimaryKey(t => t.RichtingId);
            
            CreateTable(
                "dbo.Schooljaars",
                c => new
                    {
                        SchooljaarId = c.Int(nullable: false, identity: true),
                        SchooljaarNaam = c.String(nullable: false),
                        SchooljaarStartDatum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SchooljaarId);
            
            CreateTable(
                "dbo.Toestemmings",
                c => new
                    {
                        ToestemmingId = c.Int(nullable: false, identity: true),
                        IsAkkoord = c.Boolean(nullable: false),
                        ToestemmingSoort_id = c.Int(nullable: false),
                        Inschrijving_InschrijvingId = c.Guid(),
                    })
                .PrimaryKey(t => t.ToestemmingId)
                .ForeignKey("dbo.ToestemmingSoorts", t => t.ToestemmingSoort_id)
                .ForeignKey("dbo.Inschrijvings", t => t.Inschrijving_InschrijvingId)
                .Index(t => t.ToestemmingSoort_id)
                .Index(t => t.Inschrijving_InschrijvingId);
            
            CreateTable(
                "dbo.ToestemmingSoorts",
                c => new
                    {
                        ToestemmingSoortId = c.Int(nullable: false, identity: true),
                        Omschrijving = c.String(nullable: false),
                        IsEnkelVoorEersteGraad = c.Boolean(nullable: false),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ToestemmingSoortId);
            
            CreateTable(
                "dbo.VoorgaandeInschrijvings",
                c => new
                    {
                        VoorgaandeInschrijvingId = c.Guid(nullable: false),
                        Jaar = c.Int(nullable: false),
                        Richting = c.String(nullable: false),
                        Clausulering = c.String(),
                        IsAttestGezien = c.Boolean(nullable: false),
                        IsBaSoAfgegeven = c.Boolean(),
                        BehaaldAttest_AttestSoortId = c.Int(nullable: false),
                        School_SchoolId = c.Int(nullable: false),
                        Schooljaar_SchooljaarId = c.Int(nullable: false),
                        Inschrijving_InschrijvingId = c.Guid(),
                    })
                .PrimaryKey(t => t.VoorgaandeInschrijvingId)
                .ForeignKey("dbo.AttestSoorts", t => t.BehaaldAttest_AttestSoortId)
                .ForeignKey("dbo.Schools", t => t.School_SchoolId)
                .ForeignKey("dbo.Schooljaars", t => t.Schooljaar_SchooljaarId)
                .ForeignKey("dbo.Inschrijvings", t => t.Inschrijving_InschrijvingId)
                .Index(t => t.BehaaldAttest_AttestSoortId)
                .Index(t => t.School_SchoolId)
                .Index(t => t.Schooljaar_SchooljaarId)
                .Index(t => t.Inschrijving_InschrijvingId);
            
            CreateTable(
                "dbo.AttestSoorts",
                c => new
                    {
                        AttestSoortId = c.Int(nullable: false, identity: true),
                        AttestNaam = c.String(nullable: false),
                        IsClausuleringVereist = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AttestSoortId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        SchoolId = c.Int(nullable: false, identity: true),
                        OfficieleNaam = c.String(nullable: false),
                        Bijnaam = c.String(),
                        Adres = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Gemeente = c.String(nullable: false),
                        IsBuitenGewoon = c.Boolean(nullable: false),
                        IsKarelDeGoede = c.Boolean(nullable: false),
                        OnderwijsSoort_OnderwijsSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SchoolId)
                .ForeignKey("dbo.OnderwijsSoorts", t => t.OnderwijsSoort_OnderwijsSoortId, cascadeDelete: true)
                .Index(t => t.OnderwijsSoort_OnderwijsSoortId);
            
            CreateTable(
                "dbo.OnderwijsSoorts",
                c => new
                    {
                        OnderwijsSoortId = c.Int(nullable: false, identity: true),
                        OnderwijsSoortNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OnderwijsSoortId);
            
            CreateTable(
                "dbo.Telefoons",
                c => new
                    {
                        TelefoonId = c.Guid(nullable: false),
                        Nummer = c.String(nullable: false),
                        Opmerking = c.String(),
                        TelefoonSoort_TelefoonSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TelefoonId)
                .ForeignKey("dbo.TelefoonSoorts", t => t.TelefoonSoort_TelefoonSoortId)
                .Index(t => t.TelefoonSoort_TelefoonSoortId);
            
            CreateTable(
                "dbo.TelefoonSoorts",
                c => new
                    {
                        TelefoonSoortId = c.Int(nullable: false, identity: true),
                        TelefoonSoortNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TelefoonSoortId);
            
            CreateTable(
                "dbo.RelatieSoorts",
                c => new
                    {
                        RelatieId = c.Int(nullable: false, identity: true),
                        RelatieNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RelatieId);
            
            CreateTable(
                "dbo.LerenKennens",
                c => new
                    {
                        LerenKennenId = c.Int(nullable: false, identity: true),
                        IsReden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LerenKennenId)
                .ForeignKey("dbo.LerenKennenSoorts", t => t.LerenKennenId)
                .Index(t => t.LerenKennenId);
            
            CreateTable(
                "dbo.TaalSoorts",
                c => new
                    {
                        TaalSoortId = c.Int(nullable: false, identity: true),
                        TaalSoortNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TaalSoortId);
            
            CreateTable(
                "dbo.LeerlingAdres",
                c => new
                    {
                        Leerling_LeerlingId = c.Guid(nullable: false),
                        Adres_AdresId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Leerling_LeerlingId, t.Adres_AdresId })
                .ForeignKey("dbo.Leerlings", t => t.Leerling_LeerlingId, cascadeDelete: true)
                .ForeignKey("dbo.Adres", t => t.Adres_AdresId, cascadeDelete: true)
                .Index(t => t.Leerling_LeerlingId)
                .Index(t => t.Adres_AdresId);
            
            CreateTable(
                "dbo.LeerlingContacts",
                c => new
                    {
                        Leerling_LeerlingId = c.Guid(nullable: false),
                        Contact_ContactId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Leerling_LeerlingId, t.Contact_ContactId })
                .ForeignKey("dbo.Leerlings", t => t.Leerling_LeerlingId, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .Index(t => t.Leerling_LeerlingId)
                .Index(t => t.Contact_ContactId);
            
            CreateTable(
                "dbo.MarketingLerenKennenSoorts",
                c => new
                    {
                        Marketing_MarketingId = c.Guid(nullable: false),
                        LerenKennenSoort_LerenKennenSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Marketing_MarketingId, t.LerenKennenSoort_LerenKennenSoortId })
                .ForeignKey("dbo.Marketings", t => t.Marketing_MarketingId, cascadeDelete: true)
                .ForeignKey("dbo.LerenKennenSoorts", t => t.LerenKennenSoort_LerenKennenSoortId, cascadeDelete: true)
                .Index(t => t.Marketing_MarketingId)
                .Index(t => t.LerenKennenSoort_LerenKennenSoortId);
            
            CreateTable(
                "dbo.RichtingOpties",
                c => new
                    {
                        Richting_RichtingId = c.Int(nullable: false),
                        Optie_OptieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Richting_RichtingId, t.Optie_OptieId })
                .ForeignKey("dbo.Richtings", t => t.Richting_RichtingId, cascadeDelete: true)
                .ForeignKey("dbo.Opties", t => t.Optie_OptieId, cascadeDelete: true)
                .Index(t => t.Richting_RichtingId)
                .Index(t => t.Optie_OptieId);
            
            CreateTable(
                "dbo.LeerlingTelefoons",
                c => new
                    {
                        Leerling_LeerlingId = c.Guid(nullable: false),
                        Telefoon_TelefoonId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Leerling_LeerlingId, t.Telefoon_TelefoonId })
                .ForeignKey("dbo.Leerlings", t => t.Leerling_LeerlingId, cascadeDelete: true)
                .ForeignKey("dbo.Telefoons", t => t.Telefoon_TelefoonId, cascadeDelete: true)
                .Index(t => t.Leerling_LeerlingId)
                .Index(t => t.Telefoon_TelefoonId);
            
            CreateTable(
                "dbo.ContactTelefoons",
                c => new
                    {
                        Contact_ContactId = c.Guid(nullable: false),
                        Telefoon_TelefoonId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contact_ContactId, t.Telefoon_TelefoonId })
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .ForeignKey("dbo.Telefoons", t => t.Telefoon_TelefoonId, cascadeDelete: true)
                .Index(t => t.Contact_ContactId)
                .Index(t => t.Telefoon_TelefoonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LerenKennens", "LerenKennenId", "dbo.LerenKennenSoorts");
            DropForeignKey("dbo.Leerlings", "Adres_AdresId", "dbo.Adres");
            DropForeignKey("dbo.ContactTelefoons", "Telefoon_TelefoonId", "dbo.Telefoons");
            DropForeignKey("dbo.ContactTelefoons", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "Relatie_RelatieId", "dbo.RelatieSoorts");
            DropForeignKey("dbo.Leerlings", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.LeerlingTelefoons", "Telefoon_TelefoonId", "dbo.Telefoons");
            DropForeignKey("dbo.LeerlingTelefoons", "Leerling_LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.Telefoons", "TelefoonSoort_TelefoonSoortId", "dbo.TelefoonSoorts");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "Schooljaar_SchooljaarId", "dbo.Schooljaars");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "School_SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Schools", "OnderwijsSoort_OnderwijsSoortId", "dbo.OnderwijsSoorts");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "BehaaldAttest_AttestSoortId", "dbo.AttestSoorts");
            DropForeignKey("dbo.Toestemmings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropForeignKey("dbo.Toestemmings", "ToestemmingSoort_id", "dbo.ToestemmingSoorts");
            DropForeignKey("dbo.Inschrijvings", "Schooljaar_SchooljaarId", "dbo.Schooljaars");
            DropForeignKey("dbo.Inschrijvings", "Richting_RichtingId", "dbo.Richtings");
            DropForeignKey("dbo.Inschrijvings", "Optie_OptieId", "dbo.Opties");
            DropForeignKey("dbo.RichtingOpties", "Optie_OptieId", "dbo.Opties");
            DropForeignKey("dbo.RichtingOpties", "Richting_RichtingId", "dbo.Richtings");
            DropForeignKey("dbo.Inschrijvings", "Marketing_MarketingId", "dbo.Marketings");
            DropForeignKey("dbo.MarketingLerenKennenSoorts", "LerenKennenSoort_LerenKennenSoortId", "dbo.LerenKennenSoorts");
            DropForeignKey("dbo.MarketingLerenKennenSoorts", "Marketing_MarketingId", "dbo.Marketings");
            DropForeignKey("dbo.Inschrijvings", "Maaltijden_MaaltijdenId", "dbo.Maaltijdens");
            DropForeignKey("dbo.Maaltijdens", "WoensdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "VrijdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "MaandagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropForeignKey("dbo.Maaltijdens", "DonderdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "DinsdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Inschrijvings", "Leerling_LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.Inschrijvings", "Leerkracht_LeerkrachtId", "dbo.Leerkrachts");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingStatus_InschrijvingStatusId", "dbo.InschrijvingStatus");
            DropForeignKey("dbo.Leerlings", "Geslacht_GeslachtId", "dbo.Geslachts");
            DropForeignKey("dbo.Leerlings", "Email_EmailId", "dbo.Emails");
            DropForeignKey("dbo.LeerlingContacts", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.LeerlingContacts", "Leerling_LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.BijkomendeInfoes", "BijkomendeInfoId", "dbo.Leerlings");
            DropForeignKey("dbo.LeerlingAdres", "Adres_AdresId", "dbo.Adres");
            DropForeignKey("dbo.LeerlingAdres", "Leerling_LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.Contacts", "Email_EmailId", "dbo.Emails");
            DropForeignKey("dbo.Contacts", "Adres_AdresId", "dbo.Adres");
            DropForeignKey("dbo.Adres", "Aanschrijving_AanschrijvingSoortId", "dbo.AanschrijvingSoorts");
            DropIndex("dbo.ContactTelefoons", new[] { "Telefoon_TelefoonId" });
            DropIndex("dbo.ContactTelefoons", new[] { "Contact_ContactId" });
            DropIndex("dbo.LeerlingTelefoons", new[] { "Telefoon_TelefoonId" });
            DropIndex("dbo.LeerlingTelefoons", new[] { "Leerling_LeerlingId" });
            DropIndex("dbo.RichtingOpties", new[] { "Optie_OptieId" });
            DropIndex("dbo.RichtingOpties", new[] { "Richting_RichtingId" });
            DropIndex("dbo.MarketingLerenKennenSoorts", new[] { "LerenKennenSoort_LerenKennenSoortId" });
            DropIndex("dbo.MarketingLerenKennenSoorts", new[] { "Marketing_MarketingId" });
            DropIndex("dbo.LeerlingContacts", new[] { "Contact_ContactId" });
            DropIndex("dbo.LeerlingContacts", new[] { "Leerling_LeerlingId" });
            DropIndex("dbo.LeerlingAdres", new[] { "Adres_AdresId" });
            DropIndex("dbo.LeerlingAdres", new[] { "Leerling_LeerlingId" });
            DropIndex("dbo.LerenKennens", new[] { "LerenKennenId" });
            DropIndex("dbo.Telefoons", new[] { "TelefoonSoort_TelefoonSoortId" });
            DropIndex("dbo.Schools", new[] { "OnderwijsSoort_OnderwijsSoortId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "Inschrijving_InschrijvingId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "Schooljaar_SchooljaarId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "School_SchoolId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "BehaaldAttest_AttestSoortId" });
            DropIndex("dbo.Toestemmings", new[] { "Inschrijving_InschrijvingId" });
            DropIndex("dbo.Toestemmings", new[] { "ToestemmingSoort_id" });
            DropIndex("dbo.Maaltijdens", new[] { "WoensdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "VrijdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "MaandagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "Inschrijving_InschrijvingId" });
            DropIndex("dbo.Maaltijdens", new[] { "DonderdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "DinsdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Inschrijvings", new[] { "Schooljaar_SchooljaarId" });
            DropIndex("dbo.Inschrijvings", new[] { "Richting_RichtingId" });
            DropIndex("dbo.Inschrijvings", new[] { "Optie_OptieId" });
            DropIndex("dbo.Inschrijvings", new[] { "Marketing_MarketingId" });
            DropIndex("dbo.Inschrijvings", new[] { "Maaltijden_MaaltijdenId" });
            DropIndex("dbo.Inschrijvings", new[] { "Leerling_LeerlingId" });
            DropIndex("dbo.Inschrijvings", new[] { "Leerkracht_LeerkrachtId" });
            DropIndex("dbo.Inschrijvings", new[] { "InschrijvingStatus_InschrijvingStatusId" });
            DropIndex("dbo.BijkomendeInfoes", new[] { "BijkomendeInfoId" });
            DropIndex("dbo.Leerlings", new[] { "Adres_AdresId" });
            DropIndex("dbo.Leerlings", new[] { "Contact_ContactId" });
            DropIndex("dbo.Leerlings", new[] { "Geslacht_GeslachtId" });
            DropIndex("dbo.Leerlings", new[] { "Email_EmailId" });
            DropIndex("dbo.Contacts", new[] { "Relatie_RelatieId" });
            DropIndex("dbo.Contacts", new[] { "Email_EmailId" });
            DropIndex("dbo.Contacts", new[] { "Adres_AdresId" });
            DropIndex("dbo.Adres", new[] { "Aanschrijving_AanschrijvingSoortId" });
            DropTable("dbo.ContactTelefoons");
            DropTable("dbo.LeerlingTelefoons");
            DropTable("dbo.RichtingOpties");
            DropTable("dbo.MarketingLerenKennenSoorts");
            DropTable("dbo.LeerlingContacts");
            DropTable("dbo.LeerlingAdres");
            DropTable("dbo.TaalSoorts");
            DropTable("dbo.LerenKennens");
            DropTable("dbo.RelatieSoorts");
            DropTable("dbo.TelefoonSoorts");
            DropTable("dbo.Telefoons");
            DropTable("dbo.OnderwijsSoorts");
            DropTable("dbo.Schools");
            DropTable("dbo.AttestSoorts");
            DropTable("dbo.VoorgaandeInschrijvings");
            DropTable("dbo.ToestemmingSoorts");
            DropTable("dbo.Toestemmings");
            DropTable("dbo.Schooljaars");
            DropTable("dbo.Richtings");
            DropTable("dbo.Opties");
            DropTable("dbo.LerenKennenSoorts");
            DropTable("dbo.Marketings");
            DropTable("dbo.MaaltijdSoorts");
            DropTable("dbo.Maaltijdens");
            DropTable("dbo.Leerkrachts");
            DropTable("dbo.InschrijvingStatus");
            DropTable("dbo.Inschrijvings");
            DropTable("dbo.Geslachts");
            DropTable("dbo.BijkomendeInfoes");
            DropTable("dbo.Leerlings");
            DropTable("dbo.Emails");
            DropTable("dbo.Contacts");
            DropTable("dbo.Adres");
            DropTable("dbo.AanschrijvingSoorts");
        }
    }
}
