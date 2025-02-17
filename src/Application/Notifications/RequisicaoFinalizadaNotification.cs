using MediatR;
using System.Text.Json;

namespace ExemploDeArquiteturaLimpa.Application.Events
{
    public class RequisicaoFinalizadaNotification: INotification
    {
        public string Idempotencia { get; private set; }
        public string Requisicao { get; private set; }
        public string Resposta { get; private set; }
        public RequisicaoFinalizadaNotification(string idempotencia, object requisicao, object resposta)
        {
            Idempotencia = idempotencia;
            Requisicao = JsonSerializer.Serialize(requisicao);
            Resposta = JsonSerializer.Serialize(resposta);
        }
    }
}
