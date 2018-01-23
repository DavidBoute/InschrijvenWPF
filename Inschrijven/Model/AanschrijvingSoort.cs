using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class AanschrijvingSoort
    {
        public int AanschrijvingSoortId { get; set; }
        public string Aanspreking { get; set; }

        // Navigation Properties
        public virtual ICollection<Adres> Adressen { get; set; }
    }
}
