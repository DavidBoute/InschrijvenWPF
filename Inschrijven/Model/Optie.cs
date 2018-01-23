using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Optie
    {
        public int OptieId { get; set; }
        public string Naam { get; set; }

        // Navigation Properties
        public virtual ICollection<Richting> Richtingen { get; set; }
    }
}
