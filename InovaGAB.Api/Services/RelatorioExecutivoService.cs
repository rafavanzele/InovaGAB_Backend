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

            var resultadosPorProjeto = projetos.Select(projeto =>
                new ResultadoProjetoRelatorioDto
                {
                    ProjetoId = projeto.Id ?? string.Empty,
                    ProjetoTitulo = projeto.Titulo,
                    DiretrizId = projeto.DiretrizId,
                    Status = projeto.Status,
                    Prazo = projeto.Prazo,
                    Investimento = projeto.Investimento,
                    RetornoPrevisto = projeto.RetornoPrevisto,
                    Progresso = projeto.Progresso,

                    Resultados = resultados
                       .Where(resultado => resultado.ProjetoId == projeto.Id)
                       .ToList()
                }
            ).ToList();

            var resultadosPorEstrategia = diretrizes.Select(diretriz =>
            {
                var projetosDaEstrategia = resultadosPorProjeto
                    .Where(projeto => projeto.DiretrizId == diretriz.Id)
                    .ToList();

                return new ResultadoEstrategiaRelatorioDto
                {
                    DiretrizId = diretriz.Id ?? string.Empty,
                    DiretrizTitulo = diretriz.Titulo,
                    Categoria = diretriz.Categoria,
                    Campanha = diretriz.Campanha,
                    TotalProjetos = projetosDaEstrategia.Count,
                    TotalResultados = projetosDaEstrategia.Sum(
                        projeto => projeto.Resultados.Count),
                    Projetos = projetosDaEstrategia
                };
            }).ToList();

            var investimentoTotalProjetos = projetos
                .Sum(p => p.InvestimentoValor ?? 0);

            var retornoFinanceiroTotal = resultados
                .Where(r => r.Unidade == "R$")
                .Sum(r => r.ValorAlcancado);

            var lucroObtido = retornoFinanceiroTotal - investimentoTotalProjetos;

            decimal? roiPercentual = investimentoTotalProjetos > 0
                ? ((retornoFinanceiroTotal - investimentoTotalProjetos)
                    / investimentoTotalProjetos) * 100
                : null;

            return new RelatorioExecutivoDto
            {
                TotalIdeias = ideias.Count,
                IdeiasPendentes = ideias.Count(i => i.Status == "Pendente"),
                IdeiasAprovadas = ideias.Count(i => i.Status == "Aprovada"),
                IdeiasRejeitadas = ideias.Count(i => i.Status == "Rejeitada"),

                TotalProjetos = projetos.Count,
                ProjetosEmAndamento = projetos.Count(p => p.Status == "Iniciado" || p.Status == "Em andamento"),
                ProjetosConcluidos = projetos.Count(p => p.Status == "Concluído"),
                TotalEquipes = equipes.Count,
                TotalDiretrizesEstrategicas = diretrizes.Count,
                TotalIndicadoresEstrategicos = indicadores.Count,
                TotalResultadosAlcancados = resultados.Count,

                InvestimentoTotalProjetos = investimentoTotalProjetos,
                RetornoFinanceiroTotal = retornoFinanceiroTotal,
                LucroObtido = lucroObtido,
                RoiPercentual = roiPercentual,

                MediaEngajamentoEquipes = mediaEngajamento,
                ResultadosPorProjeto = resultadosPorProjeto,
                ResultadosPorEstrategia = resultadosPorEstrategia,
                DataGeracao = DateTime.UtcNow
            };
        }
    }
}