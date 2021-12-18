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
using System.Windows.Shapes;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для ComparisonWindow.xaml
    /// </summary>
    public partial class ComparisonWindow : Window
    {
        ExcelAccess dataNew;
        ExcelAccess dataOld;
        public ComparisonWindow(string fileName, string baseFileName)
        {
            InitializeComponent();
            dataGridOld.ItemsSource = null;
            dataGridOld.Items.Clear();
            dataGridNew.ItemsSource = null;
            dataGridNew.Items.Clear();
            dataOld = new ExcelAccess(baseFileName);
            try
            {
                dataGridOld.ItemsSource = dataOld.GetDataFormExcelAsync().Result;
                
                Trace.WriteLine("Done! dataGridOld was updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }

            dataNew = new ExcelAccess(fileName);
            try
            {
                dataGridNew.ItemsSource = dataNew.GetDataFormExcelAsync().Result;
                

                Trace.WriteLine("Done! dataGridNew was updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }
            //string str = "Changed ID:\n";

            //MessageBox.Show(dataGridOld.Columns.Count.ToString());

            //dataGridOld.Columns[0].Width = 30;//почему-то выкидывает исключение, но работает корректно
            //dataGridNew.Columns[0].Width = 30;//почему-то выкидывает исключение, но работает корректно
            
            //var dataGridOldItemsSource = (ObservableCollection<IST>)dataGridOld.ItemsSource;
            //var dataGridNewItemsSource = (ObservableCollection<IST>)dataGridNew.ItemsSource;
            //int i = 0;
            //while (dataGridOldItemsSource[i] != null && dataGridNewItemsSource[i] != null)
            //{

            //    if (dataGridOldItemsSource[i].Id!= dataGridNewItemsSource[i].Id)
            //        MessageBox.Show($"On {i+1} row {dataGridOldItemsSource[i].Id} != {dataGridNewItemsSource[i].Id}");
            //    //if (dataGridOldItemsSource[i].Name != dataGridNewItemsSource[i].Name)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].Name} != {dataGridNewItemsSource[i].Name}");
            //    //if (dataGridOldItemsSource[i].Description != dataGridNewItemsSource[i].Description)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].Description} != {dataGridNewItemsSource[i].Description}");
            //    //if (dataGridOldItemsSource[i].Source != dataGridNewItemsSource[i].Source)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].Source} != {dataGridNewItemsSource[i].Source}");
            //    //if (dataGridOldItemsSource[i].InfluenceObject != dataGridNewItemsSource[i].InfluenceObject)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].InfluenceObject} != {dataGridNewItemsSource[i].InfluenceObject}");
            //    //if (dataGridOldItemsSource[i].PrivacyViolation != dataGridNewItemsSource[i].PrivacyViolation)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].PrivacyViolation} != {dataGridNewItemsSource[i].PrivacyViolation}");
            //    //if (dataGridOldItemsSource[i].AccessibilityViolation != dataGridNewItemsSource[i].AccessibilityViolation)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].AccessibilityViolation} != {dataGridNewItemsSource[i].AccessibilityViolation}");
            //    //if (dataGridOldItemsSource[i].IntegrityViolation != dataGridNewItemsSource[i].IntegrityViolation)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].IntegrityViolation} != {dataGridNewItemsSource[i].IntegrityViolation}");
            //    //if (dataGridOldItemsSource[i].IntroductionDate != dataGridNewItemsSource[i].IntroductionDate)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].IntroductionDate} != {dataGridNewItemsSource[i].IntroductionDate}");
            //    //if (dataGridOldItemsSource[i].LastChangeDate != dataGridNewItemsSource[i].LastChangeDate)
            //    //    MessageBox.Show($"On {i + 1} row {dataGridOldItemsSource[i].LastChangeDate} != {dataGridNewItemsSource[i].LastChangeDate}");

            //}
            //if (dataGridOldItemsSource[i] == null && dataGridNewItemsSource[i] != null)
            //    MessageBox.Show("222");
            //if (dataGridOldItemsSource[i] != null && dataGridNewItemsSource[i] == null)
            //    MessageBox.Show("111");
        }
        public void test()
        {
            dataGridOld.Columns[0].Width = 30;//почему-то выкидывает исключение, но работает корректно
            dataGridNew.Columns[0].Width = 30;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGridOld.Columns[0].Width = 30;//почему-то выкидывает исключение, но работает корректно
            dataGridNew.Columns[0].Width = 30;
        }
    }
}
