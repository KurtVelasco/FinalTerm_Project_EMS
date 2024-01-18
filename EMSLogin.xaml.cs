﻿using System;
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
                if(accounts[0].DepartmentName == "Human Resources")
                {
                    if (accounts[0].PositionName == "Administrator" && !LogInCredentials.ATTENDANCE)
                    { 
                        LogInCredentials.SetData(accounts[0].EmailAddress, accounts[0].EmployeeID, accounts[0].LastName,
                            accounts[0].DepartmentName, accounts[0].PositionName);
                        MessageBox.Show("Welcome, " + accounts[0].LastName + " " + accounts[0].FirstName, "Successful Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        EmployeeManagment_Admin em = new EmployeeManagment_Admin();
                        em.Show();
                        this.Close();                       
                    }
                    else if (accounts[0].PositionName == "Administrator" && LogInCredentials.ATTENDANCE)
                    {
                        LogInCredentials.SetData(accounts[0].EmailAddress, accounts[0].EmployeeID, accounts[0].LastName,
                            accounts[0].DepartmentName, accounts[0].PositionName);
                        MessageBox.Show("Welcome, " + accounts[0].LastName + " " + accounts[0].FirstName, "Successful Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        new Attendance().Show();
                        this.Close();
                    }
                    else
                    {
                        LogInCredentials.SetData(accounts[0].EmailAddress, accounts[0].EmployeeID, accounts[0].LastName,
                           accounts[0].DepartmentName, accounts[0].PositionName);
                        MessageBox.Show("Welcome:" + accounts[0].LastName + " " + accounts[0].FirstName, "Successful Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        EmployeeManagment_Employee ee = new EmployeeManagment_Employee();
                        ee.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Email/Password", "Wrong Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            } 
            else
            {
                MessageBox.Show("Incorrect Email/Password","Wrong Login",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }

    }
}
