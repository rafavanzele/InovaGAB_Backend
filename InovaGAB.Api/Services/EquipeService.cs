using InovaGAB.Api.DTOs;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;

namespace InovaGAB.Api.Services
{
    public class EquipeService
    {
        private readonly EquipeRepository _repository;

        private readonly ProjetoRepository _projetoRepository;

        public EquipeService(
            EquipeRepository repository,
            ProjetoRepository projetoRepository)
        {
            _repository = repository;
            _projetoRepository = projetoRepository;
        }

        public async Task<Equipe> CriarAsync(CriarEquipeDto dto, string gestorId)
        {
            if (!string.IsNullOrEmpty(dto.ProjetoId))
            {
                var projeto = await _projetoRepository.BuscarPorIdAsync(dto.ProjetoId);

                if (projeto == null)
                {
                    throw new ArgumentException("Projeto não encontrado.");
                }

                if (projeto.GestorId != gestorId)
                {
                    throw new UnauthorizedAccessException(
                        "O projeto informado não pertence ao Gestor autenticado.");
                }
            }

            var equipe = new Equipe
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Responsavel = dto.Responsavel,
                Membros = dto.Membros,
                ProjetoId = dto.ProjetoId,
                GestorId = gestorId,
                DataCriacao = DateTime.UtcNow
            };

            await _repository.CriarAsync(equipe);

            return equipe;
        }

        public async Task<List<Equipe>> ListarTodasAsync()
        {
            return await _repository.ListarTodasAsync();
        }

        public async Task<List<Equipe>> ListarPorGestorAsync(string gestorId)
        {
            return await _repository.ListarPorGestorAsync(gestorId);
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