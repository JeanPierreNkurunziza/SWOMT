using System;

namespace MyApps.Application.ViewModels
{
    /// <summary>
    /// View Models pour les présences 
    /// </summary>
   public class PresenceViewModel
    {
        public int IdPresence { get; set; }
        public int IdParticipant { get; set; }
        public int IdSiteModule { get; set; } 
        public string NomModule { get; set; }
        public string NomParticipant { get; set; }  
        public DateTime DateHeureDePresence { get; set; }
        public bool EstPresent { get; set; }
        public DateTime? DateDebutModule { get; set; }
        public DateTime? DateDeFinModule { get; set; }
        public Nullable<long> IdNational { get; set; }
        public int IdModule { get; set; }
        public Nullable<int> IdFormation { get; set; }
        public string NomFormation { get; set; }
        public int CreditModule { get; set; }
        public int NombrePrévu { get; set; }
        public int IdSite { get; set; }
        public string NomSite { get; set; }
        public DateTime? DateFinModule { get; set; }
        public Nullable<int> IdFormateurModule { get; set; }
        public string NomFormateur { get; set; }
        public int IdModuleInscription { get; set; }
        public DateTime DateInscription { get; set; }
        public bool EstSurListeAttente { get; set; }
        public int TotalParticipantPerModule { get; set; }
        public bool CheckBoxEstPresent { get; set; }
        public DateTime DateNaissance { get; set; }
        public string TelParticipant { get; set; }
        public string EmailParticipant { get; set; }
        public string SecteurParticipant { get; set; }
        public string DistrictParticipant { get; set; }
        public DateTime DateEncodage { get; set; }
    }
}
