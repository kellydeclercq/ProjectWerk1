using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.Repo;

namespace ProjectBeheerDL_Memory
{
    public class GebruikerRepositoryMemory : IGebruikerRepository
    {
        private Dictionary<string, Gebruiker> gebruikers = new();
        // email is de key 
        private int gebruikerId = 1;
        public GebruikerRepositoryMemory()
        {
            //Gebruikers aanmaken
            Gebruiker admin1 = new Gebruiker(gebruikerId, "Tom", "Tom@school.be", GebruikersRol.Beheerder); gebruikerId++;
            Gebruiker gebruiker1 = new Gebruiker(gebruikerId, "Arno", "Arno@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;

            gebruikers.Add(admin1.Email, admin1);
            gebruikers.Add(gebruiker1.Email, gebruiker1);

            Gebruiker admin2 = new Gebruiker(gebruikerId, "Kelly", "kelly@school.be", GebruikersRol.Beheerder); gebruikerId++;
            Gebruiker gebruiker2 = new Gebruiker(gebruikerId, "Carmen", "carmen@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;
            Gebruiker gebruiker3 = new Gebruiker(gebruikerId, "Loic", "loic@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;
            Gebruiker gebruiker4 = new Gebruiker(gebruikerId, "Adel", "adel@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;
            Gebruiker gebruiker5 = new Gebruiker(gebruikerId, "Lisa", "lisa@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;
            Gebruiker gebruiker6 = new Gebruiker(gebruikerId, "Eva", "eva@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;
            Gebruiker gebruiker7 = new Gebruiker(gebruikerId, "Billie", "billie@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;
            Gebruiker gebruiker8 = new Gebruiker(gebruikerId, "Matthias", "matthias@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;
            Gebruiker gebruiker9 = new Gebruiker(gebruikerId, "Sarah", "sarah@school.be", GebruikersRol.Beheerder); gebruikerId++;
            Gebruiker gebruiker10 = new Gebruiker(gebruikerId, "Jonas", "jonas@school.be", GebruikersRol.GewoneGebruiker); gebruikerId++;

            gebruikers.Add(admin2.Email, admin2);
            gebruikers.Add(gebruiker2.Email, gebruiker2);
            gebruikers.Add(gebruiker3.Email, gebruiker3);
            gebruikers.Add(gebruiker4.Email, gebruiker4);
            gebruikers.Add(gebruiker5.Email, gebruiker5);
            gebruikers.Add(gebruiker6.Email, gebruiker6);
            gebruikers.Add(gebruiker7.Email, gebruiker7);
            gebruikers.Add(gebruiker8.Email, gebruiker8);
            gebruikers.Add(gebruiker9.Email, gebruiker9);
            gebruikers.Add(gebruiker10.Email, gebruiker10);
        }

        public bool BestaatGebruikerAl(string email)
        {
            //true: ja
            //false: neen
            return gebruikers.ContainsKey(email);
        }

        public List<Gebruiker> GeefAlleGebruikers()
        {
            return gebruikers.Values.ToList();
        }

        public Gebruiker GeefGebruikeradhvEmail(string email)
        {
            if (gebruikers.ContainsKey(email)) return gebruikers[email];
            return null;

        }


        public void MaakNieuweGebruikerAan(string naam, string email, GebruikersRol rol)
        { 
            if(!gebruikers.ContainsKey(email)) 
            {
                gebruikers.Add(email, new Gebruiker(gebruikerId, naam, email, rol));
                gebruikerId++;
            }
        }


    }
}
