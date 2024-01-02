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
        public int EMPLOYEE_ID;
        public string EMPLOYEE_EMAIL = string.Empty;
        EmployeeDatabaseDataContext db = new EmployeeDatabaseDataContext(Properties.Settings.Default.MockEMSDatabaseConnectionString);
        public UpdateEmployeeDetails()
        {
            InitializeComponent();
        }
        private void FillComboBoxes()
        {
            List<USP_SELECT_ALL_tblDepartmentsResult> allDepartments = new List<USP_SELECT_ALL_tblDepartmentsResult>();
            Dictionary<int, string> departmentDict = new Dictionary<int, string>();
            allDepartments = db.USP_SELECT_ALL_tblDepartments().ToList();
            foreach (USP_SELECT_ALL_tblDepartmentsResult department in allDepartments)
            {
                departmentDict[department.DepartmentID] = department.DepartmentName;
            }
            Combobox_Department.ItemsSource = null;
            Combobox_Department.ItemsSource = departmentDict;


            //////
            ///Lambda Version 
            Dictionary<int, string> lamdaPos = db.USP_SELECT_ALL_tblPositions().ToList()
                .ToDictionary(position => position.PositionID, position => position.PositionName);
            Combobox_Position.ItemsSource = null;
            Combobox_Position.ItemsSource = lamdaPos;
            //
        }

        private void Button_GetEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool isTrure = true;
            string userInput = Textbox_GetEmail.Text;
            List<USP_SEARCH_EMPLOYEEResult> employeeResult = db.USP_SEARCH_EMPLOYEE(userInput).ToList();
            if(employeeResult.Count != 1)
            {
                MessageBox.Show("No Employee Found","Among Us OS",MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (isTrure)
            {
                MessageBox.Show("Employee Get");
                Textbox_LastName.IsEnabled = true;
                Textbox_FirstName.IsEnabled = true;
                Textbox_Email.IsEnabled = true;
                Textbox_HomeAddress.IsEnabled = true;
                Button_Update.IsEnabled = true;
            }
        }
        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            string firstName = Textbox_FirstName.Text;  
            string lastName = Textbox_LastName.Text;
            string email = Textbox_Email.Text;  
            string address = Textbox_HomeAddress.Text;  
            string contact = Textbox_Contact.Text;

            MessageBox.Show(firstName + "\n" + lastName + "\n" + email + "\n" + address + "\n" + contact);
        }
        private void Button_UpdateEmployment_Click(object sender, RoutedEventArgs e)
        {
 
        }
    }
}
