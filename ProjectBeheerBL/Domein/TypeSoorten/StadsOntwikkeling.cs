using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.typeSoorten
{
    public class StadsOntwikkeling 
    {
        public StadsOntwikkeling(VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen, 
            bool infoBordenOfWandeling, List<BouwFirma> bouwFirmas)            
        {
            if (bouwFirmas == null) throw new ProjectException("Bouwfirma's mag niet NULL zijn.");
            VergunningsStatus = vergunningsStatus;
            ArchitecturaleWaarde = architecturaleWaarde;
            Toegankelijkheid = toegankelijkheid;
            BeziensWaardigheidVoorToeristen = beziensWaardigheidVoortoeristen;
            InfobordenOfWandeling = infoBordenOfWandeling;
            BouwFirmas = bouwFirmas;
            
        }

        public VergunningsStatus VergunningsStatus { get; set; } 
        public bool ArchitecturaleWaarde { get; set; } 
        public Toegankelijkheid Toegankelijkheid { get; set; }
        public bool BeziensWaardigheidVoorToeristen { get; set; }
        public bool InfobordenOfWandeling { get; set; }
        private List<BouwFirma> bouwFirmas;
        public List<BouwFirma> BouwFirmas { 
            get => bouwFirmas;
            set
            {
                if (value == null) throw new ProjectException("Bouwfirma's mag niet NULL zijn.");
                bouwFirmas = value;
            }
        }
    }
}
