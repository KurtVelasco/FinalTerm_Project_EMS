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

namespace FinalTerm_Project_EMS
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public Logs()
        {
            InitializeComponent();
            FillComboBoxes();
        }
        public void FillComboBoxes()
        {
            Dictionary<int,string> logType = new Dictionary<int,string>();   
            foreach(tblLogType type in DB.tblLogTypes)
            {
                logType[type.LogTypeID] = type.LogType;
            }
            ComboBox_LogType.ItemsSource = logType;
        }


        public class DataLogs
        {
            public int EmployeeID { get; set; }
            public string LogDescription { get; set; }
            public string Logtype { get; set; } 
            public string Datetime { get; set; }
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagment_Admin ea = new EmployeeManagment_Admin();
            ea.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_LogType.SelectedIndex != -1)
            {
                ListView_Logs.ItemsSource = null;
                ListView_Logs.Items.Clear();
                int typeID = ((KeyValuePair<int, string>)ComboBox_LogType.SelectedItem).Key;
                var logsQuery = from logs in DB.tblLogs
                    join logType in DB.tblLogTypes on logs.LogTypeID equals logType.LogTypeID
                    where logs.LogTypeID == typeID
                    select new DataLogs
                    {
                        EmployeeID = logs.EmployeeID,
                        LogDescription = logs.LogDescription,
                        Logtype = logType.LogType,
                        Datetime = logs.LogDate.ToString()
                    };
                ListView_Logs.ItemsSource = logsQuery.ToList();
            }
            else
            {
                MessageBox.Show("Please Enter a valid Log Type", "Invalid log Type", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            

        }

        private void Button_SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            int employeeID = -1;
            if(!int.TryParse(TextBox_EmployeeID.Text, out employeeID))
            {
                MessageBox.Show("Please Enter a valid Employee ID", "Invalid EMployee ID", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            ListView_Logs.ItemsSource = null;
            ListView_Logs.Items.Clear();
            var logsQuery = from logs in DB.tblLogs
                join logType in DB.tblLogTypes on logs.LogTypeID equals logType.LogTypeID
                where logs.EmployeeID == employeeID
                select new DataLogs
                {
                    EmployeeID = logs.EmployeeID,
                    LogDescription = logs.LogDescription,
                    Logtype = logType.LogType,
                    Datetime = logs.LogDate.ToString()
                };
            ListView_Logs.ItemsSource = logsQuery.ToList();
       

        }
    }
}
