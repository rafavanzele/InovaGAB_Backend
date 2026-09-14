using InovaGAB.Api.Data;
using InovaGAB.Api.Models;
using MongoDB.Driver;

namespace InovaGAB.Api.Repositories
{
    public class AuditLogRepository
    {
        private readonly IMongoCollection<AuditLog> _auditLogs;

        public AuditLogRepository(MongoDbContext context)
        {
            _auditLogs = context.AuditLogs;
        }

        public async Task CreateAsync(AuditLog auditLog)
        {
            await _auditLogs.InsertOneAsync(auditLog);
        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _auditLogs
                .Find(_ => true)
                .SortByDescending(auditLog => auditLog.DataHoraUtc)
                .ToListAsync();
        }
    }
}