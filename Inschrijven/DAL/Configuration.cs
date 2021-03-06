namespace Inschrijven.Migrations
{
    using Inschrijven.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
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

            aanschrijvingSoorten.AddOrUpdate(x => x.Aanspreking,
                new AanschrijvingSoort[]
                {
                    new AanschrijvingSoort { Aanspreking = "Aan de heer en mevrouw" },
                    new AanschrijvingSoort { Aanspreking = "Aan de heer" },
                    new AanschrijvingSoort { Aanspreking = "Aan mevrouw" }
                });

            // AttestSoort
            var attestSoorten = context.AttestSoorten;

            attestSoorten.AddOrUpdate(x => x.AttestNaam,
                new AttestSoort[]
                {
                    new AttestSoort { AttestNaam = "A" },
                    new AttestSoort { AttestNaam = "B", IsClausuleringVereist = true },
                    new AttestSoort { AttestNaam = "C" },
                    new AttestSoort { AttestNaam = "Diploma" },
                    new AttestSoort { AttestNaam = "Studiegetuigschrift" },
                    new AttestSoort { AttestNaam = "Andere" }
                });

            // Geslacht
            var geslachten = context.Geslachten;

            geslachten.AddOrUpdate(x => x.GeslachtNaam,
                new Geslacht[]
                {
                    new Geslacht{GeslachtNaam = "Mannelijk", GeslachtAfkorting = "M"},
                    new Geslacht{GeslachtNaam = "Vrouwelijk", GeslachtAfkorting = "V"},
                    new Geslacht{GeslachtNaam = "Andere", GeslachtAfkorting = "A"}, // TODO: checken offici�le gegevens
                });

            // InschrijvingStatus
            var inschrijvingStatussen = context.InschrijvingStatussen;

            inschrijvingStatussen.AddOrUpdate(x => x.InschrijvingStatusNaam,
                new InschrijvingStatus[]
                {
                    new InschrijvingStatus{InschrijvingStatusNaam = "gerealiseerd"},
                    new InschrijvingStatus{InschrijvingStatusNaam = "onder voorbehoud"},
                    new InschrijvingStatus{InschrijvingStatusNaam = "niet gerealiseerd"},
                    new InschrijvingStatus{InschrijvingStatusNaam = "aan het inschrijven"},
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
            var lerenKennenSoorten = context.LerenKennenSoorten;

            lerenKennenSoorten.AddOrUpdate(x => x.LerenKennenSoortOmschrijving,
                new LerenKennenSoort[]
                {
                    new LerenKennenSoort{LerenKennenSoortOmschrijving = "website"},
                    new LerenKennenSoort{LerenKennenSoortOmschrijving = "mond aan mond"},
                    new LerenKennenSoort{LerenKennenSoortOmschrijving = "flyers"},
                    new LerenKennenSoort{LerenKennenSoortOmschrijving = "CLB"},
                    // TODO: lijstje uitbreiden
                });

            // MaaltijdSoort
            var maaltijdSoorten = context.MaaltijdSoorten;

            maaltijdSoorten.AddOrUpdate(x => x.MaaltijdSoortNaam,
                new MaaltijdSoort[]
                {
                    new MaaltijdSoort{MaaltijdSoortNaam = "thuis"},
                    new MaaltijdSoort{MaaltijdSoortNaam = "warme maaltijd"},
                    new MaaltijdSoort{MaaltijdSoortNaam = "broodmaaltijd"},
                    new MaaltijdSoort{MaaltijdSoortNaam = "in de stad"},
                });

            // Optie
            var opties = context.Opties;

            opties.AddOrUpdate(x => x.Naam,
                new Optie[]
                {
                    new Optie { Naam = "Project Ondernemen" },
                    new Optie { Naam = "Project Toerisme" },
                    new Optie { Naam = "Project IT" },
                });
            //toevoegen aan richtingen gebeurt bij de richtingen

            // OnderwijsSoort
            var onderwijsSoorten = context.OnderwijsSoorten;

            onderwijsSoorten.AddOrUpdate(x => x.OnderwijsSoortNaam,
                new OnderwijsSoort[]
                {
                    new OnderwijsSoort{OnderwijsSoortNaam = "lager onderwijs"},
                    new OnderwijsSoort{OnderwijsSoortNaam = "secundair onderwijs"},
                    new OnderwijsSoort{OnderwijsSoortNaam = "hoger onderwijs"},
                    new OnderwijsSoort{OnderwijsSoortNaam = "buitengewoon onderwijs"},
                });

            // RelatieSoort
            var relatieSoorten = context.RelatieSoorten;

            relatieSoorten.AddOrUpdate(x => x.RelatieNaam,
                new RelatieSoort[]
                {
                    new RelatieSoort{RelatieNaam = "vader"},
                    new RelatieSoort{RelatieNaam = "moeder"},
                    new RelatieSoort{RelatieNaam = "voogd"},
                    new RelatieSoort{RelatieNaam = "begeleider"},
                    new RelatieSoort{RelatieNaam = "internaat"},
                });

            // Richting
            var richtingen = context.Richtingen;

            richtingen.AddOrUpdate(x => new { x.Jaar, x.Naam },
                new Richting[]
                {
                    new Richting{Jaar = 1 , Naam = "Handel"},
                    new Richting{Jaar = 2 , Naam = "Handel", Opties = new List<Optie>()},
                    new Richting{Jaar = 3 , Naam = "Ondernemen en IT"},
                    new Richting{Jaar = 3 , Naam = "Ondernemen en Communicatie"},
                    new Richting{Jaar = 3 , Naam = "Office"},
                    new Richting{Jaar = 3 , Naam = "Toerisme"},
                    // TODO: lijstje uitbreiden
                });

            context.SaveChanges();

            richtingen.FirstOrDefault(x => x.Jaar == 2).Opties.Add(opties.FirstOrDefault(x => x.Naam == "Project Ondernemen"));
            richtingen.FirstOrDefault(x => x.Jaar == 2).Opties.Add(opties.FirstOrDefault(x => x.Naam == "Project Toerisme"));
            richtingen.FirstOrDefault(x => x.Jaar == 2).Opties.Add(opties.FirstOrDefault(x => x.Naam == "Project IT"));

            // School
            var scholen = context.Scholen;

            scholen.AddOrUpdate(x => x.OfficieleNaam,
                new School[]
                {
                    new School{
                        OfficieleNaam ="Sint-Jozefsinstituut",
                        Adres = "Zilverstraat 26",
                        Postcode ="8000",
                        Gemeente ="Brugge",
                        Bijnaam ="Jozefienen",
                        IsBuitenGewoon = false,
                        IsKarelDeGoede = true,
                        OnderwijsSoort = context.OnderwijsSoorten.FirstOrDefault(x => x.OnderwijsSoortNaam == "secundair onderwijs")
                    },
                    new School{
                        OfficieleNaam ="Sint-Jozefsinstituut - ASO",
                        Adres = "Noordzandstraat 76",
                        Postcode ="8000",
                        Gemeente ="Brugge",
                        Bijnaam ="Humaniora",
                        IsBuitenGewoon = false,
                        IsKarelDeGoede = true,
                        OnderwijsSoort = context.OnderwijsSoorten.FirstOrDefault(x => x.OnderwijsSoortNaam == "secundair onderwijs")
                    },
                    new School{
                        OfficieleNaam ="Technisch Instituut Heilige Familie",
                        Adres = "Oude Zak 38",
                        Postcode ="8000",
                        Gemeente ="Brugge",
                        Bijnaam ="Maricolen",
                        IsBuitenGewoon = false,
                        IsKarelDeGoede = true,
                        OnderwijsSoort = context.OnderwijsSoorten.FirstOrDefault(x => x.OnderwijsSoortNaam == "secundair onderwijs")
                    },
                });

            // Schooljaar
            var schooljaren = context.Schooljaren;

            schooljaren.AddOrUpdate(x => x.SchooljaarNaam,
                new Schooljaar[]
                {
                    new Schooljaar{SchooljaarNaam = "2016-2017", SchooljaarStartDatum = new DateTime(2016,9,1)},
                    new Schooljaar{SchooljaarNaam = "2017-2018", SchooljaarStartDatum = new DateTime(2017,9,1)},
                    new Schooljaar{SchooljaarNaam = "2018-2019", SchooljaarStartDatum = new DateTime(2018,9,1)},
                });

            // TaalSoort
            var taalSoorten = context.TaalSoorten;

            taalSoorten.AddOrUpdate(x => x.TaalSoortNaam,
                new TaalSoort[]
                {
                    new TaalSoort{TaalSoortNaam = "Nederlands"},
                    new TaalSoort{TaalSoortNaam = "Frans"},
                    new TaalSoort{TaalSoortNaam = "Duits"},
                    new TaalSoort{TaalSoortNaam = "Engels"},
                    // TODO: lijstje uitbreiden
                });

            // TelefoonSoort
            var telefoonSoorten = context.TelefoonSoorten;

            telefoonSoorten.AddOrUpdate(x => x.TelefoonSoortNaam,
                new TelefoonSoort[]
                {
                    new TelefoonSoort{TelefoonSoortNaam="gsm"},
                    new TelefoonSoort{TelefoonSoortNaam="thuistelefoon"},
                    new TelefoonSoort{TelefoonSoortNaam="werk"},
                });

            //ToestemmingSoorten
            var toestemmingSoorten = context.ToestemmingSoorten;

            toestemmingSoorten.AddOrUpdate(x => x.Omschrijving,
                new Model.ToestemmingSoort[]
                {
                    new Model.ToestemmingSoort
                    {
                        Omschrijving = "De ouder(s) / verantwoordelijke(n) gaan akkoord met " +
                                       "het pedagogisch project en het schoolreglement zoals na de te lezen op de schoolwebsite.",
                        IsEnkelVoorEersteGraad = false,
                        Code = "Schoolreglement"
                    },

                    new Model.ToestemmingSoort
                    {
                        Omschrijving = "De ouder(s) / verantwoordelijke(n) gaan er mee akkoord dat de zorgteams " +
                                       "van de basisschool en de secundaire school eventuele verdere informatie over de leerling uitwisselen.",
                        IsEnkelVoorEersteGraad = true,
                        Code = "Zorgteams"
                    },
                    new Model.ToestemmingSoort
                    {
                        Omschrijving = "De ouder(s) / verantwoordelijke(n) gaan er mee akkoord dat de school " +
                                       "een aantal schoolresultaten terugkoppelt naar de basisschool met het oog op kwaliteitsverbetering.",
                        IsEnkelVoorEersteGraad = true,
                        Code = "Schoolresultaten"
                    },
                    new Model.ToestemmingSoort
                    {
                        Omschrijving = "Foto's van de leerling die tijdens klas- en schoolactiviteiten genomen worden " +
                                       "mogen gepubliceerd op / gebruik worden voor de sociale media van de school.",
                        IsEnkelVoorEersteGraad = false,
                        Code = "FotoSocMed"
                    },
                    new Model.ToestemmingSoort
                    {
                        Omschrijving = "Foto's van de leerling die tijdens klas- en schoolactiviteiten genomen worden " +
                                       "mogen gepubliceerd op / gebruik worden voor de schoolwebsite.",
                        IsEnkelVoorEersteGraad = false,
                        Code = "FotoSite"
                    },
                    new Model.ToestemmingSoort
                    {
                        Omschrijving = "Foto's van de leerling die tijdens klas- en schoolactiviteiten genomen worden " +
                                       "mogen gepubliceerd op / gebruik worden voor de schoolbrochure.",
                        IsEnkelVoorEersteGraad = false,
                        Code = "FotoBrochure"
                    },

                });

            // Internaten
            var contacten = context.Contacten;

            contacten.AddOrUpdate(x => x.ContactId,
                new Contact[]
                {
                    new Contact
                    {
                        ContactId = Guid.Parse("{A5E163E3-8CAF-4050-BE47-B3270E20F8A5}"),
                        Voornaam = "Internaat",
                        Familienaam = "Zilverstraat",
                        Adres = context.Adressen.FirstOrDefault(x=>x.AdresId.ToString() == "{20BC59A4-FD27-441B-9043-75FD49F555DB}") ??
                                new Adres
                                {
                                    AdresId = Guid.Parse("{20BC59A4-FD27-441B-9043-75FD49F555DB}"),
                                    IsInternaat = true,
                                    Aanschrijving = aanschrijvingSoorten.First(x=>x.Aanspreking == "Aan mevrouw"),
                                    Straat = "Zilverstraat",
                                    Huisnummer = "26",
                                    Postcode = "8000",
                                    Gemeente = "Brugge"
                                },
                        Relatie = context.RelatieSoorten.First(x=> x.RelatieNaam == "Internaat"),
                        Email = context.Emails.FirstOrDefault(x=> x.EmailId.ToString() == "{E5BA15DE-9C40-4A80-B89D-A2B0CC53003D}") ??
                                new Email
                                {
                                    EmailId = Guid.Parse("{E5BA15DE-9C40-4A80-B89D-A2B0CC53003D}"),
                                    EmailAdres = "internaatsbeheerder@sintjozefbrugge.be"
                                }
                    },
                    new Contact
                    {
                        ContactId = Guid.Parse("{90EB5065-63BC-40E7-87AA-8E8B6B245AE8}"),
                        Voornaam = "Internaat",
                        Familienaam = "Palaestra",
                        Adres = context.Adressen.FirstOrDefault(x=>x.AdresId.ToString() =="{AA8661CE-57D9-4E7A-9EEF-8F159EC8E592}") ??
                                new Adres
                                {
                                    AdresId = Guid.Parse("{A101AADD-1547-4167-82F7-7CB4218B47C7}"),
                                    IsInternaat = true,
                                    Aanschrijving = aanschrijvingSoorten.First(x=>x.Aanspreking == "Aan de heer"),
                                    Straat = "Hauwerstraat",
                                    Huisnummer = "23",
                                    Postcode = "8000",
                                    Gemeente = "Brugge"
                                },
                        Relatie = context.RelatieSoorten.First(x=> x.RelatieNaam == "Internaat"),
                        Email = context.Emails.FirstOrDefault(x=> x.EmailId.ToString() == "{A51AF7FC-D488-4361-A17F-FB96BD4B5986}") ??
                                new Email
                                {
                                    EmailId = Guid.Parse("{A51AF7FC-D488-4361-A17F-FB96BD4B5986}"),
                                    EmailAdres = "info@internaat-palaestra.be"
                                }
                    }
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

            // nieuwe inschrijving om op te testen
            Inschrijving inschrijving = new Inschrijving()
            {
                InschrijvingId = Guid.Parse("3fc434b6-48e4-419d-acc1-f73a51df9ac2"),
                Leerkracht = leerkrachten.Single(x => x.LeerkrachtId.ToString() == "4452533A-5ADA-4163-926C-464252E4CD56"),
                InschrijvingStatus = inschrijvingStatussen.Single(x => x.InschrijvingStatusNaam == "gerealiseerd"),
                Richting = richtingen.Single(x => x.Jaar == 1),
                Schooljaar = schooljaren.Single(x => x.SchooljaarNaam == "2018-2019"),
                StartTijd = new DateTime(2018, 7, 5),

                Leerling = new Leerling()
                {
                    LeerlingId = Guid.Parse("{CBE2CDE4-2673-4C57-BD94-1C205FE80D48}"),
                    Voornaam = "Freddy",
                    Familienaam = "De Testaccount",
                    Geboortedatum = new DateTime(2002, 4, 6),
                    Geboorteland = "Belgi�",
                    Geboorteplaats = "Brugge",
                    Nationaliteit = "Belg",
                    Geslacht = geslachten.Single(x => x.GeslachtAfkorting == "M"),
                    Email = new Email() { EmailAdres = "Freddy.De.Testaccount@gmail.com" },

                    Adressen = new List<Adres>()
                    {
                        new Adres()
                        {
                            AdresId = Guid.Parse("{43891708-FC1A-4FB3-87F2-6693A2E69247}"),
                            Straat = "Staartstraat",
                            Huisnummer = "9",
                            Postcode = "8000",
                            Gemeente = "Brugge",
                            IsDomicilie = true,

                            Aanschrijving = aanschrijvingSoorten.Single(x=> x.Aanspreking == "Aan de heer en mevrouw")
                        }
                    },

                    Contacten = new List<Contact>()
                    {
                        new Contact()
                        {
                            ContactId = Guid.Parse("{B8073FA9-EA65-48A6-82CF-7956C6E0AF40}"),
                            Voornaam = "Ronny",
                            Familienaam = "De Testaccount",
                            Opmerking = "Dit is een opmerking",

                            Adres = new Adres()
                            {
                                AdresId = Guid.Parse("{B0C4C580-EE55-4167-8C10-352AACC0E29F}"),
                                Straat = "Staartstraat",
                                Huisnummer = "10",
                                Postcode = "8000",
                                Gemeente = "Brugge",
                                IsDomicilie = false,

                                Aanschrijving = aanschrijvingSoorten.Single(x=> x.Aanspreking == "Aan de heer")
                            },

                            Relatie = relatieSoorten.Single(x=> x.RelatieNaam == "vader"),

                            Email = new Email
                            {
                                EmailId = Guid.Parse("{D5E2D720-9E0C-4ABF-B246-45ADD2090653}"),
                                EmailAdres = "Ronny@test.com"
                            }
                        }
                    }
                },

                Maaltijden = new Maaltijden()
                {
                    MaaltijdenId = Guid.Parse("{EF186777-48B7-4E00-BE35-9CAD17A6D4B9}"),
                    MaandagMaaltijdSoort = maaltijdSoorten.Single(x => x.MaaltijdSoortNaam == "warme maaltijd"),
                    DinsdagMaaltijdSoort = maaltijdSoorten.Single(x => x.MaaltijdSoortNaam == "warme maaltijd"),
                    WoensdagMaaltijdSoort = maaltijdSoorten.Single(x => x.MaaltijdSoortNaam == "thuis"),
                    DonderdagMaaltijdSoort = maaltijdSoorten.Single(x => x.MaaltijdSoortNaam == "warme maaltijd"),
                    VrijdagMaaltijdSoort = maaltijdSoorten.Single(x => x.MaaltijdSoortNaam == "warme maaltijd"),
                },

                VoorgaandeInschrijvingen = new List<VoorgaandeInschrijving>()
                {
                    new VoorgaandeInschrijving()
                    {
                        VoorgaandeInschrijvingId = Guid.Parse("{680B1887-0060-4BC1-817D-A974491EA093}"),
                        Schooljaar = schooljaren.Single(x=>x.SchooljaarNaam=="2017-2018"),
                        School = scholen.Single(x=>x.OfficieleNaam =="Sint-Jozefsinstituut - ASO"),
                        BehaaldAttest = attestSoorten.Single(x=>x.AttestNaam == "A"),
                        Jaar = 1,
                        Richting = "latijn",
                        IsAttestGezien = true
                    }
                },

                Toestemmingen = new List<Toestemming>()
                {
                    new Toestemming()
                    {
                        ToestemmingId = 1,
                        IsAkkoord = true,
                        ToestemmingSoort = toestemmingSoorten.Single(x=>x.ToestemmingSoortId == 1)
                    },
                    new Toestemming()
                    {
                        ToestemmingId = 2,
                        IsAkkoord = true,
                        ToestemmingSoort = toestemmingSoorten.Single(x=>x.ToestemmingSoortId == 2)
                    },
                    new Toestemming()
                    {
                        ToestemmingId = 3,
                        IsAkkoord = true,
                        ToestemmingSoort = toestemmingSoorten.Single(x=>x.ToestemmingSoortId == 3)
                    },
                    new Toestemming()
                    {
                        ToestemmingId = 4,
                        IsAkkoord = true,
                        ToestemmingSoort = toestemmingSoorten.Single(x=>x.ToestemmingSoortId == 4)
                    },
                    new Toestemming()
                    {
                        ToestemmingId = 5,
                        IsAkkoord = true,
                        ToestemmingSoort = toestemmingSoorten.Single(x=>x.ToestemmingSoortId == 5)
                    },
                    new Toestemming()
                    {
                        ToestemmingId = 6,
                        IsAkkoord = true,
                        ToestemmingSoort = toestemmingSoorten.Single(x=>x.ToestemmingSoortId == 6)
                    }
                },

                Marketing = new Marketing()
                {
                    MarketingId = Guid.Parse("38bc140c-5e68-424f-b514-0dfa67e2b5c9"),
                    LerenKennenSchool = new List<Model.LerenKennen>()
                    {
                       new Model.LerenKennen()
                       {
                           LerenKennenId = 1,
                           IsReden = true,
                           LerenKennenSoort = lerenKennenSoorten.Single(x=>x.LerenKennenSoortId == 1)
                       },
                       new Model.LerenKennen()
                       {
                           LerenKennenId = 2,
                           IsReden = true,
                           LerenKennenSoort = lerenKennenSoorten.Single(x=>x.LerenKennenSoortId == 2)
                       },
                       new Model.LerenKennen()
                       {
                           LerenKennenId = 3,
                           IsReden = false,
                           LerenKennenSoort = lerenKennenSoorten.Single(x=>x.LerenKennenSoortId == 3)
                       },
                       new Model.LerenKennen()
                       {
                           LerenKennenId = 4,
                           IsReden = false,
                           LerenKennenSoort = lerenKennenSoorten.Single(x=>x.LerenKennenSoortId == 4)
                       }
                    },
                    LerenKennenSchoolVaria = "Via een helderziende",
                    WaaromGekozenSchool = "Voor een heel goede reden"
                }
            };

            context.Inschrijvingen.AddOrUpdate(inschrijving);


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
