using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using Inschrijven.Views.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class ZoekLerenKennenViewModel : BaseViewModel
    {
        private List<LerenKennenSoort> LijstManieren { get; set; }

        public LerenKennenSoort LerenKennenManier
        {
            get { return GetValue(() => LerenKennenManier); }
            set { SetValue(() => LerenKennenManier, value); }
        }

        // Commands
        #region Commands

        public ICommand SelecteerCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       Window window = frame.Parent as Window;
                       window.DialogResult = true;
                       window.Close();
                   });
            }
        }


        #endregion

        // Constructors
        #region Constructors

        public ZoekLerenKennenViewModel(IGegevensService dataService, Frame frame)
            : base(dataService, frame)
        {
            LijstManieren = dataService.GetAlleLerenKennenSoorten();

        }

        #endregion
    }
}
