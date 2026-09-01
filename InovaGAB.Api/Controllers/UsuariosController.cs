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

        [HttpPost]
        public async Task<ActionResult<Usuario>> Criar(Usuario usuario)
        {
            await _repository.CriarAsync(usuario);

            return CreatedAtAction(
                nameof(ListarTodos),
                new { id = usuario.Id },
                usuario);
        }
    }
}