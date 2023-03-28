using BookStoreAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreAPI.Data
{
    public class Repository
    {
        public readonly IMongoCollection<Book> Books;

        public Repository(IOptions<Settings> options)
        {
            MongoClient mongoClient = new MongoClient(options.Value.Connection);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(options.Value.Database);

            Books = mongoDatabase.GetCollection<Book>(options.Value.Collection);
        }
    }
}
