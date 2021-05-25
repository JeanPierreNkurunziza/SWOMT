using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApps.Application.ViewModels
{
    public class EvenementViewModel
    {
        //ViewModel pour la publication des évenements 
        public int IdEvenement { get; set; }
        public int IdFormateur { get; set; }
        public string Evenement1 { get; set; }
        public Nullable<DateTime> DateOfEvent { get; set; }
        public string NomFormateur { get; set; } 
    }
}
