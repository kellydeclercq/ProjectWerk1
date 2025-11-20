using Microsoft.Data.SqlClient;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.Exceptions;
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


        public bool BestaatGebruikerAl(string email)
        {
            const string sql = @"SELECT COUNT(*) FROM gebruiker WHERE email = @Email";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
       

        public List<Gebruiker> GeefAlleGebruikers()
        {
            const string sql = @"SELECT id, naam, email, gebruikersrol FROM gebruiker;";

            var lijst = new List<Gebruiker>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string naam = reader.GetString(1);
                        string email = reader.GetString(2);
                        string rolString = reader.GetString(3);

                        GebruikersRol rol = Enum.Parse<GebruikersRol>(rolString);

                        var gebruiker = new Gebruiker(id, naam, email, rol);
                        lijst.Add(gebruiker);
                    }
                }
            }
            return lijst;
        }


        public Gebruiker GeefGebruikeradhvEmail(string email) => throw new NotImplementedException();


        public void MaakNieuweGebruikerAan(string naam, string email, GebruikersRol rol)
        {
            if (BestaatGebruikerAl(email)) throw new ProjectException("Er bestaat al een gebruiker met dit email.");

            const string sql =
                @"INSERT INTO gebruiker(naam, email, gebruikersrol) VALUES (@Naam, @Email, @GebruikersRol);";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Naam", naam);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@GebruikersRol", rol.ToString());

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
