using Demo.HybridCache;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<Database>();
// builder.Services.AddSingleton<IDistributedCache, ExternalCache>();

// #pragma warning disable EXTEXP0018
// builder.Services.AddHybridCache();
// #pragma warning restore EXTEXP0018

var app = builder.Build();

app.MapEndpointsWithoutCache();
// app.MapEndpointsWithCache();

await app.RunAsync();