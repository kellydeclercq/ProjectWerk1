using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Beheerder;

namespace ProjectBeheerBL.Domein
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public GebruikersRol GebruikersRol { get; set; }

    }
}
