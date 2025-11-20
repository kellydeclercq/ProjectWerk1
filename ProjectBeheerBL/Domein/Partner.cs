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
        const int MinLengteNaam = 2;
        const int MinLengteTel = 9;
        const int MaxLengteTel = 13;
        const int MinRolChar = 2;
        public Partner(int id, string naam, string email, string telefoonNummer, string rolOmschrijving)
        {
            Id = id;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            RolOmschrijving = rolOmschrijving;
        }

        public Partner(int id, string naam, string email, string telefoonNummer, string? website, string rolOmschrijving)
        {
            Id = id;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Website = website;
            RolOmschrijving = rolOmschrijving;
        }

        private int _id;
        public int Id { get; private set; }

        private string _naam;
        public string Naam {
            get { return _naam; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Naam mag niet leeg zijn.");
                var trimmed = value.Trim();
                if (trimmed.Length < MinLengteNaam) throw new ProjectException($"Naam moet langer dan {MinLengteNaam} karakters zijn.");
                _naam = trimmed;
            }
        }
        private string _email;
        public string Email {
            get { return _email; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains('@')) _email = value;
                else throw new ProjectException($"email niet ok");
            }
        }
        private string _telefoonNummer;
        public string TelefoonNummer {
            get { return _telefoonNummer; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Telefoonnummer mag niet leeg zijn.");
                var trimmed = value.Trim();
                bool startMetPlus = trimmed.StartsWith("+");

                // checken bevat het nog steeds een +, zo ja verwijderen
                if (trimmed.Contains('+') && !startMetPlus) throw new ProjectException("+ mag enkel vooraan staan.");

                // andere char verwijderen
                trimmed = trimmed
                    .Replace(" ", "")
                    .Replace("/", "")
                    .Replace(".", "")
                    .Replace("-", "");
                  
                // + weghalen
                trimmed = startMetPlus ? trimmed.Substring(1) : trimmed;

                // checken of alles nu getallen zijn
                if (!trimmed.All(char.IsDigit))
                    throw new ProjectException("Telefoonnummer bevat ongeldige tekens.");

                // Lengte checken
                if (trimmed.Length < MinLengteTel|| trimmed.Length > MaxLengteTel)
                    throw new ProjectException("telefoonnummer heeft geen geldige lengte.");

                _telefoonNummer = trimmed;
            }
        }
        public string? Website { get; set; }

        private string _rolOmschrijving;
        public string RolOmschrijving {
            get { return _rolOmschrijving; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Rolomschrijving mag niet leeg zijn.");
                var trimmed = value.Trim();
                if (trimmed.Length < MinRolChar) throw new ProjectException($"Naam moet langer dan {MinRolChar} karakters zijn.");
                _rolOmschrijving = trimmed;
            }
        }
        public override string? ToString()
        {
            return $"{Naam}, {Email}, {TelefoonNummer}, rol: {RolOmschrijving}";
        }


    }
}
