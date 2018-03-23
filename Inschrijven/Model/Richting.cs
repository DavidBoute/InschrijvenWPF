using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Richting
    {
        public int RichtingId { get; set; }
        public int Jaar { get; set; }
        public string Naam { get; set; }
        public int Capaciteit { get; set; }
        public bool Register { get { return Capaciteit != 0; } }
        public int AantalEigenLeerlingenIngeschreven { get; set; }
        public bool HeeftOpties { get { return Opties.Count != 0; } }

        // Navigation Properties
        public virtual ICollection<Optie> Opties { get; set; }
    }
}
