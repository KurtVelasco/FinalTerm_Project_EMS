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
    /// Interaction logic for AddEmployeeDetails.xaml
    /// </summary>
    public partial class AddEmployeeDetails : Window
    {
        public AddEmployeeDetails()
        {
            InitializeComponent();
        }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        string EDID = txtEDID.Text;
        string EID = txtEID.Text;
        string Stat = txtStat.Text;
        string DE = txtDE.Text;
    }

    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {

    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        txtEDID.Text = "";
        txtEID.Text = "";
        txtStat.Text = "";
        txtDE.Text = "";

    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        //    Dashboaed dh = new Dashboaed;
        //    dh.Show();
        //    this.Close();
    }

    private void lbxED_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void cmbDID_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void cmbPID_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void cmbST_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

}
