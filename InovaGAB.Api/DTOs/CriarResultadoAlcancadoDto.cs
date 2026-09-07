namespace InovaGAB.Api.DTOs
{
    public class CriarResultadoAlcancadoDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public decimal ValorAlcancado { get; set; }

        public string Unidade { get; set; } = string.Empty;

        public DateTime DataResultado { get; set; }
    }
}