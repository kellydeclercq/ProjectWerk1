using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class GroeneRuimteProject
    {
        public double OppervlakteInVierkanteMeter {  get; set; }
        public int BioDiversiteitsScore { get; set; } //TODO kijken hoe we dit gaan afdwingen
        public int AantalWandelpaden { get; set; }
        public bool OpgenomenInWandelRoute { get; set; }
        public int? BezoekersScore { get; set; } //TODO optioneel + hoe keuzes afdwingen? Weer lijst of in BL
        //opties in UI kunnen opgevraagd worden via de OptieLijsten (klasse), de gekozen strings + vrij invoerveld worden in deze lijst gestoken
        public List<string> Faciliteiten { get; set; } 


    }
}
