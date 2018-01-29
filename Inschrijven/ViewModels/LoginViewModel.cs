using Inschrijven.DAL;
using Inschrijven.Helpers;
using Inschrijven.Model;
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


        public ICommand SelectedItemChangedCommand
        {
            get
            {
                return new RelayCommand(
                   (object obj) =>
                   {

                   });
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(
                   (object obj) =>
                   {
                       frame.Content = new StartInschrijvingView(db, frame, HuidigeLeerkracht);
                   },
                   canExecute => HuidigeLeerkracht != null);
            }
        }

        #endregion

        // Constructors
        #region Constructors

        public LoginViewModel(InschrijvingContext db, Frame frame, Page page)
            : base(db, frame, page)
        {
            LeerkrachtenLijst = new ObservableCollection<Leerkracht>(db.Leerkrachten);
        }

        #endregion
    }
}
