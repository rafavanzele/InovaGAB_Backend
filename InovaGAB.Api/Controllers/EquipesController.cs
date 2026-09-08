using InovaGAB.Api.DTOs;
using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EquipesController : ControllerBase
    {
        private readonly EquipeService _service;

        public EquipesController(EquipeService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Gestor")]
        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var equipes = await _service.ListarPorGestorAsync(gestorId);

            return Ok(equipes);
        }

        [Authorize(Roles = "Gestor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var equipe = await _service.BuscarPorIdAsync(id);

            if (equipe == null)
            {
                return NotFound(new { mensagem = "Equipe não encontrada." });
            }

            if (equipe.GestorId != gestorId)
            {
                return Forbid();
            }

            return Ok(equipe);
        }

        [Authorize(Roles = "Gestor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, CriarEquipeDto dto)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            var equipeExistente = await _service.BuscarPorIdAsync(id);

            if (equipeExistente == null)
            {
                return NotFound(new { mensagem = "Equipe não encontrada." });
            }

            if (equipeExistente.GestorId != gestorId)
            {
                return Forbid();
            }

            try
            {
                var equipe = await _service.AtualizarAsync(id, dto, gestorId);

                return Ok(equipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
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

            var equipeExistente = await _service.BuscarPorIdAsync(id);

            if (equipeExistente == null)
            {
                return NotFound(new { mensagem = "Equipe não encontrada." });
            }

            if (equipeExistente.GestorId != gestorId)
            {
                return Forbid();
            }

            var excluiu = await _service.ExcluirAsync(id);

            if (!excluiu)
            {
                return NotFound(new { mensagem = "Equipe não encontrada." });
            }

            return NoContent();
        }

        [Authorize(Roles = "Gestor")]
        [HttpPost]
        public async Task<IActionResult> Criar(CriarEquipeDto dto)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            try
            {
                var equipe = await _service.CriarAsync(dto, gestorId);

                return CreatedAtAction(
                    nameof(Criar),
                    new { id = equipe.Id },
                    equipe);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}