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
    /// Logique d'interaction pour Modules.xaml
    /// </summary>
    public partial class Modules : Page 
    {
        List<MyApps.Application.ViewModels.ModuleViewModel> liste = new List<MyApps.Application.ViewModels.ModuleViewModel>();

        string enregistre;
        public Modules() 
        {
            InitializeComponent();
            liste = MyApps.Application.Services.ModuleViewModelService.GetModules();
            PopulateAndBind(liste);

        }



        /// <summary>
        /// biding la liste de modules
        /// </summary>
        /// <param name="listeModules"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.ModuleViewModel> listeItems)
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
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.ModuleViewModel donnee) 
            {
                IdModule.Text = donnee.IdModule.ToString(); 
                IdFormation.Text = donnee.IdFormation.ToString();
                NomModule.Text = donnee.NomModule.ToString();
                NomFormation.Text = donnee.NomFormation.ToString();
                CreditModule.Text = donnee.CreditModule.ToString();
                NombrePrévu.Text = donnee.NombrePrévu.ToString();

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
            Module element = new Module();  
            //Competence competence = new Competence();

            if (IdModule.Text == "")
            {
                MessageBox.Show("Il faut saisir identifiant ");
                return;
            }

            if (enregistre == "Ajouter")
            {

                //element.IdModule = short.Parse(IdModule.Text);
                element.IdFormation = short.Parse(IdFormation.Text);
                element.NomModule = NomModule.Text;
                element.CreditModule = short.Parse(CreditModule.Text); 
                element.NombrPrévu = short.Parse(NombrePrévu.Text); 
              
                MyApps.Domain.Service.ModuleService.Create(element); 

            }

            if (enregistre == "Modifier")
            {

                //element.IdModule = short.Parse(IdModule.Text);
                element.IdFormation = short.Parse(IdFormation.Text);
                element.NomModule = NomModule.Text;
                element.CreditModule = short.Parse(CreditModule.Text);
                element.NombrPrévu = short.Parse(NombrePrévu.Text);
               
                MyApps.Domain.Service.ModuleService.Update(element);
            }

            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.ModuleViewModelService.GetModules(); 
            PopulateAndBind(liste);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdModule.Text == "") 
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

            MyApps.Domain.Service.ModuleService.Delete(short.Parse(IdModule.Text)); 

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.ModuleViewModelService.GetModules(); 
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdModule.Text = "";
            IdFormation.Text = "";
            NomModule.Text = "";
            NomFormation.Text = "";
            CreditModule.Text = "";
            NombrePrévu.Text = ""; 

        }
        private void ModeIsEnabledTrue()
        {
            IdModule.IsEnabled = false;
            IdFormation.IsEnabled = true;
            NomModule.IsEnabled = true;
            NomFormation.IsEnabled = true;
            CreditModule.IsEnabled = true;
            NombrePrévu.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdModule.IsEnabled = false;
            IdFormation.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomFormation.IsEnabled = false;
            CreditModule.IsEnabled = false;
            NombrePrévu.IsEnabled = false; 

        }
    }
}
