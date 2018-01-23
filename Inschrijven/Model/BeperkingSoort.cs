using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class BeperkingSoort
    {
        public int BeperkingSoortId { get; set; }
        public string BeperkingSoortNaam { get; set; }
        public bool IsVerwittigDirectie { get; set; }
        public bool IsVerslagNodig { get; set; }
    }
}
