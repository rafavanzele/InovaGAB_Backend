using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class EngajamentoEquipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string NomeEquipe { get; set; } = string.Empty;

        public int TotalMembros { get; set; }

        public int MembrosAtivos { get; set; }

        public int IdeiasSubmetidas { get; set; }

        public decimal PercentualEngajamento { get; set; }

        public DateTime DataReferencia { get; set; }

        public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
    }
}