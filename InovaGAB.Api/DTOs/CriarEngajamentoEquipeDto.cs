using System.ComponentModel.DataAnnotations;

namespace InovaGAB.Api.DTOs
{
    public class CriarEngajamentoEquipeDto
    {
        [Required(ErrorMessage = "O nome da equipe é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "O nome da equipe deve ter entre 3 e 100 caracteres.")]
        public string NomeEquipe { get; set; } = string.Empty;

        [Range(1, int.MaxValue,
            ErrorMessage = "O total de membros deve ser maior que zero.")]
        public int TotalMembros { get; set; }

        [Range(0, int.MaxValue,
            ErrorMessage = "O número de membros ativos não pode ser negativo.")]
        public int MembrosAtivos { get; set; }

        [Range(0, int.MaxValue,
            ErrorMessage = "O número de ideias submetidas não pode ser negativo.")]
        public int IdeiasSubmetidas { get; set; }

        [Range(0, 100,
            ErrorMessage = "O percentual de engajamento deve estar entre 0 e 100.")]
        public decimal PercentualEngajamento { get; set; }

        public DateTime DataReferencia { get; set; }
    }
}