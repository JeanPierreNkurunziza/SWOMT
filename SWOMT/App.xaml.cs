using SWOMT.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFGlobalExceptionHandling;

namespace SWOMT
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application, IWPFGlobalExceptionHandler
    {
        
        private void UserLogin(object sender, StartupEventArgs e) 
		{
            // Create the startup window
            try
            {
                UserLogin wnd = new UserLogin();
                // Do stuff here, e.g. to the window
                //wnd.Title = "Something else";
                // Show the window
                wnd.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Contacter service téchnique"); 
                return;
            }
   //         UserLogin wnd = new UserLogin();
			//// Do stuff here, e.g. to the window
			////wnd.Title = "Something else";
			//// Show the window
			//wnd.Show();
            this.UseGlobalExceptionHandling();
		}
        public void HandleException(Exception e)
        {
            // throw new NotImplementedException();
            // logs the exception, including inner exceptions
            //logger.Error(e);

            // shows a message box with the root exception
            MessageBox.Show(e.GetRootException().Message, "Uh-oh, Une erreur s'est produite. Veuillez réessayer!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void HandleUnrecoverableException(Exception e)
        {
           // throw new NotImplementedException();
            // logs the exception, including inner exceptions
           // logger.Fatal(e);

            // shows a message box with the root exception
            MessageBox.Show($"L'application va maintenant se fermer.\n\n'{e.GetRootException().Message}'", "Unrecoverable Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
