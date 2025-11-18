using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Interfaces.Repo;

namespace ProjectBeheerBL.Beheerder
{
    public class GebruikersManager
    {
        IGebruikerRepository _repo;

        public GebruikersManager(IGebruikerRepository repo)
        {
            _repo = repo;
        }
    }
}
