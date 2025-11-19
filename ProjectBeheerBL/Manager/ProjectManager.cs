using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.Repo;
using ProjectBeheerBL.typeSoorten;

namespace ProjectBeheerBL.Beheerder
{
    public class ProjectManager
    {
       private IProjectRepository _repo;
       public LijstService LijstService;

        public ProjectManager(IProjectRepository repo)
        {
            _repo = repo;
        }

        public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            // parameters aanmaak groeneRuimte
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, Gebruiker gebruiker, Adres adres)
        {
            _repo.MaakGroeneruimteProjectAan(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, 
                oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, 
                faciliteiten, gebruiker, adres);
        }

        public void MaakStadsontwikkelingsProjectAan(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, 
             VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, Gebruiker gebruiker, Adres adres)
        {

            _repo.MaakStadsontwikkelingsProjectAan(projectTitel, beschrijving,  startDatum,  projectStatus,
             wijk, fotos, documenten, partners,  vergunningsStatus,  architecturaleWaarde,  toegankelijkheid,  beziensWaardigheidVoortoeristen,
             infoBordenOfWandeling,  bouwfirmas, gebruiker, adres);
        }

        public void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            //parameters innovatief wonen
            int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker gebruiker, Adres adres)
        {
            _repo.MaakInnovatiefWonenProjectAan( projectTitel,  beschrijving, startDatum,  projectStatus,
             wijk,  fotos,  documenten,  partners, aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
             samenwerkingToerisme, woonvormen, gebruiker, adres);
        }

        public void MaakStadsOntwikkelingInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres)
        {
            _repo.MaakStadsOntwikkelingInnovatiefWonenProjectAan( projectTitel,  beschrijving,  startDatum,  projectStatus,
             wijk, fotos,  documenten, partners,
             vergunningsStatus,  architecturaleWaarde,  toegankelijkheid,  beziensWaardigheidVoortoeristen,
             infoBordenOfWandeling,  bouwfirmas,  aantalWooneenheden,  rondleidingMogelijk,  innovatieScore,  showwoningBeschikbaar,  samenwerkingErfgoed,
             samenwerkingToerisme,  woonvormen,  eigenaar, adres);
        }

        public void MaakStadsOntwikkelingGroeneRuimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten)
        {
            _repo.MaakStadsOntwikkelingGroeneRuimteProjectAan(projectTitel,  beschrijving,  startDatum,  projectStatus,
             wijk,  fotos,  documenten,  partners,  projectEigenaar, adres, vergunningsStatus,  architecturaleWaarde,  toegankelijkheid,  beziensWaardigheidVoortoeristen,
             infoBordenOfWandeling, bouwfirmas,  oppervlakteInVierkanteMeter,  bioDiversiteitsScore,  aantalWandelpaden,  opgenomenInWandelRoute,
             bezoekersScore, faciliteiten);
        }

        public void MaakGroeneRuimteInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres)
        {
            _repo.MaakGroeneRuimteInnovatiefWonenProjectAan(projectTitel, beschrijving, startDatum,  projectStatus,  wijk,
             fotos,  documenten,  partners,  oppervlakteInVierkanteMeter,  bioDiversiteitsScore, aantalWandelpaden,  opgenomenInWandelRoute,
             bezoekersScore,  faciliteiten,  aantalWooneenheden,  rondleidingMogelijk, innovatieScore,  showwoningBeschikbaar,  samenwerkingErfgoed,
             samenwerkingToerisme,  woonvormen,  eigenaar, adres);
        }

        public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker Eigenaar, Adres adres)
        {
            _repo.MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject( projectTitel,  beschrijving,  startDatum,  projectStatus,
             wijk, fotos,  documenten, partners,
             oppervlakteInVierkanteMeter,  bioDiversiteitsScore,  aantalWandelpaden,  opgenomenInWandelRoute,
            bezoekersScore, faciliteiten,  vergunningsStatus,  architecturaleWaarde,  toegankelijkheid,  beziensWaardigheidVoortoeristen,
             infoBordenOfWandeling,  bouwfirmas,  aantalWooneenheden,  rondleidingMogelijk,  innovatieScore,  showwoningBeschikbaar,  samenwerkingErfgoed,
             samenwerkingToerisme,  woonvormen, Eigenaar, adres);
        }

        public List<Project> GeefAlleProjecten()
        {
            return _repo.GeefAlleProjecten();
        }

        public List<Project> GeefProjectenGefilterdOpType(string type)
        {
            return _repo.GeefProjectenGefilterdOpType(type);
        }

        public List<Project> GeefProjectenGefilterdOpPartners(string partners)
        {
            return _repo.GeefProjectenGefilterdOpPartners();
        }

        public List<Project> GeefProjectenGefilterdOpStatus(string status)
        {
            return _repo.GeefProjectenGefilterdOpStatus(status);
        }

        public List<Project> GeefProjectenGefilterdOpTitel(string titel)
        {
            return _repo.GeefProjectenGefilterdOpTitel(titel);
        }
    }
}
