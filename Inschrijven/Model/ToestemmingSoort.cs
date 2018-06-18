using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class ToestemmingSoort
    {
        public int ToestemmingSoortId { get; set; }
        public string Omschrijving { get; set; }
        public bool IsEnkelVoorEersteGraad { get; set; }
    }
}
