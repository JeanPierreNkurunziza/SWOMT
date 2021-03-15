
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

    }
}
