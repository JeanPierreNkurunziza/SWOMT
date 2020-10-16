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
    /// Logique d'interaction pour Exames.xaml
    /// </summary>
    public partial class Exames : Page
    {
        List<MyApps.Application.ViewModels.ExamenViewModel> liste = new List<MyApps.Application.ViewModels.ExamenViewModel>();

        string enregistre;
        public Exames()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.ExamenViewModelService.GetExamens();
            PopulateAndBind(liste);

        }



        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.ExamenViewModel> listeItems)
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
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.ExamenViewModel donnee)
            {
                IdExamen.Text = donnee.IdExamen.ToString();
                IdModule.Text = donnee.IdSiteModule.ToString();
                NomModule.Text = donnee.NomModule.ToString();
                DateExamen.Text = donnee.DateExamen.ToString(); 

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
            Examan element = new Examan(); 
            //Competence competence = new Competence();

            if (IdModule.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom de la competence");
                return;
            }



            if (enregistre == "Ajouter")
            {

                //element.IdExamen = short.Parse(IdExamen.Text);
                element.IdSiteModule = short.Parse(IdModule.Text);
                element.DateExamen = DateTime.Parse(DateExamen.Text).Date; 

                MyApps.Domain.Service.ExamenService.Create(element); 

            }

            if (enregistre == "Modifier")
            {

                //element.IdExamen = short.Parse(IdExamen.Text);
                element.IdSiteModule = short.Parse(IdModule.Text);
               
                element.DateExamen = DateTime.Parse(DateExamen.Text).Date;

                MyApps.Domain.Service.ExamenService.Update(element);
            }


            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.ExamenViewModelService.GetExamens(); 
            PopulateAndBind(liste);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdExamen.Text == "")
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

            MyApps.Domain.Service.ExamenService.Delete(short.Parse(IdExamen.Text)); 

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.ExamenViewModelService.GetExamens();
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdExamen.Text = "";
            IdModule.Text = "";
            NomModule.Text = "";
            DateExamen.Text = "";

        }
        private void ModeIsEnabledTrue()
        {
            IdExamen.IsEnabled = false; 
            IdModule.IsEnabled = true;
            NomModule.IsEnabled = true;
            DateExamen.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdExamen.IsEnabled = false;
            IdModule.IsEnabled = false;
            NomModule.IsEnabled = false;
            DateExamen.IsEnabled = false;

        }
    }
}
