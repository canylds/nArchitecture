using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Core.Application.Pipelines.Caching;

public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheRemovingBehavior<TRequest, TResponse>> _logger;

    public CacheRemovingBehavior(IDistributedCache cache, ILogger<CacheRemovingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.BypassCache)
            return await next();

        TResponse response = await next();

        if (request.CacheGroupKeys != null)
            for (int i = 0; i < request.CacheGroupKeys.Length; i++)
            {
                byte[]? cachedGroup = await _cache.GetAsync(request.CacheGroupKeys[i], cancellationToken);

                if (cachedGroup != null)
                {
                    HashSet<string> keysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cachedGroup))!;

                    foreach (string key in keysInGroup)
                    {
                        await _cache.RemoveAsync(key, cancellationToken);
                        _logger.LogInformation($"Removed Cache -> {key}");
                    }

                    await _cache.RemoveAsync(request.CacheGroupKeys[i], cancellationToken);
                    _logger.LogInformation($"Removed Cache -> {request.CacheGroupKeys}");

                    await _cache.RemoveAsync(key: $"{request.CacheGroupKeys}SlidingExpiration", cancellationToken);
                    _logger.LogInformation($"Removed Cache -> {request.CacheGroupKeys}SlidingExpiration");
                }
            }

        if (request.CacheKey != null)
        {
            await _cache.RemoveAsync(request.CacheKey, cancellationToken);
            _logger.LogInformation($"Removed Cache -> {request.CacheKey}");
        }

        return response;
    }
}
