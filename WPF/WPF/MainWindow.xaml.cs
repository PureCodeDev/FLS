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
using System.Net;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;
using System.Collections.ObjectModel;


namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        static bool compactView = false;

        double MainiWindowHeight = 663.062;
        double MainiWindowWidth = 1271.198;
        static string remoteUri = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        static string fileName = "thrlist.xlsx";

        string path = Environment.CurrentDirectory + "\\" + fileName;
        static string baseFileName = "Base.xlsx";

        ExcelAccess data;

        IST threat = new IST();

        private static void LoadfromNet()
        {
            try
            {
                WebClient myWebClient = new WebClient();

                Trace.WriteLine($"Downloading File \"{fileName}\" from \"{remoteUri}\" .......\n\n");
                //ОБЯЗАТЕЛЬНО ДОБАВИТЬ ПРОВЕРКУ ОТКРЫТ ЛИ fileName файл, т.к. иначе он не сможет загрузить и перезаписать файлик и вылетит с исключением
                myWebClient.DownloadFile(remoteUri, fileName);
                Trace.WriteLine($"Successfully Downloaded File \"{fileName}\" from \"{remoteUri}\"");
                Trace.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Environment.CurrentDirectory);
                Trace.WriteLine("Done! Data was successfully downloaded");
                
                //UpdateBaseFile();
            }
            catch
            {
                MessageBox.Show("Somthing went wrong when downloading file!!!!");
            }
        }
        private static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();

                return false;
            }

            do
            {
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }
        private static void UpdateBaseFile()
        {
            try
            {
                if (!File.Exists(baseFileName))
                {
                    CreateBase();
                    MessageBox.Show($"File {baseFileName} was created!");
                }
                else if (FileCompare(fileName, baseFileName))
                {
                    MessageBox.Show($"New file({fileName}) is full copy the old one({baseFileName})! No changes\'re out there!");
                }
                else
                {
                    ComparisonWindow compWindow = new ComparisonWindow(fileName, baseFileName);
                    compWindow.Show();
                }

            }
            catch
            {
                MessageBox.Show("Somthing went wrong when database file created!!!!");
            }
        }

        private static void CreateBase()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    File.Copy(fileName, baseFileName, true);
                    Trace.WriteLine($"Done! Data was successfully copied from {fileName} to {baseFileName}");
                }
                catch
                {
                    MessageBox.Show("Something went wrong when creating BASE file!!!!");
                }
            }
            else
            {
                MessageBox.Show($"You haven't downloaded new copy of {fileName}. \n\tMake sure you have downloaded it and call LoadfromNet method!");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //if (this.WindowState == System.Windows.WindowState.Normal)
            //{
            //    this.WindowState = System.Windows.WindowState.Maximized;
            //}
            // if(FileStream fs = new FileStream())

            //if (!File.Exists(path))
                LoadfromNet();

            data = new ExcelAccess(baseFileName);
            try
            {
                dataGrid.ItemsSource = data.GetDataFormExcelAsync().Result;
                dataGrid.Columns[0].Width = 30;
                Trace.WriteLine("Done! dataGrid was updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }


            //DataTable table = new OleDbEnumerator().GetElements();
            //string inf = "";
            //foreach (DataRow row in table.Rows)
            //    inf += row["SOURCES_NAME"] + "\n";

            //MessageBox.Show(inf);

        }
        //static ObservableCollection<IST> source;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Items.Clear();

            LoadfromNet();

            data = new ExcelAccess(baseFileName);
            try
            {
                dataGrid.ItemsSource = data.GetDataFormExcelAsync().Result;

                Trace.WriteLine("Done! dataGrid was updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            CreateBase();

            ComparisonWindow compWindow = new ComparisonWindow(fileName, baseFileName);
            compWindow.Show();

            compWindow.test();
        }

        private void CompactviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!compactView)
            {
                //var dataGridItemsSource = (ObservableCollection<IST>)dataGrid.ItemsSource;
                //MessageBox.Show(dataGrid.Columns.Count.ToString());
                // dataGrid.MinColumnWidth = 0;
                //for (int i = 0; i != 3; i++)
                //{
                //    dataGrid.Columns[i].Visibility = Visibility.Visible;
                //    //dataGrid.Columns.RemoveAt(i);
                //}
                //dataGrid.Visibility = 
                //var dataGridItemsSource = new ObservableCollection<IST>((ObservableCollection<IST>)dataGrid.ItemsSource);
                // MessageBox.Show(dataGridItemsSource[0].Id + dataGridItemsSource[0].Name + dataGridItemsSource[0].Description + dataGridItemsSource[0].Source);
                dataGrid.Width -= 5 * dataGrid.ColumnWidth.Value - 30;
                //button.A

                Application.Current.MainWindow.Width -= 5 * dataGrid.ColumnWidth.Value - 10;
                Application.Current.MainWindow.Height = (Canvas.GetTop(compactviewButton) + compactviewButton.Height + 50);
                Application.Current.MainWindow.Left = 1280 / 2 - Application.Current.MainWindow.Width / 2;
                Application.Current.MainWindow.Top = 720 / 2 - Application.Current.MainWindow.Height / 2;

                //Canvas.SetRight(button, 5 * dataGrid.ColumnWidth.Value);

                Canvas.SetLeft(button, Canvas.GetLeft(button) - (5 * dataGrid.ColumnWidth.Value));
                Canvas.SetLeft(saveButton, Canvas.GetLeft(saveButton) - (5 * dataGrid.ColumnWidth.Value));
                Canvas.SetLeft(compactviewButton, Canvas.GetLeft(compactviewButton) - (5 * dataGrid.ColumnWidth.Value));
                //Canvas.SetRight(saveButton, Canvas.GetLeft(button) - (5 * dataGrid.ColumnWidth.Value));
                //Canvas.SetRight(compactviewButton, 5 * dataGrid.ColumnWidth.Value);

                //button.Margin = new Thickness(button.Margin.Left, button.Margin.Top, 200, button.Margin.Bottom);

                // button.Margin = new Thickness(0,100,100,0);
                compactView = !compactView;
                compactviewButton.Content = "FullView";

            }
            else
            {

                //MessageBox.Show(System.Windows.SystemParameters.PrimaryScreenWidth.ToString() +                System.Windows.SystemParameters.PrimaryScreenHeight.ToString());
                Application.Current.MainWindow.Width = MainiWindowWidth;
                Application.Current.MainWindow.Height = MainiWindowHeight;
                Application.Current.MainWindow.Left = 1280 / 2 - Application.Current.MainWindow.Width / 2;
                Application.Current.MainWindow.Top = 720 / 2 - Application.Current.MainWindow.Height / 2;

                dataGrid.Width += 5 * dataGrid.ColumnWidth.Value - 30;

                Canvas.SetLeft(button, Canvas.GetLeft(button) + (5 * dataGrid.ColumnWidth.Value));
                Canvas.SetLeft(saveButton, Canvas.GetLeft(saveButton) + (5 * dataGrid.ColumnWidth.Value));
                Canvas.SetLeft(compactviewButton, Canvas.GetLeft(compactviewButton) + (5 * dataGrid.ColumnWidth.Value));

                compactView = !compactView;
                compactviewButton.Content = "CompactView";
            }



        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                FrameworkElement id = dataGrid.Columns[0].GetCellContent(e.Row);
                if (id.GetType() == typeof(TextBox))
                {
                    threat.Id = ((TextBox)id).Text;//Convert.ToInt32(((TextBox)id).Text);
                }

                FrameworkElement name = dataGrid.Columns[1].GetCellContent(e.Row);
                if (name.GetType() == typeof(TextBox))
                {
                    threat.Name = ((TextBox)name).Text;
                }

                FrameworkElement description = dataGrid.Columns[2].GetCellContent(e.Row);
                if (description.GetType() == typeof(TextBox))
                {
                    threat.Description = ((TextBox)description).Text;
                }

                FrameworkElement source = dataGrid.Columns[3].GetCellContent(e.Row);
                if (source.GetType() == typeof(TextBox))
                {
                    threat.Source = ((TextBox)source).Text;
                }

                FrameworkElement influenceObject = dataGrid.Columns[4].GetCellContent(e.Row);
                if (source.GetType() == typeof(TextBox))
                {
                    threat.Source = ((TextBox)source).Text;
                }

                FrameworkElement privacyViolation = dataGrid.Columns[5].GetCellContent(e.Row);
                if (privacyViolation.GetType() == typeof(TextBox))
                {
                    threat.PrivacyViolation = ((TextBox)privacyViolation).Text;
                }

                FrameworkElement accessibilityViolation = dataGrid.Columns[6].GetCellContent(e.Row);
                if (accessibilityViolation.GetType() == typeof(TextBox))
                {
                    threat.AccessibilityViolation = ((TextBox)accessibilityViolation).Text;
                }

                FrameworkElement integrityViolation = dataGrid.Columns[7].GetCellContent(e.Row);
                if (integrityViolation.GetType() == typeof(TextBox))
                {
                    threat.IntegrityViolation = ((TextBox)integrityViolation).Text;
                }

                FrameworkElement introductionDate = dataGrid.Columns[8].GetCellContent(e.Row);
                if (introductionDate.GetType() == typeof(TextBox))
                {
                    threat.IntroductionDate = ((TextBox)introductionDate).Text;
                }

                FrameworkElement lastChangeDate = dataGrid.Columns[9].GetCellContent(e.Row);
                if (lastChangeDate.GetType() == typeof(TextBox))
                {
                    threat.LastChangeDate = ((TextBox)lastChangeDate).Text;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                bool IsSave = data.InsertOrUpdateRowInExcelAsync(threat).Result;
                if (IsSave)
                {
                    MessageBox.Show("Record Saved Successfully");
                }
                else
                {
                    MessageBox.Show("Problem Occured");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            threat = dataGrid.SelectedItem as IST;
        }


    }
}
