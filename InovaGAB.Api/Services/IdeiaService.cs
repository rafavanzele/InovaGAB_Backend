using InovaGAB.Api.Repositories;
using InovaGAB.Api.Models;
using InovaGAB.Api.DTOs;

namespace InovaGAB.Api.Services
{
    public class IdeiaService
    {
        private readonly IdeiaRepository _repository;

        public IdeiaService(IdeiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Ideia>> ListarTodasAsync()
        {
            return await _repository.ListarTodasAsync();
        }

        public async Task<List<Ideia>> ListarPendentesAsync()
        {
            return await _repository.ListarPendentesAsync();
        }

        public async Task<Ideia?> BuscarPorIdAsync(string id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<Ideia> CriarAsync(
            CriarIdeiaDto dto,
            string autorId,
            string autorNome)
        {
            var ideia = new Ideia
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Categoria = dto.Categoria,
                AutorId = autorId,
                AutorNome = autorNome,
                Status = "Pendente",
                DataCriacao = DateTime.UtcNow
            };

            await _repository.CriarAsync(ideia);

            return ideia;
        }

        public async Task<Ideia?> AtualizarAsync(
            string id,
            AtualizarIdeiaDto dto)
        {
            var ideia = await _repository.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return null;
            }

            ideia.Titulo = dto.Titulo;
            ideia.Descricao = dto.Descricao;
            ideia.Categoria = dto.Categoria;

            var atualizado = await _repository.AtualizarAsync(id, ideia);

            if (!atualizado)
            {
                return null;
            }

            return ideia;
        }

        public async Task<Ideia?> AtualizarStatusAsync(
            string id,
            AtualizarStatusIdeiaDto dto)
        {
            var statusPermitidos = new[] { "Aprovada", "Rejeitada" };

            if (!statusPermitidos.Contains(dto.Status))
            {
                throw new ArgumentException("Status inválido.");
            }

            var ideia = await _repository.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return null;
            }

            ideia.Status = dto.Status;

            var atualizado = await _repository.AtualizarAsync(id, ideia);

            if (!atualizado)
            {
                return null;
            }

            return ideia;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var ideia = await _repository.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return false;
            }

            return await _repository.ExcluirAsync(id);
        }
    }
}