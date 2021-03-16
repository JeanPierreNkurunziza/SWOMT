using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.Services
{
    public class FormateurModuleViewModelService
    {
        /// <summary>
        /// méthode pour mapper les modules formateur les données d'affichage
        /// </summary>
        /// <param name="listeElement"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormateursModules(List<Infrastructure.DB.FormateurModule> listeElement)
        {
            List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
          
            foreach (var itemList in listeElement)
            {
                ViewModels.FormateurModuleViewModel vm = new ViewModels.FormateurModuleViewModel()
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
        /// <summary>
        /// méthode pour afficher la liste des formateur
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormatuerModules()
        {
           
            var GetListe = FormateurModuleService.GetAll();


            return GetFormateursModules(GetListe);
        }
        /// <summary>
        /// l'affichage de liste des modules par formateurs
        /// </summary>
        /// <param name="IdFormateur"></param>
        /// <returns></returns>

        public static List<ViewModels.FormateurModuleViewModel> GetModulesPerFormateur(int IdFormateur) 
        {
           // List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
            var GetListe = FormateurModuleService.GetListModulesPerFormateur(IdFormateur);
            return GetFormateursModules(GetListe);
        }
        /// <summary>
        /// méthode pour afficher les formateur par module 
        /// </summary>
        /// <param name="IdModule"></param>
        /// <returns></returns>
        public static List<ViewModels.FormateurModuleViewModel> GetFormateurPerModule(int IdModule) 
        {
            //List<ViewModels.FormateurModuleViewModel> Liste = new List<ViewModels.FormateurModuleViewModel>();
            var GetListe = FormateurModuleService.GetListFormateurPerModules(IdModule);
            return GetFormateursModules(GetListe);
        }
    }
}
