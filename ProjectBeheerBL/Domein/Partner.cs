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
        private string telefoonNummer;
        public string TelefoonNummer {
            get { return telefoonNummer; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Telefoonnummer mag niet leeg zijn.");
                var trimmed = value.Trim();

                if (trimmed.Contains('+') && !trimmed.StartsWith('+')) throw new ProjectException("+ mag enkel vooraan staan.");
                bool heeftPlus = trimmed.StartsWith("+");
                trimmed = trimmed
                    .Replace(" ", "")
                    .Replace("/", "")
                    .Replace(".", "")
                    .Replace("-", "");

                string digits = heeftPlus ? trimmed.Substring(1) : trimmed;
                if (!digits.All(char.IsDigit))
                    throw new ProjectException("Telefoonnummer bevat ongeldige tekens.");

                if (digits.Length < 9 || digits.Length > 15)
                    throw new ProjectException("telefoonnummer heeft geen geldige lengte.");

                telefoonNummer = trimmed;
            }
        }
        public string? Website { get; set; }

        private string rolOmschrijving;
        public string RolOmschrijving {
            get { return rolOmschrijving; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Rolomschrijving mag niet leeg zijn.");
                var trimmed = value.Trim();
                if (trimmed.Length < 2) throw new ProjectException("Naam moet langer dan 2 karakters zijn.");
                rolOmschrijving = trimmed;
            }
        }
        public override string? ToString()
        {
            return $"{Naam}, {Email}, {TelefoonNummer}, rol: {RolOmschrijving}";
        }


    }
}
