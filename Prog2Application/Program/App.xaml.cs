using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Program
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            PasswordWindow passWindow = new PasswordWindow();
            MainWindow mainWindow = new MainWindow();
            passWindow.Title = "Password";
            passWindow.Show();
        }
    }
}
