using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class CertificatViewModel
    {
        public int IdCertificat { get; set; }
        public int IdParticipant { get; set; }
        public string NomParticipant { get; set; }  
        public DateTime DateDelivrance { get; set; } 
    }
}
