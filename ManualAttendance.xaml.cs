using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static FinalTerm_Project_EMS.SearchEmployee;

namespace FinalTerm_Project_EMS
{
    /// <summary>
    /// Interaction logic for Attendance.xaml
    /// </summary>
    public partial class ManualAttendance : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();
       
        public ManualAttendance()
        {
            InitializeComponent();

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            for (int i = 0; i < 24; i++)
            {
                cbxHour.Items.Add(i.ToString());
            }
            for (int i = 0; i < 60; i++)
            {
                cbxMinute.Items.Add(i.ToString());
            }
        }

        private void btnTimeIn_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields()) 
                return;

            int employeeID = -1;

            foreach (tblEmployee employee in DB.tblEmployees)
            {
                if (employee.EmailAddress == tbxEmail.Text)
                {
                    employeeID = employee.EmployeeID;
                }
            }

            if (employeeID == -1)
            {
                MessageBox.Show($"No employee with the email of {tbxEmail.Text} was found.");
                return;
            }

            int hour, minute;

            if (
                int.TryParse(cbxHour.SelectedValue.ToString(), out hour) &&
                int.TryParse(cbxMinute.SelectedValue.ToString(), out minute)
            )
            {
                DateTime timeIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);

                bool? success = false;

                DB.uspTimeIn(employeeID, timeIn, ref success);

                if ((bool)!success)
                {
                    MessageBox.Show("Time-in failed. Employee already times in for today");
                }
                else
                {
                    MessageBox.Show("Time-in successful");
                    InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "Admin Manually Timed-In EmployeeOD: " + employeeID , 2);
                }
            }
            else
            {
                MessageBox.Show("Failed to convert Hour and/or Minute values. Please try again.");
            }


        }

        private bool ValidateFields()
        {
            // No Employee Email
            if (tbxEmail.Text.Length < 1)
            {
                MessageBox.Show("Please fill out the Employee Email field.");
                return false;
            }
            // Invalid Email
            if (!tbxEmail.Text.Contains('@') || !tbxEmail.Text.Contains('.'))
            {
                MessageBox.Show("Please enter a valid email.");
                return false;
            }
            // No hour selected
            if (cbxHour.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the hour of time-in/time-out.");
                return false;
            }
            // No minute selected
            if (cbxMinute.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the minute of time-in/time-out.");
                return false;
            }
            // No date selected
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Please select the datge of the time-in/time-out.");
                return false;
            }

            return true;
        }

        private void btnTimeOut_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
                return;

            int employeeID = -1;

            foreach (tblEmployee employee in DB.tblEmployees)
            {
                if (employee.EmailAddress == tbxEmail.Text)
                {
                    employeeID = employee.EmployeeID;
                }
            }

            int hour, minute;

            if (
                int.TryParse(cbxHour.SelectedValue.ToString(), out hour) &&
                int.TryParse(cbxMinute.SelectedValue.ToString(), out minute)
            )
            {
                DateTime timeOut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);

                string status = "";
                DB.uspTimeOut(employeeID, DateTime.Now, timeOut, ref status);
                InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "Admin Manually Timed-Out EmployeeID: " + employeeID, 2);
                MessageBox.Show(status);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AttendanceManagement_Admin ama = new AttendanceManagement_Admin();
            ama.Show();
            this.Close();
        }
    }
}
