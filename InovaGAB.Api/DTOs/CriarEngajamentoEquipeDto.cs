namespace InovaGAB.Api.DTOs
{
    public class CriarEngajamentoEquipeDto
    {
        public string NomeEquipe { get; set; } = string.Empty;

        public int TotalMembros { get; set; }

        public int MembrosAtivos { get; set; }

        public int IdeiasSubmetidas { get; set; }

        public decimal PercentualEngajamento { get; set; }

        public DateTime DataReferencia { get; set; }
    }
}