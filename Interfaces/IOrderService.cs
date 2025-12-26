using OrderLookup.API.DTOs;

namespace OrderLookup.API.Interfaces;

    public interface IOrderService
    {
        Task<OrderDetailResponse?> GetOrderByNumberAsync(string orderNumber);
    }
