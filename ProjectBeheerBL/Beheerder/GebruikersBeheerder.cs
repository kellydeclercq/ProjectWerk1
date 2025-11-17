using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces;

namespace ProjectBeheerBL.Beheerder
{
    public class GebruikersBeheerder
    {
        IGebruikerRepositoryMemory _repo;

        public GebruikersBeheerder(IGebruikerRepositoryMemory repo)
        {
            _repo = repo;
        }
    }
}
