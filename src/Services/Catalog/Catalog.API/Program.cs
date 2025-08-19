using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. "Inject our dependencies"

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("CatalogDB")!);
}).UseLightweightSessions();

//If dev, seed database
if(builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("CatalogDB")!);

var app = builder.Build();

// Configure the HTTP request pipeline.

//By using Carter, we can define our endpoints in separate modules and use just one line to register them.
app.MapCarter();

app.UseExceptionHandler(options => { });
app.MapHealthChecks("/health", new HealthCheckOptions
{
    //Formats in JSON
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
