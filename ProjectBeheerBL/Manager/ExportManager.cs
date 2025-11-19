using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.Exceptions;
using ProjectBeheerBL.Domein.projectType;
using ProjectBeheerBL.Enumeraties;
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

            var records = new List<object>();

            foreach (var p in projecten)
            {
                bool heeftGR = p is IGroeneRuimte;
                bool heeftSO = p is IStadsontwikkeling;
                bool heeftIW = p is IInnovatiefWonen;

                GroeneRuimte? groeneRuimte = heeftGR ?
                    ((IGroeneRuimte)p).GroeneRuimte : null;
                StadsOntwikkeling? stadsOntwikkeling = heeftSO ?
                    ((IStadsontwikkeling)p).StadsOntwikkeling : null;
                InnovatiefWonen? innovatiefWonen = heeftIW ?
                    ((IInnovatiefWonen)p).InnovatiefWonen : null;

                var record = new {

                    ProjectId = p.Id,
                    Titel = p.ProjectTitel,
                    Beschrijving = p.Beschrijving, //TODO wat hiermee doen??
                    StartDatum = p.StartDatum,
                    ProjectStatus = p.ProjectStatus.ToString(),
                    Wijk = p.Wijk,
                    Fotos = p.Fotos.Count,
                    Documenten = p.Documenten.Count,
                    Partners = p.Partners != null && p.Partners.Any()
                    ? string.Join(" | ", p.Partners.Select(partner => partner.Naam))
                    : string.Empty,
                    ProjectEigenaar = p.ProjectEigenaar.Naam,

                    IsStadOntwikkeling = heeftSO ? "ja" : "neen",
                    IsGroeneRuimte = heeftGR ? "ja" : "neen",
                    IsInnovatiefWonen = heeftIW ? "ja" : "neen",

                    VergunningsStatus = stadsOntwikkeling?.VergunningsStatus.ToString(),
                    ArchitecturaleWaarde =
                        stadsOntwikkeling?.ArchitecturaleWaarde == true ? "ja" : "neen",
                    Toegankelijkheid = stadsOntwikkeling?.Toegankelijkheid.ToString(),
                    BeziensWaardigheidVoorToeristen =
                        stadsOntwikkeling?.BeziensWaardigheidVoorToeristen == true ? "ja" : "neen",
                    InfobordenOfWandeling =
                        stadsOntwikkeling?.InfobordenOfWandeling == true ? "ja" : "neen",
                    Bouwfirmas = stadsOntwikkeling?.BouwFirmas != null && stadsOntwikkeling.BouwFirmas.Any()
                    ? string.Join(" | ", stadsOntwikkeling.BouwFirmas.Select(bf => bf.Naam))
                    : string.Empty,

                    OppervlakteInVierkanteM = groeneRuimte?.OppervlakteInVierkanteMeter,
                    BiodiversiteitScore = groeneRuimte?.BioDiversiteitsScore,
                    AantalWandelpaden = groeneRuimte?.AantalWandelpaden,
                    OpgenomenInWandelRoute = groeneRuimte?.OpgenomenInWandelRoute == true ? "ja" : "neen",
                    BezoekersScore = groeneRuimte?.BezoekersScore,
                    Faciliteiten = groeneRuimte?.Faciliteiten != null && groeneRuimte.Faciliteiten.Any()
                    ? string.Join(" | ", groeneRuimte.Faciliteiten) : string.Empty,

                    AantalWooneenheden = innovatiefWonen?.AantalWooneenheden,
                    Rondleidingmogelijk = innovatiefWonen?.RondleidingMogelijk == true ? "ja" : "neen",
                    InnovatieScore = innovatiefWonen?.InnovatieScore,
                    ShowwoningBeschikbaar = innovatiefWonen?.ShowwoningBeschikbaar == true ? "ja" : "neen",
                    SamenwerkingErfgoed = innovatiefWonen?.SamenwerkingErfgoed == true ? "ja" : "neen",
                    SamenwerkingToerisme = innovatiefWonen?.SamenwerkingToerisme == true ? "ja" : "neen",
                    Woonvormen = innovatiefWonen?.Woonvormen != null && innovatiefWonen.Woonvormen.Any()
                    ? string.Join(" | ", innovatiefWonen.Woonvormen) : string.Empty,

                };

                records.Add(record);

            }

            using var writer = new StreamWriter(pad);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(records);

        }
    }
}
