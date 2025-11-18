using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;

namespace ProjectBeheerBL.Interfaces.Repo
{
    public interface IProjectRepository
    {
        void MaakGroeneRuimteInnovatiefWonenProjectAan();

        public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten);

        void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            //parameters innovatief wonen
            int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen);

        void MaakStadsOntwikkelingGroeneRuimteProjectAan();

        void MaakStadsOntwikkelingInnovatiefWonenProjectAan();

        void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject();

        void MaakStadsontwikkelingsProjectAan();

    }
}
