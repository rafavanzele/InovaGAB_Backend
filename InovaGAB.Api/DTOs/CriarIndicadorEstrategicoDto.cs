namespace InovaGAB.Api.DTOs
{
    public class CriarIndicadorEstrategicoDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public double ValorAtual { get; set; }

        public double Meta { get; set; }

        public string Unidade { get; set; } = string.Empty;

        public string Status { get; set; } = "Em acompanhamento";
    }
}