using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inschrijven.Model
{
    public class Maaltijden
    {
        public Guid MaaltijdenId { get; set; }

        public bool HeeftMoneySafeAccount { get; set; }
        public bool HeeftMoneySafeKaart { get; set; }

        // Navigation Properties
        public Inschrijving Inschrijving { get; set; }
        public MaaltijdSoort MaandagMaaltijdSoort { get; set; }
        public MaaltijdSoort DinsdagMaaltijdSoort { get; set; }
        public MaaltijdSoort WoensdagMaaltijdSoort { get; set; }
        public MaaltijdSoort DonderdagMaaltijdSoort { get; set; }
        public MaaltijdSoort VrijdagMaaltijdSoort { get; set; }

    }
}
