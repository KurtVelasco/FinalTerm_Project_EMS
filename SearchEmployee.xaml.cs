﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SearchEmployee : Window
    {
        EmployeeDatabaseDataContext db = new EmployeeDatabaseDataContext(Properties.Settings.Default.MockEMSDatabaseConnectionString);
        
        private List<string> employeeData = new List<string>();

        public SearchEmployee()
        {
            InitializeComponent();
            UIInitialization();
            ListAll();
        }
        
        private void UIInitialization()
        {
            //Emulates Database Data
            List<Retrieve_EmployeeDetailsResult> allDetails = new List<Retrieve_EmployeeDetailsResult>();
            allDetails = db.Retrieve_EmployeeDetails().ToList();
            foreach (var detail in allDetails)
            {
                employeeData.Add($"{detail.FirstName},{detail.MiddleName},{detail.LastName},{detail.DepartmentName},{detail.PositionName},{detail.StatusName}");
            }

            cbxDeptSearch.Items.Add("[Department]");
            cbxPosSearch.Items.Add("[Position]");
            cbxStsSearch.Items.Add("[Status]");

            cbxDeptSearch.SelectedIndex = 0;
            cbxPosSearch.SelectedIndex = 0;
            cbxStsSearch.SelectedIndex = 0;
        }

        public class Employee
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Department { get; set; }
            public string Position { get; set; }
            public string Status { get; set; }
        }

        private void ListAll()
        {
            lv_employeedata.Items.Clear();
            lv_employeedata.ItemsSource = null;
            foreach (string item in employeeData)
            {
                string[] split = item.Split(',');
                lv_employeedata.Items.Add(new Employee
                {
                    FirstName = split[0],
                    MiddleName = split[1],
                    LastName = split[2],
                    Department = split[3],
                    Position = split[4],
                    Status = split[5]
                });

                if (!cbxDeptSearch.Items.Contains(split[3]))
                {
                    cbxDeptSearch.Items.Add(split[3]);
                }
                if (!cbxPosSearch.Items.Contains(split[4]))
                {
                    cbxPosSearch.Items.Add(split[4]);
                }
                if (!cbxStsSearch.Items.Contains(split[5]))
                {
                    cbxStsSearch.Items.Add(split[5]);
                }
            }
        }
        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            ListAll();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtFNSearch.IsEnabled == true && txtLNSearch.IsEnabled == true)
            {
                if (txtFNSearch.Text != "First Name" && txtLNSearch.Text != "Last Name")
                {
                    SearchByName(txtFNSearch.Text, txtLNSearch.Text);
                }
                else
                {
                    MessageBox.Show("Both text fields must have a valid name.");
                }
            }
            else if (cbxDeptSearch.IsEnabled == true)
            {
                if (cbxDeptSearch.SelectedIndex != 0)
                {
                    SearchByComboBoxes(cbxDeptSearch.SelectedItem.ToString());
                }
            }
            else if (cbxPosSearch.IsEnabled == true)
            {
                if (cbxPosSearch.SelectedIndex != 0)
                {
                    SearchByComboBoxes(cbxPosSearch.SelectedItem.ToString());
                }
            }
            else if (cbxStsSearch.IsEnabled == true)
            {
                if (cbxStsSearch.IsEnabled == true)
                {
                    SearchByComboBoxes(cbxStsSearch.SelectedItem.ToString());
                }
            }
            InsertLogs.AddLogs(LogInCredentials.EMPLOYEE_ID, "Employee Used The Searched System", 4);
        }

        private void SearchByName(string firstName, string lastName)
        {
            //PlaceHolder
            lv_employeedata.Items.Clear();
            foreach (string item in employeeData)
            {
                string[] split = item.Split(',');
                if (firstName == split[0] && lastName == split[2])
                {
                    lv_employeedata.Items.Add(new Employee
                    {
                        FirstName = split[0],
                        MiddleName = split[1],
                        LastName = split[2],
                        Department = split[3],
                        Position = split[4],
                        Status = split[5]
                    });
                }
            }
        }

        private void SearchByComboBoxes(string value)
        {
            bool dept = false;
            bool pos = false;
            bool sts = false;
            lv_employeedata.Items.Clear();
            if (cbxDeptSearch.IsEnabled == true)
            {
                dept = true;
            }
            else if (cbxPosSearch.IsEnabled == true)
            {
                pos = true;
            }
            else if (cbxStsSearch.IsEnabled == true)
            {
                sts = true;
            }

            //PlaceHolder
            lv_employeedata.ItemsSource = null;
            foreach (string item in employeeData)
            {
                string[] split = item.Split(',');
                if (dept)
                {
                    if (value == split[3])
                    {
                        lv_employeedata.Items.Add(new Employee
                        {
                            FirstName = split[0],
                            MiddleName = split[1],
                            LastName = split[2],
                            Department = split[3],
                            Position = split[4],
                            Status = split[5]
                        });
                    }
                }
                else if (pos)
                {
                    if (value == split[4])
                    {
                        lv_employeedata.Items.Add(new Employee
                        {
                            FirstName = split[0],
                            MiddleName = split[1],
                            LastName = split[2],
                            Department = split[3],
                            Position = split[4],
                            Status = split[5]
                        });
                    }
                }
                else if (sts)
                {
                    if (value == split[5])
                    {
                        lv_employeedata.Items.Add(new Employee
                        {
                            FirstName = split[0],
                            MiddleName = split[1],
                            LastName = split[2],
                            Department = split[3],
                            Position = split[4],
                            Status = split[5]
                        });
                    }
                }
            }
        }

        private void TextGotFocus(object sender, RoutedEventArgs e)
        {
            if (txtFNSearch.IsFocused)
            {
                if (txtFNSearch.Text.Length > 0 && txtFNSearch.Text == "First Name")
                {
                    txtFNSearch.Text = "";
                }
            }
            else if (txtLNSearch.IsFocused)
            {
                if (txtLNSearch.Text.Length > 0 && txtLNSearch.Text == "Last Name")
                {
                    txtLNSearch.Text = "";
                }
            }

            cbxDeptSearch.IsEnabled = false;
            cbxPosSearch.IsEnabled = false;
            cbxStsSearch.IsEnabled = false;
        }

        private void TextLostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFNSearch.Text.Length == 0)
            {
                txtFNSearch.Text = "First Name";
            }
            else if (txtLNSearch.Text.Length == 0)
            {
                txtLNSearch.Text = "Last Name";
            }

            if (txtFNSearch.Text == "First Name" && txtLNSearch.Text == "Last Name")
            {
                cbxDeptSearch.IsEnabled = true;
                cbxPosSearch.IsEnabled = true;
                cbxStsSearch.IsEnabled = true;
            }
        }

        private void CBXLostFocus(object sender, RoutedEventArgs e)
        {
            if (cbxDeptSearch.SelectedItem.ToString() != "[Department]")
            {
                cbxPosSearch.IsEnabled = false;
                cbxStsSearch.IsEnabled = false;
                txtFNSearch.IsEnabled = false;
                txtLNSearch.IsEnabled = false;
            }
            else if (cbxPosSearch.SelectedItem.ToString() != "[Position]")
            {
                cbxDeptSearch.IsEnabled = false;
                cbxStsSearch.IsEnabled = false;
                txtFNSearch.IsEnabled = false;
                txtLNSearch.IsEnabled = false;
            }
            else if (cbxStsSearch.SelectedItem.ToString() != "[Status]")
            {
                cbxDeptSearch.IsEnabled = false;
                cbxPosSearch.IsEnabled = false;
                txtFNSearch.IsEnabled = false;
                txtLNSearch.IsEnabled = false;
            }
            else
            {
                cbxDeptSearch.IsEnabled = true;
                cbxStsSearch.IsEnabled = true;
                cbxPosSearch.IsEnabled = true;
                txtFNSearch.IsEnabled = true;
                txtLNSearch.IsEnabled = true;
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