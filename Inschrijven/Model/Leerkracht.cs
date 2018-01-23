using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Leerkracht
    {
        public Guid LeerkrachtId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public int Nummer { get; set; }
        public string Code { get; set; }
        public string VolledigeNaam { get {return Voornaam +" " + Familienaam; } }
    }
}
