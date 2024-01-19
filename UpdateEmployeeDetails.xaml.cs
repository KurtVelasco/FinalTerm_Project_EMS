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
    /// Interaction logic for UpdateEmployeeDetails.xaml
    /// </summary>
    public partial class UpdateEmployeeDetails : Window
    {
        EmployeeDatabaseDataContext db = new EmployeeDatabaseDataContext(Properties.Settings.Default.MockEMSDatabaseConnectionString);

        public Dictionary<int, string> departmentDict = new Dictionary<int, string>();
        public Dictionary<int, string> positionDict = new Dictionary<int, string>();
        public Dictionary<int, string> statusDict = new Dictionary<int, string>();
        public Dictionary<int, string> schedDict = new Dictionary<int, string>();

        private int EMPLOYEE_ID = 0;

        public UpdateEmployeeDetails()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            Combobox_Department.ItemsSource = null;
            Combobox_Position.ItemsSource = null;
            Combobox_Status.ItemsSource = null;
            Combobox_ScheduleType.ItemsSource = null;
          

            foreach (tblDepartment dps in db.tblDepartments)
            {
                departmentDict[dps.DepartmentID] = dps.DepartmentName;
            }
            foreach (tblPosition tp in db.tblPositions)
            {
                positionDict[tp.PositionID] = tp.PositionName;
            }
            foreach (tblStatuse ts in db.tblStatuses)
            {
                statusDict[ts.StatusID] = ts.StatusName;
            }
            foreach (tblScheduleType ts in db.tblScheduleTypes)
            {
                schedDict[ts.ScheduleTypeID] = ts.ScheduleType;
            }
            Combobox_Position.ItemsSource = positionDict;
            Combobox_Department.ItemsSource = departmentDict;
            Combobox_Status.ItemsSource = statusDict;
            Combobox_ScheduleType.ItemsSource = schedDict;

        }

        private void Button_GetEmployee_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeeDetais();         
        }
        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            foreach (var control in mainGrid.Children)
            {
                if (control is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // TextBox is empty
                        MessageBox.Show("Please input all fields", "Missing field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }

                else if (control is ComboBox comboBox)
                {
                    if (comboBox.SelectedIndex == -1)
                    {
                        // ComboBox is not selected
                        MessageBox.Show("Please input all fields", "Missing field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
                else if (control is DatePicker datePicker)
                {
                    if (!datePicker.SelectedDate.HasValue)
                    {
                        // DatePicker does not have a selected date
                        MessageBox.Show("Please input all fields", "Missing field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
            }
            string firstName = Textbox_FirstName.Text;  
            string middleName = Textbox_MiddleName.Text;
            string lastName = Textbox_LastName.Text;
            string email = Textbox_Email.Text;  
            string address = Textbox_HomeAddress.Text;  
            string contact = Textbox_Contact.Text;
            DateTime? birthday = DatePicker_Birthday.SelectedDate;
            db.USP_UPDATE_EMPLOYEE_PERSONAL(EMPLOYEE_ID, firstName,middleName,lastName, birthday, contact, email, address);
            MessageBox.Show("Employee Personal Information has been added", "Update Information", MessageBoxButton.OK, MessageBoxImage.Information);
            InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "Admin Updated an Employee's Personal Information");

            GetEmployeeDetais();
        }
        private void Button_UpdateEmployment_Click(object sender, RoutedEventArgs e)
        {
            foreach (var control in mainGrid.Children)
            {
                if (control is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // TextBox is empty
                        MessageBox.Show("Please input all fields", "Missing field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }

                else if (control is ComboBox comboBox)
                {
                    if (comboBox.SelectedIndex == -1)
                    {
                        // ComboBox is not selected
                        MessageBox.Show("Please input all fields", "Missing field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
                else if (control is DatePicker datePicker)
                {
                    if (!datePicker.SelectedDate.HasValue)
                    {
                        // DatePicker does not have a selected date
                        MessageBox.Show("Please input all fields", "Missing field", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
            }
            bool isEmailValid = true;
            if (!isEmailValid)
            {
                MessageBox.Show("Please enter a valid email", "Invalid Email", MessageBoxButton.YesNo, MessageBoxImage.Error);
                return;
            }

            int departmentID = ((KeyValuePair<int, string>)Combobox_Department.SelectedItem).Key;
            int positionID = ((KeyValuePair<int, string>)Combobox_Department.SelectedItem).Key;
            int statusID = ((KeyValuePair<int, string>)Combobox_Status.SelectedItem).Key;
            int schedTypeID = ((KeyValuePair<int, string>)Combobox_ScheduleType.SelectedItem).Key;
            DateTime? employedON = DatePicker_EmployedOn.SelectedDate;
            string password = Textbox_Password.Text;
            db.USP_UPDATE_EMPLOYEE_EMPLOYMENT(EMPLOYEE_ID, departmentID, positionID, schedTypeID, statusID, employedON, password);
            MessageBox.Show("Employee Employment Information has been Updated", "Update Information", MessageBoxButton.OK, MessageBoxImage.Information);
            InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "Admin Updated an Employee's Employment Information");
            GetEmployeeDetais();
        }

        private void Button_ReturnMenu_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagment_Admin am = new EmployeeManagment_Admin();
            am.Show();
            this.Close();
        }
        private void GetEmployeeDetais()
        {
            string userInput = Textbox_GetEmail.Text;
            List<USP_SEARCH_EMPLOYEE_BY_EMAILResult> employeeResult = db.USP_SEARCH_EMPLOYEE_BY_EMAIL(userInput).ToList();
            if (employeeResult.Count == 1)
            {
                USP_SEARCH_EMPLOYEE_BY_EMAILResult employee = employeeResult[0];
                Combobox_Department.SelectedItem = departmentDict.FirstOrDefault(x => x.Key == employee.DepartmentID);
                Combobox_Position.SelectedItem = positionDict.FirstOrDefault(x => x.Key == employee.PositionID);
                Combobox_Status.SelectedItem = statusDict.FirstOrDefault(x => x.Key == employee.StatusID);
                Combobox_ScheduleType.SelectedItem = schedDict.FirstOrDefault(x => x.Key == employee.ScheduleTypeID);
                Textbox_LastName.Text = employee.LastName;
                Textbox_MiddleName.Text = employee.MiddleName;
                Textbox_FirstName.Text = employee.FirstName;
                Textbox_Email.Text = employee.EmailAddress;
                Textbox_HomeAddress.Text = employee.HomeAddress;
                Textbox_Contact.Text = employee.PhoneNumber;
                Textbox_Password.Text = employee.Password;  
                DatePicker_Birthday.SelectedDate = employee.Birthday;
                DatePicker_EmployedOn.SelectedDate = employee.EmployedOn;
                EMPLOYEE_ID = employee.EmployeeID;

                Textbox_Password.IsEnabled = true;
                Textbox_MiddleName.IsEnabled = true;
                Textbox_LastName.IsEnabled = true;
                Textbox_Email.IsEnabled = true;
                Textbox_FirstName.IsEnabled = true;
                Textbox_HomeAddress.IsEnabled = true;
                Textbox_Contact.IsEnabled = true;
                DatePicker_Birthday.IsEnabled = true;
                DatePicker_EmployedOn.IsEnabled = true;
                Button_Update.IsEnabled = true;
                Button_UpdateEmployment.IsEnabled = true;   

            }
            else
            {
                MessageBox.Show("No Employee Found with that Email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
      
    }
}
