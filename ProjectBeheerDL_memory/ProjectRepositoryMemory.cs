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
      
        private int projectId = 1;
        string langeBeschrijving = "Dit bouwproject omvat de gefaseerde ontwikkeling van een multifunctioneel complex waarin duurzaamheid, efficiëntie en toekomstbestendigheid centraal staan. Tijdens de ontwerpfase worden verschillende constructieve opties onderzocht om zowel de esthetische als functionele doelstellingen te waarborgen. De uitvoering wordt gepland in nauw overleg met betrokken stakeholders, waarbij bijzondere aandacht wordt besteed aan logistieke routing en minimale verstoring van de omgeving.\r\n\r\nIn het hoofdgebouw wordt een modulaire structuur toegepast die flexibiliteit biedt voor toekomstige aanpassingen. De materialen worden geselecteerd op basis van energieprestatie, levensduur en circulaire toepassingsmogelijkheden. Daarnaast wordt een geavanceerd monitoringsysteem geïntegreerd om energieverbruik, veiligheidsparameters en klimaatbeheersing in realtime te optimaliseren.\r\n\r\nDe buitenruimte krijgt een groene invulling met onderhoudsarme beplanting, waterdoorlatende bestrating en strategische verlichting die zowel veiligheid als sfeer ondersteunt. Het bouwteam werkt volgens een strak kwaliteitsprotocol om consistentie te garanderen tijdens alle projectfasen. Eventuele afwijkingen worden tijdig gerapporteerd en beoordeeld, zodat de planning en begroting binnen de gestelde kaders blijven.";

        public ProjectRepositoryMemory()
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
            


            // 7 projecten aanmaken (elke soort)

            GroeneRuimteInnovatiefWonenProject griwp = new GroeneRuimteInnovatiefWonenProject(1, "Modern Wonen aan de Bijloke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "Bijloke", fotos, documenten, groeneRuimte, innovatiefWonen, partners);

            GroeneRuimteProject grp = new GroeneRuimteProject(2, "Groene bibilitoheek in oostakker", langeBeschrijving,
                new DateTime(2024, 07 - 4, 24), ProjectStatus.Planning, "Overpoort", fotos, documenten, groeneRuimte, partners2);

            InnovatiefWonenProject iwp = new InnovatiefWonenProject(3, "Kangoeroe woningen in Mariakerke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, innovatiefWonen, partners);

            StadsontwikkelingsGroeneRuimteProject sogrp = new StadsontwikkelingsGroeneRuimteProject(4, "ontwikkelings woningen in het groen", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "Klein marakesh", fotos, documenten, stadsontwikkeling, groeneRuimte, partners2);

            StadsontwikkelingsInnovatiefWonenProject soiwp = new StadsontwikkelingsInnovatiefWonenProject(5, "Kangoeroe woningen in apartementen", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, stadsontwikkeling, innovatiefWonen, partners);
            StadsontwikkelingsProjectGroeneRuimteInnovatiefWonenProject sopgriwp = new StadsontwikkelingsProjectGroeneRuimteInnovatiefWonenProject(6, "fully robotic co-woningen in het park", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, stadsontwikkeling, groeneRuimte, innovatiefWonen, partners2);

            StadsontwikkelingProject sop = new StadsontwikkelingProject(7, "Kangoeroe woningen in Mariakerke", langeBeschrijving,
                new DateTime(2025, 07, 26), ProjectStatus.Planning, "De Kreek", fotos, documenten, stadsontwikkeling, partners);

            //TODO constructors checken

            //Adres aanmaken
            Adres a1 = new Adres("Rijksweg", "127", 9000, "Gent");
            Adres a2 = new Adres("Floraliënlaan", "88", 9000, "Gent");
            Adres a3 = new Adres("Kraanlei", "267B", 9000, "Gent");
            Adres a4 = new Adres("R4", "/", 9000, "Gent");
            Adres a5 = new Adres("Kastanjestraat", "67", 9000, "Gent");

            #endregion
        }

        public void MaakGroeneruimteProjectAan()
        {
            throw new NotImplementedException();
        }
    }


          
    
}






