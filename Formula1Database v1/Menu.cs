using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formula1Database_v1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Goesto select a driver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            //Hide this form
            this.Hide();
            //Create new object for drivers and go to it
            DriverSelect d = new DriverSelect();
            d.ShowDialog();
            //Close current form
            this.Close();
        }

        /// <summary>
        /// Goes to insert a driver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            //Hide this form
            this.Hide();
            //Create new object for drivers and go to it
            DriverInsert d = new DriverInsert();
            d.ShowDialog();
            //Close current form
            this.Close();
        }
    }
}
