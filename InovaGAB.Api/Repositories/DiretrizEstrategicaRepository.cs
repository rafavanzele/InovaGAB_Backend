using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class DiretrizEstrategicaRepository
    {
        private readonly IMongoCollection<DiretrizEstrategica> _diretrizes;

        public DiretrizEstrategicaRepository(MongoDbContext context)
        {
            _diretrizes = context.DiretrizesEstrategicas;
        }

        public async Task CriarAsync(DiretrizEstrategica diretriz)
        {
            await _diretrizes.InsertOneAsync(diretriz);
        }

        public async Task<List<DiretrizEstrategica>> ListarTodasAsync()
        {
            return await _diretrizes
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<DiretrizEstrategica?> BuscarPorIdAsync(string id)
        {
            return await _diretrizes
                .Find(diretriz => diretriz.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AtualizarAsync(string id, DiretrizEstrategica diretriz)
        {
            await _diretrizes.ReplaceOneAsync(
                diretrizExistente => diretrizExistente.Id == id,
                diretriz);
        }

        public async Task ExcluirAsync(string id)
        {
            await _diretrizes.DeleteOneAsync(
                diretriz => diretriz.Id == id);
        }
    }
}