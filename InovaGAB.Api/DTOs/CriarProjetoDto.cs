using System.ComponentModel.DataAnnotations;

namespace InovaGAB.Api.DTOs
{
    public class CriarProjetoDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O responsável é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O responsável deve ter entre 3 e 100 caracteres.")]
        public string Responsavel { get; set; } = string.Empty;

        [Required(ErrorMessage = "O prazo é obrigatório.")]
        public string Prazo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O investimento é obrigatório.")]
        public string Investimento { get; set; } = string.Empty;

        [Required(ErrorMessage = "O retorno previsto é obrigatório.")]
        public string RetornoPrevisto { get; set; } = string.Empty;
    }
}