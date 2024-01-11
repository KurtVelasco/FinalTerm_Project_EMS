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
    /// Interaction logic for EMSLogin.xaml
    /// </summary>
    public partial class EMSLogin : Window
    {
        EmployeeDatabaseDataContext db = new EmployeeDatabaseDataContext(Properties.Settings.Default.MockEMSDatabaseConnectionString);
        public EMSLogin()
        {
            InitializeComponent();
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string password = tbx_password.Password, email = tbx_email.Text;
            List<USP_LOGIN_EMPLOYEEResult> accounts = new List<USP_LOGIN_EMPLOYEEResult>();
            accounts = db.USP_LOGIN_EMPLOYEE(email, password).ToList();
            if (accounts.Count == 1)
            {              
                if (accounts[0].PositionName == "ADMIN")
                {
                    if (accounts[0].DepartmentName == "HR")
                    {

                    }
                }
                
            }
            else
            {

            }

        }

    }
}
