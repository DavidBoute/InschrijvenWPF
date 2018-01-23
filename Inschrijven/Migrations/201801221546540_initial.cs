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
                        AansprekingSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdresId)
                .ForeignKey("dbo.AanschrijvingSoorts", t => t.AansprekingSoortId, cascadeDelete: true)
                .Index(t => t.AansprekingSoortId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Guid(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Familienaam = c.String(nullable: false),
                        Opmerking = c.String(),
                        IsOverleden = c.Boolean(),
                        AdresId = c.Guid(nullable: false),
                        RelatieId = c.Int(nullable: false),
                        EmailId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Adres", t => t.AdresId, cascadeDelete: true)
                .ForeignKey("dbo.Emails", t => t.EmailId, cascadeDelete: true)
                .ForeignKey("dbo.RelatieSoorts", t => t.RelatieId, cascadeDelete: true)
                .Index(t => t.AdresId)
                .Index(t => t.RelatieId)
                .Index(t => t.EmailId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Guid(nullable: false),
                        EmailAdres = c.String(),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.Leerlings", t => t.EmailId)
                .Index(t => t.EmailId);
            
            CreateTable(
                "dbo.Leerlings",
                c => new
                    {
                        LeerlingId = c.Guid(nullable: false),
                        Voornaam = c.String(nullable: false),
                        Familienaam = c.String(nullable: false),
                        Geboortedatum = c.DateTime(nullable: false),
                        Geboorteplaats = c.String(nullable: false),
                        Nationaliteit = c.String(nullable: false),
                        RijksregisterNummer = c.String(),
                        Foto = c.Binary(),
                        GeslachtId = c.Int(nullable: false),
                        EmailId = c.Guid(nullable: false),
                        InschrijvingId = c.Guid(nullable: false),
                        Geslacht_GeslachtId = c.Int(nullable: false),
                        Inschrijving_InschrijvingId = c.Guid(),
                    })
                .PrimaryKey(t => t.LeerlingId)
                .ForeignKey("dbo.Geslachts", t => t.Geslacht_GeslachtId)
                .ForeignKey("dbo.Inschrijvings", t => t.Inschrijving_InschrijvingId)
                .Index(t => t.Geslacht_GeslachtId)
                .Index(t => t.Inschrijving_InschrijvingId);
            
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
                .PrimaryKey(t => t.BeperkingId)
                .ForeignKey("dbo.BeperkingSoorts", t => t.BeperkingSoortId, cascadeDelete: true)
                .ForeignKey("dbo.Leerlings", t => t.LeerlingId, cascadeDelete: true)
                .Index(t => t.LeerlingId)
                .Index(t => t.BeperkingSoortId);
            
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
                        LeerlingId = c.Guid(nullable: false),
                        LeerkrachtId = c.Guid(nullable: false),
                        RichtingId = c.Int(nullable: false),
                        OptieId = c.Int(nullable: false),
                        SchooljaarId = c.Int(nullable: false),
                        MaaltijdenId = c.Guid(nullable: false),
                        AvonstudieId = c.Guid(nullable: false),
                        InschrijvingStatusId = c.Int(nullable: false),
                        ToestemmingId = c.Guid(nullable: false),
                        MarketingId = c.Guid(nullable: false),
                        InschrijvingStatus_InschrijvingStatusId = c.Int(nullable: false),
                        Optie_OptieId = c.Int(),
                        Richting_RichtingId = c.Int(nullable: false),
                        Schooljaar_SchooljaarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InschrijvingId)
                .ForeignKey("dbo.Avondstudies", t => t.InschrijvingId)
                .ForeignKey("dbo.InschrijvingStatus", t => t.InschrijvingStatus_InschrijvingStatusId)
                .ForeignKey("dbo.Leerkrachts", t => t.InschrijvingId)
                .ForeignKey("dbo.Leerlings", t => t.InschrijvingId)
                .ForeignKey("dbo.Maaltijdens", t => t.InschrijvingId)
                .ForeignKey("dbo.Marketings", t => t.InschrijvingId)
                .ForeignKey("dbo.Opties", t => t.Optie_OptieId)
                .ForeignKey("dbo.Richtings", t => t.Richting_RichtingId)
                .ForeignKey("dbo.Schooljaars", t => t.Schooljaar_SchooljaarId)
                .Index(t => t.InschrijvingId)
                .Index(t => t.InschrijvingStatus_InschrijvingStatusId)
                .Index(t => t.Optie_OptieId)
                .Index(t => t.Richting_RichtingId)
                .Index(t => t.Schooljaar_SchooljaarId);
            
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
                .PrimaryKey(t => t.AvondstudieSoortId)
                .ForeignKey("dbo.Avondstudies", t => t.Avondstudie_AvondstudieId)
                .ForeignKey("dbo.Avondstudies", t => t.Avondstudie_AvondstudieId1)
                .ForeignKey("dbo.Avondstudies", t => t.Avondstudie_AvondstudieId2)
                .ForeignKey("dbo.Avondstudies", t => t.Avondstudie_AvondstudieId3)
                .Index(t => t.Avondstudie_AvondstudieId)
                .Index(t => t.Avondstudie_AvondstudieId1)
                .Index(t => t.Avondstudie_AvondstudieId2)
                .Index(t => t.Avondstudie_AvondstudieId3);
            
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
                        InschrijvingId = c.Guid(nullable: false),
                        MaandagMaaltijdSoortId = c.Int(nullable: false),
                        DinsdagMaaltijdSoortId = c.Int(nullable: false),
                        WoensdagMaaltijdSoortId = c.Int(nullable: false),
                        DonderdagMaaltijdSoortId = c.Int(nullable: false),
                        VrijdagMaaltijdSoortId = c.Int(nullable: false),
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
                        LerenKennenKarelDeGoedeVaria = c.String(),
                    })
                .PrimaryKey(t => t.MarketingId);
            
            CreateTable(
                "dbo.LerenKennenManiers",
                c => new
                    {
                        LerenKennenManierId = c.Int(nullable: false, identity: true),
                        LerenKennenManierOmschrijving = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LerenKennenManierId);
            
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
                        ToestemmingOmschrijving = c.String(nullable: false),
                        IsAkkoord = c.Boolean(nullable: false),
                        LeerlingId = c.Guid(nullable: false),
                        Inschrijving_InschrijvingId = c.Guid(),
                    })
                .PrimaryKey(t => t.ToestemmingId)
                .ForeignKey("dbo.Inschrijvings", t => t.Inschrijving_InschrijvingId)
                .Index(t => t.Inschrijving_InschrijvingId);
            
            CreateTable(
                "dbo.VoorgaandeInschrijvings",
                c => new
                    {
                        VoorgaandeInschrijvingId = c.Guid(nullable: false),
                        Jaar = c.Int(nullable: false),
                        Richting = c.String(nullable: false),
                        Clausulering = c.String(nullable: false),
                        IsAttestGezien = c.Boolean(nullable: false),
                        IsBaSoAfgegeven = c.Boolean(),
                        SchooljaarId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        AttestId = c.Int(nullable: false),
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
                        OnderwijsSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SchoolId);
            
            CreateTable(
                "dbo.OnderwijsSoorts",
                c => new
                    {
                        OnderwijsSoortId = c.Int(nullable: false, identity: true),
                        OnderwijsSoortNaam = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OnderwijsSoortId)
                .ForeignKey("dbo.Schools", t => t.OnderwijsSoortId)
                .Index(t => t.OnderwijsSoortId);
            
            CreateTable(
                "dbo.Telefoons",
                c => new
                    {
                        TelefoonId = c.Guid(nullable: false),
                        Nummer = c.String(nullable: false),
                        Opmerking = c.String(),
                        TelefoonSoortId = c.Int(nullable: false),
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
                "dbo.BijkomendeInfoes",
                c => new
                    {
                        BijkomendeInfoId = c.Guid(nullable: false),
                        MedischeProblemen = c.String(),
                        FamilialeSituatie = c.String(),
                        TaalMoederTaalSoortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BijkomendeInfoId)
                .ForeignKey("dbo.TaalSoorts", t => t.TaalMoederTaalSoortId, cascadeDelete: true)
                .Index(t => t.TaalMoederTaalSoortId);
            
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
                "dbo.MarketingLerenKennenManiers",
                c => new
                    {
                        Marketing_MarketingId = c.Guid(nullable: false),
                        LerenKennenManier_LerenKennenManierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Marketing_MarketingId, t.LerenKennenManier_LerenKennenManierId })
                .ForeignKey("dbo.Marketings", t => t.Marketing_MarketingId, cascadeDelete: true)
                .ForeignKey("dbo.LerenKennenManiers", t => t.LerenKennenManier_LerenKennenManierId, cascadeDelete: true)
                .Index(t => t.Marketing_MarketingId)
                .Index(t => t.LerenKennenManier_LerenKennenManierId);
            
            CreateTable(
                "dbo.MarketingLerenKennenManier1",
                c => new
                    {
                        Marketing_MarketingId = c.Guid(nullable: false),
                        LerenKennenManier_LerenKennenManierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Marketing_MarketingId, t.LerenKennenManier_LerenKennenManierId })
                .ForeignKey("dbo.Marketings", t => t.Marketing_MarketingId, cascadeDelete: true)
                .ForeignKey("dbo.LerenKennenManiers", t => t.LerenKennenManier_LerenKennenManierId, cascadeDelete: true)
                .Index(t => t.Marketing_MarketingId)
                .Index(t => t.LerenKennenManier_LerenKennenManierId);
            
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
            
            CreateTable(
                "dbo.BijkomendeInfoBeperkings",
                c => new
                    {
                        BijkomendeInfo_BijkomendeInfoId = c.Guid(nullable: false),
                        Beperking_BeperkingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BijkomendeInfo_BijkomendeInfoId, t.Beperking_BeperkingId })
                .ForeignKey("dbo.BijkomendeInfoes", t => t.BijkomendeInfo_BijkomendeInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Beperkings", t => t.Beperking_BeperkingId, cascadeDelete: true)
                .Index(t => t.BijkomendeInfo_BijkomendeInfoId)
                .Index(t => t.Beperking_BeperkingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BijkomendeInfoes", "TaalMoederTaalSoortId", "dbo.TaalSoorts");
            DropForeignKey("dbo.BijkomendeInfoBeperkings", "Beperking_BeperkingId", "dbo.Beperkings");
            DropForeignKey("dbo.BijkomendeInfoBeperkings", "BijkomendeInfo_BijkomendeInfoId", "dbo.BijkomendeInfoes");
            DropForeignKey("dbo.ContactTelefoons", "Telefoon_TelefoonId", "dbo.Telefoons");
            DropForeignKey("dbo.ContactTelefoons", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "RelatieId", "dbo.RelatieSoorts");
            DropForeignKey("dbo.LeerlingTelefoons", "Telefoon_TelefoonId", "dbo.Telefoons");
            DropForeignKey("dbo.LeerlingTelefoons", "Leerling_LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.Telefoons", "TelefoonSoort_TelefoonSoortId", "dbo.TelefoonSoorts");
            DropForeignKey("dbo.Leerlings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "Schooljaar_SchooljaarId", "dbo.Schooljaars");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "School_SchoolId", "dbo.Schools");
            DropForeignKey("dbo.OnderwijsSoorts", "OnderwijsSoortId", "dbo.Schools");
            DropForeignKey("dbo.VoorgaandeInschrijvings", "BehaaldAttest_AttestSoortId", "dbo.AttestSoorts");
            DropForeignKey("dbo.Toestemmings", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropForeignKey("dbo.Inschrijvings", "Schooljaar_SchooljaarId", "dbo.Schooljaars");
            DropForeignKey("dbo.Inschrijvings", "Richting_RichtingId", "dbo.Richtings");
            DropForeignKey("dbo.Inschrijvings", "Optie_OptieId", "dbo.Opties");
            DropForeignKey("dbo.RichtingOpties", "Optie_OptieId", "dbo.Opties");
            DropForeignKey("dbo.RichtingOpties", "Richting_RichtingId", "dbo.Richtings");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingId", "dbo.Marketings");
            DropForeignKey("dbo.MarketingLerenKennenManier1", "LerenKennenManier_LerenKennenManierId", "dbo.LerenKennenManiers");
            DropForeignKey("dbo.MarketingLerenKennenManier1", "Marketing_MarketingId", "dbo.Marketings");
            DropForeignKey("dbo.MarketingLerenKennenManiers", "LerenKennenManier_LerenKennenManierId", "dbo.LerenKennenManiers");
            DropForeignKey("dbo.MarketingLerenKennenManiers", "Marketing_MarketingId", "dbo.Marketings");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingId", "dbo.Maaltijdens");
            DropForeignKey("dbo.Maaltijdens", "WoensdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "VrijdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "MaandagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "Inschrijving_InschrijvingId", "dbo.Inschrijvings");
            DropForeignKey("dbo.Maaltijdens", "DonderdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Maaltijdens", "DinsdagMaaltijdSoort_MaaltijdSoortId", "dbo.MaaltijdSoorts");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingId", "dbo.Leerlings");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingId", "dbo.Leerkrachts");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingStatus_InschrijvingStatusId", "dbo.InschrijvingStatus");
            DropForeignKey("dbo.Inschrijvings", "InschrijvingId", "dbo.Avondstudies");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId3", "dbo.Avondstudies");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId2", "dbo.Avondstudies");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId1", "dbo.Avondstudies");
            DropForeignKey("dbo.AvondstudieSoorts", "Avondstudie_AvondstudieId", "dbo.Avondstudies");
            DropForeignKey("dbo.Leerlings", "Geslacht_GeslachtId", "dbo.Geslachts");
            DropForeignKey("dbo.Emails", "EmailId", "dbo.Leerlings");
            DropForeignKey("dbo.LeerlingContacts", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.LeerlingContacts", "Leerling_LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.Beperkings", "LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.Beperkings", "BeperkingSoortId", "dbo.BeperkingSoorts");
            DropForeignKey("dbo.LeerlingAdres", "Adres_AdresId", "dbo.Adres");
            DropForeignKey("dbo.LeerlingAdres", "Leerling_LeerlingId", "dbo.Leerlings");
            DropForeignKey("dbo.Contacts", "EmailId", "dbo.Emails");
            DropForeignKey("dbo.Contacts", "AdresId", "dbo.Adres");
            DropForeignKey("dbo.Adres", "AansprekingSoortId", "dbo.AanschrijvingSoorts");
            DropIndex("dbo.BijkomendeInfoBeperkings", new[] { "Beperking_BeperkingId" });
            DropIndex("dbo.BijkomendeInfoBeperkings", new[] { "BijkomendeInfo_BijkomendeInfoId" });
            DropIndex("dbo.ContactTelefoons", new[] { "Telefoon_TelefoonId" });
            DropIndex("dbo.ContactTelefoons", new[] { "Contact_ContactId" });
            DropIndex("dbo.LeerlingTelefoons", new[] { "Telefoon_TelefoonId" });
            DropIndex("dbo.LeerlingTelefoons", new[] { "Leerling_LeerlingId" });
            DropIndex("dbo.RichtingOpties", new[] { "Optie_OptieId" });
            DropIndex("dbo.RichtingOpties", new[] { "Richting_RichtingId" });
            DropIndex("dbo.MarketingLerenKennenManier1", new[] { "LerenKennenManier_LerenKennenManierId" });
            DropIndex("dbo.MarketingLerenKennenManier1", new[] { "Marketing_MarketingId" });
            DropIndex("dbo.MarketingLerenKennenManiers", new[] { "LerenKennenManier_LerenKennenManierId" });
            DropIndex("dbo.MarketingLerenKennenManiers", new[] { "Marketing_MarketingId" });
            DropIndex("dbo.LeerlingContacts", new[] { "Contact_ContactId" });
            DropIndex("dbo.LeerlingContacts", new[] { "Leerling_LeerlingId" });
            DropIndex("dbo.LeerlingAdres", new[] { "Adres_AdresId" });
            DropIndex("dbo.LeerlingAdres", new[] { "Leerling_LeerlingId" });
            DropIndex("dbo.BijkomendeInfoes", new[] { "TaalMoederTaalSoortId" });
            DropIndex("dbo.Telefoons", new[] { "TelefoonSoort_TelefoonSoortId" });
            DropIndex("dbo.OnderwijsSoorts", new[] { "OnderwijsSoortId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "Inschrijving_InschrijvingId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "Schooljaar_SchooljaarId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "School_SchoolId" });
            DropIndex("dbo.VoorgaandeInschrijvings", new[] { "BehaaldAttest_AttestSoortId" });
            DropIndex("dbo.Toestemmings", new[] { "Inschrijving_InschrijvingId" });
            DropIndex("dbo.Maaltijdens", new[] { "WoensdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "VrijdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "MaandagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "Inschrijving_InschrijvingId" });
            DropIndex("dbo.Maaltijdens", new[] { "DonderdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.Maaltijdens", new[] { "DinsdagMaaltijdSoort_MaaltijdSoortId" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId3" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId2" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId1" });
            DropIndex("dbo.AvondstudieSoorts", new[] { "Avondstudie_AvondstudieId" });
            DropIndex("dbo.Inschrijvings", new[] { "Schooljaar_SchooljaarId" });
            DropIndex("dbo.Inschrijvings", new[] { "Richting_RichtingId" });
            DropIndex("dbo.Inschrijvings", new[] { "Optie_OptieId" });
            DropIndex("dbo.Inschrijvings", new[] { "InschrijvingStatus_InschrijvingStatusId" });
            DropIndex("dbo.Inschrijvings", new[] { "InschrijvingId" });
            DropIndex("dbo.Beperkings", new[] { "BeperkingSoortId" });
            DropIndex("dbo.Beperkings", new[] { "LeerlingId" });
            DropIndex("dbo.Leerlings", new[] { "Inschrijving_InschrijvingId" });
            DropIndex("dbo.Leerlings", new[] { "Geslacht_GeslachtId" });
            DropIndex("dbo.Emails", new[] { "EmailId" });
            DropIndex("dbo.Contacts", new[] { "EmailId" });
            DropIndex("dbo.Contacts", new[] { "RelatieId" });
            DropIndex("dbo.Contacts", new[] { "AdresId" });
            DropIndex("dbo.Adres", new[] { "AansprekingSoortId" });
            DropTable("dbo.BijkomendeInfoBeperkings");
            DropTable("dbo.ContactTelefoons");
            DropTable("dbo.LeerlingTelefoons");
            DropTable("dbo.RichtingOpties");
            DropTable("dbo.MarketingLerenKennenManier1");
            DropTable("dbo.MarketingLerenKennenManiers");
            DropTable("dbo.LeerlingContacts");
            DropTable("dbo.LeerlingAdres");
            DropTable("dbo.TaalSoorts");
            DropTable("dbo.BijkomendeInfoes");
            DropTable("dbo.RelatieSoorts");
            DropTable("dbo.TelefoonSoorts");
            DropTable("dbo.Telefoons");
            DropTable("dbo.OnderwijsSoorts");
            DropTable("dbo.Schools");
            DropTable("dbo.AttestSoorts");
            DropTable("dbo.VoorgaandeInschrijvings");
            DropTable("dbo.Toestemmings");
            DropTable("dbo.Schooljaars");
            DropTable("dbo.Richtings");
            DropTable("dbo.Opties");
            DropTable("dbo.LerenKennenManiers");
            DropTable("dbo.Marketings");
            DropTable("dbo.MaaltijdSoorts");
            DropTable("dbo.Maaltijdens");
            DropTable("dbo.Leerkrachts");
            DropTable("dbo.InschrijvingStatus");
            DropTable("dbo.AvondstudieSoorts");
            DropTable("dbo.Avondstudies");
            DropTable("dbo.Inschrijvings");
            DropTable("dbo.Geslachts");
            DropTable("dbo.BeperkingSoorts");
            DropTable("dbo.Beperkings");
            DropTable("dbo.Leerlings");
            DropTable("dbo.Emails");
            DropTable("dbo.Contacts");
            DropTable("dbo.Adres");
            DropTable("dbo.AanschrijvingSoorts");
        }
    }
}
