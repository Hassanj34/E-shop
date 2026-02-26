var builder = DistributedApplication.CreateBuilder(args);

// Backing Services
var postrgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postrgres.AddDatabase("catalogdb");


// Projects
var catalog = builder
    .AddProject<Projects.Catalog>("catalog")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);


builder.Build().Run();
