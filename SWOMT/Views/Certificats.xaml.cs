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

        string enregistre;
        public Certificats()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.CertificatViewModelService.GetCertificats();
            PopulateAndBind(liste);

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
            Certificat element = new Certificat(); 
            //Competence competence = new Competence();

            //if (NomCompetence.Text == "")
            //{
            //    MessageBox.Show("Il faut saisir le nom de la competence");
            //    return;
            //}



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
    }
}
