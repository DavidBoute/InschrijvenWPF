using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Toestemming:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isAkkoord;

        public int ToestemmingId { get; set; }
        public virtual ToestemmingSoort ToestemmingSoort { get; set; }
        public bool IsAkkoord
        {
            get { return _isAkkoord; }
            set
            {
                if (value != _isAkkoord)
                {
                    _isAkkoord = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsAkkoord"));                  
                }
            }
        }

       
    }
}
