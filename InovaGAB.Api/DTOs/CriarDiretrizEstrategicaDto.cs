using System.ComponentModel.DataAnnotations;

namespace InovaGAB.Api.DTOs
{
    public class CriarDiretrizEstrategicaDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O objetivo é obrigatório.")]
        [StringLength(300, MinimumLength = 5,
            ErrorMessage = "O objetivo deve ter entre 5 e 300 caracteres.")]
        public string Objetivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O responsável é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O responsável deve ter entre 3 e 100 caracteres.")]
        public string Responsavel { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "A categoria deve ter entre 3 e 100 caracteres.")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "A campanha é obrigatória.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "A campanha deve ter entre 3 e 100 caracteres.")]
        public string Campanha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = "Ativa";
    }
}