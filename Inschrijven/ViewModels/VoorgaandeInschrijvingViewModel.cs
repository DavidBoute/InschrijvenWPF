using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using Inschrijven.Views.Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class VoorgaandeInschrijvingViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        [AantalInschrijvingenRequired(ErrorMessage ="Voer minstens 1 voorgaande inschrijving in.")]
        public ObservableCollection<VoorgaandeInschrijving> LijstVoorgaandeInschrijvingen
        {
            get { return GetValue(() => LijstVoorgaandeInschrijvingen); }
            set { SetValue(() => LijstVoorgaandeInschrijvingen, value); }
        }

        public VoorgaandeInschrijving GeselecteerdeInschrijving
        {
            get { return GetValue(() => GeselecteerdeInschrijving); }
            set { SetValue(() => GeselecteerdeInschrijving, value); }
        }

        #endregion

        // Commands
        #region Commands

        public ICommand MaakVoorgaandeInschrijvingCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       ModalWindow modalWindow = new ModalWindow();
                       BewerkVoorgaandeInschrijvingView view = new BewerkVoorgaandeInschrijvingView(
                                                                        new VoorgaandeInschrijving()
                                                                        {
                                                                            VoorgaandeInschrijvingId = Guid.NewGuid(),
                                                                            Schooljaar = _dataService.GetStandaardSchooljaar(-1)
                                                                        }, 
                                                                        _dataService, modalWindow.Frame, _inschrijving);
                       modalWindow.Frame.Content = view;

                       bool? done = modalWindow.ShowDialog();

                       if (done ?? false)
                       {
                           BewerkVoorgaandeInschrijvingViewModel vm = view.DataContext as BewerkVoorgaandeInschrijvingViewModel;
                           VoorgaandeInschrijving newInschrijving = vm.VoorgaandeInschrijving;
                           _inschrijving.VoorgaandeInschrijvingen.Add(newInschrijving);
                           LijstVoorgaandeInschrijvingen = new ObservableCollection<VoorgaandeInschrijving>(_inschrijving.VoorgaandeInschrijvingen);
                       }
                   });
            }
        }

        public ICommand BewerkVoorgaandeInschrijvingCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       if (obj is VoorgaandeInschrijving voorgaandeInschrijving)
                       {
                           ModalWindow modalWindow = new ModalWindow();
                           BewerkVoorgaandeInschrijvingView view = new BewerkVoorgaandeInschrijvingView(
                                                                            voorgaandeInschrijving, _dataService, modalWindow.Frame, _inschrijving);
                           modalWindow.Frame.Content = view;

                           bool? done = modalWindow.ShowDialog();

                           if (done ?? false)
                           {
                               BewerkVoorgaandeInschrijvingViewModel vm = view.DataContext as BewerkVoorgaandeInschrijvingViewModel;
                               VoorgaandeInschrijving newInschrijving = vm.VoorgaandeInschrijving;

                               VoorgaandeInschrijving oldInschrijving = _inschrijving.VoorgaandeInschrijvingen
                                                                                .First(c => c.VoorgaandeInschrijvingId == newInschrijving.VoorgaandeInschrijvingId);
                               oldInschrijving = newInschrijving;
                               LijstVoorgaandeInschrijvingen = new ObservableCollection<VoorgaandeInschrijving>(_inschrijving.VoorgaandeInschrijvingen);
                           }
                       }
                   });
            }
        }


        public ICommand VerwijderVoorgaandeInschrijvingCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       if (obj is VoorgaandeInschrijving voorgaandeInschrijving)
                       {

                           VoorgaandeInschrijving oldInschrijving = _inschrijving.VoorgaandeInschrijvingen
                                                                                .First(c => c.VoorgaandeInschrijvingId == voorgaandeInschrijving.VoorgaandeInschrijvingId);
                           _inschrijving.VoorgaandeInschrijvingen.Remove(oldInschrijving);
                           LijstVoorgaandeInschrijvingen = new ObservableCollection<VoorgaandeInschrijving>(_inschrijving.VoorgaandeInschrijvingen);
                       }
                   });
            }
        }

        public ICommand VolgendeCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       await _dataService.SaveChangesAsync(_inschrijving);

                       frame.Content = new AkkoordToestemmingenView(_dataService, frame, _inschrijving);

                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules

        public class AantalInschrijvingenRequiredAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as VoorgaandeInschrijvingViewModel;

                if (viewModel.LijstVoorgaandeInschrijvingen.Count == 0)
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

        public VoorgaandeInschrijvingViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {  
            if (inschrijving.VoorgaandeInschrijvingen is null)
            {
                inschrijving.VoorgaandeInschrijvingen = new List<VoorgaandeInschrijving>();
            }

            LijstVoorgaandeInschrijvingen = new ObservableCollection<VoorgaandeInschrijving>(inschrijving.VoorgaandeInschrijvingen);

            _inschrijving = inschrijving;
        }

        #endregion
    }
}

