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
                        int id = (int)reader["id"];
                        string naam = (string)reader["naam"];
                        string email = (string)reader["email"];
                        string rolString = (string)reader["gebruikersrol"];

                        GebruikersRol rol = Enum.Parse<GebruikersRol>(rolString);

                        lijst.Add(new Gebruiker(id, naam, email, rol));
                    }
                }
            }
                return lijst;
        }


        public Gebruiker GeefGebruikeradhvEmail(string email)
        {
            const string sql = @"SELECT id, naam, email, gebruikersrol FROM gebruiker WHERE email = @Email;";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string naam = (string)reader["naam"];
                        string mail = (string)reader["email"];
                        string rolString = (string)reader["gebruikersrol"];

                        GebruikersRol rol = Enum.Parse<GebruikersRol>(rolString);

                        return new Gebruiker(id, naam, mail, rol);
                    }
                }
            }


            return null;
        }

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
