using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Windows.Forms;

namespace Formula1Database_v1
{
    class MongoDB
    {
        //generates the connection to the database       
        //Make sure that in the Database connection you put your Database connection here:
        public static string mongoDB = "mongodb://compx323-26:dz1o88Ap7g@mongodb.cms.waikato.ac.nz:27017";
        public static MongoClient dbClient = new MongoClient(mongoDB);  // C#
        public static IMongoDatabase db = dbClient.GetDatabase("compx323-26");
    }
}
