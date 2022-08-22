using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Formula1Database_v1
{
    [BsonIgnoreExtraElements]
    public class Team
    {
        [BsonId]
        public string teamName { get; set; }
        public int numChamp { get; set; }
    }
}
