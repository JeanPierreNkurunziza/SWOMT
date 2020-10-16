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
    /// Logique d'interaction pour Presences.xaml
    /// </summary>
    public partial class Presences : Page
    {
        List<MyApps.Application.ViewModels.PresenceViewModel> liste = new List<MyApps.Application.ViewModels.PresenceViewModel>();

        string enregistre;
        public Presences()
        {
            InitializeComponent();
            liste = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            PopulateAndBind(liste);

        }



        /// <summary>
        /// biding la liste de sites
        /// </summary>
        /// <param name="liste"></param>
        private void PopulateAndBind(List<MyApps.Application.ViewModels.PresenceViewModel> listeItems)
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
            if (ListElement.SelectedItem is MyApps.Application.ViewModels.PresenceViewModel donnee)
            {
                IdPresence.Text = donnee.IdPresence.ToString();
                IdModule.Text = donnee.IdSiteModule.ToString();
                IdParticipant.Text = donnee.IdParticipant.ToString();
                NomModule.Text = donnee.NomModule.ToString();
                NomParticipant.Text = donnee.NomParticipant.ToString();
                DateHeureDePresence.Text = donnee.DateHeureDePresence.ToString();
                EstPresent.Text = donnee.EstPresent.ToString();

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
            Presence element = new Presence();
            //Competence competence = new Competence();

            if (IdModule.Text == "")
            {
                MessageBox.Show("Il faut saisir le nom ");
                return;
            }



            if (enregistre == "Ajouter")
            {

                //element.IdModuleInscription = short.Parse(IdModuleInscription.Text);
                element.IdSiteModule = short.Parse(IdModule.Text);
                element.IdParticipant = short.Parse(IdParticipant.Text);
                element.DateHeureDePresence = DateTime.Parse(DateHeureDePresence.Text).Date;
                element.EstPresent = bool.Parse(EstPresent.Text);

                MyApps.Domain.Service.PresenceService.Create(element);

            }

            if (enregistre == "Modifier")
            {

                //element.IdModuleInscription = short.Parse(IdModuleInscription.Text); 
                element.IdSiteModule = short.Parse(IdModule.Text);
                element.IdParticipant = short.Parse(IdParticipant.Text);
                element.DateHeureDePresence = DateTime.Parse(DateHeureDePresence.Text).Date;
                element.EstPresent = bool.Parse(EstPresent.Text);

                MyApps.Domain.Service.PresenceService.Update(element);
            }


            ModeIsEnabledFalse();
            liste.Clear();
            liste = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            PopulateAndBind(liste);

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

            MyApps.Domain.Service.PresenceService.Delete(short.Parse(IdPresence.Text));

            ClearFormValues();
            liste.Clear();
            liste = MyApps.Application.Services.PresenceViewModelService.GetPresences();
            PopulateAndBind(liste);

        }
        private void ClearFormValues()
        {
            IdPresence.Text = "";
            IdModule.Text = "";
            IdParticipant.Text = "";
            NomModule.Text = "";
            NomParticipant.Text = "";
            DateHeureDePresence.Text = "";
            EstPresent.Text = "";


        }
        private void ModeIsEnabledTrue()
        {
            IdPresence.IsEnabled = false;
            IdModule.IsEnabled = true;
            IdParticipant.IsEnabled = true;
            NomModule.IsEnabled = true;
            NomParticipant.IsEnabled = true;
            DateHeureDePresence.IsEnabled = true;
            EstPresent.IsEnabled = true;

        }
        private void ModeIsEnabledFalse()
        {
            IdPresence.IsEnabled = false;
            IdModule.IsEnabled = false;
            IdParticipant.IsEnabled = false;
            NomModule.IsEnabled = false;
            NomParticipant.IsEnabled = false;
            DateHeureDePresence.IsEnabled = false;
            EstPresent.IsEnabled = false;

        }
    }
}
