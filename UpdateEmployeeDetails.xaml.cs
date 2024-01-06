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
        public string[] employeeStatusOptions = { "ACTIVE", "LONG ABSENCE", "TERMINATED", "RESIGNED", "DECEASED" };
        public string[] employeeScheduleType = { "TYPE1", "TYPE2", "TYPE3", }; //PlaceHolder

        public UpdateEmployeeDetails()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            List<USP_SELECT_ALL_tblDepartmentsResult> allDepartments = new List<USP_SELECT_ALL_tblDepartmentsResult>();
            allDepartments = db.USP_SELECT_ALL_tblDepartments().ToList();
            foreach (USP_SELECT_ALL_tblDepartmentsResult department in allDepartments)
            {
                departmentDict[department.DepartmentID] = department.DepartmentName;
            }
            Combobox_Department.ItemsSource = null;
            Combobox_Department.ItemsSource = departmentDict;

            List<USP_SELECT_ALL_tblPositionsResult> allPositions = db.USP_SELECT_ALL_tblPositions().ToList();
            foreach(USP_SELECT_ALL_tblPositionsResult positions in allPositions)
            {
                positionDict[positions.PositionID] = positions.PositionName;
            }
            Combobox_Position.ItemsSource = null;
            Combobox_Position.ItemsSource = positionDict;

            Combobox_Status.ItemsSource = null;
            Combobox_Status.ItemsSource = employeeStatusOptions;

            Combobox_ScheduleType.ItemsSource = null;
            Combobox_ScheduleType.ItemsSource = employeeScheduleType;

            //////
            ///Lambda Version 
            //Dictionary<int, string> lamdaPos = db.USP_SELECT_ALL_tblPositions().ToList()
            //    .ToDictionary(position => position.PositionID, position => position.PositionName);
            //Combobox_Position.ItemsSource = null;
            //Combobox_Position.ItemsSource = lamdaPos;
            //
        }

        private void Button_GetEmployee_Click(object sender, RoutedEventArgs e)
        {
            string userInput = Textbox_GetEmail.Text;
            List<USP_SEARCH_EMPLOYEEResult> employeeResult = db.USP_SEARCH_EMPLOYEE(userInput).ToList();
            if (employeeResult.Count == 1)
            {
                USP_SEARCH_EMPLOYEEResult employee = employeeResult[0];
                Combobox_Department.SelectedItem = departmentDict.FirstOrDefault(x => x.Key == employee.DepartmentID);
                Combobox_Position.SelectedValue = positionDict.FirstOrDefault(x => x.Key == employee.PositionID);  
                Combobox_Status.SelectedValue = employee.Status;
                Combobox_ScheduleType.SelectedValue = employee.ScheduleType;

                Textbox_LastName.Text = employee.LastName;
                Textbox_FirstName.Text = employee.FirstName;
                Textbox_Email.Text = employee.EmailAddress;
                Textbox_HomeAddress.Text = employee.HomeAddress;
                Textbox_Contact.Text = employee.PhoneNumber;    
                DatePicker_Birthday.SelectedDate = employee.Birthday;

                Textbox_LastName.IsEnabled = true;
                Textbox_FirstName.IsEnabled = true;
                Textbox_HomeAddress.IsEnabled = true;
                Textbox_Contact.IsEnabled = true;
                DatePicker_Birthday.IsEnabled = true;

                Button_Update.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("No Employee Found with that Email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            string firstName = Textbox_FirstName.Text;  
            string lastName = Textbox_LastName.Text;
            string email = Textbox_Email.Text;  
            string address = Textbox_HomeAddress.Text;  
            string contact = Textbox_Contact.Text;
            DateTime? birthday = DatePicker_Birthday.SelectedDate;

            db.USP_UPDATE_EMPLOYEE_PERSONAL(firstName, lastName, birthday, contact, email, address);

            MessageBox.Show("Employee Personal Information has been added", "Update Information", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Button_UpdateEmployment_Click(object sender, RoutedEventArgs e)
        {
 
        }
    }
}
