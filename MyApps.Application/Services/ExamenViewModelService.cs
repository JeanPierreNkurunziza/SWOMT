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
    }
}
