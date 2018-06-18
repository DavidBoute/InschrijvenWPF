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
        Schooljaar GetStandaardSchooljaar(int aanpassing);
        List<Schooljaar> GetAlleSchooljaren();

        List<Leerkracht> GetAlleLeerkrachten();
        List<Richting> GetAlleRichtingen();
        List<Optie> GetAlleOpties();
        List<InschrijvingStatus> GetAlleInschrijvingStatussen();
        List<Geslacht> GetAlleGeslachten();
        List<AanschrijvingSoort> GetAlleAanschrijvingen();
        List<RelatieSoort> GetAlleRelatieSoorten();
        List<TelefoonSoort> GetAlleTelefoonSoorten();
        List<MaaltijdSoort> GetAlleMaaltijdSoorten();
        List<MaaltijdSoort> GetAlleMaaltijdSoorten(int jaar, string postcode);
        List<Contact> GetInternaatContacten();
        List<School> GetAlleScholen();
        List<AttestSoort> GetAlleAttestSoorten();
        List<int> GetAlleJaren();
        List<ToestemmingSoort> GetAlleToestemmingSoorten();
        List<ToestemmingSoort> GetAlleToestemmingSoorten(int jaar);


        Task<Inschrijving> SaveChangesAsync(Inschrijving inschrijving);

        Inschrijving GetInschrijving(Guid inschrijvingId);

    }
}
