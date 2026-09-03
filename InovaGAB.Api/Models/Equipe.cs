using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class Equipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Responsavel { get; set; } = string.Empty;

        public List<string> Membros { get; set; } = new();

        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProjetoId { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}