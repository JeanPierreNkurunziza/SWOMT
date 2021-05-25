using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class SitesViewModelsServices
    {
        /// <summary>
        /// méthode pour récuperer les sites
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetSites()
        {
            List<ViewModels.SiteViewModel> Liste = new List<ViewModels.SiteViewModel>();
            var GetListe = SiteService.GetAll(); 
            foreach (var itemList in GetListe) 
            {
                ViewModels.SiteViewModel vm = new ViewModels.SiteViewModel()
                {
                    IdSite = itemList.IdSite,
                    NomSite=itemList.NomSite,
                    AdresseSite=itemList.AdresseSite,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// Récuperer les modules encours
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetListModuleEncours()
        {

            var GetListe = SitePlanningService.GetListModuleEncours();
            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// Les modules planifiés dans l'avenir
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetListeModulesPlanifiesProchainement()
        {

            var GetListe = SitePlanningService.GetListModulePlanifies();

            return GetListeSitePlannings(GetListe);
        }
        /// <summary>
        /// les données concernant la planification des sites dans la DB
        /// </summary>
        /// <param name="listElement"></param>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetListeSitePlannings(List<Infrastructure.DB.SiteModule> listElement)
        {
            List<ViewModels.SiteViewModel> Liste = new List<ViewModels.SiteViewModel>();
           
            foreach (var itemList in listElement)
            {
                ViewModels.SiteViewModel vm = new ViewModels.SiteViewModel()
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
        /// Récuperer les formations planifiés pour l'année encours 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetPlanningFormationThisYearAndNextYear()
        {
            List<ViewModels.SiteViewModel> Liste = new List<ViewModels.SiteViewModel>();
            var GetListe = PlanningService.GetListOfPlanningFormationWithinAcademicYear();
            foreach (var itemList in GetListe)
            {
                ViewModels.SiteViewModel vm = new ViewModels.SiteViewModel()
                {
                    IdPlanning = itemList.IdPlanning,
                    IdFormation = itemList.IdFormation,
                    NomFormation = PlanningService.GetNomFormation(itemList.IdFormation),
                    DateFormation = itemList.DateFormation,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// les évenements publiés par les formateurs 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetEvenements()
        {

            var GetListe = EvenementService.GetAll();
            return GetEvenements(GetListe);
        }

        /// <summary>
        /// affichage des évenements qui n'ont encore depassent 90 jours 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetCurrentEvenementsWithin90Days()
        {

            var GetListe = EvenementService.GetListOfCurrentEvenement();

            return GetEvenements(GetListe);
        }
        /// <summary>
        /// Récuperer les données des evenements publiés dans la DB
        /// </summary>
        /// <param name="eventlist"></param>
        /// <returns></returns>
        public static List<ViewModels.SiteViewModel> GetEvenements(List<Infrastructure.DB.Evenement> eventlist)
        {
            List<ViewModels.SiteViewModel> Liste = new List<ViewModels.SiteViewModel>();

            foreach (var itemList in eventlist)
            {
                ViewModels.SiteViewModel vm = new ViewModels.SiteViewModel()
                {
                    IdEvenement = itemList.IdEvenement,
                    IdFormateur = itemList.IdFormateur,
                    Evenement1 = itemList.Evenement1,
                    DateOfEvent = itemList.DateOfEVent,
                    NomFormateur = EvenementService.GetNomFormateur(itemList.IdFormateur)
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
