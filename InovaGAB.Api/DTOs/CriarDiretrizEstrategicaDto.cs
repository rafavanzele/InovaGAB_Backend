namespace InovaGAB.Api.DTOs
{
    public class CriarDiretrizEstrategicaDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Objetivo { get; set; } = string.Empty;

        public string Responsavel { get; set; } = string.Empty;

        public string Status { get; set; } = "Ativa";
    }
}