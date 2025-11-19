using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.typeSoorten
{
    public class GroeneRuimte
    {
        const int MinBioscore = 1;
        const int MaxBioScore = 10;

        const int MinBezoekScore = 1;
        const int MaxBezoekScore = 5;
        public GroeneRuimte(double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute, 
            int? bezoekersScore, List<string> faciliteiten)
        {
            if (faciliteiten == null) throw new ProjectException("Faciliteiten mag geen NULL zijn.");
            OppervlakteInVierkanteMeter = oppervlakteInVierkanteMeter;
            BioDiversiteitsScore = bioDiversiteitsScore;
            AantalWandelpaden = aantalWandelpaden;
            OpgenomenInWandelRoute = opgenomenInWandelRoute;
            BezoekersScore = bezoekersScore;
            Faciliteiten = faciliteiten;
        }

        private double _oppervlakteInVierkanteMeter;
        public double OppervlakteInVierkanteMeter 
        {
            get { return _oppervlakteInVierkanteMeter; }
            set {
                if (value < 0) throw new ProjectException($"Oppervlakte moet hoger als 0 zijn");
                _oppervlakteInVierkanteMeter = value;
            }
        }

        private int? _bioDiversiteitsScore;
        public int? BioDiversiteitsScore {
            get { return _bioDiversiteitsScore; }
            set { if (value == null)
                {
                    _bioDiversiteitsScore = null;
                    return;
                }
            
                if (value < MinBioscore-1 || value > MaxBioScore) throw new ProjectException("Score moet tussen 0-10 zijn.");
                _bioDiversiteitsScore = (int)value;
            }
        }
        private int? _aantalWandelpaden;
        public int? AantalWandelpaden 
        {
            get { return _aantalWandelpaden; }
            set
            {
                if (value == null) _aantalWandelpaden = null;
                if (value < 0) throw new ProjectException("AantalWandelpaden mag niet lager als 0 zijn");
                _aantalWandelpaden = value;
            }
        }

        public bool OpgenomenInWandelRoute { get; set; }


        private int? _bezoekersScore;
        public int? BezoekersScore {
            get { return _bezoekersScore; }
            set {
                if (value == null)
                {
                    _bezoekersScore = null;
                    return;
                }

                if (value < MinBezoekScore-1 || value > MaxBezoekScore) throw new ProjectException("Score moet tussen 0-5 zijn.");
                _bezoekersScore = value;
            }
        }

        private List<string> _faciliteiten;
        public List<string> Faciliteiten 
        { get => _faciliteiten;
            set
            {
                if (value == null) throw new ProjectException("Faciliteiten mag geen NULL zijn.");
                _faciliteiten = value;
            }
        }

        
    }
}
