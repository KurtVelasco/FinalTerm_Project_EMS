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

        //Connect to the DB using this ConnectionString
        // This is where all your USP will becalled as methods
       
        EmployeeDatabaseDataContext db = new EmployeeDatabaseDataContext(Properties.Settings.Default.MockEMSDatabaseConnectionString);
        public AddingEmployee()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string firstName = tbx_FName.Text;
                string middlename = tbx_MName.Text;
                string lastName = tbx_LName.Text;
                DateTime? birthday = DatePicker_Birthday.SelectedDate;
                string phoneNumber = tbx_PNumber.Text;
                string emailAddress = tbx_EAddress.Text;
                string homeAddress = tbx_HAddress.Text;

                //think db of a class with methods
                // the methods are the usp you drag in the dbml

                //In this case the USP INSERT EMPLOYEES Takes multiple parameters
                //you can hover on the methods to show what you need in order
                db.USP_INSERT_EMPLOYEE_DETAILS(firstName, middlename, lastName, birthday, emailAddress, homeAddress, phoneNumber);


                // Show added details in a message box
                MessageBox.Show($"Employee added:\nName: {firstName} {lastName}\nBirth Date:\nPhone Number: {phoneNumber}\nEmail Address: {emailAddress}\nHome Address: {homeAddress}", "Employee Added");

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
            tbx_PNumber.Clear();
            tbx_EAddress.Clear();
            tbx_HAddress.Clear();
        }
    }
}

