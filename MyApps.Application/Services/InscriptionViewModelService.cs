using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class InscriptionViewModelService 
    {
       /// <summary>
       /// la méthode pour mapper les données  de modèles inscription. 
       /// </summary>
       /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetInscriptions() 
        {
           
            var GetListe = InscriptionService.GetAll();
            
           return GetListElement(GetListe); 
        }
        /// <summary>
        /// les données concernant les inscriptions dans la DB
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetListElement (List<Infrastructure.DB.ModuleInscription> listeElement)
        {
            List<ViewModels.InscriptionViewModel> Liste = new List<ViewModels.InscriptionViewModel>();
            
            foreach (var itemList in listeElement)
            {
                ViewModels.InscriptionViewModel vm = new ViewModels.InscriptionViewModel()
                {
                    IdModuleInscription = itemList.IdModuleInscription,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    IdNational = InscriptionService.GetIdNational(itemList.IdParticipant),
                    DateInscription = itemList.DateInscription,
                    EstSurListeAttente = itemList.EstSurListeAttente,
                    TotalParticipantPerModule = InscriptionService.GetNbreParticipantParModule(itemList.IdSiteModule),
                };
                Liste.Add(vm);
            }

            return Liste;
        }

        /// <summary>
        /// avec l'identifiant de site module on peut afficher les paricipants concernés
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetParticipantPerModule(int IdSiteModule)
        {
          
            var GetListe = InscriptionService.GetListParticipantPerModule(IdSiteModule);
           
            return GetListElement(GetListe);
        }
        /// <summary>
        /// afficher les données via l'identifiant de participant
        /// </summary>
        /// <param name="IdParticipant"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetListAttentParticipantPerModule(int IdParticipant) 
        {
          
            var GetListe = InscriptionService.GetListAttenteParticipantPerModule(IdParticipant);
           
            return GetListElement(GetListe);
        }
        /// <summary>
        /// la liste des modules selon le partipant 
        /// </summary>
        /// <param name="IdModule"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetModulePerParticipant(int IdModule)
        {
           
            var GetListe = InscriptionService.GetListModulePerParticipant(IdModule);
           
            return GetListElement(GetListe);
        }
        public static List<ViewModels.InscriptionViewModel> GetParticipants()
        {

            var ListeParticipant = ParticipantService.GetAll();
            return GetParticipantsList(ListeParticipant);
        }
        /// <summary>
        /// afficher la liste selon le résultat de la recherche
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetParticipantByMethodeSearch(string searchString)
        {

            var ListeParticipant = ParticipantService.SearchParticipantByName(searchString);


            return GetParticipantsList(ListeParticipant);
        }
        /// <summary>
        /// Récuperer les données les informations des participants déjà inscrit dans les modules concernés
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetParticipantsList(List<Infrastructure.DB.Participant> listElement)
        {
            List<ViewModels.InscriptionViewModel> ParticipantsListe = new List<ViewModels.InscriptionViewModel>();

            foreach (var participant in listElement)
            {
                ViewModels.InscriptionViewModel vm = new ViewModels.InscriptionViewModel() 
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
        /// <summary>
        /// Les sites liés à des modules programmés 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetSitePlanning()
        {

            var GetListe = SitePlanningService.GetAll(); 
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// Récuperation des données des sites concernés pour les inscriptions 
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.InscriptionViewModel> GetListeSitePlannings(List<Infrastructure.DB.SiteModule> listElement)
        {
            List<ViewModels.InscriptionViewModel> Liste = new List<ViewModels.InscriptionViewModel>();

            foreach (var itemList in listElement)
            {
                ViewModels.InscriptionViewModel vm = new ViewModels.InscriptionViewModel()
                {
                    IdSiteModule = itemList.IdSiteModule,
                    IdSite = itemList.IdSite,
                    NomSite = SitePlanningService.GetNomSite(itemList.IdSite),
                    IdModule = itemList.IdModule,
                    NomModule = SitePlanningService.GetNomModule(itemList.IdModule),
                    DateDebutModule = itemList.DateDebutModule,
                    DateFinModule = itemList.DateFinModule,
                    IdFormateurModule = itemList.IdFormateurModule,
                    NomFormateur = SitePlanningService.GetNomFormateur(itemList.IdFormateurModule)

                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
