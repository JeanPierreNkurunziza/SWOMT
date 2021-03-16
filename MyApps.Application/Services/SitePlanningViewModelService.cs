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
        public static List<ViewModels.SitePlanningViewModel> GetSitePlanning()
        {
           
            var GetListe = SitePlanningService.GetAll();
            return GetListeSitePlannings(GetListe);
        }

        public static List<ViewModels.SitePlanningViewModel> GetModulesPerSite(int IdSite)
        {
           
            var GetListe = SitePlanningService.GetListModulesPerSite(IdSite);
            return GetListeSitePlannings(GetListe);
        }

        public static List<ViewModels.SitePlanningViewModel> GetSitePerModule(int IdModule)
        {
            
            var GetListe = SitePlanningService.GetListSitePerModule(IdModule);
            return GetListeSitePlannings(GetListe);
        }
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
        public static List<ViewModels.SitePlanningViewModel> GetListModuleEncours()
        {
            
            var GetListe = SitePlanningService.GetListModuleEncours();
            return GetListeSitePlannings(GetListe);
        }
        public static List<ViewModels.SitePlanningViewModel> GetListeModulesPlanifiesProchainement()
        {
          
            var GetListe = SitePlanningService.GetListModulePlanifies();
            
            return GetListeSitePlannings(GetListe);
        }
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

    } 
    
}
