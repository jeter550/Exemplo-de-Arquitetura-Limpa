using MediatR;
using ExemploDeArquiteturaLimpa.Application.Queries.Responses;

namespace ExemploDeArquiteturaLimpa.Application.Queries.Requests
{
    public class ConsultarSaldoQuery : IRequest<SaldoResponse>
    {
        public string IdRequisicao { get; set; }
        public string ContaCorrenteId { get; set; }
    }
}
