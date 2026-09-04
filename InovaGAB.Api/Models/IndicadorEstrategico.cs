using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class IndicadorEstrategico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public double ValorAtual { get; set; }

        public double Meta { get; set; }

        public string Unidade { get; set; } = string.Empty;

        public string Status { get; set; } = "Em acompanhamento";

        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
    }
}