using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class Partner
    {
        public int? Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelefoonNummer { get; set; }
        public string Website { get; set; }
        //TODO rolomschrijving aparte klasse of niet?
        public string RolOmschrijving { get; set; }

        public Partner(int? id, string naam, string email, string telefoonNummer, string website, string rolOmschrijving)
        {
            Id = id;
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Website = website;
            RolOmschrijving = rolOmschrijving;
        }

        public Partner(string naam, string email, string telefoonNummer, string website, string rolOmschrijving)
        {
            Naam = naam;
            Email = email;
            TelefoonNummer = telefoonNummer;
            Website = website;
            RolOmschrijving = rolOmschrijving;
        }
    }
}
