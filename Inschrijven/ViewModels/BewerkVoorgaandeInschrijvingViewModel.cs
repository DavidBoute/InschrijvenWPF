using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using Inschrijven.Views.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class BewerkVoorgaandeInschrijvingViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        [Required(ErrorMessage = "Selecteer een schooljaar.")]
        public Schooljaar Schooljaar
        {
            get { return GetValue(() => Schooljaar); }
            set { SetValue(() => Schooljaar, value); }
        }

        [Required(ErrorMessage = "Selecteer een school.")]
        public School School
        {
            get { return GetValue(() => School); }
            set { SetValue(() => School, value); }
        }

        [Required(ErrorMessage = "Selecteer een attest.")]
        public AttestSoort BehaaldAttest
        {
            get { return GetValue(() => BehaaldAttest); }
            set
            {
                SetValue(() => BehaaldAttest, value);

                // Nieuwe validatie triggeren
                SetValue(() => Clausulering, Clausulering);
            }
        }

        [Range(1, 7, ErrorMessage = "Voer een geldig getal in (1-7).")]
        public int Jaar
        {
            get { return GetValue(() => Jaar); }
            set { SetValue(() => Jaar, value); }
        }

        [Required(ErrorMessage = "Voer een waarde in.")]
        public string Richting
        {
            get { return GetValue(() => Richting); }
            set { SetValue(() => Richting, value); }
        }

        [IsClausuleringRequired(ErrorMessage ="Voer de clausulering in.")]
        public string Clausulering
        {
            get { return GetValue(() => Clausulering); }
            set { SetValue(() => Clausulering, value); }
        }

        public bool IsAttestGezien
        {
            get { return GetValue(() => IsAttestGezien); }
            set { SetValue(() => IsAttestGezien, value); }
        }

        public bool IsBasoAfgegeven
        {
            get { return GetValue(() => IsBasoAfgegeven); }
            set { SetValue(() => IsBasoAfgegeven, value); }
        }

        public bool IsBasoZichtbaar
        {
            get
            {
                bool isBasoZichtbaar = false;
                if (School != null)
                {
                    isBasoZichtbaar = School.OnderwijsSoort.OnderwijsSoortNaam == "lager onderwijs";
                }
                return isBasoZichtbaar;
            }
        }

        public List<School> LijstScholen { get; private set; }
        public List<Schooljaar> LijstSchooljaren { get; private set; }
        public List<AttestSoort> LijstAttestSoorten { get; private set; }
        public List<int> LijstJaren { get; private set; }

        public VoorgaandeInschrijving VoorgaandeInschrijving { get; private set; }

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
                       VoorgaandeInschrijving.Jaar = Jaar;
                       VoorgaandeInschrijving.Richting = Richting;
                       VoorgaandeInschrijving.Clausulering = Clausulering;
                       VoorgaandeInschrijving.IsAttestGezien = IsAttestGezien;
                       VoorgaandeInschrijving.IsBaSoAfgegeven = IsBasoAfgegeven;
                       VoorgaandeInschrijving.Schooljaar = Schooljaar;
                       VoorgaandeInschrijving.School = School;
                       VoorgaandeInschrijving.BehaaldAttest = BehaaldAttest;

                       Window window = frame.Parent as Window;
                       window.DialogResult = true;
                       window.Close();
                   });
            }
        }

        public ICommand ZoekSchoolCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       ModalWindow modalWindow = new ModalWindow();
                       ZoekSchoolView view = new ZoekSchoolView(_dataService, modalWindow.Frame);
                       modalWindow.Frame.Content = view;

                       bool? done = modalWindow.ShowDialog();

                       if (done ?? false)
                       {
                           ZoekSchoolViewModel vm = view.DataContext as ZoekSchoolViewModel;
                           School newSchool = vm.School;

                           School = newSchool;

                       }
                   });
            }
        }

        #endregion


        // Custom Validation Rules
        #region Custom Validation Rules

        public class IsClausuleringRequiredAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as BewerkVoorgaandeInschrijvingViewModel;

                if (viewModel.BehaaldAttest != null
                    && viewModel.BehaaldAttest.IsClausuleringVereist
                    && viewModel.Clausulering == String.Empty)
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

        public BewerkVoorgaandeInschrijvingViewModel(VoorgaandeInschrijving voorgaandeInschrijving, IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;

            VoorgaandeInschrijving = voorgaandeInschrijving;

            LijstScholen = dataService.GetAlleScholen();
            LijstSchooljaren = dataService.GetAlleSchooljaren();
            LijstAttestSoorten = dataService.GetAlleAttestSoorten();
            LijstJaren = dataService.GetAlleJaren();

            Schooljaar = voorgaandeInschrijving.Schooljaar;
            School = voorgaandeInschrijving.School;
            BehaaldAttest = voorgaandeInschrijving.BehaaldAttest;

            Jaar = voorgaandeInschrijving.Jaar;
            Richting = voorgaandeInschrijving.Richting;
            Clausulering = voorgaandeInschrijving.Clausulering;
            IsAttestGezien = voorgaandeInschrijving.IsAttestGezien;
            IsBasoAfgegeven = voorgaandeInschrijving.IsBaSoAfgegeven;
        }

        #endregion
    }
}
