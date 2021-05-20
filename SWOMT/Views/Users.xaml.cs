using MyApps.Infrastructure.DB; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        List<MyApps.Application.ViewModels.UserViewModel> liste = new List<MyApps.Application.ViewModels.UserViewModel>();

        string enregistre;   
        public Users()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
           
            PopulateAndBind(liste);
            liste = MyApps.Application.Services.UserViewModelService.GetUsersRoles();
            PopulateAndBindUserRole(liste);
            this.SelectedRolesusers();
         
        }
        private void PopulateAndBind(List<MyApps.Application.ViewModels.UserViewModel> listeItems) 
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListUser.DataContext = listeItems;
        }

        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {
            if (NomRechercher.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");

                liste = MyApps.Application.Services.UserViewModelService.GetUsers();
                PopulateAndBind(liste);
                return;

            }

            liste = MyApps.Application.Services.UserViewModelService.searchNameIntheList(NomRechercher.Text); 
            PopulateAndBind(liste);
        }

        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {
            NomRechercher.Text = "";
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
            PopulateAndBind(liste);
        }
        /// <summary>
        /// affichage de données après leur sélection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListUser_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListUser.SelectedItem is MyApps.Application.ViewModels.UserViewModel donnee)
            {
                IdUser.Text = donnee.IdUser.ToString();
                UserName.Text = donnee.UserName.ToString();
                MotDePasse.Text = donnee.MotDePasse.ToString();
                ComboBoxUserRole.Text = donnee.UserRole.ToString();

            }
        }
        /// <summary>
        /// méthode pour activer l'ajout d'un utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
           
          
            UserName.IsEnabled = true;
            MotDePasse.IsEnabled = true;
            ComboBoxUserRole.IsEnabled = true;
            IdUser.Text = "";
            UserName.Text = "";
            MotDePasse.Text = "";
            ComboBoxUserRole.Text = "";
        }
        /// <summary>
        /// activation des modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (IdUser.Text == "")
            {
                MessageBox.Show("Entrer l'utilisateur à modifier");
                return;
            }
            enregistre = "Modifier";
            UserName.IsEnabled = true;
            MotDePasse.IsEnabled = true;
            MotDePasse.Text ="";
           // MotDePasse.Visibility = Visibility.Collapsed; 
            ComboBoxUserRole.IsEnabled = true;

        }
        /// <summary>
        /// méthode pour supprimer un utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (IdUser.Text == "")
            {
                MessageBox.Show("Veuillez sélectionner l'identifiant à supprimer");
                return;
            }
            MyApps.Domain.Service.UserService.Delete(short.Parse(IdUser.Text));
            liste.Clear();
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
            PopulateAndBind(liste);
            UserName.IsEnabled = false;
            MotDePasse.IsEnabled = false;
            ComboBoxUserRole.IsEnabled = false;
            IdUser.Text = "";
            UserName.Text = "";
            MotDePasse.Text = "";
            ComboBoxUserRole.Text = "";
        }
        /// <summary>
        /// méthode pour mettre à jour les données des utilisateurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            //Utilisateur element = new Utilisateur();
            Utilisateur element = new Utilisateur();


            if (UserName.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom de l'utilisateur");
                return;
            }
            if (ValidatePassword(MotDePasse.Text, out string ErrorMessage) != true)
            {
                MessageBox.Show(ErrorMessage);
                return;
            }

            if (ComboBoxUserRole.Text == "")
            {
                MessageBox.Show("Il faut saisir seléctionner le role de l'utilisateur");
                return;
            }

            if (enregistre == "Ajouter")
            {


                element.UserName= UserName.Text;
                element.MotDePasse=MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.CreateHash(MotDePasse.Text);
                element.UserRole= ComboBoxUserRole.Text;
                foreach (var donne in MyApps.Application.Services.UserViewModelService.GetUsers())
                {
                    if (element.UserName == donne.UserName)
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }

                MyApps.Domain.Service.UserService.Ajouter(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdUser = short.Parse(IdUser.Text);
                element.UserName = UserName.Text;
                element.MotDePasse = MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.CreateHash(MotDePasse.Text);
                element.UserRole = ComboBoxUserRole.Text;
                
               // MyApps.Domain.Service.UserService.Update(element);
                MyApps.Domain.Service.UserService.Update(element);
            }
           
            liste.Clear();
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
            PopulateAndBind(liste);
            IdUser.IsEnabled = false;
            UserName.IsEnabled = false;
            MotDePasse.IsEnabled = false;
            ComboBoxUserRole.IsEnabled = false;
            IdUser.Text = "";
            UserName.Text = "";
            MotDePasse.Text = "";
            ComboBoxUserRole.Text = "";
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------
        //------------------------------ La gestion des roles des utilisateurs-----------------------------------------------------------

        private void PopulateAndBindUserRole(List<MyApps.Application.ViewModels.UserViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListUserRole.DataContext = listeItems;
        }

        /// <summary>
        /// méthode pour afficher les données après une selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ListUserRole_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListUserRole.SelectedItem is MyApps.Application.ViewModels.UserViewModel donnee)
            {
                IdUserRole.Text = donnee.IdUserRole.ToString();
                UserRoleName.Text = donnee.UserRoleName.ToString();
                liste = MyApps.Application.Services.UserViewModelService.GetUsersByRoles(donnee.UserRoleName);
                PopulateAndBind(liste);
                nbrUsers.Text = liste.Count().ToString();
            }
           
          
        }
        /// <summary>
        /// créer un role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterRole_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";

            IdUserRole.Text = "";
            IdUserRole.IsEnabled = false;
            UserRoleName.IsEnabled = true;
            UserRoleName.Text = "";
           
        }
        /// <summary>
        /// activation de modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierRole_Click(object sender, RoutedEventArgs e)
        {
            if (IdUserRole.Text == "")
            {
                MessageBox.Show("Entrer le role à modifier");
                return;
            }
            enregistre = "Modifier";
            UserRoleName.IsEnabled = true;
           

        }
        /// <summary>
        /// méthode pour supprimer un role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerRole_Click(object sender, RoutedEventArgs e)
        {
            if (IdUserRole.Text == "")
            {
                MessageBox.Show("Veuillez sélectionner l'identifiant à supprimer");
                return;
            }
            MyApps.Domain.Service.UserRolesServices.Delete(short.Parse(IdUserRole.Text));
            liste.Clear();
            liste = MyApps.Application.Services.UserViewModelService.GetUsersRoles();
            PopulateAndBindUserRole(liste);
            UserRoleName.IsEnabled = false;
            IdUserRole.Text = "";
            UserRoleName.Text = "";
            ComboBoxUserRole.Items.Clear();
            this.SelectedRolesusers(); 

        } 
        /// <summary>
        /// mettre à jour les données de roles 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjourRole_Click(object sender, RoutedEventArgs e) 
        {
          
            UserRole element = new UserRole();


            if (UserRoleName.Text == "")
            {
                MessageBox.Show("Il faut saisir le role");
                return;
            }

          
            if (enregistre == "Ajouter")
            {


                element.UserRoleName = UserRoleName.Text;
                foreach (var donne in MyApps.Application.Services.UserViewModelService.GetUsersRoles())
                {
                    if ((element.UserRoleName == donne.UserRoleName)) // if the items has both ids then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }
                MyApps.Domain.Service.UserRolesServices.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdUserRole = short.Parse(IdUserRole.Text);
                element.UserRoleName = UserRoleName.Text;

                
                MyApps.Domain.Service.UserRolesServices.Update(element);

            }
            liste.Clear();
            liste = MyApps.Application.Services.UserViewModelService.GetUsersRoles(); 
            PopulateAndBindUserRole(liste); 
            IdUserRole.Text = "";
            UserRoleName.Text = "";
            ComboBoxUserRole.Items.Clear();
            this.SelectedRolesusers();
        }
        /// <summary>
        /// récuperer le role de l'utilisateur
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.UserViewModel> SelectedRolesusers()
        {

            foreach (var items in liste)
            {  
                ComboBoxUserRole.Items.Add(items.UserRoleName);
            }

            return liste; 
        }
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


    }
}
