using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.Domein
{
    public class StadsOntwikkelingsProject : Project
    {
       

        public VergunningsStatus vergunningsStatus { get; set; }
        public bool ArchitecturaleWaarde { get; set; }
        public Toegankelijkheid Toegankelijkheid { get; set; }
        public bool BeziensWaardigheidVoorToeristen { get; set; }
        public bool InfobordenOfWandeling { get; set; } //TODO checken of dit niet beter opgesplitst wordt

        public StadsOntwikkelingsProject(VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoorToeristen, bool infobordenOfWandeling) µ
            :base(id, beschrijving, startDatum, projectStatus, fotos, documenten)
        {
            this.vergunningsStatus = vergunningsStatus;
            ArchitecturaleWaarde = architecturaleWaarde;
            Toegankelijkheid = toegankelijkheid;
            BeziensWaardigheidVoorToeristen = beziensWaardigheidVoorToeristen;
            InfobordenOfWandeling = infobordenOfWandeling;
        }
    }
}
