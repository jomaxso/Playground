using Demo.HybridCache;
using Microsoft.Extensions.Caching.Hybrid;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder()
    .AddServiceDefaults();

builder.Services.AddOpenApi();

builder.AddRedisDistributedCache("redis");
builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        // Expiration = TimeSpan.FromSeconds(20),
        // LocalCacheExpiration = TimeSpan.FromSeconds(0),
        // Flags = HybridCacheEntryFlags.
    };
});

builder.Services.AddSingleton<Database>();

var app = builder.Build()
    .MapDefaultEndpoints();

app.MapOpenApi();
app.MapScalarApiReference();

//app.MapEndpointsWithoutCache();
app.MapEndpointsWithCache();

await app.RunAsync();