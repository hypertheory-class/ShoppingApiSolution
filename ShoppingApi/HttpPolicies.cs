using Polly;
using Polly.Extensions.Http;

namespace ShoppingApi;

public static class HttpPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetyPolicy()
    {
        return HttpPolicyExtensions
               .HandleTransientHttpError() // any 5xx or 408 (Request Timedout)
               .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
               .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // "Jitter"
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreaker()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .CircuitBreakerAsync(1, TimeSpan.FromSeconds(10));
    }
}
