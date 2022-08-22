using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Formula1Database_v1
{
    [BsonIgnoreExtraElements]
    public class Country
    {
        [BsonId]
        public string countryName { get; set; }
        public int population { get; set; }
        public int area { get; set; }
    }
}
