using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InovaGAB.Api.Models
{
    public class AuditLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string MetodoHttp { get; set; } = string.Empty;

        public string Rota { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public long TempoRespostaMs { get; set; }

        public string? UsuarioId { get; set; }

        public string? UsuarioNome { get; set; }

        public string? Perfil { get; set; }

        public DateTime DataHoraUtc { get; set; } = DateTime.UtcNow;
    }
}