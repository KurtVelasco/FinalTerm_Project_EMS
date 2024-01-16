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
            //List<USP_SELECT_ALL_tblDepartmentsResult> allDepartments = new List<USP_SELECT_ALL_tblDepartmentsResult>();
            //allDepartments = db.USP_SELECT_ALL_tblDepartments().ToList();
            //foreach (USP_SELECT_ALL_tblDepartmentsResult department in allDepartments)
            //{
            //    departmentDict[department.DepartmentID] = department.DepartmentName;
            //}
            //Combobox_Department.ItemsSource = departmentDict;

            //List<USP_SELECT_ALL_tblPositionsResult> allPositions = db.USP_SELECT_ALL_tblPositions().ToList();
            //foreach(USP_SELECT_ALL_tblPositionsResult positions in allPositions)
            //{
            //    positionDict[positions.PositionID] = positions.PositionName;
            //}


            //statusDict = db.USP_SELECT_ALL_tblStatus().ToDictionary(status => status.StatusID, status => status.StatusName);

            //schedDict = db.USP_SELECT_ALL_tblSchedType().ToDictionary(sched => sched.ScheduleTypeID, sched => sched.ScheduleType);

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
            Combobox_Department.ItemsSource = departmentDict;
            Combobox_ScheduleType.ItemsSource = schedDict;

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
            List<USP_SEARCH_EMPLOYEE_BY_EMAILResult> employeeResult = db.USP_SEARCH_EMPLOYEE_BY_EMAIL(userInput).ToList();
            if (employeeResult.Count == 1)
            {
                USP_SEARCH_EMPLOYEE_BY_EMAILResult employee = employeeResult[0];
                Combobox_Department.SelectedItem = departmentDict.FirstOrDefault(x => x.Key == employee.DepartmentID);
                Combobox_Position.SelectedItem = positionDict.FirstOrDefault(x => x.Key == employee.PositionID);  
                Combobox_Status.SelectedItem = statusDict.FirstOrDefault(x => x.Key== employee.StatusID);
                Combobox_ScheduleType.SelectedItem = schedDict.FirstOrDefault(x => x.Key == employee.ScheduleTypeID);
                Textbox_LastName.Text = employee.LastName;
                Textbox_MiddleName.Text = employee.MiddleName;
                Textbox_FirstName.Text = employee.FirstName;
                Textbox_Email.Text = employee.EmailAddress;
                Textbox_HomeAddress.Text = employee.HomeAddress;
                Textbox_Contact.Text = employee.PhoneNumber;    
                DatePicker_Birthday.SelectedDate = employee.Birthday;
                DatePicker_EmployedOn.SelectedDate = employee.EmployedOn;   


                Textbox_MiddleName.IsEnabled = true;
                Textbox_LastName.IsEnabled = true;
                Textbox_Email.IsEnabled = true; 
                Textbox_FirstName.IsEnabled = true;
                Textbox_HomeAddress.IsEnabled = true;
                Textbox_Contact.IsEnabled = true;
                DatePicker_Birthday.IsEnabled = true;
                DatePicker_EmployedOn.IsEnabled = true;
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
            string middleName = Textbox_MiddleName.Text;
            string lastName = Textbox_LastName.Text;
            string email = Textbox_Email.Text;  
            string address = Textbox_HomeAddress.Text;  
            string contact = Textbox_Contact.Text;
            DateTime? birthday = DatePicker_Birthday.SelectedDate;

            db.USP_UPDATE_EMPLOYEE_PERSONAL(firstName,middleName,lastName, birthday, contact, email, address);

            MessageBox.Show("Employee Personal Information has been added", "Update Information", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Button_UpdateEmployment_Click(object sender, RoutedEventArgs e)
        {
 
        }

        private void Button_ReturnMenu_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagment_Admin am = new EmployeeManagment_Admin();
            am.Show();
            this.Close();
        }
    }
}
