using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace SWOMT.Views
{
    /// <summary>
    /// Logique d'interaction pour Presences.xaml
    /// </summary>
    public partial class Presences : Page
    {    
        string enregistre;
        int idSiteModuleSelected;
        int idParticipantSelected;
        string copyNomFormateur;
        List<MyApps.Application.ViewModels.PresenceViewModel> liste = new List<MyApps.Application.ViewModels.PresenceViewModel>();
        /// <summary>
        /// Un constructeur de classe presence 
        /// </summary>
        /// <param name="nomFormateur"></param>
        public Presences(string nomFormateur)
        {
            InitializeComponent();
            liste = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            var listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.GetSitePlanning();
            copyNomFormateur = nomFormateur;
            listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.AfficherModulePerFormateur(copyNomFormateur);
            PopulateAndBindModule(listeModulePlanning);
            if (MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)nomFormateur) == "Admin"
                || MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)nomFormateur) == "Secrétaire")
            {

                listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.GetSitePlanning();
                PopulateAndBindModule(listeModulePlanning);
                RechercherModule.IsEnabled = true;
                ReSetModule.IsEnabled = true;
                NomRechercherModule.IsEnabled = true;
                ModuleSiteTextBox.IsEnabled = false;
                Valider.Visibility = Visibility.Hidden;

            }
            this.SelectedNomModule();
            EstPresent.Items.Add("True");
            EstPresent.Items.Add("False");

        }
       
        /// <summary>
        /// Binding the liste of the participant inscrit dans un module 
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindListeInscription(List<MyApps.Application.ViewModels.PresenceViewModel> listeItems)
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
                IdSiteModule.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString();
                IdParticipant.Text = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();
                //NomModule.Text = donnee.NomModule.ToString();
                //NomParticipant.Text = donnee.NomParticipant.ToString();
                DateHeureDePresence.Text = donnee.DateHeureDePresence.ToString();
                EstPresent.Text = donnee.EstPresent.ToString();
            }
        }
        /// <summary>
        /// les données à modifier en cas de modification dans la liste de participants Absent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListParticipantAbsent_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipantAbsent.SelectedItem is MyApps.Application.ViewModels.PresenceViewModel donnee)
            {
                IdPresence.Text = donnee.IdPresence.ToString();
                IdSiteModule.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString();
                IdParticipant.Text = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();
                //NomModule.Text = donnee.NomModule.ToString();
                //NomParticipant.Text = donnee.NomParticipant.ToString();
                DateHeureDePresence.Text = donnee.DateHeureDePresence.ToString();
                EstPresent.Text = donnee.EstPresent.ToString();
            }
        }
        /// <summary>
        /// en cas de vouloir ajouter un élément dans la liste des presences 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Méthode pour binding la liste de participants absents
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindParticipantAbsent(List<MyApps.Application.ViewModels.PresenceViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipantAbsent.DataContext = listeItems;
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            Presence element = new Presence();
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
                element.IdSiteModule = (short)(idSiteModuleSelected);
                element.IdParticipant = (short)(idParticipantSelected);
                element.DateHeureDePresence = DateTime.Parse(DateHeureDePresence.Text).Date;
                element.EstPresent = bool.Parse(EstPresent.Text);
                //Vérifier si la date de presence est comprise entre la date de début et la date de fin 
                DateTime RemoveOneDay = DateTime.Parse(DateDébut.Text).Date;
                DateTime DateDébutRemovedOneDay = RemoveOneDay.AddDays(-1);
                //Vérifier si la date de presence est comprise entre la date de début et la date de fin 
                if ((element.DateHeureDePresence <= DateDébutRemovedOneDay) || (element.DateHeureDePresence > DateTime.Parse(DateFin.Text).Date))

                {
                    MessageBox.Show("La date de presence doit être comprise entre  : La date de Début : " + DateTime.Parse(DateDébut.Text) +
                       " et la date de Fin : " + DateTime.Parse(DateFin.Text));
                    return;
                }
                //Eviter de coter la présence avant la date du jour 
                DateTime RemoveOne = DateTime.Parse(DateHeureDePresence.Text).Date;

                if (DateTime.Now <= RemoveOne)

                {
                    MessageBox.Show("La date de presence ne doit pas être supérieur à la date d'aujourd'hui : La date du jour : " + DateTime.Now);
                    return;
                }

                //code pour éviter des doublons dans les presences et respecter la date de début et date de fin de modules
                foreach (var donne in MyApps.Application.Services.PresenceViewModelService.GetPresences())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((element.IdSiteModule == donne.IdSiteModule) && (element.IdParticipant == donne.IdParticipant) && (element.DateHeureDePresence == donne.DateHeureDePresence)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");

                        return;
                    }

                }

                MyApps.Domain.Service.PresenceService.Create(element);

            }

            if (enregistre == "Modifier")
            {
                if (IdPresence.Text == "")
                {
                    MessageBox.Show("Veuillez seléctionner un élément à modifier dans la liste des participants réussis ou échoués");
                    IdParticipant.Text = "";
                    DateHeureDePresence.Text = "";
                    EstPresent.Text = "";
                    ModeIsEnabledFalse();
                    return;
                }
                element.IdPresence = short.Parse(IdPresence.Text);
                element.IdSiteModule = (short)(idSiteModuleSelected);
                element.IdParticipant = (short)(idParticipantSelected);
                element.DateHeureDePresence = DateTime.Parse(DateHeureDePresence.Text).Date;
                element.EstPresent = bool.Parse(EstPresent.Text);
                DateTime RemoveOneDay = DateTime.Parse(DateDébut.Text).Date;
                DateTime DateDébutRemovedOneDay = RemoveOneDay.AddDays(-1);
                //Vérifier si la date de presence est comprise entre la date de début et la date de fin 
                if ((element.DateHeureDePresence <= DateDébutRemovedOneDay) || (element.DateHeureDePresence > DateTime.Parse(DateFin.Text).Date))

                {
                    MessageBox.Show("La date de presence doit être comprise entre  : La date de Début : " + DateTime.Parse(DateDébut.Text) +
                       " et la date de Fin : " + DateTime.Parse(DateFin.Text));
                    return;
                }

                //Eviter de coter la présence avant la date du jour 
                DateTime RemoveOne = DateTime.Parse(DateHeureDePresence.Text).Date;

                if (DateTime.Now <= RemoveOne)

                {
                    MessageBox.Show("La date de presence ne doit pas être supérieur à la date d'aujourd'hui : La date du jour : " + DateTime.Now);
                    return;
                }
                foreach (var donne in MyApps.Application.Services.PresenceViewModelService.GetPresences())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((element.IdSiteModule == donne.IdSiteModule) && (element.IdParticipant == donne.IdParticipant) && (element.DateHeureDePresence == donne.DateHeureDePresence)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }

                }

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

            //// Participant absent sur la liste de presence dans un module 
            var listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetPresences();
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

            //// Participant absent sur la liste de presence dans un module
            var listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetPresences();
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
        private List<MyApps.Application.ViewModels.PresenceViewModel> SelectedNomModule()
        {
            var listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.GetSitePlanning();
            foreach (var module in listeModulePlanning.OrderBy(a => a.NomModule))
            {

                IdSiteModule.Items.Add(module.NomModule + " : " + module.IdSiteModule);
            }

            return listeModulePlanning;
        }
        /// <summary>
        /// Méthode pour récuperer l'identifiant seléctionné 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxPerticipantPerModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<MyApps.Application.ViewModels.PresenceViewModel> listeInscription = new List<MyApps.Application.ViewModels.PresenceViewModel>();
            if (IdSiteModule.SelectedItem == null)
            {
                return;
            }
            string nomModuleSelected = IdSiteModule.SelectedValue.ToString();
            var listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.GetSitePlanning();
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
            //Pour chaque module selectionné récuperer les inscriptins liées
            listeInscription = MyApps.Application.Services.PresenceViewModelService.GetParticipantPerModule((short)(idSiteModuleSelected));
            
            PopulateAndBindListeInscription(listeInscription);

            ////Participants Present sur la liste de presence par module  
            liste.Clear();
            liste = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBindParticipantPresent(liste);
            //PopulateAndBind(liste);

            //// Participant absent sur la liste de presence dans un module
            var listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetPresences();
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
        private void PopulateAndBindModule(List<MyApps.Application.ViewModels.PresenceViewModel> listeItems)
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
            if (ListElementModule.SelectedItem is MyApps.Application.ViewModels.PresenceViewModel donnee)
            {
                IdSiteModule.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString();
            }
            DateDePresenceValiderTous.IsEnabled = true;
        }
        /// <summary>
        /// Méthode pour faire une rechercher à partir du nom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RechercherModule_Click(object sender, RoutedEventArgs e)
        {
            var listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.GetSitePlanning();

            if (NomRechercherModule.Text == "" && (MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)copyNomFormateur) == "Admin"
                || MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)copyNomFormateur) == "Secrétaire")) 
            {
                MessageBox.Show("Vous n'avez pas saisie le mot à chercher");
                listeModulePlanning.Clear();
                listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.GetSitePlanning();
                PopulateAndBindModule(listeModulePlanning);
                Valider.Visibility = Visibility.Hidden;
                ModuleSiteTextBox.IsEnabled = false;
                return;
            }
            else if (NomRechercherModule.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");
                return;

            }
            else if (NomRechercherModule.Text != "" && (MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)copyNomFormateur) == "Admin"
                || MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)copyNomFormateur) == "Secrétaire"))
            {
               
                listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.SearchMethodByName(NomRechercherModule.Text);
                PopulateAndBindModule(listeModulePlanning);
            }
            else if (NomRechercherModule.Text == "")
            {
                var listeperFormateur = MyApps.Application.Services.PresenceViewModelService.AfficherModulePerFormateur(copyNomFormateur);
                listeperFormateur = MyApps.Application.Services.PresenceViewModelService.SearchMethodByName(NomRechercherModule.Text);
                PopulateAndBindModule(listeperFormateur);
            }

        }
        /// <summary>
        /// Méthode pour initialiser la liste 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReSetListModule_Click(object sender, RoutedEventArgs e)
        {
            NomRechercherModule.Text = "";
         
           var listeModulePlanning = MyApps.Application.Services.PresenceViewModelService.AfficherModulePerFormateur(copyNomFormateur);
            PopulateAndBindModule(listeModulePlanning);
        }

        //******************************************************************************************************************************
        //**************************************************** gestion de datagrid de la liste de participant par module
        //******************************************************************************************************************************
        private void ListElement_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.PresenceViewModel donnee)
            {
                if (donnee == null)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;                 
                }
                IdSiteModule.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString();
                IdParticipant.Text = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();

            }
        }

        private void IdParticipant_TextChanged(object sender, TextChangedEventArgs e)
        {
            liste = MyApps.Application.Services.PresenceViewModelService.GetParticipants();
            if (IdParticipant.Text == null)
            {
                //MessageBox.Show("Please select the name of the module");
                return;
                //IdModule.SelectedValue = "";
            }
            string nomParticipantSelected = IdParticipant.Text.ToString();
            foreach (var participant in liste)
            {
                if (nomParticipantSelected == participant.NomParticipant + " : " + participant.IdNational)
                {
                    idParticipantSelected = participant.IdParticipant;
                    // NomParticipant.Text = participant.IdNational.ToString();
                }
            }
        }
        //*****************************************************************************************************************************
        //**************************************** code pour faire une rechercher dans la  liste des presences*************************

        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {
            var listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            if (NomRechercher.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");

                liste = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
                //    TotalRéussi.Text = liste.Count().ToString();
                PopulateAndBindParticipantPresent(liste);
                listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetListParticipantAbsentPerModule((short)(idSiteModuleSelected));
                //    TotalEchoué.Text = listeParticipantAbsent.Count().ToString();
                PopulateAndBindParticipantAbsent(listeParticipantAbsent);
                return;
            }
            liste = this.SearchMethodByNameListPresent(NomRechercher.Text);
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBindParticipantPresent(liste);
            listeParticipantAbsent = this.SearchMethodByNameListAbsent(NomRechercher.Text);
            TotalEchoué.Text = listeParticipantAbsent.Count().ToString();
            PopulateAndBindParticipantAbsent(listeParticipantAbsent);
        }
        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {
            var listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            NomRechercher.Text = "";
            liste = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBindParticipantPresent(liste);
            listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetListParticipantAbsentPerModule((short)(idSiteModuleSelected));
            TotalEchoué.Text = listeParticipantAbsent.Count().ToString();
            PopulateAndBindParticipantAbsent(listeParticipantAbsent);
        }

        /// <summary>
        /// Methode pour faire une recherche dans la liste 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.PresenceViewModel> SearchMethodByNameListPresent(string searchString)
        {
            List<MyApps.Application.ViewModels.PresenceViewModel> Liste = new List<MyApps.Application.ViewModels.PresenceViewModel>();
            var GetListe = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
            var assets = from s in GetListe
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(s => s.NomParticipant.ToUpper().Contains(searchString.ToUpper()) || s.DateHeureDePresence.ToString().Contains(searchString.ToString()));
            }

            return assets.ToList();
        }
        /// <summary>
        /// trouver la liste des participants absent et faire une recherche par nom participant ou date de presence 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Liste trié</returns>
        private List<MyApps.Application.ViewModels.PresenceViewModel> SearchMethodByNameListAbsent(string searchString)
        {
            List<MyApps.Application.ViewModels.PresenceViewModel> Liste = new List<MyApps.Application.ViewModels.PresenceViewModel>();
            var GetListe = MyApps.Application.Services.PresenceViewModelService.GetListParticipantAbsentPerModule((short)(idSiteModuleSelected));
            var assets = from s in GetListe
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(s => s.NomParticipant.ToUpper().Contains(searchString.ToUpper()) || s.DateHeureDePresence.ToString().Contains(searchString.ToString()));
            }

            return assets.ToList();
        }

        private void ValiderPresence_Click(object sender, RoutedEventArgs e)
        {
            Presence element = new Presence();

            if (DateDePresenceValiderTous.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Vous n'avez pas préciser la date de presence !!!. Sinon, le système par défaut validera la date courante ", "", MessageBoxButton.OKCancel);

                switch (result)
                {
                    case MessageBoxResult.OK:
                        MessageBox.Show("Valider la date courante", "My App");
                        break;

                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Annuler l'opération...", "My App");
                        return;
                }

            }
           
            if (ListElement.ItemsSource == null)
            {
                MessageBox.Show("Attention !!! Il y a pas des participants, vérifier les participant dans le module concerné. ");
                return;
            }

            liste = MyApps.Application.Services.PresenceViewModelService.GetParticipantPerModule((short)(idSiteModuleSelected));

            foreach (MyApps.Application.ViewModels.PresenceViewModel rv in ListElement.ItemsSource)
            {

                element.IdSiteModule = (short)rv.IdSiteModule;
                element.IdParticipant = (short)rv.IdParticipant;

                if (DateDePresenceValiderTous.Text == "")
                {
                    element.DateHeureDePresence = DateTime.Now;
                }
                else
                {
                    element.DateHeureDePresence = DateTime.Parse(DateDePresenceValiderTous.Text).Date;
                }
                if ((bool)rv.CheckBoxEstPresent)
                {
                    element.EstPresent = true;
                }
                else
                {
                    element.EstPresent = false;
                }
                DateTime RemoveOneDay = DateTime.Parse(DateDébut.Text).Date;
                DateTime DateDébutRemovedOneDay = RemoveOneDay.AddDays(-1);
                //Vérifier si la date de presence est comprise entre la date de début et la date de fin 
                if ((element.DateHeureDePresence <= DateDébutRemovedOneDay) || (element.DateHeureDePresence > DateTime.Parse(DateFin.Text).Date))

                {
                    MessageBox.Show("La date de presence doit être comprise entre  : La date de Début : " + DateTime.Parse(DateDébut.Text) +
                       " et la date de Fin : " + DateTime.Parse(DateFin.Text));
                    return;
                }
                DateTime RemoveOne = DateTime.Parse(DateDePresenceValiderTous.Text).Date;
                if (DateTime.Now <= RemoveOne)

                {
                    MessageBox.Show("La date de presence ne doit pas être supérieur à la date d'aujourd'hui : La date du jour : " + DateTime.Now);
                    DateDePresenceValiderTous.Text = "";
                    return;
                }
                if (MyApps.Application.Services.PresenceViewModelService.GetPresences().Any(a=>a.IdSiteModule==element.IdSiteModule && a.IdParticipant==element.IdParticipant && a.DateHeureDePresence == element.DateHeureDePresence))
                {
                    MessageBox.Show("Vous esseyez de valider les presences deux fois. ");

                    return;
                }
               
                MyApps.Domain.Service.PresenceService.Create(element);

            }

            DateDePresenceValiderTous.Text = "";

            IdParticipant.Text = "";
            DateHeureDePresence.Text = "";
            EstPresent.Text = "";
            ModeIsEnabledFalse();
            ////Participants Present sur la liste de presence par module  
            liste.Clear();
            liste = MyApps.Application.Services.PresenceViewModelService.GetListParticipantPresentPerModule((short)(idSiteModuleSelected));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBindParticipantPresent(liste);

            //// Participant absent sur la liste de presence dans un module
            var listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            listeParticipantAbsent.Clear();
            listeParticipantAbsent = MyApps.Application.Services.PresenceViewModelService.GetListParticipantAbsentPerModule((short)(idSiteModuleSelected));
            TotalEchoué.Text = listeParticipantAbsent.Count().ToString();
            PopulateAndBindParticipantAbsent(listeParticipantAbsent);
        }

        private void SelectedDateChanged_DateDePresence(object sender, SelectionChangedEventArgs e)
        {
            
            //// DateDePresenceValiderTous.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse(DateDébut.Text), DateTime.Parse(DateFin.Text)));
            DateTime RemoveOneDay = DateTime.Parse(DateDébut.Text).Date;
            DateTime DateDébutRemovedOneDay = RemoveOneDay.AddDays(-1);
           DateTime dateAverifier = DateTime.Parse(DateDePresenceValiderTous.Text).Date;
            //Vérifier si la date de presence est comprise entre la date de début et la date de fin 
            if ((dateAverifier <= DateDébutRemovedOneDay) || (dateAverifier > DateTime.Parse(DateFin.Text).Date))

            {
                MessageBox.Show("La date de presence doit être comprise entre  : La date de Début : " + DateTime.Parse(DateDébut.Text) +
                   " et la date de Fin : " + DateTime.Parse(DateFin.Text));
                return;
            }
            //Eviter de coter la présence avant la date du jour 
            DateTime RemoveOne = DateTime.Parse(DateDePresenceValiderTous.Text).Date;

            if (DateTime.Now <= RemoveOne)

            {
                MessageBox.Show("La date de presence ne doit pas être supérieur à la date d'aujourd'hui : La date du jour : " + DateTime.Now);
                DateDePresenceValiderTous.Text = "";
                return;
            }
        }
        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            List<MyApps.Application.ViewModels.PresenceViewModel> checkedList = new List<MyApps.Application.ViewModels.PresenceViewModel>();

            if (CheckAll.IsChecked == true)
            {
                liste = MyApps.Application.Services.PresenceViewModelService.GetParticipantPerModule((short)(idSiteModuleSelected));

                foreach (var itemList in liste)
                {
                    MyApps.Application.ViewModels.PresenceViewModel vm = new MyApps.Application.ViewModels.PresenceViewModel()
                    {
                        IdModuleInscription = itemList.IdModuleInscription,
                        IdSiteModule = itemList.IdSiteModule,
                        IdParticipant = itemList.IdParticipant,
                        NomModule = MyApps.Domain.Service.InscriptionService.GetNomModule(itemList.IdSiteModule),
                        NomParticipant = MyApps.Domain.Service.InscriptionService.GetNomParticipant(itemList.IdParticipant),
                        IdNational = MyApps.Domain.Service.InscriptionService.GetIdNational(itemList.IdParticipant),
                        DateInscription = itemList.DateInscription,
                        CheckBoxEstPresent = true,
                    };
                    checkedList.Add(vm);
                }
                PopulateAndBindListeInscription(checkedList);
            }
            else
            {
                PopulateAndBindListeInscription(liste);
            }

        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            
            PrintDialog printDlg = new PrintDialog();
            // Create a FlowDocument dynamically.  
            FlowDocument doc = CreateFlowDocument();
            doc.Name = "ListeAbsence";
            // Create IDocumentPaginatorSource from FlowDocument  
            IDocumentPaginatorSource idpSource = doc;
            // Call PrintDocument method to send document to printer  
            printDlg.PrintDocument(idpSource.DocumentPaginator, "ListParticipantAbsent"); 
        }
        private FlowDocument CreateFlowDocument()
        {
            // Create a FlowDocument  
            FlowDocument doc = new FlowDocument();
            // Create a Section  
            Section sec = new Section();
            // Create first Paragraph  
            Paragraph p1 = new Paragraph();
            // Create and add a new Bold, Italic and Underline  
            Bold bld = new Bold();
            bld.Inlines.Add(new Run(ListParticipantAbsent.ToString()));
            Italic italicBld = new Italic();
            italicBld.Inlines.Add(bld);
            Underline underlineItalicBld = new Underline();
            underlineItalicBld.Inlines.Add(italicBld);
            // Add Bold, Italic, Underline to Paragraph  
            p1.Inlines.Add(underlineItalicBld);
            // Add Paragraph to Section  
            sec.Blocks.Add(p1);
            // Add Section to FlowDocument  
            doc.Blocks.Add(sec);
            return doc;
        }

    }
}
