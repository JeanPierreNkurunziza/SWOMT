using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
  public  class ExamenViewModel
    {
        public int IdExamen { get; set; }
        public int IdSiteModule { get; set; } 
        public string NomModule { get; set; } 
        public DateTime DateExamen { get; set; }  
    }
}
