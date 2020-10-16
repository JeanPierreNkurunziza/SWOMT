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
    /// Logique d'interaction pour PlanningFormations.xaml
    /// </summary>
    public partial class PlanningFormations : Page
    {
        List<MyApps.Application.ViewModels.PlanningViewModel> liste = new List<MyApps.Application.ViewModels.PlanningViewModel>();

        string enregistre;
        public PlanningFormations() 
        {
            InitializeComponent();
            liste = MyApps.Application.Services.PlanningsFormation.GetPlanningFormation();
            PopulateAndBind(liste);

        }



        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.PlanningViewModel> listeItems)
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
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.PlanningViewModel donnee)
            {
                IdPlanning.Text = donnee.IdPlanning.ToString();
                IdFormation.Text = donnee.IdFormation.ToString();
                NomFormation.Text = donnee.NomFormation.ToString();
                DateFormation.Text = donnee.DateFormation.ToString();

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
            Planning element = new Planning();
            //Competence competence = new Competence();

            //if (NomCompetence.Text == "")
            //{
            //    MessageBox.Show("Il faut saisir le nom de la competence");
            //    return;
            //}



            if (enregistre == "Ajouter")
            {
                element.IdFormation =short.Parse( IdFormation.Text);
                element.DateFormation = DateTime.Parse(DateFormation.Text).Date;

                MyApps.Domain.Service.PlanningService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdPlanning = short.Parse(IdPlanning.Text);
                element.IdFormation = short.Parse(IdFormation.Text);
                element.DateFormation = DateTime.Parse( DateFormation.Text).Date;

                MyApps.Domain.Service.PlanningService.Update(element);
            }


            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.PlanningsFormation.GetPlanningFormation();
            PopulateAndBind(liste);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdPlanning.Text == "")
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

            MyApps.Domain.Service.PlanningService.Delete(short.Parse(IdPlanning.Text));

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.PlanningsFormation.GetPlanningFormation();
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdPlanning.Text = "";
            IdFormation.Text = "";
            NomFormation.Text = "";
            DateFormation.Text = "";

        }
        private void ModeIsEnabledTrue()
        {
            IdPlanning.IsEnabled = true;
            IdFormation.IsEnabled = true;
            NomFormation.IsEnabled = true;
            DateFormation.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdPlanning.IsEnabled = false;
            IdFormation.IsEnabled = false;
            NomFormation.IsEnabled = false;
            DateFormation.IsEnabled =false;

        }
    }
}
