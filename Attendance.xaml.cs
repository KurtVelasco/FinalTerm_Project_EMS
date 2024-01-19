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
    public partial class Attendance : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();
       
        public Attendance()
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
                    using (StreamReader sr = new StreamReader("attendance.csv"))
                    {
                        Dictionary<int, string> errRowsDict = new Dictionary<int, string>();
                        List<int> employeesInAttendance = new List<int>();
                        string line;
                        int rowNum = 0;

                        while ((line = sr.ReadLine()) != null)
                        {
                            // ID, TimeIn, TimeOut
                            string[] cols = line.Split(',');

                            // Missing columns
                            if (cols.Length < 3 )
                            {
                                errRowsDict.Add(rowNum, $"One or more columns were missing.");
                                continue;
                            }

                            string id = cols[0];
                            string strTimeIn = cols[1];
                            string strTimeOut = cols[2];

                            // Empty columns
                            if (id == string.Empty)
                            {
                                errRowsDict.Add(rowNum, $"Empty column for Employee ID: {id}, {strTimeIn}, {strTimeOut}");
                                continue;
                            }
                            else if (strTimeIn == string.Empty)
                            {
                                errRowsDict.Add(rowNum, $"Empty column for Time-in: {id}, {strTimeIn}, {strTimeOut}");
                                continue;
                            }
                            else if (strTimeOut == string.Empty)
                            {
                                errRowsDict.Add(rowNum, $"Empty column for Time-out: {id}, {strTimeIn}, {strTimeOut}");
                                continue;
                            }


                            // If all cols can be parsed by its data type, upload data
                            // Note: this assumes no time-ins/time-outs are invalid 
                            if
                            (
                                int.TryParse(id, out int employeeID) &&
                                DateTime.TryParse(strTimeIn, out DateTime timeIn) &&
                                DateTime.TryParse(strTimeOut, out DateTime timeOut)
                            )
                            {
                                bool isLate = false;

                                // Check for late
                                isLate = CheckForLate(timeIn, employeeID);

                                DB.uspUploadAttendanceData(employeeID, timeIn, timeOut, isLate);

                                // Add to Employees in attendance
                                employeesInAttendance.Add(employeeID);

                                // Log row number
                                rowNum++;
                            }
                            else
                            {
                                errRowsDict.Add(rowNum, $"Could not parse columns as values: {id}, {strTimeIn}, {strTimeOut}");
                                continue;
                            }
                        }

                        // Check for absences
                        RecordAbsences(employeesInAttendance);

                        MessageBox.Show("Attendance data successfully stored in the database.");

                        if (errRowsDict.Count == 0)
                        {
                            tblkLogs.Text = "No erroneous rows were encountered while pulling the data.";
                        }
                        else
                        {
                            string message = $"{errRowsDict.Count} error/s were found while pulling the data.\n";

                            foreach (KeyValuePair<int, string> error in errRowsDict)
                            {
                                message += $"Row #{error.Key}: {error.Value}\n";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Let the user know what went wrong.
                    MessageBox.Show("The attendance file could not be read");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RecordAbsences(List<int> employeesInAttendance)
        {
            foreach (tblEmployee employee in DB.tblEmployees)
            {
                if (!employeesInAttendance.Contains(employee.EmployeeID)) 
                {
                    DB.uspRecordAbsences(employee.EmployeeID);
                }
            }
        }

        private bool CheckForLate(DateTime timeIn, int employeeID)
        {

            // Check for late
            if ((timeIn.Hour == 7 && timeIn.Minute > 0) || timeIn.Hour > 7)
            {
                foreach (tblEmployeeDetail employee in DB.tblEmployeeDetails)
                {
                    // Check AM part-timers
                    if (employee.EmployeeID == employeeID && employee.ScheduleTypeID == 2)
                    {
                        return true;
                    }

                    // Check full-time employees
                    if (employee.EmployeeID == employeeID && employee.ScheduleTypeID == 5)
                    {
                        return true;
                    }
                }
            }
            // Check PM Part-timers
            else if ((timeIn.Hour == 12 && timeIn.Minute > 0) || timeIn.Hour > 12)
            {
                foreach (tblEmployeeDetail employee in DB.tblEmployeeDetails)
                { 
                    if (employee.EmployeeID == employeeID && employee.ScheduleTypeID == 4)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
