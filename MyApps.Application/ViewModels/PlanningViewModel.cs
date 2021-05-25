using System;


namespace MyApps.Application.ViewModels
{
    /// <summary>
    /// planning view model
    /// </summary>
   public class PlanningViewModel
    {
        public int IdPlanning { get; set; }
        public int IdFormation { get; set; }
        public DateTime DateFormation { get; set; }
        public string NomFormation { get; set; }
        public int IdModule { get; set; }
        public string NomModule { get; set; }
        public int CreditModule { get; set; }
        public int NombrePrévu { get; set; }
        public string Description { get; set; }
    }
}
