using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.Domein
{
    public class InnovatiefWonenProject
    {
        public int AantalWooneenheden { get; set; }
        public bool RondleidingMogelijk { get; set; }
        public int InnovatieScore { get; set; } //TODO gaan we dit in BL afdwingen of lijst van opties hebben hier?
        public bool SamenwerkingErfgoed { get; set; }
        public bool SamenwerkingToerisme { get; set; }
        //opties in UI kunnen opgevraagd worden via de OptieLijsten (klasse), de gekozen strings + vrij invoerveld worden in deze lijst gestoken
        public List<string> Woonvormen {  get; set; }

        public InnovatiefWonenProject(int aantalWooneenheden, bool rondleidingMogelijk, int innovatieScore, bool samenwerkingErfgoed, bool samenwerkingToerisme, List<string> woonvormen)
        {
            AantalWooneenheden = aantalWooneenheden;
            RondleidingMogelijk = rondleidingMogelijk;
            InnovatieScore = innovatieScore;
            SamenwerkingErfgoed = samenwerkingErfgoed;
            SamenwerkingToerisme = samenwerkingToerisme;
            Woonvormen = woonvormen;
        }
    }
}
