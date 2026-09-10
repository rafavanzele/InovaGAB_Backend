using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using InovaGAB.Api.DTOs;
using InovaGAB.Api.Services;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repository;
        private readonly TokenService _tokenService;

        public UsuariosController(
            UsuarioRepository repository,
            TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        [Authorize(Roles = "Lideranca")]
        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseDto>>> ListarTodos()
        {
            var usuarios = await _repository.ListarTodosAsync();

            var response = usuarios.Select(usuario => new UsuarioResponseDto
            {
                Id = usuario.Id!,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil
            }).ToList();

            return Ok(response);
        }

        [Authorize(Roles = "Lideranca")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> BuscarPorId(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            var usuario = await _repository.BuscarPorIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var response = new UsuarioResponseDto
            {
                Id = usuario.Id!,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil
            };

            return Ok(response);
        }

        [Authorize(Roles = "Lideranca")]
        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> Criar(CriarUsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Perfil = dto.Perfil
            };

            await _repository.CriarAsync(usuario);

            var response = new UsuarioResponseDto
            {
                Id = usuario.Id!,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil
            };

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = usuario.Id },
                response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto)
        {
            var usuario = await _repository.BuscarPorEmailAsync(dto.Email);

            if (usuario == null)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha);

            if (!senhaValida)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var token = _tokenService.GerarToken(usuario);

            var response = new LoginResponseDto
            {
                Id = usuario.Id!,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Token = token
            };

            return Ok(response);
        }

        [Authorize(Roles = "Lideranca")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, AtualizarUsuarioDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            var usuarioExistente = await _repository.BuscarPorIdAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            usuarioExistente.Nome = dto.Nome;
            usuarioExistente.Email = dto.Email;
            usuarioExistente.Perfil = dto.Perfil;

            await _repository.AtualizarAsync(id, usuarioExistente);

            return NoContent();
        }

        [Authorize(Roles = "Lideranca")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return NotFound();
            }

            var usuarioExistente = await _repository.BuscarPorIdAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            await _repository.ExcluirAsync(id);

            return NoContent();
        }
    }
}