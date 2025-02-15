var builder = WebApplication.CreateBuilder(args);

// add services to the container   
builder.Services.AddCarter(); // Add Carter to the services collection
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP request pipeline

app.MapCarter(); // Add Carter to the HTTP request pipeline
app.Run();
