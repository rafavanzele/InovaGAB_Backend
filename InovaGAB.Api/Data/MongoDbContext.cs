using InovaGAB.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InovaGAB.Api.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Usuario> Usuarios =>
            _database.GetCollection<Usuario>("usuarios");

        public IMongoCollection<Ideia> Ideias =>
            _database.GetCollection<Ideia>("ideias");

        public IMongoCollection<Projeto> Projetos =>
    _database.GetCollection<Projeto>("projetos");
    }
}