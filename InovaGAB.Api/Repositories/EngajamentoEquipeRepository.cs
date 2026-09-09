using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class EngajamentoEquipeRepository
    {
        private readonly IMongoCollection<EngajamentoEquipe> _collection;

        public EngajamentoEquipeRepository(MongoDbContext context)
        {
            _collection = context.EngajamentosEquipes;
        }

        public async Task<EngajamentoEquipe> CriarAsync(EngajamentoEquipe engajamento)
        {
            await _collection.InsertOneAsync(engajamento);
            return engajamento;
        }

        public async Task<List<EngajamentoEquipe>> ListarTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<EngajamentoEquipe?> BuscarPorIdAsync(string id)
        {
            return await _collection
                .Find(engajamento => engajamento.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AtualizarAsync(EngajamentoEquipe engajamento)
        {
            var resultado = await _collection.ReplaceOneAsync(
                item => item.Id == engajamento.Id,
                engajamento
            );

            return resultado.MatchedCount > 0;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var resultado = await _collection.DeleteOneAsync(
                engajamento => engajamento.Id == id
            );

            return resultado.DeletedCount > 0;
        }
    }
}