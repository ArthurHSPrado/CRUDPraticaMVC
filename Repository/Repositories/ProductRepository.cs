using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository , IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<GenericResponse<IEnumerable<Product>>> GetAllProducts()
        {
            var response = new GenericResponse<IEnumerable<Product>>();

            string query = @"
                    SELECT
                        *
                    FROM
                        tb_produtos";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    response.Object = await _connection.QueryAsync<Product>(query);

                    if(response.Object != null)
                    {
                        response.ErrorId = 0;
                        response.Message = "Listagem realizada com sucesso";
                    }
                    else
                    {
                        response.ErrorId = 500;
                        response.Message = "Houve falha na listagem";
                    }
                }
                else
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao conectrar-se com o banco de dados";
                }
            }
            catch(Exception ex)
            {
                response.ErrorId = 500;
                response.Message = "Falha ao conectar-se com o banco de dados";
            }
            finally
            {
                CloseConnection();
            }

            return response;
        }

        public async Task<GenericResponse<Product>> GetProductByName(string productName)
        {
            var response = new GenericResponse<Product>();

            string query = @"
                    SELECT 
                        *
                    FROM
                        tb_produtos p 
                    WHERE
                        p.Name = @Name";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    response.Object = await _connection.QueryFirstOrDefaultAsync<Product>(query,
                                      new
                                      {
                                          Name = productName,
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
                    response.Message = "Falha ao conectar com o banco de dados";
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

        public async Task<BaseResponse> RegisterProduct(Product product)
        {
            var response = new BaseResponse();

            string query = @"
                    INSERT INTO
                        tb_produtos(Name, Description, ProductId, Weight)
                    VALUES
                        (@Name, @Description, @ProductId, @Weight)";

            _connection.Open();

            try
            {
                if (IsConnectionOpen())
                {
                    var rows = await _connection.ExecuteAsync(query,
                         new
                         {
                             product.Name,
                             product.Description,
                             product.ProductId,
                             product.Weight
                         });

                    if (rows == 1)
                    {
                        response.ErrorId = 0;
                        response.Message = "Produto cadastrado com sucesso";
                    }
                    else
                    {
                        response.ErrorId = 500;
                        response.Message = "Falha ao cadastrar o Produto";
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
                response.Message = "Falha ao cadastrar o produto";
            }
            finally
            {
                CloseConnection();
            }

            return response;
        }
    }
}
