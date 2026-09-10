using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using MongoDB.Bson;

namespace InovaGAB.Api.Services
{
    public class ResultadoAlcancadoService
    {
        private readonly ResultadoAlcancadoRepository _repository;

        public ResultadoAlcancadoService(ResultadoAlcancadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultadoAlcancado> CriarAsync(
            CriarResultadoAlcancadoDto dto)
        {
            var resultado = new ResultadoAlcancado
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Categoria = dto.Categoria,
                ValorAlcancado = dto.ValorAlcancado,
                Unidade = dto.Unidade,
                DataResultado = dto.DataResultado,
                DataRegistro = DateTime.UtcNow
            };

            return await _repository.CriarAsync(resultado);
        }

        public async Task<List<ResultadoAlcancado>> ListarTodosAsync()
        {
            return await _repository.ListarTodosAsync();
        }

        public async Task<ResultadoAlcancado?> BuscarPorIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<ResultadoAlcancado?> AtualizarAsync(
            string id,
            CriarResultadoAlcancadoDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            var resultado = await _repository.BuscarPorIdAsync(id);

            if (resultado == null)
            {
                return null;
            }

            resultado.Titulo = dto.Titulo;
            resultado.Descricao = dto.Descricao;
            resultado.Categoria = dto.Categoria;
            resultado.ValorAlcancado = dto.ValorAlcancado;
            resultado.Unidade = dto.Unidade;
            resultado.DataResultado = dto.DataResultado;

            var atualizado = await _repository.AtualizarAsync(resultado);

            if (!atualizado)
            {
                return null;
            }

            return resultado;
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