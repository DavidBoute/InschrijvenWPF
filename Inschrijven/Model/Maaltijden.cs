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
        public virtual MaaltijdSoort MaandagMaaltijdSoort { get; set; }
        public virtual MaaltijdSoort DinsdagMaaltijdSoort { get; set; }
        public virtual MaaltijdSoort WoensdagMaaltijdSoort { get; set; }
        public virtual MaaltijdSoort DonderdagMaaltijdSoort { get; set; }
        public virtual MaaltijdSoort VrijdagMaaltijdSoort { get; set; }

    }
}
