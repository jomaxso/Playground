var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis")
    .WithRedisInsight();

builder.AddProject<Projects.Demo_HybridCache>("demo-api")
    .WithReference(redis).WaitFor(redis);

builder.Build().Run();