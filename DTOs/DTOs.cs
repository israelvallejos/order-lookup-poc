namespace OrderLookup.API.DTOs;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string Token, string Username, string FullName, string Role, DateTime ExpiresAt);

public record OrderSearchRequest(string OrderNumber);

public record OrderDetailResponse
{
    public OrderInfo Order { get; init; } = null!;
    public CustomerInfo Customer { get; init; } = null!;
    public List<OrderItemInfo> Items { get; init; } = new();
    public List<StatusHistoryInfo> StatusHistory { get; init; } = new();
    public ShippingInfoDto? Shipping { get; init; }
    public PaymentDetailInfo? Payment { get; init; }
    public List<ServiceNoteInfo> ServiceNotes { get; init; } = new();
}

public record OrderInfo
{
    public int Id { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public DateTime OrderDate { get; init; }
    public DateTime? EstimatedDeliveryDate { get; init; }
    public DateTime? ActualDeliveryDate { get; init; }
    public string CurrentStatus { get; init; } = string.Empty;
    public decimal Subtotal { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal ShippingCost { get; init; }
    public decimal DiscountAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public string PaymentMethod { get; init; } = string.Empty;
    public string OrderSource { get; init; } = string.Empty;
    public string SpecialInstructions { get; init; } = string.Empty;
    public bool IsGift { get; init; }
    public string GiftMessage { get; init; } = string.Empty;
    public int ItemCount { get; init; }
}

public record CustomerInfo
{
    public int Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string CustomerType { get; init; } = string.Empty;
    public DateTime DateJoined { get; init; }
    public string PreferredContactMethod { get; init; } = string.Empty;
    public string Notes { get; init; } = string.Empty;
    public List<AddressInfo> Addresses { get; init; } = new();
}

public record AddressInfo
{
    public string AddressType { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public bool IsDefault { get; init; }
}

public record OrderItemInfo
{
    public int Id { get; init; }
    public string SKU { get; init; } = string.Empty;
    public string ProductName { get; init; } = string.Empty;
    public string ProductDescription { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal DiscountPercent { get; init; }
    public decimal LineTotal { get; init; }
    public string Status { get; init; } = string.Empty;
    public string? ReturnReason { get; init; }
}

public record StatusHistoryInfo
{
    public int Id { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
    public string UpdatedBy { get; init; } = string.Empty;
    public string Notes { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}

public record ShippingInfoDto
{
    public string Carrier { get; init; } = string.Empty;
    public string ServiceType { get; init; } = string.Empty;
    public string TrackingNumber { get; init; } = string.Empty;
    public string TrackingUrl { get; init; } = string.Empty;
    public decimal ShippingWeight { get; init; }
    public string PackageCount { get; init; } = string.Empty;
    public AddressInfo ShippingAddress { get; init; } = null!;
    public DateTime? ShippedDate { get; init; }
    public DateTime? DeliveredDate { get; init; }
    public string DeliverySignature { get; init; } = string.Empty;
    public string DeliveryInstructions { get; init; } = string.Empty;
}

public record PaymentDetailInfo
{
    public string PaymentMethod { get; init; } = string.Empty;
    public string PaymentStatus { get; init; } = string.Empty;
    public string TransactionId { get; init; } = string.Empty;
    public string CardLastFour { get; init; } = string.Empty;
    public string CardBrand { get; init; } = string.Empty;
    public DateTime PaymentDate { get; init; }
    public decimal AmountPaid { get; init; }
    public decimal? RefundedAmount { get; init; }
    public AddressInfo BillingAddress { get; init; } = null!;
}

public record ServiceNoteInfo
{
    public int Id { get; init; }
    public string AgentName { get; init; } = string.Empty;
    public string NoteType { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public bool IsInternal { get; init; }
    public string Priority { get; init; } = string.Empty;
    public bool RequiresFollowup { get; init; }
    public DateTime? FollowupDate { get; init; }
}
