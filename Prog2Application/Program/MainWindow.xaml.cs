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
        string filepath = "";
        bool abortStop = false;
        List<string> donneesContents = new List<string>(); //Contient les données du fichier de données
        public MainWindow()
        {
            InitializeComponent();
            Filepath_Textbox.IsReadOnly = true;
            ToggleEnable(false);
            Delete_Button.IsEnabled = false;
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
            if (donneesContents.Count > 0 && filepath != "")
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    foreach (string item in donneesContents)
                        writer.WriteLine(item);
                }

            }
            else
                abortStop = true;
        }

        private void ToggleEnable(bool isEnabled)
        {
            if (isEnabled)
            {
                Modify_Button.IsEnabled = true;
                Ascending_Order_Radio.IsEnabled = true;
                Descending_Order_Radio.IsEnabled = true;
                Save_Button.IsEnabled = true;
            }
            else
            {
                Modify_Button.IsEnabled = false;
                Ascending_Order_Radio.IsEnabled = false;
                Descending_Order_Radio.IsEnabled = false;
                Save_Button.IsEnabled = false;
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

            if (filepath != "")
            {
                using (StreamReader reader = new StreamReader(filepath))
                    while (!reader.EndOfStream)
                        donneesContents.Add(reader.ReadLine());

                ToggleEnable(true);
            }

            ActualiserListe();
        }

        private void Quit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Display_Box.HasItems)
            {
                MessageBoxResult boxResult = MessageBox.Show("Voulez-vous sauvegarder?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (boxResult == MessageBoxResult.Yes)
                {
                    bool? isDiagOpen = false;
                    SaveFileDialog fileDiag = new SaveFileDialog();
                    isDiagOpen = fileDiag.ShowDialog();
                    filepath = fileDiag.FileName;
                    Sauvegarder();
                }
                else if (boxResult == MessageBoxResult.Cancel)
                    abortStop = true;
            }
            if (!abortStop)
                Application.Current.Shutdown();
            abortStop = false;
        }

        private void Display_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] currentLine = new string[3];
            try
            {
                currentLine = Display_Box.SelectedItem.ToString().Split(' ');
            }
            catch (Exception) { }

            if (currentLine[0] != "" && currentLine[0] != null)
                Delete_Button.IsEnabled = true;
            else
                Delete_Button.IsEnabled = false;


            Name_Textbox.Text = currentLine[0];
            Fam_Name_Textbox.Text = currentLine[1];
            Expenses_Textbox.Text = currentLine[2];
            try
            {
                if ((Name_Textbox.Text + " " + Fam_Name_Textbox.Text + " " + Expenses_Textbox.Text) == Display_Box.SelectedItem.ToString())
                    Modify_Button.IsEnabled = false;
                else
                    Modify_Button.IsEnabled = true;
            }
            catch (Exception) { }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            string toAdd = "";

            toAdd += Name_Textbox.Text + " " + Fam_Name_Textbox.Text + " " + Expenses_Textbox.Text;
            donneesContents.Add(toAdd);
            if (Display_Box.HasItems)
                ToggleEnable(true);
            
            ActualiserListe();
            
            Display_Box.SelectedIndex = Display_Box.Items.Count - 1;

            Ascending_Order_Radio.IsChecked = false;
            Descending_Order_Radio.IsChecked = false; 
        }

        private void Modify_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = Display_Box.SelectedIndex;
            string toAdd = "";


            toAdd += Name_Textbox.Text + " " + Fam_Name_Textbox.Text + " " + Expenses_Textbox.Text;

            donneesContents[index] = toAdd;

            ActualiserListe();
            Display_Box.SelectedItem = toAdd;

            Ascending_Order_Radio.IsChecked = false;
            Descending_Order_Radio.IsChecked = false;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = Display_Box.SelectedIndex;

            donneesContents.RemoveAt(index);
            if (!Display_Box.HasItems)
                ToggleEnable(false);

            ActualiserListe();

            if (index == Display_Box.Items.Count)
                Display_Box.SelectedIndex = index - 1;
            else
                Display_Box.SelectedIndex = index;
        }

        private void Ascending_Order_Radio_Checked(object sender, RoutedEventArgs e)
        {
            if (donneesContents.Count > 0)
            {
                QuicksortC(ref donneesContents, 0, donneesContents.Count - 1);
                ActualiserListe();
            }
            else
                Ascending_Order_Radio.IsChecked = false;
        }

        private void Descending_Order_Radio_Checked(object sender, RoutedEventArgs e)
        {
            if (donneesContents.Count > 0)
            {
                QuicksortD(ref donneesContents, 0, donneesContents.Count - 1);
                ActualiserListe();
            }
            else
                Descending_Order_Radio.IsChecked = false;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            bool? isDiagOpen = false;
            SaveFileDialog fileDiag = new SaveFileDialog();
            isDiagOpen = fileDiag.ShowDialog();
            filepath = fileDiag.FileName;
            Sauvegarder();
        }

        private void Name_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Name_Textbox.Text == "" || Name_Textbox.Text.Contains(" ") || Fam_Name_Textbox.Text == "" || Fam_Name_Textbox.Text.Contains(" ") || Expenses_Textbox.Text == "" || Expenses_Textbox.Text.Contains(" "))
                {
                    Add_Button.IsEnabled = false;
                    Modify_Button.IsEnabled = false;
                }
                else
                {
                    Add_Button.IsEnabled = true;
                    Modify_Button.IsEnabled = true;
                }
            }
            catch (Exception) { }
        }

        private void Fam_Name_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Name_Textbox.Text == "" || Name_Textbox.Text.Contains(" ") || Fam_Name_Textbox.Text == "" || Fam_Name_Textbox.Text.Contains(" ") || Expenses_Textbox.Text == "" || Expenses_Textbox.Text.Contains(" "))
                {
                    Add_Button.IsEnabled = false;
                    Modify_Button.IsEnabled = false;
                }
                else
                {
                    Add_Button.IsEnabled = true;
                    Modify_Button.IsEnabled = true;
                }
            }
            catch (Exception) { }
        }

        private void Expenses_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Name_Textbox.Text == "" || Name_Textbox.Text.Contains(" ") || Fam_Name_Textbox.Text == "" || Fam_Name_Textbox.Text.Contains(" ") || Expenses_Textbox.Text == "" || Expenses_Textbox.Text.Contains(" "))
                {
                    Add_Button.IsEnabled = false;
                    Modify_Button.IsEnabled = false;
                }
                else
                {
                    Add_Button.IsEnabled = true;
                    Modify_Button.IsEnabled = true;
                }
            }
            catch (Exception) { }
        }
    }
}
