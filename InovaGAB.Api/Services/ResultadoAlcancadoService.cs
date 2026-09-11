using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using MongoDB.Bson;

namespace InovaGAB.Api.Services
{
    public class ResultadoAlcancadoService
    {
        private readonly ResultadoAlcancadoRepository _repository;
        private readonly ProjetoRepository _projetoRepository;

        public ResultadoAlcancadoService(
            ResultadoAlcancadoRepository repository,
            ProjetoRepository projetoRepository)
        {
            _repository = repository;
            _projetoRepository = projetoRepository;
        }

        public async Task<ResultadoAlcancado> CriarAsync(
            CriarResultadoAlcancadoDto dto, string gestorId)
        {
            if (!ObjectId.TryParse(dto.ProjetoId, out _))
            {
                throw new ArgumentException("Projeto inválido.");
            }

            var projeto = await _projetoRepository.BuscarPorIdAsync(dto.ProjetoId);

            if (projeto == null)
            {
                throw new ArgumentException("Projeto não encontrado.");
            }

            if (projeto.GestorId != gestorId)
            {
                throw new UnauthorizedAccessException(
                    "Você não pode registrar resultado em projeto de outro gestor.");
            }

            var resultado = new ResultadoAlcancado
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Categoria = dto.Categoria,
                ProjetoId = dto.ProjetoId,
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

        public async Task<List<ResultadoAlcancado>> ListarPorGestorAsync(string gestorId)
        {
            var projetos = await _projetoRepository.ListarPorGestorAsync(gestorId);

            var projetoIds = projetos
                .Where(projeto => !string.IsNullOrEmpty(projeto.Id))
                .Select(projeto => projeto.Id!)
                .ToList();

            return await _repository.ListarPorProjetosAsync(projetoIds);
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
            CriarResultadoAlcancadoDto dto, string gestorId)
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

            var projetoAtual = await _projetoRepository.BuscarPorIdAsync(resultado.ProjetoId);

            if (projetoAtual == null)
            {
                throw new ArgumentException("Projeto do resultado não encontrado.");
            }

            if (projetoAtual.GestorId != gestorId)
            {
                throw new UnauthorizedAccessException(
                    "Você não pode alterar resultado de projeto de outro gestor."
                );
            }

            if (!ObjectId.TryParse(dto.ProjetoId, out _))
            {
                throw new ArgumentException("Projeto inválido.");
            }

            var projeto = await _projetoRepository.BuscarPorIdAsync(dto.ProjetoId);

            if (projeto == null)
            {
                throw new ArgumentException("Projeto não encontrado.");
            }

            if (projeto.GestorId != gestorId)
            {
                throw new UnauthorizedAccessException(
                    "Você não pode vincular o resultado ao projeto de outro gestor.");
            }

            resultado.Titulo = dto.Titulo;
            resultado.Descricao = dto.Descricao;
            resultado.Categoria = dto.Categoria;
            resultado.ProjetoId = dto.ProjetoId;
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

        public async Task<bool> ExcluirAsync(string id, string gestorId)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return false;
            }

            var resultado = await _repository.BuscarPorIdAsync(id);

            if (resultado == null)
            {
                return false;
            }

            var projeto = await _projetoRepository.BuscarPorIdAsync(resultado.ProjetoId);

            if (projeto == null)
            {
                throw new ArgumentException("Projeto do resultado não encontrado.");
            }

            if (projeto.GestorId != gestorId)
            {
                throw new UnauthorizedAccessException(
                    "Você não pode excluir resultado de projeto de outro gestor.");
            }

            return await _repository.ExcluirAsync(id);
        }
    }
}