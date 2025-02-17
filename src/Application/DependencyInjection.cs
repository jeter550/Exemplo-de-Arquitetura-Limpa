using MediatR;
using ExemploDeArquiteturaLimpa.Application.Commands.Requests;
using ExemploDeArquiteturaLimpa.Application.Handlers;
using ExemploDeArquiteturaLimpa.Application.Queries.Requests;
using ExemploDeArquiteturaLimpa.Application.Queries.Responses;
using ExemploDeArquiteturaLimpa.Infrastructure.Database.Sqlite;

namespace ExemploDeArquiteturaLimpa.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this WebApplicationBuilder builder)
        {
            //Commands
            builder.Services.AddScoped<IRequest<bool>, CriarContaCorrenteCommand>();   
            builder.Services.AddScoped<IRequest<string>, CriarMovimentoCommand>();
            
            //Queries
            builder.Services.AddScoped<IRequest<SaldoResponse>, ConsultarSaldoQuery>();

            //Handlers
            builder.Services.AddScoped<IRequestHandler<ConsultarSaldoQuery, SaldoResponse>, ConsultarSaldoHandler>();
            builder.Services.AddScoped<IRequestHandler<CriarMovimentoCommand, string>, CriarMovimentoHandler>();
        }

        public static void ConfigureApplication(this WebApplication app)
        {
            // sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            
// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html
        }
    }
}
