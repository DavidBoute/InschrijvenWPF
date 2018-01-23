using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Avondstudie
    {
        public Guid AvondstudieId { get; set; }

        // Foreign Keys
        public int MaandagAvondstudieSoortId { get; set; }
        public int DinsdagAvondstudieSoortId { get; set; }
        public int DonderdagAvondstudieSoortId { get; set; }
        public int VrijdagAvondstudieSoortId { get; set; }

        // Navigation Properties
        public AvondstudieSoort MaandagAvondstudieSoort { get; set; }
        public AvondstudieSoort DinsdagAvondstudieSoort { get; set; }
        public AvondstudieSoort DonderdagAvondstudieSoort { get; set; }
        public AvondstudieSoort VrijdagAvondstudieSoort { get; set; }
    }
}
