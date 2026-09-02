using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class IdeiaRepository
    {
        private readonly IMongoCollection<Ideia> _ideias;

        public IdeiaRepository(MongoDbContext context)
        {
            _ideias = context.Ideias;
        }

        public async Task<List<Ideia>> ListarTodasAsync()
        {
            return await _ideias.Find(_ => true).ToListAsync();
        }

        public async Task<Ideia?> BuscarPorIdAsync(string id)
        {
            return await _ideias
                .Find(ideia => ideia.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CriarAsync(Ideia ideia)
        {
            await _ideias.InsertOneAsync(ideia);
        }

        public async Task<bool> AtualizarAsync(string id, Ideia ideia)
        {
            var resultado = await _ideias.ReplaceOneAsync(
                ideiaExistente => ideiaExistente.Id == id,
                ideia
            );

            return resultado.ModifiedCount > 0;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var resultado = await _ideias.DeleteOneAsync(
                ideia => ideia.Id == id
            );

            return resultado.DeletedCount > 0;
        }
    }
}