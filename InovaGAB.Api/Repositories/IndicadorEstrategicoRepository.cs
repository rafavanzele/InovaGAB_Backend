using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class IndicadorEstrategicoRepository
    {
        private readonly IMongoCollection<IndicadorEstrategico> _indicadores;

        public IndicadorEstrategicoRepository(MongoDbContext context)
        {
            _indicadores = context.IndicadoresEstrategicos;
        }

        public async Task<List<IndicadorEstrategico>> ListarTodosAsync()
        {
            return await _indicadores.Find(_ => true).ToListAsync();
        }

        public async Task<IndicadorEstrategico?> BuscarPorIdAsync(string id)
        {
            return await _indicadores
                .Find(indicador => indicador.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CriarAsync(IndicadorEstrategico indicador)
        {
            await _indicadores.InsertOneAsync(indicador);
        }

        public async Task<bool> AtualizarAsync(IndicadorEstrategico indicador)
        {
            var resultado = await _indicadores.ReplaceOneAsync(
                item => item.Id == indicador.Id,
                indicador);

            return resultado.MatchedCount > 0;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var resultado = await _indicadores.DeleteOneAsync(
                indicador => indicador.Id == id);

            return resultado.DeletedCount > 0;
        }
    }
}