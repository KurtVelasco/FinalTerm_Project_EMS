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
    public partial class AttendanceManagement_Admin : Window
    {
        public AttendanceManagement_Admin()
        {
            InitializeComponent();
        }

        private void Button_ManageLeaveRequests_Click(object sender, RoutedEventArgs e)
        {
            ManageLeaveRequests mlr = new ManageLeaveRequests();
            mlr.Show();
            this.Close();
        }

        private void Button_PullAttendance_Click(object sender, RoutedEventArgs e)
        {
            PullAttendance pa = new PullAttendance();
            pa.Show();
            this.Close();
        }

        private void Button_ManualAttendance_Click(object sender, RoutedEventArgs e)
        {
            ManualAttendance ma = new ManualAttendance();
            ma.Show();
            this.Close();
        }

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            EMSLogin el = new EMSLogin();
            el.Show();
            this.Close();
        }
    }
}
