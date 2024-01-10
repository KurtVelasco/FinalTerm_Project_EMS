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
    /// Interaction logic for AddingEmployee.xaml
    /// </summary>
    public partial class AddingEmployee : Window
    {
        EmployeeDatabaseDataContext Database = new EmployeeDatabaseDataContext();
        public AddingEmployee()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get employee details from TextBoxes
                //int employeeId = Convert.ToInt32(tbx_EID.Text);
                string firstName = tbx_FName.Text;
                string lastName = tbx_LName.Text;
                DateTime birthDate = Convert.ToDateTime(tbx_BDay.Text);
                string phoneNumber = tbx_PNumber.Text;
                string emailAddress = tbx_EAddress.Text;
                string homeAddress = tbx_HAddress.Text;

                // Show added details in a message box
                MessageBox.Show($"Employee added:\nName: {firstName} {lastName}\nBirth Date: {birthDate.ToShortDateString()}\nPhone Number: {phoneNumber}\nEmail Address: {emailAddress}\nHome Address: {homeAddress}", "Employee Added");

                // This will call the new method I did the ClearTextBoxes() method
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding employee: {ex.Message}", "Error");
            }
        }

        private void ClearTextBoxes()
        {
            //  This will clear the all texboxes if I click the Ok button 
            //tbx_EID.Clear();
            tbx_FName.Clear();
            tbx_LName.Clear();
            tbx_BDay.Clear();
            tbx_PNumber.Clear();
            tbx_EAddress.Clear();
            tbx_HAddress.Clear();
        }
    }
}
