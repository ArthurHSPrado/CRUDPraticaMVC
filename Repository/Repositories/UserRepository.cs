using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Responses;
using Microsoft.Extensions.Configuration;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepository , IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<BaseResponse> RegisterUser(User user)
        {
            var response = new BaseResponse();

            string query = @"
                    INSERT INTO 
                        tb_user (Name, Cpf, BirthDate, Email, Password)
                    VALUES
                        (@Name, @Cpf, @BirdthDate, @Email, @Password )";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    var rows = await _connection.ExecuteAsync(query,
                            new
                            {
                                user.Name,
                                user.Cpf,
                                user.BirthDate,
                                user.Email,
                                user.Password
                            });

                    if (rows == 1)
                    {
                        response.ErrorId = 0;
                        response.Message = "Dados Cadastrados com Sucesso";
                    }
                    else
                    {
                        response.ErrorId = 500;
                        response.Message = "Falha ao cadastrar Usuário";
                    }
                }
                else
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao conectar ao Banco de Dados";
                }
            }
            catch(Exception ex)
            {
                response.ErrorId = 500;
                response.Message = "Falha ao cadastrar Usuário";
            }
            finally
            {
                CloseConnection();
            }

            return response;            
        }
    }
}
