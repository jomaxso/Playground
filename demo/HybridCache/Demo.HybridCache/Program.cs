using Demo.HybridCache;

var builder = WebApplication.CreateBuilder();

// builder.Services.AddHybridCache();
builder.Services.AddSingleton<Database>();

var app = builder.Build();

app.MapEndpointsWithoutCache();
// app.MapEndpointsWithCache();

await app.RunAsync();