using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1Database_v1
{
    class DriverName
    {
        private static string driverName = "";
        private static string pid = "";
        private static List<Country> countryList = new List<Country>();
        private static List<Team> teamList = new List<Team>();
        private static List<Driver> driverList = new List<Driver>();
        private static int objectID = 1;

        public static string DName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        public static string Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        public static List<Country> CountryList
        {
            get { return countryList; }
            set { countryList = value; }
        }

        public static List<Team> TeamList
        {
            get { return teamList; }
            set { teamList = value; }
        }

        public static List<Driver> DriverList
        {
            get { return driverList; }
            set { driverList = value; }
        }

        public static int ObjectID
        {
            get { return objectID; }
            set { objectID = value; }
        }
    }
}
