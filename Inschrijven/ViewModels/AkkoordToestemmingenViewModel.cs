using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class AkkoordToestemmingenViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;
        private bool _isSchoolreglementAkkoord;

        public bool IsOverrideValidatie
        {
            get { return GetValue(() => IsOverrideValidatie); }
            set
            {
                SetValue(() => IsOverrideValidatie, value);

                if (!_isSchoolreglementAkkoord && !IsOverrideValidatie)
                {
                    IsOverrideKnopZichtbaar = true;
                }
                else
                {
                    IsOverrideKnopZichtbaar = false;
                }
            }
        }

        [AkkoordSchoolreglement(ErrorMessage = "Leerlingen kunnen niet ingeschreven blijven zonder akkoord te gaan met het schoolreglement.")]
        public bool IsOverrideKnopZichtbaar
        {
            get { return GetValue(() => IsOverrideKnopZichtbaar); }
            set
            {
                SetValue(() => IsOverrideKnopZichtbaar, value);

                IsNietOverrideKnopZichtbaar = !IsOverrideKnopZichtbaar;
            }
        }

        public bool IsNietOverrideKnopZichtbaar
        {
            get { return GetValue(() => IsNietOverrideKnopZichtbaar); }
            set { SetValue(() => IsNietOverrideKnopZichtbaar, value); }
        }


        public ObservableCollection<Toestemming> LijstToestemmingen
        {
            get { return GetValue(() => LijstToestemmingen); }
            set { SetValue(() => LijstToestemmingen, value); }
        }

        #endregion

        #region Custom Event Handling

        // Zorgt voor opvangen Changed event items lijst
        void LijstToestemmingen_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Toestemming item in e.NewItems)
                    item.PropertyChanged += Toestemming_PropertyChanged;

            if (e.OldItems != null)
                foreach (Toestemming item in e.OldItems)
                    item.PropertyChanged -= Toestemming_PropertyChanged;
        }

        void Toestemming_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Toestemming toestemming = sender as Toestemming;
            if (toestemming == null) { return; }

            if (toestemming.ToestemmingSoort.Code == "Schoolreglement" && e.PropertyName == "IsAkkoord")
            {
                _isSchoolreglementAkkoord = toestemming.IsAkkoord;
                if (_isSchoolreglementAkkoord)
                {
                    IsOverrideValidatie = false;
                    IsOverrideKnopZichtbaar = false;
                }
                else
                {
                    if (!IsOverrideValidatie)
                    {
                        IsOverrideKnopZichtbaar = true;
                    }
                    else
                    {
                        IsOverrideKnopZichtbaar = false;
                    }

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
                       await _dataService.SaveChangesAsync(_inschrijving);

                       frame.Content = new MarketingView(_dataService, frame, _inschrijving);
                   });
            }
        }

        public ICommand OverrideCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       IsOverrideValidatie = true;
                   });
            }
        }



        #endregion


        // Custom Validation Rules
        #region Custom Validation Rules

        public class AkkoordSchoolreglementAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {

                AkkoordToestemmingenViewModel vm = value as AkkoordToestemmingenViewModel;
                if (vm == null) { return null; }

                if (!(vm._isSchoolreglementAkkoord
                        || vm.IsOverrideValidatie))
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

        public AkkoordToestemmingenViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            if (inschrijving.Toestemmingen is null
                || inschrijving.Toestemmingen.Count == 0)
            {
                inschrijving.Toestemmingen = new List<Toestemming>();

                foreach (var toestemmingSoort in dataService.GetAlleToestemmingSoorten(inschrijving.Richting.Jaar))
                {
                    inschrijving.Toestemmingen.Add(new Toestemming { ToestemmingSoort = toestemmingSoort });
                }
            }

            LijstToestemmingen = new ObservableCollection<Toestemming>();
            LijstToestemmingen.CollectionChanged += LijstToestemmingen_CollectionChanged;

            // CollectionChanged triggert niet bij volledig nieuwe lijst
            foreach (var item in inschrijving.Toestemmingen)
            {
                LijstToestemmingen.Add(item);
            }

            _inschrijving = inschrijving;

            IsOverrideKnopZichtbaar = true;
        }

        #endregion
    }
}
