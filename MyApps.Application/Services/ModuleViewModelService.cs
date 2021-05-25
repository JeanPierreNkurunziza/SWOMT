using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
   public class ModuleViewModelService 
    {
        /// <summary>
        /// récuperer les modules
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.ModuleViewModel> GetModulesList(List<Infrastructure.DB.Module> listeElement)   
        {
            List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
           // var GetListe = ModuleService.GetAll();
            foreach (var itemList in listeElement)
            {
                ViewModels.ModuleViewModel vm = new ViewModels.ModuleViewModel()
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
        public static List<ViewModels.ModuleViewModel> GetModules()
        {
            //List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.GetAll();
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// afficher selon le résultat de la recherche
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public static List<ViewModels.ModuleViewModel> SearchModuleByName(string searchString)
        {
           // List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.SearchMethodByName(searchString);
            return GetModulesList(GetListe);
        }
        /// <summary>
        /// afficher la liste des modules per formateur 
        /// </summary>
        /// <param name="IdFormation"></param>
        /// <returns></returns>
        public static List<ViewModels.ModuleViewModel> GetModulesPerFormation(int IdFormation)
        {
            //List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.GetModulePerFormation(IdFormation);
            return GetModulesList(GetListe);
        }
    }
}
