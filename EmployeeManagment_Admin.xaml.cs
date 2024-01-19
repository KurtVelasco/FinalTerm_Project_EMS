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
    /// Interaction logic for EmployeeManagment_Admin.xaml
    /// </summary>
    public partial class EmployeeManagment_Admin : Window
    {
        public EmployeeManagment_Admin()
        {
            InitializeComponent();
            tbx_welcomelabel.Content = $"Welcome, {LogInCredentials.EMPLOYEE_NAME}";
        }
        private void Button_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
           AddEmployee ae = new AddEmployee();
            ae.Show();
            this.Close();
        }

        private void Button_UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployeeDetails ue = new UpdateEmployeeDetails();
            ue.Show();
            this.Close();
        }

        private void Button_ViewLogs_Click(object sender, RoutedEventArgs e)
        {
            SearchEmployee se = new SearchEmployee();
            se.Show();
            this.Close();
        }

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Placeholder_Click(object sender, RoutedEventArgs e)
        {
            Logs l = new Logs();
            l.Show();
            this.Close();
           
        }
    }
}
