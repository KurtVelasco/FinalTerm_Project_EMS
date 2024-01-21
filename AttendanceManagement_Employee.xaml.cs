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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AttendanceManagement_Employee : Window
    {
        public AttendanceManagement_Employee()
        {
            InitializeComponent();
        }

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_RequestLeave_Click(object sender, RoutedEventArgs e)
        {
            LeaveRequestForm lrf = new LeaveRequestForm();
            lrf.Show();
            this.Close();
        }
    }
}
