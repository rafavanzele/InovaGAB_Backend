using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class ResultadoAlcancadoRepository
    {
        private readonly IMongoCollection<ResultadoAlcancado> _collection;

        public ResultadoAlcancadoRepository(MongoDbContext context)
        {
            _collection = context.ResultadosAlcancados;
        }

        public async Task<ResultadoAlcancado> CriarAsync(ResultadoAlcancado resultado)
        {
            await _collection.InsertOneAsync(resultado);
            return resultado;
        }

        public async Task<List<ResultadoAlcancado>> ListarTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<ResultadoAlcancado>> ListarPorProjetosAsync(
            IEnumerable<string> projetoIds)
        {
            var ids = projetoIds.ToList();

            if (ids.Count == 0)
            {
                return new List<ResultadoAlcancado>();
            }

            var filtro = Builders<ResultadoAlcancado>.Filter.In(
                resultado => resultado.ProjetoId,
                ids
            );

            return await _collection.Find(filtro).ToListAsync();
        }

        public async Task<ResultadoAlcancado?> BuscarPorIdAsync(string id)
        {
            return await _collection
                .Find(resultado => resultado.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AtualizarAsync(ResultadoAlcancado resultado)
        {
            var retorno = await _collection.ReplaceOneAsync(
                item => item.Id == resultado.Id,
                resultado
            );

            return retorno.MatchedCount > 0;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var resultado = await _collection.DeleteOneAsync(
                resultado => resultado.Id == id
            );

            return resultado.DeletedCount > 0;
        }
    }
}