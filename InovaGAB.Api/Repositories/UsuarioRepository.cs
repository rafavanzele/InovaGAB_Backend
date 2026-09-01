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

        public async Task<Usuario?> BuscarPorIdAsync(string id)
        {
            return await _usuarios.Find(usuario => usuario.Id == id).FirstOrDefaultAsync();
        }

        public async Task CriarAsync(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task AtualizarAsync(string id, Usuario usuario)
        {
            await _usuarios.ReplaceOneAsync(
                usuarioExistente => usuarioExistente.Id == id,
                usuario
            );
        }

        public async Task ExcluirAsync(string id)
        {
            await _usuarios.DeleteOneAsync(usuario => usuario.Id == id);
        }
    }
}