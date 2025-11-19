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
        private Dictionary<string, Gebruiker> gebruikers = new();       // email is de key 

        public GebruikerRepositoryMemory()
        {
            //Gebruikers aanmaken
            Gebruiker admin1 = new Gebruiker("Tom", "Tom@school.be", GebruikersRol.Beheerder);
            Gebruiker gebruiker1 = new Gebruiker("Arno", "Arno@school.be", GebruikersRol.GewoneGebruiker);

            gebruikers.Add(admin1.Email, admin1);
            gebruikers.Add(gebruiker1.Email, gebruiker1);
        }

        public bool BestaatGebruikerAl(string email)
        {
            //true: ja
            //false: neen
            return gebruikers.ContainsKey(email);
        }

        public List<Gebruiker> GeefAlleGebruikers()
        {
            throw new NotImplementedException();
        }

        public Gebruiker GeefGebruikeradhvEmail(string email)
        {
            if (gebruikers.ContainsKey(email)) return gebruikers[email];
            return null;

        }

        public void MaakNieuweGebruikerAan(string naam, string email, GebruikersRol rol)
        { 
            if(!gebruikers.ContainsKey(email)) gebruikers.Add(email, new Gebruiker(naam, email, rol));
        }


    }
}
