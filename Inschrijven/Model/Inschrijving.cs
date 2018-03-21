using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Inschrijving
    {
        public Guid InschrijvingId { get; set; }
        public DateTime StartTijd { get; set; }
        public bool IsHerinschrijving { get; set; }
        public bool IsAvondstudie { get; set; }


        // Foreign Keys
        public Guid LeerlingId { get; set; }
        public Guid LeerkrachtId { get; set; }
        public int RichtingId { get; set; }
        public int OptieId { get; set; }
        public int SchooljaarId { get; set; }
        public Guid MaaltijdenId { get; set; }
        public Guid AvonstudieId { get; set; }
        public int InschrijvingStatusId { get; set; }
        public Guid ToestemmingId { get; set; }
        public Guid MarketingId { get; set; }


        // Navigation Properties
        public virtual Leerling Leerling { get; set; }
        public virtual Leerkracht Leerkracht { get; set; }
        public virtual Richting Richting { get; set; }
        public virtual Optie Optie { get; set; }
        public virtual Schooljaar Schooljaar {get; set;}
        public virtual Maaltijden Maaltijden { get; set; }
        public virtual InschrijvingStatus InschrijvingStatus { get; set; }
        public virtual Marketing Marketing { get; set; }
        
        public virtual ICollection<VoorgaandeInschrijving> VoorgaandeInschrijvingen { get; set; }
        public virtual ICollection<Toestemming> Toestemmingen { get; set; }
    }
}
