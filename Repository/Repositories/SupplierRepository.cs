using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Responses;
using Microsoft.Extensions.Configuration;

namespace Repository.Repositories
{
    public class SupplierRepository : BaseRepository , ISupplierRepository
    {
        public SupplierRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<GenericResponse<IEnumerable<Supplier>>> GetAllSupplier()
        {
            var response = new GenericResponse<IEnumerable<Supplier>>();

            string query = @"
                    SELECT
                        *
                    FROM
                        tb_forncedores";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    response.Object = await _connection.QueryAsync<Supplier>(query);

                    if (response.Object != null)
                    {
                        response.ErrorId = 0;
                        response.Message = "Listagem realizada com sucesso";
                    }
                    else
                    {
                        response.ErrorId = 500;
                        response.Message = "Falha na listagem de fornecedores";
                    }
                }
                else
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao conectar com o banco de dados";
                }
            }
            catch(Exception e)
            {
                response.ErrorId = 500;
                response.Message = "Falha ao realizar listagem";
            }
            finally
            {
                CloseConnection();
            }

            return response;
        }

        public async Task<GenericResponse<Supplier>> GetSupplierByCnpj(string Cnpj)
        {
            var response = new GenericResponse<Supplier>();

            string query = @"
                    SELECT 
                        *
                    FROM 
                        tb_forncedores f
                    WHERE 
                        f.Cnpj = @Cnpj";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    response.Object = await _connection.QueryFirstOrDefaultAsync<Supplier>(query,
                                      new
                                      {
                                          Cnpj = Cnpj
                                      });

                    if (response.Object != null)
                    {
                        response.ErrorId = 0;
                        response.Message = "Busca realizada com sucesso";
                    }
                    else
                    {
                        response.ErrorId = 500;
                        response.Message = "Falha ao realizar busca";
                    }
                }
                else
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao conectar com o servidor";
                }
            }
            catch(Exception e)
            {
                response.ErrorId = 500;
                response.Message = "Falha ao realizar busca";
            }
            finally
            {
                CloseConnection();
            }

            return response;
        }

        public async Task<GenericResponse<Supplier>> GetSupplierByName(string Name)
        {
            var response = new GenericResponse<Supplier>();

            string query = @"
                    SELECT 
                        *
                    FROM
                        tb_fornecedores f
                    WHERE 
                        f.Name = @Name";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    response.Object = await _connection.QueryFirstOrDefaultAsync<Supplier>(query,
                                      new
                                      {
                                          Name = Name
                                      });

                    if (response.Object != null)
                    {
                        response.ErrorId = 0;
                        response.Message = "Busca efetuada com sucesso";
                    }
                    else
                    {
                        response.ErrorId = 500;
                        response.Message = "Falha ao efetuar busca";
                    }
                }
                else
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao conectar com o banco de dados";
                }
            }
            catch(Exception e)
            {
                response.ErrorId = 500;
                response.Message = "Falha ao efetuar busca";
            }
            finally
            {
                CloseConnection();
            }

            return response;
        }

        public async Task<BaseResponse> RegisterSupplier(Supplier supplier)
        {
            var response = new BaseResponse();

            string query = @"
                    INSERT INTO 
                        tb_fornecedores(Name, Cnpj, RegistrationDate)
                    VALUES
                        (@Name, @Cnpj, @RegistrationDate)";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    var rows = await _connection.ExecuteAsync(query,
                         new
                         {
                             supplier.Name,
                             supplier.Cnpj,
                             supplier.RegistrationDate
                         });

                    if (rows == 1)
                    {
                        response.ErrorId = 0;
                        response.Message = "Fornecedor cadastrado com sucesso";
                    }
                    else
                    {
                        response.ErrorId = 500;
                        response.Message = "Falha ao cadastrar Fornecedor";
                    }                   
                }
                else
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao conectar-se com o banco de dados";
                }                          
            }
            catch(Exception ex)
            {
                response.ErrorId = 500;
                response.Message = "Falha ao cadastrar Fornecedor";
            }
            finally
            {
                CloseConnection();
            }

            return response;
        }
    }
}
