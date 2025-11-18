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
        public BouwFirma(string naam, string email, string telefoonNummer, string? website)
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Website = website;
        }

        public BouwFirma(string naam, string email, string telefoonNummer)
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
        }

        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public string? Website { get; set; }

        public override string ToString()
        {
            return $"{Naam}, {Email}, {TelefoonNummer}";
        }
    }
}
