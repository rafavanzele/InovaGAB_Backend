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

        public async Task<List<Equipe>> ListarTodasAsync()
        {
            return await _equipes
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<Equipe?> BuscarPorIdAsync(string id)
        {
            return await _equipes
                .Find(equipe => equipe.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AtualizarAsync(string id, Equipe equipe)
        {
            await _equipes.ReplaceOneAsync(
                equipeExistente => equipeExistente.Id == id,
                equipe);
        }

        public async Task ExcluirAsync(string id)
        {
            await _equipes.DeleteOneAsync(
                equipe => equipe.Id == id);
        }
    }
}