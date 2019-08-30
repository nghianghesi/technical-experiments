using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoQueries.Utils;

namespace MongoQueries
{
    [MongoCollection("testcollection", InitIndexes= "InitIndexes")]
    public class TestEntity
    {
        private static void InitIndexes(IMongoDatabase db)
        {

            /*var indexKeys = Builders<TestEntity>.IndexKeys.Text(t => t.name);
            var indexOptions = new CreateIndexOptions();
            var indexModel = new CreateIndexModel<TestEntity>(indexKeys, indexOptions);
            db.GetCollection<TestEntity>().Indexes.CreateOne(indexModel);*/

            var indexKeys = Builders<TestEntity>.IndexKeys.Ascending(t => t.name_lower);
            var indexOptions = new CreateIndexOptions();
            var indexModel = new CreateIndexModel<TestEntity>(indexKeys, indexOptions);
            db.GetCollection<TestEntity>().Indexes.CreateOne(indexModel);

        }
        public Guid id { get; set; }
        public string name { get; set; }
        public string name_lower { get { return name.ToLower(); } set { return; } }
    }
}
