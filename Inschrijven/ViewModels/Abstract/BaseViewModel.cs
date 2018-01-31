using Inschrijven.DAL;
using Inschrijven.Services;
using Inschrijven.Services.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Inschrijven.ViewModels.Abstract
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        internal IGegevensService _dataService;
        internal Page page;
        internal Frame frame;

        private ValidationService _validationService;
        public ValidationService ValidationService
        {
            get { return _validationService; }
            set { _validationService = value; OnPropertyChanged(); }
        }

        // Constructors
        #region Constructors

        public BaseViewModel(IGegevensService dataService, Frame frame, Page page)
        {
            this._dataService = dataService;
            this.frame = frame;
            this.page = page;
        }

        #endregion

        // INotifyPropertyChanged Implementation
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
