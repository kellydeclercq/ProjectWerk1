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
        private string _naam;
        public string Naam {
            get { return _naam; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Naam mag niet leeg zijn");
                var trimmed = value.Trim();
                _naam = trimmed;
            }
        }
        private string _email;
        public string Email {
            get { return _email; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains('@')) _email = value;
                else throw new ProjectException ($"email {value} niet ok");
            }
        }
        public GebruikersRol GebruikersRol { get; set; }


        public Gebruiker(string naam, string email, GebruikersRol gebruikersRol)
        {
            Naam = naam;
            Email = email;
            GebruikersRol = gebruikersRol;
        }
        public override string? ToString()
        {
            return $"{Naam}, {Email}, {GebruikersRol}";
        }
    }
}
