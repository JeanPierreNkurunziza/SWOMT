
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
       
    }
}
