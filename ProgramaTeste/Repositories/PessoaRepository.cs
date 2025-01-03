using Dapper;
using ProgramaTeste.Models;
using System.Data;
using System.Data.Common;

namespace ProgramaTeste.Repositories
{  

    public class PessoaRepository
    {
        private readonly DapperContext _context;

        public PessoaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            var query = "SELECT * FROM Pessoa";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Pessoa>(query);
            }
        }

        public async Task AddAsync(Pessoa pessoa)
        {
            var query = "INSERT INTO Pessoa (Nome, Telefone, CPF) VALUES (@Nome, @Telefone, @CPF)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, pessoa); // Insere os dados usando Dapper
            }
        }


        public async Task<Pessoa> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Pessoa WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Pessoa>(query, new { Id = id });
            }
        }

        public async Task UpdateAsync(Pessoa pessoa)
        {
            var query = "UPDATE Pessoa SET Nome = @Nome, Telefone = @Telefone, CPF = @CPF WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, pessoa);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Pessoa WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
            
        }

    }

}
