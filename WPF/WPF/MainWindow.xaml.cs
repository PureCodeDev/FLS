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


namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        static string remoteUri = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        static string fileName = "thrlist.xlsx";

        string path = Environment.CurrentDirectory + "\\" + fileName;
        //static string basefileName = "Base.xlsx";

        ExcelAccess data;

        IST threat = new IST();

        private static void LoadfromNet()
        {
            try
            {
                WebClient myWebClient = new WebClient();

                Trace.WriteLine($"Downloading File \"{fileName}\" from \"{remoteUri}\" .......\n\n");

                myWebClient.DownloadFile(remoteUri, fileName);
                Trace.WriteLine($"Successfully Downloaded File \"{fileName}\" from \"{remoteUri}\"");
                Trace.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Environment.CurrentDirectory);
            }
            catch
            {
                MessageBox.Show("Somthing went wrong when downloading file!!!!");
            }
            finally
            {
                Trace.WriteLine("DONE!");
            }

        }
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // if(FileStream fs = new FileStream())

            if (!File.Exists(path))
                LoadfromNet();

            data = new ExcelAccess();
            try
            {
                dataGrid.ItemsSource = data.GetDataFormExcelAsync().Result;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Items.Clear();

            LoadfromNet();

            data = new ExcelAccess();
            try
            {
                dataGrid.ItemsSource = data.GetDataFormExcelAsync().Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("DDDDDDDDDDDDDDDDDDDDDDDDDDD");
        }
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    data = new ExcelAccess();
        //    try
        //    {
        //        dataGrid.ItemsSource = data.GetDataFormExcelAsync().Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                FrameworkElement id = dataGrid.Columns[0].GetCellContent(e.Row);
                if (id.GetType() == typeof(TextBox))
                {
                    threat.Id = Convert.ToInt32(((TextBox)id).Text);
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

                FrameworkElement privacyViolation = dataGrid.Columns[4].GetCellContent(e.Row);
                if (privacyViolation.GetType() == typeof(TextBox))
                {
                    threat.PrivacyViolation = ((TextBox)privacyViolation).Text;
                }

                FrameworkElement accessibilityViolation = dataGrid.Columns[5].GetCellContent(e.Row);
                if (accessibilityViolation.GetType() == typeof(TextBox))
                {
                    threat.AccessibilityViolation = ((TextBox)accessibilityViolation).Text;
                }

                FrameworkElement integrityViolation = dataGrid.Columns[6].GetCellContent(e.Row);
                if (integrityViolation.GetType() == typeof(TextBox))
                {
                    threat.IntegrityViolation = ((TextBox)integrityViolation).Text;
                }

                FrameworkElement introductionDate = dataGrid.Columns[7].GetCellContent(e.Row);
                if (introductionDate.GetType() == typeof(TextBox))
                {
                    threat.IntroductionDate = ((TextBox)introductionDate).Text;
                }

                FrameworkElement lastChangeDate = dataGrid.Columns[8].GetCellContent(e.Row);
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
