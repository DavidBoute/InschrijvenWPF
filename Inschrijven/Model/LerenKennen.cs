using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class LerenKennen: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isReden;

        public int LerenKennenId { get; set; }
        public virtual LerenKennenSoort LerenKennenSoort { get; set; }
        public bool IsReden
        {
            get { return _isReden; }
            set
            {
                if (value != _isReden)
                {
                    _isReden = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsReden"));
                }
            }
        }
    }
}
