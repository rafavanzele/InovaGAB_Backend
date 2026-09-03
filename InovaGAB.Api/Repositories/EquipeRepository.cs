using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class EquipeRepository
    {
        private readonly IMongoCollection<Equipe> _equipes;

        public EquipeRepository(MongoDbContext context)
        {
            _equipes = context.Equipes;
        }

        public async Task CriarAsync(Equipe equipe)
        {
            await _equipes.InsertOneAsync(equipe);
        }
    }
}