using InovaGAB.Api.DTOs;
using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Gestor")]
    public class EquipesController : ControllerBase
    {
        private readonly EquipeService _service;

        public EquipesController(EquipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var equipes = await _service.ListarTodasAsync();

            return Ok(equipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var equipe = await _service.BuscarPorIdAsync(id);

            if (equipe == null)
            {
                return NotFound(new { mensagem = "Equipe não encontrada." });
            }

            return Ok(equipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, CriarEquipeDto dto)
        {
            var equipe = await _service.AtualizarAsync(id, dto);

            if (equipe == null)
            {
                return NotFound(new { mensagem = "Equipe não encontrada." });
            }

            return Ok(equipe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var excluiu = await _service.ExcluirAsync(id);

            if (!excluiu)
            {
                return NotFound(new { mensagem = "Equipe não encontrada." });
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarEquipeDto dto)
        {
            var equipe = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(Criar),
                new { id = equipe.Id },
                equipe);
        }
    }
}