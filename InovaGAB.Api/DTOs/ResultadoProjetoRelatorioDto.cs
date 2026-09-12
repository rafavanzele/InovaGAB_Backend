using InovaGAB.Api.Models;

namespace InovaGAB.Api.DTOs
{
    public class ResultadoProjetoRelatorioDto
    {
        public string ProjetoId { get; set; } = string.Empty;

        public string ProjetoTitulo { get; set; } = string.Empty;

        public string DiretrizId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string Prazo { get; set; } = string.Empty;

        public string Investimento { get; set; } = string.Empty;

        public string RetornoPrevisto { get; set; } = string.Empty;

        public float Progresso { get; set; }

        public List<ResultadoAlcancado> Resultados { get; set; } = new();
    }
}