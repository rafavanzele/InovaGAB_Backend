using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;

namespace InovaGAB.Api.Services
{
    public class EquipeService
    {
        private readonly EquipeRepository _repository;

        public EquipeService(EquipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Equipe> CriarAsync(CriarEquipeDto dto)
        {
            var equipe = new Equipe
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Responsavel = dto.Responsavel,
                Membros = dto.Membros,
                ProjetoId = dto.ProjetoId,
                DataCriacao = DateTime.UtcNow
            };

            await _repository.CriarAsync(equipe);

            return equipe;
        }

        public async Task<List<Equipe>> ListarTodasAsync()
        {
            return await _repository.ListarTodasAsync();
        }

        public async Task<Equipe?> BuscarPorIdAsync(string id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<Equipe?> AtualizarAsync(string id, CriarEquipeDto dto)
        {
            var equipe = await _repository.BuscarPorIdAsync(id);

            if (equipe == null)
            {
                return null;
            }

            equipe.Nome = dto.Nome;
            equipe.Descricao = dto.Descricao;
            equipe.Responsavel = dto.Responsavel;
            equipe.Membros = dto.Membros;
            equipe.ProjetoId = dto.ProjetoId;

            await _repository.AtualizarAsync(id, equipe);

            return equipe;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var equipe = await _repository.BuscarPorIdAsync(id);

            if (equipe == null)
            {
                return false;
            }

            await _repository.ExcluirAsync(id);

            return true;
        }
    }
}