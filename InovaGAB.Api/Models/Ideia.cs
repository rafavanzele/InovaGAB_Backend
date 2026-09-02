using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class Ideia
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public string AutorId { get; set; } = string.Empty;

        public string AutorNome { get; set; } = string.Empty;

        public string Status { get; set; } = "Pendente";

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}