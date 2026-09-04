using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InovaGAB.Api.DTOs;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Lideranca")]
    public class IndicadoresEstrategicosController : ControllerBase
    {
        private readonly IndicadorEstrategicoService _service;

        public IndicadoresEstrategicosController(
            IndicadorEstrategicoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarIndicadorEstrategicoDto dto)
        {
            var indicador = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = indicador.Id },
                indicador);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var indicador = await _service.BuscarPorIdAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            return Ok(indicador);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var indicadores = await _service.ListarTodosAsync();

            return Ok(indicadores);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            string id,
            CriarIndicadorEstrategicoDto dto)
        {
            var indicador = await _service.AtualizarAsync(id, dto);

            if (indicador == null)
            {
                return NotFound();
            }

            return Ok(indicador);
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