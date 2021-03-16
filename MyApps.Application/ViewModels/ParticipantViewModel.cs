using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class ParticipantViewModel
    {
        public int IdParticipant { get; set; }
        public string NomParticipant { get; set; }
        public DateTime DateNaissance { get; set; }
        public Nullable<long> IdNational { get; set; } 
        public string TelParticipant { get; set; }
        public string EmailParticipant { get; set; }
        public string SecteurParticipant { get; set; }
        public string DistrictParticipant { get; set; }
        public DateTime DateEncodage { get; set; } 
       
    }
}
