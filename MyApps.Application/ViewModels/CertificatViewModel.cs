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
        //participant model
      //  public int IdParticipant { get; set; }
      //  public string NomParticipant { get; set; }
        public DateTime DateNaissance { get; set; }
        public Nullable<long> IdNational { get; set; }
        public string TelParticipant { get; set; }
        public string EmailParticipant { get; set; }
        public string SecteurParticipant { get; set; }
        public string DistrictParticipant { get; set; }
        public DateTime DateEncodage { get; set; }
        // resultat models
        public int IdResultat { get; set; }
        public int IdModuleInscription { get; set; }
        public int IdExamen { get; set; }
        public string NomModule { get; set; }
      //  public string NomParticipant { get; set; }
        public int? Points { get; set; }
        public bool EstPresent { get; set; }
        public bool ParticipantRéussi { get; set; }
    }
}
