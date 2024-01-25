using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PullAttendance.xaml
    /// </summary>
    public partial class PullAttendance : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public PullAttendance()
        {
            InitializeComponent();
        }

        private bool ValidateEmployee(int employeeID)
        {
            // Inactive employee 
            foreach (tblEmployeeDetail employeeDetail in DB.tblEmployeeDetails)
            {
                if (employeeDetail.EmployeeID == employeeID)
                {
                    if (employeeDetail.StatusID != 1)
                    {
                        MessageBox.Show("Employee is either inactive or tagged as AWOL");
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnPullData_Click(object sender, RoutedEventArgs e)
        {
            // Check priveleges
            if (LogInCredentials.EMPLOYEE_POSITION.ToUpper() == "ASSOCIATE")
            {
                MessageBox.Show("You do not have the permissions necessary to perform this action.");
                return;
            }    
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
                            if (cols.Length < 3)
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
                                // Check for inactive employee
                                if (!ValidateEmployee(employeeID))
                                    continue;

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

                                DB.USP_INSERT_LOGS(LogInCredentials.EMPLOYEE_ID, $"Row #{error.Key}: {error.Value}", 8);
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
                // If employee not in attendance
                if (!employeesInAttendance.Contains(employee.EmployeeID))
                {
                    foreach (tblEmployeeDetail employeeDetail in DB.tblEmployeeDetails)
                    {
                        // If employee is active
                        if (employeeDetail.EmployeeID == employee.EmployeeID && employeeDetail.StatusID == 1)
                        {
                            DB.uspRecordAbsences(employee.EmployeeID);
                        }
                    }
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AttendanceManagement_Admin ama = new AttendanceManagement_Admin();
            ama.Show();
            this.Close();
        }
    }
}
