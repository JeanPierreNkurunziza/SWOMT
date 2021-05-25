using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace SWOMT.Views
{
    /// <summary>
    /// Logique d'interaction pour Sites.xaml
    /// </summary>
    public partial class Sites : Page
    {
        string roleNameSelected;
        string nomUserSelected;
        List<MyApps.Application.ViewModels.SiteViewModel> liste = new List<MyApps.Application.ViewModels.SiteViewModel>();
        /// <summary>
        /// le constructeur de la classe Sites
        /// </summary>
        /// <param name="roleName"></param>
        public Sites(string roleName, string username) 
        {
            InitializeComponent();
            liste = MyApps.Application.Services.SitesViewModelsServices.GetListModuleEncours();
            PopulateAndBindListEncours(liste);
            liste = MyApps.Application.Services.SitesViewModelsServices.GetListeModulesPlanifiesProchainement();
            PopulateAndBindListPlanifié(liste);
            liste = MyApps.Application.Services.SitesViewModelsServices.GetPlanningFormationThisYearAndNextYear();
            PopulateAndBindFormationsPlanifié(liste);
            liste =MyApps.Application.Services.SitesViewModelsServices.GetCurrentEvenementsWithin90Days();          
            PopulateAndBindEvenement(liste);

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
        private void PopulateAndBindListEncours(List<MyApps.Application.ViewModels.SiteViewModel> listeItems)
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
        private void PopulateAndBindListPlanifié(List<MyApps.Application.ViewModels.SiteViewModel> listeItems)
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
        private void PopulateAndBindFormationsPlanifié(List<MyApps.Application.ViewModels.SiteViewModel> listeItems)
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
        private void PopulateAndBindEvenement(List<MyApps.Application.ViewModels.SiteViewModel> listeItems)
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
                liste.Clear();
                liste = MyApps.Application.Services.SitesViewModelsServices.GetEvenements(); 
                PopulateAndBindEvenement(liste);
            }
        }
        /// <summary>
        /// activer les schamps de modification de mot de passe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderInscription_Click(object sender, RoutedEventArgs e)
        {
            Ancien.Visibility = Visibility;
            OldPassWord.Visibility = Visibility;
            ValiderOldPassWord.Visibility = Visibility;
            password.Password = "";
            OldPassWord.Password = "";

        }
        /// <summary>
        /// Mettre à jour le mot de passe en cas de besoin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpDatePassword_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur element = new Utilisateur();
           
            if (ValidatePassword(password.Password, out string ErrorMessage) != true)
            {
                MessageBox.Show(ErrorMessage);
                Ancien.Visibility = Visibility.Hidden;
                OldPassWord.Visibility = Visibility.Hidden;
                password.Visibility = Visibility.Hidden;
                UpDatePassord.Visibility = Visibility.Hidden;
                return;
            }
            


            element.UserName = nomUserSelected;
            element.MotDePasse =MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.CreateHash(password.Password);
            element.UserRole = roleNameSelected;
            element.IdUser = MyApps.Domain.Service.UserService.GetUtilisateurUserId(nomUserSelected);
            MyApps.Domain.Service.UserService.Update(element);
            password.Password = "";
            OldPassWord.Password = "";
            Ancien.Visibility = Visibility.Hidden;
            OldPassWord.Visibility = Visibility.Hidden;
            password.Visibility = Visibility.Hidden;
            UpDatePassord.Visibility = Visibility.Hidden;
            ValiderOldPassWord.Visibility = Visibility.Hidden;

        }
        /// <summary>
        /// Validation de mot de passe
        /// </summary>
        /// <param name="password"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Le mot de passe ne doit pas être vide");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une lettre minuscule ";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une lettre majuscule ";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe ne doit pas être inférieur à 8 ou supérieur à 15 caractères ";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une valeur numérique ";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins un caractère spécial ";
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// la vérificatino de l'ancien mot de passé avant de le modifier 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOldPassWord(object sender, RoutedEventArgs e)
        {
            string utilisateurValidé = MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.verifyCrypto(OldPassWord.Password, nomUserSelected);
            if (utilisateurValidé == null)
            {
                MessageBox.Show("Le nom d'utilisateur et le mot de passe ne correspondent pas SVP!");
                password.Visibility = Visibility.Hidden;
                UpDatePassord.Visibility = Visibility.Hidden;
                ValiderOldPassWord.Visibility = Visibility.Hidden;
                OldPassWord.Visibility = Visibility.Hidden;
                Ancien.Visibility = Visibility.Hidden;
                password.Password = "";
                OldPassWord.Password = "";
                return;
            }
            else if (utilisateurValidé != null)
            {               
                    MessageBox.Show("Veuillez saisir le nouveau mot de passe SVP!");
                    password.Visibility = Visibility;
                    UpDatePassord.Visibility = Visibility;
                    ValiderOldPassWord.Visibility = Visibility.Hidden; 
                    
            }
        }
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                MyApps.Application.ViewModels.SiteViewModel dataRowViews = (MyApps.Application.ViewModels.SiteViewModel)((Button)e.Source).DataContext;
                String FormateurName = dataRowViews.NomFormateur.ToString();
                String MessageDescription = dataRowViews.Evenement1.ToString();
                string DateEvenement = dataRowViews.DateOfEvent.ToString();
                MessageBox.Show("Publié par : " + FormateurName + "\r\nDéscription : " + MessageDescription + "\r\nDate de Publication : " + DateEvenement);
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
