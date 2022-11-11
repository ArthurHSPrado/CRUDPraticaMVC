using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Repositories
{
    public abstract class BaseRepository
    {
        public readonly IConfiguration _configuration;

        public IDbConnection _connection;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeDataBase();
        }

        public void InitializeDataBase()
        {
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public void OpenIfClosed()
        {
            if(_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public bool IsConnectionOpen()
        {
            return _connection.State == ConnectionState.Open;
        }

        public void CloseConnection()
        {
            try
            {
                if (IsConnectionOpen())
                {
                    _connection.Close();
                }
            }
            catch
            {
                return;
            }
        }
    }
}
