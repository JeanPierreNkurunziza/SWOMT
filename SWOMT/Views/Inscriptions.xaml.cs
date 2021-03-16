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
    /// Logique d'interaction pour Inscriptions.xaml
    /// </summary>
    public partial class Inscriptions : Page
    {
        List<MyApps.Application.ViewModels.InscriptionViewModel> listeAttente = new List<MyApps.Application.ViewModels.InscriptionViewModel>();
        List<MyApps.Application.ViewModels.InscriptionViewModel> listeModulePerParticipant = new List<MyApps.Application.ViewModels.InscriptionViewModel>();
        List<MyApps.Application.ViewModels.InscriptionViewModel> liste = new List<MyApps.Application.ViewModels.InscriptionViewModel>();
        List<MyApps.Application.ViewModels.InscriptionViewModel> clearListe = new List<MyApps.Application.ViewModels.InscriptionViewModel>();
        List<MyApps.Application.ViewModels.ParticipantViewModel> listeParticipant = new List<MyApps.Application.ViewModels.ParticipantViewModel>();
        List<MyApps.Application.ViewModels.SitePlanningViewModel> listeModule = new List<MyApps.Application.ViewModels.SitePlanningViewModel>(); 
        List<MyApps.Application.ViewModels.ParticipantViewModel> listeParticipant2 = new List<MyApps.Application.ViewModels.ParticipantViewModel>();

        int idParticipantSelected =0;
        int idModuleSelected;
        string enregistre;
        public Inscriptions() 
        {
            InitializeComponent();
           // this.WindowState = WindowState.Maximized;
            liste = MyApps.Application.Services.InscriptionViewModelService.GetInscriptions();
            listeAttente = MyApps.Application.Services.InscriptionViewModelService.GetInscriptions();
            listeModulePerParticipant = MyApps.Application.Services.InscriptionViewModelService.GetInscriptions();

            listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            PopulateAndBindParticipant(listeParticipant);
            listeParticipant2 = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();   
            //PopulateAndBindParticipantComboBox(listeParticipant2); 
            listeModule = MyApps.Application.Services.SitePlanningViewModelService.GetSitePlanning();
            //PopulateAndBindSiteModule(listeModule);

            this.SelectedNomModule(); 
            this.SelectedNomParticipant();
            this.SelectedNomParticipantsGetModules(); 
            

           
            EstSurListeAttente.Items.Add("true");
            EstSurListeAttente.Items.Add("false");

        }



        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="liste"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.InscriptionViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElement.DataContext = listeItems;
        }
        /// <summary>
        /// binding la liste des inscriptions en attente 
        /// </summary>
        /// <param name="listeItems"></param>
       
        private void PopulateAndBindAttente(List<MyApps.Application.ViewModels.InscriptionViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElementAttente.DataContext = listeItems;
        }
        /// <summary>
        /// binding la liste des participants
        /// </summary>
        /// <param name="listeCompetences"></param>
        private void PopulateAndBindParticipant(List<MyApps.Application.ViewModels.ParticipantViewModel> listeCompetences) 
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipant.DataContext = listeCompetences;

        }
        /// <summary>
        /// méthode pour faire remplir le combobox le liste de modules
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.SitePlanningViewModel> SelectedNomModule()
        {
            
            foreach (var module in listeModule.OrderBy(a => a.NomModule))
            {
                
                IdSiteModule.Items.Add(module.NomModule + " : "+ module.IdSiteModule); 
            }

            return listeModule; 
        }
        /// <summary>
        /// Method of assigning the name of the site by its ID
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.ParticipantViewModel> SelectedNomParticipant()
        {

            foreach (var participant in listeParticipant.OrderBy(a => a.NomParticipant))
            {
                IdParticipant.Items.Add(participant.NomParticipant + " : " + participant.IdNational.ToString());
            }

            return listeParticipant;
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
                if (donnee.IdSiteModule == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }
                IdModuleInscription.Text = donnee.IdModuleInscription.ToString();
                    IdSiteModule.Text = donnee.NomModule.ToString() +" : "+donnee.IdSiteModule.ToString();
                    IdParticipant.Text = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();
                    NomModule.Text = donnee.NomModule.ToString();
                    // NomParticipant.Text = MyApps.Domain.Service.InscriptionService.GetIdNational(donnee.IdParticipant).ToString();
                    DateInscription.Text = donnee.DateInscription.ToString();
                    EstSurListeAttente.Text = donnee.EstSurListeAttente.ToString();
                    IdParticipants.SelectedItem = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();

            }
            
            
        }
        private void ListElementAttente_MouseDoubleClick(object sender, SelectionChangedEventArgs e) 
        {
            if (ListElementAttente.SelectedItem is MyApps.Application.ViewModels.InscriptionViewModel donnee)
            {
                if (donnee.IdSiteModule == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }

                IdModuleInscription.Text = donnee.IdModuleInscription.ToString();
                    IdSiteModule.Text = donnee.NomModule.ToString() + " : " + donnee.IdSiteModule.ToString();
                    IdParticipant.Text = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();
                    NomModule.Text = donnee.NomModule.ToString();
                    // NomParticipant.Text = MyApps.Domain.Service.InscriptionService.GetIdNational(donnee.IdParticipant).ToString();
                    DateInscription.Text = donnee.DateInscription.ToString();
                    EstSurListeAttente.Text = donnee.EstSurListeAttente.ToString();
                    IdParticipants.SelectedItem = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();

            }
           
        }
        /// <summary>
        /// méthode pour ajouter un elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            //enregistre = "Ajouter";
            //ClearFormValues();
            //ModeIsEnabledTrue();
            if (IdSiteModule.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner un nom de module");
                return;
            }
            IdModuleInscription.IsEnabled = false;
            IdSiteModule.IsEnabled = true;
            IdParticipant.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            DateInscription.IsEnabled = false;
            EstSurListeAttente.IsEnabled = false;
            enregistre = "Ajouter";
            IdModuleInscription.Text = "";
            IdModuleInscription.IsEnabled = false;
            //IdSite.Text = "";
            IdParticipant.Text = "";
            NomModule.Text = "";
            NomModule.IsEnabled = false;
            NomParticipant.Text = "";
            NomParticipant.IsEnabled = false;
            DateInscription.Text = "";
            EstSurListeAttente.Text = "";

            ModeIsEnabledTrue();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            ModuleInscription element = new ModuleInscription();
            //Competence competence = new Competence();

            if (IdParticipant.Text == "")
            {
                MessageBox.Show("Veuillez selectionner les données à d'un participant à inscrire ou modifier ");
                return;
            }
            if (EstSurListeAttente.Text == "")
            {
                MessageBox.Show("Veuillez préciser si le participant est sur la liste d'attente");
                return; 
            }



            if (enregistre == "Ajouter")
            {

                //element.IdModuleInscription = short.Parse(IdModuleInscription.Text);
                element.IdSiteModule = (short)(idModuleSelected);
                element.IdParticipant = (short)(idParticipantSelected);  
                element.DateInscription = DateTime.Now; 
                element.EstSurListeAttente = bool.Parse(EstSurListeAttente.Text);
                foreach (var donne in MyApps.Application.Services.InscriptionViewModelService.GetInscriptions())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((element.IdSiteModule == donne.IdSiteModule) && (element.IdParticipant == donne.IdParticipant)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }

                MyApps.Domain.Service.InscriptionService.Create(element); 

            }

            if (enregistre == "Modifier")
            {

                element.IdModuleInscription = short.Parse(IdModuleInscription.Text); 
                element.IdSiteModule = (short)(idModuleSelected);
                element.IdParticipant = (short)(idParticipantSelected);
                element.DateInscription = DateTime.Parse(DateInscription.Text).Date;
                element.EstSurListeAttente = bool.Parse(EstSurListeAttente.Text);

                MyApps.Domain.Service.InscriptionService.Update(element);
            }


           
            liste.Clear();
            liste = MyApps.Application.Services.InscriptionViewModelService.GetParticipantPerModule((short)(idModuleSelected));
            TotalParticipant.Text = liste.Count().ToString();
            PopulateAndBind(liste);
            listeAttente.Clear();
            listeAttente = MyApps.Application.Services.InscriptionViewModelService.GetListAttentParticipantPerModule((short)(idModuleSelected));
            TotalListeAttente.Text = listeAttente.Count().ToString();
            PopulateAndBindAttente(listeAttente);
            listeModulePerParticipant.Clear();
            listeModulePerParticipant = MyApps.Application.Services.InscriptionViewModelService.GetModulePerParticipant((short)(idParticipantSelected));
            PopulateAndBindModulePerParticipant(listeModulePerParticipant);
            IdModuleInscription.Text = "";
           // IdModule.SelectedItem = "";
            IdParticipant.SelectedValue = "";
            NomModule.Text = "";
            NomParticipant.Text = "";
            DateInscription.Text = "";
            EstSurListeAttente.SelectedValue = "";
            ModeIsEnabledFalse();

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdParticipant.Text == "")
            {
                MessageBox.Show("Entrer un participant à modifier");
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
            if (IdModuleInscription.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un élément à supprimer dans la liste ");
                return;
            }
            MyApps.Domain.Service.InscriptionService.Delete(short.Parse(IdModuleInscription.Text));
            IdModuleInscription.IsEnabled = false; 
            IdSiteModule.IsEnabled = true;
            IdParticipant.SelectedValue = "";
            NomModule.Text = "";
            NomParticipant.Text = "";
            DateInscription.Text = "";
            EstSurListeAttente.SelectedValue = "";
            
            liste.Clear();
            liste = MyApps.Application.Services.InscriptionViewModelService.GetParticipantPerModule((short)(idModuleSelected));
            PopulateAndBind(liste);
            ClearFormValues();
           

        }
        /// <summary>
        /// binding la liste des inscriptions dans des modules
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindModulePerParticipant(List<MyApps.Application.ViewModels.InscriptionViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListSiteModule.DataContext = listeItems;
        }
        /// <summary>
        /// méthode pour porter les changements en cas de selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxIdSiteModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdSiteModule.SelectedItem == null)
            {
                return;
            }
            string nomModuleSelected = IdSiteModule.SelectedValue.ToString();
          
            foreach (var module in listeModule)  
            {
                if (nomModuleSelected == module.NomModule + " : "+ module.IdSiteModule)                   
                {
                    idModuleSelected = module.IdSiteModule;
                    NomSite.Text = module.NomSite;
                    DateDébut.Text = module.DateDebutModule.ToString();
                    DateFin.Text = module.DateFinModule.ToString();
                } 
            }
          
            IdModuleInscription.IsEnabled = false;
            IdSiteModule.IsEnabled = true;
            IdParticipant.SelectedValue = "";           
            NomModule.Text = "";
            NomParticipant.Text = "";
            DateInscription.Text = "";
            EstSurListeAttente.Text = "";

            liste.Clear();
            ListElement.DataContext = clearListe;
            IdModuleInscription.IsEnabled = false;
           // IdModule.IsEnabled = false;
            IdParticipant.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            DateInscription.IsEnabled = false;
            EstSurListeAttente.IsEnabled = false;

            liste = MyApps.Application.Services.InscriptionViewModelService.GetParticipantPerModule((short)(idModuleSelected));
            TotalParticipant.Text = liste.Count().ToString();
            PopulateAndBind(liste);
            listeAttente = MyApps.Application.Services.InscriptionViewModelService.GetListAttentParticipantPerModule((short)(idModuleSelected));
            TotalListeAttente.Text = listeAttente.Count().ToString();
            PopulateAndBindAttente(listeAttente);

          

        }
        private void ComboBoxIdParticipant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdParticipant.SelectedItem == null)
            {
                //MessageBox.Show("Please select the name of the module");
                return;
                //IdModule.SelectedValue = "";
            }
            string nomParticipantSelected = IdParticipant.SelectedValue.ToString();
            foreach (var participant in listeParticipant)
            {
                if ( nomParticipantSelected == participant.NomParticipant + " : " + participant.IdNational)
                {
                    idParticipantSelected = participant.IdParticipant;
                    NomParticipant.Text = participant.IdNational.ToString();
                }
            }

            listeModulePerParticipant.Clear();
            listeModulePerParticipant = MyApps.Application.Services.InscriptionViewModelService.GetModulePerParticipant((short)(idParticipantSelected));
            PopulateAndBindModulePerParticipant(listeModulePerParticipant);
        }
     

        private void ClearFormValues()
        {
            IdModuleInscription.Text = "";
            IdSiteModule.SelectedItem = "";
            IdParticipant.SelectedValue = "";
            NomModule.Text = "";
            NomParticipant.Text = "";
            DateInscription.Text = "";
            EstSurListeAttente.SelectedValue = "";
            

        }
        private void ModeIsEnabledTrue()
        {
            IdModuleInscription.IsEnabled = false;
            IdSiteModule.IsEnabled = true;
            IdParticipant.IsEnabled = true;
            NomModule.IsEnabled = true;
            NomParticipant.IsEnabled = true;
            DateInscription.IsEnabled = false;
            EstSurListeAttente.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdModuleInscription.IsEnabled = false;
            IdSiteModule.IsEnabled = true;
            IdParticipant.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            DateInscription.IsEnabled = false;
            EstSurListeAttente.IsEnabled = false;
         
        }

        //-------------------------------------------left side of the page : participant parts---------------

        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListParticipant_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipant.SelectedItem is MyApps.Application.ViewModels.ParticipantViewModel donnee)
            {
                if (donnee.IdParticipant == 0) 
                {
                    MessageBox.Show("Selectionner un élément dans la liste");
                    return;
                    //IdSite.Text = "";   
                }
                Id.Text = donnee.IdParticipant.ToString();
                Nom.Text = donnee.NomParticipant.ToString();
                DateNaissance.Text = donnee.DateNaissance.ToString();
                IdNational.Text = donnee.IdNational.ToString();
                TelParticipant.Text = donnee.TelParticipant.ToString();
                EmailParticipant.Text = donnee.EmailParticipant.ToString();
                SecteurParticipant.Text = donnee.SecteurParticipant.ToString();
                DistrictParticipant.Text = donnee.DistrictParticipant.ToString();
                DateEncodage.Text = donnee.DateEncodage.ToShortDateString();
               
                IdParticipants.SelectedItem = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();
                   
                IdParticipant.SelectedItem = donnee.NomParticipant.ToString() + " : " + donnee.IdNational.ToString();
                NomParticipant.Text = donnee.IdNational.ToString();

            }

            //IdParticipants.SelectedItem = donnee.NomParticipant.ToString();

        }

        private void AjouterParticipant_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValuesParticipant();
            ModeIsEnabledTrueParticipant();
            Id.IsEnabled=false;
            DateEncodage.IsEnabled = false;
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjourParticipant_Click(object sender, RoutedEventArgs e)
        {
            Participant participant = new Participant();

            DateTime ages = DateTime.Now.AddYears(-18);
            if (Nom.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom de participant");
                return;
            }
            if (DateNaissance.Text == "")
            {
                MessageBox.Show("Il faut saisir le date de naissance");
                return;
            }



            if (enregistre == "Ajouter")
            {

                participant.NomParticipant = Nom.Text;
                participant.DateNaissance = DateTime.Parse(DateNaissance.Text).Date;
                participant.IdNational = long.Parse(IdNational.Text);
                participant.TélParticipant = TelParticipant.Text;
                participant.EmailParticipant = EmailParticipant.Text;
                participant.SecteurParticipant = SecteurParticipant.Text;
                participant.DistrictParticipant = DistrictParticipant.Text;
                participant.DateEncodage = DateTime.Now;

                if(participant.DateNaissance > ages ) 
                {
                    MessageBox.Show("Votre age est inférieur à 18 ans");
                    return;
                }

                foreach (var donne in MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((participant.NomParticipant == donne.NomParticipant) && (participant.IdNational == donne.IdNational)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }
                MyApps.Domain.Service.ParticipantService.Create(participant);

            }

            if (enregistre == "Modifier")
            {
                participant.IdParticipant = short.Parse(Id.Text);
                participant.NomParticipant = Nom.Text;
                participant.DateNaissance = DateTime.Parse(DateNaissance.Text).Date;
                participant.IdNational = long.Parse(IdNational.Text);
                participant.TélParticipant = TelParticipant.Text;
                participant.EmailParticipant = EmailParticipant.Text;
                participant.SecteurParticipant = SecteurParticipant.Text;
                participant.DistrictParticipant = DistrictParticipant.Text;
                participant.DateEncodage = DateTime.Parse(DateEncodage.Text);
                if (participant.DateNaissance > ages)
                {
                    MessageBox.Show("Votre age est inférieur à 18 ans");
                    return;
                }

                
                MyApps.Domain.Service.ParticipantService.Update(participant);
            }
        
            listeParticipant.Clear();
            listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            PopulateAndBindParticipant(listeParticipant);
            ClearFormValuesParticipant();
            
            ModeIsEnabledFalseParticipant();
            IdParticipant.Items.Clear();
            this.SelectedNomParticipant(); 
            IdParticipants.Items.Clear();
            this.SelectedNomParticipantsGetModules();
        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierParticipant_Click(object sender, RoutedEventArgs e)
        {

            if (Id.Text == "")
            {
                MessageBox.Show("Entrer l'identifiant à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrueParticipant();
            Id.IsEnabled = false;
            DateEncodage.IsEnabled = false;


        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerParticipant_Click(object sender, RoutedEventArgs e)
        {
            //le code pour signaler la presence de l'idParticipant dans la table Inscription on doit d'abord faire une vérification
            if (Id.Text == "")
            {
                MessageBox.Show("Veuillez selection l'ID à supprimer");
                return;
            }
            // to avoid the suppression of the participant that has already assigned to the module 
            foreach (var donne in MyApps.Application.Services.InscriptionViewModelService.GetInscriptions())
            {
                if ((short.Parse(Id.Text) == donne.IdParticipant))
                {
                    MessageBox.Show("Le participant est inscrit dans un module ! D'abord, Veuillez le supprimer dans la liste de module");
                    ClearFormValuesParticipant();
                    return;
                }
            }

            MyApps.Domain.Service.ParticipantService.Delete(short.Parse(Id.Text));

            Id.IsEnabled = true;
            ClearFormValuesParticipant();
            listeParticipant.Clear();
            listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            PopulateAndBindParticipant(listeParticipant);
            ClearFormValuesParticipant();
            IdParticipant.Items.Clear();
            this.SelectedNomParticipant();
            IdParticipants.Items.Clear();
            this.SelectedNomParticipantsGetModules();
        }
        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {

            if (NomRechercher.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");
                listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
                PopulateAndBindParticipant(listeParticipant);
                return;

            }

            listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipantByMethodeSearch(NomRechercher.Text);
            PopulateAndBindParticipant(listeParticipant);
        }
        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {
            NomRechercher.Text = ""; 
            listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipantByMethodeSearch(NomRechercher.Text);
            PopulateAndBindParticipant(listeParticipant); 
        }
        private void ClearFormValuesParticipant()
        {
            Id.Text = "";
            Nom.Text = "";
            DateNaissance.Text = "";
            IdNational.Text = "";
            TelParticipant.Text = "";
            EmailParticipant.Text = "";
            SecteurParticipant.Text = "";
            DistrictParticipant.Text = "";
            DateEncodage.Text = "";
        }
        private void ModeIsEnabledTrueParticipant()
        {
            Id.IsEnabled = true;
            Nom.IsEnabled = true;
            DateNaissance.IsEnabled = true;
            IdNational.IsEnabled = true;
            TelParticipant.IsEnabled = true;
            EmailParticipant.IsEnabled = true;
            SecteurParticipant.IsEnabled = true;
            DistrictParticipant.IsEnabled = true;
            DateEncodage.IsEnabled = true;
        }
        private void ModeIsEnabledFalseParticipant() 
        {
            Id.IsEnabled = false;
            Nom.IsEnabled = false;
            DateNaissance.IsEnabled = false;
            IdNational.IsEnabled = false;
            TelParticipant.IsEnabled = false;
            EmailParticipant.IsEnabled = false;
            SecteurParticipant.IsEnabled = false;
            DistrictParticipant.IsEnabled = false;
            DateEncodage.IsEnabled = false;
        }

        //----------------------------------------- The part Three : liste of modules 
        private void PopulateAndBindParticipantComboBox(List<MyApps.Application.ViewModels.ParticipantViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            IdParticipants.DataContext = listeItems;  
        }
        private List<MyApps.Application.ViewModels.ParticipantViewModel> SelectedNomParticipantsGetModules()
        {

            foreach (var participant in listeParticipant.OrderBy(a => a.NomParticipant))
            {
                IdParticipants.Items.Add(participant.NomParticipant + " : " + participant.IdNational);
            }

            return listeParticipant;
        }
        private void ListSiteModule_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListSiteModule.SelectedItem is MyApps.Application.ViewModels.SitePlanningViewModel donnee)
            {
                IdSiteModule.Text = donnee.IdSiteModule.ToString();
                //IdFormation.Text = donnee.IdFormation.ToString();
                //NomModule.Text = donnee.NomModule.ToString();
                //NomFormation.Text = donnee.NomFormation.ToString();
                //CreditModule.Text = donnee.CreditModule.ToString();
                //NombrePrévu.Text = donnee.NombrePrévu.ToString();

            }
        }

        private void ComboBoxModulesPerParticipant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdParticipants.SelectedItem == null)
            {
                //MessageBox.Show("Please select the name of the module");
                return;
                //IdModule.SelectedValue = "";
            }
            string nomParticipantSelected = IdParticipants.SelectedValue.ToString(); 
            foreach (var participant in listeParticipant)
            {
                if (nomParticipantSelected == participant.NomParticipant + " : " + participant.IdNational)
                {
                    idParticipantSelected = participant.IdParticipant;
                   // NomParticipant.Text = participant.IdNational.ToString();
                }
            }

            listeModulePerParticipant.Clear();
            listeModulePerParticipant = MyApps.Application.Services.InscriptionViewModelService.GetModulePerParticipant((short)(idParticipantSelected));
            PopulateAndBindModulePerParticipant(listeModulePerParticipant);
        }
        /// <summary>
        /// méthode pour valider la réference de module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (IdSiteModuleRechercher.Text == "")
            {
                MessageBox.Show("Entrer la réference de site module");

                return;
            }

            if (!Int16.TryParse(IdSiteModuleRechercher.Text, out Int16 nbr ))
            {
                MessageBox.Show(" La réference saisie n'existe pas !!!  Veuillez consulter la planning des site et module !!! ");
                IdSiteModuleRechercher.Text = "";
                return;
            }
            var module = MyApps.Domain.Service.SitePlanningService.GetOne(short.Parse(IdSiteModuleRechercher.Text));
            if (module == null)
            {
                MessageBox.Show("La réference saisie n'existe pas !!!  Veuillez consulter la planning des site et module");
                IdSiteModuleRechercher.Text = "";
                return;
            }

            foreach (var mod in listeModule)
            {
                if (mod.IdSiteModule == module.IdSiteModule)
                {
                    IdSiteModule.SelectedItem = mod.NomModule + " : " + mod.IdSiteModule;
                }
            }
                       
            IdSiteModuleRechercher.Text = "";
        }
    }
}
