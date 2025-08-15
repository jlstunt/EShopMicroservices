var builder = WebApplication.CreateBuilder(args);

// Add services to the container. "Inject our dependencies"
builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("CatalogDB")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.

//By using Carter, we can define our endpoints in separate modules and use just one line to register them.
app.MapCarter();



app.Run();
