namespace Inschrijven.Migrations
{
    using Inschrijven.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Inschrijven.DAL.InschrijvingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Inschrijven.DAL.InschrijvingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // AanschrijvingSoort
            var aanschrijvingSoorten = context.AanschrijvingSoorten;

            aanschrijvingSoorten.AddOrUpdate(new AanschrijvingSoort[]
            {
                new AanschrijvingSoort { Aanspreking = "Aan de heer en mevrouw" },
                new AanschrijvingSoort { Aanspreking = "Aan de heer" },
                new AanschrijvingSoort { Aanspreking = "Aan mevrouw" }
            });


            // AttestSoort
            var attestSoorten = context.Attesten;

            attestSoorten.AddOrUpdate(new AttestSoort[]
            {
                new AttestSoort { AttestNaam = "A" },
                new AttestSoort { AttestNaam = "B" },
                new AttestSoort { AttestNaam = "C" },
                new AttestSoort { AttestNaam = "Diploma" },
                new AttestSoort { AttestNaam = "Studiegetuigschrift" },
                new AttestSoort { AttestNaam = "Andere" }
            });

            // AvondstudieSoort
            var avondstudieSoorten = context.AvondstudieSoorten;

            avondstudieSoorten.AddOrUpdate(new AvondstudieSoort[]
            {
                new AvondstudieSoort { AvondstudieSoortNaam = "tot 17 u.", IsVoorzienOpVrijdag = true },
                new AvondstudieSoort { AvondstudieSoortNaam = "tot 17.30 u.", IsVoorzienOpVrijdag = false },
                new AvondstudieSoort { AvondstudieSoortNaam = "tot 18 u.", IsVoorzienOpVrijdag = false }
            });


            // BeperkingSoort
            var beperkingSoorten = context.BeperkingSoorten;

            beperkingSoorten.AddOrUpdate(new BeperkingSoort[]
            {
                new BeperkingSoort{BeperkingSoortNaam = "Dyslexie", IsVerslagNodig = false, IsVerwittigDirectie = false},
                new BeperkingSoort{BeperkingSoortNaam = "Dyscalculie", IsVerslagNodig = false, IsVerwittigDirectie = false},
                new BeperkingSoort{BeperkingSoortNaam = "ADHD", IsVerslagNodig = false, IsVerwittigDirectie = false},
                new BeperkingSoort{BeperkingSoortNaam = "ASS", IsVerslagNodig = true, IsVerwittigDirectie = true},
                // TODO: lijstje uitbreiden
            });

            // Geslacht
            var geslachten = context.Geslachten;

            geslachten.AddOrUpdate(new Geslacht[]
            {
                new Geslacht{GeslachtNaam = "Mannelijk", GeslachtAfkorting = "M"},
                new Geslacht{GeslachtNaam = "Vrouwelijk", GeslachtAfkorting = "V"},
                new Geslacht{GeslachtNaam = "Andere", GeslachtAfkorting = "A"}, // TODO: checken offici�le gegevens
            });


            // InschrijvingStatus
            var inschrijvingStatussen = context.InschrijvingStatussen;

            inschrijvingStatussen.AddOrUpdate(new InschrijvingStatus[]
            {
                new InschrijvingStatus{InschrijvingStatusNaam = "gerealiseerd"},
                new InschrijvingStatus{InschrijvingStatusNaam = "onder voorbehoud"},
                new InschrijvingStatus{InschrijvingStatusNaam = "niet gerealiseerd"},
            });

            // Leerkracht
            var leerkrachten = context.Leerkrachten;

            leerkrachten.AddOrUpdate(new Leerkracht[]
            {
                new Leerkracht{Voornaam = "David", Familienaam = "Boute", Code = "DBT", Nummer = 0, LeerkrachtId = Guid.Parse("{4452533A-5ADA-4163-926C-464252E4CD56}")},
                new Leerkracht{Voornaam = "Brecht", Familienaam = "Dewilde", Code = "BDW", Nummer = 0, LeerkrachtId = Guid.Parse("{5667C059-17A8-4A7B-97AF-C01AB0F8B851}")},
                // TODO: lijstje uitbreiden
            });

            // LerenKennenManier
            var lerenKennenManieren = context.LerenKennenManieren;

            lerenKennenManieren.AddOrUpdate(new LerenKennenManier[]
            {
                new LerenKennenManier{LerenKennenManierOmschrijving = "website"},
                new LerenKennenManier{LerenKennenManierOmschrijving = "mond aan mond"},
                new LerenKennenManier{LerenKennenManierOmschrijving = "flyers"},
                new LerenKennenManier{LerenKennenManierOmschrijving = "CLB"},
                // TODO: lijstje uitbreiden
            });

            // MaaltijdSoort
            var maaltijdSoorten = context.MaaltijdSoorten;

            maaltijdSoorten.AddOrUpdate(new MaaltijdSoort[]
            {
                new MaaltijdSoort{MaaltijdSoortNaam = "thuis"},
                new MaaltijdSoort{MaaltijdSoortNaam = "warme maaltijd"},
                new MaaltijdSoort{MaaltijdSoortNaam = "broodmaaltijd"},
                new MaaltijdSoort{MaaltijdSoortNaam = "in de stad"},
            });

            // Optie

            //TODO: systeem uitdenken, wss na commit pas linken

            // OnderwijsSoort
            var onderwijsSoorten = context.OnderwijsSoorten;

            onderwijsSoorten.AddOrUpdate(new OnderwijsSoort[]
            {
                new OnderwijsSoort{OnderwijsSoortNaam = "lager onderwijs"},
                new OnderwijsSoort{OnderwijsSoortNaam = "secundair onderwijs"},
                new OnderwijsSoort{OnderwijsSoortNaam = "hoger onderwijs"},
                new OnderwijsSoort{OnderwijsSoortNaam = "buitengewoon onderwijs"},
            });

            // RelatieSoort
            var relatieSoorten = context.RelatieSoorten;

            relatieSoorten.AddOrUpdate(new RelatieSoort[]
            {
                new RelatieSoort{RelatieNaam = "vader"},
                new RelatieSoort{RelatieNaam = "moeder"},
                new RelatieSoort{RelatieNaam = "voogd"},
                new RelatieSoort{RelatieNaam = "begeleider"},
            });

            // Richting
            var richtingen = context.Richtingen;

            richtingen.AddOrUpdate(new Richting[]
            {
                new Richting{Jaar = 1 , Naam = "Handel"},
                new Richting{Jaar = 2 , Naam = "Handel"},
                new Richting{Jaar = 3 , Naam = "Ondernemen en IT"},
                new Richting{Jaar = 3 , Naam = "Ondernemen en Communicatie"},
                new Richting{Jaar = 3 , Naam = "Office"},
                new Richting{Jaar = 3 , Naam = "Toerisme"},
                // TODO: lijstje uitbreiden
            });

            // Schooljaar
            var schooljaren = context.Schooljaren;

            schooljaren.AddOrUpdate(new Schooljaar[]
            {
                new Schooljaar{SchooljaarNaam = "2016-2017", SchooljaarStartDatum = new DateTime(2016,9,1)},
                new Schooljaar{SchooljaarNaam = "2017-2018", SchooljaarStartDatum = new DateTime(2017,9,1)},
                new Schooljaar{SchooljaarNaam = "2018-2019", SchooljaarStartDatum = new DateTime(2018,9,1)},
            });

            // TaalSoort
            var taalSoorten = context.TaalSoorten;

            taalSoorten.AddOrUpdate(new TaalSoort[]
            {
                new TaalSoort{TaalSoortNaam = "Nederlands"},
                new TaalSoort{TaalSoortNaam = "Frans"},
                new TaalSoort{TaalSoortNaam = "Duits"},
                new TaalSoort{TaalSoortNaam = "Engels"},
                // TODO: lijstje uitbreiden
            });

            // TelefoonSoort
            var telefoonSoorten = context.TelefoonSoorten;

            telefoonSoorten.AddOrUpdate(new TelefoonSoort[]
            {
                new TelefoonSoort{TelefoonSoortNaam="gsm"},
                new TelefoonSoort{TelefoonSoortNaam="thuistelefoon"},
                new TelefoonSoort{TelefoonSoortNaam="werk"},
            });

            // Commit to database
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}