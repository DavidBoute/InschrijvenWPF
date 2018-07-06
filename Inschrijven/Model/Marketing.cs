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
        public string WaaromGekozenSchool { get; set; }

        // Navigation Properties
        public virtual ICollection<LerenKennen> LerenKennenSchool { get; set; }
    }
}
