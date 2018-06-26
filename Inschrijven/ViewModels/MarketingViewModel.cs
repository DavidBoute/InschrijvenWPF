using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using Inschrijven.Views.Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class MarketingViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;


        public ObservableCollection<LerenKennenSoort> LerenKennenSchool
        {
            get { return GetValue(() => LerenKennenSchool); }
            set { SetValue(() => LerenKennenSchool, value); }
        }

        public string LerenKennenSchoolVaria
        {
            get { return GetValue(() => LerenKennenSchoolVaria); }
            set { SetValue(() => LerenKennenSchoolVaria, value); }
        }

        public string WaaromGekozenSchool
        {
            get { return GetValue(() => WaaromGekozenSchool); }
            set { SetValue(() => WaaromGekozenSchool, value); }
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

                       //frame.Content = new AkkoordToestemmingenView(_dataService, frame, _inschrijving);

                   });
            }
        }

        public ICommand ZoekCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       ModalWindow modalWindow = new ModalWindow();
                       ZoekLerenKennenView view = new ZoekLerenKennenView(_dataService, modalWindow.Frame);
                       modalWindow.Frame.Content = view;

                       bool? done = modalWindow.ShowDialog();

                       if (done ?? false)
                       {
                           ZoekLerenKennenViewModel vm = view.DataContext as ZoekLerenKennenViewModel;
                           LerenKennenSoort newLerenKennen = vm.LerenKennenManier;
                           _inschrijving.Marketing.LerenKennenSchool.Add(newLerenKennen);
                           LerenKennenSchool = new ObservableCollection<LerenKennenSoort>(_inschrijving.Marketing.LerenKennenSchool);
                       }

                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules


        #endregion

        // Constructors
        #region Constructors

        public MarketingViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            if (inschrijving.Marketing is null)
            {
                inschrijving.Marketing = new Marketing() { MarketingId = Guid.NewGuid() };
                inschrijving.Marketing.LerenKennenSchool = new List<LerenKennenSoort>();
            }

            LerenKennenSchool = new ObservableCollection<LerenKennenSoort>(inschrijving.Marketing.LerenKennenSchool);
            LerenKennenSchoolVaria = inschrijving.Marketing.LerenKennenSchoolVaria;

            WaaromGekozenSchool = inschrijving.Marketing.WaaromGekozenSchool;

            _inschrijving = inschrijving;
        }

        #endregion
    }
}
