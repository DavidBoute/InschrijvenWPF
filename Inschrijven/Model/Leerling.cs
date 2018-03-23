using System;
using System.Collections.Generic;

namespace Inschrijven.Model
{
    public class Leerling
    {
        public Guid LeerlingId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string Volledigenaam { get { return Voornaam + " " + Familienaam; } }
        public DateTime Geboortedatum { get; set; }
        public string Geboorteplaats { get; set; } 
        public string Nationaliteit { get; set; }
        public string RijksregisterNummer { get; set; }
        public byte[] Foto { get; set; }

        // Navigation Properties
        public virtual Geslacht Geslacht { get; set; }
        public virtual Email Email { get; set; }
        public virtual Inschrijving Inschrijving { get; set; }
        public virtual BijkomendeInfo BijkomendeInfo { get; set; }

        public virtual ICollection<Contact> Contacten { get; set; }
        public virtual ICollection<Adres> Adressen { get; set; }
        public virtual ICollection<Telefoon> TelefoonNummers { get; set; }

    }
}
