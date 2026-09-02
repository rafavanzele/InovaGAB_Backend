namespace InovaGAB.Api.DTOs
{
    public class CriarProjetoDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Responsavel { get; set; } = string.Empty;

        public string Prazo { get; set; } = string.Empty;

        public string Investimento { get; set; } = string.Empty;

        public string RetornoPrevisto { get; set; } = string.Empty;
    }
}