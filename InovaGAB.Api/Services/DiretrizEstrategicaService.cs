using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;

namespace InovaGAB.Api.Services
{
    public class DiretrizEstrategicaService
    {
        private readonly DiretrizEstrategicaRepository _repository;

        public DiretrizEstrategicaService(DiretrizEstrategicaRepository repository)
        {
            _repository = repository;
        }

        public async Task<DiretrizEstrategica> CriarAsync(CriarDiretrizEstrategicaDto dto)
        {
            var diretriz = new DiretrizEstrategica
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Objetivo = dto.Objetivo,
                Responsavel = dto.Responsavel,
                Status = dto.Status,
                DataCriacao = DateTime.UtcNow
            };

            await _repository.CriarAsync(diretriz);

            return diretriz;
        }

        public async Task<List<DiretrizEstrategica>> ListarTodasAsync()
        {
            return await _repository.ListarTodasAsync();
        }

        public async Task<DiretrizEstrategica?> BuscarPorIdAsync(string id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<DiretrizEstrategica?> AtualizarAsync(
            string id,
            CriarDiretrizEstrategicaDto dto)
        {
            var diretriz = await _repository.BuscarPorIdAsync(id);

            if (diretriz == null)
            {
                return null;
            }

            diretriz.Titulo = dto.Titulo;
            diretriz.Descricao = dto.Descricao;
            diretriz.Objetivo = dto.Objetivo;
            diretriz.Responsavel = dto.Responsavel;
            diretriz.Status = dto.Status;

            await _repository.AtualizarAsync(id, diretriz);

            return diretriz;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            var diretriz = await _repository.BuscarPorIdAsync(id);

            if (diretriz == null)
            {
                return false;
            }

            await _repository.ExcluirAsync(id);

            return true;
        }
    }
}