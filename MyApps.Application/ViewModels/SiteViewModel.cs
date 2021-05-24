using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
    public class SiteViewModel
    {
        public int IdSite { get; set; }
        public string NomSite { get; set; }
        public string AdresseSite { get; set; }
        public int IdSiteModule { get; set; }
        public int IdModule { get; set; }
        public string NomModule { get; set; }
        public DateTime? DateDebutModule { get; set; }
        public DateTime? DateFinModule { get; set; }
        public Nullable<int> IdFormateurModule { get; set; }
        public string NomFormateur { get; set; }
        public int IdPlanning { get; set; }
        public int IdFormation { get; set; }
        public DateTime DateFormation { get; set; }
        public string NomFormation { get; set; }
        public int IdEvenement { get; set; }
        public int IdFormateur { get; set; }
        public string Evenement1 { get; set; }
        public Nullable<DateTime> DateOfEvent { get; set; }

    }
}
