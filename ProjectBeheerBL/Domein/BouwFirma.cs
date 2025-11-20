using ProjectBeheerBL.Domein.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class BouwFirma
    {
        const int MinLengteNaam = 2;
        const int MinLengteTel = 9;
        const int MaxLengteTel = 13;

        public BouwFirma(int id, string naam, string email, string telefoonNummer, string? website)
        {
            Id = id;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Website = website;
        }

        public BouwFirma(int id, string naam, string email, string telefoonNummer)
        {
            Id = id;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
        }


        private int _id;
        public int Id { get; private set; }

        private string _naam;
        public string Naam {
            get { return _naam; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Naam mag niet leeg zijn.");
                var trimmed = value.Trim();
                if (trimmed.Length < 2) throw new ProjectException($"Naam moet langer dan {MinLengteNaam} karakters zijn.");
                _naam = trimmed;
            }
        }

        private string _email;
        public string Email {
            get { return _email; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Contains('@')) _email = value;
                else throw new ProjectException($"email {value} niet ok");
            }
        }

        private string _telefoonNummer;
       

        public string TelefoonNummer { 
            get { return _telefoonNummer; }
            set {
                if (string.IsNullOrWhiteSpace(value)) throw new ProjectException("Telefoonnummer bouwfirma mag niet leeg zijn.");
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
                    throw new ProjectException("Telefoonnummer bouwfirma bevat ongeldige tekens.");

                // Lengte checken
                if (trimmed.Length < MinLengteTel || trimmed.Length > MaxLengteTel)
                    throw new ProjectException("telefoonnummer bouwfirma heeft geen geldige lengte.");

                _telefoonNummer = trimmed;
            }
        }
        private string? _website;
        public string? Website
        {
            get => _website;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _website = null;
                _website = value.Trim();
            }
        }

        public override string ToString()
        {
            return $"{Naam}, {Email}, {TelefoonNummer}";
        }
    }
}
