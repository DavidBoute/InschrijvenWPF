﻿using Inschrijven.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.DAL
{
    public class InschrijvingContext : DbContext
    {
        public InschrijvingContext() : base("InschrijvingContext")
        {
        }

        // Oplijsten DBSets
        public DbSet<AanschrijvingSoort> AanschrijvingSoorten { get; set; }
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<AttestSoort> Attesten { get; set; }
        public DbSet<Avondstudie> Avondstudie { get; set; }
        public DbSet<AvondstudieSoort> AvondstudieSoorten { get; set; }
        public DbSet<Beperking> Beperkingen { get; set; }
        public DbSet<BeperkingSoort> BeperkingSoorten { get; set; }
        public DbSet<BijkomendeInfo> BijkomendeInfo { get; set; }
        public DbSet<Contact> Contacten { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Geslacht> Geslachten { get; set; }
        public DbSet<Inschrijving> Inschrijvingen { get; set; }
        public DbSet<InschrijvingStatus> InschrijvingStatussen { get; set; }
        public DbSet<Leerkracht> Leerkrachten { get; set; }
        public DbSet<Leerling> Leerlingen { get; set; }
        public DbSet<LerenKennenManier> LerenKennenManieren { get; set; }
        public DbSet<Maaltijden> Maaltijden { get; set; }
        public DbSet<MaaltijdSoort> MaaltijdSoorten { get; set; }
        public DbSet<Marketing> Marketing { get; set; }
        public DbSet<OnderwijsSoort> OnderwijsSoorten { get; set; }
        public DbSet<Optie> Opties { get; set; }
        public DbSet<RelatieSoort> RelatieSoorten { get; set; }
        public DbSet<Richting> Richtingen { get; set; }
        public DbSet<School> Scholen { get; set; }
        public DbSet<Schooljaar> Schooljaren { get; set; }
        public DbSet<TaalSoort> TaalSoorten { get; set; }
        public DbSet<Telefoon> Telefoons { get; set; }
        public DbSet<TelefoonSoort> TelefoonSoorten { get; set; }
        public DbSet<Toestemming> Toestemmingen { get; set; }
        public DbSet<VoorgaandeInschrijving> VoorgaandeInschrijvingen { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // AanschrijvingSoort
            var aanschrijving = modelBuilder.Entity<AanschrijvingSoort>();

            aanschrijving.HasKey(x => x.AanschrijvingSoortId);
            aanschrijving.Property(x => x.AanschrijvingSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            aanschrijving.Property(x => x.Aanspreking).IsRequired();

            // Adres
            var adres = modelBuilder.Entity<Adres>();

            adres.HasKey(x => x.AdresId);
            adres.Property(x => x.AdresId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            adres.Property(x => x.Straat).IsRequired();
            adres.Property(x => x.Huisnummer).IsRequired();
            adres.Property(x => x.Postcode).IsRequired();
            adres.Property(x => x.Gemeente).IsRequired();
            adres.Property(x => x.Deelgemeente).IsOptional();
            adres.Property(x => x.IsDomicilie).IsRequired();
            adres.Property(x => x.IsAanschrijf).IsRequired();
            adres.Property(x => x.IsInternaat).IsRequired();

            //adres.HasMany(x => x.Leerlingen)
            //    .WithMany(x => x.Adressen);
            //adres.HasMany(x => x.Contacten)
            //    .WithOptional(x => x.Adres);
            adres.HasRequired(x => x.Aanschrijving)
                .WithMany(x => x.Adressen)
                .HasForeignKey(x => x.AansprekingSoortId);

            // AttestSoort
            var attestSoort = modelBuilder.Entity<AttestSoort>();

            attestSoort.HasKey(x => x.AttestSoortId);
            attestSoort.Property(x => x.AttestSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            attestSoort.Property(x => x.AttestNaam).IsRequired();

            // Avondstudie
            var avondstudie = modelBuilder.Entity<Avondstudie>();

            avondstudie.HasKey(x => x.AvondstudieId);
            avondstudie.Property(x => x.AvondstudieId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            avondstudie.HasOptional(x => x.MaandagAvondstudieSoort)
                .WithOptionalPrincipal();
            avondstudie.HasOptional(x => x.DinsdagAvondstudieSoort)
                .WithOptionalPrincipal();
            avondstudie.HasOptional(x => x.DonderdagAvondstudieSoort)
                .WithOptionalPrincipal();
            avondstudie.HasOptional(x => x.VrijdagAvondstudieSoort)
                .WithOptionalPrincipal();

            // AvondstudieSoort
            var avondstudieSoort = modelBuilder.Entity<AvondstudieSoort>();

            avondstudieSoort.HasKey(x => x.AvondstudieSoortId);
            avondstudieSoort.Property(x => x.AvondstudieSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            avondstudieSoort.Property(x => x.AvondstudieSoortNaam).IsRequired();
            avondstudieSoort.Property(x => x.IsVoorzienOpVrijdag).IsRequired();

            // Beperking
            var beperking = modelBuilder.Entity<Beperking>();

            beperking.HasKey(x => x.BeperkingId);
            beperking.Property(x => x.BeperkingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            beperking.Property(x => x.NaamAlternatief).IsOptional();
            beperking.Property(x => x.HeeftAttest).IsRequired();
            beperking.Property(x => x.IsAttestIngediend).IsOptional();
            beperking.Property(x => x.IsVerslagIngediend).IsOptional();
            beperking.Property(x => x.IsMDecreet).IsOptional();
            beperking.Property(x => x.MDecreetMaatregelen).IsOptional();

            //beperking.HasRequired(x => x.Leerling)
            //    .WithMany(x => x.Beperkingen)
            //    .HasForeignKey(x=> x.LeerlingId);
            beperking.HasRequired(x => x.BeperkingSoort)
                .WithMany();

            // BeperkingSoort
            var beperkingSoort = modelBuilder.Entity<BeperkingSoort>();

            beperkingSoort.HasKey(x => x.BeperkingSoortId);
            beperkingSoort.Property(x => x.BeperkingSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            beperkingSoort.Property(x => x.BeperkingSoortNaam).IsRequired();
            beperkingSoort.Property(x => x.IsVerslagNodig).IsRequired();
            beperkingSoort.Property(x => x.IsVerwittigDirectie).IsRequired();

            // BijkomendeInfo
            var bijkomendeInfo = modelBuilder.Entity<BijkomendeInfo>();

            bijkomendeInfo.HasKey(x => x.BijkomendeInfoId);
            bijkomendeInfo.Property(x => x.BijkomendeInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            bijkomendeInfo.Property(x => x.MedischeProblemen).IsOptional();
            bijkomendeInfo.Property(x => x.FamilialeSituatie).IsOptional();

            bijkomendeInfo.HasRequired(x => x.TaalMoeder)
                .WithMany();
            bijkomendeInfo.HasMany(x => x.BeperkingLijst)
                .WithMany();

            // Contact
            var contact = modelBuilder.Entity<Contact>();

            contact.HasKey(x => x.ContactId);
            contact.Property(x => x.ContactId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            contact.Property(x => x.Voornaam).IsRequired();
            contact.Property(x => x.Familienaam).IsRequired();
            contact.Property(x => x.Opmerking).IsOptional();
            contact.Property(x => x.IsOverleden).IsOptional();

            contact.HasRequired(x => x.Adres)
                .WithMany(x => x.Contacten)
                .HasForeignKey(x => x.AdresId);
            contact.HasRequired(x => x.Relatie)
                .WithMany()
                .HasForeignKey(x => x.RelatieId);
            contact.HasRequired(x => x.Email)
                .WithMany()
                .HasForeignKey(x => x.EmailId);
            //contact.HasMany(x => x.Leerlingen)
            //    .WithMany(x => x.Contacten);
            contact.HasMany(x => x.TelefoonNummers)
                .WithMany();

            // Geslacht
            var geslacht = modelBuilder.Entity<Geslacht>();

            geslacht.HasKey(x => x.GeslachtId);
            geslacht.Property(x => x.GeslachtId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            geslacht.Property(x => x.GeslachtNaam).IsRequired();
            geslacht.Property(x => x.GeslachtAfkorting).IsRequired();

            // Inschrijving
            var inschrijving = modelBuilder.Entity<Inschrijving>();

            inschrijving.HasKey(x => x.InschrijvingId);
            inschrijving.Property(x => x.InschrijvingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            inschrijving.Property(x => x.StartTijd).IsRequired();
            inschrijving.Property(x => x.IsHerinschrijving).IsRequired();

            inschrijving.HasRequired(x => x.Leerling)
                .WithRequiredDependent();
            inschrijving.HasRequired(x => x.Leerkracht)
                .WithRequiredDependent();
            inschrijving.HasRequired(x => x.Richting)
                .WithOptional();
            inschrijving.HasOptional(x => x.Optie)
                .WithOptionalDependent();
            inschrijving.HasRequired(x => x.Schooljaar)
                .WithOptional();
            inschrijving.HasRequired(x => x.Maaltijden)
                .WithRequiredDependent();
            inschrijving.HasRequired(x => x.Avondstudie)
                .WithRequiredDependent();
            inschrijving.HasRequired(x => x.InschrijvingStatus)
                .WithOptional();
            inschrijving.HasRequired(x => x.Toestemmingen)
                .WithOptional();
            inschrijving.HasRequired(x => x.Marketing)
                .WithOptional();

            inschrijving.HasMany(x => x.VoorgaandeInschrijvingen)
                .WithOptional();
            inschrijving.HasMany(x => x.Toestemmingen)
                .WithOptional();

            // InschrijvingStatus
            var inschrijvingStatus = modelBuilder.Entity<InschrijvingStatus>();

            inschrijvingStatus.HasKey(x => x.InschrijvingStatusId);
            inschrijvingStatus.Property(x => x.InschrijvingStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            inschrijvingStatus.Property(x => x.InschrijvingStatusNaam).IsRequired();

            // Leerkracht
            var leerkracht = modelBuilder.Entity<Leerkracht>();

            leerkracht.HasKey(x => x.LeerkrachtId);
            leerkracht.Property(x => x.LeerkrachtId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            leerkracht.Property(x => x.Voornaam).IsRequired();
            leerkracht.Property(x => x.Familienaam).IsRequired();
            leerkracht.Property(x => x.Nummer).IsOptional();
            leerkracht.Property(x => x.Code).IsOptional();

            // Leerling
            var leerling = modelBuilder.Entity<Leerling>();

            leerling.HasKey(x => x.LeerlingId);
            leerling.Property(x => x.LeerlingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            leerling.Property(x => x.Voornaam).IsRequired();
            leerling.Property(x => x.Familienaam).IsRequired();
            leerling.Property(x => x.Geboortedatum).IsRequired();
            leerling.Property(x => x.Geboorteplaats).IsRequired();
            leerling.Property(x => x.Nationaliteit).IsRequired();
            leerling.Property(x => x.RijksregisterNummer).IsOptional();
            leerling.Property(x => x.Foto).IsOptional();

            leerling.HasRequired(x => x.Geslacht)
                .WithOptional();
            leerling.HasRequired(x => x.Email)
                .WithRequiredPrincipal();
            leerling.HasMany(x => x.Contacten)
                .WithMany(x => x.Leerlingen);
            leerling.HasMany(x => x.Adressen)
                .WithMany(x => x.Leerlingen);
            leerling.HasMany(x => x.TelefoonNummers)
                .WithMany();
            leerling.HasMany(x => x.Beperkingen)
                .WithRequired(x => x.Leerling)
                .HasForeignKey(x => x.LeerlingId);

            // LerenKennenManier
            var lerenKennenManier = modelBuilder.Entity<LerenKennenManier>();

            lerenKennenManier.HasKey(x => x.LerenKennenManierId);
            lerenKennenManier.Property(x => x.LerenKennenManierId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            lerenKennenManier.Property(x => x.LerenKennenManierOmschrijving).IsRequired();

            // Maaltijden
            var maaltijden = modelBuilder.Entity<Maaltijden>();

            maaltijden.HasKey(x => x.MaaltijdenId);
            maaltijden.Property(x => x.MaaltijdenId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //maaltijden.HasRequired(x => x.Inschrijving)
            //    .WithRequiredDependent(x => x.Maaltijden);
            maaltijden.HasRequired(x => x.MaandagMaaltijdSoort)
                .WithOptional();
            maaltijden.HasRequired(x => x.DinsdagMaaltijdSoort)
                .WithOptional();
            maaltijden.HasRequired(x => x.WoensdagMaaltijdSoort)
                .WithOptional();
            maaltijden.HasRequired(x => x.DonderdagMaaltijdSoort)
                .WithOptional();
            maaltijden.HasRequired(x => x.VrijdagMaaltijdSoort)
                .WithOptional();

            // MaaltijdSoort
            var maaltijdSoort = modelBuilder.Entity<MaaltijdSoort>();

            maaltijdSoort.HasKey(x => x.MaaltijdSoortId);
            maaltijdSoort.Property(x => x.MaaltijdSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            maaltijdSoort.Property(x => x.MaaltijdSoortNaam).IsRequired();

            // Marketing
            var marketing = modelBuilder.Entity<Marketing>();

            marketing.HasKey(x => x.MarketingId);
            marketing.Property(x => x.MarketingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            marketing.Property(x => x.LerenKennenSchoolVaria).IsOptional();
            marketing.Property(x => x.LerenKennenKarelDeGoedeVaria).IsOptional();

            marketing.HasMany(x => x.LerenKennenSchool)
                .WithMany();
            marketing.HasMany(x => x.LerenKennenKarelDeGoede)
                .WithMany();

            // OnderwijsSoort
            var onderwijsSoort = modelBuilder.Entity<OnderwijsSoort>();

            onderwijsSoort.HasKey(x => x.OnderwijsSoortId);
            onderwijsSoort.Property(x => x.OnderwijsSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            onderwijsSoort.Property(x => x.OnderwijsSoortNaam).IsRequired();
            
            // Optie
            var optie = modelBuilder.Entity<Optie>();

            optie.HasKey(x => x.OptieId);
            optie.Property(x => x.OptieId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            optie.Property(x => x.Naam).IsRequired();

            //optie.HasMany(x => x.Richtingen)
            //    .WithMany(x => x.Opties);

            // RelatieSoort
            var relatieSoort = modelBuilder.Entity<RelatieSoort>();

            relatieSoort.HasKey(x => x.RelatieId);
            relatieSoort.Property(x => x.RelatieId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            relatieSoort.Property(x => x.RelatieNaam).IsRequired();

            // Richting
            var richting = modelBuilder.Entity<Richting>();

            richting.HasKey(x => x.RichtingId);
            richting.Property(x => x.RichtingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            richting.Property(x => x.Jaar).IsRequired();
            richting.Property(x => x.Naam).IsRequired();
            richting.Property(x => x.Capaciteit).IsOptional();
            richting.Property(x => x.AantalEigenLeerlingenIngeschreven).IsOptional();

            richting.HasMany(x => x.Opties)
                .WithMany(x => x.Richtingen);

            // School
            var school = modelBuilder.Entity<School>();

            school.HasKey(x => x.SchoolId);
            school.Property(x => x.SchoolId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            school.Property(x => x.OfficieleNaam).IsRequired();
            school.Property(x => x.Bijnaam).IsOptional();
            school.Property(x => x.Adres).IsRequired();
            school.Property(x => x.Postcode).IsRequired();
            school.Property(x => x.Gemeente).IsRequired();
            school.Property(x => x.IsBuitenGewoon).IsRequired();
            school.Property(x => x.IsKarelDeGoede).IsRequired();

            school.HasRequired(x => x.OnderwijsSoort)
                .WithOptional();

            // Schooljaar
            var schooljaar = modelBuilder.Entity<Schooljaar>();

            schooljaar.HasKey(x => x.SchooljaarId);
            schooljaar.Property(x => x.SchooljaarId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            schooljaar.Property(x => x.SchooljaarNaam).IsRequired();
            schooljaar.Property(x => x.SchooljaarStartDatum).IsRequired();

            // TaalSoort
            var taalSoort = modelBuilder.Entity<TaalSoort>();

            taalSoort.HasKey(x => x.TaalSoortId);
            taalSoort.Property(x => x.TaalSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            taalSoort.Property(x => x.TaalSoortNaam).IsRequired();

            // Telefoon
            var telefoon = modelBuilder.Entity<Telefoon>();

            telefoon.HasKey(x => x.TelefoonId);
            telefoon.Property(x => x.TelefoonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            telefoon.Property(x => x.Nummer).IsRequired();
            telefoon.Property(x => x.Opmerking).IsOptional();

            telefoon.HasRequired(x => x.TelefoonSoort)
                .WithOptional();

            // TelefoonSoort
            var telefoonSoort = modelBuilder.Entity<TelefoonSoort>();

            telefoonSoort.HasKey(x => x.TelefoonSoortId);
            telefoonSoort.Property(x => x.TelefoonSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            telefoonSoort.Property(x => x.TelefoonSoortNaam).IsRequired();

            // Toestemming
            var toestemming = modelBuilder.Entity<Toestemming>();

            toestemming.HasKey(x => x.ToestemmingId);
            toestemming.Property(x => x.ToestemmingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            toestemming.Property(x => x.ToestemmingOmschrijving).IsRequired();
            toestemming.Property(x => x.IsAkkoord).IsRequired();

            // VoorgaandeInschrijving
            var voorgaandeInschrijving = modelBuilder.Entity<VoorgaandeInschrijving>();

            voorgaandeInschrijving.HasKey(x => x.VoorgaandeInschrijvingId);
            voorgaandeInschrijving.Property(x => x.VoorgaandeInschrijvingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            voorgaandeInschrijving.Property(x => x.Jaar).IsRequired();
            voorgaandeInschrijving.Property(x => x.Richting).IsRequired();
            voorgaandeInschrijving.Property(x => x.Clausulering).IsRequired();
            voorgaandeInschrijving.Property(x => x.IsAttestGezien).IsRequired();
            voorgaandeInschrijving.Property(x => x.IsBaSoAfgegeven).IsOptional();

            voorgaandeInschrijving.HasRequired(x => x.Schooljaar)
                .WithOptional();

            voorgaandeInschrijving.HasRequired(x => x.School)
                .WithOptional();

            voorgaandeInschrijving.HasRequired(x => x.BehaaldAttest)
                .WithOptional();
        }
    }
}