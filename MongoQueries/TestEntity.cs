using System;
using System.Collections.Generic;
using System.Text;
using MongoQueries.Utils;

namespace MongoQueries
{
    [MongoCollection("testcollection")]
    public class TestEntity
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string name_lower { get { return name.ToLower(); } set { return; } }
    }
}
