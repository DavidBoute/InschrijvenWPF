using Inschrijven.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Services.Abstract
{
    public interface IGegevensService
    {
        Schooljaar GetStandaardSchooljaar();
        List<Schooljaar> GetAlleSchooljaren();

        List<Leerkracht> GetAlleLeerkrachten();
        List<Richting> GetAlleRichtingen();
        List<Optie> GetAlleOpties();
        List<InschrijvingStatus> GetAlleInschrijvingStatussen();
        List<Geslacht> GetAlleGeslachten();
        List<AanschrijvingSoort> GetAlleAanschrijvingen();

        Task<Inschrijving> SaveChangesAsync(Inschrijving inschrijving);

        Inschrijving GetInschrijving(Guid inschrijvingId);

    }
}
