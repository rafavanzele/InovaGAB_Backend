using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class ProjetoRepository
    {
        private readonly IMongoCollection<Projeto> _projetos;

        public ProjetoRepository(MongoDbContext context)
        {
            _projetos = context.Projetos;
        }

        public async Task<List<Projeto>> ListarTodosAsync()
        {
            return await _projetos
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<Projeto?> BuscarPorIdAsync(string id)
        {
            return await _projetos
                .Find(projeto => projeto.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CriarAsync(Projeto projeto)
        {
            await _projetos.InsertOneAsync(projeto);
        }

        public async Task<bool> AtualizarAsync(Projeto projeto)
        {
            var resultado = await _projetos.ReplaceOneAsync(
                p => p.Id == projeto.Id,
                projeto);

            return resultado.ModifiedCount > 0;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var resultado = await _projetos.DeleteOneAsync(
                projeto => projeto.Id == id);

            return resultado.DeletedCount > 0;
        }
    }
}