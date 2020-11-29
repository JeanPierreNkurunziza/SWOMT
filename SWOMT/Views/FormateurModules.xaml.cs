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
    /// Logique d'interaction pour FormateurModules.xaml
    /// </summary>
    public partial class FormateurModules : Page 
    {
        List<MyApps.Application.ViewModels.FormateurViewModel> Emptyliste = new List<MyApps.Application.ViewModels.FormateurViewModel>();
        List<MyApps.Application.ViewModels.FormateurViewModel> liste = new List<MyApps.Application.ViewModels.FormateurViewModel>();
        List<MyApps.Application.ViewModels.FormateurModuleViewModel> clearList = new List<MyApps.Application.ViewModels.FormateurModuleViewModel>(); 
        List<MyApps.Application.ViewModels.FormateurModuleViewModel> liste2 = new List<MyApps.Application.ViewModels.FormateurModuleViewModel>();
        List<MyApps.Application.ViewModels.FormateurViewModel> listeFormateur = new List<MyApps.Application.ViewModels.FormateurViewModel>();
        List<MyApps.Application.ViewModels.ModuleViewModel> listeModule = new List<MyApps.Application.ViewModels.ModuleViewModel>();
        List<MyApps.Application.ViewModels.FormationViewModel> listeFormation = new List<MyApps.Application.ViewModels.FormationViewModel>(); 
        int idModuleSelected=0; 
        int idFormateurSelected;
        int idFormationSelected;
        string enregistre;
        public FormateurModules(string roleName)  
        {
            InitializeComponent();
            liste= MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
            listeFormateur = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
            listeModule = MyApps.Application.Services.ModuleViewModelService.GetModules();
            listeFormation = MyApps.Application.Services.FormationViewModelsServices.GetFormations();
            liste2 = MyApps.Application.Services.FormateurModuleViewModelService.GetFormatuerModules();
           
            
            this.selectedNomModule();
            this.selectedNomFormateur();
            this.selectedNomFormation();

            PopulateAndBindFormateurs(listeFormateur);
            PopulateAndBindModule(listeModule);
            // PopulateAndBind(liste);
            if ((string)roleName != "Admin")
            {
                //BoutonInscription.IsEnabled=false;
                FormateurtextBox.IsEnabled=false;
                // FormateurtextBox.Content=false;
                ModuleFormateurtextBox.IsEnabled = false;
                GroupBoxModuleTextBox.IsEnabled = false;
                
            }


        }

        /// <summary>
        /// La méthode qui assigne le nom du formateur à partir de son identifiant 
        /// </summary>
        /// <returns>liste de noms </returns>
        private List<MyApps.Application.ViewModels.FormateurViewModel> selectedNomFormateur()
        {
            //var listeView = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();

            foreach (var formateur in liste)
            {
                IdFormateur.Items.Add(formateur.NomFormateur);
              
            }

            return liste;
        }
        /// <summary>
        /// récuperation le nom de module à partir de son identifiant
        /// </summary>
        /// <returns> liste module</returns>
        private List<MyApps.Application.ViewModels.ModuleViewModel> selectedNomModule()
        {
            //var listeView = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();

            foreach (var module in listeModule)
            {
                IdModule.Items.Add(module.NomModule);
            }

            return listeModule;
        }


        /// <summary>
        /// biding la liste des noms de module
        /// </summary>
        /// <param name="liste"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.ModuleViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            IdModule.DataContext = listeItems;
        }
        /// <summary>
        /// Biding la liste des noms de formateur
        /// </summary>
        /// <param name="formateurBinding"></param>
        private void FormateurBinding(List<MyApps.Application.ViewModels.FormateurViewModel> formateurBinding)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            IdFormateur.DataContext = formateurBinding;
        }
        /// <summary>
        /// biding la liste des modules
        /// </summary>
        /// <param name="moduleBinding"></param>
        private void ModulesBinding(List<MyApps.Application.ViewModels.FormateurModuleViewModel> moduleBinding)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListModule.DataContext = moduleBinding;
        }
        /// <summary>
        /// Biding la liste des formateures
        /// </summary>
        /// <param name="listeItems"></param>
        private void PopulateAndBindFormateurs(List<MyApps.Application.ViewModels.FormateurViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListElement.DataContext = listeItems;
        }

        /// <summary>
        /// affichage des données d'apres un simple click dans la liste 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListElement_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.FormateurViewModel donnee)
            {
                Id.Text = donnee.IdFormateur.ToString();
                NomFormateur.Text = donnee.NomFormateur.ToString();
                Domaine.Text = donnee.Domaine.ToString();
                TelFormateur.Text = donnee.TelFormateur.ToString();
                EmailFormateur.Text = donnee.EmailFormateur.ToString();
                DateEncodage.Text = donnee.DateEncodage.ToShortTimeString();
                IdFormateur.SelectedItem = donnee.NomFormateur.ToString();

            }
        }
        /// <summary>
        /// le bouton ajouter donne l'accès à ajouter des données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            //Vérifier si l'identifiant n'est pas null
            if(IdFormateur.SelectedItem == null)
            {
                MessageBox.Show("Please select the name of trainer");
                return;
            }
            IdFormateurModule.IsEnabled = false;
            IdFormateur.IsEnabled = true;   
            IdModule.IsEnabled=false;     
            VersionModule.IsEnabled = false;
            enregistre = "Ajouter";
            IdModule.Text = "";
            VersionModule.Text = "";

            ModeIsEnabledTrue();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  un élement dans la liste 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            //assigner l'objet d'une classe FormateurMdoule
            FormateurModule element = new FormateurModule();
            
            // véifier si l'identifiant de module n'est pas null
            if (IdModule.Text == "")
            {
                MessageBox.Show("Il faut selectionner le nom de module");
                return;
            }
            if (VersionModule.SelectedDate== null)  
            {
                MessageBox.Show("il faut saisir une date ");
                return;
            }
            if (enregistre == "Ajouter")
            {              
                element.IdFormateur = (short)(idFormateurSelected); //the first id to check
                element.IdModule = (short)(idModuleSelected); // the second id to check

                element.VersionModule = DateTime.Parse(VersionModule.Text).Date;
                //check if the items added is not already in the list of the trainers
                foreach (var donne in MyApps.Application.Services.FormateurModuleViewModelService.GetFormatuerModules())
                {
                    if ((element.IdFormateur == donne.IdFormateur) && (element.IdModule == donne.IdModule)) // if the items has both ids then rejects
                    {
                        MessageBox.Show("les données existé déjà ! dans la base de données");
                        return;
                    }
                }
                MyApps.Domain.Service.FormateurModuleService.Create(element); // if the idformateur and idmodule not existed then creat the item
            }

            if (enregistre == "Modifier")
            {
                element.IdFormateurModule = short.Parse(IdFormateurModule.Text);
                element.IdFormateur = (short)(idFormateurSelected);
                element.IdModule = (short)(idModuleSelected);
             
                element.VersionModule = DateTime.Parse(VersionModule.Text).Date; 

                MyApps.Domain.Service.FormateurModuleService.Update(element); // validate the modification 
            }
            liste2.Clear();  //rendre la liste vide 
            //reflesh la liste des formateur Modules 
            liste2 = MyApps.Application.Services.FormateurModuleViewModelService.GetModulesPerFormateur((short)(idFormateurSelected));
            ModulesBinding(liste2);
            //aprés validation on n'efface les élements dans la formulaire
            IdModule.SelectedValue = "";
            VersionModule.Text = "";
            ModeIsEnabledFalse();
            ClearFormValues();
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
                MessageBox.Show("Entrer la module à modifier"); 
                return;
            }
            enregistre = "Modifier";
            IdFormateur.IsEnabled = false;
            IdModule.IsEnabled = false;

            VersionModule.IsEnabled = true;

        }

        /// <summary>
        /// méthode pour supprimer un element dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            
            //le code pour signaler la presence de l'id dans la table Inscription on doit d'abord faire une vérification
            if (IdFormateurModule.Text== "") 
            {
                MessageBox.Show("Séléctionnner l' élément à supprimer ");
                return;
            }
            MyApps.Domain.Service.FormateurModuleService.Delete(short.Parse(IdFormateurModule.Text));
            
            IdFormateur.IsEnabled = true;
            IdModule.SelectedValue = "";
            VersionModule.Text = "";
            liste2.Clear();
            liste2 = MyApps.Application.Services.FormateurModuleViewModelService.GetModulesPerFormateur((short)(idFormateurSelected));
            ModulesBinding(liste2);
            IdFormateurModule.Text = "";
            IdFormateur.SelectedItem = "";
            IdModule.SelectedValue = "";
            VersionModule.Text = "";

        }
        private void ClearFormValues()
        {
            IdFormateur.SelectedItem = "";
            IdModule.SelectedItem = "";
        
            VersionModule.Text = "";
        
        }
        private void ModeIsEnabledTrue()
        {
            IdFormateur.IsEnabled = true;
            IdModule.IsEnabled = true;
       
            VersionModule.IsEnabled = true;
          
        }
        private void ModeIsEnabledFalse()
        {
            IdFormateur.IsEnabled = true;
            IdModule.IsEnabled = false;
           
            VersionModule.IsEnabled = false;
        
        }

        //*****************************************************************************************************************************
        //**************************************** gestion des formateurs**************************************************************
        //*****************************************************************************************************************************

        private void IdFormateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdFormateur.SelectedIndex == -1)
            {
                MessageBox.Show("Selectionner l'identifiant du formateur");
                return;
            }

            string nomFormateurSelected = IdFormateur.SelectedValue.ToString();
            
            foreach (var formateur in listeFormateur)
            {
                if (formateur.NomFormateur == nomFormateurSelected) 
                {
                    idFormateurSelected = formateur.IdFormateur; 
                }
            }

            IdFormateur.IsEnabled = true;
            IdModule.SelectedValue="";
            VersionModule.Text = "";
            //VersionModule.IsEnabled=false;
            liste2.Clear();
            ListModule.DataContext = clearList;

            IdModule.IsEnabled = false;

            VersionModule.IsEnabled = false;

            liste2 = MyApps.Application.Services.FormateurModuleViewModelService.GetModulesPerFormateur((short)(idFormateurSelected)); 
            ModulesBinding(liste2);
           
        }

        private void ListModule_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if(ListModule.SelectedItem is MyApps.Application.ViewModels.FormateurModuleViewModel donnee)
            {
                
                IdModule.Text = donnee.NomModule.ToString(); 

                VersionModule.Text = donnee.VersionModule.ToString();
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
            foreach(var module in listeModule)
            {
                if (module.NomModule == nomModuleSelected)
                {
                    idModuleSelected = module.IdModule;
                }
            }
            liste2.Clear();
            liste2 = MyApps.Application.Services.FormateurModuleViewModelService.GetFormateurPerModule((short)(idModuleSelected));
            ModulesBinding(liste2);
           
        }

        private void AjouterFormateur_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValuesFormateur();
            ModeIsEnabledTrueFormateur();
        }

        private void ModifierFormateur_Click(object sender, RoutedEventArgs e)
        {
            if (Id.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrueFormateur();

        }

        private void SupprimerFormateur_Click(object sender, RoutedEventArgs e)
        {
            if (Id.Text=="") 
            {
                MessageBox.Show("Please select the trainer to delete");
                return;
            }
            foreach (var donne in MyApps.Application.Services.FormateurModuleViewModelService.GetFormatuerModules())
            {
                if ((short.Parse(Id.Text) == donne.IdFormateur))
                {
                    MessageBox.Show("le formateur a des modules à gérér ! supprimer d'abord ses modules");
                    ClearFormValuesFormateur();
                    return;
                }
            }
            MyApps.Domain.Service.FormateurService.Delete(short.Parse(Id.Text));

            ClearFormValuesFormateur();

            listeFormateur = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
            PopulateAndBindFormateurs(listeFormateur);

            //MessageBox.Show("Reload the pages to validate the modifications");
            //Main.Content = new FormateurModules(); 
        }

        private void MettreAjourFormateur_Click(object sender, RoutedEventArgs e) 
        {
            Formateur element = new Formateur();
            //Competence competence = new Competence();

            if (NomFormateur.Text=="")
            {
                MessageBox.Show("Il faut mettre les données à enregistrer");
                return; 
            }



            if (enregistre == "Ajouter")
            {
                element.NomFormateur = NomFormateur.Text;
                element.Domaine = Domaine.Text;
                element.TélFormateur = TelFormateur.Text;
                element.EmailFormateur = EmailFormateur.Text;
                element.DateEncodage = DateTime.Parse(DateEncodage.Text);

                MyApps.Domain.Service.FormateurService.Create(element);
               
            }

            if (enregistre == "Modifier")
            {
                element.IdFormateur = short.Parse(Id.Text);
                element.NomFormateur = NomFormateur.Text;
                element.Domaine = Domaine.Text;
                element.TélFormateur = TelFormateur.Text;
                element.EmailFormateur = EmailFormateur.Text;
                element.DateEncodage = DateTime.Parse(DateEncodage.Text);

                MyApps.Domain.Service.FormateurService.Update(element);
               
            }

            ModeIsEnabledFalseFormateur();
          
            listeFormateur = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
            ClearFormValuesFormateur();
            PopulateAndBindFormateurs(listeFormateur);
           // MessageBox.Show("Reload the pages to validate the modifications");

        }
        private void ClearFormValuesFormateur()
        {
            Id.Text = "";
            NomFormateur.Text = "";
            Domaine.Text = "";
            TelFormateur.Text = "";
            EmailFormateur.Text = "";
            DateEncodage.Text = "";

        }
        private void ModeIsEnabledTrueFormateur()
        {
            Id.IsEnabled = false;
            NomFormateur.IsEnabled = true;
            Domaine.IsEnabled = true;
            TelFormateur.IsEnabled = true;
            EmailFormateur.IsEnabled = true;
            DateEncodage.IsEnabled = true;

        }
        private void ModeIsEnabledFalseFormateur()
        {
            Id.IsEnabled = false;
            NomFormateur.IsEnabled = false;
            Domaine.IsEnabled = false;
            TelFormateur.IsEnabled = false;
            EmailFormateur.IsEnabled = false;
            DateEncodage.IsEnabled = false;

        }

        //*********************************************************************************************************************
        //**************************** Gestion des modules*********************************************************************
        //*********************************************************************************************************************
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

        private List<MyApps.Application.ViewModels.FormationViewModel> selectedNomFormation()
        {
            //var listeView = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();

            foreach (var formateur in listeFormation)
            {
                IdFormation.Items.Add(formateur.NomFormation); 

            }

            return listeFormation; 
        }
        private void IdFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IdFormation.SelectedItem == null)
            {
                //MessageBox.Show("Please select the name of the module");
                return;
                //IdModule.SelectedValue = "";
            }
            string nomFormationSelected = IdFormation.SelectedValue.ToString();
            foreach (var formation in listeFormation)
            {
                if (formation.NomFormation == nomFormationSelected)
                {
                    idFormationSelected = formation.IdFormation; 
                }
            }
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
                idModule.Text = donnee.IdModule.ToString();
                IdFormation.Text = donnee.NomFormation.ToString();
                NomModule.Text = donnee.NomModule.ToString();
               // NomFormation.Text = donnee.NomFormation.ToString();
                CreditModule.Text = donnee.CreditModule.ToString();
                NombrePrévu.Text = donnee.NombrePrévu.ToString();
                IdModule.SelectedItem = donnee.NomModule.ToString();
            }
            if (enregistre != "Ajouter")
            {
                IdFormateur.SelectedValue = "";
            }
           
        }

        private void AjouterModule_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValuesModule();
            ModeIsEnabledTrueModule();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjourModule_Click(object sender, RoutedEventArgs e)
        {
            Module element = new Module();
           

            if (idModule.Text == "")
            {
                MessageBox.Show("Il faut saisir identifiant ");
                return;
            }
            if (!int.TryParse(CreditModule.Text, out int nbr))
            {
                MessageBox.Show("Format du nombre pour le Crédit de module  est incorrect SVP !");
                return;
            }
            if (!int.TryParse(NombrePrévu.Text, out int nbrPrevu))
            {
                MessageBox.Show("Format du nombre Prévus des participants est incorrect SVP !");
                return;
            }

            if (enregistre == "Ajouter")
            {

               // element.IdModule = short.Parse(IdModule.Text);
                element.IdFormation = (short)(idFormationSelected);
                element.NomModule = NomModule.Text;
                element.CreditModule = short.Parse(CreditModule.Text);
                element.NombrPrévu = short.Parse(NombrePrévu.Text);

                MyApps.Domain.Service.ModuleService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                element.IdModule = short.Parse(idModule.Text);
                element.IdFormation = (short)(idFormationSelected);
                element.NomModule = NomModule.Text;
                element.CreditModule = short.Parse(CreditModule.Text);
                element.NombrPrévu = short.Parse(NombrePrévu.Text);

                MyApps.Domain.Service.ModuleService.Update(element);
            }

            ModeIsEnabledFalseModule();
            //listeModule.Clear();
            listeModule = MyApps.Application.Services.ModuleViewModelService.GetModules();
            ClearFormValuesModule(); 
            PopulateAndBind(listeModule);
           

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierModule_Click(object sender, RoutedEventArgs e)
        {

            if (idModule.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledTrueModule();

        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerModule_Click(object sender, RoutedEventArgs e)
        {
            //le code pour signaler la presence de l'idParticipant dans la table Inscription on doit d'abord faire une vérification
            if (idModule.Text=="")
            {
                MessageBox.Show("Séléctionner un élement à supprimer");
                return;
            }
            MyApps.Domain.Service.ModuleService.Delete(short.Parse(IdModule.Text));

            ClearFormValuesModule();
            listeModule.Clear();
            listeModule = MyApps.Application.Services.ModuleViewModelService.GetModules();
            PopulateAndBind(listeModule);

        }
        private void ClearFormValuesModule()
        {
            idModule.Text = "";
            IdFormation.SelectedValue = "";
            NomModule.Text = "";
           // NomFormation.Text = "";
            CreditModule.Text = "";
            NombrePrévu.Text = "";

        }
        private void ModeIsEnabledTrueModule()
        {
            idModule.IsEnabled = false;
            IdFormation.IsEnabled = true;
            NomModule.IsEnabled = true;
           // NomFormation.IsEnabled = true;
            CreditModule.IsEnabled = true;
            NombrePrévu.IsEnabled = true;

        }
        private void ModeIsEnabledFalseModule()
        {
            idModule.IsEnabled = false;
            IdFormation.IsEnabled = false;
            NomModule.IsEnabled = false;
           // NomFormation.IsEnabled = false;
            CreditModule.IsEnabled = false;
            NombrePrévu.IsEnabled = false;

        }

        //****************************************************************************************************************
        //*************************** les méthodes de recherches *********************************************************
        //****************************************************************************************************************
        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {

            if (NomRechercher.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");

                listeFormateur = MyApps.Application.Services.FormateurViewModelsService.GetFormateurs();
                PopulateAndBindFormateurs(listeFormateur);
                return;

            }

            listeFormateur = MyApps.Application.Services.FormateurViewModelsService.SearchFormateurByName(NomRechercher.Text);
            PopulateAndBindFormateurs(listeFormateur);
        }
        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {
            NomRechercher.Text = "";
            listeFormateur = MyApps.Application.Services.FormateurViewModelsService.SearchFormateurByName(NomRechercher.Text);
            PopulateAndBindFormateurs(listeFormateur); 
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
