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
    public class StartInschrijvingViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;
        private List<Richting> _alleRichtingen;
        private List<Optie> _alleOpties;

        public ObservableCollection<int> Jaren
        {
            get { return GetValue(() => Jaren); }
            set { SetValue(() => Jaren, value); }
        }

        public ObservableCollection<Schooljaar> SchooljarenLijst
        {
            get { return GetValue(() => SchooljarenLijst); }
            set { SetValue(() => SchooljarenLijst, value); }
        }

        public ObservableCollection<Richting> RichtingenGefilterd
        {
            get { return GetValue(() => RichtingenGefilterd); }
            set { SetValue(() => RichtingenGefilterd, value); }
        }

        public ObservableCollection<Optie> OptiesGefilterd
        {
            get { return GetValue(() => OptiesGefilterd); }
            set
            {
                SetValue(() => OptiesGefilterd, value);
                IsOptieZichtbaar = (OptiesGefilterd != null && OptiesGefilterd.Count() != 0) ?
                    Visibility.Visible : Visibility.Hidden;
                Optie = null;
            }
        }

        [Required(ErrorMessage = "Selecteer een schooljaar.")]
        public Schooljaar Schooljaar
        {
            get { return GetValue(() => Schooljaar); }
            set { SetValue(() => Schooljaar, value); }
        }

        [Range(1, 7, ErrorMessage = "Selecteer een jaar.")]
        public int Jaar
        {
            get { return GetValue(() => Jaar); }
            set
            {
                SetValue(() => Jaar, value);
                RichtingenGefilterd = _alleRichtingen.Where(x => x.Jaar == value)
                                                    .ToObservableCollection();
            }
        }

        [Required(ErrorMessage = "Selecteer een richting.")]
        public Richting Richting
        {
            get { return GetValue(() => Richting); }
            set
            {
                SetValue(() => Richting, value);
                OptiesGefilterd = _alleOpties.Where(x => x.Richtingen.Contains(Richting))
                                                .ToObservableCollection();
            }
        }

        public Visibility IsOptieZichtbaar
        {
            get { return GetValue(() => IsOptieZichtbaar); }
            set { SetValue(() => IsOptieZichtbaar, value); }
        }

        [IsOptieRequired(ErrorMessage = "Bij de geselecteerde richting moet een optie gekozen worden")]
        public Optie Optie
        {
            get { return GetValue(() => Optie); }
            set { SetValue(() => Optie, value); }
        }

        #endregion

        // Commands
        #region Commands

        public ICommand StartInschrijvingCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       _inschrijving.Schooljaar = this.Schooljaar;
                       _inschrijving.Richting = this.Richting;
                       _inschrijving.Optie = this.Optie;

                       _inschrijving.InschrijvingStatus = _dataService.GetAlleInschrijvingStatussen()
                                                            .FirstOrDefault(x=> x.InschrijvingStatusNaam == "niet gerealiseerd");

                       _inschrijving.StartTijd = DateTime.Now;
                       _inschrijving.IsHerinschrijving = false;
                       _inschrijving.IsAvondstudie = false;

                       await _dataService.SaveChangesAsync(_inschrijving);

                       frame.Content = new LeerlingGegevensView(_dataService, frame, _inschrijving);
                   });
            }
        }

        #endregion

        // Custom Validation Rules
        // https://stackoverflow.com/questions/16100300/asp-net-mvc-custom-validation-by-dataannotation
        public class IsOptieRequiredAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as StartInschrijvingViewModel;

                int aantalOpties = 0;
                if (viewModel.OptiesGefilterd != null)
                {
                    aantalOpties = viewModel.OptiesGefilterd.Count();
                }

                if (aantalOpties != 0 && value == null)
                {
                    return new System.ComponentModel.DataAnnotations.ValidationResult
                        (this.FormatErrorMessage(validationContext.DisplayName));
                }
                return null;
            }
        }

        // Constructors
        #region Constructors

        public StartInschrijvingViewModel(IGegevensService dataService, Frame frame, Leerkracht inschrijvendeLeerkracht, Inschrijving inschrijving = null)
            : base(dataService, frame)
        {
            SchooljarenLijst = dataService.GetAlleSchooljaren().ToObservableCollection();
            Jaren = new int[] { 1, 2, 3, 4, 5, 6, 7 }.ToObservableCollection();
            _alleRichtingen = dataService.GetAlleRichtingen();
            _alleOpties = dataService.GetAlleOpties();
            IsOptieZichtbaar = Visibility.Hidden;

            if (inschrijving != null)
            { 
                _inschrijving = inschrijving;
                Schooljaar = inschrijving.Schooljaar;
                Jaar = inschrijving.Richting.Jaar;
                Richting = inschrijving.Richting;
                Optie = inschrijving.Optie;
            }
            else
            {
                _inschrijving = new Inschrijving()
                {
                    InschrijvingId = Guid.NewGuid(),
                    Leerkracht = inschrijvendeLeerkracht
                };
            }

        }

        #endregion
    }
}
