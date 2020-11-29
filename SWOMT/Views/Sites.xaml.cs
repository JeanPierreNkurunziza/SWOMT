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

        string roleNameSelected;
        string nomUserSelected;
        /// <summary>
        /// le constructeur de la classe Sites
        /// </summary>
        /// <param name="roleName"></param>
        public Sites(string roleName, string username) 
        {
            InitializeComponent();
            listeEncours = MyApps.Application.Services.SitePlanningViewModelService.GetListModuleEncours();
            listePlanifies = MyApps.Application.Services.SitePlanningViewModelService.GetListeModulesPlanifiesProchainement();
            listeFormation = MyApps.Application.Services.PlanningsFormation.GetPlanningFormationThisYearAndNextYear();
            listeEvenement=MyApps.Application.Services.EvenementViewModelService.GetCurrentEvenementsWithin90Days();
           
            PopulateAndBindListEncours(listeEncours);
            PopulateAndBindFormationsPlanifié(listeFormation);
            PopulateAndBindListPlanifié(listePlanifies);
            PopulateAndBindEvenement(listeEvenement);

            roleNameSelected = (string)roleName;
            nomUserSelected = username;

            if ((string)roleName != "Admin")
            {
                //BoutonInscription.IsEnabled=false;
                supprimer.Visibility = Visibility.Hidden; 
            }
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
        /// <summary>
        /// la méthode qui permettre de sélectionner un élément dans la liste des événements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Evenement_SelectinChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListEvenement.SelectedItem is MyApps.Application.ViewModels.EvenementViewModel donnee)
            {
                //pour eviter le système de cracher quant on selectionne le vide
                if (donnee.IdEvenement == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }
              
            }
        }
        /// <summary>
        /// Méthode qui permet de supprimer un élément sélectionner dans la liste des événements 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (ListEvenement.SelectedItem is MyApps.Application.ViewModels.EvenementViewModel donnee)
            {
                //pour eviter le système de cracher quant on selectionne le vide
                if (donnee.IdEvenement == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }

                MyApps.Domain.Service.EvenementService.Delete(donnee.IdEvenement);
                listeEvenement.Clear();
                listeEvenement = MyApps.Application.Services.EvenementViewModelService.GetEvenements();
                PopulateAndBindEvenement(listeEvenement);
            }
        }

        private void ValiderInscription_Click(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility;
            UpDatePassord.Visibility = Visibility;
        }

        private void UpDatePassword_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur element = new Utilisateur();
            if (password.Password == "")
            {
                MessageBox.Show("Veuillez entrer le mot de passe SVP!");
                password.Visibility = Visibility.Hidden;
                UpDatePassord.Visibility = Visibility.Hidden;
                return;
            }


            
            element.UserName = nomUserSelected;
            element.MotDePasse = password.Password;
            element.UserRole = roleNameSelected;
            element.IdUser = MyApps.Domain.Service.UserService.GetUtilisateurUserId(nomUserSelected);
            MyApps.Domain.Service.UserService.MetàJourUser(element.IdUser,element.UserName, element.MotDePasse, element.UserRole);
            password.Password = "";
            password.Visibility = Visibility.Hidden;
            UpDatePassord.Visibility = Visibility.Hidden;

        }
    }
}
