using System;


namespace MyApps.Application.ViewModels
{
  public  class ExamenViewModel
    {
        public int IdExamen { get; set; }
        public int IdSiteModule { get; set; } 
        public string NomModule { get; set; } 
        public DateTime DateExamen { get; set; }
        public string NomFormateur { get; set; }  
    }
}
