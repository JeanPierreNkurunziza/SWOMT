﻿using MyApps.Infrastructure.DB;
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
        List<MyApps.Application.ViewModels.FormationViewModel> listeFormation = new List<MyApps.Application.ViewModels.FormationViewModel>();
        List<MyApps.Application.ViewModels.ModuleViewModel> listeModule = new List<MyApps.Application.ViewModels.ModuleViewModel>();

        string enregistre;
        public PlanningFormations() 
        {
            InitializeComponent();
            liste = MyApps.Application.Services.PlanningsFormation.GetPlanningFormation();
            PopulateAndBind(liste);
            listeFormation = MyApps.Application.Services.FormationViewModelsServices.GetFormations();
            PopulateAndBindFormation(listeFormation);
            listeModule = MyApps.Application.Services.ModuleViewModelService.GetModules();
            PopulateAndBindModule(listeModule);

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
                element.IdFormation =short.Parse( Id.Text);
                element.DateFormation = DateTime.Parse(DateFormation.Text).Date;

                MyApps.Domain.Service.PlanningService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdPlanning = short.Parse(IdPlanning.Text);
                element.IdFormation = short.Parse(Id.Text);
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

        //*************************************************************************************************************************
        //**************************************** code pour la partie : gestin de formation **************************************

        /// <summary>
        /// biding la liste de formations
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBindFormation(List<MyApps.Application.ViewModels.FormationViewModel> listeFormations)
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
            listeModule.Clear();
            listeModule = MyApps.Application.Services.ModuleViewModelService.GetModulesPerFormation(short.Parse(IdFormation.Text));
            PopulateAndBindModule(listeModule);
        }

        private void AjouterFormation_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValuesFormation();
            ModeIsEnabledTrueFormation();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjourFormation_Click(object sender, RoutedEventArgs e)
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


            ModeIsEnabledFalseFormation();
            listeFormation.Clear();
            listeFormation = MyApps.Application.Services.FormationViewModelsServices.GetFormations();
            PopulateAndBindFormation(listeFormation);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierFormation_Click(object sender, RoutedEventArgs e)
        {

            if (IdFormation.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrueFormation();


        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerFormation_Click(object sender, RoutedEventArgs e)
        {
            //le code pour signaler la presence de l'idParticipant dans la table Inscription on doit d'abord faire une vérification

            MyApps.Domain.Service.FormationService.Delete(short.Parse(IdFormation.Text));

            ClearFormValuesFormation();
            listeFormation.Clear();
            listeFormation = MyApps.Application.Services.FormationViewModelsServices.GetFormations();
            PopulateAndBindFormation(listeFormation);

        }
        private void ClearFormValuesFormation()
        {
            IdFormation.Text = "";
            NomFormation.Text = "";
            Description.Text = "";

        }
        private void ModeIsEnabledTrueFormation()
        {
            IdFormation.IsEnabled = true;
            NomFormation.IsEnabled = true;
            Description.IsEnabled = true;

        }
        private void ModeIsEnabledFalseFormation()
        {
            IdFormation.IsEnabled = false;
            NomFormation.IsEnabled = false;
            Description.IsEnabled = false;

        }

        //****************************************************************************************************************************
        //************************************ code de la partie a droite : gérer la liste des modules********************************

        private void PopulateAndBindModule(List<MyApps.Application.ViewModels.ModuleViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElementModule.DataContext = listeItems;
        }

        //****************************************************************************************************************
        //*************************** les méthodes de recherches *********************************************************
        //****************************************************************************************************************
        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {

            if (NomRechercher.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");

                listeFormation = MyApps.Application.Services.FormationViewModelsServices.GetFormations();
                PopulateAndBindFormation(listeFormation);
                return;

            }

            listeFormation = MyApps.Application.Services.FormationViewModelsServices.GetSearchByName(NomRechercher.Text);
            PopulateAndBindFormation(listeFormation);
        }
        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {
            NomRechercher.Text = "";
            listeFormation = MyApps.Application.Services.FormationViewModelsServices.GetSearchByName(NomRechercher.Text); 
            PopulateAndBindFormation(listeFormation);
        }
        private void RechercherModule_Click(object sender, RoutedEventArgs e)
        {

            if (NomRechercherModule.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");

                listeModule = MyApps.Application.Services.ModuleViewModelService.GetModules();
                PopulateAndBindModule(listeModule);
                return;

            }

            listeModule = MyApps.Application.Services.ModuleViewModelService.SearchModuleByName(NomRechercherModule.Text);
            PopulateAndBindModule(listeModule);
        }
        private void ReSetListModule_Click(object sender, RoutedEventArgs e)
        {
            NomRechercherModule.Text = "";
            listeModule = MyApps.Application.Services.ModuleViewModelService.SearchModuleByName(NomRechercherModule.Text);
            PopulateAndBindModule(listeModule);
        }
    }
}
