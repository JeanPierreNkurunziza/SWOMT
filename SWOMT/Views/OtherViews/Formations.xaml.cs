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
    /// Logique d'interaction pour Formations.xaml
    /// </summary>
    public partial class Formations : Page
    {
        List<MyApps.Application.ViewModels.FormationViewModel> liste = new List<MyApps.Application.ViewModels.FormationViewModel>();

        string enregistre;
        public Formations()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.FormationViewModelsServices.GetFormations();
            PopulateAndBind(liste);

        }



        /// <summary>
        /// biding la liste de formations
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.FormationViewModel> listeFormations)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListFormation.DataContext = listeFormations;
        }




        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListFormation_MouseDoubleClick(object sender, SelectionChangedEventArgs e) 
        {
            if (ListFormation.SelectedItem is MyApps.Application.ViewModels.FormationViewModel donnee)
            {
                IdFormation.Text = donnee.IdFormation.ToString();
                NomFormation.Text = donnee.NomFormation.ToString();
                Description.Text = donnee.Description.ToString();
               
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValues();
            ModeIsEnabledTrue();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            Formation formation = new Formation();
            //Competence competence = new Competence();

            //if (NomCompetence.Text == "")
            //{
            //    MessageBox.Show("Il faut saisir le nom de la competence");
            //    return;
            //}



            if (enregistre == "Ajouter")
            {
                formation.NomFormation = NomFormation.Text;
                formation.Description = Description.Text;
             
                MyApps.Domain.Service.FormationService.Create(formation);

            }

            if (enregistre == "Modifier")
            {

                formation.IdFormation = short.Parse(IdFormation.Text);
                formation.NomFormation = NomFormation.Text;
                formation.Description = Description.Text;
               
                MyApps.Domain.Service.FormationService.Update(formation);
            }


            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.FormationViewModelsServices.GetFormations();
            PopulateAndBind(liste);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdFormation.Text == "") 
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

            MyApps.Domain.Service.FormationService.Delete(short.Parse(IdFormation.Text));

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.FormationViewModelsServices.GetFormations(); 
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdFormation.Text = "";
            NomFormation.Text = "";
            Description.Text = "";
           
        }
        private void ModeIsEnabledTrue()
        {
            IdFormation.IsEnabled = true;
            NomFormation.IsEnabled = true;
            Description.IsEnabled = true;
            
        }
        private void ModeIsEnabledFalse()
        {
            IdFormation.IsEnabled = false;
            NomFormation.IsEnabled = false;
            Description.IsEnabled = false;
           
        }
    }
}
