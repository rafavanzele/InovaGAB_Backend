using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public UsuariosController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListarTodos()
        {
            var usuarios = await _repository.ListarTodosAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarPorId(string id)
        {
            var usuario = await _repository.BuscarPorIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Criar(Usuario usuario)
        {
            await _repository.CriarAsync(usuario);

            return CreatedAtAction(
                nameof(ListarTodos),
                new { id = usuario.Id },
                usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, Usuario usuario)
        {
            var usuarioExistente = await _repository.BuscarPorIdAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            usuario.Id = id;

            await _repository.AtualizarAsync(id, usuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var usuarioExistente = await _repository.BuscarPorIdAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            await _repository.ExcluirAsync(id);

            return NoContent();
        }
    }
}