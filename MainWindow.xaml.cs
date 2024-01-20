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

namespace FinalTerm_Project_EMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_EMS_Click(object sender, RoutedEventArgs e)
        {
            if(LogInCredentials.EMPLOYEE_POSITION == "Administrator")
            {
                EmployeeManagment_Admin ea = new EmployeeManagment_Admin();
                ea.Show();
                this.Close();
            }
            else
            {
                EmployeeManagment_Employee ee = new EmployeeManagment_Employee();
                ee.Show();
                this.Close();   
            }
        }

        private void Button_ATM_Click(object sender, RoutedEventArgs e)
        {
            if (LogInCredentials.EMPLOYEE_POSITION == "Administrator")
            {
                AttendanceManagement_Admin ama = new AttendanceManagement_Admin();
                ama.Show();
                this.Close();
            }
            else
            {

            }
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Log out to the System?", "Log Out",MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (res == MessageBoxResult.Yes)
            {
                EMSLogin em = new EMSLogin();
                LogInCredentials.ResetData();
                em.Show();
                this.Close();
            }

        }
    }
}
