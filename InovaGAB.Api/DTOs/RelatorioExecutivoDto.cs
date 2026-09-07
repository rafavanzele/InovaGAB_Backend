namespace InovaGAB.Api.DTOs
{
    public class RelatorioExecutivoDto
    {
        public int TotalIdeias { get; set; }

        public int IdeiasPendentes { get; set; }

        public int IdeiasAprovadas { get; set; }

        public int IdeiasRejeitadas { get; set; }

        public int TotalProjetos { get; set; }

        public int TotalEquipes { get; set; }

        public int TotalDiretrizesEstrategicas { get; set; }

        public int TotalIndicadoresEstrategicos { get; set; }

        public int TotalResultadosAlcancados { get; set; }

        public decimal MediaEngajamentoEquipes { get; set; }

        public DateTime DataGeracao { get; set; }
    }
}