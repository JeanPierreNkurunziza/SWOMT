using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class SitePlanningViewModelService
    {
        /// <summary>
        /// méthode pour récuperer les sites
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetSites()
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
            var GetListe = SiteService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.SitePlanningViewModel vm = new ViewModels.SitePlanningViewModel()
                {
                    IdSite = itemList.IdSite,
                    NomSite = itemList.NomSite,
                    AdresseSite = itemList.AdresseSite,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// Récuperation les données pour la planification des sites 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetSitePlanning()
        {
           
            var GetListe = SitePlanningService.GetAll();
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// Récuperer les modules par site
        /// </summary>
        /// <param name="IdSite"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetModulesPerSite(int IdSite)
        {
           
            var GetListe = SitePlanningService.GetListModulesPerSite(IdSite);
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// on récupere les sites par module
        /// </summary>
        /// <param name="IdModule"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetSitePerModule(int IdModule)
        {
            
            var GetListe = SitePlanningService.GetListSitePerModule(IdModule);
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// Méthode pour faire une recherche selon le nom de site, de module et de formateur
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> SearchMethodByName(string searchString)
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
            var GetListe = GetSitePlanning();
            var assets = from s in GetListe
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(s => s.NomModule.ToUpper().Contains(searchString.ToUpper()) || s.DateDebutModule.ToString().Contains(searchString.ToString())
                                            || s.NomFormateur.ToUpper().Contains(searchString.ToUpper()));
            }

            return assets.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> AfficherModulePerFormateur(string searchString)
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
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
        /// Récupere les modules encours de formation
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetListModuleEncours()
        {
            
            var GetListe = SitePlanningService.GetListModuleEncours();
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// Récuperer les modules planifiés dans l'avenir
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetListeModulesPlanifiesProchainement()
        {
          
            var GetListe = SitePlanningService.GetListModulePlanifies();
            
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// Récuperer les données des sites et modules dans la DB
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetListeSitePlannings(List<Infrastructure.DB.SiteModule> listElement)
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
           
            foreach (var itemList in listElement)
            {
                ViewModels.SitePlanningViewModel vm = new ViewModels.SitePlanningViewModel()
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
        /// afficher les modules
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetModules() 
        {
           
            var GetListe = ModuleService.GetAll();
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// afficher selon le résultat de la recherche
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> SearchModuleByName(string searchString)
        {
            var GetListe = ModuleService.SearchMethodByName(searchString);
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// afficher la liste des modules per formateur 
        /// </summary>
        /// <param name="IdFormation"></param>
        /// <returns></returns>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetModulesList(List<Infrastructure.DB.Module> listeElement)
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();
            // var GetListe = ModuleService.GetAll();
            foreach (var itemList in listeElement)
            {
                ViewModels.SitePlanningViewModel vm = new ViewModels.SitePlanningViewModel() 
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
        /// méthode pour mapper les modules formateur les données d'affichage
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetFormateurPerModule(int IdModule)
        {
            //List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
            var GetListe = FormateurModuleService.GetListFormateurPerModules(IdModule);
            return GetFormateursModules(GetListe);
        }
        /// <summary>
        /// Récuperer les données des formateur et modules concernés 
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.SitePlanningViewModel> GetFormateursModules(List<Infrastructure.DB.FormateurModule> listeElement)
        {
            List<ViewModels.SitePlanningViewModel> Liste = new List<ViewModels.SitePlanningViewModel>();

            foreach (var itemList in listeElement)
            {
                ViewModels.SitePlanningViewModel vm = new ViewModels.SitePlanningViewModel()
                {
                    IdFormateurModule = itemList.IdFormateurModule,
                    IdFormateur = itemList.IdFormateur,
                    IdModule = itemList.IdModule,
                    NomModule = FormateurModuleService.GetNomModule(itemList.IdModule),
                    NomFormateur = FormateurModuleService.GetNomFormateur(itemList.IdFormateur),
                    VersionModule = itemList.VersionModule,

                };
                Liste.Add(vm);
            }

            return Liste;
        }
    } 
    
}
