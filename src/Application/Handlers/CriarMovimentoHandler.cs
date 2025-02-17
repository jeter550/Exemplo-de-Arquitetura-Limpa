using MediatR;
using ExemploDeArquiteturaLimpa.Application.Commands.Requests;
using ExemploDeArquiteturaLimpa.Domain.Entities;
using ExemploDeArquiteturaLimpa.Domain.Enumerators;
using Infrastructure.Repositories.Interfaces;
using Domain.Interfaces.Repositories;

namespace ExemploDeArquiteturaLimpa.Application.Handlers
{
    public class CriarMovimentoHandler : IRequestHandler<CriarMovimentoCommand, string>
    {
        private readonly ISqliteIdempotenciaRepository _sqliteIdempotenciaRepository;
        private readonly IContaCorrenteRepository _sqliteContaCorrenteRepository;
        public CriarMovimentoHandler(
            ISqliteIdempotenciaRepository sqliteIdempotenciaRepository, 
            IContaCorrenteRepository sqliteContaCorrenteRepository)
        {
            _sqliteIdempotenciaRepository = sqliteIdempotenciaRepository;
            _sqliteContaCorrenteRepository = sqliteContaCorrenteRepository;
        }

        public async Task<string> Handle(CriarMovimentoCommand request, CancellationToken cancellationToken)
        {

            var resultadoIdempotencia = await _sqliteIdempotenciaRepository.ConsultarAsync(request.ChaveIdempotencia);
            if (!string.IsNullOrEmpty(resultadoIdempotencia))
                return Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resultadoIdempotencia);

            var conta = await _sqliteContaCorrenteRepository.SelecionarContaAsync(request.ContaCorrenteId);

            if (conta == null)
                throw new Exception("INVALID_ACCOUNT");
            if (!conta.Ativo)
                throw new Exception("INACTIVE_ACCOUNT");
            if (request.Valor <= 0)
                throw new Exception("INVALID_VALUE");
            if (request.TipoMovimento != 'C' && request.TipoMovimento != 'D')
                throw new Exception("INVALID_TYPE");

            var movimento = new Movimento()
            {
                Id = Guid.NewGuid().ToString(),
                DataMovimento = DateTime.UtcNow,
                TipoMovimento = (TipoMovimento)request.TipoMovimento,
                Valor = request.Valor
            };

            await _sqliteContaCorrenteRepository.MovimentarSaldoAsync(movimento);

            return movimento.Id;
        }
    }
}
