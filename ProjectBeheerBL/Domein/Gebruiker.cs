using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Beheerder;
using ProjectBeheerBL.Domein.Exceptions;

namespace ProjectBeheerBL.Domein
{
    public class Gebruiker
    {
        public Gebruiker(string naam, string email, GebruikersRol gebruikersRol)
        {
            Naam = naam;
            Email = email;
            GebruikersRol = gebruikersRol;
        }


        private string naam;
        public string Naam {
            get { return naam; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Naam mag niet leeg zijn");
                var trimmed = value.Trim();
                naam = trimmed;
            }
        }
        private string email;
        public string Email {
            get { return email; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains('@')) email = value;
                else throw new ProjectException ($"email {value} niet ok");
            }
        }
        public GebruikersRol GebruikersRol { get; set; }

        public override string? ToString()
        {
            return $"{Naam}, {Email}, {GebruikersRol}";
        }
    }
}
