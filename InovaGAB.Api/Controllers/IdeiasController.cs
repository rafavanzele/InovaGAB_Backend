using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Mvc;
using InovaGAB.Api.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdeiasController : ControllerBase
    {
        private readonly IdeiaService _service;

        public IdeiasController(IdeiaService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Operador")]
        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var autorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(autorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var ideias = await _service.ListarPorAutorAsync(autorId);

            return Ok(ideias);
        }

        [Authorize(Roles = "Gestor")]
        [HttpGet("pendentes")]
        public async Task<IActionResult> ListarPendentes()
        {
            var ideias = await _service.ListarPendentesAsync();

            return Ok(ideias);
        }

        [Authorize(Roles = "Operador")]
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var autorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(autorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var ideia = await _service.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return NotFound(new { mensagem = "Ideia não encontrada." });
            }

            if (ideia.AutorId != autorId)
            {
                return Forbid();
            }

            return Ok(ideia);
        }

        [Authorize(Roles = "Operador")]
        [HttpPost]
        public async Task<IActionResult> Criar(CriarIdeiaDto dto)
        {
            var autorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var autorNome = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(autorId) || string.IsNullOrEmpty(autorNome))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var ideia = await _service.CriarAsync(
                dto,
                autorId,
                autorNome
            );

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = ideia.Id },
                ideia
            );
        }

        [Authorize(Roles = "Operador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            string id,
            AtualizarIdeiaDto dto)
        {
            var autorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(autorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var ideiaExistente = await _service.BuscarPorIdAsync(id);

            if (ideiaExistente == null)
            {
                return NotFound(new { mensagem = "Ideia não encontrada." });
            }

            if (ideiaExistente.AutorId != autorId)
            {
                return Forbid();
            }

            var ideia = await _service.AtualizarAsync(id, dto);

            if (ideia == null)
            {
                return NotFound(new { mensagem = "Ideia não encontrada." });
            }

            return Ok(ideia);
        }

        [Authorize(Roles = "Gestor")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(
            string id,
            AtualizarStatusIdeiaDto dto)
        {
            try
            {
                var ideia = await _service.AtualizarStatusAsync(id, dto);

                if (ideia == null)
                {
                    return NotFound(new { mensagem = "Ideia não encontrada." });
                }

                return Ok(ideia);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [Authorize(Roles = "Operador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var autorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(autorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var ideiaExistente = await _service.BuscarPorIdAsync(id);

            if (ideiaExistente == null)
            {
                return NotFound(new { mensagem = "Ideia não encontrada." });
            }

            if (ideiaExistente.AutorId != autorId)
            {
                return Forbid();
            }

            var excluido = await _service.ExcluirAsync(id);

            if (!excluido)
            {
                return NotFound(new { mensagem = "Ideia não encontrada." });
            }

            return NoContent();
        }
    }
}