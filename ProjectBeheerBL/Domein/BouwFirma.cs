using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class BouwFirma
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public string Website { get; set; }

        public BouwFirma(int id, string naam, string email, string telefoonNummer, string website)
        {
            Id = id;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Website = website;
        }
    }
}
