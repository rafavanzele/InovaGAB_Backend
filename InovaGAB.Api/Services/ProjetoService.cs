using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using InovaGAB.Api.DTOs;
using MongoDB.Bson;

namespace InovaGAB.Api.Services
{
    public class ProjetoService
    {
        private readonly ProjetoRepository _repository;
        private readonly DiretrizEstrategicaRepository _diretrizRepository;

        public ProjetoService(
            ProjetoRepository repository,
            DiretrizEstrategicaRepository diretrizRepository)
        {
            _repository = repository;
            _diretrizRepository = diretrizRepository;
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
            if (!ObjectId.TryParse(dto.DiretrizId, out _))
            {
                throw new ArgumentException("Diretriz estratégica inválida.");
            }

            var diretriz = await _diretrizRepository.BuscarPorIdAsync(dto.DiretrizId);

            if (diretriz == null)
            {
                throw new ArgumentException("Diretriz estratégica não encontrada.");
            }

            if (diretriz.Status != "Ativa")
            {
                throw new ArgumentException(
                    "A diretriz estratégica informada não está ativa."
                );
            }

            var projeto = new Projeto
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DiretrizId = dto.DiretrizId,
                Responsavel = dto.Responsavel,
                Prazo = dto.Prazo,
                Investimento = dto.Investimento,
                InvestimentoValor = dto.InvestimentoValor,
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
            projeto.InvestimentoValor = dto.InvestimentoValor;
            projeto.RetornoPrevisto = dto.RetornoPrevisto;
            projeto.Status = dto.Status;
            projeto.Progresso = dto.Progresso;

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