using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class PresenceViewModel
    {
        public int IdPresence { get; set; }
        public int IdParticipant { get; set; }
        public int IdSiteModule { get; set; } 
        public string NomModule { get; set; }
        public string NomParticipant { get; set; }  
        public DateTime DateHeureDePresence { get; set; }
        public bool EstPresent { get; set; }
        
    }
}
