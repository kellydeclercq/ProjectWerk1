using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Interfaces.Repo;

namespace ProjectBeheerDL_Memory
{
    public class GebruikerRepositoryMemory : IGebruikerRepository
    {
        private Dictionary<int, Gebruiker> gebruikers = new();
        private int gebruikersId = 1;

        public GebruikerRepositoryMemory()
        {
            gebruikers.Add(gebruikersId, new Gebruiker()); gebruikersId++;
        }
    }
}
