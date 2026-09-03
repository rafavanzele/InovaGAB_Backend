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
    }
}