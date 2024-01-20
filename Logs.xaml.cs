﻿using System;
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
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        public EmployeeDatabaseDataContext DB { get; set; } = new EmployeeDatabaseDataContext();

        public Logs()
        {
            InitializeComponent();
            FillComboBoxes();
        }
        public void FillComboBoxes()
        {
            Dictionary<int,string> logType = new Dictionary<int,string>();   
            foreach(tblLogType type in DB.tblLogTypes)
            {
                logType[type.LogTypeID] = type.LogType;
            }
            ComboBox_LogType.ItemsSource = logType;
        }
        public void GetLogs()
        {
            foreach (tblLog logs in DB.tblLogs)
            {
                ListView_Logs.Items.Add(new DataLogs
                {
                    EmployeeID = logs.EmployeeID,
                    LogDescription = logs.LogDescription, 
                    Logtype = logs.LogTypeID,
                    Datetime = logs.LogDate.ToString() 
                });
            }
        }

        public class DataLogs
        {
            public int EmployeeID { get; set; }
            public string LogDescription { get; set; }
            public int Logtype { get; set; } 
            public string Datetime { get; set; }
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagment_Admin ea = new EmployeeManagment_Admin();
            ea.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListView_Logs.ItemsSource = null;
            ListView_Logs.Items.Clear();
            int typeID = ((KeyValuePair<int,string>)ComboBox_LogType.SelectedItem).Key;
            foreach (tblLog logs in DB.tblLogs)
            {
                if(logs.LogTypeID == typeID)
                {
                    ListView_Logs.Items.Add(new DataLogs
                    {
                        EmployeeID = logs.EmployeeID,
                        LogDescription = logs.LogDescription,
                        Logtype = logs.LogTypeID,
                        Datetime = logs.LogDate.ToString()
                    });
                }
            }

        }

        private void Button_SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            int employeeID = -1;
            if(!int.TryParse(TextBox_EmployeeID.Text, out employeeID))
            {
                MessageBox.Show("Please Enter a valid EMployee ID", "Invalid EMployee ID", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            ListView_Logs.ItemsSource = null;
            ListView_Logs.Items.Clear();
           
            foreach (tblLog logs in DB.tblLogs)
            {
                if (logs.EmployeeID == employeeID)
                {
                    ListView_Logs.Items.Add(new DataLogs
                    {
                        EmployeeID = logs.EmployeeID,
                        LogDescription = logs.LogDescription,
                        Logtype = logs.LogTypeID,
                        Datetime = logs.LogDate.ToString()
                    });
                }
            }
        }
    }
}