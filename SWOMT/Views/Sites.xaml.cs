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
        List<MyApps.Application.ViewModels.SiteViewModel> liste = new List<MyApps.Application.ViewModels.SiteViewModel>();

        string enregistre;
        public Sites() 
        {
            InitializeComponent();
            liste = MyApps.Application.Services.SitesViewModelsServices.GetSites();
            PopulateAndBind(liste);

        }



        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.SiteViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElement.DataContext = listeItems;
        }




        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListElement_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.SiteViewModel donnee)
            {
                IdSite.Text = donnee.IdSite.ToString();
                NomSite.Text = donnee.NomSite.ToString();
                AdresseSite.Text = donnee.AdresseSite.ToString();

            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValues();
            ModeIsEnabledTrue();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            Site element = new Site();
            //Competence competence = new Competence();

            //if (NomCompetence.Text == "")
            //{
            //    MessageBox.Show("Il faut saisir le nom de la competence");
            //    return;
            //}



            if (enregistre == "Ajouter")
            {
                element.NomSite = NomSite.Text;
                element.AdresseSite = AdresseSite.Text;

                MyApps.Domain.Service.SiteService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdSite = short.Parse(IdSite.Text);
                element.NomSite = NomSite.Text;
                element.AdresseSite = AdresseSite.Text;

                MyApps.Domain.Service.SiteService.Update(element);
            }


            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.SitesViewModelsServices.GetSites();
            PopulateAndBind(liste);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdSite.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrue();


        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            //le code pour signaler la presence de l'idParticipant dans la table Inscription on doit d'abord faire une vérification

            MyApps.Domain.Service.SiteService.Delete(short.Parse(IdSite.Text));

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.SitesViewModelsServices.GetSites(); 
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdSite.Text = "";
            NomSite.Text = "";
            AdresseSite.Text = "";

        }
        private void ModeIsEnabledTrue()
        {
            IdSite.IsEnabled = true;
            NomSite.IsEnabled = true;
            AdresseSite.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdSite.IsEnabled = false;
            NomSite.IsEnabled = false;
            AdresseSite.IsEnabled = false;

        }
    }
}
