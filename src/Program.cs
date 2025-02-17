using ExemploDeArquiteturaLimpa.Application;
using ExemploDeArquiteturaLimpa.Infrastructure;
using ExemploDeArquiteturaLimpa;

var builder = WebApplication.CreateBuilder(args);

builder.Add();
builder.AddApplication();
builder.AddInfrastructure();

var app = builder.Build();

app.Configure();
app.ConfigureApplication();

app.Run();