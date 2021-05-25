using System;


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

