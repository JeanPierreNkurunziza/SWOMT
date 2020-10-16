using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class InscriptionViewModel
    {
        public int IdModuleInscription { get; set; }
        public int IdSiteModule { get; set; } 
        public int IdParticipant { get; set; }
        public string NomModule { get; set; }
        public string NomParticipant { get; set; }
        public Nullable<long> IdNational { get; set; }
        public DateTime DateInscription { get; set; }
        public bool EstSurListeAttente { get; set; } 
        public int TotalParticipantPerModule { get; set; }
        
    }
}
