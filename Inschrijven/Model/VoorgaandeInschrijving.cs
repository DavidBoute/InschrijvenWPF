using System;

namespace Inschrijven.Model
{
    public class VoorgaandeInschrijving
    {
        public Guid VoorgaandeInschrijvingId { get; set; }
        public int Jaar { get; set; }
        public string Richting { get; set; }
        public string Clausulering { get; set; }
        public bool IsAttestGezien { get; set; }
        public bool IsBaSoAfgegeven { get; set; }

        // Navigation Properties
        public virtual Schooljaar Schooljaar { get; set; }
        public virtual School School { get; set; }
        public virtual AttestSoort BehaaldAttest { get; set; }
    }
}
