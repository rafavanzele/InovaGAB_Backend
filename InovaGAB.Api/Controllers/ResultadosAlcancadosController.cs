using InovaGAB.Api.DTOs;
using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Lideranca")]
    public class ResultadosAlcancadosController : ControllerBase
    {
        private readonly ResultadoAlcancadoService _service;

        public ResultadosAlcancadosController(ResultadoAlcancadoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarResultadoAlcancadoDto dto)
        {
            var resultado = await _service.CriarAsync(dto);

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var resultados = await _service.ListarTodosAsync();

            return Ok(resultados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var resultado = await _service.BuscarPorIdAsync(id);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            string id,
            CriarResultadoAlcancadoDto dto)
        {
            var resultado = await _service.AtualizarAsync(id, dto);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
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