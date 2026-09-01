using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class UsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioRepository(MongoDbContext context)
        {
            _usuarios = context.Usuarios;
        }

        public async Task<List<Usuario>> ListarTodosAsync()
        {
            return await _usuarios.Find(_ => true).ToListAsync();
        }

        public async Task CriarAsync(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
        }
    }
}