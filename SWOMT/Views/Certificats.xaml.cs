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
    /// Logique d'interaction pour Certificats.xaml
    /// </summary>
    public partial class Certificats : Page
    {
       
        string enregistre;
        List<MyApps.Application.ViewModels.CertificatViewModel> liste = new List<MyApps.Application.ViewModels.CertificatViewModel>();
        public Certificats(string roleName)
        {
            InitializeComponent();
            liste = MyApps.Application.Services.CertificatViewModelService.GetParticipants();        
            PopulateAndBindParticipant(liste);
            if ((string)roleName != "Admin")
            {
                CertificatTextBox.IsEnabled = false;
            }
            if ((string)roleName != "Secrétaire" && (string)roleName != "Admin" && (string)roleName != "Formateur")
            {

                IdNationale.Visibility = Visibility.Hidden;
                Datenaissance.Visibility = Visibility.Hidden;
                email.Visibility = Visibility.Hidden;
                telephone.Visibility = Visibility.Hidden;
            }

        }

        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="listeFormation"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.CertificatViewModel> listeItems)
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
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.CertificatViewModel donnee)
            {
                IdCertificat.Text = donnee.IdCertificat.ToString();
                IdParticipant.Text = donnee.IdParticipant.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();

            }
           
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            
            ModeIsEnabledFalse();
            
            NomParticipant.Text = "";
            DateDelivrance.Text = "";
            DateDelivrance.IsEnabled = true;
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            Certificat element = new Certificat();

            if (IdParticipant.Text == "")
            {
                MessageBox.Show("Il faut sélectionner un élément dans liste des certificats délivrés ");
                return;
            }
            if (NomParticipant.Text == "")
            {
                MessageBox.Show("Il faut sélectionner un participant réussi un module ");
                return;
            }

            if (DateDelivrance.Text=="")
            {
                MessageBox.Show("Il faut sélectionner dans la liste des modules réussis par un participant");
                return;
            }


            if (enregistre == "Ajouter")
            {
                element.IdParticipant = short.Parse(IdParticipant.Text);
                element.DateDelivrance = DateTime.Now;

                MyApps.Domain.Service.CertificatService.Create(element); 

            }

            if (enregistre == "Modifier")
            {

                element.IdCertificat = short.Parse(IdCertificat.Text);
                element.IdParticipant = short.Parse(IdParticipant.Text);
                element.DateDelivrance = DateTime.Parse(DateDelivrance.Text).Date;

                MyApps.Domain.Service.CertificatService.Update(element);
            }
            liste.Clear();
            liste = MyApps.Application.Services.CertificatViewModelService.GetCertificatsPerParticipant(short.Parse(IdParticipant.Text));
            NbrCertificatRécu.Text = liste.Count().ToString();
            PopulateAndBind(liste);
            DateDelivrance.Text = ""; 
            ModeIsEnabledFalse();
           

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdCertificat.Text == "")
            {
                MessageBox.Show("Entrer la formation à modifier");
                return;
            }
            enregistre = "Modifier";
            ModeIsEnabledFalse();


        }

        /// <summary>
        /// méthode pour supprimer une  competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            //tester si l'identifiant n'est pas null 
            if (IdCertificat.Text == "")
            {
                MessageBox.Show("Il faut saisir identifiant de certificat ");
                return;
            }

            MyApps.Domain.Service.CertificatService.Delete(short.Parse(IdCertificat.Text));
            
            liste.Clear();
            liste = MyApps.Application.Services.CertificatViewModelService.GetCertificatsPerParticipant(short.Parse(IdParticipant.Text));
            NbrCertificatRécu.Text = liste.Count().ToString();
            PopulateAndBind(liste);
            ClearFormValues();
        }
        private void ClearFormValues()
        {
            IdCertificat.Text = "";
            IdParticipant.Text = "";
            NomParticipant.Text = "";
            DateDelivrance.Text = "";

        }
      
        private void ModeIsEnabledFalse()
        {
            IdCertificat.IsEnabled = false;
            IdParticipant.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            DateDelivrance.IsEnabled = false; 

        }
        //************************************************************************************************************************************************
        //************************************** LA LISTE DES PARTICIPANT PARTIE DE DROITE ***************************************************************
        private void PopulateAndBindParticipant(List<MyApps.Application.ViewModels.CertificatViewModel> listeCompetences)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
           
            ListParticipant.DataContext = listeCompetences;

        }
        private void ListParticipant_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipant.SelectedItem is MyApps.Application.ViewModels.CertificatViewModel donnee)
            {

                IdParticipant.Text = donnee.IdParticipant.ToString();
                var ListeModulesReussis = MyApps.Application.Services.CertificatViewModelService.GetListModulesRéussis(short.Parse(IdParticipant.Text));
                PopulateAndBindRéussis(ListeModulesReussis);
                TotalRéussi.Text = ListeModulesReussis.Count().ToString();
                var ListeModulesEchoues = MyApps.Application.Services.CertificatViewModelService.GetListModulesEchoué(short.Parse(IdParticipant.Text));
                PopulateAndBindParticipantFailed(ListeModulesEchoues);
                TotalEchoué.Text = ListeModulesEchoues.Count().ToString(); 

                IdParticipant.Text = donnee.IdParticipant.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
                liste.Clear();
                liste = MyApps.Application.Services.CertificatViewModelService.GetCertificatsPerParticipant(short.Parse(IdParticipant.Text));
                NbrCertificatRécu.Text = liste.Count().ToString();
                PopulateAndBind(liste);
            }

        }
        private void Rechercher_Click(object sender, RoutedEventArgs e)
        {
            var  listeParticipant = MyApps.Application.Services.CertificatViewModelService.GetParticipants();
            if (NomRechercher.Text == "")
            {
                MessageBox.Show("Entrer le nom à rechercher");
                PopulateAndBindParticipant(listeParticipant);
                return;
            }

            listeParticipant = MyApps.Application.Services.CertificatViewModelService.GetParticipantByMethodeSearch(NomRechercher.Text);
            PopulateAndBindParticipant(listeParticipant);
        }
        private void ReSetList_Click(object sender, RoutedEventArgs e)
        {
            NomRechercher.Text = "";
            var listeParticipant = MyApps.Application.Services.CertificatViewModelService.GetParticipantByMethodeSearch(NomRechercher.Text);
            PopulateAndBindParticipant(listeParticipant);
        }

        //************************************************************************************************************************************
        //************************************* Partie à gauche : les liste des participant réussis et échoués********************************

        private void PopulateAndBindRéussis(List<MyApps.Application.ViewModels.CertificatViewModel> listeItems)
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
        private void PopulateAndBindParticipantFailed(List<MyApps.Application.ViewModels.CertificatViewModel> listeItems)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipantFailed.DataContext = listeItems;
        }
        private void ListParticipantRéussi_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {

            if (ListParticipantRéussi.SelectedItem is MyApps.Application.ViewModels.CertificatViewModel donnee)
            {
                if (donnee.IdExamen == 0)
                {
                    MessageBox.Show("Selectionner un élément dans la liste");
                    return;
                 
                }
                enregistre = "Ajouter";
                NomParticipant.Text = donnee.NomParticipant.ToString();
                DateDelivrance.Text = DateTime.Now.ToString();

            }
        }

        private void ListParticipantFailed_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {

            if (ListParticipantFailed.SelectedItem is MyApps.Application.ViewModels.CertificatViewModel donnee)
            {
                if (donnee.IdExamen == 0)
                {
                    MessageBox.Show("Selectionner un élément dans la liste");
                    return;
                 
                }
               
            }
          
        }
        private void ComboBoxModulesPerParticipant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //code pour faire une rechercher dans la liste des participants réussis 

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = new DatePicker();
            datePicker.SetBinding(ToolTipProperty, "Date");
            datePicker.SelectedDateChanged += (s, ea) =>
             {
                 DateTime? date = datePicker.SelectedDate;
                 string value = date != null ? date.Value.ToString("dd-MM-yyyy") : null;
                 datePicker.ToolTip = value;
             };
        }
    }
}
