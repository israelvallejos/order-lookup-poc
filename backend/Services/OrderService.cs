using Microsoft.EntityFrameworkCore;
using OrderLookup.API.Data;
using OrderLookup.API.DTOs;
using OrderLookup.API.Interfaces;

namespace OrderLookup.API.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<OrderDetailResponse?> GetOrderByNumberAsync(string orderNumber)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
                .ThenInclude(c => c.Addresses)
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Include(o => o.StatusHistory)
            .Include(o => o.ShippingInfo)
            .Include(o => o.PaymentDetail)
            .Include(o => o.ServiceNotes)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

        if (order == null)
            return null;
        
        return new OrderDetailResponse
        {
            Order = new OrderInfo
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                ActualDeliveryDate = order.ActualDeliveryDate,
                CurrentStatus = order.CurrentStatus,
                Subtotal = order.Subtotal,
                TaxAmount = order.TaxAmount,
                ShippingCost = order.ShippingCost,
                DiscountAmount = order.DiscountAmount,
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod,
                OrderSource = order.OrderSource,
                SpecialInstructions = order.SpecialInstructions,
                IsGift = order.IsGift,
                GiftMessage = order.GiftMessage,
                ItemCount = order.Items.Count
            },
            Customer = new CustomerInfo
            {
                Id = order.Customer.Id,
                FullName = $"{order.Customer.FirstName} {order.Customer.LastName}",
                Email = order.Customer.Email,
                Phone = order.Customer.Phone,
                CustomerType = order.Customer.CustomerType,
                DateJoined = order.Customer.DateJoined,
                PreferredContactMethod = order.Customer.PreferredContactMethod,
                Notes = order.Customer.Notes,
                Addresses = order.Customer.Addresses.Select(a => new AddressInfo
                {
                    AddressType = a.AddressType,
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    IsDefault = a.IsDefault
                }).ToList()
            },
            Items = order.Items.Select(i => new OrderItemInfo
            {
                Id = i.Id,
                SKU = i.Product.SKU,
                ProductName = i.Product.Name,
                ProductDescription = i.Product.Description,
                Category = i.Product.Category,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                DiscountPercent = i.DiscountPercent,
                LineTotal = i.LineTotal,
                Status = i.Status,
                ReturnReason = i.ReturnReason
            }).ToList(),
            StatusHistory = order.StatusHistory
                .OrderBy(sh => sh.Timestamp)
                .Select(sh => new StatusHistoryInfo
                {
                    Id = sh.Id,
                    Status = sh.Status,
                    Timestamp = sh.Timestamp,
                    UpdatedBy = sh.UpdatedBy,
                    Notes = sh.Notes,
                    Location = sh.Location
                }).ToList(),
            Shipping = order.ShippingInfo != null ? new ShippingInfoDto
            {
                Carrier = order.ShippingInfo.Carrier,
                ServiceType = order.ShippingInfo.ServiceType,
                TrackingNumber = order.ShippingInfo.TrackingNumber,
                TrackingUrl = order.ShippingInfo.TrackingUrl,
                ShippingWeight = order.ShippingInfo.ShippingWeight,
                PackageCount = order.ShippingInfo.PackageCount,
                ShippingAddress = new AddressInfo
                {
                    Street = order.ShippingInfo.ShippingAddressStreet,
                    City = order.ShippingInfo.ShippingAddressCity,
                    State = order.ShippingInfo.ShippingAddressState,
                    PostalCode = order.ShippingInfo.ShippingAddressPostalCode,
                    Country = order.ShippingInfo.ShippingAddressCountry
                },
                ShippedDate = order.ShippingInfo.ShippedDate,
                DeliveredDate = order.ShippingInfo.DeliveredDate,
                DeliverySignature = order.ShippingInfo.DeliverySignature,
                DeliveryInstructions = order.ShippingInfo.DeliveryInstructions
            } : null,
            Payment = order.PaymentDetail != null ? new PaymentDetailInfo
            {
                PaymentMethod = order.PaymentDetail.PaymentMethod,
                PaymentStatus = order.PaymentDetail.PaymentStatus,
                TransactionId = order.PaymentDetail.TransactionId,
                CardLastFour = order.PaymentDetail.CardLastFour,
                CardBrand = order.PaymentDetail.CardBrand,
                PaymentDate = order.PaymentDetail.PaymentDate,
                AmountPaid = order.PaymentDetail.AmountPaid,
                RefundedAmount = order.PaymentDetail.RefundedAmount,
                BillingAddress = new AddressInfo
                {
                    Street = order.PaymentDetail.BillingAddressStreet,
                    City = order.PaymentDetail.BillingAddressCity,
                    State = order.PaymentDetail.BillingAddressState,
                    PostalCode = order.PaymentDetail.BillingAddressPostalCode,
                    Country = order.PaymentDetail.BillingAddressCountry
                }
            } : null,
            ServiceNotes = order.ServiceNotes
                .OrderByDescending(sn => sn.CreatedAt)
                .Select(sn => new ServiceNoteInfo
                {
                    Id = sn.Id,
                    AgentName = sn.AgentName,
                    NoteType = sn.NoteType,
                    Content = sn.Content,
                    CreatedAt = sn.CreatedAt,
                    IsInternal = sn.IsInternal,
                    Priority = sn.Priority,
                    RequiresFollowup = sn.RequiresFollowup,
                    FollowupDate = sn.FollowupDate
                }).ToList()
        };
    }
}
