using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoQueries.Utils
{

    public class MongoCollectionAttribute : Attribute
    {
        public MongoCollectionAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set; }
    }
    public static class MongoExtensions
    {
        public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase db)
        {
            object[] collectionAttr = typeof(TDocument).GetCustomAttributes(typeof(MongoCollectionAttribute), false);
            if (collectionAttr.Length > 0)
            {
                return db.GetCollection<TDocument>((collectionAttr[0] as MongoCollectionAttribute).Name);
            }
            else
            {
                return db.GetCollection<TDocument>(typeof(TDocument).Name);
            }
        }

    }
}
