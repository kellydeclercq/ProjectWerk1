using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;
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

        public bool BestaatGebruikerAl(string email)
        {
            return _repo.BestaatGebruikerAl(email);
        }

        public Gebruiker GeefGebruikeradhvEmail(string email)
        {
            return _repo.GeefGebruikeradhvEmail(email);
        }

        public void MaakNieuweGebruikerAan(string naam, string email, GebruikersRol rol)
        {
            _repo.MaakNieuweGebruikerAan(naam, email, rol);
        }

        public List<Gebruiker> GeefAlleGebruikers()
        {
            return _repo.GeefAlleGebruikers();
        }

    }
}
