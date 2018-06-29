using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class BijkomendeInfo
    {
        public Guid BijkomendeInfoId { get; set; }
        public string Moedertaal { get; set; }
        public string MedischeProblemen { get; set; }
        public string TaalProblemen { get; set; }
        public string LeerProblemen { get; set; }

        public bool VerhoogdeZorgVraag { get; set; }
        public bool VerslagBuitengewoonOnderwijs { get; set; }
        public bool GemotiveerdVerslag { get; set; }
        public bool OndersteuningsUur { get; set; }
    }
}
