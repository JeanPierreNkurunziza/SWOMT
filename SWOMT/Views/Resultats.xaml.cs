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
        List<MyApps.Application.ViewModels.ResultatViewModel> liste = new List<MyApps.Application.ViewModels.ResultatViewModel>();
        List<MyApps.Application.ViewModels.ResultatViewModel> listParticipantFailed = new List<MyApps.Application.ViewModels.ResultatViewModel>();
        List<MyApps.Application.ViewModels.ExamenViewModel> listeExamen = new List<MyApps.Application.ViewModels.ExamenViewModel>();
        List<MyApps.Application.ViewModels.SitePlanningViewModel> listeModule = new List<MyApps.Application.ViewModels.SitePlanningViewModel>();
        List<MyApps.Application.ViewModels.InscriptionViewModel> listeInscription = new List<MyApps.Application.ViewModels.InscriptionViewModel>();
        List<MyApps.Application.ViewModels.InscriptionViewModel> clearListe = new List<MyApps.Application.ViewModels.InscriptionViewModel>();

        string enregistre;
        int idSiteModuleSelected;
        public Resultats() 
        {
            InitializeComponent();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetResultats();
            listeExamen = MyApps.Application.Services.ExamenViewModelService.GetExamens();
            listeModule = MyApps.Application.Services.SitePlanningViewModelService.GetSitePlanning();
            listParticipantFailed = MyApps.Application.Services.ResultatsVieModelService.GetResultats();
            
            PopulateAndBindExamen(listeExamen);
            this.SelectedNomModule();
            this.SelectedNomModuleExamenPlanned();

            EstPresent.Items.Add("True");
            EstPresent.Items.Add("False");
            ParticipantRéussi.Items.Add("True");
            ParticipantRéussi.Items.Add("False"); 

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

        private void PopulateAndBindParticipantFailed(List<MyApps.Application.ViewModels.ResultatViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipantFailed.DataContext = listeItems;
        }
        private void PopulateAndBindListeInscription(List<MyApps.Application.ViewModels.InscriptionViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElement.DataContext = listeItems;
            
        }
        /// <summary>
        /// the method to fill in the combobox the liste items for selection 
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.SitePlanningViewModel> SelectedNomModule()
        {

            foreach (var module in listeModule)
            {

                IdSite.Items.Add(module.NomModule + " : " + module.IdSiteModule);
            }

            return listeModule;
        }
        private List<MyApps.Application.ViewModels.ExamenViewModel> SelectedNomModuleExamenPlanned()
        {

            foreach (var module in listeExamen)
            {

                IdSiteModule.Items.Add(module.NomModule + " : " + module.IdSiteModule + " : " + module.DateExamen);
               // IdExamens.Items.Add(module.IdExamen);
               
            }
            
            return listeExamen;
        }


        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListElement_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.InscriptionViewModel donnee)
            {
                //IdResultat.Text = donnee.IdResultat.ToString();
                //IdExamen.Text = donnee.IdExamen.ToString();
                IdModuleInscription.Text = donnee.IdModuleInscription.ToString();
                NomModule.Text = donnee.NomModule.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
                
                //Points.Text = donnee.Points.ToString();
                //EstPresent.Text = donnee.EstPresent.ToString();
                //ParticipantRéussi.Text = donnee.ParticipantRéussi.ToString(); 
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



            if (enregistre == "Ajouter")
            {

                //element.IdSiteModule = short.Parse(IdSiteModule.Text);
                element.IdExamen = short.Parse(IdExamens.Text);
                element.IdModuleInscription = short.Parse(IdModuleInscription.Text);
                element.Points = short.Parse(Points.Text);
                element.EstPresent = bool.Parse(EstPresent.Text);
                element.ParticipantRéussi = bool.Parse(ParticipantRéussi.Text);
                MyApps.Domain.Service.ResultatService.Create(element); 

            }

            if (enregistre == "Modifier")
            {

                //element.IdSiteModule = short.Parse(IdSiteModule.Text); 
                element.IdExamen = short.Parse(IdExamens.Text);
                element.IdModuleInscription = short.Parse(IdModuleInscription.Text); 
                element.Points = short.Parse(Points.Text);
                element.EstPresent = bool.Parse(EstPresent.Text);
                element.ParticipantRéussi = bool.Parse(ParticipantRéussi.Text);

                MyApps.Domain.Service.ResultatService.Update(element); 
            }


            ModeIsEnabledFalse();
            ClearFormValues();
            
            //liste.Clear();
            //liste = MyApps.Application.Services.ResultatsVieModelService.GetResultats();
            //PopulateAndBind(liste);

            //Participant réussi dans un module 
            liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantRéussi(short.Parse(IdExamens.Text));
            TotalRéussi.Text = liste.Count().ToString();
            PopulateAndBind(liste);

            // Participant échoué dans un module
            listParticipantFailed.Clear();
            listParticipantFailed = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantEchoué(short.Parse(IdExamens.Text));
            TotalEchoué.Text = listParticipantFailed.Count().ToString();
            PopulateAndBindParticipantFailed(listParticipantFailed);
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

            MyApps.Domain.Service.ResultatService.Delete(short.Parse(IdResultat.Text));

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetResultats(); 
            PopulateAndBind(liste);

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
        private void PopulateAndBindExamen(List<MyApps.Application.ViewModels.ExamenViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListExamen.DataContext = listeItems;
        }

        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListExamen_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListExamen.SelectedItem is MyApps.Application.ViewModels.ExamenViewModel donnee)
            {
                IdExamen.Text = donnee.IdExamen.ToString();
                IdSite.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString() ; 
                NomModule.Text = donnee.NomModule.ToString();
                DateExamen.Text = donnee.DateExamen.ToString();

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
                MessageBox.Show("You have to put the date of the examen");
                return;
            }

            if (enregistre == "Ajouter")
            {

                //element.IdExamen = short.Parse(IdExamen.Text);
                element.IdSiteModule = (short)(idSiteModuleSelected);
                element.DateExamen = DateTime.Parse(DateExamen.Text).Date;
                foreach (var donne in MyApps.Application.Services.ExamenViewModelService.GetExamens())
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

                //element.IdExamen = short.Parse(IdExamen.Text);
                element.IdSiteModule = (short)(idSiteModuleSelected);

                element.DateExamen = DateTime.Parse(DateExamen.Text).Date;

                MyApps.Domain.Service.ExamenService.Update(element);
            }
           
            listeExamen.Clear();
            listeExamen = MyApps.Application.Services.ExamenViewModelService.GetExamens();
            PopulateAndBindExamen(listeExamen);
            ClearFormValuesExamen();
            ModeIsEnabledFalseExamen();            

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
                MessageBox.Show("Please select the Id to delete");
                return;
            }
            foreach (var donne in MyApps.Application.Services.ResultatsVieModelService.GetResultats())
            {
                if ((short.Parse(IdExamen.Text) == donne.IdExamen))
                {
                    MessageBox.Show("The idExamen is used in the Results table");
                    ClearFormValuesExamen();
                    return;
                }
            }

            MyApps.Domain.Service.ExamenService.Delete(short.Parse(IdExamen.Text));
            IdExamen.IsEnabled = true;
            ClearFormValuesExamen();
            listeExamen.Clear();
            listeExamen = MyApps.Application.Services.ExamenViewModelService.GetExamens();
            PopulateAndBindExamen(listeExamen);
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

        private void ComboBoxModulesPerParticipant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListParticipantRéussi_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipantRéussi.SelectedItem is MyApps.Application.ViewModels.ResultatViewModel donnee)
            {
               // IdResultat.Text = donnee.IdResultat.ToString();
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

        }

        private void ComboxBoxIdSiteModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdSite.SelectedItem == null)
            {
                return;
            }
            string nomModuleSelected = IdSite.SelectedValue.ToString(); 

            foreach (var module in listeExamen) 
            {
                if (nomModuleSelected == module.NomModule + " : " + module.IdSiteModule + " : " + module.DateExamen)
                {
                    idSiteModuleSelected = module.IdSiteModule;
                    //NomSite.Text = module.NomSite;
                    //DateDébut.Text = module.DateDebutModule.ToString();
                    //DateFin.Text = module.DateFinModule.ToString();
                    
                }
            }
            
        }

        private void ComboBoxPerticipantPerModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdSiteModule.SelectedItem == null)
            {
                return;
            }
            string nomModuleSelected = IdSiteModule.SelectedValue.ToString();

            foreach (var module in listeExamen)
            {
                if (nomModuleSelected == module.NomModule + " : " + module.IdSiteModule + " : " + module.DateExamen)
                {
                    idSiteModuleSelected = module.IdSiteModule;
                    IdExamens.Text = module.IdExamen.ToString();
                    //NomSite.Text = module.NomSite;
                    //DateDébut.Text = module.DateDebutModule.ToString();
                    //DateFin.Text = module.DateFinModule.ToString();
                }
            }
            foreach (var modules in listeModule)
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
            //IdParticipant.SelectedValue = "";
            NomModule.Text = "";
            NomParticipant.Text = "";
            //DateInscription.Text = "";
           // EstSurListeAttente.Text = "";

            listeInscription.Clear();
            ListElement.DataContext = clearListe;
            IdModuleInscription.IsEnabled = false;
            // IdModule.IsEnabled = false;
            //IdParticipant.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            //DateInscription.IsEnabled = false;
            //EstSurListeAttente.IsEnabled = false;

            listeInscription = MyApps.Application.Services.InscriptionViewModelService.GetParticipantPerModule((short)(idSiteModuleSelected));
            TotalParticipant.Text = listeInscription.Count().ToString();
            PopulateAndBindListeInscription(listeInscription);

            //Participants Réussis list and theirs resultats  
            liste.Clear();
            liste = MyApps.Application.Services.ResultatsVieModelService.GetListParticipantRéussi(short.Parse(IdExamens.Text));
            TotalRéussi.Text = liste.Count().ToString();
            //PopulateAndBind(liste);
            PopulateAndBind(liste);

            // Participant échoué dans un module
            listParticipantFailed.Clear();
            listParticipantFailed= MyApps.Application.Services.ResultatsVieModelService.GetListParticipantEchoué(short.Parse(IdExamens.Text));
            TotalEchoué.Text = listParticipantFailed.Count().ToString();
            PopulateAndBindParticipantFailed(listParticipantFailed);
        }
    }
}
