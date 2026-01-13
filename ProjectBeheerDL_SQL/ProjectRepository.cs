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
                SqlTransaction transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.CommandText = sql;

                try
                {
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

                    cmd.Parameters.AddWithValue("@is_stadsontwikkeling", isStadsontwikkeling);
                    cmd.Parameters.AddWithValue("@is_groene_ruimte", isGroeneRuimte);
                    cmd.Parameters.AddWithValue("@is_innovatief_wonen", isInnovatiefWonen);

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

        public List<Project> GeefAlleProjecten()
        {
            throw new NotImplementedException();
            //var lijst = new List<Project>();

            //using (var conn = new SqlConnection(_connectionString))
            //using (var cmd = new SqlCommand(SqlSelectAll, conn))
            //{
            //    conn.Open();
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            lijst.Add(MapProject(reader));
            //        }
            //    }
            //}

            //return lijst;
        }

        public List<Project> GeefProjectenGefilterd(string projectnaam, string wijk, ProjectStatus status, string eigenaar, List<bool> typeChecks, DateTime start, DateTime eind)
         => throw new NotImplementedException();

       

        public List<Project> GeefProjectenGefilterdOpType(List<bool> types)
        {
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpPartners(string partners)
        {
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpStatus(string status)
        {
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpTitel(string titel)
        {
            throw new NotImplementedException();
        }

        public List<Project> GeefProjectenGefilterdOpWijk(string wijk)
        {
            throw new NotImplementedException();
        }


    }

}

