using InovaGAB.Api.Repositories;
using InovaGAB.Api.Models;
using InovaGAB.Api.DTOs;
using MongoDB.Bson;

namespace InovaGAB.Api.Services
{
    public class IdeiaService
    {
        private readonly IdeiaRepository _repository;
        private readonly DiretrizEstrategicaRepository _diretrizRepository;

        public IdeiaService(
            IdeiaRepository repository,
            DiretrizEstrategicaRepository diretrizRepository)
        {
            _repository = repository;
            _diretrizRepository = diretrizRepository;
        }

        public async Task<List<Ideia>> ListarTodasAsync()
        {
            return await _repository.ListarTodasAsync();
        }

        public async Task<List<Ideia>> ListarPorAutorAsync(string autorId)
        {
            return await _repository.ListarPorAutorAsync(autorId);
        }

        public async Task<List<Ideia>> ListarPendentesAsync()
        {
            return await _repository.ListarPendentesAsync();
        }

        public async Task<Ideia?> BuscarPorIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task<Ideia> CriarAsync(
            CriarIdeiaDto dto,
            string autorId,
            string autorNome)
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

            var ideia = new Ideia
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Categoria = dto.Categoria,
                DiretrizId = dto.DiretrizId,
                AutorId = autorId,
                AutorNome = autorNome,
                Status = "Pendente",
                DataCriacao = DateTime.UtcNow
            };

            await _repository.CriarAsync(ideia);

            return ideia;
        }

        public async Task<Ideia?> AtualizarAsync(
            string id,
            AtualizarIdeiaDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            var ideia = await _repository.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return null;
            }

            ideia.Titulo = dto.Titulo;
            ideia.Descricao = dto.Descricao;
            ideia.Categoria = dto.Categoria;

            var atualizado = await _repository.AtualizarAsync(id, ideia);

            if (!atualizado)
            {
                return null;
            }

            return ideia;
        }

        public async Task<Ideia?> AtualizarStatusAsync(
            string id,
            AtualizarStatusIdeiaDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            var statusPermitidos = new[] { "Aprovada", "Rejeitada" };

            if (!statusPermitidos.Contains(dto.Status))
            {
                throw new ArgumentException("Status inválido.");
            }

            var ideia = await _repository.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return null;
            }

            if (ideia.Status != "Pendente")
            {
                throw new ArgumentException("A ideia já foi avaliada.");
            }

            ideia.Status = dto.Status;

            var atualizado = await _repository.AtualizarAsync(id, ideia);

            if (!atualizado)
            {
                return null;
            }

            return ideia;
        }

        public async Task<Ideia?> AtualizarPriorizacaoAsync(
            string id,
            AtualizarPriorizacaoIdeiaDto dto)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null;
            }

            var ideia = await _repository.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return null;
            }

            ideia.Priorizada = dto.Priorizada;

            var atualizado = await _repository.AtualizarAsync(id, ideia);

            if (!atualizado)
            {
                return null;
            }

            return ideia;
        }

        public async Task<bool> ExcluirAsync(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return false;
            }

            var ideia = await _repository.BuscarPorIdAsync(id);

            if (ideia == null)
            {
                return false;
            }

            return await _repository.ExcluirAsync(id);
        }
    }
}