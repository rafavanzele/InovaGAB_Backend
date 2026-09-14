namespace InovaGAB.Api.Observability
{
    public class ApiMetrics
    {
        private long _totalRequests;
        private long _successfulRequests;
        private long _failedRequests;
        private long _totalElapsedMilliseconds;

        public void RecordRequest(int statusCode, long elapsedMilliseconds)
        {
            Interlocked.Increment(ref _totalRequests);
            Interlocked.Add(ref _totalElapsedMilliseconds, elapsedMilliseconds);

            if (statusCode >= 200 && statusCode < 400)
            {
                Interlocked.Increment(ref _successfulRequests);
            }
            else
            {
                Interlocked.Increment(ref _failedRequests);
            }
        }

        public object GetSnapshot()
        {
            var totalRequests = Interlocked.Read(ref _totalRequests);
            var totalElapsed = Interlocked.Read(ref _totalElapsedMilliseconds);

            return new
            {
                TotalRequests = totalRequests,
                SuccessfulRequests = Interlocked.Read(ref _successfulRequests),
                FailedRequests = Interlocked.Read(ref _failedRequests),
                AverageResponseTimeMs = totalRequests == 0
                    ? 0
                    : Math.Round((double)totalElapsed / totalRequests, 2)
            };
        }
    }
}