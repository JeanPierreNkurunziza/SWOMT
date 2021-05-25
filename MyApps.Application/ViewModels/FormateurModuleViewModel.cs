using System;


namespace MyApps.Application.ViewModels
{
    /// <summary>
    /// View models pour gérer les formateur et leur modules respectives
    /// </summary>
   public class FormateurModuleViewModel
    {

        public int IdFormateurModule { get; set; }  
        public int IdFormateur { get; set; }
        public int IdModule { get; set; }
        public string NomModule { get; set; }
        public string NomFormateur { get; set; } 
        public DateTime? VersionModule { get; set; }      
        public Nullable<int> IdFormation { get; set; }     
        public string NomFormation { get; set; }
        public int CreditModule { get; set; }
        public int NombrePrévu { get; set; }
        public string Domaine { get; set; }
        public string TelFormateur { get; set; }
        public string EmailFormateur { get; set; }
        public DateTime DateEncodage { get; set; } 
        public string Description { get; set; }
    }
}
