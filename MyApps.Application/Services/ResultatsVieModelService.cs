using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class ResultatsVieModelService
    {
        /// <summary>
        /// récuperer la liste des éléments à traiter
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetResultatsList(List<Infrastructure.DB.Resultat> listeElement)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            
            foreach (var itemList in listeElement)
            {
                ViewModels.ResultatViewModel vm = new ViewModels.ResultatViewModel()
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
        /// <summary>
        /// récuperer une liste des résultats 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetResultats() 
        {
            
            var GetListe = ResultatService.GetAll();
            
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer des participants réussis
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListParticipantRéussi(int idExamen) 
        {
           
            var GetListe = ResultatService.GetListParticipantRéussi(idExamen);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer des participant échouées
        /// </summary>
        /// <param name="idExamen"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListParticipantEchoué(int idExamen) 
        {
            
            var GetListe = ResultatService.GetListParticipantEchoué(idExamen);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une liste des modules per inscription
        /// </summary>
        /// <param name="idModuleInscription"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListModulesEchoué(int idModuleInscription)  
        {
           
            var GetListe = ResultatService.GetListModuleEchoué(idModuleInscription);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// récuperer une liste des modules réussis par inscription 
        /// </summary>
        /// <param name="idModuleInscription"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListModulesRéussis(int idModuleInscription) 
        {
            //List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ResultatService.GetListModuleRéussis(idModuleInscription);
            return GetResultatsList(GetListe);
        }
        /// <summary>
        /// Les participants par module
        /// </summary>
        /// <param name="IdSiteModule"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetParticipantPerModule(int IdSiteModule)
        {

            var GetListe = InscriptionService.GetListParticipantPerModule(IdSiteModule);

            return GetListElement(GetListe);
        }
        /// <summary>
        /// Récuperer les données des modules concernés pour les inscriptions
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListElement(List<Infrastructure.DB.ModuleInscription> listeElement)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();

            foreach (var itemList in listeElement)
            {
                ViewModels.ResultatViewModel vm = new ViewModels.ResultatViewModel()
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
        /// les données sur la planification des sites
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetSitePlanning()
        {

            var GetListe = SitePlanningService.GetAll();
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// les modules par formateur
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> AfficherModulePerFormateur(string searchString)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = GetSitePlanning();
            var assets = from s in GetListe
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(s => s.NomFormateur.ToUpper().Contains(searchString.ToUpper()));
            }

            return assets.ToList();
        }
        /// <summary>
        /// Récuperer les données des sites et leurs modules dans la DB
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetListeSitePlannings(List<Infrastructure.DB.SiteModule> listElement)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();

            foreach (var itemList in listElement)
            {
                ViewModels.ResultatViewModel vm = new ViewModels.ResultatViewModel()
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
        /// Récuperer les données concernés pour les examens
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetExamens()
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            var GetListe = ExamenService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.ResultatViewModel vm = new ViewModels.ResultatViewModel()
                {
                    IdExamen = itemList.IdExamen,
                    IdSiteModule = itemList.IdSiteModule,
                    NomModule = ExamenService.GetNomModule(itemList.IdSiteModule),
                    DateExamen = itemList.DateExamen,
                    NomFormateur = MyApps.Domain.Service.ExamenService.GetNomFormateur(itemList.IdSiteModule)
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// Méthode pour faire une rechercher selon de formateur ou le nom de module
        /// </summary>
        /// <param name="SearchNomFormateur"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> SearchByNameNomFormateur(string SearchNomFormateur)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            //var GetListe = ExamenViewModelService.GetExamens();
            var assets = from s in ResultatsVieModelService.GetExamens()
                         select s;
            if (!String.IsNullOrEmpty(SearchNomFormateur))
            {
                assets = assets.Where(s => s.NomFormateur.ToUpper().Contains(SearchNomFormateur.ToUpper()));
            }

            return assets.ToList();

        }
        /// <summary>
        /// Méthode pour faire une rechercher dans la liste de module
        /// </summary>
        /// <param name="SearchNomModule"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> SearchByNameModule(string SearchNomModule)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();
            //var GetListe = ExamenViewModelService.GetExamens();
            var assets = from s in ResultatsVieModelService.GetExamens()
                         select s;
            if (!String.IsNullOrEmpty(SearchNomModule))
            {
                assets = assets.Where(s => s.NomModule.ToUpper().Contains(SearchNomModule.ToUpper()) || s.NomFormateur.ToUpper().Contains(SearchNomModule.ToUpper()));
            }

            return assets.ToList();

        }
        /// <summary>
        /// récuperer les formateurs
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetFormateurs()
        {

            var GetListe = FormateurService.GetAll();
            return GetFormateurListss(GetListe);
        }
        /// <summary>
        /// Récuperer les données des formateurs dans la DB
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.ResultatViewModel> GetFormateurListss(List<Infrastructure.DB.Formateur> listElement)
        {
            List<ViewModels.ResultatViewModel> Liste = new List<ViewModels.ResultatViewModel>();

            foreach (var itemList in listElement)
            {
                ViewModels.ResultatViewModel vm = new ViewModels.ResultatViewModel()
                {
                    IdFormateur = itemList.IdFormateur,
                    NomFormateur = itemList.NomFormateur,
                    Domaine = itemList.Domaine,
                    TelFormateur = itemList.TélFormateur,
                    EmailFormateur = itemList.EmailFormateur,
                    DateEncodage = itemList.DateEncodage,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
