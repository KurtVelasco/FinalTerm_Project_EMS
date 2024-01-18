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
    /// Interaction logic for LeaveRequestForm.xaml
    /// </summary>
    public partial class LeaveRequestForm : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public LeaveRequestForm()
        {
            InitializeComponent();
            FillReasonsOptions();
        }

        private bool ValidateData()
        {
            // Employee ID
            if (tbxEmployeeID.Text.Length == 0)
            {
                MessageBox.Show("Please fill out the EmployeeID field.");
                return false;
            }
            else if (!int.TryParse(tbxEmployeeID.Text, out int employeeID))
            {
                MessageBox.Show("Please enter a valid integer for the EmployeeID field.");
                return false;
            }

            // Reason for leave
            if (cbxReason.SelectedIndex  == -1)
            {
                MessageBox.Show("Please select a reason for your leave.");
                return false;
            }

            // Start Date
            if (dpStartDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a start date for the leave you're currently filing.");
                return false;
            }

            // End Date
            if (dpEndDate.SelectedDate == null)
            {
                MessageBox.Show("Please select an end date for the leave you're currently filing.");
                return false;
            }

            // Destination Address
            if (tbxDestination.Text.Length == 0)
            {
                MessageBox.Show("Please fill out the destination address field.");
                return false;
            }

            return true;
        }

        private bool ValidateEmployee()
        {
            foreach (tblEmployeeDetail employee in DB.tblEmployeeDetails)
            {
                if (int.Parse(tbxEmployeeID.Text) == employee.EmployeeID && employee.ScheduleTypeID != 5)
                {
                    MessageBox.Show("Failed to file Leave Request. Only full-time employees are allowed to file leave requests.");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateLeaveEntitlements()
        {
            foreach (tblLeaveEntitlement leaveEntitlement in DB.tblLeaveEntitlements)
            {
                if (leaveEntitlement.EmployeeID == int.Parse(tbxEmployeeID.Text))
                {
                    // Vacation
                    if (cbxReason.SelectedValue.ToString().ToUpper() == "VACATION" && 
                        leaveEntitlement.LeaveTypeID == 1 && 
                        leaveEntitlement.Entitlements == 0)
                    {
                        MessageBoxResult dialogResult = MessageBox.Show("You have no more leave entitlements for vacations. This leave (if accepted) will be unpaid. Would you still like to proceed?", "WARNING", MessageBoxButton.YesNo);
                        if (dialogResult == MessageBoxResult.Yes)
                        {
                            cbxReason.SelectedIndex = 2;
                            return true;
                        }
                        else if (dialogResult == MessageBoxResult.No)
                        {
                            return false;
                        }
                    }
                    // Sick
                    else if (cbxReason.SelectedValue.ToString().ToUpper() == "SICK LEAVE" && 
                        leaveEntitlement.LeaveTypeID == 2 && 
                        leaveEntitlement.Entitlements == 0)
                    {
                        MessageBoxResult dialogResult = MessageBox.Show("You have no more leave entitlements for sick leaves. This leave (if accepted) will be unpaid. Would you still like to proceed?", "WARNING", MessageBoxButton.YesNo);
                        if (dialogResult == MessageBoxResult.Yes)
                        {
                            cbxReason.SelectedIndex = 2;
                            return true;
                        }
                        else if (dialogResult == MessageBoxResult.No)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void FillReasonsOptions()
        {
            foreach (tblLeaveType leaveType in DB.tblLeaveTypes)
            {
                cbxReason.Items.Add(leaveType.LeaveType);
            }

            cbxReason.SelectedIndex = 0;
        }

        private void btnFileRequest_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateData())
                return;

            if (!ValidateEmployee())
                return;

            if (!ValidateLeaveEntitlements())
                return;

            int employeeID = int.Parse(tbxEmployeeID.Text);
            bool? isVacation = cbxReason.SelectedValue.ToString().ToUpper() == "VACATION" ? true : false;
            string reqNotes = tbxRequestNotes.Text.Length == 0 ? null : tbxRequestNotes.Text;

            if (cbxReason.SelectedValue.ToString().ToUpper() == "UNPAID")
            {
                isVacation = null;
            }

            bool? success = false;

            DB.uspFileLeaveRequest(employeeID, cbxReason.SelectedIndex + 1, dpStartDate.SelectedDate, dpEndDate.SelectedDate, isVacation, tbxDestination.Text, reqNotes, ref success);
        
            if (!(bool)success)
            {
                MessageBox.Show("Failed to file a request for leave. Specified employee has a pending request or no employee with the specified ID is found.");
            }
            else
            {
                MessageBox.Show("Leave Request successfully filed.");
            }
        }
    }
}
