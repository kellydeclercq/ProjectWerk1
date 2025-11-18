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
            int? bezoekersScore, List<string> faciliteiten)
        {
            _repo.MaakGroeneruimteProjectAan(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, 
                oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, 
                faciliteiten);
        }

        public void MaakStadsontwikkelingsProjectAan(int? id, string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, StadsOntwikkeling stadsOntwikkeling)
        {

            _repo.MaakStadsontwikkelingsProjectAan(id, projectTitel, beschrijving,  startDatum,  projectStatus,
             wijk, fotos, documenten, partners, stadsOntwikkeling);
        }

        public void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            //parameters innovatief wonen
            int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen)
        {
            _repo.MaakInnovatiefWonenProjectAan( projectTitel,  beschrijving, startDatum,  projectStatus,
             wijk,  fotos,  documenten,  partners, aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
             samenwerkingToerisme, woonvormen);
        }

        public void MaakStadsOntwikkelingInnovatiefWonenProjectAan()
        {
            _repo.MaakStadsOntwikkelingInnovatiefWonenProjectAan();
        }

        public void MaakStadsOntwikkelingGroeneRuimteProjectAan()
        {
            _repo.MaakStadsOntwikkelingGroeneRuimteProjectAan();
        }

        public void MaakGroeneRuimteInnovatiefWonenProjectAan()
        {
            _repo.MaakGroeneRuimteInnovatiefWonenProjectAan();
        }

        public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject()
        {
            _repo.MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject();
        }


    }
}
