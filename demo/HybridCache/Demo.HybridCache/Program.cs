using Demo.HybridCache;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<Database>();
// builder.Services.AddSingleton<IDistributedCache, ExternalCache>();

// builder.Services.AddHybridCache();

var app = builder.Build();

app.MapEndpointsWithoutCache();
// app.MapEndpointsWithCache();

await app.RunAsync();