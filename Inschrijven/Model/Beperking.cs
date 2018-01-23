using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Beperking
    {
        public Guid BeperkingId { get; set; }
        public string NaamAlternatief { get; set; }
        public bool HeeftAttest { get; set; }
        public bool IsAttestIngediend { get; set; }
        public bool IsVerslagIngediend { get; set; }
        public bool IsMDecreet { get; set; }
        public string MDecreetMaatregelen { get; set; }

        // Foreign Keys
        public Guid LeerlingId { get; set; }
        public int BeperkingSoortId { get; set; }

        // Navigation Properties
        public Leerling Leerling { get; set; }
        public BeperkingSoort BeperkingSoort { get; set; }
    }
}
