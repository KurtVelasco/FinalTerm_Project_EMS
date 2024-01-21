using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public SmtpClient Gmail { get; set; }
        public tblLeaveRequest LeaveRequest { get; set; }
        public string Remarks { get; set; }
        
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public ManageEmployeeRequest(tblLeaveRequest leaveRequest)
        {
            InitializeComponent();
            this.LeaveRequest = leaveRequest;

            if (leaveRequest.IsApproved != null)
            {
                btnApprove.IsEnabled = false;
                btnDeny.IsEnabled = false;
            }
            else 
            {
                Gmail = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("northville.internals@gmail.com", "mlqe tbyo zljl ervy"),
                    EnableSsl = true,
                };
            }
            
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
            foreach (tblLeaveType leaveType1 in DB.tblLeaveTypes)
            {
                if (LeaveRequest.LeaveTypeID == leaveType1.LeaveTypeID) {
                    tbxLeaveType.Text = leaveType1.LeaveType;
                }
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
            DB.uspRespondToLeaveRequest(LeaveRequest.EmployeeID, LeaveRequest.LeaveRequestID, LeaveRequest.LeaveTypeID, true, tbxRemarks.Text);
            MessageBox.Show("Leave Request has been approved. Window will now close.");

            tblEmployee reviewingEmployee = null;
            tblEmployee requestingEmployee = null;

            foreach (tblEmployee employee in DB.tblEmployees)
            {
                if (employee.EmployeeID == LogInCredentials.EMPLOYEE_ID)
                {
                    reviewingEmployee = employee;
                }
                else if (employee.EmployeeID == LeaveRequest.EmployeeID)
                {
                    requestingEmployee = employee;
                }
            }

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("northville.internals@gmail.com"),
                Subject = "Re: Your Recent Leave Request",
                Body = $"Good News! Your leave request (#{LeaveRequest.LeaveRequestID}) has been approved by {reviewingEmployee.FirstName} {reviewingEmployee.LastName}. {(LeaveRequest.ApprovalRemarks.Length > 0 ? $"Additional remarks about its approval may be found below:\n {LeaveRequest.ApprovalRemarks}" : $"No additional remarks found. Enjoy your leave! For inquiries, you may contact {reviewingEmployee.EmailAddress} or {reviewingEmployee.PhoneNumber}.")}",
            };
            mailMessage.To.Add(requestingEmployee.EmailAddress);
            Gmail.Send(mailMessage);

            InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "User has approved a leave Request with the ID:"   + LeaveRequest.LeaveRequestID, 6);
            this.Close();
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            DB.uspRespondToLeaveRequest(LeaveRequest.EmployeeID, LeaveRequest.LeaveRequestID, LeaveRequest.LeaveTypeID, false, tbxRemarks.Text);
            MessageBox.Show("Leave Request has been denied. Window will now close.");


            tblEmployee reviewingEmployee = null;
            tblEmployee requestingEmployee = null;

            foreach (tblEmployee employee in DB.tblEmployees)
            {
                if (employee.EmployeeID == LogInCredentials.EMPLOYEE_ID)
                {
                    reviewingEmployee = employee;
                }
                else if (employee.EmployeeID == LeaveRequest.EmployeeID)
                {
                    requestingEmployee = employee;
                }
            }

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("northville.internals@gmail.com"),
                Subject = "Re: Your Recent Leave Request",
                Body = $"Unfortunately, your leave request (#{LeaveRequest.LeaveRequestID}) has been denied by {reviewingEmployee.FirstName} {reviewingEmployee.LastName}. {(LeaveRequest.ApprovalRemarks.Length > 0 ? $"Additional remarks about its denial may be found below:\n {LeaveRequest.ApprovalRemarks}" : $"No additional remarks found. You may contact the reviewer at {reviewingEmployee.EmailAddress} or {reviewingEmployee.PhoneNumber}.")}",
            };
            mailMessage.To.Add(requestingEmployee.EmailAddress);
            Gmail.Send(mailMessage);

            InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "User has denied a leave Request with the ID:" + LeaveRequest.LeaveRequestID, 6);
            this.Close();
        }
    }
}
