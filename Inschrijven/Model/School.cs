using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class School
    {
        public int SchoolId { get; set; }
        public string OfficieleNaam { get; set; }
        public string Bijnaam { get; set; }
        public string Naam { get { return OfficieleNaam + (Bijnaam != String.Empty ? " - " + Bijnaam : ""); } } 
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public bool IsBuitenGewoon { get; set; }
        public bool IsKarelDeGoede { get; set; }
        
        // Navigation Properties
        public virtual OnderwijsSoort OnderwijsSoort { get; set; }
    }
}
