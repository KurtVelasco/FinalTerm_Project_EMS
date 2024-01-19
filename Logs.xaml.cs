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
            GetLogs();
        }

        public void GetLogs()
        {
            foreach (tblLog logs in DB.tblLogs)
            {
                ListView_Logs.Items.Add(new DataLogs
                {
                    EmployeeID = logs.EmployeeID,
                    LogDescription = logs.LogDescription, // Add this line to set LogDescription
                    Datetime = logs.LogDate.ToString() // Assuming LogDateTime is a DateTime property
                });
            }
        }

        public class DataLogs
        {
            public int EmployeeID { get; set; }
            public string LogDescription { get; set; }
            public string Datetime { get; set; }
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagment_Admin ea = new EmployeeManagment_Admin();
            ea.Show();
            this.Close();
        }
    }
}
