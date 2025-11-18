using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.Domein
{
    public class StadsOntwikkeling : Project
    {
        public StadsOntwikkeling(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus, string wijk, List<byte[]>? fotos, List<byte[]>? documenten,
            VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen, bool infoBordenOfWandeling, List<BouwFirma> bouwFirmas) : 
            base(id, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten)
        {
            VergunningsStatus = vergunningsStatus;
            ArchitecturaleWaarde = architecturaleWaarde;
            Toegankelijkheid = toegankelijkheid;
            BeziensWaardigheidVoorToeristen = beziensWaardigheidVoortoeristen;
            InfobordenOfWandeling = infoBordenOfWandeling;
            BouwFirmas = new List<BouwFirma>();
        }

        public VergunningsStatus VergunningsStatus { get; set; }
        public bool ArchitecturaleWaarde { get; set; }
        public Toegankelijkheid Toegankelijkheid { get; set; }
        public bool BeziensWaardigheidVoorToeristen { get; set; }
        public bool InfobordenOfWandeling { get; set; } //TODO checken of dit niet beter opgesplitst wordt

        public List<BouwFirma> BouwFirmas { get; set; }
    }
}
