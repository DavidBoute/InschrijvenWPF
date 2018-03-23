using Inschrijven.Extensions;
using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
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
