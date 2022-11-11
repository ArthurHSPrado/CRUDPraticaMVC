using Domain.Entities;
using Domain.Responses;

namespace Domain.Interfaces.Repository
{
    public interface ISupplierRepository
    {
        Task<BaseResponse> RegisterSupplier(Supplier supplier);

        Task<GenericResponse<IEnumerable<Supplier>>> GetAllSupplier();

        Task<GenericResponse<Supplier>> GetSupplierByCnpj(string Cnpj);

        public Task<GenericResponse<Supplier>> GetSupplierByName(string Name);
    }
}
