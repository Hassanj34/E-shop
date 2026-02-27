var builder = DistributedApplication.CreateBuilder(args);

// Backing Services
var postrgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postrgres.AddDatabase("catalogdb");

var redisCache = builder
    .AddRedis("redisCache")
    .WithRedisInsight()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);


// Projects
var catalog = builder
    .AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

var basket = builder
    .AddProject<Projects.Basket>("basket")
    .WithReference(redisCache)
    .WaitFor(redisCache);


builder.Build().Run();
