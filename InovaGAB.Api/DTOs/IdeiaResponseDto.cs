namespace InovaGAB.Api.DTOs
{
    public class IdeiaResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string AutorId { get; set; } = string.Empty;
        public string AutorNome { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}