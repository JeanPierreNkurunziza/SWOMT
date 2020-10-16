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
        public static List<ViewModels.ModuleViewModel> GetModules()
        {
            List<ViewModels.ModuleViewModel> Liste = new List<ViewModels.ModuleViewModel>();
            var GetListe = ModuleService.GetAll(); 
            foreach (var itemList in GetListe)
            {
                ViewModels.ModuleViewModel vm = new ViewModels.ModuleViewModel() 
                {
                    IdModule = itemList.IdModule,
                    IdFormation = itemList.IdFormation, 
                    NomModule= itemList.NomModule,
                    NomFormation = ModuleService.GetNomFormation(itemList.IdFormation), 
                    CreditModule = itemList.CreditModule,
                    NombrePrévu= itemList.NombrPrévu 
                };
                Liste.Add(vm);
            }

            return Liste;
        }
    }
}
