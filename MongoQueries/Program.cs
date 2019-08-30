using MongoDB.Driver;
using System;
using MongoQueries.Utils;
using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost:27017/test";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");

            /*for (int i = 0; i < 500000; i++)
            {
                db.GetCollection<TestEntity>().InsertOne(new TestEntity(){id = Guid.NewGuid(), name= $@"test{i}"});
            }

            for (int i = 0; i < 500000; i++)
            {
                db.GetCollection<TestEntity>().InsertOne(new TestEntity() { id = Guid.NewGuid(), name = $@"abc{i}" });
            }

            for (int i = 0; i < 500000; i++)
            {
                db.GetCollection<TestEntity>().InsertOne(new TestEntity() { id = Guid.NewGuid(), name = $@"xyz{i}" });
            }*/

            var q = new List<string>() { "abc1", "xyz1", "test1" };
            var filter = Builders<TestEntity>.Filter.Where(x => q.Contains(x.name_lower));

            var tests = db.GetCollection<TestEntity>().FindSync(filter).ToList();
            foreach(var t in tests)
            {
                Console.WriteLine(t.ToJson());
            }
        }
    }
}
