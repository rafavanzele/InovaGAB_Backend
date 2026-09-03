using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class DiretrizEstrategica
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Objetivo { get; set; } = string.Empty;

        public string Responsavel { get; set; } = string.Empty;

        public string Status { get; set; } = "Ativa";

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}