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
        public UpdateEmployeeDetails()
        {
            InitializeComponent();
        }
        private void Button_GetEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool isTrure = true;
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
        private void FillComboBoxes()
        {
            // Example code, will be implemented once the the Database has been established

            //List<USP_GETALL_AUTHORSResult> allAuthors = new List<USP_GETALL_AUTHORSResult>();
            //allAuthors = db.USP_GETALL_AUTHORS().ToList();
            //foreach (USP_GETALL_AUTHORSResult authors in allAuthors)
            //{
            //    authorsDict[authors.AuthorID] = authors.AuthorName;
            //}
            //comboBoxAuthors.ItemsSource = null;
            //comboBoxAuthors.ItemsSource = authorsDict;
        }
        private void Button_UpdateEmployment_Click(object sender, RoutedEventArgs e)
        {
 
            //int departmentID = //KeyValuePair get The ID
        }
    }
}
