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

        // Foreign Keys
        public int SchooljaarId { get; set; }
        public int SchoolId { get; set; }
        public int AttestId { get; set; }

        // Navigation Properties
        public Schooljaar Schooljaar { get; set; }
        public School School { get; set; }
        public AttestSoort BehaaldAttest { get; set; }
    }
}
