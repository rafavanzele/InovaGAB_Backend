using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using InovaGAB.Api.DTOs;
using MongoDB.Bson;

namespace InovaGAB.Api.Services
{
    public class ProjetoService
    {
        private readonly ProjetoRepository _repository;

        public ProjetoService(ProjetoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Projeto>> ListarTodosAsync()
        {
            return await _repository.ListarTodosAsync();
        }

        public async Task<List<Projeto>> ListarPorGestorAsync(string gestorId)
        {
            return await _repository.ListarPorGestorAsync(gestorId);
        }

        public async Task<Projeto?> BuscarPorIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<Projeto> CriarAsync(CriarProjetoDto dto, string gestorId)
        {
            var projeto = new Projeto
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Responsavel = dto.Responsavel,
                Prazo = dto.Prazo,
                Investimento = dto.Investimento,
                RetornoPrevisto = dto.RetornoPrevisto,
                GestorId = gestorId,
                DataCriacao = DateTime.UtcNow,
                Status = "Iniciado",
                Resultado = "Em andamento",
                Progresso = 0
            };

            await _repository.CriarAsync(projeto);

            return projeto;
        }

        public async Task<Projeto?> AtualizarAsync(
            string id,
            AtualizarProjetoDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            var projeto = await _repository.BuscarPorIdAsync(id);

            if (projeto == null)
            {
                return null;
            }

            projeto.Titulo = dto.Titulo;
            projeto.Descricao = dto.Descricao;
            projeto.Responsavel = dto.Responsavel;
            projeto.Prazo = dto.Prazo;
            projeto.Investimento = dto.Investimento;
            projeto.RetornoPrevisto = dto.RetornoPrevisto;

            var atualizado = await _repository.AtualizarAsync(projeto);

            if (!atualizado)
            {
                return null;
            }

            return projeto;
        }

        public async Task<bool> AtualizarAsync(Projeto projeto)
        {
            return await _repository.AtualizarAsync(projeto);
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