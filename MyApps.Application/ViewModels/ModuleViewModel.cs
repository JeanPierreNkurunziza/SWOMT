using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class ModuleViewModel
    {
        public int IdModule { get; set; }
        public Nullable<int> IdFormation { get; set; } 
        public string NomModule { get; set; }
        public string NomFormation { get; set; } 
        public int CreditModule { get; set; }
        public int NombrePrévu { get; set; }  
    }
}
