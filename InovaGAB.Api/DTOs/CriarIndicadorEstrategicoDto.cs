using System.ComponentModel.DataAnnotations;

namespace InovaGAB.Api.DTOs
{
    public class CriarIndicadorEstrategicoDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0, double.MaxValue,
            ErrorMessage = "O valor atual não pode ser negativo.")]
        public double ValorAtual { get; set; }

        [Range(0.01, double.MaxValue,
            ErrorMessage = "A meta deve ser maior que zero.")]
        public double Meta { get; set; }

        [Required(ErrorMessage = "A unidade é obrigatória.")]
        [StringLength(50, MinimumLength = 1,
            ErrorMessage = "A unidade deve ter entre 1 e 50 caracteres.")]
        public string Unidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = "Em acompanhamento";
    }
}