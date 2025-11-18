using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces;

namespace ProjectBeheerDL_Memory
{
    public class ProjectRepositoryMemory : IProjectRepository
    {
        private LijstService lijstService { get; set; }
        private Dictionary<int, Project> projecten = new();
        private List<byte[]> fotos = new List<byte[]>();
        private List<byte[]> documenten = new List<byte[]>();
       
        private List<string> woonvormen = new() {lijstService.}

        private int projectId = 1;

        public ProjectRepositoryMemory()
        {
            //foto's en documenten optioneel maken

            fotos = new List<byte[]>();
            documenten = new List<byte[]>();

            projecten.Add(projectId, new GroeneRuimte(1, "Kloosterhof", "beschrijving1", new DateTime(2025, 12, 18),
                new ProjectStatus(), "Kolegem", fotos[0], documenten[0], 40.0, 2, 3, true, 10, new List<string> { "Speelplein" }; projectId++;

            projecten.Add(projectId, new InnovatiefWonen(1, "Residentie Ter Leie", "beschrijving2", new DateTime(2026, 05, 02),
                new ProjectStatus(), "Malem" fotos[0], documenten[0], 3, true, 4, true, false, true, woonvormen ); projectId++;


            projecten.Add(projectId, new StadsOntwikkeling(1, "Zonnepanelen VIERNULVIER", "beschrijving3", new DateTime(2024, 10, 12),
                new ProjectStatus(), "zuid" fotos[0], documenten[0], VergunningsStatus.Goegekeurd, false, Toegankelijkheid.Gesloten, false, false); projectId++;


        private Dictionary<int, Partner> partners = new();
        private int partnerId = 1;



            partners.Add(partnerId, new Partner(1, "AOC group", "jos@aoc.com", "0471 22 44 66", "www.aoc.be", "Ruwbouw")); partnerId++;
            partners.Add(partnerId, new Partner(1, "StromendWater group", "ann.@stromendwater.com", "0499 23 11 02", "www.stromendwater.be", "Sanitair")); partnerId++;
            partners.Add(partnerId, new Partner(1, "Volts group", "John@volts.com", "0486 25 36 66", "www.voltsElectric.be", "Electra")); partnerId++;
        


        private Dictionary<int, BouwFirma> bouwfirmas = new();
        private int bouwfirmaId = 1;



        bouwfirmas.Add(bouwfirmaId, new BouwFirma(1, "")); bouwfirmaId++;
        


        private Dictionary<int, Adres> adressen = new();
        private int adresId = 1;



            adressen.Add(adresId, new Adres("Rijksweg", "127", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("Floraliënlaan", "88", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("Kraanlei", "267B", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("R4", "/", 9000, "Gent")); adresId++;
            adressen.Add(adresId, new Adres("Kastanjestraat", "67", 9000, "Gent")); adresId++;

        }                  
    }
}


