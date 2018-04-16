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
    public abstract class BaseViewModel : PropertyChangedNotification
    {
        internal IGegevensService _dataService;
        internal Frame frame;

        // Constructors
        #region Constructors

        public BaseViewModel(IGegevensService dataService, Frame frame)
        {
            this._dataService = dataService;
            this.frame = frame;
        }

        #endregion
    }
}
