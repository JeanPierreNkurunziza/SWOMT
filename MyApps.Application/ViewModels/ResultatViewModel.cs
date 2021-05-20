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

        // inscription models
       // public int IdModuleInscription { get; set; }
        public int IdSiteModule { get; set; }
        public int IdParticipant { get; set; }
      //  public string NomModule { get; set; }
       // public string NomParticipant { get; set; }
        public Nullable<long> IdNational { get; set; }
        public DateTime DateInscription { get; set; }
        public bool EstSurListeAttente { get; set; }
        public int TotalParticipantPerModule { get; set; }
        public bool CheckBoxEstPresent { get; set; }

        // site planning models
       // public int IdSiteModule { get; set; }
        public int IdSite { get; set; }
        public string NomSite { get; set; }
        public int IdModule { get; set; }
       // public string NomModule { get; set; }
        public DateTime? DateDebutModule { get; set; }
        public DateTime? DateFinModule { get; set; }
        public Nullable<int> IdFormateurModule { get; set; }
        public string NomFormateur { get; set; }

        // examens model
        //public int IdExamen { get; set; }
       // public int IdSiteModule { get; set; }
       // public string NomModule { get; set; }
        public DateTime DateExamen { get; set; }

        // public string NomFormateur { get; set; }

        //formateur model
        public int IdFormateur { get; set; }
       // public string NomFormateur { get; set; }
        public string Domaine { get; set; }

        public string TelFormateur { get; set; }
        public string EmailFormateur { get; set; }

        public DateTime DateEncodage { get; set; }
    }
}
