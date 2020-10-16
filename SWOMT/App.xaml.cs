using SWOMT.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SWOMT
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
		private void UserLogin(object sender, StartupEventArgs e) 
		{
			// Create the startup window
			UserLogin wnd = new UserLogin();
			// Do stuff here, e.g. to the window
			//wnd.Title = "Something else";
			// Show the window
			wnd.Show();
		}
	}
}
