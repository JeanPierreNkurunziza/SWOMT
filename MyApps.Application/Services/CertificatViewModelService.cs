using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    /// <summary>
    /// Service view model
    /// </summary>
   public class CertificatViewModelService
    {
        public static List<ViewModels.CertificatViewModel> GetCertificatsList(List<Infrastructure.DB.Certificat> listElement)
        {
            List<ViewModels.CertificatViewModel> Liste = new List<ViewModels.CertificatViewModel>();
           
            foreach (var itemList in listElement)
            {
                ViewModels.CertificatViewModel vm = new ViewModels.CertificatViewModel()
                {
                    IdCertificat = itemList.IdCertificat,
                    IdParticipant = itemList.IdParticipant,
                    NomParticipant = CertificatService.GetNomParticipant(itemList.IdParticipant),
                    DateDelivrance = itemList.DateDelivrance,
                };
                Liste.Add(vm);
            }

            return Liste;
        }

        /// <summary>
        /// méthose service pour des certificats dispensés
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetCertificats() 
        {           
            var GetListe = CertificatService.GetAll();
           
            return GetCertificatsList(GetListe);
        }
        /// <summary>
        /// méthode qui va nous permettre de récuperer les certificats par participant
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns> une liste des certificats </returns>
        public static List<ViewModels.CertificatViewModel> GetCertificatsPerParticipant( int IdParticipant)
        {
           
            var GetListe = CertificatService.GetListPerParticipant(IdParticipant); 
            
             return GetCertificatsList(GetListe);
        }
        /// <summary>
        /// Une méthode permettre de récuperer les participants 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetParticipants()
        {

            var ListeParticipant = ParticipantService.GetAll();
            return GetParticipantsList(ListeParticipant);
        }

        /// <summary>
        /// Récuperer les données concernant les participants 
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetParticipantsList(List<Infrastructure.DB.Participant> listElement)
        {
            List<ViewModels.CertificatViewModel> ParticipantsListe = new List<ViewModels.CertificatViewModel>();

            foreach (var participant in listElement)
            {
                ViewModels.CertificatViewModel vm = new ViewModels.CertificatViewModel()
                {
                    IdParticipant = participant.IdParticipant,
                    NomParticipant = participant.NomParticipant,
                    DateNaissance = participant.DateNaissance,
                    IdNational = participant.IdNational,
                    TelParticipant = participant.TélParticipant,
                    EmailParticipant = participant.EmailParticipant,
                    SecteurParticipant = participant.SecteurParticipant,
                    DistrictParticipant = participant.DistrictParticipant,
                    DateEncodage = participant.DateEncodage,

                };
                ParticipantsListe.Add(vm);
            }

            return ParticipantsListe;
        }
        /// récuperer des participants réussis
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetListParticipantRéussi(int idExamen)
        {

            var GetListe = ResultatService.GetListParticipantRéussi(idExamen);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer des participant échouées
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetListParticipantEchoué(int idExamen)
        {

            var GetListe = ResultatService.GetListParticipantEchoué(idExamen);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une liste des modules per inscription
        /// </summary>
        /// <param name="idModuleInscription"></param>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetListModulesEchoué(int idModuleInscription)
        {

            var GetListe = ResultatService.GetListModuleEchoué(idModuleInscription);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une liste des modules réussis par inscription 
        /// </summary>
        /// <param name="idModuleInscription"></param>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetListModulesRéussis(int idModuleInscription)
        {
            //List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetListModuleRéussis(idModuleInscription);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// Méthode pour faire une recherche selon le string passe en paramétre 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>retrourne le participant recherché </returns>
        public static List<ViewModels.CertificatViewModel> GetParticipantByMethodeSearch(string searchString)
        {

            var ListeParticipant = ParticipantService.SearchParticipantByName(searchString);


            return GetParticipantsList(ListeParticipant);
        }
        /// <summary>
        /// récuperer une liste des résultats 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetResultats()
        {

            var GetListe = ResultatService.GetAll();

            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// Récuperer les données concernant les résultats des participants
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.CertificatViewModel> GetResultatsList(List<Infrastructure.DB.Resultat> listeElement)
        {
            List<ViewModels.CertificatViewModel> Liste = new List<ViewModels.CertificatViewModel>();

            foreach (var itemList in listeElement)
            {
                ViewModels.CertificatViewModel vm = new ViewModels.CertificatViewModel()
                {
                    IdResultat = itemList.IdResultat,
                    IdExamen = itemList.IdExamen,
                    IdModuleInscription = itemList.IdModuleInscription,
                    NomModule = ResultatService.GetNomModule(itemList.IdExamen),
                    NomParticipant = ResultatService.GetNomParticipant(itemList.IdModuleInscription),
                    Points = itemList.Points,
                    EstPresent = itemList.EstPresent,
                    ParticipantRéussi = itemList.ParticipantRéussi
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
