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
        public string MedischeProblemen { get; set; }
        public string FamilialeSituatie { get; set; }
        
        // Foreign Keys
        public int TaalMoederTaalSoortId { get; set; }

        // Navigation Properties
        public TaalSoort TaalMoeder { get; set; }

        public ICollection<Beperking> BeperkingLijst { get; set; }

    }
}
