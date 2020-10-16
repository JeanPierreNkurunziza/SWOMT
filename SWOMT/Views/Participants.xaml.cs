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
    /// Logique d'interaction pour Participants.xaml
    /// </summary>
    public partial class Participants : Page
    {
        List<MyApps.Application.ViewModels.ParticipantViewModel> liste = new List<MyApps.Application.ViewModels.ParticipantViewModel>();

        string enregistre;
        public Participants()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            PopulateAndBind(liste);
           
        }
       
       

        /// <summary>
        /// biding la liste de competence
        /// </summary>
        /// <param name="listeCompetences"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.ParticipantViewModel> listeCompetences)
        {
            Binding monBinding = new Binding
            {
                Path = new PropertyPath("Value")
            };
            ListParticipant.DataContext = listeCompetences;


        }

        


        /// <summary>
        /// afffichage apres la selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListParticipant_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if (ListParticipant.SelectedItem is MyApps.Application.ViewModels.ParticipantViewModel donnee)
            {
                IdParticipant.Text = donnee.IdParticipant.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
                DateNaissance.Text = donnee.DateNaissance.ToString();
                IdNational.Text = donnee.IdNational.ToString();
                TelParticipant.Text = donnee.TelParticipant.ToString();
                EmailParticipant.Text = donnee.EmailParticipant.ToString();
                SecteurParticipant.Text = donnee.SecteurParticipant.ToString();
                DistrictParticipant.Text = donnee.DistrictParticipant.ToString();
                DateEncodage.Text = donnee.DateEncodage.ToShortDateString();
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            enregistre = "Ajouter";
            ClearFormValues();
            ModeIsEnabledTrue();
        }
        /// <summary>
        /// méthode pour mettre à jour et ajouter  une competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MettreAjour_Click(object sender, RoutedEventArgs e)
        {
            Participant participant = new Participant();
            //Competence competence = new Competence();

            //if (NomCompetence.Text == "")
            //{
            //    MessageBox.Show("Il faut saisir le nom de la competence");
            //    return;
            //}



            if (enregistre == "Ajouter")
            {

                participant.NomParticipant = NomParticipant.Text;
                participant.DateNaissance = DateTime.Parse(DateNaissance.Text).Date;
                participant.IdNational = long.Parse(IdNational.Text);
                participant.TélParticipant = TelParticipant.Text;
                participant.EmailParticipant = EmailParticipant.Text;
                participant.SecteurParticipant = SecteurParticipant.Text;
                participant.DistrictParticipant = DistrictParticipant.Text;
                participant.DateEncodage = DateTime.Parse(DateEncodage.Text);
                MyApps.Domain.Service.ParticipantService.Create(participant);

            }

            if (enregistre == "Modifier")
            {
                participant.IdParticipant = short.Parse(IdParticipant.Text);
                participant.NomParticipant = NomParticipant.Text;
                participant.DateNaissance = DateTime.Parse(DateNaissance.Text).Date;
                participant.IdNational = long.Parse(IdNational.Text);
                participant.TélParticipant = TelParticipant.Text;
                participant.EmailParticipant = EmailParticipant.Text;
                participant.SecteurParticipant = SecteurParticipant.Text;
                participant.DistrictParticipant = DistrictParticipant.Text;
                participant.DateEncodage = DateTime.Parse(DateEncodage.Text);
                MyApps.Domain.Service.ParticipantService.Update(participant);
            }


            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            PopulateAndBind(liste);

        }
        /// <summary>
        /// méthode pour liberer le schamps à modifier une competence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

            if (IdParticipant.Text == "")
            {
                MessageBox.Show("Entrer la competence à modifier");
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

            MyApps.Domain.Service.ParticipantService.Delete(short.Parse(IdParticipant.Text));

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.ParticipantsViewModelServices.GetParticipants();
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdParticipant.Text = "";
            NomParticipant.Text = "";
            DateNaissance.Text = "";
            IdNational.Text = "";
            TelParticipant.Text = "";
            EmailParticipant.Text = "";
            SecteurParticipant.Text = "";
            DistrictParticipant.Text = "";
            DateEncodage.Text = "";
        }
        private void ModeIsEnabledTrue()
        {
            IdParticipant.IsEnabled = true;
            NomParticipant.IsEnabled = true;
            DateNaissance.IsEnabled = true;
            IdNational.IsEnabled = true;
            TelParticipant.IsEnabled = true;
            EmailParticipant.IsEnabled = true;
            SecteurParticipant.IsEnabled = true;
            DistrictParticipant.IsEnabled = true;
            DateEncodage.IsEnabled = true;
        }
        private void ModeIsEnabledFalse()
        {
            IdParticipant.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            DateNaissance.IsEnabled = false;
            IdNational.IsEnabled = false;
            TelParticipant.IsEnabled = false;
            EmailParticipant.IsEnabled = false;
            SecteurParticipant.IsEnabled = false;
            DistrictParticipant.IsEnabled = false;
            DateEncodage.IsEnabled = false;
        }
    }
}
