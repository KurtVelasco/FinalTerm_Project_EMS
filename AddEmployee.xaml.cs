using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private string LastName;
        private string FirstName;
        private string Email;
        private string HomeAddress;
        private string Contact;
        private string GetEmail;
        private string Password;
        private string MiddleName;
        private int Department;
        private int Position;
        private int Status;
        private int ScheduleType;
    

        private DateTime? Birthday;
        private DateTime? EmployedOn;

        public AddEmployee()
        {
            InitializeComponent();
            FillComboBoxes();
        }
        EmployeeDatabaseDataContext db = new EmployeeDatabaseDataContext(Properties.Settings.Default.MockEMSDatabaseConnectionString);


        private void FillComboBoxes()
        {
            Dictionary<int, string> department = new Dictionary<int, string>();
            Dictionary<int, string> position = new Dictionary<int, string>();
            Dictionary<int, string> status = new Dictionary<int, string>();
            Dictionary<int, string> schedule = new Dictionary<int, string>();
            //Deleted because Keane said so
            //Dictionary<int, string> department = db.USP_SELECT_ALL_tblDepartments().ToDictionary(dep => dep.DepartmentID, dep => dep.DepartmentName);
            //Dictionary<int, string> position = db.USP_SELECT_ALL_tblPositions().ToDictionary(pos => pos.PositionID, pos => pos.PositionName);
            //Dictionary<int, string> status = db.USP_SELECT_ALL_tblStatus().ToDictionary(stat => stat.StatusID, stat => stat.StatusName);
            //Dictionary<int,string> schedule = db.USP_SELECT_ALL_tblSchedType().ToDictionary(sched => sched.ScheduleTypeID, sched => sched.ScheduleType);
            foreach (tblDepartment dps in db.tblDepartments)
            {
                department[dps.DepartmentID] = dps.DepartmentName;  
            }
            foreach(tblPosition tp in db.tblPositions)
            {
                position[tp.PositionID] = tp.PositionName;
            } 
            foreach(tblStatuse ts in db.tblStatuses)
            {
                status[ts.StatusID] = ts.StatusName;    
            }
            foreach(tblScheduleType ts in db.tblScheduleTypes)
            {
                schedule[ts.ScheduleTypeID] = ts.ScheduleType;
            }

            Combobox_Department.ItemsSource = department;
            Combobox_Position.ItemsSource = position;
            Combobox_ScheduleType.ItemsSource = schedule;

        }
        private void Button_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool isEmailValid = true;
            if (Textbox_MiddleName.Text.Length < 1)
            {
                Textbox_MiddleName.Text = "N/A";
            }
            foreach (var control in mainGrid.Children)
            {
                if (control is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
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
            if (Email.Length > 255 || !Email.Contains('@') || Email.Count(c => c == '.') < 1)
            {
                MessageBox.Show("Email is invalid, Please enter a valid email", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Contact.Length > 11 || !int.TryParse(Contact, out _))
            {
                MessageBox.Show("Contact Number is Invalid, Please type a valid Cellphone Number", "Invalid Cellphone Number", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult ms = MessageBox.Show("Add Employee to the Database?", "Add Employee?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(ms == MessageBoxResult.Yes) {
                AddEmployeeDatabase();
            }
            
        }
        private void AddEmployeeDatabase()
        {
            LastName = Textbox_LastName.Text;
            FirstName = Textbox_FirstName.Text;
            Email = Textbox_Email.Text;
            HomeAddress = Textbox_HomeAddress.Text;
            Contact = Textbox_Contact.Text;
            Password = Textbox_Password.Text;
            MiddleName = Textbox_MiddleName.Text;
            Department = ((KeyValuePair<int, string>)Combobox_Department.SelectedItem).Key;
            Position = ((KeyValuePair<int, string>)Combobox_Position.SelectedItem).Key;
            ScheduleType = ((KeyValuePair<int, string>)Combobox_ScheduleType.SelectedItem).Key;
            Birthday = DatePicker_Birthday.SelectedDate;
            EmployedOn = DatePicker_EmployedOn.SelectedDate;


            if (Email.Length > 255 || !Email.Contains('@') || Email.Count(c => c == '.') < 1)
            {
                MessageBox.Show("Email is invalid, Please enter a valid email", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Contact.Length > 11 || !int.TryParse(Contact, out _))
            {
                MessageBox.Show("Contact Number is Invalid, Please type a valid Cellphone Number", "Invalid Cellphone Number", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<USP_CHECK_DUPLICATE_EMAILSResult> checkEmployee = db.USP_CHECK_DUPLICATE_EMAILS(Textbox_Email.Text).ToList();
            if (checkEmployee.Count > 0)
            {
                MessageBox.Show("Email already exist in the database", "Duplicate Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                
                db.USP_INSERT_EMPLOYEE(FirstName, MiddleName, LastName, Birthday, Contact, Email, HomeAddress, Department, Position, 1, ScheduleType, Password, EmployedOn);
                MessageBox.Show("Added Employee with the Email: " + Email, "Added Employee" + LastName +", " + FirstName, MessageBoxButton.OK, MessageBoxImage.Information   );
                // InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "User added a new employee to the database: " + Email);
                AddEmployee am = new AddEmployee();
                am.Show();
                this.Close();
            }
        }

        private void Button_ReturnMenu_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagment_Admin am = new EmployeeManagment_Admin();
            am.Show();
            this.Close();   
        }


    }
}
