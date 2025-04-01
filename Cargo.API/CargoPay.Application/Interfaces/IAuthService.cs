using CargoPay.Application.DTOS;

namespace CargoPay.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }

}
