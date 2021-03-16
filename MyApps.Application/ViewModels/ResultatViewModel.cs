using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class ResultatViewModel
    {
        public int IdResultat { get; set; }
        public int IdModuleInscription { get; set; }
        public int IdExamen { get; set; }
        public string NomModule { get; set; }
        public string NomParticipant { get; set; }  
        public int? Points { get; set; }   
        public bool EstPresent { get ; set; }
        public bool ParticipantRéussi { get; set; }  

       
    }
}
