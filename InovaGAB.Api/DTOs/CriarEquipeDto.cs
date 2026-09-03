namespace InovaGAB.Api.DTOs
{
    public class CriarEquipeDto
    {
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Responsavel { get; set; } = string.Empty;

        public List<string> Membros { get; set; } = new();

        public string? ProjetoId { get; set; }
    }
}