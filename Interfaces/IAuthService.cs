using OrderLookup.API.DTOs;

namespace OrderLookup.API.Interfaces;


    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }