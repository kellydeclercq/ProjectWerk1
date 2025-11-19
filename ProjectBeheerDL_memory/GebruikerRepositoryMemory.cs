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
        private Dictionary<string, Gebruiker> gebruikers = new();       // email is de key 

        public GebruikerRepositoryMemory()
        {
            //Gebruikers aanmaken
            Gebruiker admin1 = new Gebruiker("Tom", "Tom@school.be", GebruikersRol.Beheerder);
            Gebruiker gebruiker1 = new Gebruiker("Arno", "Arno@school.be", GebruikersRol.GewoneGebruiker);

            gebruikers.Add(admin1.Email, admin1);
            gebruikers.Add(gebruiker1.Email, gebruiker1);

            Gebruiker admin2 = new Gebruiker("Kelly", "kelly@school.be", GebruikersRol.Beheerder);
            Gebruiker gebruiker2 = new Gebruiker("Carmen", "carmen@school.be", GebruikersRol.GewoneGebruiker);
            Gebruiker gebruiker3 = new Gebruiker("Loic", "loic@school.be", GebruikersRol.GewoneGebruiker);
            Gebruiker gebruiker4 = new Gebruiker("Adel", "adel@school.be", GebruikersRol.GewoneGebruiker);
            Gebruiker gebruiker5 = new Gebruiker("Lisa", "lisa@school.be", GebruikersRol.GewoneGebruiker);
            Gebruiker gebruiker6 = new Gebruiker("Eva", "eva@school.be", GebruikersRol.GewoneGebruiker);
            Gebruiker gebruiker7 = new Gebruiker("Billie", "billie@school.be", GebruikersRol.GewoneGebruiker);
            Gebruiker gebruiker8 = new Gebruiker("Matthias", "matthias@school.be", GebruikersRol.GewoneGebruiker);
            Gebruiker gebruiker9 = new Gebruiker("Sarah", "sarah@school.be", GebruikersRol.Beheerder);
            Gebruiker gebruiker10 = new Gebruiker("Jonas", "jonas@school.be", GebruikersRol.GewoneGebruiker);

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

        public List<Project> GeefProjectenGefilterdOpPartners()
        {
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpStatus(string status)
        {
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpTitel(string titel)
        {
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpType(string type)
        {
            throw new NotImplementedException();
        }

        public void MaakNieuweGebruikerAan(string naam, string email, GebruikersRol rol)
        { 
            if(!gebruikers.ContainsKey(email)) gebruikers.Add(email, new Gebruiker(naam, email, rol));
        }


    }
}
