using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeMonkeys___HVMPR_Project.Forms
{
    public partial class MenuPage : Form
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();                // minimizes current page
            Form1 mainmap = new Form1();  // creates new map
            mainmap.ShowDialog();         // shows map
            this.Close();               // exits menu page
        }

        private void addDriverPress_Click(object sender, EventArgs e)
        {
            this.Hide();                // minimizes current page
            Form4 DriversForm = new Form4();  // creates new add driver
            DriversForm.ShowDialog();         // shows adddriver
            this.Close();               // exits menu page

        }

        private void addVanPress_Click(object sender, EventArgs e)
        {
            this.Hide();                // minimizes current page
            Form3 VansForm = new Form3();  // creates new addvan page
            VansForm.ShowDialog();         // shows addvan
            this.Close();               // exits menu page
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            this.Hide();                // minimizes current page
            Form2 login = new Form2();  // creates new login page
            login.ShowDialog();         // shows login page
            this.Close();               // exits menu page
        }
    }
}
