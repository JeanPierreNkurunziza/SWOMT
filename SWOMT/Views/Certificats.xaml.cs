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
        List<MyApps.Application.ViewModels.CertificatViewModel> liste = new List<MyApps.Application.ViewModels.CertificatViewModel>();
        List<MyApps.Application.ViewModels.ParticipantViewModel> listeParticipant = new List<MyApps.Application.ViewModels.ParticipantViewModel>();
        List<MyApps.Application.ViewModels.ResultatViewModel> ListeModulesEchoues = new List<MyApps.Application.ViewModels.ResultatViewModel>();
        List<MyApps.Application.ViewModels.ResultatViewModel> ListeModulesReussis = new List<MyApps.Application.ViewModels.ResultatViewModel>();


        string enregistre;
       
        public Certificats()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.CertificatViewModelService.GetCertificats();
            PopulateAndBind(liste);
            listeParticipant = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            PopulateAndBindParticipant(listeParticipant);
            ListeModulesEchoues = MyApps.Application.Services.ResultatsVieModelService.GetResultats();
            ListeModulesReussis = MyApps.Application.Services.ResultatsVieModelService.GetResultats(); 
            //PopulateAndBindRéussis(ListeResultats);
            //PopulateAndBindParticipantFailed(ListeResultats);

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
                DateDelivrance.Text = donnee.DateDelivrance.ToString();

            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            
            ModeIsEnabledFalse();
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
            //Competence competence = new Competence();

            if (IdCertificat.Text == "")
            {
                MessageBox.Show("Il faut saisir identifiant de certificat ");
                return;
            }



            if (enregistre == "Ajouter")
            {
                element.IdParticipant = short.Parse(IdParticipant.Text);
                element.DateDelivrance = DateTime.Parse(DateDelivrance.Text).Date;

                MyApps.Domain.Service.CertificatService.Create(element); 

            }

            if (enregistre == "Modifier")
            {

                element.IdCertificat = short.Parse(IdCertificat.Text);
                element.IdParticipant = short.Parse(IdParticipant.Text);
                element.DateDelivrance = DateTime.Parse(DateDelivrance.Text).Date;

                MyApps.Domain.Service.CertificatService.Update(element);
            }

            ClearFormValues();
            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.CertificatViewModelService.GetCertificats();
            PopulateAndBind(liste);

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

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.CertificatViewModelService.GetCertificats();
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdCertificat.Text = "";
            IdParticipant.Text = "";
            NomParticipant.Text = "";
            DateDelivrance.Text = "";

        }
        private void ModeIsEnabledTrue()
        {
            IdCertificat.IsEnabled = true;
            IdParticipant.IsEnabled = true;
            NomParticipant.IsEnabled = true;
            DateDelivrance.IsEnabled = true;

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

        private void PopulateAndBindParticipant(List<MyApps.Application.ViewModels.ParticipantViewModel> listeCompetences)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipant.DataContext = listeCompetences;

        }
        private void ListParticipant_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipant.SelectedItem is MyApps.Application.ViewModels.ParticipantViewModel donnee)
            {
               
                       
             ListeModulesReussis = MyApps.Application.Services.ResultatsVieModelService.GetListModulesRéussis(
                  MyApps.Domain.Service.ResultatService.GetIdInscription((short)(donnee.IdParticipant)));
              PopulateAndBindRéussis(ListeModulesReussis);
                TotalRéussi.Text = ListeModulesReussis.Count().ToString();

                ListeModulesEchoues = MyApps.Application.Services.ResultatsVieModelService.GetListModulesEchoué(
                    MyApps.Domain.Service.ResultatService.GetIdInscription((short)(donnee.IdParticipant)));
             PopulateAndBindParticipantFailed(ListeModulesEchoues);
                TotalEchoué.Text = ListeModulesEchoues.Count().ToString(); 

                IdParticipant.Text = donnee.IdParticipant.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
            }

           

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

        //************************************************************************************************************************************
        //************************************* Partie à gauche : les liste des participant réussis et échoués********************************

      
        
        private void PopulateAndBindRéussis(List<MyApps.Application.ViewModels.ResultatViewModel> listeItems)
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
        private void ListParticipantRéussi_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {

            if (ListParticipantRéussi.SelectedItem is MyApps.Application.ViewModels.ResultatViewModel donnee)
            {
                if (donnee.IdExamen == 0)
                {
                    MessageBox.Show("Séléctionner un élément dans la liste");
                    return;
                 
                }
              
                NomParticipant.Text = donnee.NomParticipant.ToString();
          
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
                 
                }
               
            }
          
        }
        private void ComboBoxModulesPerParticipant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //code pour faire une rechercher dans la liste des participants réussis 

        }

    }
}
