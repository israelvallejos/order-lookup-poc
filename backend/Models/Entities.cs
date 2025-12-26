namespace OrderLookup.API.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "CustomerService";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string CustomerType { get; set; } = "Regular"; // Regular, VIP, Wholesale
    public DateTime DateJoined { get; set; }
    public string PreferredContactMethod { get; set; } = "Email";
    public string Notes { get; set; } = string.Empty;
    
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class Address
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string AddressType { get; set; } = "Shipping"; // Shipping, Billing
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    
    public Customer Customer { get; set; } = null!;
}

public class Product
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public string CurrentStatus { get; set; } = "Pending";
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string OrderSource { get; set; } = "Web"; // Web, Mobile, Phone, InStore
    public string SpecialInstructions { get; set; } = string.Empty;
    public bool IsGift { get; set; }
    public string GiftMessage { get; set; } = string.Empty;
    
    public Customer Customer { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public ICollection<StatusHistory> StatusHistory { get; set; } = new List<StatusHistory>();
    public ShippingInfo? ShippingInfo { get; set; }
    public PaymentDetail? PaymentDetail { get; set; }
    public ICollection<CustomerServiceNote> ServiceNotes { get; set; } = new List<CustomerServiceNote>();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal LineTotal { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Picked, Packed, Shipped, Delivered, Returned
    public string? ReturnReason { get; set; }
    
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}

public class StatusHistory
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    
    public Order Order { get; set; } = null!;
}

public class ShippingInfo
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Carrier { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty; // Standard, Express, Overnight
    public string TrackingNumber { get; set; } = string.Empty;
    public string TrackingUrl { get; set; } = string.Empty;
    public decimal ShippingWeight { get; set; }
    public string PackageCount { get; set; } = "1";
    public string ShippingAddressStreet { get; set; } = string.Empty;
    public string ShippingAddressCity { get; set; } = string.Empty;
    public string ShippingAddressState { get; set; } = string.Empty;
    public string ShippingAddressPostalCode { get; set; } = string.Empty;
    public string ShippingAddressCountry { get; set; } = string.Empty;
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public string DeliverySignature { get; set; } = string.Empty;
    public string DeliveryInstructions { get; set; } = string.Empty;
    
    public Order Order { get; set; } = null!;
}

public class PaymentDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // CreditCard, PayPal, BankTransfer
    public string PaymentStatus { get; set; } = "Pending"; // Pending, Authorized, Captured, Refunded, Failed
    public string TransactionId { get; set; } = string.Empty;
    public string CardLastFour { get; set; } = string.Empty;
    public string CardBrand { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal? RefundedAmount { get; set; }
    public string BillingAddressStreet { get; set; } = string.Empty;
    public string BillingAddressCity { get; set; } = string.Empty;
    public string BillingAddressState { get; set; } = string.Empty;
    public string BillingAddressPostalCode { get; set; } = string.Empty;
    public string BillingAddressCountry { get; set; } = string.Empty;
    
    public Order Order { get; set; } = null!;
}

public class CustomerServiceNote
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string AgentName { get; set; } = string.Empty;
    public string NoteType { get; set; } = "General"; // General, Complaint, Inquiry, Resolution, Followup
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsInternal { get; set; } = true;
    public string Priority { get; set; } = "Normal"; // Low, Normal, High, Urgent
    public bool RequiresFollowup { get; set; }
    public DateTime? FollowupDate { get; set; }
    
    public Order Order { get; set; } = null!;
}
