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
    /// Interaction logic for ManageEmployeeRequest.xaml
    /// </summary>
    public partial class ManageEmployeeRequest : Window
    {
        public tblLeaveRequest LeaveRequest { get; set; }
        public string Remarks { get; set; }
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public ManageEmployeeRequest(tblLeaveRequest leaveRequest)
        {
            InitializeComponent();

            this.LeaveRequest = leaveRequest;
            LoadData();
        }

        public void LoadData()
        {
            foreach (tblEmployee employee in DB.tblEmployees)
            {
                if (employee.EmployeeID == LeaveRequest.EmployeeID)
                {
                    tbxFiledBy.Text = $"(#{employee.EmployeeID}) {employee.FirstName} {employee.LastName}";
                }
            }
            if (LeaveRequest.isVacation != null)
            {
                switch (LeaveRequest.isVacation)
                {
                    case true:
                        tbxLeaveType.Text = "Vacation";
                        break;
                    case false:
                        tbxLeaveType.Text = "Sick Leave";
                      break;
                }
            }
            else
            {
                tbxLeaveType.Text = "Unpaid";
            }

            tbxDateFiled.Text = LeaveRequest.DateFiled.ToString();
            tbxStartDate.Text = LeaveRequest.StartDate.ToString();
            tbxEndDate.Text = LeaveRequest.EndDate.ToString();
            tbxDestinationAddress.Text = LeaveRequest.DestinationAddress;
            tbxRequestNotes.Text = LeaveRequest.RequestNotes;

            foreach (tblLeaveEntitlement entitlement in DB.tblLeaveEntitlements)
            {
                int leaveTypeID = 2;

                foreach (tblLeaveType leaveType in DB.tblLeaveTypes)
                {
                    if (leaveType.LeaveType.ToUpper() == tbxLeaveType.Text.ToUpper())
                    {
                        leaveTypeID = leaveType.LeaveTypeID;
                    }
                }

                if (entitlement.EmployeeID == LeaveRequest.EmployeeID && entitlement.LeaveTypeID == leaveTypeID)
                {
                    tbxEntitlements.Text = entitlement.Entitlements.ToString();
                }
            }

            // Disable
            tbxFiledBy.IsEnabled = false;
            tbxLeaveType.IsEnabled = false;
            tbxStartDate.IsEnabled = false;
            tbxEntitlements.IsEnabled = false;
            tbxDateFiled.IsEnabled = false;
            tbxEndDate.IsEnabled = false;
            tbxDestinationAddress.IsEnabled = false;
            tbxRequestNotes.IsEnabled = false;
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            DB.uspRespondToLeaveRequest(LeaveRequest.EmployeeID, LeaveRequest.LeaveRequestID, true, tbxRemarks.Text);
            MessageBox.Show("Leave Request has been approved. Window will now close.");
            InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "User has denied a leave Request with an ID:"   + LeaveRequest.EmployeeID, 6);
            this.Close();
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            DB.uspRespondToLeaveRequest(LeaveRequest.EmployeeID, LeaveRequest.LeaveRequestID, false, tbxRemarks.Text);
            MessageBox.Show("Leave Request has been denied. Window will now close.");
            InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "User has denied a leave Request with an ID:" + LeaveRequest.EmployeeID, 6);
            this.Close();
        }
    }
}
