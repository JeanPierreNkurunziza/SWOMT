using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWOMT.Views
{
    /// <summary>
    /// Logique d'interaction pour Sites.xaml
    /// </summary>
    public partial class Sites : Page
    {
        List<MyApps.Application.ViewModels.SitePlanningViewModel> listeEncours = new List<MyApps.Application.ViewModels.SitePlanningViewModel>();
        List<MyApps.Application.ViewModels.SitePlanningViewModel> listePlanifies = new List<MyApps.Application.ViewModels.SitePlanningViewModel>();
        List<MyApps.Application.ViewModels.PlanningViewModel> listeFormation = new List<MyApps.Application.ViewModels.PlanningViewModel>();
        List<MyApps.Application.ViewModels.EvenementViewModel> listeEvenement = new List<MyApps.Application.ViewModels.EvenementViewModel>();


        public Sites() 
        {
            InitializeComponent();
            listeEncours = MyApps.Application.Services.SitePlanningViewModelService.GetListModuleEncours();
            listePlanifies = MyApps.Application.Services.SitePlanningViewModelService.GetListeModulesPlanifiesProchainement();
            listeFormation = MyApps.Application.Services.PlanningsFormation.GetPlanningFormation();
            listeEvenement=MyApps.Application.Services.EvenementViewModelService.GetEvenements();
            PopulateAndBindListEncours(listeEncours);
            PopulateAndBindFormationsPlanifié(listeFormation);
            PopulateAndBindListPlanifié(listePlanifies);
            PopulateAndBindEvenement(listeEvenement); 
        }



        /// <summary>
        /// biding la liste de modules encours 
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindListEncours(List<MyApps.Application.ViewModels.SitePlanningViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListModuleEncours.DataContext = listeItems;
        }
        /// <summary>
        /// binding la liste de modules planifiés  
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindListPlanifié(List<MyApps.Application.ViewModels.SitePlanningViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListModulePlanifies.DataContext = listeItems;
        }

        /// <summary>
        /// binding la liste des formations planifiées 
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindFormationsPlanifié(List<MyApps.Application.ViewModels.PlanningViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListFormation.DataContext = listeItems;
        }

        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateAndBindEvenement(List<MyApps.Application.ViewModels.EvenementViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListEvenement.DataContext = listeItems;
        }
    }
}
