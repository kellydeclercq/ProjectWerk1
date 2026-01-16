using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.projectType;
using ProjectBeheerBL.typeSoorten;

namespace ProjectBeheerBL.Beheerder
{
    public class ExportManager
    {
        public void ExporteerProjectenNaarCsv(List<Project> projecten, string pad)
        {
            if (projecten == null || projecten.Count == 0)
                throw new ProjectException("Geen projecten om te exporteren.");

            if (string.IsNullOrWhiteSpace(pad))
                throw new ProjectException("Ongeldig pad voor export.");

            //plaats om alle rijen te verzamelen voor csv
            var records = new List<object>();

            foreach (var p in projecten)
            {
                //per project checken welke interface dit project implementeert. 
                // zo bepalen welk type het is en of welke combo
                bool heeftGR = p is IGroeneRuimte;
                bool heeftSO = p is IStadsontwikkeling; 
                bool heeftIW = p is IInnovatiefWonen;

                //als project de interface implementeer haal extra data op anders null
                GroeneRuimte? groeneRuimte = heeftGR ?
                    ((IGroeneRuimte)p).GroeneRuimte : null;
                StadsOntwikkeling? stadsOntwikkeling = heeftSO ?
                    ((IStadsontwikkeling)p).StadsOntwikkeling : null;
                InnovatiefWonen? innovatiefWonen = heeftIW ?
                    ((IInnovatiefWonen)p).InnovatiefWonen : null;

                var record = new {
                    //anoniem object, dat 1 rij voorstel in csv met alle gegeven

                    //algemene gegevens voor elk project
                    ProjectId = p.Id,
                    Titel = p.ProjectTitel,
                    Beschrijving = p.Beschrijving, 
                    StartDatum = p.StartDatum,
                    ProjectStatus = p.ProjectStatus.ToString(),
                    Adres = p.Adres.ToString(),
                    Wijk = p.Wijk,
                    Fotos = p.Fotos.Count,
                    Documenten = p.Documenten.Count,
                    //partnes samen voegen in 1 veld
                    Partners = p.Partners != null && p.Partners.Any()
                    ? string.Join(" | ", p.Partners.Select(partner => partner.Naam))
                    : string.Empty,
                    ProjectEigenaar = p.ProjectEigenaar.Naam,

                    //checks om in csv te tonen welk type project het is
                    IsStadOntwikkeling = heeftSO ? "ja" : "neen",
                    IsGroeneRuimte = heeftGR ? "ja" : "neen",
                    IsInnovatiefWonen = heeftIW ? "ja" : "neen",

                    //SOP specificaties
                    VergunningsStatus = heeftSO
                    ? stadsOntwikkeling!.VergunningsStatus.ToString() : string.Empty,

                    ArchitecturaleWaarde = heeftSO
                    ? (stadsOntwikkeling!.ArchitecturaleWaarde ? "ja" : "neen") : string.Empty,

                    Toegankelijkheid = heeftSO
                    ? stadsOntwikkeling!.Toegankelijkheid.ToString() : string.Empty,

                    BeziensWaardigheidVoorToeristen = heeftSO
                    ? (stadsOntwikkeling!.BeziensWaardigheidVoorToeristen ? "ja" : "neen") : string.Empty,

                    InfobordenOfWandeling = heeftSO
                    ? (stadsOntwikkeling!.InfobordenOfWandeling ? "ja" : "neen") : string.Empty,

                    Bouwfirmas = heeftSO && stadsOntwikkeling!.BouwFirmas != null && stadsOntwikkeling.BouwFirmas.Any()
                    ? string.Join(" | ", stadsOntwikkeling.BouwFirmas.Select(bf => bf.Naam)) : string.Empty,

                    //GRP specificaties
                    OppervlakteInVierkanteM = heeftGR
                    ? groeneRuimte!.OppervlakteInVierkanteMeter: (double?)null,

                    BiodiversiteitScore = heeftGR
                    ? groeneRuimte!.BioDiversiteitsScore: (int?)null,

                    AantalWandelpaden = heeftGR
                    ? groeneRuimte!.AantalWandelpaden: (int?)null,

                    OpgenomenInWandelRoute = heeftGR
                    ? (groeneRuimte!.OpgenomenInWandelRoute ? "ja" : "neen") : string.Empty,

                    BezoekersScore = heeftGR
                    ? groeneRuimte!.BezoekersScore : (int?)null,

                    Faciliteiten = heeftGR && groeneRuimte!.Faciliteiten != null && groeneRuimte.Faciliteiten.Any()
                    ? string.Join(" | ", groeneRuimte.Faciliteiten) : string.Empty,

                    //IWP specificaties
                    AantalWooneenheden = heeftIW
                    ? innovatiefWonen!.AantalWooneenheden : (int?)null,

                    Rondleidingmogelijk = heeftIW
                    ? (innovatiefWonen!.RondleidingMogelijk ? "ja" : "neen") : string.Empty,

                    InnovatieScore = heeftIW
                    ? innovatiefWonen!.InnovatieScore : (int?)null,

                    ShowwoningBeschikbaar = heeftIW
                    ? (innovatiefWonen!.ShowwoningBeschikbaar ? "ja" : "neen") : string.Empty,

                    SamenwerkingErfgoed = heeftIW
                    ? (innovatiefWonen!.SamenwerkingErfgoed ? "ja" : "neen") : string.Empty,

                    SamenwerkingToerisme = heeftIW
                    ? (innovatiefWonen!.SamenwerkingToerisme ? "ja" : "neen") : string.Empty,

                    Woonvormen = heeftIW && innovatiefWonen!.Woonvormen != null && innovatiefWonen.Woonvormen.Any()
                    ? string.Join(" | ", innovatiefWonen.Woonvormen) : string.Empty

                };


                records.Add(record);

            }
            //csv config met puntkomma als scheiding , alles quoten ""
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture) {
                Delimiter = ";",
                ShouldQuote = args => true
            };

            //basis bestandsnaam
            string basisNaam = "MyProjectExport";
            string extension = ".csv";
            string volledigPad = Path.Combine(pad, basisNaam + extension);

            //als het bestand al bestaat voeg een nummertje toe op het einde van de naam
            int counter = 1;
            while(File.Exists(volledigPad))
            {
                volledigPad = Path.Combine(pad, basisNaam + counter.ToString() + extension);
                counter++;
            }

            //hier csv bestand effectie wegschrijven
            using var writer = new StreamWriter(volledigPad);
            using var csv = new CsvWriter(writer, config);

            // Formatteren van doubles (geen onnodige decimalen)
            csv.Context.TypeConverterOptionsCache.GetOptions<double>().Formats = new[] { "0.##" };
            csv.Context.TypeConverterOptionsCache.GetOptions<double?>().Formats = new[] { "0.##" };

            //alle records in 1 x naar csv schrijven
            csv.WriteRecords(records);


        }
    }
}
