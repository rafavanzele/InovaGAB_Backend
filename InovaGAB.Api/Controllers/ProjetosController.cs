using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InovaGAB.Api.DTOs;
using System.Security.Claims;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjetosController : ControllerBase
    {
        private readonly ProjetoService _service;

        public ProjetosController(ProjetoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var projetos = await _service.ListarTodosAsync();

            return Ok(projetos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var projeto = await _service.BuscarPorIdAsync(id);

            if (projeto == null)
            {
                return NotFound(new { mensagem = "Projeto não encontrado." });
            }

            return Ok(projeto);
        }

        [Authorize(Roles = "Gestor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            string id,
            AtualizarProjetoDto dto)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var projetoExistente = await _service.BuscarPorIdAsync(id);

            if (projetoExistente == null)
            {
                return NotFound(new { mensagem = "Projeto não encontrado." });
            }

            if (projetoExistente.GestorId != gestorId)
            {
                return Forbid();
            }

            var projeto = await _service.AtualizarAsync(id, dto);

            if (projeto == null)
            {
                return NotFound(new { mensagem = "Projeto não encontrado." });
            }

            return Ok(projeto);
        }

        [Authorize(Roles = "Gestor")]
        [HttpPost]
        public async Task<IActionResult> Criar(CriarProjetoDto dto)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var projeto = await _service.CriarAsync(dto, gestorId);

            return CreatedAtAction(
                nameof(ListarTodos),
                new { id = projeto.Id },
                projeto);
        }

        [Authorize(Roles = "Gestor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var projetoExistente = await _service.BuscarPorIdAsync(id);

            if (projetoExistente == null)
            {
                return NotFound(new { mensagem = "Projeto não encontrado." });
            }

            if (projetoExistente.GestorId != gestorId)
            {
                return Forbid();
            }

            var excluido = await _service.ExcluirAsync(id);

            if (!excluido)
            {
                return NotFound(new { mensagem = "Projeto não encontrado." });
            }

            return NoContent();
        }
    }
}