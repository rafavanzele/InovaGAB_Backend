using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Lideranca")]
    public class RelatoriosExecutivosController : ControllerBase
    {
        private readonly RelatorioExecutivoService _service;

        public RelatoriosExecutivosController(RelatorioExecutivoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GerarRelatorio()
        {
            var relatorio = await _service.GerarRelatorioAsync();

            return Ok(relatorio);
        }
    }
}