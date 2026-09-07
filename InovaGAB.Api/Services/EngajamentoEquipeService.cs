using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;

namespace InovaGAB.Api.Services
{
    public class EngajamentoEquipeService
    {
        private readonly EngajamentoEquipeRepository _repository;

        public EngajamentoEquipeService(EngajamentoEquipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<EngajamentoEquipe> CriarAsync(
            CriarEngajamentoEquipeDto dto)
        {
            var engajamento = new EngajamentoEquipe
            {
                NomeEquipe = dto.NomeEquipe,
                TotalMembros = dto.TotalMembros,
                MembrosAtivos = dto.MembrosAtivos,
                IdeiasSubmetidas = dto.IdeiasSubmetidas,
                PercentualEngajamento = dto.PercentualEngajamento,
                DataReferencia = dto.DataReferencia,
                DataRegistro = DateTime.UtcNow
            };

            return await _repository.CriarAsync(engajamento);
        }

        public async Task<List<EngajamentoEquipe>> ListarTodosAsync()
        {
            return await _repository.ListarTodosAsync();
        }

        public async Task<EngajamentoEquipe?> BuscarPorIdAsync(string id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            return await _repository.ExcluirAsync(id);
        }
    }
}