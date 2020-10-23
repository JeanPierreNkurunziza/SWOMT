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
    /// Logique d'interaction pour Presences.xaml
    /// </summary>
    public partial class Presences : Page
    {
        List<MyApps.Application.ViewModels.PresenceViewModel> liste = new List<MyApps.Application.ViewModels.PresenceViewModel>();
        List<MyApps.Application.ViewModels.PresenceViewModel> listeParticipantAbsent = new List<MyApps.Application.ViewModels.PresenceViewModel>(); 
        List<MyApps.Application.ViewModels.ModuleViewModel> listeModule = new List<MyApps.Application.ViewModels.ModuleViewModel>();
        List<MyApps.Application.ViewModels.SitePlanningViewModel> listeModulePlanning = new List<MyApps.Application.ViewModels.SitePlanningViewModel>();
        List<MyApps.Application.ViewModels.InscriptionViewModel> listeInscription = new List<MyApps.Application.ViewModels.InscriptionViewModel>();
        List<MyApps.Application.ViewModels.ParticipantViewModel> listeParticipant = new List<MyApps.Application.ViewModels.ParticipantViewModel>();

        string enregistre;
        int idSiteModuleSelected;
        int idParticipantSelected;
        public Presences() 
        {
            InitializeComponent();
            liste = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            listeModule = MyApps.Application.Services.ModuleViewModelService.GetModules();
            listeModulePlanning = MyApps.Application.Services.SitePlanningViewModelService.GetSitePlanning();
            listeInscription = MyApps.Application.Services.InscriptionViewModelService.GetInscriptions();
            listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            
            PopulateAndBindModule(listeModulePlanning); 
            this.SelectedNomModule();
            EstPresent.Items.Add("False");
            EstPresent.Items.Add("True");

        }



        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="liste"></param>
        private void PopulateAndBindParticipantPresent(List<MyApps.Application.ViewModels.PresenceViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipantPresent.DataContext = listeItems;
        }
        private void PopulateAndBindParticipantAbsent(List<MyApps.Application.ViewModels.PresenceViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipantAbsent.DataContext = listeItems; 
        }

        /// <summary>
        /// Binding the liste of the participant inscrit dans un module 
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindListeInscription(List<MyApps.Application.ViewModels.InscriptionViewModel> listeItems)
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
        private void ListParticipantPresent_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipantPresent.SelectedItem is MyApps.Application.ViewModels.PresenceViewModel donnee)
            {
                IdPresence.Text = donnee.IdPresence.ToString();
                IdSiteModule.Text = donnee.NomParticipant.ToString();
                IdParticipant.Text = donnee.NomParticipant.ToString();
                //NomModule.Text = donnee.NomModule.ToString();
                //NomParticipant.Text = donnee.NomParticipant.ToString();
                DateHeureDePresence.Text = donnee.DateHeureDePresence.ToString();
                EstPresent.Text = donnee.EstPresent.ToString();

            }
        }
        private void ListParticipantAbsent_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipantAbsent.SelectedItem is MyApps.Application.ViewModels.PresenceViewModel donnee)
            {
                IdPresence.Text = donnee.IdPresence.ToString();
                IdSiteModule.Text = donnee.NomModule.ToString();
                IdParticipant.Text = donnee.NomParticipant.ToString();
                //NomModule.Text = donnee.NomModule.ToString();
                //NomParticipant.Text = donnee.NomParticipant.ToString();
                DateHeureDePresence.Text = donnee.DateHeureDePresence.ToString();
                EstPresent.Text = donnee.EstPresent.ToString();

            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            DateHeureDePresence.Text = "";
            EstPresent.Text = "";
            //ClearFormValues();
            //ModeIsEnabledTrue();
            DateHeureDePresence.IsEnabled = true;
            EstPresent.IsEnabled = true;
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            Presence element = new Presence();
            //Competence competence = new Competence();

            if (IdSiteModule.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom ");
                return;
            }
            if (IdParticipant.Text == "")
            {
                MessageBox.Show("Il faut selectionner un participant ");
                return;
            }
            if (DateHeureDePresence.Text == "")
            {
                MessageBox.Show("Il faut selectionner une date de presence ");
                return;
            }
            if (EstPresent.Text == "")
            {
                MessageBox.Show("Il faut preciser la presence d'un participant "); 
                return;
            }


            if (enregistre == "Ajouter")
            {

                //element.IdModuleInscription = short.Parse(IdModuleInscription.Text);
                element.IdSiteModule = (short)(idSiteModuleSelected);
                element.IdParticipant = (short)(idParticipantSelected);
                element.DateHeureDePresence = DateTime.Parse(DateHeureDePresence.Text).Date;
                element.EstPresent = bool.Parse(EstPresent.Text);
                foreach (var donne in MyApps.Application.Services.PresenceViewModelService.GetPresences())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((element.IdSiteModule == donne.IdSiteModule) && (element.IdParticipant == donne.IdParticipant) && (element.DateHeureDePresence== donne.DateHeureDePresence)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }

                }
                if (IdSiteModule.SelectedItem == null)
                {
                    return;
                }
                string nomModuleSelected = IdSiteModule.SelectedValue.ToString();

                foreach (var module in listeModulePlanning)
                {
                    //if (nomModuleSelected == module.NomModule + " : " + module.IdSiteModule)
                    //{
                    //    idSiteModuleSelected = module.IdSiteModule;
                    //    //NomSite.Text = module.NomSite;
                    //    //DateDébut.Text = module.DateDebutModule.ToString();
                    //    //DateFin.Text = module.DateFinModule.ToString();
                       
                    //}
                    if (((short)(idSiteModuleSelected) == module.IdSiteModule) && (element.DateHeureDePresence >= module.DateDebutModule) && (element.DateHeureDePresence <= module.DateFinModule)) // if already the items exist then rejects
                   
                    {
                        MessageBox.Show("date is not right" + module.DateDebutModule.ToString() + " : " + module.DateFinModule.ToString());
                        return;
                    }
                }
                MyApps.Domain.Service.PresenceService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                //element.IdModuleInscription = short.Parse(IdModuleInscription.Text); 
                element.IdSiteModule = (short)(idSiteModuleSelected);
                element.IdParticipant = (short)(idParticipantSelected);
                element.DateHeureDePresence = DateTime.Parse(DateHeureDePresence.Text).Date;
                element.EstPresent = bool.Parse(EstPresent.Text);

                MyApps.Domain.Service.PresenceService.Update(element);
            }

            IdParticipant.Text = "";
            DateHeureDePresence.Text = "";
            EstPresent.Text = "";
            ModeIsEnabledFalse();
            ////Participants Present sur la liste de presence par module  
            liste.Clear();
            liste = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBindParticipantPresent(liste);
            //PopulateAndBind(liste);

            //// Participant absent sur la liste de presence dans un module
            listeParticipantAbsent.Clear();
            listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetListParticipantAbsentPerModule((short)(idSiteModuleSelected));
            TotalEchoué.Text = listeParticipantAbsent.Count().ToString();
            PopulateAndBindParticipantAbsent(listeParticipantAbsent);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdSiteModule.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            enregistre = "Ajouter";
            DateHeureDePresence.Text = "";
            EstPresent.Text = "";
            //ClearFormValues();
            //ModeIsEnabledTrue();
            DateHeureDePresence.IsEnabled = true;
            EstPresent.IsEnabled = true;


        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            //le code pour signaler la presence de l'idParticipant dans la table Inscription on doit d'abord faire une vérification
            if (IdPresence.Text == "")
            {
                MessageBox.Show("Entrer l'identifiant à supprimer"); 
                return;
            }
            MyApps.Domain.Service.PresenceService.Delete(short.Parse(IdPresence.Text));

            ClearFormValues();
            ////Participants Present sur la liste de presence par module  
            liste.Clear();
            liste = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBindParticipantPresent(liste);
            //PopulateAndBind(liste);

            //// Participant absent sur la liste de presence dans un module
            listeParticipantAbsent.Clear();
            listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetListParticipantAbsentPerModule((short)(idSiteModuleSelected));
            TotalEchoué.Text = listeParticipantAbsent.Count().ToString();
            PopulateAndBindParticipantAbsent(listeParticipantAbsent);

        }
        private void ClearFormValues()
        {
            IdPresence.Text = "";
            IdSiteModule.Text = "";
            IdParticipant.Text = "";
            //NomModule.Text = "";
            //NomParticipant.Text = "";
            DateHeureDePresence.Text = "";
            EstPresent.Text = "";


        }
        private void ModeIsEnabledTrue()
        {
            IdPresence.IsEnabled = false;
            IdSiteModule.IsEnabled = true;
            IdParticipant.IsEnabled = true;
            //NomModule.IsEnabled = true;
            //NomParticipant.IsEnabled = true;
            DateHeureDePresence.IsEnabled = true;
            EstPresent.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdPresence.IsEnabled = false;
            IdSiteModule.IsEnabled = false;
            IdParticipant.IsEnabled = false;
            //NomModule.IsEnabled = false;
            //NomParticipant.IsEnabled = false;
            DateHeureDePresence.IsEnabled = false;
            EstPresent.IsEnabled = false;

        }
        //*******************************************************************************************************************
        //********************** La partie de combobox Nom de module et afficher les différentes liste************************
        private List<MyApps.Application.ViewModels.SitePlanningViewModel> SelectedNomModule()
        {

            foreach (var module in listeModulePlanning.OrderBy(a => a.NomModule))
            {

                IdSiteModule.Items.Add(module.NomModule + " : " + module.IdSiteModule);
            }

            return listeModulePlanning;
        }
        private void ComboBoxPerticipantPerModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdSiteModule.SelectedItem == null)
            {
                return;
            }
            string nomModuleSelected = IdSiteModule.SelectedValue.ToString();

            foreach (var module in listeModulePlanning)
            {
                if (nomModuleSelected == module.NomModule + " : " + module.IdSiteModule)
                {
                    idSiteModuleSelected = module.IdSiteModule;
                    NomSite.Text = module.NomSite;
                    DateDébut.Text = module.DateDebutModule.ToString();
                    DateFin.Text = module.DateFinModule.ToString();
                }
            }



            //IdModuleInscription.IsEnabled = false;
            //IdSiteModule.IsEnabled = true;
            ////IdParticipant.SelectedValue = "";
            //NomModule.Text = "";
            //NomParticipant.Text = "";
            ////DateInscription.Text = "";
            //// EstSurListeAttente.Text = "";

            //listeInscription.Clear();
            //ListElement.DataContext = clearListe;
            //IdModuleInscription.IsEnabled = false;
            //// IdModule.IsEnabled = false;
            ////IdParticipant.IsEnabled = false;
            //NomModule.IsEnabled = false;
            //NomParticipant.IsEnabled = false;
            ////DateInscription.IsEnabled = false;
            ////EstSurListeAttente.IsEnabled = false;

            listeInscription = MyApps.Application.Services.InscriptionViewModelService.GetParticipantPerModule((short)(idSiteModuleSelected));
            TotalParticipant.Text = listeInscription.Count().ToString();
            PopulateAndBindListeInscription(listeInscription);

            ////Participants Present sur la liste de presence par module  
            liste.Clear();
            liste = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBindParticipantPresent(liste);
            //PopulateAndBind(liste);

            //// Participant absent sur la liste de presence dans un module
            listeParticipantAbsent.Clear();
            listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetListParticipantAbsentPerModule((short)(idSiteModuleSelected));
            TotalEchoué.Text = listeParticipantAbsent.Count().ToString();
            PopulateAndBindParticipantAbsent(listeParticipantAbsent); 
        }


        //*********************************************************************************************************************
        //**************************** Gestion des modules*********************************************************************
        //*********************************************************************************************************************
        /// <summary>
        /// biding la liste de modules
        /// </summary>
        /// <param name="listeModules"></param>
        private void PopulateAndBindModule(List<MyApps.Application.ViewModels.SitePlanningViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElementModule.DataContext = listeItems;
        }

       

        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListElementModule_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListElementModule.SelectedItem is MyApps.Application.ViewModels.SitePlanningViewModel donnee) 
            {
                
                IdSiteModule.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString();
            }
            //if (enregistre != "Ajouter")
            //{
            //    IdFormateur.SelectedValue = "";
            //}

        }
        private void RechercherModule_Click(object sender, RoutedEventArgs e)
        {

            if (NomRechercherModule.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");

                listeModulePlanning = MyApps.Application.Services.SitePlanningViewModelService.GetSitePlanning();
                PopulateAndBindModule(listeModulePlanning);
                return;

            }

            listeModulePlanning = MyApps.Application.Services.SitePlanningViewModelService.SearchMethodByName(NomRechercherModule.Text);
            PopulateAndBindModule(listeModulePlanning);
        }
        private void ReSetListModule_Click(object sender, RoutedEventArgs e)
        {
            NomRechercherModule.Text = "";
            listeModulePlanning = MyApps.Application.Services.SitePlanningViewModelService.SearchMethodByName(NomRechercherModule.Text); 
            PopulateAndBindModule(listeModulePlanning);
        }

        //******************************************************************************************************************************
        //**************************************************** gestion de datagrid de la liste de participant par module
        //******************************************************************************************************************************
        private void ListElement_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {


            if (ListElement.SelectedItem is MyApps.Application.ViewModels.InscriptionViewModel donnee)
            {
                if (donnee.IdSiteModule == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }
                //IdModuleInscription.Text = donnee.IdModuleInscription.ToString();
                IdSiteModule.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString();
                IdParticipant.Text = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();
               // NomModule.Text = donnee.NomModule.ToString();
                // NomParticipant.Text = MyApps.Domain.Service.InscriptionService.GetIdNational(donnee.IdParticipant).ToString();
                //DateInscription.Text = donnee.DateInscription.ToString();
                //EstSurListeAttente.Text = donnee.EstSurListeAttente.ToString();
                //IdParticipants.SelectedItem = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();

            }


        }

        private void IdParticipant_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IdParticipant.Text == null)
            {
                //MessageBox.Show("Please select the name of the module");
                return;
                //IdModule.SelectedValue = "";
            }
            string nomParticipantSelected = IdParticipant.Text.ToString();
            foreach (var participant in listeParticipant)
            {
                if (nomParticipantSelected == participant.NomParticipant + " : " + participant.IdNational)
                {
                    idParticipantSelected = participant.IdParticipant;
                   // NomParticipant.Text = participant.IdNational.ToString();
                }
            }
        }
    }
}
