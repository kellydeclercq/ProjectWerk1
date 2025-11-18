using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.Domein
{
    public class GroeneRuimte
    {
        public GroeneRuimte(double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute, int? bezoekersScore, List<string> faciliteiten)
        {
            OppervlakteInVierkanteMeter = oppervlakteInVierkanteMeter;
            BioDiversiteitsScore = bioDiversiteitsScore;
            AantalWandelpaden = aantalWandelpaden;
            OpgenomenInWandelRoute = opgenomenInWandelRoute;
            BezoekersScore = bezoekersScore;
            Faciliteiten = faciliteiten;
        }

        private double _oppervlakteInVierkanteMeter;
        public double OppervlakteInVierkanteMeter {
            get { return _oppervlakteInVierkanteMeter; }
            set {
                if (value <= 0) throw new ProjectException($"Oppervlakte {value} incorrect.");
                _oppervlakteInVierkanteMeter = value;
            }
        }

        private int _bioDiversiteitsScore;
        public int? BioDiversiteitsScore {
            get { return _bioDiversiteitsScore; }
            set {
                if (value < 0 || value > 10) throw new ProjectException("Score moet tussen 0-10 zijn.");
                _bioDiversiteitsScore = value;
            }
        }
        public int? AantalWandelpaden { get; set; }
        public bool OpgenomenInWandelRoute { get; set; }
        public int? BezoekersScore { get; set; } //TODO optioneel + hoe keuzes afdwingen? Weer lijst of in BL
        //opties in UI kunnen opgevraagd worden via de OptieLijsten (klasse), de gekozen strings + vrij invoerveld worden in deze lijst gestoken
        public List<string> Faciliteiten { get; set; }

        
    }
}
