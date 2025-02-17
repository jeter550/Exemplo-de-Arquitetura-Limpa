using Dapper;
using Domain.Interfaces.Repositories;
using ExemploDeArquiteturaLimpa.Domain.Entities;
using System.Data;
using System.Data.Common;

namespace ExemploDeArquiteturaLimpa.Infrastructure.Repositories.Sqlite
{
    public class SqliteContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDbConnection _dbConnection;

        public SqliteContaCorrenteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ContaCorrente> SelecionarContaAsync(string contaCorrenteId)
        {
           var sql = "SELECT * FROM contacorrente WHERE idcontacorrente = @ContaCorrenteId";
           return await  _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { ContaCorrenteId = contaCorrenteId });
        }

        public async Task<decimal> ConsultarSaldoAsync(string contaCorrenteId)
        {
            return await _dbConnection.ExecuteScalarAsync<decimal>(@"
                SELECT COALESCE(SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE -valor END), 0) 
                FROM movimento 
                WHERE idcontacorrente = @ContaCorrenteId", new { ContaCorrenteId = contaCorrenteId});
        }

        public async Task MovimentarSaldoAsync(Movimento movimento)
        {
            var sql = "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@Id, @ContaCorrenteId, @DataMovimento, @TipoMovimento, @Valor)";
            await _dbConnection.ExecuteAsync(sql, movimento);
        }
    }
}
