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
    /// Logique d'interaction pour Utilisateur.xaml
    /// </summary>
    public partial class Utilisateur : Page
    {
        List<MyApps.Application.ViewModels.UserViewModel> liste = new List<MyApps.Application.ViewModels.UserViewModel>();
        string enregistre; 
        public Utilisateur()
        {
            InitializeComponent();

            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
            PopulateAndBind(liste);
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

        private void ListUser_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListUser.SelectedItem is MyApps.Application.ViewModels.UserViewModel donnee)
            {
                IdUser.Text = donnee.IdUser.ToString();
                UserName.Text = donnee.UserName.ToString();
                MotDePasse.Text = donnee.MotDepasse.ToString();
                UserRole.Text = donnee.UserRole.ToString();

            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            UserName.IsEnabled = true;
            MotDePasse.IsEnabled = true;
            UserRole.IsEnabled = true;
            IdUser.Text = "";
            UserName.Text = "";
            MotDePasse.Text = "";
            UserRole.Text = "";
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            UserName.IsEnabled = true;
            MotDePasse.IsEnabled = true;
            UserRole.IsEnabled = true;
            
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (IdUser.Text == "")
            {
                MessageBox.Show("Veuillez sélectionner l'identifiant à supprimer");
                return;
            }
            MyApps.Domain.Service.UserService.Delete(short.Parse(IdUser.Text));

            UserName.IsEnabled = true;
            MotDePasse.IsEnabled = true;
            UserRole.IsEnabled = true;
            UserName.Text = "";
            MotDePasse.Text = "";
            UserRole.Text = "";
        }

        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            MyApps.Infrastructure.DB.Utilisateur element = new MyApps.Infrastructure.DB.Utilisateur();


            if (UserName.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom de l'utilisateur");
                return;
            }



            if (enregistre == "Ajouter")
            {


                element.UserName = UserName.Text;
                element.MotDePasse = MotDePasse.Text;
                element.UserRole = UserRole.Text;

                MyApps.Domain.Service.UserService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdUser = short.Parse(IdUser.Text);
                element.UserName = UserName.Text;
                element.MotDePasse = MotDePasse.Text;
                element.UserRole = UserRole.Text;

                MyApps.Domain.Service.UserService.Update(element);
            }
            liste.Clear();
            liste = MyApps.Application.Services.UserViewModelService.GetUsers();
            PopulateAndBind(liste);
            IdUser.Text = "";
            UserName.Text = "";
            MotDePasse.Text = "";
            UserRole.Text = "";
        }
    }
}
