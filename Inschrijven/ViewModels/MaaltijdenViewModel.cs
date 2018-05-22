using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class MaaltijdenViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        [Intern(ErrorMessage = "Selecteer een internaat.")]
        public bool IsIntern
        {
            get { return GetValue(() => IsIntern); }
            set
            {
                SetValue(() => IsIntern, value);
                if (value == false)
                {
                    InternaatContact = null;
                }
            }
        }

        public Contact InternaatContact
        {
            get { return GetValue(() => InternaatContact); }
            set
            {
                SetValue(() => InternaatContact, value);

                // Nieuwe validatie triggeren
                SetValue(() => IsIntern, IsIntern);
            }
        }

        [Required(ErrorMessage = "Kies een optie.")]
        public MaaltijdSoort MaandagMaaltijdSoort
        {
            get { return GetValue(() => MaandagMaaltijdSoort); }
            set { SetValue(() => MaandagMaaltijdSoort, value); }
        }

        [Required(ErrorMessage = "Kies een optie.")]
        public MaaltijdSoort DinsdagMaaltijdSoort
        {
            get { return GetValue(() => DinsdagMaaltijdSoort); }
            set { SetValue(() => DinsdagMaaltijdSoort, value); }
        }

        [Required(ErrorMessage = "Kies een optie.")]
        public MaaltijdSoort WoensdagMaaltijdSoort
        {
            get { return GetValue(() => WoensdagMaaltijdSoort); }
            set { SetValue(() => WoensdagMaaltijdSoort, value); }
        }

        [Required(ErrorMessage = "Kies een optie.")]
        public MaaltijdSoort DonderdagMaaltijdSoort
        {
            get { return GetValue(() => DonderdagMaaltijdSoort); }
            set { SetValue(() => DonderdagMaaltijdSoort, value); }
        }

        [Required(ErrorMessage = "Kies een optie.")]
        public MaaltijdSoort VrijdagMaaltijdSoort
        {
            get { return GetValue(() => VrijdagMaaltijdSoort); }
            set { SetValue(() => VrijdagMaaltijdSoort, value); }
        }

        public bool VolgtAvondstudie
        {
            get { return GetValue(() => VolgtAvondstudie); }
            set { SetValue(() => VolgtAvondstudie, value); }
        }

        public List<MaaltijdSoort> Maaltijdsoorten { get; private set; }
        public List<MaaltijdSoort> MaaltijdsoortenWoensdag { get; private set; }
        public List<Contact> Internaten { get; private set; }

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
                       _inschrijving.Maaltijden.MaandagMaaltijdSoort = MaandagMaaltijdSoort;
                       _inschrijving.Maaltijden.DinsdagMaaltijdSoort = DinsdagMaaltijdSoort;
                       _inschrijving.Maaltijden.WoensdagMaaltijdSoort = WoensdagMaaltijdSoort;
                       _inschrijving.Maaltijden.DonderdagMaaltijdSoort = DonderdagMaaltijdSoort;
                       _inschrijving.Maaltijden.VrijdagMaaltijdSoort = VrijdagMaaltijdSoort;

                       if (IsIntern)
                       {
                           // Verwijderen van oud Internaat Contact indien een aanpassing is gebeurd
                           if (_inschrijving.Leerling.Contacten.Any(x => x.IsInternaat && x.ContactId != InternaatContact.ContactId))
                           {
                               _inschrijving.Leerling.Contacten.Remove(
                                        _inschrijving.Leerling.Contacten.First(x => x.IsInternaat));
                           }

                           // Toevoegen van internaat indien nog geen contact is
                           if (!_inschrijving.Leerling.Contacten.Any(x => x.ContactId == InternaatContact.ContactId))
                           {
                               _inschrijving.Leerling.Contacten.Add(InternaatContact);
                           }
                       }
                       else
                       {
                           // Verwijderen van Internaat Contact indien geen intern
                           // en al ingesteld tijdens een vorige sessie
                           if (_inschrijving.Leerling.Contacten.Any(x => x.IsInternaat))
                           {
                               _inschrijving.Leerling.Contacten.Remove(
                                   _inschrijving.Leerling.Contacten.First(x => x.IsInternaat));
                           }
                       }

                       _inschrijving.IsAvondstudie = VolgtAvondstudie;

                       await _dataService.SaveChangesAsync(_inschrijving);
                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules

        public class InternAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as MaaltijdenViewModel;
                bool isIntern = (bool)value;
                Contact internaatContact = viewModel.InternaatContact;

                if (isIntern && internaatContact == null)
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

        public MaaltijdenViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;

            int jaar = inschrijving.Richting.Jaar;
            string postcode = inschrijving.Leerling.Adressen.FirstOrDefault(x => x.IsDomicilie).Postcode;

            Maaltijdsoorten = dataService.GetAlleMaaltijdSoorten(jaar, postcode);
            MaaltijdsoortenWoensdag = dataService.GetAlleMaaltijdSoorten(jaar, "8000");  // 8000 - Zodat thuis altijd een optie is.

            Internaten = dataService.GetInternaatContacten();
            IsIntern = inschrijving.Leerling.Contacten.Any(x => x.IsInternaat);
            InternaatContact = inschrijving.Leerling.Contacten.FirstOrDefault(x => x.IsInternaat);

            if (inschrijving.Maaltijden == null)
            {
                inschrijving.Maaltijden = new Maaltijden() { MaaltijdenId = Guid.NewGuid() };
                inschrijving.Maaltijden.MaandagMaaltijdSoort = new MaaltijdSoort();
                inschrijving.Maaltijden.DinsdagMaaltijdSoort = new MaaltijdSoort();
                inschrijving.Maaltijden.WoensdagMaaltijdSoort = new MaaltijdSoort();
                inschrijving.Maaltijden.DonderdagMaaltijdSoort = new MaaltijdSoort();
                inschrijving.Maaltijden.VrijdagMaaltijdSoort = new MaaltijdSoort();
            }

            MaandagMaaltijdSoort = inschrijving.Maaltijden.MaandagMaaltijdSoort;
            DinsdagMaaltijdSoort = inschrijving.Maaltijden.DinsdagMaaltijdSoort;
            WoensdagMaaltijdSoort = inschrijving.Maaltijden.WoensdagMaaltijdSoort;
            DonderdagMaaltijdSoort = inschrijving.Maaltijden.DonderdagMaaltijdSoort;
            VrijdagMaaltijdSoort = inschrijving.Maaltijden.VrijdagMaaltijdSoort;

            VolgtAvondstudie = inschrijving.IsAvondstudie;
        }

        #endregion
    }

}
