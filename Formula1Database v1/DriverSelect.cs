using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Formula1Database_v1
{
    public partial class DriverSelect : Form
    {
        public DriverSelect()
        {
            InitializeComponent();
            //Connect to the driver collection
            var driver = MongoDB.db.GetCollection<BsonDocument>("Driver");
            var documents = driver.Find(new BsonDocument()).ToList();
            //For each driver in the collection add them to the combobox
            foreach (var item in documents)
            {
                comboBoxDriver.Items.Add(item[2] + " " + item[3]);
            }
        }

        /// <summary>
        /// Selects a driver and goes to driver placing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGo_Click(object sender, EventArgs e)
        {
            //Check if combo box is selected
            if (comboBoxDriver.Text != null)
            {
                //Connect to the driver collection
                var driver = MongoDB.db.GetCollection<BsonDocument>("Driver");
                var documents = driver.Find(new BsonDocument()).ToList();
                int index = comboBoxDriver.SelectedIndex;
                var item = documents[index];
                //Get the id and name of the driver
                DriverName.Pid = item[1].ToString();
                DriverName.DName = comboBoxDriver.Text;

                //Hide this form
                this.Hide();
                //Create new object for drivers and go to it
                DriverPlacing dp = new DriverPlacing();
                dp.ShowDialog();
                //Close current form
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a driver");
            }
            
        }

        /// <summary>
        /// Goes back to menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            //Hide this form
            this.Hide();
            //Create new object for drivers and go to it
            Menu d = new Menu();
            d.ShowDialog();
            //Close current form
            this.Close();
        }
    }
}
