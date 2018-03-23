using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Telefoon
    {
        public Guid TelefoonId { get; set; }
        public string Nummer { get; set; }
        public string Opmerking { get; set; }

        // Navigation Properties
        public TelefoonSoort TelefoonSoort { get; set; }
    }
}
