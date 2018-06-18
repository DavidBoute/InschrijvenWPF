using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Toestemming
    {
        public int ToestemmingId { get; set; }
        public ToestemmingSoort ToestemmingSoort { get; set; }
        public bool IsAkkoord { get; set; }
    }
}
