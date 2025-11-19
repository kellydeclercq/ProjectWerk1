using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.ProjectTypesSubklasses;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.Repo;
using ProjectBeheerBL.typeSoorten;

namespace ProjectBeheerDL_Memory
{
    public class ProjectRepositoryMemory : IProjectRepository
    {
        private IGebruikerRepository gebruikersRepo;
        private int projectId = 1;
        private Dictionary<int, Project> projectLijst = new Dictionary<int, Project>();
        
        //foto's en documenten optioneel maken
        private LijstService lijstService = new LijstService();
        private Dictionary<int, Project> projecten = new();
        private List<byte[]> fotos = new List<byte[]>();
        private List<byte[]> documenten = new List<byte[]>();
        private List<string> woonvormen = new();
        private List<string> faciliteiten = new();  
        private List<BouwFirma> bouwFirmas = new();
        private List<Partner> partners = new();
        private List<Partner> partners2 = new();
      
        
        string langeBeschrijving = "Dit bouwproject omvat de gefaseerde ontwikkeling van een multifunctioneel complex waarin duurzaamheid, efficiëntie en toekomstbestendigheid centraal staan. Tijdens de ontwerpfase worden verschillende constructieve opties onderzocht om zowel de esthetische als functionele doelstellingen te waarborgen. De uitvoering wordt gepland in nauw overleg met betrokken stakeholders, waarbij bijzondere aandacht wordt besteed aan logistieke routing en minimale verstoring van de omgeving.\r\n\r\nIn het hoofdgebouw wordt een modulaire structuur toegepast die flexibiliteit biedt voor toekomstige aanpassingen. De materialen worden geselecteerd op basis van energieprestatie, levensduur en circulaire toepassingsmogelijkheden. Daarnaast wordt een geavanceerd monitoringsysteem geïntegreerd om energieverbruik, veiligheidsparameters en klimaatbeheersing in realtime te optimaliseren.\r\n\r\nDe buitenruimte krijgt een groene invulling met onderhoudsarme beplanting, waterdoorlatende bestrating en strategische verlichting die zowel veiligheid als sfeer ondersteunt. Het bouwteam werkt volgens een strak kwaliteitsprotocol om consistentie te garanderen tijdens alle projectfasen. Eventuele afwijkingen worden tijdig gerapporteerd en beoordeeld, zodat de planning en begroting binnen de gestelde kaders blijven.";

        public ProjectRepositoryMemory(IGebruikerRepository gebruikersRepo)
        {
            #region hard coded data
            lijstService.Faciliteiten.Add("speeltuin");
            lijstService.Faciliteiten.Add("picknickzone");
            lijstService.Faciliteiten.Add("inforborden");

            lijstService.Woonvormen.Add("co-housing");
            lijstService.Woonvormen.Add("modulaire woningen");

            woonvormen.Add(lijstService.Woonvormen[0]);
            faciliteiten.Add(lijstService.Faciliteiten[0]);
            faciliteiten.Add(lijstService.Faciliteiten[1]);

            //Partners aanmaken
            partners.Add(new Partner("AOC group", "jos@aoc.com", "0471 22 44 66", "www.aoc.be", "Ruwbouw"));
            partners.Add(new Partner("StromendWater group", "ann.@stromendwater.com", "0499 23 11 02", "www.stromendwater.be", "Sanitair"));
            partners.Add(new Partner("Volts group", "John@volts.com", "0486 25 36 66", "www.voltsElectric.be", "Electra"));
            partners2.Add (new Partner("kunstschool", "kunst@academie.com", "0492 22 88 66", "www.academie.be", "muurschilders"));

            //Bouwfirma aanmaken
            BouwFirma b = new BouwFirma("ElectricienJos", "jos@electricien.be", "0498751245", "www.ElectricienJos.com");
            BouwFirma b2 = new BouwFirma("Giproc Werken Gent", "Maarten@gmail.be", "0497845245", "www.giprocGent.com");


            // Drie typen project aanmaken (twee opties voor diversiteit)
            StadsOntwikkeling stadsontwikkeling  =  new StadsOntwikkeling(VergunningsStatus.Goegekeurd, false, Toegankelijkheid.Gedeeltelijk, false, true, bouwFirmas);
            GroeneRuimte groeneRuimte = new GroeneRuimte(400.50, 4, 2, true,  null, faciliteiten);           
            InnovatiefWonen innovatiefWonen = new InnovatiefWonen( 4, true, 9, true, false, false, woonvormen);
            
            //TODO gebruiker toevoegen

            // 7 projecten aanmaken (elke soort)

            GroeneRuimteInnovatiefWonenProject griwp = new GroeneRuimteInnovatiefWonenProject(projectId, "Modern Wonen aan de Bijloke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "Bijloke", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], groeneRuimte, innovatiefWonen);
            projectId++;
            GroeneRuimteProject grp = new GroeneRuimteProject(projectId, "Groene bibilitoheek in oostakker", langeBeschrijving,
                new DateTime(2024, 07 - 4, 24), ProjectStatus.Planning, "Overpoort", fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[0], groeneRuimte);

            InnovatiefWonenProject iwp = new InnovatiefWonenProject(projectId, "Kangoeroe woningen in Mariakerke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], innovatiefWonen);
            projectId++;
            StadsontwikkelingsGroeneRuimteProject sogrp = new StadsontwikkelingsGroeneRuimteProject(projectId, "ontwikkelings woningen in het groen", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "Klein marakesh", fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[0], stadsontwikkeling, groeneRuimte);
            projectId++;
            StadsontwikkelingsInnovatiefWonenProject soiwp = new StadsontwikkelingsInnovatiefWonenProject(projectId, "Kangoeroe woningen in apartementen", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], stadsontwikkeling, innovatiefWonen);
            projectId++;
            StadsontwikkelingsGroeneRuimteInnovatiefWonenProject sopgriwp = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(projectId, "fully robotic co-woningen in het park", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[0], stadsontwikkeling, groeneRuimte, innovatiefWonen);
            projectId++;
            StadsontwikkelingProject sop = new StadsontwikkelingProject(projectId, "Kangoeroe woningen in Mariakerke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], stadsontwikkeling);
            projectId++;

            projectLijst.Add((int)griwp.Id, griwp);
            projectLijst.Add((int)grp.Id,grp);
            projectLijst.Add((int)iwp.Id,iwp);
            projectLijst.Add((int)sogrp.Id, sogrp);
            projectLijst.Add((int)soiwp.Id, soiwp);
            projectLijst.Add((int)sopgriwp.Id, sopgriwp);
            projectLijst.Add((int)sop.Id, sop);


            //TODO constructors checken

            //Adres aanmaken
            Adres a1 = new Adres("Rijksweg", "127", 9000, "Gent");
            Adres a2 = new Adres("Floraliënlaan", "88", 9000, "Gent");
            Adres a3 = new Adres("Kraanlei", "267B", 9000, "Gent");
            Adres a4 = new Adres("R4", "/", 9000, "Gent");
            Adres a5 = new Adres("Kastanjestraat", "67", 9000, "Gent");

            #endregion
        }

        public List<Project> GeefAlleProjecten()
        {
            return projectLijst.Values.ToList();
        }

        //TODO methoden uitwerken --kelly--
        public void MaakGroeneRuimteInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar)
        {
            GroeneRuimteInnovatiefWonenProject project = new GroeneRuimteInnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum,
                projectStatus, wijk, fotos, documenten, partners, eigenaar,
                MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten),
                MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));
            
            projectLijst.Add(projectId, project);
            projectId++;

        }

        public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, 
            // parameters aanmaak groeneRuimte
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, Gebruiker gebruiker)
        {

            GroeneRuimteProject project = new GroeneRuimteProject(projectId, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, gebruiker,
                MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));

            projectLijst.Add(projectId ,project);
            projectId++;
        }

        public void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            //parameters innovatief wonen
            int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker gebruiker)
        {
            InnovatiefWonenProject project =  new InnovatiefWonenProject(projectId, projectTitel,  beschrijving,  startDatum, projectStatus,
             wijk, fotos,  documenten, partners, gebruiker , MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
            samenwerkingToerisme, woonvormen));

            projectLijst.Add(projectId, project);
            projectId++;
        }

        public void MaakStadsOntwikkelingGroeneRuimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten)
        {
            StadsontwikkelingsGroeneRuimteProject project = new StadsontwikkelingsGroeneRuimteProject(projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar,
                MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
                MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));
            projectLijst.Add(projectId, project);
            projectId++;
        }

        public void MaakStadsOntwikkelingInnovatiefWonenProjectAan()
        {
            throw new NotImplementedException();
        }

        public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject()
        {
            throw new NotImplementedException();
        }

        public void MaakStadsontwikkelingsProjectAan(string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, Gebruiker gebruiker)
        {
            StadsontwikkelingProject project = new StadsontwikkelingProject(projectId, projectTitel, beschrijving, startDatum, projectStatus,
             wijk, fotos, documenten, partners, gebruiker, MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen,
             infoBordenOfWandeling, bouwfirmas));
            projectLijst.Add(projectId, project);
            projectId++;

        }

        //methode aanmaak types

        private GroeneRuimte MaakGroeneRuimteAan(double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten)
        {
            return new GroeneRuimte(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten);
        }

        private InnovatiefWonen MaakInnovatiefWonenAan(int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen)
        {
            return new InnovatiefWonen(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
            samenwerkingToerisme, woonvormen);
        }

        private StadsOntwikkeling MaakStadsOntwikkelingAan(VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas)
        {
            return new StadsOntwikkeling(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen,
             infoBordenOfWandeling, bouwfirmas);
        }
    
    }
 
}






