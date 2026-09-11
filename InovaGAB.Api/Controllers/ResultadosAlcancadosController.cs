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
    public class ResultadosAlcancadosController : ControllerBase
    {
        private readonly ResultadoAlcancadoService _service;

        public ResultadosAlcancadosController(ResultadoAlcancadoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Gestor")]
        [HttpPost]
        public async Task<IActionResult> Criar(CriarResultadoAlcancadoDto dto)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            try
            {
                var resultado = await _service.CriarAsync(dto, gestorId);

                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new { mensagem = ex.Message }
                );
            }
        }

        [Authorize(Roles = "Gestor,Lideranca")]
        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var perfil = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(usuarioId) || string.IsNullOrEmpty(perfil))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            if (perfil == "Lideranca")
            {
                var todosResultados = await _service.ListarTodosAsync();
                return Ok(todosResultados);
            }

            var resultadosDoGestor = await _service.ListarPorGestorAsync(usuarioId);

            return Ok(resultadosDoGestor);
        }

        [Authorize(Roles = "Gestor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            string id,
            CriarResultadoAlcancadoDto dto)
        {
            var gestorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(gestorId))
            {
                return Unauthorized(new { mensagem = "Usuário não autenticado." });
            }

            try
            {
                var resultado = await _service.AtualizarAsync(id, dto, gestorId);

                if (resultado == null)
                {
                    return NotFound();
                }

                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new { mensagem = ex.Message }
                );
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

            try
            {
                var excluido = await _service.ExcluirAsync(id, gestorId);

                if (!excluido)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new { mensagem = ex.Message }
                );
            }
        }
    }
}