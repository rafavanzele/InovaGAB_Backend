namespace InovaGAB.Api.DTOs
{
    public class RelatorioExecutivoDto
    {
        public int TotalIdeias { get; set; }

        public int IdeiasPendentes { get; set; }

        public int IdeiasAprovadas { get; set; }

        public int IdeiasRejeitadas { get; set; }

        public int TotalProjetos { get; set; }

        public int ProjetosEmAndamento { get; set; }

        public int ProjetosConcluidos { get; set; }

        public int TotalEquipes { get; set; }

        public int TotalDiretrizesEstrategicas { get; set; }

        public int TotalIndicadoresEstrategicos { get; set; }

        public int TotalResultadosAlcancados { get; set; }

        public decimal InvestimentoTotalProjetos { get; set; }

        public decimal RetornoFinanceiroTotal { get; set; }

        public decimal LucroObtido { get; set; }

        public decimal? RoiPercentual { get; set; }

        public decimal MediaEngajamentoEquipes { get; set; }

        public List<ResultadoProjetoRelatorioDto> ResultadosPorProjeto { get; set; } = new();

        public List<ResultadoEstrategiaRelatorioDto> ResultadosPorEstrategia { get; set; } = new();

        public DateTime DataGeracao { get; set; }
    }
}