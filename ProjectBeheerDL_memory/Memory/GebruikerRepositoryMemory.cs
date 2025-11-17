using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerDL.Memory
{
    public class GebruikerRepositoryMemory
    {
        private Dictionary<int, Gebruiker> gebruikers = new();
        private int gebruikersId = 1;

        public GebruikerRepositoryMemory()
        {
            gebruikers = gebruikers.Add(gebruikersId, new Gebruiker()); gebruikersId++;
        }
    }
}
