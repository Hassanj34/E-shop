
using Basket.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.AddRedisDistributedCache(connectionName: "redisCache");
builder.Services.AddScoped<BasketService>();

builder.Services.AddHttpClient<CatalogApiClient>(client =>
{
    client.BaseAddress = new("https+http://catalog");
});

builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("redisCache");
    return ConnectionMultiplexer.Connect(connectionString!);
});

builder.Services.AddScoped<RedisKeyFetcher>();

builder.Services.AddAuthentication()
    .AddKeycloakJwtBearer(
        serviceName: "keycloak",
        realm: "eshop",
        configureOptions: options =>
        {
            options.Authority = "http://localhost:8080/realms/eshop";
            options.Audience = "eshop-client";
            options.RequireHttpsMetadata = false;
        });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapBasketEndpoints();
app.UseHttpsRedirection();

app.Run();