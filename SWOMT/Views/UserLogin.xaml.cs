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
using System.Windows.Shapes;

namespace SWOMT.Views
{
    /// <summary>
    /// Logique d'interaction pour UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        List<MyApps.Application.ViewModels.UserViewModel> liste = new List<MyApps.Application.ViewModels.UserViewModel>();
        List<MyApps.Application.ViewModels.UserRolesViewModel> UserRoleList = new List<MyApps.Application.ViewModels.UserRolesViewModel>();

        public UserLogin()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();

            UserRoleList = MyApps.Application.Services.UserRolesViewModelService.GetUsersRoles(); 
            //foreach (var utilisateur in liste)
            //{
            //    username.Items.Add(utilisateur.UserName);
            //}
            this.SelectedRolesusers(); 
        }

        private void LoginSubmit(object sender, RoutedEventArgs e)
        {
            
            if (username.Text == "")
            {
                MessageBox.Show("Veuillez entrer l'utilisateur SVP!");
                return;
            }

            if (password.Password == "")
            {
                MessageBox.Show("Veuillez entrer le mot de passe SVP!");
                return;
            }

           // string utilisateurNom = MyApps.Domain.Service.UserService.GetUtilisateurNom(username.Text); 

            //if (username.Text == "")
            //{
            //    MessageBox.Show("L'utilisateur n'existe pas dans la base de données SVP");
            //    return;
            //}
            
            string utilisateurValidé = MyApps.Domain.Service.UserService.LoginUserNom(username.Text, password.Password);
            

            if (utilisateurValidé == null)
            {
                MessageBox.Show("Le nom d'utilisateur et le mot de passe ne correspondent pas SVP!");
                return;
            }
           
            string utilisateurUserRole = MyApps.Domain.Service.UserService.GetUtilisateurUserRole(utilisateurValidé);

            TableauDeBord accueil = new TableauDeBord(utilisateurValidé.ToString() ,utilisateurUserRole.ToString());   
            accueil.Show();
            username.Text = "";
            password.Password = "";
       
        }
        

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            LabelRoleUtilisateur.Visibility = Visibility;
            ComboBoxUserRole.Visibility=Visibility;
            ValiderInscription.Visibility = Visibility;
           
        }

        private void ValiderInscription_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur element = new Utilisateur();
            if (username.Text == "")
            {
                MessageBox.Show("Veuillez entrer l'utilisateur SVP!");
                return;
            }

            if (password.Password == "")
            {
                MessageBox.Show("Veuillez entrer le mot de passe SVP!");
                return;
            }

           // string utilisateurNom = MyApps.Domain.Service.UserService.GetUtilisateurNom(username.Text);

            if (username.Text == "")
            {
                MessageBox.Show("L'utilisateur n'existe pas dans la base de données SVP");
                return;
            }
            element.UserName = username.Text;
            element.MotDePasse = password.Password;
            element.UserRole = ComboBoxUserRole.Text;
            foreach (var donne in MyApps.Application.Services.UserViewModelService.GetUsers())
            {
                if (element.UserName == donne.UserName) // if the items has both ids then rejects
                {
                    MessageBox.Show("les données existé déjà ! dans la base de données");
                    return;
                }
            }

            MyApps.Domain.Service.UserService.Create(element.UserName, element.MotDePasse, element.UserRole);
            username.Text = "";
            password.Password = "";
            LabelRoleUtilisateur.Visibility = Visibility.Hidden;
            ComboBoxUserRole.Visibility = Visibility.Hidden;
            ValiderInscription.Visibility = Visibility.Hidden;
        }
        private List<MyApps.Application.ViewModels.UserRolesViewModel> SelectedRolesusers()
        {

            foreach (var items in UserRoleList.Where(f => f.UserRoleName != "Admin" && f.UserRoleName != "Secrétaire"))
            {
                ComboBoxUserRole.Items.Add(items.UserRoleName);
            }

            return UserRoleList;
        }
       
    }
}
