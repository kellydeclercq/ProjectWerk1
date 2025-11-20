using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerDL_SQL
{
    public class GebruikerRepository  : IGebruikerRepository
    {
        private readonly string _connectionString;

        public GebruikerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void UploadToDatabase()

        public bool BestaatGebruikerAl(string email) => throw new NotImplementedException();
        //{
            
        //    ////true: ja
        //    ////false: neen
        //    //return gebruikers.ContainsKey(email);
        //}

        public List<Gebruiker> GeefAlleGebruikers() => throw new NotImplementedException();
        //{
        //    return gebruikers.Values.ToList();
        //}

        public Gebruiker GeefGebruikeradhvEmail(string email) => throw new NotImplementedException();
        //{
        //    if (gebruikers.ContainsKey(email)) return gebruikers[email];
        //    return null;

        //}

        public void MaakNieuweGebruikerAan(string naam, string email, GebruikersRol rol) => throw new NotImplementedException();
        //{
        //    if (!gebruikers.ContainsKey(email)) gebruikers.Add(email, new Gebruiker(naam, email, rol));
        //}
    }
}
