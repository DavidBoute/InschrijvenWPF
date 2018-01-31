using Inschrijven.DAL;
using Inschrijven.Extensions;
using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
            private set { _leerkrachtenLijst = value; OnPropertyChanged(); }
        }

        private Leerkracht _huidigeLeerkracht;
        public Leerkracht HuidigeLeerkracht
        {
            get { return _huidigeLeerkracht; }
            set { _huidigeLeerkracht = value; OnPropertyChanged(); }
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
                   },
                   canExecute => HuidigeLeerkracht != null);
            }
        }

        #endregion

        // Constructors
        #region Constructors

        public LoginViewModel(IGegevensService dataService, Frame frame, Page page)
            : base(dataService, frame, page)
        {
            LeerkrachtenLijst = _dataService.GetAlleLeerkrachten().ToObservableCollection();
        }

        #endregion
    }
}
