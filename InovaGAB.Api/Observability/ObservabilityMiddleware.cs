using System.Diagnostics;
using InovaGAB.Api.Models;
using InovaGAB.Api.Repositories;
using System.Security.Claims;

namespace InovaGAB.Api.Observability
{
    public class ObservabilityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ObservabilityMiddleware> _logger;

        public ObservabilityMiddleware(
            RequestDelegate next,
            ILogger<ObservabilityMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ApiMetrics metrics,
            AuditLogRepository auditLogRepository)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                var statusCode = context.Response.StatusCode;

                metrics.RecordRequest(statusCode, elapsedMilliseconds);

                var auditLog = new AuditLog
                {
                    MetodoHttp = context.Request.Method,
                    Rota = context.Request.Path,
                    StatusCode = statusCode,
                    TempoRespostaMs = elapsedMilliseconds,
                    UsuarioId = context.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    UsuarioNome = context.User.FindFirstValue(ClaimTypes.Name),
                    Perfil = context.User.FindFirstValue(ClaimTypes.Role),
                    DataHoraUtc = DateTime.UtcNow
                };

                await auditLogRepository.CreateAsync(auditLog);

                _logger.LogInformation(
                    "HTTP {Method} {Path} respondeu {StatusCode} em {ElapsedMilliseconds} ms",
                    context.Request.Method,
                    context.Request.Path,
                    statusCode,
                    elapsedMilliseconds);
            }
        }
    }
}