using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class PlanningViewModel
    {
        public int IdPlanning { get; set; }
        public int IdFormation { get; set; }
        public DateTime DateFormation { get; set; }
        public string NomFormation { get; set; }  

    }
}
