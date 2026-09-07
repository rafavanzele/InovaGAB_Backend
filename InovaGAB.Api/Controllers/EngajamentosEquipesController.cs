using InovaGAB.Api.DTOs;
using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Lideranca")]
    public class EngajamentosEquipesController : ControllerBase
    {
        private readonly EngajamentoEquipeService _service;

        public EngajamentosEquipesController(EngajamentoEquipeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarEngajamentoEquipeDto dto)
        {
            var engajamento = await _service.CriarAsync(dto);

            return Ok(engajamento);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var engajamentos = await _service.ListarTodosAsync();

            return Ok(engajamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var engajamento = await _service.BuscarPorIdAsync(id);

            if (engajamento == null)
            {
                return NotFound();
            }

            return Ok(engajamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var excluido = await _service.ExcluirAsync(id);

            if (!excluido)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}