using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.typeSoorten;

namespace ProjectBeheerBL.Interfaces.Repo
{
    public interface IProjectRepository
    {
        void MaakGroeneRuimteInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar);

        public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, Gebruiker gebruiker);

        void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            //parameters innovatief wonen
            int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker gebruiker);

        void MaakStadsOntwikkelingGroeneRuimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten);

        void MaakStadsOntwikkelingInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar);

        void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker Eigenaar);

        void MaakStadsontwikkelingsProjectAan(string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, Gebruiker gebruiker);

        List<Project> GeefAlleProjecten();
        List<Project> GeefProjectenGefilterdOpType(string type);
        List<Project> GeefProjectenGefilterdOpPartners();
        List<Project> GeefProjectenGefilterdOpStatus(string status);
        List<Project> GeefProjectenGefilterdOpTitel(string titel);
    }
}
