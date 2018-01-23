using Inschrijven.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.ViewModels.Abstract
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Constructor
        public BaseViewModel(InschrijvingContext db)
        {

        }

        public BaseViewModel():this(new InschrijvingContext()) { }
        
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
