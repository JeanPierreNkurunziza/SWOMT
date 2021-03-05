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
    /// Logique d'interaction pour SitePlannings.xaml
    /// </summary>
    public partial class SitePlannings : Page
    {
        List<MyApps.Application.ViewModels.SitePlanningViewModel> liste = new List<MyApps.Application.ViewModels.SitePlanningViewModel>();
        List<MyApps.Application.ViewModels.SitePlanningViewModel> clearListe = new List<MyApps.Application.ViewModels.SitePlanningViewModel>();
        List<MyApps.Application.ViewModels.ModuleViewModel> listeModule = new List<MyApps.Application.ViewModels.ModuleViewModel>();
        List<MyApps.Application.ViewModels.SiteViewModel> listeSite = new List<MyApps.Application.ViewModels.SiteViewModel>();
        List<MyApps.Application.ViewModels.FormateurModuleViewModel> listeFormateurModule = new List<MyApps.Application.ViewModels.FormateurModuleViewModel>();


        int idSiteSelected;
        int idModuleSelected = 0;
        int idFormateurModuleSelected;
        string enregistre;
        
        public SitePlannings()
        {
            InitializeComponent(); 
            liste = MyApps.Application.Services.SitePlanningViewModelService.GetSitePlanning();
            listeModule = MyApps.Application.Services.ModuleViewModelService.GetModules();
            listeSite = MyApps.Application.Services.SitesViewModelsServices.GetSites();
          
            PopulateAndBindSites(listeSite);
            PopulateAndBindModule(listeModule);
            this.SelectedNomModule();
            this.SelectedNomSite();

        }

        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="liste"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.SitePlanningViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListModuleSite.DataContext = listeItems;
        }
       
        /// <summary>
        /// Biding the list of the sites
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindSites(List<MyApps.Application.ViewModels.SiteViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElement.DataContext = listeItems;
        }
        /// <summary>
        /// Biding la liste des noms de modules
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.ModuleViewModel> SelectedNomModule()
        {
           
            foreach (var module in listeModule)
            {
                IdModule.Items.Add(module.NomModule);
            }

            return listeModule;
        }
        private List<MyApps.Application.ViewModels.FormateurModuleViewModel> SelectedNomFormateurPerModule ()
        {
            listeFormateurModule = MyApps.Application.Services.FormateurModuleViewModelService.GetFormateurPerModule((short)(idModuleSelected));
            foreach (var module in listeFormateurModule)
            {
                
                IdFormateurModule.Items.Add(module.NomFormateur + " : " + module.IdFormateurModule);
            }
           

            return listeFormateurModule; 
        }
        /// <summary>
        /// Method of assigning the name of the site by its ID
        /// </summary>
        /// <returns></returns>
        private List<MyApps.Application.ViewModels.SiteViewModel> SelectedNomSite()
        {

            foreach (var site in listeSite)
            {
                IdSite.Items.Add(site.NomSite);
            }

            return listeSite;
        }


        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListModuleSite_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListModuleSite.SelectedItem is MyApps.Application.ViewModels.SitePlanningViewModel donnee)
            {
                IdSiteModule.Text = donnee.IdSiteModule.ToString();
                IdSite.Text = donnee.NomSite.ToString();
                IdModule.Text = donnee.NomModule.ToString();
                //NomModule.Text = donnee.NomModule.ToString();
                //NomSite.Text = donnee.NomSite.ToString();
                DateDebutModule.Text = donnee.DateDebutModule.ToString();
                DateFinModule.Text = donnee.DateFinModule.ToString();
                IdFormateurModule.SelectedItem = donnee.NomFormateur.ToString() + " : " + donnee.IdFormateurModule.ToString();  

            }
        }
        /// <summary>
        /// Method to ativate the ModulePerSite form to be editable 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
           
            if (IdSite.SelectedItem == null)
            {
                MessageBox.Show("Veuillez selectionner le nom de site");
                return;
            }
            IdSiteModule.IsEnabled = false;
            IdSite.IsEnabled = true;
            IdModule.IsEnabled = false;
            //NomSite.IsEnabled = false;
            //NomModule.IsEnabled = false;
            DateDebutModule.IsEnabled = false;
            DateFinModule.IsEnabled = false;
            enregistre = "Ajouter";
            IdSiteModule.IsEnabled =false;
            //IdSite.Text = "";
            IdModule.Text = "";
            //NomSite.Text = "";
            //NomModule.Text = "";
            DateDebutModule.Text = "";
            DateFinModule.Text = "";
            IdFormateurModule.IsEnabled = true;

            ModeIsEnabledTrue();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            SiteModule element = new SiteModule(); 
            //Competence competence = new Competence();

            if (IdModule.Text == "")
            {
                MessageBox.Show("Un simple clic sur le bouton 'Ajouter' où bien selectionner un élément de mettre à jour dans la liste ");
                return;
            }
            if (IdFormateurModule.Text == "")
            {
                MessageBox.Show(" veuillez sélectionner un formateur ");
                return;
            }

            if (enregistre == "Ajouter")
            {

                //element.IdSiteModule = short.Parse(IdSiteModule.Text);
                element.IdSite = (short)(idSiteSelected);
                element.IdModule = (short)(idModuleSelected);
               
                element.DateDebutModule = DateTime.Parse(DateDebutModule.Text).Date;
                if (element.DateDebutModule < DateTime.Now)
                {
                    MessageBox.Show("La date de début est inférieur à la date currente");
                    return;
                }
                element.DateFinModule = DateTime.Parse(DateFinModule.Text).Date;
                if (element.DateFinModule < element.DateDebutModule)
                {
                    MessageBox.Show("La date de fin est inférieur à la date de début");
                    return;
                }
               
                element.IdFormateurModule = (short)(idFormateurModuleSelected);

                MyApps.Domain.Service.SitePlanningService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdSiteModule = short.Parse(IdSiteModule.Text);
                element.IdSite = (short)(idSiteSelected);
                element.IdModule = (short)(idModuleSelected);              
                element.DateDebutModule = DateTime.Parse(DateDebutModule.Text);
                if (element.DateDebutModule < DateTime.Now)
                {
                    MessageBox.Show("La date de début est inférieur à la date currente");
                    return;
                }
                element.DateFinModule = DateTime.Parse(DateFinModule.Text).Date;
                if (element.DateFinModule < element.DateDebutModule)
                {
                    MessageBox.Show("La date de fin est inférieur à la date de début");
                    return;
                }
                element.IdFormateurModule = (short)(idFormateurModuleSelected);

                MyApps.Domain.Service.SitePlanningService.Update(element);
            }
            
            liste.Clear();
            liste = MyApps.Application.Services.SitePlanningViewModelService.GetModulesPerSite((short)(idSiteSelected));
            PopulateAndBind(liste);
            IdSiteModule.Text = "";
            //IdSite.SelectedItem = "";
            IdModule.SelectedValue = "";
            DateDebutModule.Text = "";
            DateFinModule.Text = "";
            IdFormateurModule.SelectedValue = "";
            ModeIsEnabledFalse();
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
                MessageBox.Show("Entrer le site à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrue();
            IdFormateurModule.IsEnabled = true;


        }

        /// <summary>
        /// Method to delete the items in the list of moduler per site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e) 
        {
            
            if (IdSiteModule.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un élément à supprimer");
                return;
            }

            MyApps.Domain.Service.SitePlanningService.Delete(short.Parse(IdSiteModule.Text));
            IdSiteModule.IsEnabled = false;
            IdSite.IsEnabled = true;
            IdModule.SelectedValue = "";
            DateDebutModule.Text = "";
            DateFinModule.Text = "";
            liste.Clear();
            liste = MyApps.Application.Services.SitePlanningViewModelService.GetModulesPerSite((short)(idSiteSelected)); //reflesh the list
            PopulateAndBind(liste);
            IdSiteModule.Text = "";
            IdSite.SelectedItem = "";
            IdModule.SelectedValue = "";
            DateDebutModule.Text = "";
            DateFinModule.Text = "";
            IdFormateurModule.Text = "";
        }
        private void ClearFormValues()
        {
            IdSiteModule.Text = "";
            IdModule.Text = "";
            IdSite.Text = "";
            //NomModule.Text = "";
            //NomSite.Text = "";
            DateDebutModule.Text = "";
            DateFinModule.Text = "";

        }
        private void ModeIsEnabledTrue()
        {
            IdSiteModule.IsEnabled = false;
            IdSite.IsEnabled = true;
            IdModule.IsEnabled = true;
            //NomSite.IsEnabled = true;
            //NomModule.IsEnabled = true;          
            DateDebutModule.IsEnabled = true;
            DateFinModule.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdSiteModule.IsEnabled = false;
            IdSite.IsEnabled = true;
            IdModule.IsEnabled = false;
           
            //NomModule.IsEnabled = false;
            //NomSite.IsEnabled =false;
            DateDebutModule.IsEnabled = false;
            DateFinModule.IsEnabled = false;

        }

        private void ListElement_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {

            if (ListElement.SelectedItem is MyApps.Application.ViewModels.SiteViewModel donnee)
            {
                Id.Text = donnee.IdSite.ToString();
                Nom.Text = donnee.NomSite.ToString();
                AdresseSite.Text = donnee.AdresseSite.ToString();
                IdSite.SelectedItem = donnee.NomSite.ToString(); 

            }
        }

        private void IdModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdModule.SelectedItem == null)
            {
                //MessageBox.Show("Please select the name of the module");
                return;
                //IdModule.SelectedValue = "";
            }
            string nomModuleSelected = IdModule.SelectedValue.ToString();
            foreach (var module in listeModule)
            {
                if (module.NomModule == nomModuleSelected)
                {
                    idModuleSelected = module.IdModule;
                }
            }
            IdFormateurModule.Items.Clear(); 
            this.SelectedNomFormateurPerModule();
            
            

        }

        private void IdSite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdSite.SelectedItem == null)
            {
                return;
            }
            string nomSiteSelected = IdSite.SelectedValue.ToString();

            foreach (var site in listeSite)
            {
                if (site.NomSite == nomSiteSelected)
                {
                    idSiteSelected = site.IdSite;
                }
            }
            IdSiteModule.IsEnabled = false;
            IdSite.IsEnabled = true;
            IdModule.SelectedValue = "";
            DateDebutModule.Text = "";
            DateFinModule.Text = "";
            
            liste.Clear();
            ListModuleSite.DataContext = clearListe;
            IdSiteModule.IsEnabled = false;
            IdModule.IsEnabled = false;
            DateFinModule.IsEnabled = false;
            DateFinModule.IsEnabled = false;

            liste = MyApps.Application.Services.SitePlanningViewModelService.GetModulesPerSite((short)(idSiteSelected));
            PopulateAndBind(liste);
        }
        private void AjouterSite_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValuesSite();
            ModeIsEnabledTrueSite();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjourSite_Click(object sender, RoutedEventArgs e)
        {
            Site element = new Site();
            //Competence competence = new Competence();

            if (Nom.Text == "")
            {
                MessageBox.Show("Veuillez entrer le nom de site ");
                return;
            }



            if (enregistre == "Ajouter")
            {
                element.NomSite = Nom.Text;
                element.AdresseSite = AdresseSite.Text;
                foreach (var donne in MyApps.Application.Services.SitesViewModelsServices.GetSites())
                {
                    //avoid the duplicate datas in the liste of sites 
                    if ((element.NomSite == donne.NomSite) && (element.AdresseSite == donne.AdresseSite)) // if already the items exist then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }

                MyApps.Domain.Service.SiteService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdSite = short.Parse(Id.Text);
                element.NomSite = Nom.Text;
                element.AdresseSite = AdresseSite.Text;

                MyApps.Domain.Service.SiteService.Update(element);
            }


            //ModeIsEnabledFalseSite();
            listeSite.Clear();
            listeSite = MyApps.Application.Services.SitesViewModelsServices.GetSites();
            PopulateAndBindSites(listeSite);
            Id.Text = "";
            Nom.Text = "";
            AdresseSite.Text = ""; 
            ModeIsEnabledFalseSite();
            IdSite.Items.Clear();
            this.SelectedNomSite();

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierSite_Click(object sender, RoutedEventArgs e)
        {

            if (Id.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrueSite();


        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerSite_Click(object sender, RoutedEventArgs e)
        {
            //le code pour signaler la presence de l'idParticipant dans la table Inscription on doit d'abord faire une vérification
            if (Id.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un élément à supprimer");
                return;
            }
            // to avoid the suppression of the site that has already assigned the module to manage 
            foreach (var donne in MyApps.Application.Services.SitePlanningViewModelService.GetSitePlanning())
            {
                if ((short.Parse(Id.Text) == donne.IdSite))
                {
                    MessageBox.Show("Le site gere des module ! Veuillez supprimer d'abord les modules concernées dans le site planning ");
                    ClearFormValuesSite(); 
                    return; 
                }
            }
            MyApps.Domain.Service.SiteService.Delete(short.Parse(Id.Text));

            Id.IsEnabled = true;
            Nom.Text = "";
            AdresseSite.Text = "";
            listeSite.Clear();
            listeSite = MyApps.Application.Services.SitesViewModelsServices.GetSites();
            PopulateAndBindSites(listeSite);
            ClearFormValuesSite();
            IdSite.Items.Clear();
            this.SelectedNomSite();
        }
        private void ClearFormValuesSite()
        {
            Id.Text = "";
            Nom.Text = "";
            AdresseSite.Text = "";

        }
        private void ModeIsEnabledTrueSite()
        {
            Id.IsEnabled = false;
            Nom.IsEnabled = true;
            AdresseSite.IsEnabled = true;

        }
        private void ModeIsEnabledFalseSite()
        {
            Id.IsEnabled = false;
            Nom.IsEnabled = false;
            AdresseSite.IsEnabled = false;

        }

        //******************************************************************************************************************************
        //***************************************** la gestion des modules et site par module*******************************************
        //******************************************************************************************************************************
        /// <summary>
        /// biding la liste de modules
        /// </summary>
        /// <param name="listeModules"></param>
        private void PopulateAndBindModule(List<MyApps.Application.ViewModels.ModuleViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElementModule.DataContext = listeItems;
        }
        private void PopulateAndBindSitePerModule(List<MyApps.Application.ViewModels.SitePlanningViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListSitePerModule.DataContext = listeItems;
        }


        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListElementModule_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListElementModule.SelectedItem is MyApps.Application.ViewModels.ModuleViewModel donnee)
            {
                Id.Text = donnee.IdModule.ToString();
                //IdFormation.Text = donnee.NomFormation.ToString();
                //NomModule.Text = donnee.NomModule.ToString();
                //// NomFormation.Text = donnee.NomFormation.ToString();
                //CreditModule.Text = donnee.CreditModule.ToString();
                //NombrePrévu.Text = donnee.NombrePrévu.ToString();
                IdModule.SelectedItem = donnee.NomModule.ToString();
            }
            //if (enregistre != "Ajouter")
            //{
            //    IdFormateur.SelectedValue = "";
            //}
            liste.Clear();
            liste = MyApps.Application.Services.SitePlanningViewModelService.GetSitePerModule((short)(idModuleSelected));
            PopulateAndBindSitePerModule(liste); 

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
        /// <summary>
        /// Combobox pour séléctionner le nom de formateur par module 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdFormateurModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdFormateurModule.SelectedItem == null)
            {
                return;
            }
            string nomFormateurModuleSelected = IdFormateurModule.SelectedValue.ToString();

            foreach (var module in MyApps.Application.Services.FormateurModuleViewModelService.GetFormateurPerModule((short)(idModuleSelected))) 
            {
                if (nomFormateurModuleSelected == module.NomFormateur + " : " + module.IdFormateurModule) 
                {
                    idFormateurModuleSelected = module.IdFormateurModule; 
                    
                }
            }
           
        }
    }
}
