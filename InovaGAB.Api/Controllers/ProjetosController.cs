using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InovaGAB.Api.DTOs;
using System.Security.Claims;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Gestor")]
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
    }
}