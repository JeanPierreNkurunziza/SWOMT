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
    /// Logique d'interaction pour Formateurs.xaml
    /// </summary>
    public partial class Formateurs : Page
    {
        List<MyApps.Application.ViewModels.FormateurViewModel> liste = new List<MyApps.Application.ViewModels.FormateurViewModel>(); 

        string enregistre;
        public Formateurs()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
            PopulateAndBind(liste);

        }



        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.FormateurViewModel> listeItems)
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
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.FormateurViewModel donnee)
            {
                IdFormateur.Text = donnee.IdFormateur.ToString();
                NomFormateur.Text = donnee.NomFormateur.ToString();
                Domaine.Text = donnee.Domaine.ToString();
                TelFormateur.Text = donnee.TelFormateur.ToString();
                EmailFormateur.Text = donnee.EmailFormateur.ToString();
                DateEncodage.Text = donnee.DateEncodage.ToShortTimeString(); 

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
            Formateur element = new Formateur(); 
            //Competence competence = new Competence();

            //if (NomCompetence.Text == "")
            //{
            //    MessageBox.Show("Il faut saisir le nom de la competence");
            //    return;
            //}



            if (enregistre == "Ajouter")
            {
                element.NomFormateur = NomFormateur.Text;
                element.Domaine = Domaine.Text;
                element.TélFormateur = TelFormateur.Text;
                element.EmailFormateur = EmailFormateur.Text;
                element.DateEncodage = DateTime.Parse(DateEncodage.Text);

                MyApps.Domain.Service.FormateurService.Create(element);

            }

            if (enregistre == "Modifier")
            {
                element.IdFormateur = short.Parse(IdFormateur.Text);
                element.NomFormateur = NomFormateur.Text;
                element.Domaine = Domaine.Text;
                element.TélFormateur = TelFormateur.Text;
                element.EmailFormateur = EmailFormateur.Text;
                element.DateEncodage = DateTime.Parse(DateEncodage.Text);

                MyApps.Domain.Service.FormateurService.Update(element);
            }


            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
            PopulateAndBind(liste);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdFormateur.Text == "")
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

            MyApps.Domain.Service.FormateurService.Delete(short.Parse(IdFormateur.Text));

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdFormateur.Text = "";
            NomFormateur.Text = "";
            Domaine.Text = "";
            TelFormateur.Text = "";
            EmailFormateur.Text = "";
            DateEncodage.Text = "";

        }
        private void ModeIsEnabledTrue()
        {
            IdFormateur.IsEnabled = false;
            NomFormateur.IsEnabled = true;
            Domaine.IsEnabled = true;
            TelFormateur.IsEnabled = true;
            EmailFormateur.IsEnabled = true;
            DateEncodage.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdFormateur.IsEnabled = false;
            NomFormateur.IsEnabled = false;
            Domaine.IsEnabled = false;
            TelFormateur.IsEnabled = false;
            EmailFormateur.IsEnabled = false;
            DateEncodage.IsEnabled = false;

        }
    }
}
