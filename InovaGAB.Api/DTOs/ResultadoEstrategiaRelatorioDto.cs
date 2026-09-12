namespace InovaGAB.Api.DTOs
{
    public class ResultadoEstrategiaRelatorioDto
    {
        public string DiretrizId { get; set; } = string.Empty;

        public string DiretrizTitulo { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public string Campanha { get; set; } = string.Empty;

        public int TotalProjetos { get; set; }

        public int TotalResultados { get; set; }

        public List<ResultadoProjetoRelatorioDto> Projetos { get; set; } = new();
    }
}