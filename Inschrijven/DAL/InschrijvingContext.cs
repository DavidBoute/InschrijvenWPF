using Inschrijven.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
        public DbSet<AttestSoort> AttestSoorten { get; set; }
        public DbSet<BijkomendeInfo> BijkomendeInfo { get; set; }
        public DbSet<Contact> Contacten { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Geslacht> Geslachten { get; set; }
        public DbSet<Inschrijving> Inschrijvingen { get; set; }
        public DbSet<InschrijvingStatus> InschrijvingStatussen { get; set; }
        public DbSet<Leerkracht> Leerkrachten { get; set; }
        public DbSet<Leerling> Leerlingen { get; set; }
        public DbSet<LerenKennen> LerenKennen { get; set; }
        public DbSet<LerenKennenSoort> LerenKennenSoorten{ get; set; }
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
        public DbSet<ToestemmingSoort> ToestemmingSoorten { get; set; }
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


            adres.HasRequired(x => x.Aanschrijving)
                .WithMany(x => x.Adressen);

            // AttestSoort
            var attestSoort = modelBuilder.Entity<AttestSoort>();

            attestSoort.HasKey(x => x.AttestSoortId);
            attestSoort.Property(x => x.AttestSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            attestSoort.Property(x => x.AttestNaam).IsRequired();
            attestSoort.Property(x => x.IsClausuleringVereist).IsRequired();

            // BijkomendeInfo
            var bijkomendeInfo = modelBuilder.Entity<BijkomendeInfo>();

            bijkomendeInfo.HasKey(x => x.BijkomendeInfoId);
            bijkomendeInfo.Property(x => x.BijkomendeInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            bijkomendeInfo.Property(x => x.MedischeProblemen).IsOptional();
            bijkomendeInfo.Property(x => x.TaalProblemen).IsOptional();
            bijkomendeInfo.Property(x => x.LeerProblemen).IsOptional();
            bijkomendeInfo.Property(x => x.VerhoogdeZorgVraag).IsOptional();
            bijkomendeInfo.Property(x => x.VerslagBuitengewoonOnderwijs).IsOptional();
            bijkomendeInfo.Property(x => x.GemotiveerdVerslag).IsOptional();
            bijkomendeInfo.Property(x => x.OndersteuningsUur).IsOptional();

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
                .WillCascadeOnDelete(false);
            contact.HasRequired(x => x.Relatie)
                .WithMany();
            contact.HasRequired(x => x.Email)
                .WithMany()
                .WillCascadeOnDelete(false);   
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
            inschrijving.Property(x => x.IsAvondstudie).IsRequired();
            inschrijving.Property(x => x.IsAkkoordSchoolreglement).IsRequired();

            inschrijving.HasOptional(x => x.Leerling)
                .WithOptionalDependent(x=> x.Inschrijving);
            inschrijving.HasRequired(x => x.Leerkracht)
                .WithMany();
            inschrijving.HasRequired(x => x.Richting)
                .WithMany();
            inschrijving.HasOptional(x => x.Optie)
                .WithMany();
            inschrijving.HasRequired(x => x.Schooljaar)
                .WithMany();
            inschrijving.HasOptional(x => x.Maaltijden)
                .WithOptionalDependent();
            inschrijving.HasRequired(x => x.InschrijvingStatus)
                .WithMany();
            inschrijving.HasOptional(x => x.Toestemmingen)
                .WithOptionalDependent();
            inschrijving.HasOptional(x => x.Marketing)
                .WithOptionalDependent();

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

            leerling.HasRequired(x => x.Geslacht);
            leerling.HasRequired(x => x.Email);
            leerling.HasMany(x => x.Contacten)
                .WithMany();
            leerling.HasMany(x => x.Adressen)
                .WithMany();
            leerling.HasMany(x => x.TelefoonNummers)
                .WithMany();
            leerling.HasOptional(x => x.BijkomendeInfo)
                .WithRequired();

            // LerenKennen
            var lerenKennen= modelBuilder.Entity<LerenKennen>();

            lerenKennen.HasKey(x => x.LerenKennenId);
            lerenKennen.Property(x => x.LerenKennenId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            lerenKennen.Property(x => x.IsReden).IsRequired();

            lerenKennen.HasRequired(x => x.LerenKennenSoort)
                .WithOptional();


            // LerenKennenSoort
            var lerenKennenSoort = modelBuilder.Entity<LerenKennenSoort>();

            lerenKennenSoort.HasKey(x => x.LerenKennenSoortId);
            lerenKennenSoort.Property(x => x.LerenKennenSoortId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            lerenKennenSoort.Property(x => x.LerenKennenSoortOmschrijving).IsRequired();

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
            marketing.Property(x => x.WaaromGekozenSchool).IsOptional();

            marketing.HasMany(x => x.LerenKennenSchool)
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
                .WithMany();

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

            toestemming.Property(x => x.IsAkkoord).IsRequired();

            toestemming.HasRequired(x => x.ToestemmingSoort)
                .WithOptional();

            // ToestemmingSoort
            var toestemmingSoort = modelBuilder.Entity<ToestemmingSoort>();

            toestemmingSoort.HasKey(x => x.ToestemmingSoortId);
            toestemmingSoort.Property(x => x.Omschrijving).IsRequired();
            toestemmingSoort.Property(x => x.IsEnkelVoorEersteGraad).IsRequired();
            toestemmingSoort.Property(x => x.Code).IsRequired();


            // VoorgaandeInschrijving
            var voorgaandeInschrijving = modelBuilder.Entity<VoorgaandeInschrijving>();

            voorgaandeInschrijving.HasKey(x => x.VoorgaandeInschrijvingId);
            voorgaandeInschrijving.Property(x => x.VoorgaandeInschrijvingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            voorgaandeInschrijving.Property(x => x.Jaar).IsRequired();
            voorgaandeInschrijving.Property(x => x.Richting).IsRequired();
            voorgaandeInschrijving.Property(x => x.Clausulering).IsOptional();
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
