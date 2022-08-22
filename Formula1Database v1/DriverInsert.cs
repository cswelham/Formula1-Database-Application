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
    public partial class DriverInsert : Form
    {
        public DriverInsert()
        {
            InitializeComponent();

            //Setup Number combobox
            for (int i=0; i < 101; i++)
            {
                comboBoxNum.Items.Add(i);
            }

            //Setup country combobox
            try
            {
                //Connect to the driver collection
                var c = MongoDB.db.GetCollection<BsonDocument>("Driver");
                var documents = c.Find(new BsonDocument()).ToList();
                List<string> countries = new List<string>();
                //foreach (var item in documents)
                //
                   // if (!(countries.Contains(item[8][0].ToString())))
                    //{
                    //    countries.Add(item[8][0].ToString());
                    //}
                    //comboBoxCountry.Items.Add(item[8][0]);                    
                //}
                bool valid = true;
                foreach (var item in documents)
                {
                    foreach (Country country in DriverName.CountryList)
                    {
                        if (country.countryName == item[8][0].ToString())
                        {
                            valid = false;
                        }
                    }
                    if (valid)
                    {
                        Country count = new Country { countryName = item[8][0].ToString(), population = int.Parse(item[8][1].ToString()), area = int.Parse(item[8][2].ToString()) };
                        DriverName.CountryList.Add(count);
                    }
                    valid = true;
                }
                //Connect to the GrandPrix collection
                var c2 = MongoDB.db.GetCollection<BsonDocument>("GrandPrix");
                var documents2 = c2.Find(new BsonDocument()).ToList();
                //Add each distinct country to the array
                bool valid2 = true;
                foreach (var item in documents2)
                {
                    foreach (Country country in DriverName.CountryList)
                    {
                        if (country.countryName == item[5][0].ToString())
                        {
                            valid2 = false;
                        }
                    }
                    if (valid2)
                    {
                        Country count = new Country { countryName = item[5][0].ToString(), population = int.Parse(item[5][1].ToString()), area = int.Parse(item[5][2].ToString()) };
                        DriverName.CountryList.Add(count);
                    }
                    valid2 = true;
                }
                //Add each country to the listbox
                foreach (Country co in DriverName.CountryList)
                {
                    comboBoxCountry.Items.Add(co.countryName);
                }
            }
            catch
            {
                MessageBox.Show("There is no data for countries");
            }

            //Setup team combobox
            try
            {
                //Connect to the team collection
                var t = MongoDB.db.GetCollection<BsonDocument>("Driver");
                var documents = t.Find(new BsonDocument()).ToList();
                //Add distinct teams to the combobox
                bool valid = true;
                foreach (var item in documents)
                {
                    foreach (Team team in DriverName.TeamList)
                    {
                        if (team.teamName == item[9][0].ToString())
                        {
                            valid = false;
                        }
                    }
                    if (valid)
                    {
                        Team tea = new Team { teamName = item[9][0].ToString(), numChamp = int.Parse(item[9][1].ToString())};
                        DriverName.TeamList.Add(tea);
                    }
                    valid = true;
                }
                //Add each country to the listbox
                foreach (Team te in DriverName.TeamList)
                {
                    comboBoxTeam.Items.Add(te.teamName);
                }

            }
            catch
            {
                MessageBox.Show("There is no data for teams");
            }

            //Setup salary combobox
            for (int i = 1; i < 26; i++)
            {
                comboBoxSalary.Items.Add(i);
            }
        }

        /// <summary>
        /// Clears all controls
        /// </summary>
        public void initaliseControls()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Clear();
                }
                else if (c is ComboBox)
                {
                    (c as ComboBox).Text = "";
                }
            }
        }

        /// <summary>
        /// Inserts a driver into the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            string firstName = ""; string lastName = ""; string dateBirth = ""; string dateSalary = "";
            int number=0; int numChamp=0; int pastPos=0; int salary=0; int pid=0;
            string day1 = ""; string month1 = ""; string year1 = ""; string day2 = ""; string month2 = ""; string year2 = "";
            Country country;
            Team team;
            bool past = true;

            try
            {
                //Get all inputs
                firstName = textBoxFName.Text;
                lastName = textBoxLName.Text;
                country = DriverName.CountryList[comboBoxCountry.SelectedIndex];
                team = DriverName.TeamList[comboBoxTeam.SelectedIndex];
                number = int.Parse(comboBoxNum.Text);
                numChamp = int.Parse(textBoxNumChamp.Text);
                if (textBoxPastPos.Text != "")
                {
                    pastPos = int.Parse(textBoxPastPos.Text);
                }
                else
                {
                    past = false;
                }
                salary = int.Parse(comboBoxSalary.Text);

                //Format date
                day1 = dateTimePickerBirth.Value.Date.Day.ToString();
                month1 = dateTimePickerBirth.Value.Date.Month.ToString();
                year1 = dateTimePickerBirth.Value.Date.Year.ToString();
                day2 = dateTimePickerSalary.Value.Date.Day.ToString();
                month2 = dateTimePickerSalary.Value.Date.Month.ToString();
                year2 = dateTimePickerSalary.Value.Date.Year.ToString();

                if (day1.Length == 1)
                {
                    day1 = "0" + day1;
                }
                if (day2.Length == 1)
                {
                    day2 = "0" + day2;
                }
                if (month1.Length == 1)
                {
                    month1 = "0" + month1;
                }
                if (month2.Length == 1)
                {
                    month2 = "0" + month2;
                }
                dateBirth = day1 + "/" + month1 + "/" + year1;
                dateSalary = day2 + "/" + month2 + "/" + year2;

                //Connect to the participates driver
                var driver = MongoDB.db.GetCollection<BsonDocument>("Driver");              
                //Getting participates data and storing it in an array
                var document = driver.Find(new BsonDocument()).ToList();
                string[] database = new string[50];
                //Loop through each driver and find who has the highest id number
                foreach (var item in document)
                {
                    if (int.Parse(item[1].ToString()) > pid)
                    {
                        pid = int.Parse(item[1].ToString());
                    }
                }

                //var result = driver.Aggregate().Group(new BsonDocument
                //{
                //    {"id", "pid" }, {"pid", new BsonDocument("$max","$pid") }
                //}).ToList();
                //foreach (var item in result)
                //{
                //    Console.WriteLine(item.ToString());
                //}
            }
            catch
            {
                MessageBox.Show("Make sure your data is in the right format");
                return;
            }

            try
            {
                pid++;
                //Connect to the driver collection
                var d = MongoDB.db.GetCollection<Driver>("Driver");
                //Create a new document with the values entered
                var driver = new Driver(); ;
                if (past)
                {
                    driver = new Driver
                    {
                        ObjectID = pid,
                        pid = pid,
                        fname = firstName,
                        lname = lastName,
                        dob = dateBirth,
                        numChamp = numChamp,
                        pastPos = pastPos,
                        countryName = country,
                        teamName = team,
                        salary = salary,
                        endDate = dateSalary
                    };
                }
                else
                {
                    driver = new Driver
                    {
                        ObjectID = pid,
                        pid = pid,
                        fname = firstName,
                        lname = lastName,
                        dob = dateBirth,
                        numChamp = numChamp,
                        countryName = country,
                        teamName = team,
                        salary = salary,
                        endDate = dateSalary
                    };
                }
                //var doc = new BsonDocument
                //{
                //    {"pid", pid},
                //    {"fname", firstName},
               //     {"lname", lastName },
                //    {"dob", dateBirth},
                //    {"numChamp", numChamp.ToString() },
                //    {"num", number.ToString() },
                //    {"pastPos", pastPos.ToString() },
                //    {"countryName", country},
                //    {"teamName", team },
                //    {"salary", salary.ToString() },
                //    {"endDate", dateSalary }
                //};
                //Add the driver to the collection
                d.InsertOne(driver);
                //DriverName.ObjectID = pid;
                MessageBox.Show("Driver has been inserted succesfully with id number: "+pid);
                initaliseControls();
            }
            catch
            {
                MessageBox.Show("Could not insert driver. Try again.");
            }
        }

        /// <summary>
        /// Go back to menu
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
