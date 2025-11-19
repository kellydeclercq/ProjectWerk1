using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerBL.typeSoorten
{
    public class InnovatiefWonen 
    {
        public InnovatiefWonen(int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed, 
            bool samenwerkingToerisme, List<string> woonvormen)
        {
            if (woonvormen == null) throw new ProjectException("Woonvormen mag geen NULL zijn.");
            AantalWooneenheden = aantalWooneenheden;
            RondleidingMogelijk = rondleidingMogelijk;
            InnovatieScore = innovatieScore;
            ShowwoningBeschikbaar = showwoningBeschikbaar;
            SamenwerkingErfgoed = samenwerkingErfgoed;
            SamenwerkingToerisme = samenwerkingToerisme;
            Woonvormen = woonvormen;
        }

        private int _aantalWoonheden;
        public int AantalWooneenheden { 
            get { return _aantalWoonheden; } 
            set{
                if(value <= 0) throw new ProjectException($"Aantal incorrect.");
                _aantalWoonheden = value;
                }
            }
        public bool RondleidingMogelijk { get; set; }

        private int? _innovatieScore;      
        public int? InnovatieScore {
            get { return _innovatieScore; }
            set {
                if (value == null)
                { 
                    _innovatieScore = null;
                    return;
                }

                if (value < 0 || value > 10) throw new ProjectException("Score moet tussen 0-10 zijn.");
                _innovatieScore = (int)value;
            }
        }
        public bool ShowwoningBeschikbaar { get; set; }
        public bool SamenwerkingErfgoed { get; set; }
        public bool SamenwerkingToerisme { get; set; }

        private List<string> woonvormen;
        public List<string> Woonvormen { 
            get => woonvormen;
            set
            {
                if (value == null) throw new ProjectException("Woonvormen mag geen NULL zijn.");
                woonvormen = value;
            }
        }


    }
}
