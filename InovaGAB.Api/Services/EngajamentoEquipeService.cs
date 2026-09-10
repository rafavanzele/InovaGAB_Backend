using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using MongoDB.Bson;

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
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<EngajamentoEquipe?> AtualizarAsync(
            string id,
            CriarEngajamentoEquipeDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            var engajamento = await _repository.BuscarPorIdAsync(id);

            if (engajamento == null)
            {
                return null;
            }

            engajamento.NomeEquipe = dto.NomeEquipe;
            engajamento.TotalMembros = dto.TotalMembros;
            engajamento.MembrosAtivos = dto.MembrosAtivos;
            engajamento.IdeiasSubmetidas = dto.IdeiasSubmetidas;
            engajamento.PercentualEngajamento = dto.PercentualEngajamento;
            engajamento.DataReferencia = dto.DataReferencia;

            var atualizado = await _repository.AtualizarAsync(engajamento);

            if (!atualizado)
            {
                return null;
            }

            return engajamento;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return false;
            }

            return await _repository.ExcluirAsync(id);
        }
    }
}