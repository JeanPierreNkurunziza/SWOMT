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
                };
                Liste.Add(vm);
            }

            return Liste;
        }
        public static List<ViewModels.ExamenViewModel> SearchByNameModule( string SearchNomModule)
        {
            List<ViewModels.ExamenViewModel> Liste = new List<ViewModels.ExamenViewModel>();
            //var GetListe = ExamenViewModelService.GetExamens();
            var assets = from s in ExamenViewModelService.GetExamens()
                         select s;
            if (!String.IsNullOrEmpty(SearchNomModule))
            {
                assets = assets.Where(s => s.NomModule.ToUpper().Contains(SearchNomModule.ToUpper()));
            }

            return assets.ToList();
            
        }
    }
}
