using ProjectBeheerBL.Domein.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class Partner
    {
        public Partner(string naam, string email, string telefoonNummer, string rolOmschrijving)
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            RolOmschrijving = rolOmschrijving;
        }

        public Partner(string naam, string email, string telefoonNummer, string? website, string rolOmschrijving)
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Website = website;
            RolOmschrijving = rolOmschrijving;
        }

        private string naam;
        public string Naam {
            get { return naam; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Naam mag niet leeg zijn.");
                var trimmed = value.Trim();
                if (trimmed.Length < 2) throw new ProjectException("Naam moet langer dan 2 karakters zijn.");
                naam = trimmed;
            }
        }
        private string email;
        public string Email {
            get { return email; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains('@')) email = value;
                else throw new ProjectException($"email {value} niet ok");
            }
        }
        public string TelefoonNummer { get; set; }
        public string? Website { get; set; }

        private string rolOmschrijving;
        public string RolOmschrijving
        {
            get { return rolOmschrijving; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Rolomschrijving mag niet leeg zijn.");
                var trimmed = value.Trim();
                if (trimmed.Length < 2) throw new ProjectException("Naam moet langer dan 2 karakters zijn.");
                rolOmschrijving = trimmed;
            }
        }
    }
}
