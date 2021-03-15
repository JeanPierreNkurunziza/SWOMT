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
    /// Logique d'interaction pour Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        List<MyApps.Application.ViewModels.UserViewModel> liste = new List<MyApps.Application.ViewModels.UserViewModel>();
        List<MyApps.Application.ViewModels.UserRolesViewModel> UserRoleList = new List<MyApps.Application.ViewModels.UserRolesViewModel>();

        string enregistre;   
        public Users()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
           
            PopulateAndBind(liste);
            UserRoleList = MyApps.Application.Services.UserRolesViewModelService.GetUsersRoles();
            PopulateAndBindUserRole(UserRoleList);
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

        }

        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {

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
            MotDePasse.IsEnabled = false;
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



            if (enregistre == "Ajouter")
            {


                element.UserName= UserName.Text;
                element.MotDePasse=MotDePasse.Text;
                element.UserRole= ComboBoxUserRole.Text;
                foreach (var donne in MyApps.Application.Services.UserViewModelService.GetUsers())
                {
                    if (element.UserName == donne.UserName)
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }

                MyApps.Domain.Service.UserService.Create(element.UserName, element.MotDePasse, element.UserRole);

            }

            if (enregistre == "Modifier")
            {

                element.IdUser = short.Parse(IdUser.Text);
                element.UserName = UserName.Text;
                element.MotDePasse = MotDePasse.Text;
                element.UserRole = ComboBoxUserRole.Text;
                
               // MyApps.Domain.Service.UserService.Update(element);
                MyApps.Domain.Service.UserService.MetàJourUser(element.IdUser,element.UserName,element.MotDePasse,element.UserRole);
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

        private void PopulateAndBindUserRole(List<MyApps.Application.ViewModels.UserRolesViewModel> listeItems)
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
            if (ListUserRole.SelectedItem is MyApps.Application.ViewModels.UserRolesViewModel donnee)
            {
                IdUserRole.Text = donnee.IdUserRole.ToString();
                UserRoleName.Text = donnee.UserRoleName.ToString(); 
               
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
            UserRoleList.Clear();
            UserRoleList = MyApps.Application.Services.UserRolesViewModelService.GetUsersRoles();
            PopulateAndBindUserRole(UserRoleList);
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
                foreach (var donne in MyApps.Application.Services.UserRolesViewModelService.GetUsersRoles())
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
            UserRoleList.Clear();
            UserRoleList = MyApps.Application.Services.UserRolesViewModelService.GetUsersRoles(); 
            PopulateAndBindUserRole(UserRoleList); 
            IdUserRole.Text = "";
            UserRoleName.Text = "";
            ComboBoxUserRole.Items.Clear();
            this.SelectedRolesusers();
        }
        /// <summary>
        /// récuperer le role de l'utilisateur
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.UserRolesViewModel> SelectedRolesusers()
        {

            foreach (var items in UserRoleList)
            {  
                ComboBoxUserRole.Items.Add(items.UserRoleName);
            }

            return UserRoleList; 
        }

    }
}
