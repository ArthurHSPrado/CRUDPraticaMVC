using Domain.Entities;
using Domain.Responses;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<BaseResponse> RegisterUser(User user);
    }
}
