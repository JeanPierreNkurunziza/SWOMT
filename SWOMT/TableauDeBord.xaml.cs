using SWOMT.Views;
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
using System.Windows.Shapes;

namespace SWOMT
{
    /// <summary>
    /// Logique d'interaction pour TableauDeBord.xaml
    /// </summary>
    public partial class TableauDeBord : Window
    {
        public TableauDeBord()
        {
            InitializeComponent();
           // this.WindowState = WindowState.Maximized; 
        }


        private void GestionParticipants(object sender, RoutedEventArgs e)
        {
            Main.Content = new Participants();
        }

        private void GestionFormation(object sender, RoutedEventArgs e)
        {
            Main.Content = new Formations();
        }

        private void GestionSite(object sender, RoutedEventArgs e)
        {
            Main.Content = new Sites();
        }

        private void PlanningFormation(object sender, RoutedEventArgs e)
        {
            Main.Content = new PlanningFormations();
        }

        private void GestionFormateur(object sender, RoutedEventArgs e)
        {
            Main.Content = new Formateurs();
        }

        private void GestionCertificat(object sender, RoutedEventArgs e)
        {
            Main.Content = new Certificats();
        }

        private void GestionExamen(object sender, RoutedEventArgs e)
        {
            Main.Content = new Exames();
        }

        private void GestionModule(object sender, RoutedEventArgs e)
        {
            Main.Content = new Modules();
        }

        private void GestionSiteModule(object sender, RoutedEventArgs e)
        {
            Main.Content = new SitePlannings();
        }

        private void GestionRésultat(object sender, RoutedEventArgs e)
        {
            Main.Content = new Resultats();
        }

        private void GestionInscription(object sender, RoutedEventArgs e)
        {
            Main.Content = new Inscriptions();
        }

        private void GestionDePresence(object sender, RoutedEventArgs e)
        {
            Main.Content = new Presences();
        }

        private void GestionFormateurModule(object sender, RoutedEventArgs e)
        {
            Main.Content = new FormateurModules();
        }
    }
}
