
using MyApps.Domain.Service;
using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class PlanningsFormation
    {
        /// <summary>
        /// récuperer le planning des formations
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PlanningViewModel> GetPlanningFormation()
        {
            List<ViewModels.PlanningViewModel> Liste = new List<ViewModels.PlanningViewModel>();
            var GetListe = PlanningService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.PlanningViewModel vm = new ViewModels.PlanningViewModel()
                {
                    IdPlanning = itemList.IdPlanning,
                    IdFormation = itemList.IdFormation,
                    NomFormation = PlanningService.GetNomFormation(itemList.IdFormation),
                    DateFormation=itemList.DateFormation,
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        /// <summary>
        /// récuperer les planning de formation pour l'année encours 
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PlanningViewModel> GetPlanningFormationThisYearAndNextYear() 
        {
            List<ViewModels.PlanningViewModel> Liste = new List<ViewModels.PlanningViewModel>();
            var GetListe = PlanningService.GetListOfPlanningFormationWithinAcademicYear();
            foreach (var itemList in GetListe)
            {
                ViewModels.PlanningViewModel vm = new ViewModels.PlanningViewModel()
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
        /// Récuperer les données des modules concernés pour la planification de formation 
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.PlanningViewModel> GetModulesList(List<Infrastructure.DB.Module> listeElement)
        {
            List<ViewModels.PlanningViewModel> Liste = new List<ViewModels.PlanningViewModel>();
            foreach (var itemList in listeElement)
            {
                ViewModels.PlanningViewModel vm = new ViewModels.PlanningViewModel()
                {
                    IdModule = itemList.IdModule,
                    IdFormation = (int)itemList.IdFormation,
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
        public static List<ViewModels.PlanningViewModel> GetModules()
        {
            //List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.GetAll();
            return GetModulesList(GetListe);
        }
        public static List<ViewModels.PlanningViewModel> GetModulesPerFormation(int IdFormation)
        {
            //List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.GetModulePerFormation(IdFormation);
            return GetModulesList(GetListe);
        }
        public static List<ViewModels.PlanningViewModel> SearchModuleByName(string searchString)
        {
            // List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.SearchMethodByName(searchString);
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// mapping datas of the view model and view
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.PlanningViewModel> GetFormations()
        {
            List<ViewModels.PlanningViewModel> FormationListe = new List<ViewModels.PlanningViewModel>();
            var ListeFormation = FormationService.GetAll();
            foreach (var formation in ListeFormation)
            {
                ViewModels.PlanningViewModel vm = new ViewModels.PlanningViewModel()
                {
                    IdFormation = formation.IdFormation,
                    NomFormation = formation.NomFormation,
                    Description = formation.Description,

                };
                FormationListe.Add(vm);
            }

            return FormationListe;
        }
        /// <summary>
        /// afficher les données selon la rechercher
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.PlanningViewModel> GetSearchByName(string searchString)
        {
            List<ViewModels.PlanningViewModel> FormationListe = new List<ViewModels.PlanningViewModel>();
            var ListeFormation = FormationService.SearchMethodByName(searchString);  // appel d'un méthode de rechercher par nom 
            foreach (var formation in ListeFormation)
            {
                ViewModels.PlanningViewModel vm = new ViewModels.PlanningViewModel()
                {
                    IdFormation = formation.IdFormation,
                    NomFormation = formation.NomFormation,
                    Description = formation.Description,

                };
                FormationListe.Add(vm);
            }

            return FormationListe;
        }

    }
}
