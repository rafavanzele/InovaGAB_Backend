using System.ComponentModel.DataAnnotations;

namespace InovaGAB.Api.DTOs
{
    public class CriarEquipeDto
    {
        [Required(ErrorMessage = "O nome da equipe é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O nome da equipe deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O responsável é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O responsável deve ter entre 3 e 100 caracteres.")]
        public string Responsavel { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "A equipe deve possuir pelo menos um membro.")]
        public List<string> Membros { get; set; } = new();

        public string? ProjetoId { get; set; }
    }
}