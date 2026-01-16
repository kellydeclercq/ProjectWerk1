using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using ProjectBeheerBL.Domein;
using ProjectBeheerBL.Domein.ProjectTypesSubklasses;
using ProjectBeheerBL.Enumeraties;
using ProjectBeheerBL.Interfaces.projectType;
using ProjectBeheerBL.Interfaces.Repo;
using ProjectBeheerBL.typeSoorten;

namespace ProjectBeheerDL_SQL
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string VoegLijstSamenInDb(List<string> items)
        {
            if (items == null || items.Count == 0) return "";
            return string.Join("|", items);
        }


        private void SchrijfProjectNaarDb(
              string projectTitel, string beschrijving, DateTime? startdatum, ProjectStatus projectStatus, string wijk,
              List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
              Gebruiker eigenaar, Adres adres,

              bool isStadsontwikkeling, bool isGroeneRuimte, bool isInnovatiefWonen,

              // GROENE RUIMTE
              double? oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool? opgenomenInWandelRoute,
              int? bezoekersScore, List<string>? faciliteiten,

              // INNOVATIEF WONEN
              int? aantalWooneenheden, bool? rondleidingMogelijk, int? innovatieScore, bool? showwoningBeschikbaar,
              bool? samenwerkingErfgoed, bool? samenwerkingToerisme, List<string>? woonvormen,

              // STADSONTWIKKELING
              VergunningsStatus? vergunningsStatus, bool? architecturaleWaarde, Toegankelijkheid? toegankelijkheid,
              bool? beziensWaardigheidVoortoeristen, bool? infoBordenOfWandeling)
        {
            const string sql = @"
                                INSERT INTO project (
                                    project_titel, beschrijving, startdatum, project_status,
                                    straat, huisnummer, postcode, gemeente, wijk, project_eigenaar_id,
                                    is_stadsontwikkeling, is_groene_ruimte, is_innovatief_wonen,

                                    vergunningsstatus, architecturale_waarde, toegankelijkheid,
                                    bezienswaardigheid_voor_toeristen, infoborden_of_wandeling,

                                    oppervlakte_in_vierkante_meter, biodiversiteitsscore, aantal_wandelpaden,
                                    opgenomen_in_wandelroute, bezoekersscore, faciliteiten,

                                    aantal_wooneenheden, rondleiding_mogelijk, innovatie_score,
                                    showwoning_beschikbaar, samenwerking_erfgoed, samenwerking_toerisme, woonvormen
                                )
                                VALUES (
                                    @project_titel, @beschrijving, @startdatum, @project_status,
                                    @straat, @huisnummer, @postcode, @gemeente, @wijk, @project_eigenaar_id,
                                    @is_stadsontwikkeling, @is_groene_ruimte, @is_innovatief_wonen,

                                    @vergunningsstatus, @architecturale_waarde, @toegankelijkheid,
                                    @bezienswaardigheid_voor_toeristen, @infoborden_of_wandeling,

                                    @oppervlakte_in_vierkante_meter, @biodiversiteitsscore, @aantal_wandelpaden,
                                    @opgenomen_in_wandelroute, @bezoekersscore, @faciliteiten,

                                    @aantal_wooneenheden, @rondleiding_mogelijk, @innovatie_score,
                                    @showwoning_beschikbaar, @samenwerking_erfgoed, @samenwerking_toerisme, @woonvormen
                                );
                                ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                // Transaction: alles of niets (als er iets fout gaat => rollback)
                SqlTransaction transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.CommandText = sql;

                try
                {
                    //hier alle sql parameters
                    //als iets neit bestaat voor projecttype => DBNull.Value

                    // Algemeen project
                    cmd.Parameters.AddWithValue("@project_titel", projectTitel);
                    cmd.Parameters.AddWithValue("@beschrijving", beschrijving);
                    cmd.Parameters.AddWithValue("@startdatum", startdatum ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@project_status", projectStatus.ToString());

                    cmd.Parameters.AddWithValue("@straat", adres.Straat);
                    cmd.Parameters.AddWithValue("@huisnummer", adres.Huisnummer);
                    cmd.Parameters.AddWithValue("@postcode", adres.Postcode);
                    cmd.Parameters.AddWithValue("@gemeente", adres.Gemeente);
                    cmd.Parameters.AddWithValue("@wijk", wijk);
                    cmd.Parameters.AddWithValue("@project_eigenaar_id", eigenaar.Id);

                    //checks om te bewaren in db welk type het is
                    cmd.Parameters.AddWithValue("@is_stadsontwikkeling", isStadsontwikkeling);
                    cmd.Parameters.AddWithValue("@is_groene_ruimte", isGroeneRuimte);
                    cmd.Parameters.AddWithValue("@is_innovatief_wonen", isInnovatiefWonen);


                    // TYPE-SPECIFIEKE velden:
                    // als null => DBNull.Value, anders waarde
                    // Stadsontwikkeling
                    cmd.Parameters.AddWithValue("@vergunningsstatus",
                        vergunningsStatus == null ? (object)DBNull.Value : vergunningsStatus.ToString());
                    cmd.Parameters.AddWithValue("@architecturale_waarde",
                        architecturaleWaarde ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@toegankelijkheid",
                        toegankelijkheid == null ? (object)DBNull.Value : toegankelijkheid.ToString());
                    cmd.Parameters.AddWithValue("@bezienswaardigheid_voor_toeristen",
                        beziensWaardigheidVoortoeristen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@infoborden_of_wandeling",
                        infoBordenOfWandeling ?? (object)DBNull.Value);

                    // Groene ruimte
                    cmd.Parameters.AddWithValue("@oppervlakte_in_vierkante_meter",
                        oppervlakteInVierkanteMeter ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@biodiversiteitsscore",
                        bioDiversiteitsScore ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@aantal_wandelpaden",
                        aantalWandelpaden ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@opgenomen_in_wandelroute",
                        opgenomenInWandelRoute ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@bezoekersscore",
                        bezoekersScore ?? (object)DBNull.Value);
                    //faciliteitne om zetten naar 1 string
                    cmd.Parameters.AddWithValue("@faciliteiten",
                        faciliteiten == null ? (object)DBNull.Value : VoegLijstSamenInDb(faciliteiten));

                    // Innovatief wonen
                    cmd.Parameters.AddWithValue("@aantal_wooneenheden",
                        aantalWooneenheden ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@rondleiding_mogelijk",
                        rondleidingMogelijk ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@innovatie_score",
                        innovatieScore ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@showwoning_beschikbaar",
                        showwoningBeschikbaar ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@samenwerking_erfgoed",
                        samenwerkingErfgoed ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@samenwerking_toerisme",
                        samenwerkingToerisme ?? (object)DBNull.Value);
                    //omzetten naar 1 string
                    cmd.Parameters.AddWithValue("@woonvormen",
                        woonvormen == null ? (object)DBNull.Value : VoegLijstSamenInDb(woonvormen));

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        //maak enkele projecten

        //// geef alleen parameters die logisch zijn voor een GroeneRuimteProject

        // Daarna roep SchrijfProjectNaarDb methode op
        // en zet checks juist + zet de andere types op null
        public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
                                               List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
                                               double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
                                               int? bezoekersScore, List<string> faciliteiten, Gebruiker gebruiker, Adres Adres)
        {
            SchrijfProjectNaarDb(
                projectTitel, beschrijving, startDatum, projectStatus, wijk,
                fotos, documenten, partners,
                gebruiker, Adres,

                isStadsontwikkeling: false,
                isGroeneRuimte: true,
                isInnovatiefWonen: false,

                oppervlakteInVierkanteMeter: oppervlakteInVierkanteMeter,
                bioDiversiteitsScore: bioDiversiteitsScore,
                aantalWandelpaden: aantalWandelpaden,
                opgenomenInWandelRoute: opgenomenInWandelRoute,
                bezoekersScore: bezoekersScore,
                faciliteiten: faciliteiten,

                aantalWooneenheden: null,
                rondleidingMogelijk: null,
                innovatieScore: null,
                showwoningBeschikbaar: null,
                samenwerkingErfgoed: null,
                samenwerkingToerisme: null,
                woonvormen: null,

                vergunningsStatus: null,
                architecturaleWaarde: null,
                toegankelijkheid: null,
                beziensWaardigheidVoortoeristen: null,
                infoBordenOfWandeling: null);
        }

        public void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
                                                  string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
                                                  int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
                                                  bool samenwerkingToerisme, List<string> woonvormen, Gebruiker gebruiker, Adres Adres)
        {
            SchrijfProjectNaarDb(
                projectTitel, beschrijving, startDatum, projectStatus, wijk,
                fotos, documenten, partners,
                gebruiker, Adres,

                isStadsontwikkeling: false,
                isGroeneRuimte: false,
                isInnovatiefWonen: true,

                oppervlakteInVierkanteMeter: null,
                bioDiversiteitsScore: null,
                aantalWandelpaden: null,
                opgenomenInWandelRoute: null,
                bezoekersScore: null,
                faciliteiten: null,

                aantalWooneenheden: aantalWooneenheden,
                rondleidingMogelijk: rondleidingMogelijk,
                innovatieScore: innovatieScore,
                showwoningBeschikbaar: showwoningBeschikbaar,
                samenwerkingErfgoed: samenwerkingErfgoed,
                samenwerkingToerisme: samenwerkingToerisme,
                woonvormen: woonvormen,

                vergunningsStatus: null,
                architecturaleWaarde: null,
                toegankelijkheid: null,
                beziensWaardigheidVoortoeristen: null,
                infoBordenOfWandeling: null);
        }

        public void MaakStadsontwikkelingsProjectAan(string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
                                                     string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners,
                                                     VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
                                                     bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, Gebruiker gebruiker, Adres Adres)
        {
            SchrijfProjectNaarDb(
                projectTitel, beschrijving, startDatum, projectStatus, wijk,
                fotos, documenten, partners,
                gebruiker, Adres,

                isStadsontwikkeling: true,
                isGroeneRuimte: false,
                isInnovatiefWonen: false,

                oppervlakteInVierkanteMeter: null,
                bioDiversiteitsScore: null,
                aantalWandelpaden: null,
                opgenomenInWandelRoute: null,
                bezoekersScore: null,
                faciliteiten: null,

                aantalWooneenheden: null,
                rondleidingMogelijk: null,
                innovatieScore: null,
                showwoningBeschikbaar: null,
                samenwerkingErfgoed: null,
                samenwerkingToerisme: null,
                woonvormen: null,

                vergunningsStatus: vergunningsStatus,
                architecturaleWaarde: architecturaleWaarde,
                toegankelijkheid: toegankelijkheid,
                beziensWaardigheidVoortoeristen: beziensWaardigheidVoortoeristen,
                infoBordenOfWandeling: infoBordenOfWandeling);

        }

        //maak dubbele projecten
        public void MaakGroeneRuimteInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
                                                              List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
                                                              double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
                                                              int? bezoekersScore, List<string> faciliteiten,
                                                              int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
                                                              bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres Adres)
        {
            SchrijfProjectNaarDb(
                projectTitel, beschrijving, startDatum, projectStatus, wijk,
                fotos, documenten, partners,
                eigenaar, Adres,

                isStadsontwikkeling: false,
                isGroeneRuimte: true,
                isInnovatiefWonen: true,

                oppervlakteInVierkanteMeter: oppervlakteInVierkanteMeter,
                bioDiversiteitsScore: bioDiversiteitsScore,
                aantalWandelpaden: aantalWandelpaden,
                opgenomenInWandelRoute: opgenomenInWandelRoute,
                bezoekersScore: bezoekersScore,
                faciliteiten: faciliteiten,

                aantalWooneenheden: aantalWooneenheden,
                rondleidingMogelijk: rondleidingMogelijk,
                innovatieScore: innovatieScore,
                showwoningBeschikbaar: showwoningBeschikbaar,
                samenwerkingErfgoed: samenwerkingErfgoed,
                samenwerkingToerisme: samenwerkingToerisme,
                woonvormen: woonvormen,

                vergunningsStatus: null,
                architecturaleWaarde: null,
                toegankelijkheid: null,
                beziensWaardigheidVoortoeristen: null,
                infoBordenOfWandeling: null);
        }


        public void MaakStadsOntwikkelingGroeneRuimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
                                                                string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
                                                                Gebruiker projectEigenaar, Adres Adres, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, 
                                                                Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,bool infoBordenOfWandeling, 
                                                                List<BouwFirma> bouwfirmas, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, 
                                                                bool opgenomenInWandelRoute, int? bezoekersScore, List<string> faciliteiten)
        {
            SchrijfProjectNaarDb(
                projectTitel, beschrijving, startDatum, projectStatus, wijk,
                fotos, documenten, partners,
                projectEigenaar, Adres,

                isStadsontwikkeling: true,
                isGroeneRuimte: true,
                isInnovatiefWonen: false,

                oppervlakteInVierkanteMeter: oppervlakteInVierkanteMeter,
                bioDiversiteitsScore: bioDiversiteitsScore,
                aantalWandelpaden: aantalWandelpaden,
                opgenomenInWandelRoute: opgenomenInWandelRoute,
                bezoekersScore: bezoekersScore,
                faciliteiten: faciliteiten,

                aantalWooneenheden: null,
                rondleidingMogelijk: null,
                innovatieScore: null,
                showwoningBeschikbaar: null,
                samenwerkingErfgoed: null,
                samenwerkingToerisme: null,
                woonvormen: null,

                vergunningsStatus: vergunningsStatus,
                architecturaleWaarde: architecturaleWaarde,
                toegankelijkheid: toegankelijkheid,
                beziensWaardigheidVoortoeristen: beziensWaardigheidVoortoeristen,
                infoBordenOfWandeling: infoBordenOfWandeling);

        }

        public void MaakStadsOntwikkelingInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
                                                                   string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
                                                                   VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
                                                                   bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, 
                                                                   bool showwoningBeschikbaar, bool samenwerkingErfgoed, bool samenwerkingToerisme, List<string> woonvormen,Gebruiker eigenaar, 
                                                                   Adres Adres)
        {
            SchrijfProjectNaarDb(
                projectTitel, beschrijving, startDatum, projectStatus, wijk,
                fotos, documenten, partners,
                eigenaar, Adres,

                isStadsontwikkeling: true,
                isGroeneRuimte: false,
                isInnovatiefWonen: true,

                oppervlakteInVierkanteMeter: null,
                bioDiversiteitsScore: null,
                aantalWandelpaden: null,
                opgenomenInWandelRoute: null,
                bezoekersScore: null,
                faciliteiten: null,

                aantalWooneenheden: aantalWooneenheden,
                rondleidingMogelijk: rondleidingMogelijk,
                innovatieScore: innovatieScore,
                showwoningBeschikbaar: showwoningBeschikbaar,
                samenwerkingErfgoed: samenwerkingErfgoed,
                samenwerkingToerisme: samenwerkingToerisme,
                woonvormen: woonvormen,

                vergunningsStatus: vergunningsStatus,
                architecturaleWaarde: architecturaleWaarde,
                toegankelijkheid: toegankelijkheid,
                beziensWaardigheidVoortoeristen: beziensWaardigheidVoortoeristen,
                infoBordenOfWandeling: infoBordenOfWandeling);

        }

        //crazy combo
        public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
                                                                             string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
                                                                             double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
                                                                             int? bezoekersScore, List<string> faciliteiten, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, 
                                                                             Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen, bool infoBordenOfWandeling, 
                                                                             List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, 
                                                                             bool showwoningBeschikbaar, bool samenwerkingErfgoed, bool samenwerkingToerisme, List<string> woonvormen, 
                                                                             Gebruiker Eigenaar, Adres Adres)
        {
            SchrijfProjectNaarDb(
                projectTitel, beschrijving, startDatum, projectStatus, wijk,
                fotos, documenten, partners,
                Eigenaar, Adres,

                isStadsontwikkeling: true,
                isGroeneRuimte: true,
                isInnovatiefWonen: true,

                oppervlakteInVierkanteMeter: oppervlakteInVierkanteMeter,
                bioDiversiteitsScore: bioDiversiteitsScore,
                aantalWandelpaden: aantalWandelpaden,
                opgenomenInWandelRoute: opgenomenInWandelRoute,
                bezoekersScore: bezoekersScore,
                faciliteiten: faciliteiten,

                aantalWooneenheden: aantalWooneenheden,
                rondleidingMogelijk: rondleidingMogelijk,
                innovatieScore: innovatieScore,
                showwoningBeschikbaar: showwoningBeschikbaar,
                samenwerkingErfgoed: samenwerkingErfgoed,
                samenwerkingToerisme: samenwerkingToerisme,
                woonvormen: woonvormen,

                vergunningsStatus: vergunningsStatus,
                architecturaleWaarde: architecturaleWaarde,
                toegankelijkheid: toegankelijkheid,
                beziensWaardigheidVoortoeristen: beziensWaardigheidVoortoeristen,
                infoBordenOfWandeling: infoBordenOfWandeling );

        }

        #region oude code
        //public Project MapProject(SqlDataReader reader)
        //{
        //    // algemene info
        //    int id = (int)reader["id"];
        //    string titel = (string)reader["project_titel"];
        //    string beschrijving = (string)reader["beschrijving"];

        //    DateTime? startDatum =
        //        reader["startdatum"] == DBNull.Value
        //            ? (DateTime?)null
        //            : (DateTime)reader["startdatum"];

        //    string statusString = (string)reader["project_status"];
        //    ProjectStatus status = Enum.Parse<ProjectStatus>(statusString);

        //    string wijk = (string)reader["wijk"];

        //    var adres = new Adres(
        //        (string)reader["straat"],
        //        (string)reader["huisnummer"],
        //        (int)reader["postcode"],
        //        (string)reader["gemeente"]
        //    );

        //    int eigenaarId = (int)reader["eigenaar_id"];
        //    string eigenaarNaam = (string)reader["eigenaar_naam"];
        //    string eigenaarEmail = (string)reader["eigenaar_email"];
        //    string eigenaarRolString = (string)reader["eigenaar_rol"];
        //    GebruikersRol eigenaarRol = Enum.Parse<GebruikersRol>(eigenaarRolString);

        //    var eigenaar = new Gebruiker(eigenaarId, eigenaarNaam, eigenaarEmail, eigenaarRol);

        //    bool isSOP = (bool)reader["is_stadsontwikkeling"];
        //    bool isGRP = (bool)reader["is_groene_ruimte"];
        //    bool isIWP = (bool)reader["is_innovatief_wonen"];

        //    var fotos = new List<byte[]>();
        //    var docs = new List<byte[]>();
        //    var partners = new List<Partner>();

        //    GroeneRuimte? groeneRuimte = null;
        //    StadsOntwikkeling? stadsOntwikkeling = null;
        //    InnovatiefWonen? innovatiefWonen = null;

        //    if (isGRP)
        //    {
        //        double opp =
        //            reader["oppervlakte_in_vierkante_meter"] == DBNull.Value
        //                ? 0.0
        //                : (double)reader["oppervlakte_in_vierkante_meter"];

        //        int? bioScore =
        //            reader["biodiversiteitsscore"] == DBNull.Value
        //                ? (int?)null
        //                : (int)reader["biodiversiteitsscore"];

        //        int? aantalWandelpaden =
        //            reader["aantal_wandelpaden"] == DBNull.Value
        //                ? (int?)null
        //                : (int)reader["aantal_wandelpaden"];

        //        bool opgenomenInWandelroute =
        //            reader["opgenomen_in_wandelroute"] == DBNull.Value
        //                ? false
        //                : (bool)reader["opgenomen_in_wandelroute"];

        //        int? bezoekersScore =
        //            reader["bezoekersscore"] == DBNull.Value
        //                ? (int?)null
        //                : (int)reader["bezoekersscore"];

        //        string faciliteitenCsv =
        //            reader["faciliteiten"] == DBNull.Value
        //                ? ""
        //                : (string)reader["faciliteiten"];

        //        var faciliteiten = SplitStringToList(faciliteitenCsv);

        //        groeneRuimte = new GroeneRuimte(opp, bioScore, aantalWandelpaden, opgenomenInWandelroute,bezoekersScore, faciliteiten);
        //    }

        //    if (isIWP)
        //    {
        //        int aantalWooneenheden =
        //            reader["aantal_wooneenheden"] == DBNull.Value
        //                ? 0
        //                : (int)reader["aantal_wooneenheden"];

        //        bool rondleidingMogelijk =
        //            reader["rondleiding_mogelijk"] == DBNull.Value
        //                ? false
        //                : (bool)reader["rondleiding_mogelijk"];

        //        int? innovatieScore =
        //            reader["innovatie_score"] == DBNull.Value
        //                ? (int?)null
        //                : (int)reader["innovatie_score"];

        //        bool showwoningBeschikbaar =
        //            reader["showwoning_beschikbaar"] == DBNull.Value
        //                ? false
        //                : (bool)reader["showwoning_beschikbaar"];

        //        bool samenwerkingErfgoed =
        //            reader["samenwerking_erfgoed"] == DBNull.Value
        //                ? false
        //                : (bool)reader["samenwerking_erfgoed"];

        //        bool samenwerkingToerisme =
        //            reader["samenwerking_toerisme"] == DBNull.Value
        //                ? false
        //                : (bool)reader["samenwerking_toerisme"];

        //        string woonvormenCsv =
        //            reader["woonvormen"] == DBNull.Value
        //                ? ""
        //                : (string)reader["woonvormen"];

        //        var woonvormen = SplitStringToList(woonvormenCsv);

        //        innovatiefWonen = new InnovatiefWonen(
        //            aantalWooneenheden,rondleidingMogelijk,innovatieScore,showwoningBeschikbaar,samenwerkingErfgoed, samenwerkingToerisme, woonvormen);
        //    }

        //    if (isSOP)
        //    {
        //        string vergString =
        //            reader["vergunningsstatus"] == DBNull.Value
        //                ? "InAanvraag"      //TODO kies hier een bestaande enumwaarde
        //                : (string)reader["vergunningsstatus"];

        //        VergunningsStatus vergunningsstatus =
        //            Enum.Parse<VergunningsStatus>(vergString);

        //        bool architecturaleWaarde =
        //            reader["architecturale_waarde"] == DBNull.Value
        //                ? false
        //                : (bool)reader["architecturale_waarde"];

        //        string toegString =
        //            reader["toegankelijkheid"] == DBNull.Value
        //                ? "Gedeeltelijk"   //TODO kies hier ook een bestaande enumwaarde
        //                : (string)reader["toegankelijkheid"];

        //        Toegankelijkheid toegankelijkheid =
        //            Enum.Parse<Toegankelijkheid>(toegString);

        //        bool bezienswaardigheid =
        //            reader["bezienwaardigheid_voor_toeristen"] == DBNull.Value
        //                ? false
        //                : (bool)reader["bezienwaardigheid_voor_toeristen"];

        //        bool infoborden =
        //            reader["infoborden_of_wandeling"] == DBNull.Value
        //                ? false
        //                : (bool)reader["infoborden_of_wandeling"];

        //        // Bouwfirmas later (komt uit tussentabel)
        //        var bouwfirmas = new List<BouwFirma>();

        //        stadsOntwikkeling = new StadsOntwikkeling(vergunningsstatus, architecturaleWaarde, toegankelijkheid,bezienswaardigheid,infoborden,bouwfirmas);
        //    }

        //    return MaakProjectSubtype(id, titel, beschrijving, startDatum, status,  wijk, fotos, docs, partners, eigenaar, adres, groeneRuimte, stadsOntwikkeling, innovatiefWonen);
        //}
        //private Project MaakProjectSubtype( int id, string titel, string beschrijving, DateTime? startDatum, ProjectStatus status, string wijk, List<byte[]> fotos, List<byte[]> docs,
        //    List<Partner> partners, Gebruiker eigenaar, Adres adres, GroeneRuimte? GRP, StadsOntwikkeling? SOP, InnovatiefWonen? IWP)
        //{
        //    if (GRP != null && IWP == null && SOP == null)
        //        return new GroeneRuimteProject
        //            (id, titel, beschrijving, startDatum, status, wijk, fotos, docs, partners, eigenaar, adres, GRP);

        //    if (GRP == null && IWP != null && SOP == null)
        //        return new InnovatiefWonenProject
        //            (id, titel, beschrijving, startDatum, status, wijk, fotos, docs, partners, eigenaar, adres, IWP);

        //    if (GRP == null && IWP == null && SOP != null)
        //        return new StadsontwikkelingProject
        //            (id, titel, beschrijving, startDatum, status, wijk, fotos, docs, partners, eigenaar, adres, SOP);

        //    if (GRP != null && IWP != null && SOP == null)
        //        return new GroeneRuimteInnovatiefWonenProject
        //            (id, titel, beschrijving, startDatum, status, wijk, fotos, docs, partners, eigenaar, adres, GRP, IWP);

        //    if (GRP != null && IWP == null && SOP != null)
        //        return new StadsontwikkelingsGroeneRuimteProject
        //            (id, titel, beschrijving, startDatum, status, wijk, fotos, docs, partners, eigenaar, adres, SOP, GRP);

        //    if (GRP == null && IWP != null && SOP != null)
        //        return new StadsontwikkelingsInnovatiefWonenProject
        //            (id, titel, beschrijving, startDatum, status, wijk, fotos, docs, partners, eigenaar, adres, SOP, IWP);

        //    if (GRP != null && IWP != null && SOP != null)
        //        return new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject
        //            (id, titel, beschrijving, startDatum, status, wijk, fotos, docs, partners, eigenaar, adres, SOP, GRP, IWP);

        //    // zou niet mogen gebeuren als je flags correct staan
        //    throw new Exception("Ongeldige projectcombinatie in databank.");
        //}

        #endregion


        #region oude code
        //public void MaakGroeneRuimteInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
        //    List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
        //    int? bezoekersScore, List<string> faciliteiten, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
        //    bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres) => throw new NotImplementedException();
        ////{
        ////    GroeneRuimteInnovatiefWonenProject project = new GroeneRuimteInnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum,
        ////        projectStatus, wijk, fotos, documenten, partners, eigenaar, adres,
        ////        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten),
        ////        MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

        ////    projectLijst.Add(projectId, project);
        ////    projectId++;
        ////}



        //public void MaakGroeneruimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus, string wijk,
        //    List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
        //    // parameters aanmaak groeneRuimte
        //    double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
        //    int? bezoekersScore, List<string> faciliteiten, Gebruiker gebruiker, Adres adres) => throw new NotImplementedException();
        ////{
        ////    GroeneRuimteProject project = new GroeneRuimteProject(projectId, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, gebruiker, adres,
        ////        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));

        ////    projectLijst.Add(projectId, project);
        ////    projectId++;
        ////}



        //public void MaakInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
        //    string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
        //    //parameters innovatief wonen
        //    int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
        //    bool samenwerkingToerisme, List<string> woonvormen, Gebruiker gebruiker, Adres adres) => throw new NotImplementedException();
        ////{
        ////    InnovatiefWonenProject project = new InnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum, projectStatus,
        ////     wijk, fotos, documenten, partners, gebruiker, adres, MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
        ////    samenwerkingToerisme, woonvormen));

        ////    projectLijst.Add(projectId, project);
        ////    projectId++;
        ////}



        //public void MaakStadsOntwikkelingGroeneRuimteProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
        //    string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners, Gebruiker projectEigenaar, Adres adres, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
        //    bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
        //    int? bezoekersScore, List<string> faciliteiten) => throw new NotImplementedException();
        ////{
        ////    StadsontwikkelingsGroeneRuimteProject project = new StadsontwikkelingsGroeneRuimteProject(projectId, projectTitel, beschrijving, startDatum, projectStatus, wijk, fotos, documenten, partners, projectEigenaar, adres,
        ////        MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
        ////        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten));
        ////    projectLijst.Add(projectId, project);
        ////    projectId++;
        ////}



        //public void MaakStadsOntwikkelingInnovatiefWonenProjectAan(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
        //    string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
        //    VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
        //    bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
        //    bool samenwerkingToerisme, List<string> woonvormen, Gebruiker eigenaar, Adres adres) => throw new NotImplementedException();
        ////{
        ////    StadsontwikkelingsInnovatiefWonenProject project = new StadsontwikkelingsInnovatiefWonenProject(projectId, projectTitel, beschrijving, startDatum,
        ////        projectStatus, wijk, fotos, documenten, partners, eigenaar, adres,
        ////        MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
        ////        MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

        ////    projectLijst.Add(projectId, project);
        ////    projectId++;
        ////}



        //public void MaakStadsontwikkelingsGroeneRuimteinnovatiefWonenProject(string projectTitel, string beschrijving, DateTime? startDatum, ProjectStatus projectStatus,
        //    string wijk, List<byte[]>? fotos, List<byte[]>? documenten, List<Partner> partners,
        //    double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
        //    int? bezoekersScore, List<string> faciliteiten, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
        //    bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
        //    bool samenwerkingToerisme, List<string> woonvormen, Gebruiker Eigenaar, Adres adres
        //    ) => throw new NotImplementedException();
        ////{
        ////    StadsontwikkelingsGroeneRuimteInnovatiefWonenProject project = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(projectId, projectTitel, beschrijving,
        ////        startDatum, projectStatus, wijk, fotos, documenten, partners, Eigenaar, adres,
        ////        MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen, infoBordenOfWandeling, bouwfirmas),
        ////        MaakGroeneRuimteAan(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten),
        ////        MaakInnovatiefWonenAan(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed, samenwerkingToerisme, woonvormen));

        ////    projectLijst.Add(projectId, project);
        ////    projectId++;
        ////}



        //public void MaakStadsontwikkelingsProjectAan(string projectTitel, string beschrijving, DateTime startDatum, ProjectStatus projectStatus,
        //    string wijk, List<byte[]> fotos, List<byte[]> documenten, List<Partner> partners, VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
        //    bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas, Gebruiker gebruiker, Adres adres) => throw new NotImplementedException();
        ////{
        ////    StadsontwikkelingProject project = new StadsontwikkelingProject(projectId, projectTitel, beschrijving, startDatum, projectStatus,
        ////     wijk, fotos, documenten, partners, gebruiker, adres, MaakStadsOntwikkelingAan(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen,
        ////     infoBordenOfWandeling, bouwfirmas));
        ////    projectLijst.Add(projectId, project);
        ////    projectId++;

        ////}



        ////methode aanmaak types

        //private GroeneRuimte MaakGroeneRuimteAan(double oppervlakteInVierkanteMeter, int? bioDiversiteitsScore, int? aantalWandelpaden, bool opgenomenInWandelRoute,
        //    int? bezoekersScore, List<string> faciliteiten) => throw new NotImplementedException();
        ////{
        ////    return new GroeneRuimte(oppervlakteInVierkanteMeter, bioDiversiteitsScore, aantalWandelpaden, opgenomenInWandelRoute, bezoekersScore, faciliteiten);
        ////}

        //private InnovatiefWonen MaakInnovatiefWonenAan(int aantalWooneenheden, bool rondleidingMogelijk, int? innovatieScore, bool showwoningBeschikbaar, bool samenwerkingErfgoed,
        //    bool samenwerkingToerisme, List<string> woonvormen) => throw new NotImplementedException();
        ////{
        ////    return new InnovatiefWonen(aantalWooneenheden, rondleidingMogelijk, innovatieScore, showwoningBeschikbaar, samenwerkingErfgoed,
        ////    samenwerkingToerisme, woonvormen);
        ////}

        //private StadsOntwikkeling MaakStadsOntwikkelingAan(VergunningsStatus vergunningsStatus, bool architecturaleWaarde, Toegankelijkheid toegankelijkheid, bool beziensWaardigheidVoortoeristen,
        //    bool infoBordenOfWandeling, List<BouwFirma> bouwfirmas) => throw new NotImplementedException();
        ////{
        ////    return new StadsOntwikkeling(vergunningsStatus, architecturaleWaarde, toegankelijkheid, beziensWaardigheidVoortoeristen,
        ////     infoBordenOfWandeling, bouwfirmas);
        ////}

        #endregion

        // FILTEREN en SORTEREN en OPVRAGEN
        // Dictionary zodat elk project maar 1 keer aanmaak
        //  door LEFT JOIN krijg ik meerdere rijen per project
        private List<Project> GeefProjecten(string sqlQuery, List<SqlParameter> parameters)
        {
            Dictionary<int, Project> data = new Dictionary<int, Project>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sqlQuery;
                // Parameters toevoegen als de query filters heeft
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int id = (int)rd["id"];

                        if (!data.ContainsKey(id))
                        {

                            string titel = rd["project_titel"] as string ?? "";
                            string beschrijving = rd["beschrijving"] as string ?? "";


                            DateTime? date = rd["startdatum"] as DateTime?;


                            string statusStr = rd["project_status"] as string;
                            ProjectStatus status = !string.IsNullOrEmpty(statusStr)
                                ? (ProjectStatus)Enum.Parse(typeof(ProjectStatus), statusStr)
                                : ProjectStatus.Planning;


                            string straat = rd["straat"] as string ?? "";
                            string huisnr = rd["huisnummer"] as string ?? "";
                            int postcode = rd["postcode"] as int? ?? 0;
                            string gemeente = rd["gemeente"] as string ?? "";
                            Adres adres = new Adres(straat, huisnr, postcode, gemeente);

                            string wijk = rd["wijk"] as string ?? "";


                            int eigenaarId = rd["gebruiker_id"] as int? ?? 0;
                            string eigenaarNaam = rd["eigenaar_naam"] as string ?? "Onbekend";
                            string eigenaarEmail = rd["eigenaar_email"] as string ?? "";
                            string rolStr = rd["gebruikersrol"] as string;
                            GebruikersRol rol = !string.IsNullOrEmpty(rolStr)
                                ? (GebruikersRol)Enum.Parse(typeof(GebruikersRol), rolStr)
                                : GebruikersRol.GewoneGebruiker;

                            Gebruiker eigenaar = new Gebruiker(eigenaarId, eigenaarNaam, eigenaarEmail, rol);


                            bool isStads = rd["is_stadsontwikkeling"] as bool? ?? false;
                            bool isGroen = rd["is_groene_ruimte"] as bool? ?? false;
                            bool isInnov = rd["is_innovatief_wonen"] as bool? ?? false;


                            StadsOntwikkeling stads = null;
                            GroeneRuimte groen = null;
                            InnovatiefWonen innov = null;

                            if (isStads)
                            {
                                string vergStr = rd["vergunningsstatus"] as string;
                                var verg = !string.IsNullOrEmpty(vergStr) ? (VergunningsStatus)Enum.Parse(typeof(VergunningsStatus), vergStr) : VergunningsStatus.InAanvraag;
                                bool arch = rd["architecturale_waarde"] as bool? ?? false;
                                string toegStr = rd["toegankelijkheid"] as string;
                                var toeg = !string.IsNullOrEmpty(toegStr) ? (Toegankelijkheid)Enum.Parse(typeof(Toegankelijkheid), toegStr) : Toegankelijkheid.Gesloten;
                                bool bez = rd["bezienswaardigheid_voor_toeristen"] as bool? ?? false;
                                bool info = rd["infoborden_of_wandeling"] as bool? ?? false;

                                stads = new StadsOntwikkeling(verg, arch, toeg, bez, info, new List<BouwFirma>());
                            }

                            if (isGroen)
                            {
                                double opp = rd["oppervlakte_in_vierkante_meter"] as double? ?? 0.0;
                                int? bio = rd["biodiversiteitsscore"] as int?;
                                int? paden = rd["aantal_wandelpaden"] as int?;
                                bool route = rd["opgenomen_in_wandelroute"] as bool? ?? false;
                                int? bezScore = rd["bezoekersscore"] as int?;

                                string facStr = rd["faciliteiten"] as string;

                                List<string> faciliteiten = !string.IsNullOrEmpty(facStr) ? facStr.Split(new[] { ',', '|' }).ToList() : new List<string>();

                                groen = new GroeneRuimte(opp, bio, paden, route, bezScore, faciliteiten);
                            }

                            if (isInnov)
                            {
                                int aantal = rd["aantal_wooneenheden"] as int? ?? 0;
                                bool rond = rd["rondleiding_mogelijk"] as bool? ?? false;
                                int? innScore = rd["innovatie_score"] as int?;
                                bool show = rd["showwoning_beschikbaar"] as bool? ?? false;
                                bool erf = rd["samenwerking_erfgoed"] as bool? ?? false;
                                bool toer = rd["samenwerking_toerisme"] as bool? ?? false;

                                string woonStr = rd["woonvormen"] as string;
                                List<string> woonvormen = !string.IsNullOrEmpty(woonStr) ? woonStr.Split(new[] { ',', '|' }).ToList() : new List<string>();

                                innov = new InnovatiefWonen(aantal, rond, innScore, show, erf, toer, woonvormen);
                            }


                            Project p = null;
                            var fotos = new List<byte[]>();
                            var docs = new List<byte[]>();
                            var partners = new List<Partner>();

                            if (isStads && isGroen && isInnov)
                                p = new StadsontwikkelingsGroeneRuimteInnovatiefWonenProject(id, titel, beschrijving, date, status, wijk, fotos, docs, partners, eigenaar, adres, stads, groen, innov);
                            else if (isStads && isGroen)
                                p = new StadsontwikkelingsGroeneRuimteProject(id, titel, beschrijving, date, status, wijk, fotos, docs, partners, eigenaar, adres, stads, groen);
                            else if (isStads && isInnov)
                                p = new StadsontwikkelingsInnovatiefWonenProject(id, titel, beschrijving, date, status, wijk, fotos, docs, partners, eigenaar, adres, stads, innov);
                            else if (isGroen && isInnov)
                                p = new GroeneRuimteInnovatiefWonenProject(id, titel, beschrijving, date, status, wijk, fotos, docs, partners, eigenaar, adres, groen, innov);
                            else if (isStads)
                                p = new StadsontwikkelingProject(id, titel, beschrijving, date, status, wijk, fotos, docs, partners, eigenaar, adres, stads);
                            else if (isGroen)
                                p = new GroeneRuimteProject(id, titel, beschrijving, date, status, wijk, fotos, docs, partners, eigenaar, adres, groen);
                            else if (isInnov)
                                p = new InnovatiefWonenProject(id, titel, beschrijving, date, status, wijk, fotos, docs, partners, eigenaar, adres, innov);

                            if (p != null) data.Add(id, p);
                        }


                        if (data.TryGetValue(id, out Project currentProject))
                        {

                            if (rd["partner_id"] != DBNull.Value)
                            {
                                int pId = (int)rd["partner_id"];
                                if (!currentProject.Partners.Any(x => x.Id == pId))
                                {
                                    currentProject.Partners.Add(new Partner(
                                        pId,
                                        rd["partner_naam"] as string ?? "",
                                        rd["partner_email"] as string ?? "",
                                        rd["partner_tel"] as string ?? "",
                                        rd["partner_rol"] as string ?? ""
                                    ));
                                }
                            }


                            if (rd["bouw_id"] != DBNull.Value)
                            {
                                int bId = (int)rd["bouw_id"];
                                if (currentProject is IStadsontwikkeling stadsProj)
                                {
                                    if (!stadsProj.StadsOntwikkeling.BouwFirmas.Any(b => b.Id == bId))
                                    {
                                        stadsProj.StadsOntwikkeling.BouwFirmas.Add(new BouwFirma(
                                            bId,
                                            rd["bouwfirma_naam"] as string ?? "",
                                            rd["bouwfirma_email"] as string ?? "",
                                            rd["bouwfirma_tel"] as string ?? ""
                                        ));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return data.Values.ToList();
        }

        public List<Project> GeefAlleProjecten()
        {
            string sql = @"
                SELECT 
                    p.*, 
                    g.id AS gebruiker_id, g.naam AS eigenaar_naam, g.email AS eigenaar_email, g.gebruikersrol,
                    b.id as bouw_id, b.naam AS bouwfirma_naam, b.email AS bouwfirma_email, b.telefoonnummer AS bouwfirma_tel,
                    par.id as partner_id, par.naam AS partner_naam, par.email as partner_email, par.telefoonnummer as partner_tel, pp.rol_omschrijving AS partner_rol,
                    d.filenaam AS document_naam, f.filenaam AS foto_naam
                FROM project p
                    LEFT JOIN gebruiker g ON p.project_eigenaar_id = g.id
                    LEFT JOIN project_bouwfirma pb ON p.id = pb.project_id
                    LEFT JOIN bouwfirma b ON pb.bouwfirma_id = b.id
                    LEFT JOIN project_partner pp ON p.id = pp.project_id
                    LEFT JOIN partner par ON pp.partner_id = par.id
                    LEFT JOIN document d ON p.id = d.project_id
                    LEFT JOIN foto f ON p.id = f.project_id
                ORDER BY p.id";

            return GeefProjecten(sql, null);
        }

        public List<Project> GeefProjectenGefilterd(string projectnaam, string wijk, ProjectStatus status, string eigenaar, List<bool> typeChecks, DateTime start, DateTime eind)
        {
            //groen: 0
            //innov: 1
            //stadsont: 2               

            string sql = @"
                SELECT 
                    p.*, 
                    g.id AS gebruiker_id, g.naam AS eigenaar_naam, g.email AS eigenaar_email, g.gebruikersrol,
                    b.id as bouw_id, b.naam AS bouwfirma_naam, b.email AS bouwfirma_email, b.telefoonnummer AS bouwfirma_tel,
                    par.id as partner_id, par.naam AS partner_naam, par.email as partner_email, par.telefoonnummer as partner_tel, pp.rol_omschrijving AS partner_rol,
                    d.filenaam AS document_naam, f.filenaam AS foto_naam
                FROM project p
                    LEFT JOIN gebruiker g ON p.project_eigenaar_id = g.id
                    LEFT JOIN project_bouwfirma pb ON p.id = pb.project_id
                    LEFT JOIN bouwfirma b ON pb.bouwfirma_id = b.id
                    LEFT JOIN project_partner pp ON p.id = pp.project_id
                    LEFT JOIN partner par ON pp.partner_id = par.id
                    LEFT JOIN document d ON p.id = d.project_id
                    LEFT JOIN foto f ON p.id = f.project_id
                WHERE 
                    (@projectnaam IS NULL OR p.project_titel LIKE '%' + @projectnaam + '%')
                    AND (@wijk IS NULL OR p.wijk LIKE '%' + @wijk + '%')
                    AND (@eigenaar IS NULL OR g.naam LIKE '%' + @eigenaar + '%')
                    AND (@status IS NULL OR p.project_status = @status)
                    AND (p.startdatum >= @start AND p.startdatum <= @eind)
                    AND (
                        -- Logica: Als alles false is (0), toon alles. Anders filter op aangevinkte types.
                        (@checkGroen = 0 AND @checkInnov = 0 AND @checkStads = 0)
                        OR (@checkGroen = 1 AND p.is_groene_ruimte = 1)
                        OR (@checkInnov = 1 AND p.is_innovatief_wonen = 1)
                        OR (@checkStads = 1 AND p.is_stadsontwikkeling = 1)
                    )
                ORDER BY p.id";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@projectnaam", string.IsNullOrEmpty(projectnaam) ? (object)DBNull.Value : projectnaam));
            parameters.Add(new SqlParameter("@wijk", string.IsNullOrEmpty(wijk) ? (object)DBNull.Value : wijk));
            parameters.Add(new SqlParameter("@eigenaar", string.IsNullOrEmpty(eigenaar) ? (object)DBNull.Value : eigenaar));

            if (status == null)
            {
                parameters.Add(new SqlParameter("@status", DBNull.Value));
            }
            else
            {
                parameters.Add(new SqlParameter("@status", status.ToString()));
            }

            if (start == DateTime.MinValue)
            {
                parameters.Add(new SqlParameter("@start", System.Data.SqlTypes.SqlDateTime.MinValue.Value));
            }
            else
            {
                parameters.Add(new SqlParameter("@start", start));
            }

            if (eind == DateTime.MinValue || eind == DateTime.MaxValue)
            {
                parameters.Add(new SqlParameter("@eind", System.Data.SqlTypes.SqlDateTime.MaxValue.Value));
            }
            else
            {
                parameters.Add(new SqlParameter("@eind", eind));
            }
            parameters.Add(new SqlParameter("@checkGroen", typeChecks.Count > 0 && typeChecks[0] ? 1 : 0));
            parameters.Add(new SqlParameter("@checkInnov", typeChecks.Count > 1 && typeChecks[1] ? 1 : 0));
            parameters.Add(new SqlParameter("@checkStads", typeChecks.Count > 2 && typeChecks[2] ? 1 : 0));

            return GeefProjecten(sql, parameters);
        }

        public List<Project> GeefProjectenGefilterdOpPartners(string partners)
        {
            string sql = @"
                SELECT 
                    p.*, 
                    g.id AS gebruiker_id, g.naam AS eigenaar_naam, g.email AS eigenaar_email, g.gebruikersrol,
                    b.id as bouw_id, b.naam AS bouwfirma_naam, b.email AS bouwfirma_email, b.telefoonnummer AS bouwfirma_tel,
                    par.id as partner_id, par.naam AS partner_naam, par.email as partner_email, par.telefoonnummer as partner_tel, pp.rol_omschrijving AS partner_rol,
                    d.filenaam AS document_naam, f.filenaam AS foto_naam
                FROM project p
                    LEFT JOIN gebruiker g ON p.project_eigenaar_id = g.id
                    LEFT JOIN project_bouwfirma pb ON p.id = pb.project_id
                    LEFT JOIN bouwfirma b ON pb.bouwfirma_id = b.id
                    LEFT JOIN project_partner pp ON p.id = pp.project_id
                    LEFT JOIN partner par ON pp.partner_id = par.id
                    LEFT JOIN document d ON p.id = d.project_id
                    LEFT JOIN foto f ON p.id = f.project_id
                WHERE p.id IN (
                    SELECT pp_sub.project_id
                    FROM project_partner pp_sub
                    INNER JOIN partner par_sub ON pp_sub.partner_id = par_sub.id
                    WHERE par_sub.naam LIKE '%' + @partnerNaam + '%'
                )
                ORDER BY p.id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@partnerNaam", string.IsNullOrEmpty(partners) ? (object)DBNull.Value : partners));

            return GeefProjecten(sql, parameters);
        }

        public List<Project> GeefProjectenGefilterdOpStatus(string status)
        {
            if (Enum.TryParse(status, out ProjectStatus statusEnum))
            {
                return GeefProjectenGefilterd(null, null, statusEnum, null, new List<bool>(), DateTime.MinValue, DateTime.MaxValue);
            }
            return new List<Project>();
        }

        public List<Project> GeefProjectenGefilterdOpTitel(string titel)
        {
            return GeefProjectenGefilterd(titel, null, default, null, new List<bool>(), DateTime.MinValue, DateTime.MaxValue);
        }

        public List<Project> GeefProjectenGefilterdOpWijk(string wijk)
        {
            return GeefProjectenGefilterd(null, wijk, default, null, new List<bool>(), DateTime.MinValue, DateTime.MaxValue);
        }

    }

}

