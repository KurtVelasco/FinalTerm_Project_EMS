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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SearchEmployee : Window
    {
        List<string> SAMPLE_DATA = new List<string>();
        public SearchEmployee()
        {
            InitializeComponent();
            UIInitialization();
            ListAll();
        }
        
        private void UIInitialization()
        {
            //Emulates Database Data
            SAMPLE_DATA.Add("Eldridge Gabriel,Esteban,Non-Teaching Department,Maintenance,Active");
            SAMPLE_DATA.Add("Kurt Francis,Velasco,Non-Teaching Department,Maintenance,Inactive");
            SAMPLE_DATA.Add("Keane Andre,Tolentino,Non-Teaching Department,Maintenance,Active");

            cbxDeptSearch.Items.Add("[Department]");
            cbxPosSearch.Items.Add("[Position]");
            cbxStsSearch.Items.Add("[Status]");

            cbxDeptSearch.SelectedIndex = 0;
            cbxPosSearch.SelectedIndex = 0;
            cbxStsSearch.SelectedIndex = 0;
        }

        private void ListAll()
        {
            lbxFNResult.Items.Clear();
            lbxLNResult.Items.Clear();
            lbxDeptResult.Items.Clear();
            lbxPosResult.Items.Clear();
            lbxStsResult.Items.Clear();
            foreach (string item in SAMPLE_DATA)
            {
                string[] split = item.Split(',');
                lbxFNResult.Items.Add(split[0]);
                lbxLNResult.Items.Add(split[1]);
                lbxDeptResult.Items.Add(split[2]);
                lbxPosResult.Items.Add(split[3]);
                lbxStsResult.Items.Add(split[4]);
                if (!cbxDeptSearch.Items.Contains(split[2]))
                {
                    cbxDeptSearch.Items.Add(split[2]);
                }
                if (!cbxPosSearch.Items.Contains(split[3]))
                {
                    cbxPosSearch.Items.Add(split[3]);
                }
                if (!cbxStsSearch.Items.Contains(split[4]))
                {
                    cbxStsSearch.Items.Add(split[4]);
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
        }

        private void SearchByName(string firstName, string lastName)
        {
            //PlaceHolder
            lbxFNResult.Items.Clear();
            lbxLNResult.Items.Clear();
            lbxDeptResult.Items.Clear();
            lbxPosResult.Items.Clear();
            lbxStsResult.Items.Clear();
            foreach (string item in SAMPLE_DATA)
            {
                string[] split = item.Split(',');
                if (firstName == split[0] && lastName == split[1])
                {
                    lbxFNResult.Items.Add(split[0]);
                    lbxLNResult.Items.Add(split[1]);
                    lbxDeptResult.Items.Add(split[2]);
                    lbxPosResult.Items.Add(split[3]);
                    lbxStsResult.Items.Add(split[4]);
                }
            }
        }

        private void SearchByComboBoxes(string value)
        {
            bool dept = false;
            bool pos = false;
            bool sts = false;

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
            lbxFNResult.Items.Clear();
            lbxLNResult.Items.Clear();
            lbxDeptResult.Items.Clear();
            lbxPosResult.Items.Clear();
            lbxStsResult.Items.Clear();
            foreach (string item in SAMPLE_DATA)
            {
                string[] split = item.Split(',');
                if (dept)
                {
                    if (value == split[2])
                    {
                        lbxFNResult.Items.Add(split[0]);
                        lbxLNResult.Items.Add(split[1]);
                        lbxDeptResult.Items.Add(split[2]);
                        lbxPosResult.Items.Add(split[3]);
                        lbxStsResult.Items.Add(split[4]);
                    }
                }
                else if (pos)
                {
                    if (value == split[3])
                    {
                        lbxFNResult.Items.Add(split[0]);
                        lbxLNResult.Items.Add(split[1]);
                        lbxDeptResult.Items.Add(split[2]);
                        lbxPosResult.Items.Add(split[3]);
                        lbxStsResult.Items.Add(split[4]);
                    }
                }
                else if (sts)
                {
                    if (value == split[4])
                    {
                        lbxFNResult.Items.Add(split[0]);
                        lbxLNResult.Items.Add(split[1]);
                        lbxDeptResult.Items.Add(split[2]);
                        lbxPosResult.Items.Add(split[3]);
                        lbxStsResult.Items.Add(split[4]);
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

    }
}
