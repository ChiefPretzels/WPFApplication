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
using System.IO;
using Microsoft.Win32;

namespace Program
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filepath = @"C:\";
        public MainWindow()
        {
            InitializeComponent();  
            //This is a comment fuckayou
            List<string> donneesContents = new List<string>(); //Contient les données du fichier de données
        }

        private void Read_File_Button_Click(object sender, RoutedEventArgs e)
        {
            bool? isDiagOpen = false;
            OpenFileDialog fileDiag = new OpenFileDialog();
            isDiagOpen = fileDiag.ShowDialog();
            if (isDiagOpen == true)
            {
                Filepath_Textbox.Text = fileDiag.FileName;
                filepath = Filepath_Textbox.Text;
            }
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
