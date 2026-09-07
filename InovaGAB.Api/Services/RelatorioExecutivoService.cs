using InovaGAB.Api.DTOs;
using InovaGAB.Api.Repositories;

namespace InovaGAB.Api.Services
{
    public class RelatorioExecutivoService
    {
        private readonly IdeiaRepository _ideiaRepository;
        private readonly ProjetoRepository _projetoRepository;
        private readonly EquipeRepository _equipeRepository;
        private readonly DiretrizEstrategicaRepository _diretrizRepository;
        private readonly IndicadorEstrategicoRepository _indicadorRepository;
        private readonly ResultadoAlcancadoRepository _resultadoRepository;
        private readonly EngajamentoEquipeRepository _engajamentoRepository;

        public RelatorioExecutivoService(
            IdeiaRepository ideiaRepository,
            ProjetoRepository projetoRepository,
            EquipeRepository equipeRepository,
            DiretrizEstrategicaRepository diretrizRepository,
            IndicadorEstrategicoRepository indicadorRepository,
            ResultadoAlcancadoRepository resultadoRepository,
            EngajamentoEquipeRepository engajamentoRepository)
        {
            _ideiaRepository = ideiaRepository;
            _projetoRepository = projetoRepository;
            _equipeRepository = equipeRepository;
            _diretrizRepository = diretrizRepository;
            _indicadorRepository = indicadorRepository;
            _resultadoRepository = resultadoRepository;
            _engajamentoRepository = engajamentoRepository;
        }

        public async Task<RelatorioExecutivoDto> GerarRelatorioAsync()
        {
            var ideias = await _ideiaRepository.ListarTodasAsync();
            var projetos = await _projetoRepository.ListarTodosAsync();
            var equipes = await _equipeRepository.ListarTodasAsync();
            var diretrizes = await _diretrizRepository.ListarTodasAsync();
            var indicadores = await _indicadorRepository.ListarTodosAsync();
            var resultados = await _resultadoRepository.ListarTodosAsync();
            var engajamentos = await _engajamentoRepository.ListarTodosAsync();

            var mediaEngajamento = engajamentos.Count > 0
                ? engajamentos.Average(e => e.PercentualEngajamento)
                : 0;

            return new RelatorioExecutivoDto
            {
                TotalIdeias = ideias.Count,
                IdeiasPendentes = ideias.Count(i => i.Status == "Pendente"),
                IdeiasAprovadas = ideias.Count(i => i.Status == "Aprovada"),
                IdeiasRejeitadas = ideias.Count(i => i.Status == "Rejeitada"),

                TotalProjetos = projetos.Count,
                TotalEquipes = equipes.Count,
                TotalDiretrizesEstrategicas = diretrizes.Count,
                TotalIndicadoresEstrategicos = indicadores.Count,
                TotalResultadosAlcancados = resultados.Count,

                MediaEngajamentoEquipes = mediaEngajamento,
                DataGeracao = DateTime.UtcNow
            };
        }
    }
}