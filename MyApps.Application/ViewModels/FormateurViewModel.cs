using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class FormateurViewModel
    {
        public int IdFormateur { get; set; }
        public string NomFormateur { get; set; }
        public string Domaine { get; set; }
       
        public string TelFormateur { get; set; }
        public string EmailFormateur { get; set; } 
       
        public DateTime DateEncodage { get; set; }
    }
}

