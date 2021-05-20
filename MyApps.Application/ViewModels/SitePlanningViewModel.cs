using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
   public class SitePlanningViewModel
    {
        public int IdSiteModule { get; set; }
        public int IdSite { get; set; }
        public string NomSite { get; set; }
        public int IdModule { get; set; }
        public string NomModule { get; set; }  
        public DateTime? DateDebutModule { get; set; }
        public DateTime? DateFinModule { get; set; }
        public Nullable<int> IdFormateurModule { get; set; }
        public string NomFormateur { get; set; }

        //Objet modules
       // public int IdModule { get; set; }
        public Nullable<int> IdFormation { get; set; }
       // public string NomModule { get; set; }
        public string NomFormation { get; set; }
        public int CreditModule { get; set; }
        public int NombrePrévu { get; set; }

        // objet site
      //  public int IdSite { get; set; }
      //  public string NomSite { get; set; }
        public string AdresseSite { get; set; }

        // objet Formatuer Module 
      //  public int IdFormateurModule { get; set; }
        public int IdFormateur { get; set; }
      //  public int IdModule { get; set; }
     //   public string NomModule { get; set; }
      //  public string NomFormateur { get; set; }
        public DateTime? VersionModule { get; set; }
    }
}
