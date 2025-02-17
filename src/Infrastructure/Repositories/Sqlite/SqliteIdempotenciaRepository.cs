using Dapper;
using System.Data;
using Infrastructure.Repositories.Interfaces;

namespace ExemploDeArquiteturaLimpa.Infrastructure.Repositories.Sqlite
{
    public class SqliteIdempotenciaRepository: ISqliteIdempotenciaRepository
    {
        private readonly IDbConnection _dbConnection;

        public SqliteIdempotenciaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<string> ConsultarAsync(string chaveIdempotencia)
        {
            var idempotenciaSql = "SELECT resultado FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia";
            return await _dbConnection.QueryFirstOrDefaultAsync<string>(idempotenciaSql, new { ChaveIdempotencia = chaveIdempotencia} );
        }

        public async Task RegistrarAsync(string chaveIdempotencia, string requisicaoJson, string resultadoJson)
        {
            var inserirIdempotenciaSql = @"
                INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
                VALUES (@ChaveIdempotencia, @Requisicao, @Resultado)";

            await _dbConnection.ExecuteAsync(inserirIdempotenciaSql, new
            {
                ChaveIdempotencia = chaveIdempotencia,
                Requisicao = requisicaoJson,
                Resultado = resultadoJson
            });
        }
    }
}
