using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;

namespace InovaGAB.Api.Services
{
    public class IndicadorEstrategicoService
    {
        private readonly IndicadorEstrategicoRepository _repository;

        public IndicadorEstrategicoService(IndicadorEstrategicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<IndicadorEstrategico>> ListarTodosAsync()
        {
            return await _repository.ListarTodosAsync();
        }

        public async Task<IndicadorEstrategico?> BuscarPorIdAsync(string id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<IndicadorEstrategico> CriarAsync(CriarIndicadorEstrategicoDto dto)
        {
            var indicador = new IndicadorEstrategico
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                ValorAtual = dto.ValorAtual,
                Meta = dto.Meta,
                Unidade = dto.Unidade,
                Status = dto.Status,
                DataAtualizacao = DateTime.UtcNow
            };

            await _repository.CriarAsync(indicador);

            return indicador;
        }

        public async Task<IndicadorEstrategico?> AtualizarAsync(
            string id,
            CriarIndicadorEstrategicoDto dto)
        {
            var indicador = await _repository.BuscarPorIdAsync(id);

            if (indicador == null)
            {
                return null;
            }

            indicador.Titulo = dto.Titulo;
            indicador.Descricao = dto.Descricao;
            indicador.ValorAtual = dto.ValorAtual;
            indicador.Meta = dto.Meta;
            indicador.Unidade = dto.Unidade;
            indicador.Status = dto.Status;
            indicador.DataAtualizacao = DateTime.UtcNow;

            await _repository.AtualizarAsync(indicador);

            return indicador;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            return await _repository.ExcluirAsync(id);
        }
    }
}