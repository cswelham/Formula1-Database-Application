using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Formula1Database_v1
{
    [BsonIgnoreExtraElements]
    public class Driver
    {
        [BsonId]
        public int ObjectID { get; set; }
        public int pid { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string dob { get; set; }
        public int numChamp { get; set; }
        public int num { get; set; }
        public int pastPos { get; set; }
        public Country countryName { get; set; }
        public Team teamName { get; set; }
        public int salary { get; set; }
        public string endDate { get; set; }
        //public Array participates { get; set; }
    }
}
