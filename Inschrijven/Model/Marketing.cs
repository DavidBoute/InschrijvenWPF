using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Marketing
    {
        public Guid MarketingId { get; set; }
        
        public string LerenKennenSchoolVaria { get; set; }
        public string LerenKennenKarelDeGoedeVaria { get; set; }

        // Navigation Properties
        public ICollection<LerenKennenManier> LerenKennenSchool { get; set; }
        public ICollection<LerenKennenManier> LerenKennenKarelDeGoede { get; set; }
    }
}
