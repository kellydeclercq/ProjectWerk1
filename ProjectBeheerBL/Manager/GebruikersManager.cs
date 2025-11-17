using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces;

namespace ProjectBeheerBL.Beheerder
{
    public class GebruikersManager
    {
        IGebruikerRepositoryMemory _repo;

        public GebruikersManager(IGebruikerRepositoryMemory repo)
        {
            _repo = repo;
        }
    }
}
