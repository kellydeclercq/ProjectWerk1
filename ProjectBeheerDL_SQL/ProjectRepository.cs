using Microsoft.Data.SqlClient;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.ProjectTypesSubklasses;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.Repo;
using ProjectBeheerBL.typeSoorten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeheerDL_SQL
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Project> GeefAlleProjecten()
        {
            #region sql code voor project
            const string sql = @"SELECT
                                id,
                                project_titel,
                                beschrijving,
                                startdatum,
                                project_status,
                                straat,
                                huisnummer,
                                postcode,
                                gemeente,
                                wijk,
                                project_eigenaar_id,
                                is_stadsontwikkeling,
                                is_groene_ruimte,
                                is_innovatief_wonen,
                                vergunningsstatus,
                                architecturale_waarde,
                                toegankelijkheid,
                                bezienswaardigheid_voor_toeristen,
                                infoborden_of_wandeling,
                                oppervlakte_in_vierkante_meter,
                                biodiversiteitsscore,
                                aantal_wandelpaden,
                                opgenomen_in_wandelroute,
                                bezoekersscore,
                                faciliteiten,
                                aantal_wooneenheden,
                                rondleiding_mogelijk,
                                innovatie_score,
                                showwoning_beschikbaar,
                                samenwerking_erfgoed,
                                samenwerking_toerisme,
                                woonvormen
                                FROM project;";
            #endregion

            var projecten = new List<Project>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["id"];
                        string projectTitel = (string)reader["project_titel"];
                        string beschrijving = (string)reader["beschrijving"];
                        DateTime? startdatum = reader["startdatum"] == DBNull.Value ?
                            (DateTime?)null : (DateTime)reader["startdatum"];
                        
                        ProjectStatus status = Enum.Parse<ProjectStatus>((string)reader["project_status"]);
                        string wijk = (string)reader["wijk"];

                        var adres = new Adres(
                         (string)reader["straat"],
                         (string)reader["huisnummer"],
                         (int)reader["postcode"],
                         (string)reader["gemeente"]);
                        
                        int eigenaarId = (int)reader["project_eigenaar_id"];

                        bool SOP = (bool)reader["is_stadsontwikkeling"];
                        bool GRP = (bool)reader["is_groene_ruimte"];
                        bool IWP = (bool)reader["is_innovatief_wonen"];

                        List<byte[]> fotos = new();
                        List<byte[]> docs = new();
                        List<Partner> partners = new();

                        GroeneRuimte? groeneRuimte = null;
                        StadsOntwikkeling? stadsOntwikkeling = null;
                        InnovatiefWonen? innovatiefWonen = null;
                    }

        public void MaakGroeneRuimteInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres) => throw new NotImplementedException();
        //{
        //    GroeneRuimteInnovatiefWonenProject project = new GroeneRuimteInnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum,
        //        projectStatus, wijk, fotos, documenten, partners, eigenaar, adres,
        //        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten),
        //        MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

        //    projectLijst.Add(projectId, project);
        //    projectId++;
        //}



        public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
            List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            // parameters aanmaak groeneRuimte
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, Gebruiker gebruiker, Adres adres) => throw new NotImplementedException();
        //{
        //    GroeneRuimteProject project = new GroeneRuimteProject(projectId, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, gebruiker, adres,
        //        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));

        //    projectLijst.Add(projectId, project);
        //    projectId++;
        //}



        public void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            //parameters innovatief wonen
            int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker gebruiker, Adres adres) => throw new NotImplementedException();
        //{
        //    InnovatiefWonenProject project = new InnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum, projectStatus,
        //     wijk, fotos, documenten, partners, gebruiker, adres, MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
        //    samenwerkingToerisme, woonvormen));

        //    projectLijst.Add(projectId, project);
        //    projectId++;
        //}



        public void MaakStadsOntwikkelingGroeneRuimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten) => throw new NotImplementedException();
        //{
        //    StadsontwikkelingsGroeneRuimteProject project = new StadsontwikkelingsGroeneRuimteProject(projectId, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar, adres,
        //        MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
        //        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));
        //    projectLijst.Add(projectId, project);
        //    projectId++;
        //}



        public void MaakStadsOntwikkelingInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres) => throw new NotImplementedException();
        //{
        //    StadsontwikkelingsInnovatiefWonenProject project = new StadsontwikkelingsInnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum,
        //        projectStatus, wijk, fotos, documenten, partners, eigenaar, adres,
        //        MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
        //        MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

        //    projectLijst.Add(projectId, project);
        //    projectId++;
        //}



        public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
            double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen, Gebruiker Eigenaar, Adres adres
            ) => throw new NotImplementedException();
        //{
        //    StadsontwikkelingsGroeneRuimteInnovatiefWonenProject project = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(projectId, projectTitel, beschrijving,
        //        startDatum, projectStatus, wijk, fotos, documenten, partners, Eigenaar, adres,
        //        MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
        //        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten),
        //        MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

        //    projectLijst.Add(projectId, project);
        //    projectId++;
        //}



        public void MaakStadsontwikkelingsProjectAan(string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
            string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, Gebruiker gebruiker, Adres adres) => throw new NotImplementedException();
        //{
        //    StadsontwikkelingProject project = new StadsontwikkelingProject(projectId, projectTitel, beschrijving, startDatum, projectStatus,
        //     wijk, fotos, documenten, partners, gebruiker, adres, MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen,
        //     infoBordenOfWandeling, bouwfirmas));
        //    projectLijst.Add(projectId, project);
        //    projectId++;

        //}



        //methode aanmaak types

        private GroeneRuimte MaakGroeneRuimteAan(double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
            int? bezoekersScore, List<string> faciliteiten) => throw new NotImplementedException();
        //{
        //    return new GroeneRuimte(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten);
        //}

        private InnovatiefWonen MaakInnovatiefWonenAan(int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
            bool samenwerkingToerisme, List<string> woonvormen) => throw new NotImplementedException();
        //{
        //    return new InnovatiefWonen(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
        //    samenwerkingToerisme, woonvormen);
        //}

        private StadsOntwikkeling MaakStadsOntwikkelingAan(VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
            bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas) => throw new NotImplementedException();
        //{
        //    return new StadsOntwikkeling(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen,
        //     infoBordenOfWandeling, bouwfirmas);
        //}


        // FILTEREN en SORTEREN en OPVRAGEN
        public List<Project> GeefAlleProjecten() => throw new NotImplementedException();
        //{
        //    return projectLijst.Values.ToList();
        //}

        public List<Project> GeefProjectenGefilterdOpPartners(string partner) => throw new NotImplementedException();
        //{
        //    var lijst = projectLijst
        //        .Select(x => x.Value)
        //        .Where(p => p.Partners
        //        .Any(p => p.Naam.Equals(partner, StringComparison.OrdinalIgnoreCase)))
        //        .ToList();

        //    return lijst;
        //}

        public List<Project> GeefProjectenGefilterdOpStatus(string status) => throw new NotImplementedException();
        //{
        //    var lijst = projectLijst.Select(x => x.Value)
        //        .Where(x => x.ProjectStatus.ToString().Equals(status, StringComparison.OrdinalIgnoreCase))
        //        .ToList();

        //    return lijst;
        //}

        public List<Project> GeefProjectenGefilterdOpTitel(string titel) => throw new NotImplementedException();
        //{
        //    var lijst = projectLijst.Select(x => x.Value)
        //        .Where(x => x.ProjectTitel.Equals(titel, StringComparison.OrdinalIgnoreCase))
        //        .ToList();

        //    return lijst;
        //}

        public List<Project> GeefProjectenGefilterdOpType(List<bool> types) => throw new NotImplementedException();
        //{
        //    //groen: 0
        //    //innov: 1
        //    //stadsont: 2

        //    //TODO nog uitwerken met booleans
  
        //}

        public List<Project> GeefProjectenGefilterdOpWijk(string wijk) => throw new NotImplementedException();
        //{

        //}

    }

}

