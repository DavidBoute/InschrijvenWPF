using Inschrijven.Extensions;
using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class LeerlingGegevensViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        [Required(ErrorMessage = "Typ de voornaam in")]
        public string Voornaam
        {
            get { return GetValue(() => Voornaam); }
            set { SetValue(() => Voornaam, value); }
        }

        [Required(ErrorMessage = "Typ de familienaam in")]
        public string Familienaam
        {
            get { return GetValue(() => Familienaam); }
            set { SetValue(() => Familienaam, value); }
        }

        [DateTimeRange(ErrorMessage = "Selecteer een geldige geboortedatum")]
        public DateTime GeboorteDatum
        {
            get { return GetValue(() => GeboorteDatum); }
            set { SetValue(() => GeboorteDatum, value); }
        }

        [Required(ErrorMessage = "Typ de geboorteplaats in")]
        public string Geboorteplaats
        {
            get { return GetValue(() => Geboorteplaats); }
            set { SetValue(() => Geboorteplaats, value); }
        }

        [Required(ErrorMessage = "Typ de nationaliteit in")]
        public string Nationaliteit
        {
            get { return GetValue(() => Nationaliteit); }
            set { SetValue(() => Nationaliteit, value); }
        }

        public string Rijksregisternummer
        {
            get { return GetValue(() => Rijksregisternummer); }
            set { SetValue(() => Rijksregisternummer, value); }
        }

        //Foto? Fotolocatie?

        [Required(ErrorMessage = "Selecteer het geslacht")]
        public Geslacht Geslacht
        {
            get { return GetValue(() => Geslacht); }
            set { SetValue(() => Geslacht, value); }
        }

        public Email Email
        {
            get { return GetValue(() => Email); }
            set { SetValue(() => Email, value); }
        }

        [Required(ErrorMessage = "Typ de straat in")]
        public int DomicilieAanspreking
        {
            get { return GetValue(() => DomicilieAanspreking); }
            set { SetValue(() => DomicilieAanspreking, value); }
        }

        [Required(ErrorMessage = "Typ de straat in")]
        public string DomicilieStraat
        {
            get { return GetValue(() => DomicilieStraat); }
            set { SetValue(() => DomicilieStraat, value); }
        }

        [Required(ErrorMessage = "Typ het huisnummer en eventueel de bus in")]
        public string DomicilieHuisnummer
        {
            get { return GetValue(() => DomicilieHuisnummer); }
            set { SetValue(() => DomicilieHuisnummer, value); }
        }

        [Required(ErrorMessage = "Typ de postcode in")]
        public string DomiciliePostcode
        {
            get { return GetValue(() => DomiciliePostcode); }
            set { SetValue(() => DomiciliePostcode, value); }
        }

        [Required(ErrorMessage = "Typ de Gemeente in")]
        public string DomicilieGemeente
        {
            get { return GetValue(() => DomicilieGemeente); }
            set { SetValue(() => DomicilieGemeente, value); }
        }

        public string DomicilieDeelGemeente
        {
            get { return GetValue(() => DomicilieDeelGemeente); }
            set { SetValue(() => DomicilieDeelGemeente, value); }
        }

        public List<Geslacht> LijstGeslachten
        {
            get { return GetValue(() => LijstGeslachten); }
            private set { SetValue(() => LijstGeslachten, value); }
        }

        public List<AanschrijvingSoort> LijstAanschrijvingSoorten
        {
            get { return GetValue(() => LijstAanschrijvingSoorten); }
            private set { SetValue(() => LijstAanschrijvingSoorten, value); }
        }

        #endregion

        // Commands
        #region Commands

        public ICommand VolgendeCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       Leerling leerling = _inschrijving.Leerling ?? new Leerling()
                                                                    {
                                                                        LeerlingId = Guid.NewGuid(),
                                                                        Adressen = new List<Adres>()
                                                                    };

                       leerling.Voornaam = Voornaam;
                       leerling.Familienaam = Familienaam;
                       leerling.Geboortedatum = GeboorteDatum;
                       leerling.Geboorteplaats = Geboorteplaats;
                       leerling.Nationaliteit = Nationaliteit;
                       leerling.RijksregisterNummer = Rijksregisternummer;
                       leerling.Geslacht = Geslacht;
                       leerling.Email = Email;

                       Adres domicilieAdres = leerling.Adressen
                                                        .SingleOrDefault(x => x.IsDomicilie)
                                                        ?? new Adres() { AdresId = Guid.NewGuid()};
                       AanschrijvingSoort aanschrijving = LijstAanschrijvingSoorten
                                                            .Single(x => x.AanschrijvingSoortId == DomicilieAanspreking);

                       domicilieAdres.Aanschrijving = aanschrijving;
                       domicilieAdres.Straat = DomicilieStraat;
                       domicilieAdres.Huisnummer = DomicilieHuisnummer;
                       domicilieAdres.Postcode = DomiciliePostcode;
                       domicilieAdres.Gemeente = DomicilieGemeente;
                       domicilieAdres.Deelgemeente = DomicilieDeelGemeente;
                       domicilieAdres.IsDomicilie = true;

                       if (leerling.Adressen.Any(x => x.IsDomicilie))
                       {
                           Adres oudAdres = leerling.Adressen
                                                    .Single(x => x.AdresId == domicilieAdres.AdresId);
                           oudAdres = domicilieAdres;
                       }
                       else
                       {
                           leerling.Adressen.Add(domicilieAdres);
                       }

                       _inschrijving.Leerling = leerling;

                       await _dataService.SaveChangesAsync(_inschrijving);

                       frame.Content = new ContactenGegevensView(_dataService, frame, _inschrijving);
                   });
            }
        }

        #endregion

        // Custom Validation Rules

        #region Custom Validation Rules

        public class DateTimeRangeAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                DateTime datum = (DateTime)value;

                DateTime startDatum = DateTime.Today.AddYears(-25);
                DateTime eindDatum = DateTime.Today.AddYears(-11);

                if (!(startDatum < datum
                        && datum < eindDatum))
                {
                    return new System.ComponentModel.DataAnnotations.ValidationResult
                        (this.FormatErrorMessage(validationContext.DisplayName));
                }
                return null;
            }
        }

        #endregion

        // Constructors
        #region Constructors

        public LeerlingGegevensViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;

            LijstGeslachten = _dataService.GetAlleGeslachten();
            LijstAanschrijvingSoorten = _dataService.GetAlleAanschrijvingen();

            Leerling leerling = _inschrijving.Leerling;
            if (leerling != null)
            {
                Voornaam = leerling.Voornaam;
                Familienaam = leerling.Familienaam;
                GeboorteDatum = leerling.Geboortedatum;
                Geboorteplaats = leerling.Geboorteplaats;
                Nationaliteit = leerling.Nationaliteit;
                Rijksregisternummer = leerling.RijksregisterNummer;
                Geslacht = leerling.Geslacht;
                Email = leerling.Email;

                Adres domicilieAdres = leerling.Adressen
                                                .SingleOrDefault(x => x.IsDomicilie);
                if (domicilieAdres != null)
                {
                    DomicilieAanspreking = domicilieAdres.Aanschrijving.AanschrijvingSoortId;
                    DomicilieStraat = domicilieAdres.Straat;
                    DomicilieHuisnummer = domicilieAdres.Huisnummer;
                    DomiciliePostcode = domicilieAdres.Postcode;
                    DomicilieGemeente = domicilieAdres.Gemeente;
                    DomicilieDeelGemeente = domicilieAdres.Deelgemeente;
                }
            }
            else
            {
                GeboorteDatum = DateTime.Today;
                Email = new Email() { EmailId = Guid.NewGuid()};
            }
        }

        #endregion
    }
}
