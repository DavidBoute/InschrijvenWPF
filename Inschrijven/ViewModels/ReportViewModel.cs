using Inschrijven.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.ViewModels
{
    class ReportViewModel
    {
        private Inschrijving _inschrijving;

        // Inschrijving Properties
        #region Inschrijving

        public Guid InschrijvingId { get { return _inschrijving.InschrijvingId; } }
        public DateTime StartTijd { get { return _inschrijving.StartTijd; } }
        public bool IsHerinschrijving { get { return _inschrijving.IsHerinschrijving; } }
        public bool IsAvondstudie { get { return _inschrijving.IsAvondstudie; } }
        public bool IsAkkoordSchoolreglement { get { return _inschrijving.IsAkkoordSchoolreglement; } }

        public virtual Leerling Leerling { get { return _inschrijving.Leerling; } }
        public virtual Leerkracht Leerkracht { get { return _inschrijving.Leerkracht; } }
        public virtual Richting Richting { get { return _inschrijving.Richting; } }
        public virtual Optie Optie { get { return _inschrijving.Optie; } }
        public virtual Schooljaar Schooljaar { get { return _inschrijving.Schooljaar; } }
        public virtual Maaltijden Maaltijden { get { return _inschrijving.Maaltijden; } }
        public virtual InschrijvingStatus InschrijvingStatus { get { return _inschrijving.InschrijvingStatus; } }
        public virtual Marketing Marketing { get { return _inschrijving.Marketing; } }

        public virtual ICollection<VoorgaandeInschrijving> VoorgaandeInschrijvingen { get { return _inschrijving.VoorgaandeInschrijvingen; } }
        public virtual ICollection<Toestemming> Toestemmingen { get { return _inschrijving.Toestemmingen; } }

        #endregion

        // Calculated
        #region Calulated

        public string GeboorteplaatsVolledig { get { return Leerling.Geboorteplaats + " (" + Leerling.Geboorteland + ")"; } }

        public bool IsIntern { get { return Leerling.Contacten.Any(x => x.IsInternaat); } }
        public string InternaatNaam { get { return Leerling.Contacten.FirstOrDefault(x => x.IsInternaat)?.VolledigeNaam ?? ""; } }

        public string Verblijftype
        {
            get
            {
                if (IsIntern)
                {
                    return "intern";
                }

                if (LijstMaaltijden.Any(x => x.MaaltijdSoortNaam == "warme maaltijd"
                                        || x.MaaltijdSoortNaam == "broodmaaltijd"))
                {
                    return "half-intern";
                }

                return "extern";
            }
        }

        public string EigenGsm { get { return Leerling.TelefoonNummers.FirstOrDefault(x => x.TelefoonSoort.TelefoonSoortNaam == "gsm")?.Nummer ?? ""; } }

        public bool IsPRZichtbaar { get { return LijstLerenKennen.Any(); } }

        #endregion

        public List<MaaltijdSoort> LijstMaaltijden { get; private set; }
        public List<AdresWrapper> LijstAdressen { get; private set; }
        public List<Contact> LijstContacten { get; private set; }
        public List<VoorgaandeInschrijving> LijstInschrijvingen { get; private set; }
        public List<Toestemming> LijstToestemmingen { get; private set; }
        public List<LerenKennen> LijstLerenKennen { get; private set; }

        public ReportViewModel(Inschrijving inschrijving)
        {
            _inschrijving = inschrijving;

            LijstMaaltijden = new List<MaaltijdSoort>
            {
                Maaltijden.MaandagMaaltijdSoort,
                Maaltijden.DinsdagMaaltijdSoort,
                Maaltijden.WoensdagMaaltijdSoort,
                Maaltijden.DonderdagMaaltijdSoort,
                Maaltijden.VrijdagMaaltijdSoort
            };

            #region Adressen

            LijstAdressen = new List<AdresWrapper>();

            // Eerst domicilie adres
            LijstAdressen.Add(new AdresWrapper(Leerling.Adressen.Single(x => x.IsDomicilie)));

            // dan de andere adressen van de leerling
            foreach (var adres in Leerling.Adressen)
            {
                if (!LijstAdressen.Any(x => x.AdresId == adres.AdresId))
                {
                    LijstAdressen.Add(new AdresWrapper(adres));
                }
            }

            // dan de adressen dan de contacten
            foreach (var contact in Leerling.Contacten)
            {
                Adres adres = contact.Adres;
                if (!LijstAdressen.Any(x => x.AdresId == adres.AdresId))
                {
                    LijstAdressen.Add(new AdresWrapper(adres));
                }
            }

            #endregion

            LijstContacten = Leerling.Contacten.ToList();

            LijstInschrijvingen = VoorgaandeInschrijvingen.OrderBy(x=>x.Schooljaar).ToList();

            LijstToestemmingen = Toestemmingen.ToList();

            LijstLerenKennen = Marketing.LerenKennenSchool.Where(x=>x.IsReden).ToList();
            if (!String.IsNullOrWhiteSpace(Marketing.LerenKennenSchoolVaria))
            {
                LijstLerenKennen.Add(new LerenKennen()
                {
                    IsReden = true,
                    LerenKennenSoort = new LerenKennenSoort()
                    {
                        LerenKennenSoortOmschrijving = Marketing.LerenKennenSchoolVaria
                    }
                });
            }
        }

        public class AdresWrapper
        {
            private Adres _adres;

            // Adres properties
            #region Adres

            public Guid AdresId { get { return _adres.AdresId; } }
            public string Straat { get { return _adres.Straat; } }
            public string Huisnummer { get { return _adres.Huisnummer; } }
            public string Postcode { get { return _adres.Postcode; } }
            public string Gemeente { get { return _adres.Gemeente; } }
            public string Deelgemeente { get { return _adres.Deelgemeente; } }
            public bool IsDomicilie { get { return _adres.IsDomicilie; } }
            public bool IsAanschrijf { get { return _adres.IsAanschrijf; } }
            public bool IsInternaat { get { return _adres.IsInternaat; } }
            public virtual AanschrijvingSoort Aanschrijving { get { return _adres.Aanschrijving; } }
            public virtual ICollection<Leerling> Leerlingen { get { return _adres.Leerlingen; } }
            public virtual ICollection<Contact> Contacten { get { return _adres.Contacten; } }

            #endregion

            // Calculated
            #region Calculated

            public string Header
            {
                get
                {
                    if (IsDomicilie) { return "Domicilie-adres"; }
                    if (IsAanschrijf) { return "Bijkomend aanschrijfadres"; }
                    return "Bijkomend adres";
                }
            }

            public string AdresContacten
            {
                get
                {
                    string contacten = "";

                    foreach (var contact in Contacten)
                    {
                        contacten += contact.Relatie.RelatieNaam + " / ";
                    }

                    if (contacten.Length >= 2) { contacten = contacten.Remove(contacten.Length - 2); }

                    return contacten;
                }
            }

            public string StraatHuisnummer { get { return Straat + " " + Huisnummer; } }
            public string PostcodeGemeente
            {
                get
                {
                    string postcodeGemeente = Postcode;

                    if (!String.IsNullOrWhiteSpace(Deelgemeente))
                    {
                        postcodeGemeente += " " + Deelgemeente + " (" + Gemeente + ")";
                    }
                    else
                    {
                        postcodeGemeente += " " + Gemeente;
                    }

                    return postcodeGemeente;
                }
            }

            #endregion

            public AdresWrapper(Adres adres)
            {
                _adres = adres;
            }
        }
    }
}
