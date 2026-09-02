using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class Projeto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Responsavel { get; set; } = string.Empty;

        public string Status { get; set; } = "Iniciado";

        public string Prazo { get; set; } = string.Empty;

        public string Investimento { get; set; } = string.Empty;

        public string RetornoPrevisto { get; set; } = string.Empty;

        public string Resultado { get; set; } = "Em andamento";

        public float Progresso { get; set; } = 0;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [BsonRepresentation(BsonType.ObjectId)]
        public string GestorId { get; set; } = string.Empty;
    }
}