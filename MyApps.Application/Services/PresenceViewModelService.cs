using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class PresenceViewModelService
    {
        /// <summary>
        /// Récuperer les données de presence 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetPresences()
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            var GetListe = PresenceService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel()
                {
                    IdPresence = itemList.IdPresence,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    DateHeureDePresence = itemList.DateHeureDePresence,
                    EstPresent = itemList.EstPresent,
                    IdNational = PresenceService.GetIdNational(itemList.IdParticipant)

                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// récuperer les participants present par module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetListParticipantPresentPerModule( int IdSiteModule)
        {
           
            var GetListe = PresenceService.GetListParticipantPresentPerModule(IdSiteModule);
            return GetListPresencess(GetListe);
        }
        /// <summary>
        /// Méthode pour récuperer les données des participants par module et qui n'ont pas réussis ce module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetListParticipantAbsentPerModule(int IdSiteModule)
        {
           
            var GetListe = PresenceService.GetListParticipantAbsentPerModule(IdSiteModule); 
           

            return GetListPresencess(GetListe);
        }
        /// <summary>
        /// afficher selon le résultat de la recherche
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> SearchMethodByName(string searchString)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            var GetListe = GetSitePlanning();
            var assets = from s in GetListe
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(s => s.NomModule.ToUpper().Contains(searchString.ToUpper()) || s.NomSite.ToUpper().Contains(searchString.ToUpper()) || s.DateHeureDePresence.ToString().Contains(searchString.ToString()));
            }

            return assets.ToList();
        }
        /// <summary>
        /// Récuperer les données des presences dans la DB
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetListPresencess(List<Infrastructure.DB.Presence> listElement)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
           
            foreach (var itemList in listElement)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel()
                {
                    IdPresence = itemList.IdPresence,
                    IdSiteModule = itemList.IdSiteModule,
                    IdParticipant = itemList.IdParticipant,
                    NomModule = InscriptionService.GetNomModule(itemList.IdSiteModule),
                    NomParticipant = InscriptionService.GetNomParticipant(itemList.IdParticipant),
                    DateHeureDePresence = itemList.DateHeureDePresence,
                    EstPresent = itemList.EstPresent,
                    DateDebutModule = PresenceService.GetDateDebut(itemList.IdSiteModule),
                    DateDeFinModule = PresenceService.GetDateFin(itemList.IdSiteModule),
                    IdNational = PresenceService.GetIdNational(itemList.IdParticipant)
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// Récuperer les données des modules concernés dans la DB
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetModulesList(List<Infrastructure.DB.Module> listeElement)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            // var GetListe = ModuleService.GetAll();
            foreach (var itemList in listeElement)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel()
                {
                    IdModule = itemList.IdModule,
                    IdFormation = itemList.IdFormation,
                    NomModule = itemList.NomModule,
                    NomFormation = ModuleService.GetNomFormation(itemList.IdFormation),
                    CreditModule = itemList.CreditModule,
                    NombrePrévu = itemList.NombrPrévu
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// afficher les modules
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetModules() 
        {
            //List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.GetAll();
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// chaque formateur passe en parametre on récupere ses modules
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> AfficherModulePerFormateur(string searchString)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();
            var GetListe = GetSitePlanning();
            var assets = from s in GetListe
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(s => s.NomFormateur.ToUpper().Contains(searchString.ToUpper()));
            }

            return assets.ToList();
        }
        public static List<ViewModels.PresenceViewModel> GetSitePlanning()
        {

            var GetListe = SitePlanningService.GetAll(); 
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// Récuperer les sites et leurs modules concernés pour les présences 
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetListeSitePlannings(List<Infrastructure.DB.SiteModule> listElement)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();

            foreach (var itemList in listElement)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel()
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
        /// <summary>
        /// Récuperer les inscriptions
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetInscriptions()
        {

            var GetListe = InscriptionService.GetAll();

            return GetListElement(GetListe);
        }
        /// <summary>
        /// Récuperer les participants par module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetParticipantPerModule(int IdSiteModule)
        {

            var GetListe = InscriptionService.GetListParticipantPerModule(IdSiteModule);

            return GetListElement(GetListe);
        }
        /// <summary>
        /// récuperer les données dans la DB concernant les inscriptions selon les modules
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetListElement(List<Infrastructure.DB.ModuleInscription> listeElement)
        {
            List<ViewModels.PresenceViewModel> Liste = new List<ViewModels.PresenceViewModel>();

            foreach (var itemList in listeElement)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel() 
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
        /// Récuperer les participants
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetParticipants()
        {

            var ListeParticipant = ParticipantService.GetAll();
            return GetParticipantsList(ListeParticipant);
        }
        /// <summary>
        /// les données des participants dans la DB
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.PresenceViewModel> GetParticipantsList(List<Infrastructure.DB.Participant> listElement)
        {
            List<ViewModels.PresenceViewModel> ParticipantsListe = new List<ViewModels.PresenceViewModel>();

            foreach (var participant in listElement)
            {
                ViewModels.PresenceViewModel vm = new ViewModels.PresenceViewModel()
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
    }
}
