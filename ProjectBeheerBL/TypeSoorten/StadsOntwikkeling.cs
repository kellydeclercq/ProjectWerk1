using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.typeSoorten
{
    public class StadsOntwikkeling 
    {
        public StadsOntwikkeling(VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen, 
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas)            
        {
            VergunningsStatus = vergunningsStatus;
            ArchitecturaleWaarde = architecturaleWaarde;
            Toegankelijkheid = toegankelijkheid;
            BeziensWaardigheidVoorToeristen = beziensWaardigheidVoortoeristen;
            InfobordenOfWandeling = infoBordenOfWandeling;
            BouwFirmas = bouwfirmas;
        }

        public VergunningsStatus VergunningsStatus { get; set; }
        public bool ArchitecturaleWaarde { get; set; }
        public Toegankelijkheid Toegankelijkheid { get; set; }
        public bool BeziensWaardigheidVoorToeristen { get; set; }
        public bool InfobordenOfWandeling { get; set; }

        public List<BouwFirma> BouwFirmas { get; set; }
    }
}
