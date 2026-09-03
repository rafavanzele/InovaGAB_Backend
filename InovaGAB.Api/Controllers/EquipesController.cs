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