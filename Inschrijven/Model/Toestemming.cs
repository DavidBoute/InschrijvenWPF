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
        private bool _isAkkoord;

        public int Id { get; set; }     
        public bool IsAkkoord
        {
            get { return _isAkkoord; }
            set
            {
                if (value != _isAkkoord)
                {
                    _isAkkoord = value;
                    PropertyChanged(this,new PropertyChangedEventArgs("IsAkkoord"));
                }
            }
        }

        public virtual ToestemmingSoort ToestemmingSoort { get; set; }
        public virtual Inschrijving Inschrijving { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
