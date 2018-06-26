using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inschrijven.DAL;
using Inschrijven.Model;

namespace Inschrijven.Services.Abstract
{
    public class GegevensFromDatabaseService : IGegevensService
    {
        private InschrijvingContext db;

        public Schooljaar GetStandaardSchooljaar()
        {
            return GetStandaardSchooljaar(0);
        }

        public Schooljaar GetStandaardSchooljaar(int jaarAanpassing)
        {
            DateTime vandaag = DateTime.Today;

            if (vandaag.Month <= 3) jaarAanpassing = -1;

            // Indien schooljaar nog niet bestaat, aanvullen
            if (!db.Schooljaren
                    .Any(x => x.SchooljaarStartDatum.Year == vandaag.Year + jaarAanpassing))
            {
                try
                {
                    db.Schooljaren.Add(new Schooljaar
                    {
                        SchooljaarStartDatum = new DateTime(vandaag.Year + jaarAanpassing, 9, 1),
                        SchooljaarNaam = $"{vandaag.Year + jaarAanpassing}-{vandaag.Year + jaarAanpassing + 1}"
                    });

                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            Schooljaar schooljaar = db.Schooljaren
                    .SingleOrDefault(x => x.SchooljaarStartDatum.Year == vandaag.Year + jaarAanpassing);

            return schooljaar;
        }

        public List<Schooljaar> GetAlleSchooljaren()
        {
            return db.Schooljaren.ToList();
        }


        public List<Leerkracht> GetAlleLeerkrachten()
        {
            return db.Leerkrachten.ToList();
        }

        public List<Richting> GetAlleRichtingen()
        {
            return db.Richtingen.Include("Opties").ToList();
        }

        public List<Optie> GetAlleOpties()
        {
            return db.Opties.ToList();
        }

        public List<InschrijvingStatus> GetAlleInschrijvingStatussen()
        {
            return db.InschrijvingStatussen.ToList();
        }

        public List<Geslacht> GetAlleGeslachten()
        {
            return db.Geslachten.ToList();
        }

        public List<AanschrijvingSoort> GetAlleAanschrijvingen()
        {
            return db.AanschrijvingSoorten.ToList();
        }

        public List<RelatieSoort> GetAlleRelatieSoorten()
        {
            return db.RelatieSoorten.ToList();
        }

        public List<TelefoonSoort> GetAlleTelefoonSoorten()
        {
            return db.TelefoonSoorten.ToList();
        }

        public List<MaaltijdSoort> GetAlleMaaltijdSoorten()
        {
            return db.MaaltijdSoorten.ToList();
        }

        public List<MaaltijdSoort> GetAlleMaaltijdSoorten(int jaar, string postcode)
        {
            List<MaaltijdSoort> beschikbareMaaltijdSoorten = db.MaaltijdSoorten.Where(x => x.MaaltijdSoortNaam == "warme maaltijd"
                                                                                        || x.MaaltijdSoortNaam == "broodmaaltijd")
                                                                                        .ToList();

            if (jaar >= 5)
            {
                beschikbareMaaltijdSoorten.Add(db.MaaltijdSoorten.First(x => x.MaaltijdSoortNaam == "in de stad"));
            }

            string[] postcodesThuis = new string[] { "8000", "8200", "8300" };
            if (postcodesThuis.Contains(postcode))
            {
                beschikbareMaaltijdSoorten.Add(db.MaaltijdSoorten.First(x => x.MaaltijdSoortNaam == "thuis"));
            }

            return beschikbareMaaltijdSoorten;
        }

        public List<School> GetAlleScholen()
        {
            return db.Scholen.Include("OnderwijsSoort").ToList();
        }

        public List<Contact> GetInternaatContacten()
        {
            return db.Contacten
                        .Where(x => x.Relatie == db.RelatieSoorten.FirstOrDefault(y => y.RelatieNaam == "Internaat"))
                        .ToList();
        }

        public List<AttestSoort> GetAlleAttestSoorten()
        {
            return db.AttestSoorten.ToList();
        }

        public List<int> GetAlleJaren()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        }

        public List<ToestemmingSoort> GetAlleToestemmingSoorten()
        {
            return db.ToestemmingSoorten.ToList();
        }

        public List<ToestemmingSoort> GetAlleToestemmingSoorten(int jaar)
        {
            if ((new int[] { 1, 2 }).Contains(jaar))
            {
                return GetAlleToestemmingSoorten();
            }
            else
            {
                return GetAlleToestemmingSoorten()
                    .Where(x => !x.IsEnkelVoorEersteGraad).ToList();
            }
        }

        public List<LerenKennenSoort> GetAlleLerenKennenSoorten()
        {
            return db.LerenKennenSoorten.ToList();
        }

        public async Task<Inschrijving> SaveChangesAsync(Inschrijving inschrijving)
        {

            var record = db.Inschrijvingen.Find(inschrijving.InschrijvingId);

            if (db.Inschrijvingen.Find(inschrijving.InschrijvingId) == null)
            {
                db.Inschrijvingen.Add(inschrijving);
            }
            else
            {
                record = inschrijving;
            }

            await db.SaveChangesAsync();

            return inschrijving;
        }

        public Inschrijving GetInschrijving(Guid inschrijvingId)
        {
            return db.Inschrijvingen.FirstOrDefault(x => x.InschrijvingId == inschrijvingId);
        }


        // Constructor
        public GegevensFromDatabaseService(InschrijvingContext db)
        {
            this.db = db;
        }
    }
}
