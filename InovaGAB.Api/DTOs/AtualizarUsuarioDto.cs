using System.ComponentModel.DataAnnotations;

namespace InovaGAB.Api.DTOs
{
    public class AtualizarUsuarioDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O perfil é obrigatório.")]
        [RegularExpression(
            "^(Operador|Gestor|Lideranca)$",
            ErrorMessage = "O perfil deve ser Operador, Gestor ou Lideranca."
)]
        public string Perfil { get; set; } = string.Empty;
    }
}