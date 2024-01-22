using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using static FinalTerm_Project_EMS.ManageLeaveRequests;
using static FinalTerm_Project_EMS.SearchEmployee;

namespace FinalTerm_Project_EMS
{
    /// <summary>
    /// Interaction logic for ManageLeaveRequests.xaml
    /// </summary>
    public partial class ManageLeaveRequests : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public ManageLeaveRequests()
        {
            InitializeComponent();

            LoadFilters();
        }

        public class LeaveRequest
        {
            public string LeaveRequestID { get; set; }
            public string DateFiled { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public int EmployeeID { get; set; }
            public string LeaveType { get; set; }
            public string Status { get; set; }
            public bool? isApproved { get; set; }
        }
        private void LoadLeaveRequestsSampleView()
        {
            LeaveRequest request1 = new LeaveRequest
            {
                LeaveRequestID = "1",
                DateFiled = "2024-01-17",
                StartDate = "2024-02-01",
                EndDate = "2024-02-05",
                EmployeeID = 1,
                LeaveType = "Vacation",
                Status = "Pending"
            };

            LeaveRequest request2 = new LeaveRequest
            {
                LeaveRequestID = "2",
                DateFiled = "2024-01-18",
                StartDate = "2024-03-10",
                EndDate = "2024-03-15",
                EmployeeID = 1,
                LeaveType = "Sick Leave",
                Status = "Approved"
            };


            lvLeaveRequests.Items.Add(request1);
            lvLeaveRequests.Items.Add(request2);
        }

        private void ReloadLeaveRequestsDisplay(List<LeaveRequest> leaveRequests)
        {
            foreach (LeaveRequest leaveRequest in leaveRequests)
            {
                lvLeaveRequests.Items.Clear();
                lvLeaveRequests.ItemsSource = null;
                lvLeaveRequests.Items.Add(leaveRequest);
            }
        }

        private List<LeaveRequest> GetLeaveRequests()
        {
            List<LeaveRequest> leaveRequests = new List<LeaveRequest>();

            string leaveRequestID, dateFiled, startDate, endDate;
            int employeeID;

            foreach (tblLeaveRequest leaveRequest in DB.tblLeaveRequests)
            {
                string status = leaveRequest.IsApproved == null ? "Pending" : (bool)leaveRequest.IsApproved ? "Approved" : "Denied";
                string leaveType = "";
                leaveRequestID = leaveRequest.LeaveRequestID.ToString();
                dateFiled = leaveRequest.DateFiled.ToString();
                startDate = leaveRequest.StartDate.ToString();
                endDate = leaveRequest.EndDate.ToString();
                employeeID = leaveRequest.EmployeeID;

                foreach (tblLeaveType leaveType1 in DB.tblLeaveTypes)
                {
                    if (leaveRequest.LeaveTypeID == leaveType1.LeaveTypeID)
                    {
                        leaveType = leaveType1.LeaveType;
                    }
                }

                LeaveRequest lr = new LeaveRequest
                {
                    LeaveRequestID = leaveRequestID,
                    DateFiled = dateFiled,
                    StartDate = startDate,
                    EndDate = endDate,
                    EmployeeID = employeeID,
                    LeaveType = leaveType,
                    Status = status,
                    isApproved = leaveRequest.IsApproved,
                };

                leaveRequests.Add(lr);
            }

            return leaveRequests;
        }

        private void LoadFilters()
        {
            cbxFilters.Items.Add("Email");
            cbxFilters.Items.Add("EmployeeID");
        }

        private void tbxSearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxSearchBar.Text = string.Empty;
        }

        private void tbxSearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxSearchBar.Text == "")
                tbxSearchBar.Text = "Search by EmployeeID or Email...";
        }

        private void lvLeaveRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvLeaveRequests.SelectedIndex == -1)
                return;

            // Open Employee Leave Request Manager
            foreach (tblLeaveRequest leaveRequest in DB.tblLeaveRequests)
            {
                if (leaveRequest.LeaveRequestID == int.Parse(((LeaveRequest)lvLeaveRequests.SelectedItem).LeaveRequestID))
                {
                    new ManageEmployeeRequest(leaveRequest).ShowDialog();
                }
            }

            // Reload Leave Request View
            ReloadLeaveRequestsDisplay(GetLeaveRequests());
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            ReloadLeaveRequestsDisplay(GetLeaveRequests());
        }

        private void btnViewAscending_Click(object sender, RoutedEventArgs e)
        {
            List<LeaveRequest> ascending = GetLeaveRequests();


            ascending.Sort((x, y) => x.DateFiled.CompareTo(y.DateFiled));

            lvLeaveRequests.Items.Clear();
            lvLeaveRequests.ItemsSource = null;

            foreach (LeaveRequest leaveRequest in ascending)
            {
                lvLeaveRequests.Items.Add(leaveRequest);
            }
        }

        private void btnViewDescending_Click(object sender, RoutedEventArgs e)
        {
            List<LeaveRequest> descending = GetLeaveRequests();

            descending.Sort((x, y) => y.DateFiled.CompareTo(x.DateFiled));

            lvLeaveRequests.Items.Clear();
            lvLeaveRequests.ItemsSource = null;

            foreach (LeaveRequest leaveRequest in descending)
            {
                lvLeaveRequests.Items.Add(leaveRequest);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<LeaveRequest> results = new List<LeaveRequest>();
            if (cbxFilters.SelectedIndex != -1)
            {
                if (cbxFilters.SelectedItem.ToString() == "EmployeeID")
                {
                    if (int.TryParse(tbxSearchBar.Text, out int employeeID))
                    {
                        List<LeaveRequest> leaveRequests = GetLeaveRequests();

                        if ((bool)chkbxPending.IsChecked)
                        {
                            results = leaveRequests
                                                   .FindAll((lr) => lr.EmployeeID == employeeID)
                                                   .FindAll((lr) => lr.isApproved == null);
                        }
                        else
                        {
                            results = leaveRequests
                                                   .FindAll((lr) => lr.EmployeeID == employeeID)
                                                   .FindAll((lr) => lr.isApproved != null);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid integer value for the EmployeeID.");
                        return;
                    }

                }
                else if (cbxFilters.SelectedItem.ToString() == "Email")
                {
                    if (!tbxSearchBar.Text.Contains('@') || !tbxSearchBar.Text.Contains("."))
                    {
                        MessageBox.Show("Please enter a valid email address.");
                        return;
                    }

                    foreach (tblEmployee employee in DB.tblEmployees)
                    {
                        if (employee.EmailAddress.ToUpper().Contains(tbxSearchBar.Text.ToUpper()))
                        {
                            List<LeaveRequest> leaveRequests = GetLeaveRequests();

                            if ((bool)chkbxPending.IsChecked)
                            {
                                results = leaveRequests
                                                       .FindAll((lr) => lr.EmployeeID == employee.EmployeeID)
                                                       .FindAll((lr) => lr.isApproved == null);
                            }
                            else
                            {
                                results = leaveRequests
                                                       .FindAll((lr) => lr.EmployeeID == employee.EmployeeID)
                                                       .FindAll((lr) => lr.isApproved != null);
                            }
                        }
                    }
                }

            }
            else
            {
                List<LeaveRequest> leaveRequests = GetLeaveRequests();

                if ((bool)chkbxPending.IsChecked)
                {
                    results = leaveRequests.FindAll((lr) => lr.isApproved == null);
                }
                else
                {
                    results = leaveRequests.FindAll((lr) => lr.isApproved != null);
                }
            }

            ReloadLeaveRequestsDisplay(results);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AttendanceManagement_Admin ama = new AttendanceManagement_Admin();
            ama.Show();
            this.Close();
        }
    }
}
