using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class BouwFirma
    {
      
        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public string? Website { get; set; }

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
    }
}
