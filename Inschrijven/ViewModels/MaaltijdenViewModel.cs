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

        [Required(ErrorMessage ="Kies een optie.")]
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


        }

        #endregion
    }

}
