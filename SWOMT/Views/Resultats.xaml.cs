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
    /// Logique d'interaction pour Resultats.xaml
    /// </summary>
    public partial class Resultats : Page
    {
        string nomevent;
        string enregistre;
        int idSiteModuleSelected;
        int idFormateurSelected;
        string nomFormateurProgrammeExamen;
         List<MyApps.Application.ViewModels.ResultatViewModel> liste = new List<MyApps.Application.ViewModels.ResultatViewModel>();
        public Resultats(string nomFormateur, string userRole)  
        {
            InitializeComponent();
            liste = MyApps.Application.Services.ResultatsVieModelService.SearchByNameNomFormateur(nomFormateur);
            nomFormateurProgrammeExamen = nomFormateur;
            
            PopulateAndBindExamen(liste);
            if ((string)userRole == "Admin" || (string)userRole=="Secrétaire")
            {

                //BoutonInscription.IsEnabled=false;
                liste = MyApps.Application.Services.ResultatsVieModelService.GetExamens(); 
                PopulateAndBindExamen(liste);
                Rechercher.IsEnabled = true;
                ReSet.IsEnabled = true;
                NomRechercher.IsEnabled =true; 
            }
           bool  nomToCheckFormateur = MyApps.Domain.Service.FormateurService.GetFormateurNom(nomFormateur);
           
                if (!nomToCheckFormateur)
                {
                    EvenementToPost.Visibility = Visibility.Collapsed;
                ExamenTextBox.IsEnabled = false;
                ModuleSiteTextBox.IsEnabled = false;
                return;
                }
          

            if (MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)nomFormateur) != "Formateur")
            {
                
                ExamenTextBox.IsEnabled = false;
                ModuleSiteTextBox.IsEnabled = false;
            }
            this.SelectedNomModule();
            this.SelectedNomModuleExamenPlanned();
            //this.SelectedNomFormateur();
            IdFormateur.Items.Add(nomFormateur); 
            EstPresent.Items.Add("Présent");

            EstPresent.Items.Add("Absent");
            ParticipantRéussi.Items.Add("True");
            ParticipantRéussi.Items.Add("False");
            nomevent = nomFormateur;
           
        }

        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="liste"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.ResultatViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipantRéussi.DataContext = listeItems;
        }

        /// <summary>
        /// binding la liste des participant echoués
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindParticipantFailed(List<MyApps.Application.ViewModels.ResultatViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
           
            ListParticipantFailed.DataContext = listeItems;            
        }
        private void PopulateAndBindListeInscription(List<MyApps.Application.ViewModels.ResultatViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElement.DataContext = listeItems;
            
        }
        /// <summary>
        /// the method to fill in the combobox of the list items for selection 
        /// </summary>
        /// <returns></returns>      
        private List<MyApps.Application.ViewModels.ResultatViewModel> SelectedNomModuleExamenPlanned()
        {
            liste = MyApps.Application.Services.ResultatsVieModelService.GetExamens();
            foreach (var module in liste)
            {
                IdSiteModule.Items.Add(module.NomModule + " : " + module.IdSiteModule + " : " + module.DateExamen);
               
            }
            
            return liste;
        }
        /// <summary>
        /// afffichage après la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListElement_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.ResultatViewModel donnee)
            {
                if (donnee.IdSiteModule == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                }

                IdModuleInscription.Text = donnee.IdModuleInscription.ToString();
                NomModule.Text = donnee.NomModule.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
            }           
        }
        /// <summary>
        /// bouton pour ajouter les champs des saisies 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            
            ClearFormValues();
            ModeIsEnabledTrue();
            IdModuleInscription.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            ParticipantRéussi.IsEnabled = false;

        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            Resultat element = new Resultat();
            //Competence competence = new Competence();

            if (IdExamens.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom ");
                return;
            }
            if (IdModuleInscription.Text == "")
            {
                MessageBox.Show("Il faut saisir identifiant de participant ");
                return;
            }
            if (Points.Text == "")
            {
                MessageBox.Show("Il faut saisir des résultats obtenus ");
                return;
            }
            if (EstPresent.Text == "")
            {
                MessageBox.Show("Veuillez préciser si le participant s'est présenté ou pas à l'examen");
                return;
            }


            // Programmer un examen
            if (enregistre == "Ajouter")
            {

                //element.IdSiteModule = short.Parse(IdSiteModule.Text);
                element.IdExamen = short.Parse(IdExamens.Text);
                element.IdModuleInscription = short.Parse(IdModuleInscription.Text);
                element.Points = short.Parse(Points.Text);
                if (EstPresent.SelectedItem.ToString() == "Présent")
                {
                    EstPresent.Text = "True";
                }
                if (EstPresent.SelectedItem.ToString() == "Absent")
                {
                    EstPresent.Text = "False";
                }

                element.EstPresent = bool.Parse(EstPresent.Text);
                    if (element.Points >= 50) // si les points obtenues inférieurs à 50 % le participant à échouer
                    {
                        element.ParticipantRéussi = true;
                    }
                    else
                    {
                    element.ParticipantRéussi = false;
                    }
               
                foreach (var donne in MyApps.Application.Services.ResultatsVieModelService.GetResultats())
                {
                    //avoid the duplicate datas in the list of results 
                    if ((element.IdExamen == donne.IdExamen) && (element.IdModuleInscription == donne.IdModuleInscription)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }                
                MyApps.Domain.Service.ResultatService.Create(element); 
            }
            // modifier les points d'un examen programmé
            if (enregistre == "Modifier")
            {

                element.IdResultat = short.Parse(IdResultat.Text);  
                element.IdExamen = short.Parse(IdExamens.Text);
                element.IdModuleInscription = short.Parse(IdModuleInscription.Text); 
                element.Points = short.Parse(Points.Text);
                if (EstPresent.SelectedItem.ToString() == "Présent")
                {
                    EstPresent.Text = "True";
                }
                if (EstPresent.SelectedItem.ToString() == "Absent")
                {
                    EstPresent.Text = "False";
                }
                element.EstPresent = bool.Parse(EstPresent.Text);
                if (element.Points >= 50)
                {
                    element.ParticipantRéussi = true;
                }
                else
                {
                    element.ParticipantRéussi = false;
                }
                //valider les modifications

                MyApps.Domain.Service.ResultatService.Update(element); 
            }


            ModeIsEnabledFalse();
            ClearFormValues();
            
            liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetExamens(); 
            //Participant réussi dans un module 
           // liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantRéussi(short.Parse(IdExamens.Text));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBind(liste);

            // Participant échoué dans un module
           // liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantEchoué(short.Parse(IdExamens.Text));
            TotalEchoué.Text = liste.Count().ToString();
            PopulateAndBindParticipantFailed(liste); 
        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdExamens.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
          
            //ClearFormValues();
            ModeIsEnabledTrue();
            IdModuleInscription.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            ParticipantRéussi.IsEnabled = false;

        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            //le code pour signaler la presence de l'id on doit d'abord faire une vérification
            if (IdResultat.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un élément dans la liste à supprimer");
                return;
            }
           
            MyApps.Domain.Service.ResultatService.Delete(short.Parse(IdResultat.Text));
            //Participant réussi dans un module 
            // liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantRéussi(short.Parse(IdExamens.Text));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBind(liste);

            // Participant échoué dans un module
            //liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantEchoué(short.Parse(IdExamens.Text));
            TotalEchoué.Text = liste.Count().ToString();
            PopulateAndBindParticipantFailed(liste);
            liste = MyApps.Application.Services.ResultatsVieModelService.GetResultats();
            //PopulateAndBind(liste);
            ClearFormValues();
            ModeIsEnabledFalse();

        }
        private void ClearFormValues()
        {
           // IdResultat.Text = "";
           // IdExamens.Text = "";
            IdModuleInscription.Text = "";
            NomModule.Text = "";
            NomParticipant.Text = "";
            Points.Text = "";
            EstPresent.Text = "";
            ParticipantRéussi.Text = "";

        }
        private void ModeIsEnabledTrue()
        {
            //IdResultat.IsEnabled =false;
            IdExamens.IsEnabled = false;
            IdModuleInscription.IsEnabled =true;
            NomModule.IsEnabled =true;
            NomParticipant.IsEnabled=true;
            Points.IsEnabled = true;
            EstPresent.IsEnabled = true;
            ParticipantRéussi.IsEnabled =true;

        }
        private void ModeIsEnabledFalse()
        {
            //IdResultat.IsEnabled = false;
            IdExamens.IsEnabled = false;
            IdModuleInscription.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            Points.IsEnabled = false;
            EstPresent.IsEnabled = false;
            ParticipantRéussi.IsEnabled = false;

        }

        // filling the list of the examens and plan for the exams : The right side of the page
        //--------------------------------------------------------------------------------------------------------
        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBindExamen(List<MyApps.Application.ViewModels.ResultatViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListExamen.DataContext = listeItems;
        }
        /// <summary>
        /// la méthode qui passe les élement de la liste dans la combobox idsite 
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.ResultatViewModel> SelectedNomModule()
        {
            liste = MyApps.Application.Services.ResultatsVieModelService.AfficherModulePerFormateur(nomFormateurProgrammeExamen); 
            foreach (var module in liste) 
            {

                IdSite.Items.Add(module.NomModule + " : " + module.IdSiteModule);
            }

            return liste;
        }
        /// <summary>
        /// méthode de manipuler le combobox de IdSide dans la partie Examen de gauche 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboxBoxIdSiteModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            liste = MyApps.Application.Services.ResultatsVieModelService.AfficherModulePerFormateur(nomFormateurProgrammeExamen);
            if (IdSite.SelectedItem == null)
            {
                return;
            }
            string nomModuleSelected = IdSite.SelectedValue.ToString();

            foreach (var module in liste) 
            {
                if (nomModuleSelected == module.NomModule + " : " + module.IdSiteModule) 
                {
                    idSiteModuleSelected = module.IdSiteModule;
               
                }
            }

        }

        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListExamen_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            
            if (ListExamen.SelectedItem is MyApps.Application.ViewModels.ResultatViewModel donnee)
            {
                //pour eviter le système de cracher quant on selectionne le vide
                if (donnee.IdSiteModule == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }

                IdExamen.Text = donnee.IdExamen.ToString();
                IdSite.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString() ; 
                NomModule.Text = donnee.NomModule.ToString();
                DateExamen.Text = donnee.DateExamen.ToString();
                IdSiteModule.Text= donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString() + " : "+ donnee.DateExamen.ToString();

                DateTime RemoveOneDay = DateTime.Parse(DateExamen.Text).Date;
                //Vérifier si la date de coter un examen est superieur à la date d'examen  
                if (DateTime.Now <= RemoveOneDay) 
                {                   
                    ajouter.IsEnabled=false; 
                }
                else
                {
                    ajouter.IsEnabled = true;
                }
            }           
        }

        private void AjouterExamen_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValuesExamen();
            ModeIsEnabledTrueExamen();
            IdExamen.IsEnabled = false;
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjourExamen_Click(object sender, RoutedEventArgs e)
        {
            Examan element = new Examan();
            //Competence competence = new Competence();

            if (IdSite.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom de la competence");
                return;
            }
            if (DateExamen.Text == "")
            {
                MessageBox.Show("Veuillez saisir la date de l'examen");
                return;
            }
            DateTime dateExamen;
            if (!DateTime.TryParse(DateExamen.Text, out dateExamen))
            {
                MessageBox.Show("Format du date est incorrect SVP !");
                return;
            }
            if (enregistre == "Ajouter")
            {

                //element.IdExamen = short.Parse(IdExamen.Text);
                element.IdSiteModule = (short)(idSiteModuleSelected);
                element.DateExamen = DateTime.Parse(DateExamen.Text).Date;

                // la date d'examen doit etre superieur à la date courante
                if(element.DateExamen < DateTime.Now)
                {
                    MessageBox.Show("La date ne doit pas etre inférieur à la date courante");
                    return;
                }

                foreach (var donne in MyApps.Application.Services.ResultatsVieModelService.GetExamens())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((element.IdSiteModule == donne.IdSiteModule) && (element.DateExamen == donne.DateExamen)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }

                MyApps.Domain.Service.ExamenService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdExamen = short.Parse(IdExamen.Text);
                element.IdSiteModule = (short)(idSiteModuleSelected);

                element.DateExamen = DateTime.Parse(DateExamen.Text).Date;
                if (element.DateExamen < DateTime.Now)
                {
                    MessageBox.Show("La date ne doit pas etre inférieur à la date courante");
                    return;
                }
                foreach (var donne in MyApps.Application.Services.ResultatsVieModelService.GetExamens())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((element.IdSiteModule == donne.IdSiteModule) && (element.DateExamen == donne.DateExamen)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }

                MyApps.Domain.Service.ExamenService.Update(element);
            }
           
            liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.SearchByNameNomFormateur(nomFormateurProgrammeExamen);
            PopulateAndBindExamen(liste);
            ClearFormValuesExamen();
            ModeIsEnabledFalseExamen();
            IdSiteModule.Items.Clear();
            this.SelectedNomModuleExamenPlanned();
        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierExamen_Click(object sender, RoutedEventArgs e)
        {

            if (IdExamen.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrueExamen();
            IdExamen.IsEnabled = false;
        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerExamen_Click(object sender, RoutedEventArgs e)
        {
            //to verify the id of the items to delete 
            if (IdExamen.Text == "")
            {
                MessageBox.Show("Veuillez selectionner l'ID à supprimer");
                return;
            }
            foreach (var donne in MyApps.Application.Services.ResultatsVieModelService.GetResultats())
            {
                if ((short.Parse(IdExamen.Text) == donne.IdExamen))
                {
                    MessageBox.Show("Identiant de l'examen est associé à un élément dans la table Résultats");
                    ClearFormValuesExamen();
                    return;
                }
            }

            MyApps.Domain.Service.ExamenService.Delete(short.Parse(IdExamen.Text));
            IdExamen.IsEnabled = true;
            ClearFormValuesExamen();
            liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.SearchByNameNomFormateur(nomFormateurProgrammeExamen); 

            PopulateAndBindExamen(liste);
            ClearFormValuesExamen();

        }
        private void ClearFormValuesExamen()
        {
            IdExamen.Text = "";
            IdSite.Text = "";
            NomModule.Text = "";
            DateExamen.Text = "";
        }
        private void ModeIsEnabledTrueExamen()
        {
            IdExamen.IsEnabled = false;
            IdSite.IsEnabled = true;
            NomModule.IsEnabled = true;
            DateExamen.IsEnabled = true;
        }
        private void ModeIsEnabledFalseExamen()
        {
            IdExamen.IsEnabled = false;
            IdSite.IsEnabled = false; 
            NomModule.IsEnabled = false;
            DateExamen.IsEnabled = false;

        }

        //*******************************************************************************************************************************
        //     la partie de code pour le manipuler les liste des participants et leurs points
        //********************************************************************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxModulesPerParticipant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //code pour faire une rechercher dans la liste des participants réussis 
        }

        private void ListParticipantRéussi_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            
            if (ListParticipantRéussi.SelectedItem is MyApps.Application.ViewModels.ResultatViewModel donnee)
            {
                if (donnee.IdExamen == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }
                IdResultat.Text = donnee.IdResultat.ToString();
                IdExamen.Text = donnee.IdExamen.ToString();
                IdModuleInscription.Text = donnee.IdModuleInscription.ToString();
                NomModule.Text = donnee.NomModule.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
                Points.Text = donnee.Points.ToString();
                EstPresent.Text = donnee.EstPresent.ToString();
                ParticipantRéussi.Text = donnee.ParticipantRéussi.ToString();
            }
           
        }
        private void ListParticipantFailed_MouseDoubleClick(object sender, SelectionChangedEventArgs e) 
        {
            
            if (ListParticipantFailed.SelectedItem is MyApps.Application.ViewModels.ResultatViewModel donnee)
            {
                if (donnee.IdExamen == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }
                IdResultat.Text = donnee.IdResultat.ToString();
                IdExamen.Text = donnee.IdExamen.ToString();
                IdModuleInscription.Text = donnee.IdModuleInscription.ToString();
                NomModule.Text = donnee.NomModule.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
                Points.Text = donnee.Points.ToString();
                EstPresent.Text = donnee.EstPresent.ToString();
                ParticipantRéussi.Text = donnee.ParticipantRéussi.ToString();
            }
            if (IdResultat.Text == "" && IdExamen.Text == "" && IdModuleInscription.Text == "" && NomParticipant.Text == "")
            {
                MessageBox.Show("Séléctionner un élément dans la liste");
                return;
            }
        }
        private void ComboBoxPerticipantPerModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            liste = MyApps.Application.Services.ResultatsVieModelService.GetExamens();
            if (IdSiteModule.SelectedItem == null)
            {
                return;
            }
            string nomModuleSelected = IdSiteModule.SelectedValue.ToString();

            foreach (var module in liste)
            {
                if (nomModuleSelected == module.NomModule + " : " + module.IdSiteModule + " : " + module.DateExamen)
                {
                    idSiteModuleSelected = module.IdSiteModule;
                    IdExamens.Text = module.IdExamen.ToString();
                }
            }
            liste = MyApps.Application.Services.ResultatsVieModelService.GetSitePlanning();
            foreach (var modules in liste)
            {
                if (idSiteModuleSelected ==  modules.IdSiteModule) 
                {
                    //idSiteModuleSelected = module.IdSiteModule; 
                    NomSite.Text = modules.NomSite;
                    DateDébut.Text = modules.DateDebutModule.ToString();
                    DateFin.Text = modules.DateFinModule.ToString();
                }
            }
            IdModuleInscription.IsEnabled = false;
            IdSiteModule.IsEnabled = true;
            NomModule.Text = "";
            NomParticipant.Text = "";
            liste.Clear();
            IdModuleInscription.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            //on récuperer la liste des participants par module
            liste = MyApps.Application.Services.ResultatsVieModelService.GetParticipantPerModule((short)(idSiteModuleSelected));
            TotalParticipant.Text = liste.Count().ToString();
            PopulateAndBindListeInscription(liste);
            liste = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantRéussi(short.Parse(IdExamens.Text));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBind(liste);

            // Participant échoué dans un module

            liste= MyApps.Application.Services.ResultatsVieModelService.GetListParticipantEchoué(short.Parse(IdExamens.Text));
            TotalEchoué.Text = liste.Count().ToString();
            PopulateAndBindParticipantFailed(liste);
        }

        private void Points_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (!int.TryParse(Points.Text, out int nombrelimit))
            {
               // MessageBox.Show("Saisissez des nombres !  Pas plus de 2"); 
                Points.Text = "";
            }
        }

        // Méthode pour faire une rechercher dans la liste des examens
        //***************************************************************************************************************
        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {
            var listeExamenPlanning = MyApps.Application.Services.ResultatsVieModelService.GetExamens(); 

            if (NomRechercher.Text == "" && (MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)nomFormateurProgrammeExamen) == "Admin"
                || MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)nomFormateurProgrammeExamen) == "Secrétaire"))
            {
                MessageBox.Show("Vous n'avez pas saisie le mot à chercher");
                listeExamenPlanning.Clear();
                listeExamenPlanning = MyApps.Application.Services.ResultatsVieModelService.GetExamens();
                PopulateAndBindExamen(listeExamenPlanning);           
                ModuleSiteTextBox.IsEnabled = false;
                return;
            }
            else if (NomRechercher.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");
                return;

            }
            else if (NomRechercher.Text != "" && (MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)nomFormateurProgrammeExamen) == "Admin"
                || MyApps.Domain.Service.UserService.GetUtilisateurUserRole((string)nomFormateurProgrammeExamen) == "Secrétaire"))
            {

                listeExamenPlanning = MyApps.Application.Services.ResultatsVieModelService.SearchByNameModule(NomRechercher.Text);
                PopulateAndBindExamen(listeExamenPlanning);
            }
            else if (NomRechercher.Text == "")
            {
                var listeperFormateur = MyApps.Application.Services.ResultatsVieModelService.SearchByNameModule(nomFormateurProgrammeExamen);
              
                PopulateAndBindExamen(listeperFormateur);
            }

        }
        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {
            NomRechercher.Text = "";
            liste = MyApps.Application.Services.ResultatsVieModelService.SearchByNameModule(nomFormateurProgrammeExamen);
            PopulateAndBindExamen(liste);
        }


        //*********************************************************************************************************************************
        //********************************************* Code pour post un évenement par un formateur***************************************
      
        /// <summary>
        /// afficher les champs pour publier un événement 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Event_Click(object sender, RoutedEventArgs e)
        {
            Evenement.Visibility = Visibility;
            Evenement1.Visibility = Visibility;
            IdFormateur.Visibility = Visibility;
            Annuler.Visibility = Visibility;
            IdFormateur.SelectedItem = nomevent.ToString();

        }
        private void ComboBoxIdFormateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            liste = MyApps.Application.Services.ResultatsVieModelService.GetFormateurs();
            if (IdFormateur.SelectedItem == null)
            {
                return;
            }
            string nomFormateurSelected = IdFormateur.SelectedValue.ToString();

            foreach (var module in liste)
            {
                if (nomFormateurSelected == module.NomFormateur)
                {
                    idFormateurSelected = module.IdFormateur;

                }

            }
        }
        /// <summary>
        /// envoyer un événement par un formateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Message_Click(object sender, RoutedEventArgs e) 
        {
            Evenement element = new Evenement();
            
            if (Evenement1.Text == "")
            {
                MessageBox.Show("Veuillez écrire un message pour publier");
               
                return;
            }
            if (IdFormateur.Text == "")
            {
                MessageBox.Show("Veuillez sélectionner votre nom ");
                return;
            }

            element.Evenement1 = Evenement1.Text;
            element.DateOfEVent = DateTime.Now;

            string nomFormateurSelected = IdFormateur.SelectedValue.ToString();
            liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetFormateurs();
            foreach (var module in liste)
            {
                if (nomevent == module.NomFormateur)
                {
                    //idFormateurSelected = module.IdFormateur;
                    element.IdFormateur = module.IdFormateur;
                }

            }

            
            MyApps.Domain.Service.EvenementService.Create(element);
            Evenement1.Text = "";
            IdFormateur.Text = "";
            Evenement.Visibility = Visibility.Hidden;
            Evenement1.Visibility = Visibility.Hidden;
            IdFormateur.Visibility = Visibility.Hidden;
            Annuler.Visibility = Visibility.Hidden;
        }

       
        /// <summary>
        /// annuler une publication d'un événement 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Evenement1.Text = "";
            IdFormateur.Text = "";
            Evenement.Visibility = Visibility.Hidden;
            Evenement1.Visibility = Visibility.Hidden;
            IdFormateur.Visibility = Visibility.Hidden;
            Annuler.Visibility = Visibility.Hidden;
        }

    }
}
