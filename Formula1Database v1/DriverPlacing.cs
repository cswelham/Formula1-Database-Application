using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;



namespace Formula1Database_v1
{
    public partial class DriverPlacing : Form
    {
        public DriverPlacing()
        {
            InitializeComponent();
            //Get driver data
            String[] driverSplit = DriverName.DName.Split(' ');
            //String[] idPos = new string[2];
            try
            {
                //Connect to the participates collection
                var part = MongoDB.db.GetCollection<BsonDocument>("Driver");
                var filter = Builders<BsonDocument>.Filter.Eq("$pid", DriverName.Pid);
                //var documents = part.Find(filter).ToList();
                //long count = part.CountDocuments(filter);

                //Getting participates data and storing it in an array
                var document2 = part.Find(new BsonDocument()).ToList();
                //string[] database = new string[50];
                List<Array> data = new List<Array>();
                int count = 0;
                foreach (var item in document2)
                {
                    if (item[1].ToString() == DriverName.Pid && item[12] != null)
                    {
                        //foreach (var it in item[12])
                        //{
                        try
                        {
                            if (int.Parse(item[12][0][1].ToString()) <= 10)
                            {
                                count++;
                            }
                            if (int.Parse(item[12][1][1].ToString()) <= 10)
                            {
                                count++;
                            }
                            //Display drivers to gui
                            listBoxDriver.Items.Add(driverSplit[0].PadRight(20) + driverSplit[1].PadRight(20) + count.ToString());
                        } 
                        catch
                        {
                            //Display drivers to gui
                            listBoxDriver.Items.Add(driverSplit[0].PadRight(20) + driverSplit[1].PadRight(20) + count.ToString());
                        }
                        
                        //var what = item[12][1][1].ToString();
                       // var what2 = item[1];
                       //     string[] a = new string[3];
                       //     a[0] = item[12][1].ToString();
                       //     data.Add(a);
                        //}
                       
                    }                    
                }

                //Find drivers with the same pid who place in the top 10
                //int count = 0;
                //foreach (Array r in data)
                //{
                 //   if (r != null)
                //    {
                //        string[] record = r.Split(',');
                //        if (r.Length > 3)
               //         {
               //             if (record[1] == DriverName.Pid && int.Parse(record[2]) < 11)
               //             {
               //                 count++;
               //             }
               //         }
               //     }      
               // }

                //Display drivers to gui
                //listBoxDriver.Items.Add(driverSplit[0].PadRight(20) + driverSplit[1].PadRight(20) + count.ToString());

            }
            catch
            {
                int count = 0;
                MessageBox.Show("The driver has not participated in a Grand Prix or has null data in the database");
                listBoxDriver.Items.Add(driverSplit[0].PadRight(20) + driverSplit[1].PadRight(20) + count.ToString());
            }
        }

        /// <summary>
        /// Go back to driver select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hide this form
            this.Hide();
            //Create new object for drivers and go to it
            DriverSelect d = new DriverSelect();
            d.ShowDialog();
            //Close current form
            this.Close();
        }
    }
}
