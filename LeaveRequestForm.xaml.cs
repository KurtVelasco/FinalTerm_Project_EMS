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

            // Duration
            if (tbxDuration.Text.Length == 0)
            {
                MessageBox.Show("Please fill out the Duration field.");
                return false;
            }
            else if (!int.TryParse(tbxDuration.Text, out int employeeID))
            {
                MessageBox.Show("Please enter a valid integer for the Duration field.");
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

            // Destination Address
            if (tbxDestination.Text.Length == 0)
            {
                MessageBox.Show("Please fill out the destination address field.");
                return false;
            }

            return true;
        }

        private void FillReasonsOptions()
        {
            cbxReason.Items.Add("Vacation");
            cbxReason.Items.Add("Sickness");

            cbxReason.SelectedIndex = 0;
        }

        private void btnFileRequest_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateData())
                return;

            int employeeID = int.Parse(tbxEmployeeID.Text);
            int duration = int.Parse(tbxDestination.Text);
            bool isVacation = cbxReason.SelectedValue.ToString().ToUpper() == "VACATION" ? true : false;
            string reqNotes = tbxRequestNotes.Text.Length == 0 ? null : tbxRequestNotes.Text;

            bool? success = false;

            DB.uspFileLeaveRequest(employeeID, dpStartDate.SelectedDate, duration, isVacation, tbxDestination.Text, reqNotes, ref success);
        
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
