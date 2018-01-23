using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Contact
    {
        public Guid ContactId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string Opmerking { get; set; }
        public bool IsOverleden { get; set; }

        // Foreign Keys
        public Guid AdresId { get; set; }
        public int RelatieId { get; set; }
        public Guid EmailId { get; set; }

        // Navigation Properties
        public virtual Adres Adres { get; set; }
        public virtual RelatieSoort Relatie { get; set; }
        public virtual Email Email { get; set; }

        public virtual ICollection<Leerling> Leerlingen { get; set; }
        public virtual ICollection<Telefoon> TelefoonNummers { get; set; }
    }
}
