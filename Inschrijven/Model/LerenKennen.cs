using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class LerenKennen
    {
        public int LerenKennenId { get; set; }
        public LerenKennenSoort LerenKennenSoort { get; set; }
        public bool IsReden { get; set; }
    }
}
