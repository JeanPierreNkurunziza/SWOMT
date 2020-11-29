using MyApps.Infrastructure.DB;
using SWOMT.Views;
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
using System.Windows.Shapes;

namespace SWOMT
{
    /// <summary>
    /// Logique d'interaction pour TableauDeBord.xaml
    /// </summary>
    public partial class TableauDeBord : Window
    {
        List<MyApps.Application.ViewModels.UserViewModel> liste = new List<MyApps.Application.ViewModels.UserViewModel>();
        //string nomUser;
        public TableauDeBord(string UserName, string UserRole) 
        {
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
            InitializeComponent();
            // this.WindowState = WindowState.Maximized; 
            Main.Content = new Sites(UserRole.ToString(), UserName.ToString());
           
            TextBoxUserName.Content = UserName.ToString();
            TextBoxUserRole.Content = UserRole.ToString();

           
            if ((string)TextBoxUserRole.Content != "Admin")
            {
              
                BoutonUsers.Visibility = Visibility.Hidden;
              
            }
            if ((string)TextBoxUserRole.Content != "Secrétaire" && (string)TextBoxUserRole.Content != "Admin")
            {
                
                BoutonInscription.Visibility=Visibility.Hidden;
                BoutonSitePlanning.Visibility=Visibility.Hidden;               
            }
            if ((string)TextBoxUserRole.Content == "Personnel")
            {

                BoutonPresence.Visibility = Visibility.Hidden;
                BoutonResultats.Visibility = Visibility.Hidden;
            }

        }

        /// <summary>
        /// pour gerer les participant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void GestionParticipants(object sender, RoutedEventArgs e)
        //{
        //    Main.Content = new Participants();
        //}


        //private void GestionFormation(object sender, RoutedEventArgs e)
        //{

        //   
        //    
        //        Main.Content = new Formations(); 

        //    
                      
        //}

        private void GestionSite(object sender, RoutedEventArgs e)
        {
           
                Main.Content = new Sites(TextBoxUserRole.Content.ToString(), TextBoxUserName.Content.ToString());
                         
        }
        /// <summary>
        /// Bouton pour gérer les siets et leurs plannings de modules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlanningFormation(object sender, RoutedEventArgs e)
        {
            Main.Content = new PlanningFormations(TextBoxUserRole.Content.ToString()); 
        }

        //private void GestionFormateur(object sender, RoutedEventArgs e)
        //{
        //    Main.Content = new Formateurs();
        //}
        /// <summary>
        /// Bouton pour gérer les participants réussis et leurs certificats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestionCertificat(object sender, RoutedEventArgs e)
        {
            Main.Content = new Certificats(TextBoxUserRole.Content.ToString());
        }

        //private void GestionExamen(object sender, RoutedEventArgs e)
        //{
        //    Main.Content = new Exames();
        //}

        //private void GestionModule(object sender, RoutedEventArgs e)
        //{
        //    Main.Content = new Modules();
        //}
        /// <summary>
        /// button pour declencher la gestion des site et modueles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestionSiteModule(object sender, RoutedEventArgs e)
        {
            Main.Content = new SitePlannings();
        }

        /// <summary>
        /// Bouton pour gérer le résultats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestionRésultat(object sender, RoutedEventArgs e)
        {
            Main.Content = new Resultats(TextBoxUserName.Content.ToString(), TextBoxUserRole.Content.ToString()); 
        }

        /// <summary>
        /// Bouton pour gérer les inscriptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestionInscription(object sender, RoutedEventArgs e)
        {
            Main.Content = new Inscriptions();
        }
        /// <summary>
        /// Bouton pour gérer des présences
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestionDePresence(object sender, RoutedEventArgs e)
        {
            Main.Content = new Presences(TextBoxUserName.Content.ToString());
        }
        /// <summary>
        /// Bouton pour gérer les formateurs et leurs modules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestionFormateurModule(object sender, RoutedEventArgs e)
        {
            Main.Content = new FormateurModules(TextBoxUserRole.Content.ToString()); 
        }
        private void GestionUsers(object sender, RoutedEventArgs e)
        {
            Main.Content = new Users();
        }
    }
}
