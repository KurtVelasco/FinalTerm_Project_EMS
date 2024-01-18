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

            LoadLeaveRequestsView();
            LoadFilters();
        }

        public class LeaveRequest
        {
            public string LeaveRequestID { get; set; }
            public string DateFiled { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string EmployeeID { get; set; }
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
                EmployeeID = "EMP001",
                LeaveType = "Vacation",
                Status = "Pending"
            };

            LeaveRequest request2 = new LeaveRequest
            {
                LeaveRequestID = "2",
                DateFiled = "2024-01-18",
                StartDate = "2024-03-10",
                EndDate = "2024-03-15",
                EmployeeID = "EMP002",
                LeaveType = "Sick Leave",
                Status = "Approved"
            };


            lvLeaveRequests.Items.Add(request1);
            lvLeaveRequests.Items.Add(request2);
        }
        private void LoadLeaveRequestsView()
        {
            lvLeaveRequests.Items.Clear();
            lvLeaveRequests.ItemsSource = null;
            // Date filed, Employee ID, Start Date, End Date, Type of Leave, Status (Pending, Approved, Denied)

            string leaveRequestID, dateFiled, startDate, endDate, employeeID, leaveType = "Unpaid", status = "Pending";

            foreach (tblLeaveRequest leaveRequest in DB.tblLeaveRequests)
            {
                leaveRequestID = leaveRequest.LeaveRequestID.ToString();
                dateFiled = leaveRequest.DateFiled.ToString();
                startDate = leaveRequest.StartDate.ToString();
                endDate = leaveRequest.EndDate.ToString();
                employeeID = leaveRequest.EmployeeID.ToString();

                if (leaveRequest.isVacation != null)
                {
                    switch (leaveRequest.isVacation)
                    {
                        case true:
                            leaveType = "Vacation";
                            break;
                        case false:
                            leaveType = "Sick Leave";
                            break;
                    }
                }


                if (leaveRequest.IsApproved != null)
                {
                    switch (leaveRequest.IsApproved)
                    {
                        case true:
                            status = "Approved";
                            break;
                        case false:
                            status = "Denied";
                            break;
                    }
                }


                lvLeaveRequests.Items.Add(new LeaveRequest 
                { 
                    LeaveRequestID = leaveRequestID,
                    DateFiled = dateFiled,
                    StartDate = startDate,
                    EndDate = endDate,
                    EmployeeID = employeeID,
                    LeaveType = leaveType,
                    Status = status,
                    isApproved = leaveRequest.IsApproved,
                }
                );
            }

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

            foreach (tblLeaveRequest leaveRequest in DB.tblLeaveRequests)
            {
                if (leaveRequest.LeaveRequestID == int.Parse(((LeaveRequest)lvLeaveRequests.SelectedItem).LeaveRequestID))
                {
                    new ManageEmployeeRequest(leaveRequest).ShowDialog();
                }
            }

            LoadLeaveRequestsView();
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            LoadLeaveRequestsView();
        }

        private void btnViewAscending_Click(object sender, RoutedEventArgs e)
        {
            List<LeaveRequest> ascending = new List<LeaveRequest>();

            foreach (LeaveRequest leaveRequest in lvLeaveRequests.Items)
            {
                ascending.Add(leaveRequest);
            }

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
            List<LeaveRequest> descending = new List<LeaveRequest>();

            foreach (LeaveRequest leaveRequest in lvLeaveRequests.Items)
            {
                descending.Add(leaveRequest);
            }

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
            //if (cbxFilters.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please select a search-by filter.");
            //    return;
            //}

            List<LeaveRequest> results = new List<LeaveRequest>();
            if (cbxFilters.SelectedIndex != -1)
            {
                if (cbxFilters.SelectedItem.ToString() == "EmployeeID")
                {
                    if (int.TryParse(tbxSearchBar.Text, out int employeeID))
                    {
                        foreach (tblLeaveRequest leaveRequest in DB.tblLeaveRequests)
                        {
                            if (leaveRequest.EmployeeID == employeeID)
                            {
                                string leaveType = "Unpaid";
                                string status = "Pending";

                                if (leaveRequest.isVacation != null)
                                {
                                    switch (leaveRequest.isVacation)
                                    {
                                        case true:
                                            leaveType = "Vacation";
                                            break;
                                        case false:
                                            leaveType = "Sick Leave";
                                            break;
                                    }
                                }

                                if (leaveRequest.IsApproved != null)
                                {
                                    switch (leaveRequest.IsApproved)
                                    {
                                        case true:
                                            status = "Approved";
                                            break;
                                        case false:
                                            status = "Denied";
                                            break;
                                    }
                                }
                                if ((bool)chkbxPending.IsChecked && leaveRequest.IsApproved == null)
                                {
                                    results.Add(new LeaveRequest
                                    {
                                        LeaveRequestID = leaveRequest.LeaveRequestID.ToString(),
                                        DateFiled = leaveRequest.DateFiled.ToString(),
                                        StartDate = leaveRequest.StartDate.ToString(),
                                        EndDate = leaveRequest.EndDate.ToString(),
                                        EmployeeID = leaveRequest.EmployeeID.ToString(),
                                        LeaveType = leaveType,
                                        Status = status
                                    });
                                }
                                else if ((bool)chkbxPending.IsChecked && leaveRequest.IsApproved != null)
                                {
                                    results.Add(new LeaveRequest
                                    {
                                        LeaveRequestID = leaveRequest.LeaveRequestID.ToString(),
                                        DateFiled = leaveRequest.DateFiled.ToString(),
                                        StartDate = leaveRequest.StartDate.ToString(),
                                        EndDate = leaveRequest.EndDate.ToString(),
                                        EmployeeID = leaveRequest.EmployeeID.ToString(),
                                        LeaveType = leaveType,
                                        Status = status
                                    });
                                }
                            }
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
                            foreach (tblLeaveRequest leaveRequest in DB.tblLeaveRequests)
                            {
                                if (employee.EmployeeID == leaveRequest.EmployeeID)
                                {
                                    string leaveType = "Unpaid";
                                    string status = "Pending";

                                    if (leaveRequest.isVacation != null)
                                    {
                                        switch (leaveRequest.isVacation)
                                        {
                                            case true:
                                                leaveType = "Vacation";
                                                break;
                                            case false:
                                                leaveType = "Sick Leave";
                                                break;
                                        }
                                    }

                                    if (leaveRequest.IsApproved != null)
                                    {
                                        switch (leaveRequest.IsApproved)
                                        {
                                            case true:
                                                status = "Approved";
                                                break;
                                            case false:
                                                status = "Denied";
                                                break;
                                        }
                                    }

                                    results.Add(new LeaveRequest
                                    {
                                        LeaveRequestID = leaveRequest.LeaveRequestID.ToString(),
                                        DateFiled = leaveRequest.DateFiled.ToString(),
                                        StartDate = leaveRequest.StartDate.ToString(),
                                        EndDate = leaveRequest.EndDate.ToString(),
                                        EmployeeID = leaveRequest.EmployeeID.ToString(),
                                        LeaveType = leaveType,
                                        Status = status
                                    });
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                foreach (tblLeaveRequest leaveRequest in DB.tblLeaveRequests)
                {
                    string leaveType = "Unpaid";
                    string status = "Pending";

                    if (leaveRequest.isVacation != null)
                    {
                        switch (leaveRequest.isVacation)
                        {
                            case true:
                                leaveType = "Vacation";
                                break;
                            case false:
                                leaveType = "Sick Leave";
                                break;
                        }
                    }

                    if (leaveRequest.IsApproved != null)
                    {
                        switch (leaveRequest.IsApproved)
                        {
                            case true:
                                status = "Approved";
                                break;
                            case false:
                                status = "Denied";
                                break;
                        }
                    }
                    results.Add(new LeaveRequest
                    {
                        LeaveRequestID = leaveRequest.LeaveRequestID.ToString(),
                        DateFiled = leaveRequest.DateFiled.ToString(),
                        StartDate = leaveRequest.StartDate.ToString(),
                        EndDate = leaveRequest.EndDate.ToString(),
                        EmployeeID = leaveRequest.EmployeeID.ToString(),
                        LeaveType = leaveType,
                        Status = status,
                        isApproved = leaveRequest.IsApproved,
                    });
                }
            }

            DisplaySearchResults(results);
        }

        private void DisplaySearchResults(List<LeaveRequest> results)
        {
            lvLeaveRequests.Items.Clear();
            lvLeaveRequests.ItemsSource = null;

            foreach (LeaveRequest leaveRequest in results)
            {
                if (chkbxPending.IsChecked == true && leaveRequest.isApproved == null)
                    lvLeaveRequests.Items.Add(leaveRequest);
                else if (!chkbxPending.IsChecked == false && leaveRequest.isApproved != null)
                    lvLeaveRequests.Items.Add(leaveRequest);
            }
        }
    }
}
