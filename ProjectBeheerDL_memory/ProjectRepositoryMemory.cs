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

        public ProjectRepositoryMemory(IGebruikerRepository? gebruikersRepo)
        {
            #region hard coded data
            lijstService.Faciliteiten.Add("speeltuin");
            lijstService.Faciliteiten.Add("picknickzone");
            lijstService.Faciliteiten.Add("infoborden");

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
            bouwFirmas.Add(b);
            bouwFirmas.Add(b2);

            //Adres aanmaken
            Adres a1 = new Adres("Rijksweg", "127", 9000, "Gent");
            Adres a2 = new Adres("Floraliënlaan", "88", 9000, "Gent");
            Adres a3 = new Adres("Kraanlei", "267B", 9000, "Gent");
            Adres a4 = new Adres("R4", "20", 9000, "Gent");
            Adres a5 = new Adres("Kastanjestraat", "67", 9000, "Gent");


            // Drie typen project aanmaken (twee opties voor diversiteit)
            StadsOntwikkeling stadsontwikkeling  =  new StadsOntwikkeling(VergunningsStatus.Goegekeurd, false, Toegankelijkheid.Gedeeltelijk, false, true, bouwFirmas);
            GroeneRuimte groeneRuimte = new GroeneRuimte(400.50, 4, 2, true,  null, faciliteiten);           
            InnovatiefWonen innovatiefWonen = new InnovatiefWonen( 4, true, 9, true, false, false, woonvormen);

            
            
            //TODO gebruiker toevoegen

            // 7 projecten aanmaken (elke soort)

            GroeneRuimteInnovatiefWonenProject griwp = new GroeneRuimteInnovatiefWonenProject(projectId, "Modern Wonen aan de Bijloke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "Bijloke", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], a1, groeneRuimte, innovatiefWonen);
            projectId++;
            GroeneRuimteProject grp = new GroeneRuimteProject(projectId, "Groene bibilitoheek in oostakker", langeBeschrijving,
                new DateTime(2024, 07 - 4, 24), ProjectStatus.Planning, "Overpoort", fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[0], a2, groeneRuimte);
            projectId++;
            InnovatiefWonenProject iwp = new InnovatiefWonenProject(projectId, "Kangoeroe woningen in Mariakerke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], a3, innovatiefWonen);
            projectId++;
            StadsontwikkelingsGroeneRuimteProject sogrp = new StadsontwikkelingsGroeneRuimteProject(projectId, "ontwikkelings woningen in het groen", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "Klein marakesh", fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[0], a4, stadsontwikkeling, groeneRuimte);
            projectId++;
            StadsontwikkelingsInnovatiefWonenProject soiwp = new StadsontwikkelingsInnovatiefWonenProject(projectId, "Kangoeroe woningen in apartementen", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], a5, stadsontwikkeling, innovatiefWonen);
            projectId++;
            StadsontwikkelingsGroeneRuimteInnovatiefWonenProject sopgriwp = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(projectId, "fully robotic co-woningen in het park", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[0], a1, stadsontwikkeling, groeneRuimte, innovatiefWonen);
            projectId++;
            StadsontwikkelingProject sop = new StadsontwikkelingProject(projectId, "Kangoeroe woningen in Mariakerke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[0], a2, stadsontwikkeling);
            projectId++;

            // 1: Enkel Groene Ruimte, andere wijk en status
            var gr2 = new GroeneRuimte(250.0, 8, 3, true, 5, new List<string> { "speeltuin", "hondenweide" });
            var grp2 = new GroeneRuimteProject(
                projectId, "Park De Sterre heraanleg", langeBeschrijving,
                new DateTime(2023, 10, 10), ProjectStatus.Uitvoering, "De Sterre",
                fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[1], a3, gr2);
            projectLijst.Add(projectId, grp2);
            projectId++;

            // 2: Enkel Innovatief Wonen, andere gebruiker en status
            var iw2 = new InnovatiefWonen(16, true, 7, false, true, false, new List<string> { "co-housing", "studentenstudio's" });
            var iwp2 = new InnovatiefWonenProject(
                projectId, "Studentenhuisvesting Rooigem", langeBeschrijving,
                new DateTime(2024, 02, 15), ProjectStatus.Uitvoering, "Rooigem",
                fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[2], a4, iw2);
            projectLijst.Add(projectId, iwp2);
            projectId++;

            // 3: Enkel Stadsontwikkeling, Afgerond
            var so2 = new StadsOntwikkeling(
                VergunningsStatus.Goegekeurd, true, Toegankelijkheid.VolledigOpenbaar, true, true, bouwFirmas);
            var sop2 = new StadsontwikkelingProject(
                projectId, "Herontwikkeling Oude Dokken", langeBeschrijving,
                new DateTime(2020, 05, 12), ProjectStatus.Afgerond, "Oude Dokken",
                fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[3],a5, so2);
            projectLijst.Add(projectId, sop2);
            projectId++;

            // 4: GroeneRuimte + InnovatiefWonen, Afgerond
            var gr3 = new GroeneRuimte(180, 6, 1, false, 4, new List<string> { "picknickzone" });
            var iw3 = new InnovatiefWonen(8, false, null, false, false, false, new List<string> { "tiny houses" });
            var griwp2 = new GroeneRuimteInnovatiefWonenProject(
                projectId, "Tiny house cluster in natuurzone", langeBeschrijving,
                new DateTime(2022, 09, 01), ProjectStatus.Afgerond, "Bourgoyen",
                fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[4], a1, gr3, iw3);
            projectLijst.Add(projectId, griwp2);
            projectId++;

            // 5: Stadsontwikkeling + GroeneRuimte, Uitvoering
            var gr4 = new GroeneRuimte(520, 9, 4, true, 5, new List<string> { "speeltuin", "sportveld" });
            var so3 = new StadsOntwikkeling(
                VergunningsStatus.Goegekeurd, true, Toegankelijkheid.Gedeeltelijk, true, false, bouwFirmas);
            var sogrp2 = new StadsontwikkelingsGroeneRuimteProject(
                projectId, "Groene boulevard aan de Schelde", langeBeschrijving,
                new DateTime(2024, 11, 01), ProjectStatus.Uitvoering, "Scheldekaai",
                fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[5],a2, so3, gr4);
            projectLijst.Add(projectId, sogrp2);
            projectId++;

            // 6: Stadsontwikkeling + InnovatiefWonen, Geannuleerd
            var iw4 = new InnovatiefWonen(20, true, 5, false, true, true, new List<string> { "seniorenflats" });
            var so4 = new StadsOntwikkeling(
                VergunningsStatus.InAanvraag, false, Toegankelijkheid.Gesloten, false, false, bouwFirmas);
            var soiwp2 = new StadsontwikkelingsInnovatiefWonenProject(
                projectId, "Seniorencomplex aan Watersportbaan", langeBeschrijving,
                new DateTime(2021, 03, 20), ProjectStatus.Geannuleerd, "Watersportbaan",
                fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[6],a3, so4, iw4);
            projectLijst.Add(projectId, soiwp2);
            projectId++;

            // 7: Alle drie types, Uitvoering
            var gr5 = new GroeneRuimte(650, 10, 5, true, 5, new List<string> { "speeltuin", "waterpartij", "moestuin" });
            var iw5 = new InnovatiefWonen(30, true, 9, true, true, true, new List<string> { "co-housing", "zorgwonen" });
            var so5 = new StadsOntwikkeling(
                VergunningsStatus.Goegekeurd, true, Toegankelijkheid.VolledigOpenbaar, true, true, bouwFirmas);
            var sopgriwp2 = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(
                projectId, "Groen woonpark Ledeberg", langeBeschrijving,
                new DateTime(2024, 06, 10), ProjectStatus.Uitvoering, "Ledeberg",
                fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[7],a4, so5, gr5, iw5);
            projectLijst.Add(projectId, sopgriwp2);
            projectId++;

            // 8: Enkel Groene Ruimte, klein en eenvoudig
            var gr6 = new GroeneRuimte(90, 3, 1, false, 3, new List<string> { "bankjes" });
            var grp3 = new GroeneRuimteProject(
                projectId, "Buurtparkje Brugse Poort", langeBeschrijving,
                new DateTime(2023, 04, 05), ProjectStatus.Planning, "Brugse Poort",
                fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[8],a5, gr6);
            projectLijst.Add(projectId, grp3);
            projectId++;

            // 9: Enkel Innovatief Wonen, groot
            var iw6 = new InnovatiefWonen(60, true, 8, true, false, true, new List<string> { "huurunits", "co-working" });
            var iwp3 = new InnovatiefWonenProject(
                projectId, "Stadscampus voor starters", langeBeschrijving,
                new DateTime(2025, 01, 15), ProjectStatus.Planning, "Dampoort",
                fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[9],a1, iw6);
            projectLijst.Add(projectId, iwp3);
            projectId++;

            // 10: Enkel Stadsontwikkeling, in uitvoering
            var so6 = new StadsOntwikkeling(
                VergunningsStatus.InAanvraag, true, Toegankelijkheid.Gedeeltelijk, true, true, bouwFirmas);
            var sop3 = new StadsontwikkelingProject(
                projectId, "Herinrichting Sint-Pietersplein", langeBeschrijving,
                new DateTime(2023, 09, 01), ProjectStatus.Uitvoering, "Sint-Pietersplein",
                fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[10],a2, so6);
            projectLijst.Add(projectId, sop3);
            projectId++;

            // 11: Groene Ruimte + Innovatief Wonen, Planning
            var gr7 = new GroeneRuimte(320, 7, 2, true, 4, new List<string> { "speeltuin", "natuurzone" });
            var iw7 = new InnovatiefWonen(18, false, 6, false, false, false, new List<string> { "modulaire woningen" });
            var griwp3 = new GroeneRuimteInnovatiefWonenProject(
                projectId, "Natuurvriendelijke woonwijk Drongen", langeBeschrijving,
                new DateTime(2025, 02, 01), ProjectStatus.Planning, "Drongen",
                fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[2], a3, gr7, iw7);
            projectLijst.Add(projectId, griwp3);
            projectId++;

            // 12: Stadsontwikkeling + Groene Ruimte, Afgerond
            var gr8 = new GroeneRuimte(500, 9, 3, true, 5, new List<string> { "speeltuin", "skatepark" });
            var so7 = new StadsOntwikkeling(
                VergunningsStatus.Goegekeurd, true, Toegankelijkheid.VolledigOpenbaar, true, true, bouwFirmas);
            var sogrp3 = new StadsontwikkelingsGroeneRuimteProject(
                projectId, "Jeugd- en sportpark Nieuw Gent", langeBeschrijving,
                new DateTime(2021, 06, 01), ProjectStatus.Afgerond, "Nieuw Gent",
                fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[3],a4, so7, gr8);
            projectLijst.Add(projectId, sogrp3);
            projectId++;

            // 13: Stadsontwikkeling + Innovatief Wonen, Uitvoering
            var iw8 = new InnovatiefWonen(40, true, 7, true, false, true, new List<string> { "lofts", "co-living" });
            var so8 = new StadsOntwikkeling(
                VergunningsStatus.InAanvraag, true, Toegankelijkheid.Gedeeltelijk, true, false, bouwFirmas);
            var soiwp3 = new StadsontwikkelingsInnovatiefWonenProject(
                projectId, "Loftproject Oude Fabriek", langeBeschrijving,
                new DateTime(2024, 08, 20), ProjectStatus.Uitvoering, "Muide",
                fotos, documenten, partners, gebruikersRepo.GeefAlleGebruikers()[4], a5, so8, iw8);
            projectLijst.Add(projectId, soiwp3);
            projectId++;

            // 14: Alle drie types, Planning
            var gr9 = new GroeneRuimte(700, 10, 6, true, 5, new List<string> { "speeltuin", "stadsmoestuin", "hondenweide" });
            var iw9 = new InnovatiefWonen(50, true, 10, true, true, true, new List<string> { "co-housing", "zorgwonen", "ateliers" });
            var so9 = new StadsOntwikkeling(
                VergunningsStatus.InAanvraag, true, Toegankelijkheid.Gedeeltelijk, true, true, bouwFirmas);
            var sopgriwp3 = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(
                projectId, "Stadslabo Rabot", langeBeschrijving,
                new DateTime(2025, 11, 01), ProjectStatus.Planning, "Rabot",
                fotos, documenten, partners2, gebruikersRepo.GeefAlleGebruikers()[5],a1, so9, gr9, iw9);
            projectLijst.Add(projectId, sopgriwp3);
            projectId++;

            projectLijst.Add((int)griwp.Id, griwp);
            projectLijst.Add((int)grp.Id,grp);
            projectLijst.Add((int)iwp.Id,iwp);
            projectLijst.Add((int)sogrp.Id, sogrp);
            projectLijst.Add((int)soiwp.Id, soiwp);
            projectLijst.Add((int)sopgriwp.Id, sopgriwp);
            projectLijst.Add((int)sop.Id, sop);


            //TODO constructors checken



            #endregion
        }

      

        //TODO methoden uitwerken --kelly--
        public void MaakGroeneRuimteInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres)
        {
            GroeneRuimteInnovatiefWonenProject project = new GroeneRuimteInnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum,
                projectStatus, wijk, fotos, documenten, partners, eigenaar, adres,
                MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten),
                MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));
            
            projectLijst.Add(projectId, project);
            projectId++;

        }



        public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,  
            // parameters aanmaak groeneRuimte
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, Gebruiker gebruiker, Adres adres)
        {

            GroeneRuimteProject project = new GroeneRuimteProject(projectId, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, gebruiker, adres,
                MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));

            projectLijst.Add(projectId ,project);
            projectId++;
        }



        public void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            //parameters innovatief wonen
            int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker gebruiker, Adres adres)
        {
            InnovatiefWonenProject project =  new InnovatiefWonenProject(projectId, projectTitel,  beschrijving,  startDatum, projectStatus,
             wijk, fotos,  documenten, partners, gebruiker, adres, MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
            samenwerkingToerisme, woonvormen));

            projectLijst.Add(projectId, project);
            projectId++;
        }



        public void MaakStadsOntwikkelingGroeneRuimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres,VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten)
        {
            StadsontwikkelingsGroeneRuimteProject project = new StadsontwikkelingsGroeneRuimteProject(projectId, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar,adres,
                MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
                MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));
            projectLijst.Add(projectId, project);
            projectId++;
        }



        public void MaakStadsOntwikkelingInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres)           
        {
            StadsontwikkelingsInnovatiefWonenProject project = new StadsontwikkelingsInnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum,
                projectStatus, wijk, fotos, documenten, partners, eigenaar, adres,
                MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
                MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

            projectLijst.Add(projectId, project);
            projectId++;
        }



        public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker Eigenaar, Adres adres
            )
        {
            StadsontwikkelingsGroeneRuimteInnovatiefWonenProject project = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(projectId, projectTitel, beschrijving,
                startDatum, projectStatus, wijk, fotos, documenten, partners, Eigenaar, adres,
                MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
                MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten),
                MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

            projectLijst.Add(projectId, project);
            projectId++;




        }



        public void MaakStadsontwikkelingsProjectAan(string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, Gebruiker gebruiker, Adres adres)
        {
            StadsontwikkelingProject project = new StadsontwikkelingProject(projectId, projectTitel, beschrijving, startDatum, projectStatus,
             wijk, fotos, documenten, partners, gebruiker, adres, MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen,
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


        // FILTEREN en SORTEREN en OPVRAGEN
        public List<Project> GeefAlleProjecten()
        {
            return projectLijst.Values.ToList();
        }

        public List<Project> GeefProjectenGefilterdOpPartners(string partner)
        {
            var lijst = projectLijst
                .Select(x => x.Value)
                .Where(p => p.Partners
                .Any(p => p.Naam.Equals(partner, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return lijst;
        }

        public List<Project> GeefProjectenGefilterdOpStatus(string status)
        {
            var lijst = projectLijst.Select(x => x.Value)
                .Where(x => x.ProjectStatus.ToString().Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return lijst;
        }

        public List<Project> GeefProjectenGefilterdOpTitel(string titel)
        {
            var lijst = projectLijst.Select(x => x.Value)
                .Where(x => x.ProjectTitel.Equals(titel, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return lijst;
        }

        public List<Project> GeefProjectenGefilterdOpType(List<bool> types)
        {
            //groen: 0
            //innov: 1
            //stadsont: 2

            //TODO nog uitwerken met booleans
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpWijk(string wijk)
        {
            throw new NotImplementedException();
        }

    }
 
}






