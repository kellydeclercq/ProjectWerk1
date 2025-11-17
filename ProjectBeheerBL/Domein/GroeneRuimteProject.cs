using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.Domein
{
    public class GroeneRuimteProject : Project
    {
        public GroeneRuimteProject(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, List<byte[]> fotos, List<byte[]> documenten) : base(id, projectTitel, beschrijving, startDatum, projectStatus, fotos, documenten)
            :base(id, beschrijving, startDatum, projectStatus, fotos, documenten)
        {
        }

        public double OppervlakteInVierkanteMeter {  get; set; }
        public int BioDiversiteitsScore { get; set; } //TODO kijken hoe we dit gaan afdwingen
        public int AantalWandelpaden { get; set; }
        public bool OpgenomenInWandelRoute { get; set; }
        public int? BezoekersScore { get; set; } //TODO optioneel + hoe keuzes afdwingen? Weer lijst of in BL
        //opties in UI kunnen opgevraagd worden via de OptieLijsten (klasse), de gekozen strings + vrij invoerveld worden in deze lijst gestoken
        public List<string> Faciliteiten { get; set; }

        
    }
}
