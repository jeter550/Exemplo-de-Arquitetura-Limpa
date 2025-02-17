using ExemploDeArquiteturaLimpa.Domain.Entities;
using System.Data;

namespace Domain.Interfaces.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> SelecionarContaAsync(string contaCorrenteId);
        Task<decimal> ConsultarSaldoAsync(string contaCorrenteId);
        Task MovimentarSaldoAsync(Movimento movimento);

    }
}
