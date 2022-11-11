using Domain.Entities;
using Domain.Responses;

namespace Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<BaseResponse> RegisterProduct(Product product);

        Task<GenericResponse<IEnumerable<Product>>> GetAllProducts();

        Task<GenericResponse<Product>> GetProductByName(string productName);
    }
}
