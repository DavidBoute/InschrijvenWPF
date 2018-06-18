using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
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
    public class OpmerkingenViewModel: BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        public string TaalOpmerkingen
        {
            get { return GetValue(() => TaalOpmerkingen); }
            set { SetValue(() => TaalOpmerkingen, value); }
        }

        public string MedischeOpmerkingen
        {
            get { return GetValue(() => MedischeOpmerkingen); }
            set { SetValue(() => MedischeOpmerkingen, value); }
        }

        public string LeerOpmerkingen
        {
            get { return GetValue(() => LeerOpmerkingen); }
            set { SetValue(() => LeerOpmerkingen, value); }
        }

        [Zorg(ErrorMessage="Duid een motivering voor de verhoogde zorgvraag aan.")]
        public bool VerhoogdeZorgVraag
        {
            get { return GetValue(() => VerhoogdeZorgVraag); }
            set
            {
                SetValue(() => VerhoogdeZorgVraag, value);
                if (!value)
                {
                    VerslagBuitengewoonOnderwijs = false;
                    GemotiveerdVerslag = false;
                    OndersteuningsUur = false;
                }
            }
        }

        public bool VerslagBuitengewoonOnderwijs
        {
            get { return GetValue(() => VerslagBuitengewoonOnderwijs); }
            set
            {
                SetValue(() => VerslagBuitengewoonOnderwijs, value);
                if (value)
                {
                    VerhoogdeZorgVraag = true;
                }
            }
        }

        public bool GemotiveerdVerslag
        {
            get { return GetValue(() => GemotiveerdVerslag); }
            set
            {
                SetValue(() => GemotiveerdVerslag, value);
                if (value)
                {
                    VerhoogdeZorgVraag = true;
                }
            }
        }

        public bool OndersteuningsUur
        {
            get { return GetValue(() => OndersteuningsUur); }
            set
            {
                SetValue(() => OndersteuningsUur, value);
                if (value)
                {
                    VerhoogdeZorgVraag = true;
                }
            }
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
                       _inschrijving.Leerling.BijkomendeInfo.MedischeProblemen = MedischeOpmerkingen;
                       _inschrijving.Leerling.BijkomendeInfo.TaalProblemen = TaalOpmerkingen;
                       _inschrijving.Leerling.BijkomendeInfo.LeerProblemen = LeerOpmerkingen;
                       _inschrijving.Leerling.BijkomendeInfo.VerhoogdeZorgVraag = VerhoogdeZorgVraag;
                       _inschrijving.Leerling.BijkomendeInfo.VerslagBuitengewoonOnderwijs = VerslagBuitengewoonOnderwijs;
                       _inschrijving.Leerling.BijkomendeInfo.GemotiveerdVerslag = GemotiveerdVerslag;
                       _inschrijving.Leerling.BijkomendeInfo.OndersteuningsUur = OndersteuningsUur;

                       if (VerhoogdeZorgVraag)
                       {
                           MessageBox.Show("Breng de directie op de hoogte dat een leerling met verhoogde zorg wenst in te schrijven.");
                       }

                       await _dataService.SaveChangesAsync(_inschrijving);

                       frame.Content = new VoorgaandeInschrijvingView(_dataService, frame, _inschrijving);
                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules

        public class ZorgAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as OpmerkingenViewModel;               

                if (viewModel.VerhoogdeZorgVraag 
                    && !viewModel.VerslagBuitengewoonOnderwijs
                    && !viewModel.GemotiveerdVerslag
                    && !viewModel.OndersteuningsUur)
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

        public OpmerkingenViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;

            if (inschrijving.Leerling.BijkomendeInfo is null)
            {
                inschrijving.Leerling.BijkomendeInfo = new BijkomendeInfo() { BijkomendeInfoId = Guid.NewGuid()};
            }

            MedischeOpmerkingen = inschrijving.Leerling.BijkomendeInfo.MedischeProblemen;
            TaalOpmerkingen = inschrijving.Leerling.BijkomendeInfo.TaalProblemen;
            LeerOpmerkingen = inschrijving.Leerling.BijkomendeInfo.LeerProblemen;
            VerhoogdeZorgVraag = inschrijving.Leerling.BijkomendeInfo.VerhoogdeZorgVraag;
            VerslagBuitengewoonOnderwijs = inschrijving.Leerling.BijkomendeInfo.VerslagBuitengewoonOnderwijs;
            GemotiveerdVerslag = inschrijving.Leerling.BijkomendeInfo.GemotiveerdVerslag;
            OndersteuningsUur = inschrijving.Leerling.BijkomendeInfo.OndersteuningsUur;
        }

        #endregion
    }
}
