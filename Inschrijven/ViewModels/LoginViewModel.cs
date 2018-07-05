using Inschrijven.Extensions;
using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private ObservableCollection<Leerkracht> _leerkrachtenLijst;
        public ObservableCollection<Leerkracht> LeerkrachtenLijst
        {
            get { return _leerkrachtenLijst; }
            private set { _leerkrachtenLijst = value; }
        }

        [Required(ErrorMessage = "Selecteer een inschrijver")]
        public Leerkracht HuidigeLeerkracht
        {
            get { return GetValue(() => HuidigeLeerkracht); }
            set { SetValue(() => HuidigeLeerkracht, value); }
        }

        #endregion

        // Commands
        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(
                   (object obj) =>
                   {
                       frame.Content = new StartInschrijvingView(_dataService, frame, HuidigeLeerkracht);
                   });
            }
        }

        public ICommand ShortcutCommand
        {
            get
            {
                return new RelayCommand(
                   (object obj) =>
                   {
                       Guid guid = Guid.Parse("3fc434b6-48e4-419d-acc1-f73a51df9ac2");
                       Inschrijving inschrijving = _dataService.GetInschrijving(guid);

                       //frame.Content = new StartInschrijvingView(_dataService, frame, HuidigeLeerkracht, inschrijving);
                       //frame.Content = new LeerlingGegevensView(_dataService, frame, inschrijving);
                       //frame.Content = new ContactenGegevensView(_dataService, frame,  inschrijving);
                       //frame.Content = new MaaltijdenView(_dataService, frame, inschrijving);
                       //frame.Content = new OpmerkingenView(_dataService, frame, inschrijving);
                       //frame.Content = new VoorgaandeInschrijvingView(_dataService, frame, inschrijving);
                       frame.Content = new AkkoordToestemmingenView(_dataService, frame, inschrijving);
                   });
            }
        }

        public ICommand AfdrukCommand
        {
            get
            {
                return new RelayCommand(
                   (object obj) =>
                   {
                       Guid guid = Guid.Parse("3fc434b6-48e4-419d-acc1-f73a51df9ac2");
                       Inschrijving inschrijving = _dataService.GetInschrijving(guid);

                       frame.Content = new AfdrukView(_dataService, frame, inschrijving);
                   });
            }
        }

        #endregion

        // Constructors
        #region Constructors

        public LoginViewModel(IGegevensService dataService, Frame frame)
            : base(dataService, frame)
        {
            LeerkrachtenLijst = _dataService.GetAlleLeerkrachten().ToObservableCollection();
        }

        #endregion
    }
}
