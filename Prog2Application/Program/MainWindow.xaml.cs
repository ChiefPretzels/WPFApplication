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

        private void QuicksortC(ref List<string> elements, int left, int right)
        {
            int i = left, j = right;
            string pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                    i++;
                while (elements[j].CompareTo(pivot) > 0)
                    j--;
                if (i <= j)
                {
                    string tmpEle = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmpEle;
                    i++;
                    j--;
                }
            }

            if (left < j)
                QuicksortC(ref elements, left, j);
            if (i < right)
                QuicksortC(ref elements, i, right);
        }

        private void QuicksortD(ref List<string> elements, int left, int right)
        {
            int i = left, j = right;
            string pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) > 0)
                    i++;
                while (elements[j].CompareTo(pivot) < 0)
                    j--;
                if (i <= j)
                {
                    string tmpEle = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmpEle;
                    i++;
                    j--;
                }
            }

            if (left < j)
                QuicksortD(ref elements, left, j);
            if (i < right)
                QuicksortD(ref elements, i, right);
        }

        private void Sauvegarder()
        {
            if (donneesContents.Count > 0)
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    foreach (string item in donneesContents)
                        writer.WriteLine(item);
                }
                
            }
        }

        private void Read_File_Button_Click(object sender, RoutedEventArgs e)
        {
            bool? isDiagOpen = false;
            OpenFileDialog fileDiag = new OpenFileDialog();
            isDiagOpen = fileDiag.ShowDialog();

            if (isDiagOpen == true)
            {
                Filepath_Textbox.Text = fileDiag.FileName;
                filepath = fileDiag.FileName;
            }

            donneesContents.Clear();

            using (StreamReader reader = new StreamReader(filepath))
                while (!reader.EndOfStream)
                    donneesContents.Add(reader.ReadLine());

            ActualiserListe();
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Voulez-vous sauvegarder?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.Yes)
                Sauvegarder();
            else if (boxResult == MessageBoxResult.Cancel)
                return;
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

            toAdd += Name_Textbox.Text + " " + Fam_Name_Textbox.Text + " " + Expenses_Textbox.Text;

            donneesContents.Add(toAdd);

            ActualiserListe();

            Display_Box.SelectedIndex = Display_Box.Items.Count - 1;

            Ascending_Order_Radio.IsChecked = false;
            Descending_Order_Radio.IsChecked = false;
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

            Ascending_Order_Radio.IsChecked = false;
            Descending_Order_Radio.IsChecked = false;
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

        private void Ascending_Order_Radio_Checked(object sender, RoutedEventArgs e)
        {
            QuicksortC(ref donneesContents, 0, donneesContents.Count - 1);
            ActualiserListe(); 
        }

        private void Descending_Order_Radio_Checked(object sender, RoutedEventArgs e)
        {
            QuicksortD(ref donneesContents, 0, donneesContents.Count - 1);
            ActualiserListe();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            bool? isDiagOpen = false;
            SaveFileDialog fileDiag = new SaveFileDialog();
            isDiagOpen = fileDiag.ShowDialog();
            filepath = fileDiag.FileName;
            Sauvegarder();
        }
    }
}
