using InovaGAB.Api.DTOs;
using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Lideranca")]
    public class DiretrizesEstrategicasController : ControllerBase
    {
        private readonly DiretrizEstrategicaService _service;

        public DiretrizesEstrategicasController(DiretrizEstrategicaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarDiretrizEstrategicaDto dto)
        {
            var diretriz = await _service.CriarAsync(dto);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = diretriz.Id },
                diretriz);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var diretrizes = await _service.ListarTodasAsync();

            return Ok(diretrizes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(string id)
        {
            var diretriz = await _service.BuscarPorIdAsync(id);

            if (diretriz == null)
            {
                return NotFound(new { mensagem = "Diretriz estratégica não encontrada." });
            }

            return Ok(diretriz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            string id,
            CriarDiretrizEstrategicaDto dto)
        {
            var diretriz = await _service.AtualizarAsync(id, dto);

            if (diretriz == null)
            {
                return NotFound(new { mensagem = "Diretriz estratégica não encontrada." });
            }

            return Ok(diretriz);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var excluiu = await _service.ExcluirAsync(id);

            if (!excluiu)
            {
                return NotFound(new { mensagem = "Diretriz estratégica não encontrada." });
            }

            return NoContent();
        }
    }
}