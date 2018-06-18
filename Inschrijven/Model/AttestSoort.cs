using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class AttestSoort
    {
        public int AttestSoortId { get; set; }
        public string AttestNaam { get; set; }
        public bool IsClausuleringVereist { get; set; }
    }
}
