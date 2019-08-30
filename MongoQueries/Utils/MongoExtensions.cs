using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
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

        public string InitIndexes { get; set; }
    }
    public static class MongoExtensions
    {
        private static IDictionary<Type, bool> initialiedMarker = new ConcurrentDictionary<Type, bool>();
        public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase db)
        {
            var TDocumentType = typeof(TDocument);
            object[] collectionAttr = TDocumentType.GetCustomAttributes(typeof(MongoCollectionAttribute), false);
            if (collectionAttr.Length > 0)
            {
                MongoCollectionAttribute attr = collectionAttr[0] as MongoCollectionAttribute;
                if (attr != null)
                {
                    string collectionName = string.IsNullOrWhiteSpace(attr.Name) ? TDocumentType.Name : attr.Name;
                    if (!string.IsNullOrWhiteSpace(attr.InitIndexes)
                        && !initialiedMarker.ContainsKey(TDocumentType))
                    {
                        initialiedMarker[TDocumentType] = true;
                        var method = TDocumentType.GetMethod(attr.InitIndexes, BindingFlags.NonPublic | BindingFlags.Static);
                        if (method != null && method.IsStatic)
                        {
                            method.Invoke(null, new object[] { db });
                        }
                    }
                    return db.GetCollection<TDocument>(collectionName);
                }
            }


            return db.GetCollection<TDocument>(TDocumentType.Name);

        }

    }
}
