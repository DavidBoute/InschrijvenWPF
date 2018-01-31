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
            DateTime vandaag = DateTime.Today;

            int jaarAanpassing = 0;
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



        // Constructor
        public GegevensFromDatabaseService(InschrijvingContext db)
        {
            this.db = db;
        }
    }
}
