using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class ResultadoAlcancado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public string ProjetoId { get; set; } = string.Empty;

        public decimal ValorAlcancado { get; set; }

        public string Unidade { get; set; } = string.Empty;

        public DateTime DataResultado { get; set; }

        public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
    }
}