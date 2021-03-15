using MyApps.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace MyApps.Application.Services
{
   public class ExamenViewModelService
    {
        /// <summary>
        /// méthode pour afficher une liste des examens dans la vue concernée
        /// </summary>
        /// <returns></returns>
        public static List<ViewModels.ExamenViewModel> GetExamens()
        {
            List<ViewModels.ExamenViewModel> Liste = new List<ViewModels.ExamenViewModel>();
            var GetListe = ExamenService.GetAll();
            foreach (var itemList in GetListe)
            {
                ViewModels.ExamenViewModel vm = new ViewModels.ExamenViewModel()
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
        /// méthode pour permettre un utilisateur de faire une recherche sur les modules
        /// </summary>
        /// <param name="SearchNomModule"></param>
        /// <returns></returns>
        public static List<ViewModels.ExamenViewModel> SearchByNameModule( string SearchNomModule)
        {
            List<ViewModels.ExamenViewModel> Liste = new List<ViewModels.ExamenViewModel>();
            //var GetListe = ExamenViewModelService.GetExamens();
            var assets = from s in ExamenViewModelService.GetExamens()
                         select s;
            if (!String.IsNullOrEmpty(SearchNomModule))
            {
                assets = assets.Where(s => s.NomModule.ToUpper().Contains(SearchNomModule.ToUpper()) || s.NomFormateur.ToUpper().Contains(SearchNomModule.ToUpper()));
            }

            return assets.ToList();
            
        }
        /// <summary>
        ///  méthode pour permettre un utilisateur de faire une recherche sur les formateurs
        /// </summary>
        /// <param name="SearchNomFormateur"></param>
        /// <returns></returns>
        public static List<ViewModels.ExamenViewModel> SearchByNameNomFormateur(string SearchNomFormateur)
        {
            List<ViewModels.ExamenViewModel> Liste = new List<ViewModels.ExamenViewModel>();
            //var GetListe = ExamenViewModelService.GetExamens();
            var assets = from s in ExamenViewModelService.GetExamens()
                         select s;
            if (!String.IsNullOrEmpty(SearchNomFormateur)) 
            {
                assets = assets.Where(s => s.NomFormateur.ToUpper().Contains(SearchNomFormateur.ToUpper()));
            }

            return assets.ToList();

        }

    }
}
