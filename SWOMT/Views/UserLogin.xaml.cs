using MyApps.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace SWOMT.Views
{
    /// <summary>
    /// Logique d'interaction pour UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        List<MyApps.Application.ViewModels.UserRolesViewModel> UserRoleList = new List<MyApps.Application.ViewModels.UserRolesViewModel>();

        public UserLogin()
        {
            InitializeComponent();
            try
            {
                UserRoleList = MyApps.Application.Services.UserRolesViewModelService.GetUsersRoles();

                this.SelectedRolesusers();
            }
            catch (Exception)
            {
                MessageBox.Show("Vérifier la connection sqlServer !!! La base de données introuvable ");
                this.Close();
            }
            // passer la liste des roles utilisateur dans le combobox
        }

        /// <summary>
        /// Bouton pour valider les données d'un utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginSubmit(object sender, RoutedEventArgs e)
        {
            
            if (username.Text == "")
            {
                MessageBox.Show("Veuillez entrer l'utilisateur SVP!");
                return;
            }

            if (password.Password == "")
            {
                MessageBox.Show("Veuillez entrer le mot de passe SVP!");
                return;
            }

            string nomExiste = MyApps.Domain.Service.UserService.GetUtilisateurNom(username.Text);
            if (nomExiste == null)
            {
                MessageBox.Show("Le nom d'utilisateur saisie n'existe pas !!!", username.Text);
                return;
            }
            //appel d'un méthode pour vérifier si le nom et le mot de passe correspond et réturne le nom d'utilisateur
            // le mot de passé saisié est verfié via la méthode verifyCrypto 
            string utilisateurValidé = MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.VerifyCrypto(password.Password, username.Text);
            if (utilisateurValidé == null) 
            {
                MessageBox.Show("Le nom d'utilisateur et le mot de passe ne correspondent pas SVP!");
                return;
            }
            else if(utilisateurValidé != null) 
            {
                //récupérer le role d'un utilisateur 
                string utilisateurUserRole = MyApps.Domain.Service.UserService.GetUtilisateurUserRole(utilisateurValidé);

                TableauDeBord accueil = new TableauDeBord(utilisateurValidé.ToString(), utilisateurUserRole.ToString());
                accueil.Show();
                username.Text = "";
                password.Password = "";
            }
                  
        }
        /// <summary>
        /// bouton pour annuler la page de login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// bouton pour s'inscrire à nouveau
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            //affichage des champs cashés
            LabelRoleUtilisateur.Visibility = Visibility; 
            ComboBoxUserRole.Visibility=Visibility;
            ValiderInscription.Visibility = Visibility;
           
        }
        /// <summary>
        /// enregistrement d'un compte 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderInscription_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur element = new Utilisateur();
            if (username.Text == "")
            {
                MessageBox.Show("Veuillez entrer l'utilisateur SVP!");
                LabelRoleUtilisateur.Visibility = Visibility.Hidden;
                ComboBoxUserRole.Visibility = Visibility.Hidden;
                ValiderInscription.Visibility = Visibility.Hidden;
                return;
            }

            if (ValidatePassword(password.Password, out string ErrorMessage)!=true)
            {          
                MessageBox.Show(ErrorMessage);
                return;
            }
           
            element.UserName = username.Text;
            //hashage de mot de passe pour l'encoder dans la DB à l'aide de la méthode CreateHash 
            element.MotDePasse =MyApps.Domain.SecuriteService.AuthentificationService.PasswordSecurity.CreateHash(password.Password);
            element.UserRole = ComboBoxUserRole.Text;
            foreach (var donne in MyApps.Application.Services.UserViewModelService.GetUsers())
            {
                if (element.UserName == donne.UserName) 
                {
                    MessageBox.Show("les données existé déjà ! dans la base de données");
                    return;
                }
            }

            MyApps.Domain.Service.UserService.Ajouter(element); 
            username.Text = "";
            password.Password = "";
            LabelRoleUtilisateur.Visibility = Visibility.Hidden;
            ComboBoxUserRole.Visibility = Visibility.Hidden;
            ValiderInscription.Visibility = Visibility.Hidden;
        }
        private List<MyApps.Application.ViewModels.UserRolesViewModel> SelectedRolesusers()
        {

            foreach (var items in UserRoleList.Where(f => f.UserRoleName != "Admin" && f.UserRoleName != "Secrétaire"))
            {
                ComboBoxUserRole.Items.Add(items.UserRoleName);
            }

            return UserRoleList;
        }
        /// <summary>
        /// Vérifier le nombre minimun de characteur à valider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderCharacterNumbers(object sender, RoutedEventArgs e)
        {
            if (((PasswordBox)sender).Password.Length < 8)
            {
                MessageBox.Show("Il faut au moins 8 lettres ou chiffres");   
                return; 
            }
        }

        /// <summary>
        /// La validation de mot de passe qui doit respecter aux exigences de la complexité du mot de passe 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Le mot de passe ne doit pas être vide");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une lettre minuscule ";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une lettre majuscule ";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe ne doit pas être inférieur à 8 ou supérieur à 15 caractères ";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins une valeur numérique ";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins un caractère spécial ";
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
