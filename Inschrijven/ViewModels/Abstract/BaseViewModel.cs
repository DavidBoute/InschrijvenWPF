using Inschrijven.DAL;
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
        internal InschrijvingContext db;
        internal Page page;
        internal Frame frame;

        // Constructors
        #region Constructors
 
        public BaseViewModel(InschrijvingContext db, Frame frame, Page page)
        {
            this.db = db;
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
