using Dapper;
using Npgsql;
using System.Data;

namespace Project.Storage
{
    public interface IStorage
    {        
       public Task Save(string data);
       public Task<string> Retrieve(int id);
    }

    public class Storage : IStorage
    {
        private readonly IDbConnection _connection;
        private readonly string _connectionString = "Server=localhost;Port=5432;Database=project;User Id=ProjectAdmin;Password=123456;";
        public Storage()
        {
            _connection = new NpgsqlConnection(_connectionString);
        }

        public async Task Save(string data)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            var sql = "INSERT INTO public.item (value) VALUES (@value)";

            await _connection.ExecuteAsync(sql, new { value = data });
        }

        public async Task<string> Retrieve(int id)
        {          
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            var sql = "SELECT value FROM public.item WHERE id = @id";

            var result = await _connection.QueryFirstOrDefaultAsync<string>(sql, new
            {
                id = id
            });

            return result;
        }
    }
}
