using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Adres
    {
        public Guid AdresId { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Deelgemeente { get; set; }

        public bool IsDomicilie { get; set; }
        public bool IsAanschrijf { get; set; }
        public bool IsInternaat { get; set; }

        // Foreign Keys
        public int AansprekingSoortId { get; set; }

        // Navigation Properties
        public virtual AanschrijvingSoort Aanschrijving { get; set; }

        public virtual ICollection<Leerling> Leerlingen { get; set; }
        public virtual ICollection<Contact> Contacten { get; set; }


    }
}
