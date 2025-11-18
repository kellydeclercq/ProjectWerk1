using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;

namespace ProjectBeheerBL.Interfaces.Repo
{
    public interface IGebruikerRepository
    {
        public Gebruiker GeefGebruikeradhvEmail(string email);

        public void MaakNieuweGebruikerAan(string naam, string email, GebruikersRol rol);

        public bool BestaatGebruikerAl(string email);
        

    }
}
