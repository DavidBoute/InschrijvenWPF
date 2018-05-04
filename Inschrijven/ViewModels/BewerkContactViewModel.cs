using Inschrijven.Model;
using Inschrijven.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Services.Abstract;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Inschrijven.Views.Window;
using Inschrijven.Views;

namespace Inschrijven.ViewModels
{
    public class BewerkContactViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        [Required(ErrorMessage = "Vul de voornaam in")]
        public string Voornaam
        {
            get { return GetValue(() => Voornaam); }
            set { SetValue(() => Voornaam, value); }
        }

        [Required(ErrorMessage = "Vul de familienaam in")]
        public string Familienaam
        {
            get { return GetValue(() => Familienaam); }
            set { SetValue(() => Familienaam, value); }
        }

        [Required(ErrorMessage = "Kies een aanschrijving")]
        public AanschrijvingSoort Aanschrijving
        {
            get { return GetValue(() => Aanschrijving); }
            set { SetValue(() => Aanschrijving, value); }
        }

        [Required(ErrorMessage = "Vul de straat in")]
        public string Straat
        {
            get { return GetValue(() => Straat); }
            set { SetValue(() => Straat, value); }
        }

        [Required(ErrorMessage = "Vul het huisnummer in")]
        public string Huisnummer
        {
            get { return GetValue(() => Huisnummer); }
            set { SetValue(() => Huisnummer, value); }
        }

        [Required(ErrorMessage = "Vul de postcode in")]
        public string Postcode
        {
            get { return GetValue(() => Postcode); }
            set { SetValue(() => Postcode, value); }
        }

        [Required(ErrorMessage = "Vul de gemeente in")]
        public string Gemeente
        {
            get { return GetValue(() => Gemeente); }
            set { SetValue(() => Gemeente, value); }
        }

        public string Deelgemeente
        {
            get { return GetValue(() => Deelgemeente); }
            set { SetValue(() => Deelgemeente, value); }
        }

        [Required(ErrorMessage = "Kies de relatie")]
        public RelatieSoort Relatie
        {
            get { return GetValue(() => Relatie); }
            set { SetValue(() => Relatie, value); }
        }

        public string Email
        {
            get { return GetValue(() => Email); }
            set { SetValue(() => Email, value); }
        }

        public string Opmerking
        {
            get { return GetValue(() => Opmerking); }
            set { SetValue(() => Opmerking, value); }
        }

        public ObservableCollection<Telefoon> Telefoonnummers
        {
            get { return GetValue(() => Telefoonnummers); }
            set { SetValue(() => Telefoonnummers, value); }
        }

        public List<AanschrijvingSoort> AanschrijvingSoorten { get; private set; }
        public List<RelatieSoort> RelatieSoorten { get; private set; }

        public Contact Contact { get; private set; }

        public bool IsDomicilie
        {
            get { return GetValue(() => IsDomicilie); }
            set { SetValue(() => IsDomicilie, value); IsGeenDomicilie = !IsDomicilie; }
        }

        public bool IsGeenDomicilie
        {
            get { return GetValue(() => IsGeenDomicilie); }
            private set { SetValue(() => IsGeenDomicilie, value); }
        }

        #endregion

        // Commands
        #region Commands

        public ICommand OpslaanCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       Contact.Relatie = Relatie;
                       Contact.Voornaam = Voornaam;
                       Contact.Familienaam = Familienaam;
                       Contact.Opmerking = Opmerking;

                       if (Contact.Email == null)
                           Contact.Email = new Email() { EmailId = Guid.NewGuid() };
                       Contact.Email.EmailAdres = Email;

                       if (Contact.Adres == null)
                           Contact.Adres = new Adres() { AdresId = Guid.NewGuid() };
                       Contact.Adres.Aanschrijving = Aanschrijving;
                       Contact.Adres.Straat = Straat;
                       Contact.Adres.Huisnummer = Huisnummer;
                       Contact.Adres.Postcode = Postcode;
                       Contact.Adres.Gemeente = Gemeente;
                       Contact.Adres.Deelgemeente = Deelgemeente;

                       Contact.TelefoonNummers = Telefoonnummers;


                       Window window = frame.Parent as Window;
                       window.DialogResult = true;
                       window.Close();
                   });
            }
        }

        public ICommand MaakTelefoonCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       ModalWindow modalWindow = new ModalWindow();
                       BewerkTelefoonView view = new BewerkTelefoonView(new Telefoon() { TelefoonId = Guid.NewGuid() }, _dataService, modalWindow.Frame, _inschrijving);
                       modalWindow.Frame.Content = view;

                       bool? done = modalWindow.ShowDialog();

                       if (done ?? false)
                       {
                           BewerkTelefoonViewModel vm = view.DataContext as BewerkTelefoonViewModel;
                           Telefoon newTelefoon = vm.Telefoon;

                           if (Contact.TelefoonNummers == null)
                               Contact.TelefoonNummers = new List<Telefoon>();
                           Contact.TelefoonNummers.Add(newTelefoon);
                           Telefoonnummers = new ObservableCollection<Telefoon>(Contact.TelefoonNummers);
                       }
                   });
            }
        }

        public ICommand IsDomicilieCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       bool isDomicilie = (bool)obj;

                       if (!isDomicilie)
                       {
                           Contact.Adres = _inschrijving.Leerling.Adressen.First(a => a.IsDomicilie);
                           IsDomicilie = true;
                       }
                       else
                       {
                           Contact.Adres = new Adres() { AdresId = Guid.NewGuid() };
                           IsDomicilie = false;
                       }

                       Aanschrijving = Contact.Adres.Aanschrijving;
                       Straat = Contact.Adres.Straat;
                       Huisnummer = Contact.Adres.Huisnummer;
                       Postcode = Contact.Adres.Postcode;
                       Gemeente = Contact.Adres.Gemeente;
                       Deelgemeente = Contact.Adres.Deelgemeente;

                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules



        #endregion

        // Constructors
        #region Constructors

        public BewerkContactViewModel(Contact contact, IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;
            Contact = contact;

            AanschrijvingSoorten = dataService.GetAlleAanschrijvingen();
            RelatieSoorten = dataService.GetAlleRelatieSoorten();

            Relatie = contact.Relatie;
            Voornaam = contact.Voornaam;
            Familienaam = contact.Familienaam;
            Email = contact.Email?.EmailAdres;
            Opmerking = contact.Opmerking;
            Aanschrijving = contact.Adres?.Aanschrijving;
            Straat = contact.Adres?.Straat;
            Huisnummer = contact.Adres?.Huisnummer;
            Postcode = contact.Adres?.Postcode;
            Gemeente = contact.Adres?.Gemeente;
            Deelgemeente = contact.Adres?.Deelgemeente;
            Telefoonnummers = new ObservableCollection<Telefoon>(contact.TelefoonNummers ?? new Telefoon[] { });

            IsDomicilie = contact.Adres?.IsDomicilie ?? false;
        }
        #endregion
    }
}
