using System.ComponentModel.DataAnnotations;

public class CriarResultadoAlcancadoDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(500, MinimumLength = 10,
        ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "A categoria deve ter entre 3 e 100 caracteres.")]
    public string Categoria { get; set; } = string.Empty;

    [Range(0, double.MaxValue,
        ErrorMessage = "O valor alcançado não pode ser negativo.")]
    public decimal ValorAlcancado { get; set; }

    [Required(ErrorMessage = "A unidade é obrigatória.")]
    [StringLength(50, MinimumLength = 1,
        ErrorMessage = "A unidade deve ter entre 1 e 50 caracteres.")]
    public string Unidade { get; set; } = string.Empty;

    public DateTime DataResultado { get; set; }
}