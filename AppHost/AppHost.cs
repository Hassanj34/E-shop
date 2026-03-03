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

var rabbitmq = builder
    .AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder
    .AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);


// Projects
var catalog = builder
    .AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WaitFor(catalogDb)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

var basket = builder
    .AddProject<Projects.Basket>("basket")
    .WithReference(redisCache)
    .WithReference(catalog)
    .WaitFor(redisCache)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithReference(keycloak)
    .WaitFor(keycloak);


builder.Build().Run();
