using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Inschrijven.Views.Window;
using Inschrijven.Views;
using System.Collections.ObjectModel;
using System.Collections;

namespace Inschrijven.ViewModels
{
    public class ContactenGegevensViewModel: BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        [ContactsAmount(ErrorMessage ="Vul minstens 1 contactpersoon in.")]
        public ObservableCollection<Contact> LijstContacten
        {
            get { return GetValue(() => LijstContacten); }
            private set { SetValue(() => LijstContacten, value); }
        }

        public Contact GeselecteerdContact
        {
            get { return GetValue(() => GeselecteerdContact); }
            set { SetValue(() => GeselecteerdContact, value); }
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
                       await _dataService.SaveChangesAsync(_inschrijving);

                       frame.Content = new MaaltijdenView(_dataService, frame, _inschrijving);
                   });
            }
        }

        public ICommand MaakContactCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       ModalWindow modalWindow = new ModalWindow();
                       BewerkContactView view = new BewerkContactView(new Contact() { ContactId = Guid.NewGuid()}, _dataService, modalWindow.Frame, _inschrijving);
                       modalWindow.Frame.Content = view;

                       bool? done = modalWindow.ShowDialog();

                       if (done ?? false)
                       {
                           BewerkContactViewModel vm = view.DataContext as BewerkContactViewModel;
                           Contact newContact = vm.Contact;
                           _inschrijving.Leerling.Contacten.Add(newContact);
                           LijstContacten = new ObservableCollection<Contact>(_inschrijving.Leerling.Contacten);
                       }
                   });
            }
        }

        public ICommand BewerkContactCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       Contact contact = obj as Contact;

                       Window modalWindow = new ModalWindow();
                       Frame frame = (Frame)modalWindow.FindName("frame");
                       BewerkContactView view = new BewerkContactView(contact, _dataService, frame, _inschrijving);
                       frame.Content = view;

                       bool? done = modalWindow.ShowDialog();

                       if (done ?? false)
                       {
                           BewerkContactViewModel vm = view.DataContext as BewerkContactViewModel;
                           Contact newContact = vm.Contact;

                           Contact oldContact = _inschrijving.Leerling.Contacten.First(c => c.ContactId == newContact.ContactId);
                           oldContact = newContact;
                           LijstContacten = new ObservableCollection<Contact>(_inschrijving.Leerling.Contacten);
                       }
                   });
            }
        }

        public ICommand VerwijderContactCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       Contact contact = obj as Contact;

                       Contact oldContact = _inschrijving.Leerling.Contacten.First(c => c.ContactId == contact.ContactId);
                       _inschrijving.Leerling.Contacten.Remove(oldContact);
                       LijstContacten = new ObservableCollection<Contact>(_inschrijving.Leerling.Contacten);
                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules

        public class ContactsAmountAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                int amount = ((ICollection)value).Count;

                if (amount == 0)
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

        public ContactenGegevensViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            if (inschrijving.Leerling.Contacten == null)
            {
                inschrijving.Leerling.Contacten = new List<Contact>();
            }

            LijstContacten =  new ObservableCollection<Contact>(inschrijving.Leerling.Contacten);

            _inschrijving = inschrijving;
        }

        #endregion
    }
}
