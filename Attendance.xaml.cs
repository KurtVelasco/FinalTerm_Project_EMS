using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace FinalTerm_Project_EMS
{
    /// <summary>
    /// Interaction logic for Attendance.xaml
    /// </summary>
    public partial class Attendance : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public Attendance()
        {
            InitializeComponent();
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

            int hour, minute;

            if (
                int.TryParse(cbxHour.SelectedValue.ToString(), out hour) &&
                int.TryParse(cbxMinute.SelectedValue.ToString(), out minute)
            )
            {
                DateTime timeIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);

                DB.uspTimeIn(employeeID, timeIn);
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

                MessageBox.Show(status);
            }
        }

        private void btnPullData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if ((bool)ofd.ShowDialog())
            {
                try
                {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader("attendance.csv"))
                    {
                        string line;
                        // Read and display lines from the file until the end of
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            // ID, TimeIn, TimeOut
                            string[] cols = line.Split(',');
                            string id = cols[0];
                            string strTimeIn = cols[1];
                            string strTimeOut = cols[2];

                            if 
                            (
                                int.TryParse(id, out int employeeID) &&
                                DateTime.TryParse(strTimeIn, out DateTime timeIn) &&
                                DateTime.TryParse(strTimeOut, out DateTime timeOut)
                            )
                            {
                                DB.uspUploadAttendanceData(timeIn, timeOut, employeeID);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The attendance file could not be read");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
