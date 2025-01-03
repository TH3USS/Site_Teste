using Dapper;
using ProgramaTeste.Models;
using System.Data;
using System.Data.Common;

namespace ProgramaTeste.Repositories
{
        public class EnderecoRepository
    {
        private readonly DapperContext _context;

        public EnderecoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Endereco endereco)
        {
            var query = "INSERT INTO Endereco (PessoaId, Logradouro, CEP, Cidade, Estado) VALUES (@PessoaId, @Logradouro, @CEP, @Cidade, @Estado)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, endereco);
            }
        }

        public async Task<IEnumerable<Endereco>> GetByPessoaIdAsync(int pessoaId)
        {
            var query = "SELECT * FROM Endereco WHERE PessoaId = @PessoaId";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Endereco>(query, new { PessoaId = pessoaId });
            }
        }

        public async Task<Endereco> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Endereco WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Endereco>(query, new { Id = id });
            }
        }

        public async Task UpdateAsync(Endereco endereco)
        {
            var query = "UPDATE Endereco SET Logradouro = @Logradouro, CEP = @CEP, Cidade = @Cidade, Estado = @Estado WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, endereco);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Endereco WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }


    }

}
