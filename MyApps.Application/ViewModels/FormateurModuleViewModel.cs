using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class FormateurModuleViewModel
    {
        public int IdFormateur { get; set; }
        public int IdModule { get; set; }
        public string NomModule { get; set; }
        public string NomFormateur { get; set; } 
        public DateTime? VersionModule { get; set; }  
    }
}
