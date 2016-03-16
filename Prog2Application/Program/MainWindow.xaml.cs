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
        List<string> donneesContents = new List<string>(); //Contient les données du fichier de données
        public MainWindow()
        {
            InitializeComponent();
            Filepath_Textbox.IsReadOnly = true;
        }

        private void ActualiserListe()
        {
            Display_Box.Items.Clear();
            foreach (string item in donneesContents)
            {
                string tmpString = "";
                tmpString = item.Replace(';', ' ');
                Display_Box.Items.Add(tmpString);
            }
        }

        private void Read_File_Button_Click(object sender, RoutedEventArgs e)
        {
            donneesContents.Clear();
            bool? isDiagOpen = false;
            OpenFileDialog fileDiag = new OpenFileDialog();
            isDiagOpen = fileDiag.ShowDialog();

            if (isDiagOpen == true)
            {
                Filepath_Textbox.Text = fileDiag.FileName;
                filepath = fileDiag.FileName;
            }

            using (StreamReader reader = new StreamReader(filepath))
                while (!reader.EndOfStream)
                    donneesContents.Add(reader.ReadLine());

            ActualiserListe();
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Display_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] currentLine = new string[10];
            try
            {
                currentLine = Display_Box.SelectedItem.ToString().Split(' ');
            }
            catch (Exception)
            {
            }
            Name_Textbox.Text = currentLine[0];
            Fam_Name_Textbox.Text = currentLine[1];
            Expenses_Textbox.Text = currentLine[2];
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            string toAdd = "";

            toAdd += Name_Textbox.Text;
            toAdd += " " + Fam_Name_Textbox.Text;
            toAdd += " " + Expenses_Textbox.Text;

            donneesContents.Add(toAdd);

            ActualiserListe();

            Display_Box.SelectedIndex = Display_Box.Items.Count - 1;
        }

        private void Modify_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = Display_Box.SelectedIndex;
            string toAdd = "";


            toAdd += Name_Textbox.Text;
            toAdd += " " + Fam_Name_Textbox.Text;
            toAdd += " " + Expenses_Textbox.Text;

            donneesContents[index] = toAdd;

            ActualiserListe();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = Display_Box.SelectedIndex;

            donneesContents.RemoveAt(index);

            ActualiserListe();

            if (index == Display_Box.Items.Count)
                Display_Box.SelectedIndex = index - 1;
            else
                Display_Box.SelectedIndex = index;
        }
    }
}
