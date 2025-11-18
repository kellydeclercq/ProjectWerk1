using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.Exceptions;
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
                    ProjectStatus = p.ProjectStatus,
                    Wijk = p.Wijk,
                    Fotos = p.Fotos.Count,
                    Documenten = p.Documenten.Count,
                    Partners = p.Partners,
                    ProjectEignaar = p.ProjectEigenaar,

                    IsStadOntwikkeling = heeftSO ? "ja" : "neen",
                    IsGroeneRuimte = heeftGR ? "ja" : "neen",
                    IsInnovatiefWonen = heeftIW ? "ja" : "neen",

                    OppervlakteInVierkanteM = groeneRuimte.OppervlakteInVierkanteMeter,
                    BiodiversiteitScore = groeneRuimte.BioDiversiteitsScore,
                    AantalWandelpaden = groeneRuimte.AantalWandelpaden,
                    OpgenomenInWandelRoute = groeneRuimte.OpgenomenInWandelRoute? "ja" : "neen",
                    BezoekersScore = groeneRuimte.BezoekersScore,
                }



            }

        }
    }
}
