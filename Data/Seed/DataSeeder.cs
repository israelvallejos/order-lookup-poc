using OrderLookup.API.Models;
using BCrypt.Net;

namespace OrderLookup.API.Data.Seed;

public static class DataSeeder
{
    public static void SeedData(AppDbContext context)
    {
        if (context.Users.Any()) return;
        
        var users = new List<User>
        {
            new() { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), FullName = "Administrador", Role = "Admin" },
            new() { Username = "agent1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("agent123"), FullName = "María González", Role = "CustomerService" },
            new() { Username = "agent2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("agent123"), FullName = "Carlos Rodríguez", Role = "CustomerService" }
        };
        context.Users.AddRange(users);
        
        var products = new List<Product>
        {
            new() { SKU = "LAPTOP-001", Name = "Notebook ProBook 15", Description = "15.6\" FHD, Intel i7, 16GB RAM, 512GB SSD", Category = "Electrónica", Price = 1299990m, Weight = 2.1m },
            new() { SKU = "LAPTOP-002", Name = "Notebook UltraSlim 14", Description = "14\" 4K, AMD Ryzen 9, 32GB RAM, 1TB SSD", Category = "Electrónica", Price = 1899990m, Weight = 1.4m },
            new() { SKU = "MOUSE-001", Name = "Mouse Inalámbrico Ergonómico", Description = "Bluetooth 5.0, 4000 DPI, Recargable", Category = "Accesorios", Price = 79990m, Weight = 0.12m },
            new() { SKU = "KEYBOARD-001", Name = "Teclado Mecánico Gamer", Description = "RGB, Cherry MX Blue, Tamaño Completo", Category = "Accesorios", Price = 149990m, Weight = 1.1m },
            new() { SKU = "MONITOR-001", Name = "Monitor UltraWide 34\"", Description = "34\" UWQHD, 144Hz, IPS, Hub USB-C", Category = "Electrónica", Price = 699990m, Weight = 8.5m },
            new() { SKU = "HEADSET-001", Name = "Audífonos Inalámbricos Pro", Description = "ANC, 30hrs Batería, Bluetooth 5.2", Category = "Audio", Price = 349990m, Weight = 0.28m },
            new() { SKU = "WEBCAM-001", Name = "Webcam 4K Pro", Description = "4K 60fps, Autoenfoque, Micrófono Integrado", Category = "Accesorios", Price = 199990m, Weight = 0.15m },
            new() { SKU = "DOCK-001", Name = "Docking Station Thunderbolt 4", Description = "16-en-1, 96W PD, Soporte Dual 4K", Category = "Accesorios", Price = 299990m, Weight = 0.45m },
            new() { SKU = "CHAIR-001", Name = "Silla Ejecutiva Ergonómica", Description = "Respaldo Mesh, Soporte Lumbar, Brazos Ajustables", Category = "Mobiliario", Price = 599990m, Weight = 22.0m },
            new() { SKU = "DESK-001", Name = "Escritorio Eléctrico Ajustable", Description = "150x75cm, Ajuste Eléctrico, Memoria de Altura", Category = "Mobiliario", Price = 799990m, Weight = 45.0m },
            new() { SKU = "TABLET-001", Name = "Tablet ProTab 12.9", Description = "12.9\" OLED, 256GB, WiFi + 5G", Category = "Electrónica", Price = 1099990m, Weight = 0.68m },
            new() { SKU = "CASE-001", Name = "Funda Notebook Premium", Description = "15.6\" Neopreno, Resistente al Agua", Category = "Accesorios", Price = 49990m, Weight = 0.3m },
            new() { SKU = "CHARGER-001", Name = "Cargador GaN 100W", Description = "4 Puertos, USB-C PD, Enchufe Plegable", Category = "Accesorios", Price = 89990m, Weight = 0.2m },
            new() { SKU = "SSD-001", Name = "SSD NVMe 2TB", Description = "PCIe 4.0, 7000MB/s Lectura, Disipador", Category = "Componentes", Price = 249990m, Weight = 0.05m },
            new() { SKU = "RAM-001", Name = "Kit Memoria DDR5 32GB", Description = "2x16GB, 5600MHz, CL36", Category = "Componentes", Price = 189990m, Weight = 0.02m }
        };
        context.Products.AddRange(products);
        
        var customers = new List<Customer>
        {
            new()
            {
                FirstName = "Roberto",
                LastName = "Silva Mendoza",
                Email = "roberto.silva@email.com",
                Phone = "+56 9 8765 4321",
                CustomerType = "VIP",
                DateJoined = DateTime.UtcNow.AddYears(-3),
                PreferredContactMethod = "Phone",
                Notes = "Cliente preferente. Siempre paga a tiempo. Prefiere contacto telefónico para asuntos urgentes."
            },
            new()
            {
                FirstName = "Ana",
                LastName = "Martínez Rojas",
                Email = "ana.martinez@techcorp.cl",
                Phone = "+56 9 1234 5678",
                CustomerType = "Wholesale",
                DateJoined = DateTime.UtcNow.AddYears(-2),
                PreferredContactMethod = "Email",
                Notes = "Cliente B2B - TechCorp Chile. Pedidos trimestrales en volumen. Pago a 30 días."
            },
            new()
            {
                FirstName = "Diego",
                LastName = "Fernández Castro",
                Email = "diego.fernandez@gmail.com",
                Phone = "+56 9 5555 1234",
                CustomerType = "Regular",
                DateJoined = DateTime.UtcNow.AddMonths(-6),
                PreferredContactMethod = "Email",
                Notes = "Cliente nuevo. Primer pedido grande."
            }
        };
        context.Customers.AddRange(customers);
        context.SaveChanges();
        
        var addresses = new List<Address>
        {
            
            new() { CustomerId = customers[0].Id, AddressType = "Shipping", Street = "Av. Providencia 1234, Depto 801", City = "Providencia", State = "Región Metropolitana", PostalCode = "7500000", Country = "Chile", IsDefault = true },
            new() { CustomerId = customers[0].Id, AddressType = "Billing", Street = "Av. Providencia 1234, Depto 801", City = "Providencia", State = "Región Metropolitana", PostalCode = "7500000", Country = "Chile", IsDefault = true },
            new() { CustomerId = customers[0].Id, AddressType = "Shipping", Street = "Oficina: Av. Apoquindo 4500, Of. 1205", City = "Las Condes", State = "Región Metropolitana", PostalCode = "7550000", Country = "Chile", IsDefault = false },
            
            new() { CustomerId = customers[1].Id, AddressType = "Shipping", Street = "Av. Andrés Bello 2711, Piso 12", City = "Las Condes", State = "Región Metropolitana", PostalCode = "7550611", Country = "Chile", IsDefault = true },
            new() { CustomerId = customers[1].Id, AddressType = "Billing", Street = "Av. Andrés Bello 2711, Piso 12", City = "Las Condes", State = "Región Metropolitana", PostalCode = "7550611", Country = "Chile", IsDefault = true },
            
            new() { CustomerId = customers[2].Id, AddressType = "Shipping", Street = "Calle Los Aromos 567", City = "Ñuñoa", State = "Región Metropolitana", PostalCode = "7750000", Country = "Chile", IsDefault = true },
            new() { CustomerId = customers[2].Id, AddressType = "Billing", Street = "Calle Los Aromos 567", City = "Ñuñoa", State = "Región Metropolitana", PostalCode = "7750000", Country = "Chile", IsDefault = true }
        };
        context.Addresses.AddRange(addresses);
        context.SaveChanges();
        
        var order1 = new Order
        {
            OrderNumber = "ORD-2024-78542",
            CustomerId = customers[0].Id,
            OrderDate = DateTime.UtcNow.AddDays(-15),
            EstimatedDeliveryDate = DateTime.UtcNow.AddDays(-8),
            ActualDeliveryDate = DateTime.UtcNow.AddDays(-9),
            CurrentStatus = "Delivered",
            Subtotal = 4629910m,
            TaxAmount = 879682m,
            ShippingCost = 0m,
            DiscountAmount = 462991m,
            TotalAmount = 5046601m,
            PaymentMethod = "CreditCard",
            OrderSource = "Web",
            SpecialInstructions = "Llamar 30 minutos antes de entregar. Edificio con acceso controlado - código 4521#",
            IsGift = false
        };
        context.Orders.Add(order1);
        context.SaveChanges();
        
        var order1Items = new List<OrderItem>
        {
            new() { OrderId = order1.Id, ProductId = products[1].Id, Quantity = 1, UnitPrice = 1899990m, DiscountPercent = 10, LineTotal = 1709991m, Status = "Delivered" },
            new() { OrderId = order1.Id, ProductId = products[4].Id, Quantity = 2, UnitPrice = 699990m, DiscountPercent = 10, LineTotal = 1259982m, Status = "Delivered" },
            new() { OrderId = order1.Id, ProductId = products[7].Id, Quantity = 1, UnitPrice = 299990m, DiscountPercent = 10, LineTotal = 269991m, Status = "Delivered" },
            new() { OrderId = order1.Id, ProductId = products[5].Id, Quantity = 1, UnitPrice = 349990m, DiscountPercent = 10, LineTotal = 314991m, Status = "Delivered" },
            new() { OrderId = order1.Id, ProductId = products[2].Id, Quantity = 2, UnitPrice = 79990m, DiscountPercent = 10, LineTotal = 143982m, Status = "Delivered" },
            new() { OrderId = order1.Id, ProductId = products[3].Id, Quantity = 1, UnitPrice = 149990m, DiscountPercent = 10, LineTotal = 134991m, Status = "Delivered" },
            new() { OrderId = order1.Id, ProductId = products[6].Id, Quantity = 1, UnitPrice = 199990m, DiscountPercent = 10, LineTotal = 179991m, Status = "Delivered" },
            new() { OrderId = order1.Id, ProductId = products[12].Id, Quantity = 2, UnitPrice = 89990m, DiscountPercent = 10, LineTotal = 161982m, Status = "Delivered" }
        };
        context.OrderItems.AddRange(order1Items);
        
        var order1StatusHistory = new List<StatusHistory>
        {
            new() { OrderId = order1.Id, Status = "Pending", Timestamp = order1.OrderDate, UpdatedBy = "Sistema", Notes = "Pedido ingresado vía web", Location = "Santiago, Chile" },
            new() { OrderId = order1.Id, Status = "Payment Confirmed", Timestamp = order1.OrderDate.AddMinutes(5), UpdatedBy = "Pasarela de Pago", Notes = "Pago autorizado - Visa ****4532", Location = "Santiago, Chile" },
            new() { OrderId = order1.Id, Status = "Processing", Timestamp = order1.OrderDate.AddHours(2), UpdatedBy = "Sistema Bodega", Notes = "Pedido enviado a centro de distribución", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order1.Id, Status = "Picking", Timestamp = order1.OrderDate.AddHours(4), UpdatedBy = "Juan Pérez", Notes = "Productos siendo recolectados del inventario", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order1.Id, Status = "Quality Check", Timestamp = order1.OrderDate.AddHours(6), UpdatedBy = "Equipo QA", Notes = "Todos los productos verificados - números de serie registrados", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order1.Id, Status = "Packed", Timestamp = order1.OrderDate.AddHours(8), UpdatedBy = "Equipo Embalaje", Notes = "3 bultos preparados - peso total 12.5kg", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order1.Id, Status = "Ready for Pickup", Timestamp = order1.OrderDate.AddDays(1), UpdatedBy = "Depto. Despacho", Notes = "Esperando retiro del transportista", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order1.Id, Status = "Shipped", Timestamp = order1.OrderDate.AddDays(1).AddHours(10), UpdatedBy = "Chilexpress", Notes = "Retirado por transportista", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order1.Id, Status = "In Transit", Timestamp = order1.OrderDate.AddDays(2), UpdatedBy = "Chilexpress", Notes = "Paquete en tránsito hacia destino", Location = "Hub Chilexpress Santiago Centro" },
            new() { OrderId = order1.Id, Status = "Out for Delivery", Timestamp = order1.OrderDate.AddDays(6), UpdatedBy = "Chilexpress", Notes = "En reparto con el repartidor", Location = "Providencia" },
            new() { OrderId = order1.Id, Status = "Delivery Attempted", Timestamp = order1.OrderDate.AddDays(6).AddHours(3), UpdatedBy = "Repartidor", Notes = "Cliente no disponible - se reintentará mañana", Location = "Av. Providencia 1234" },
            new() { OrderId = order1.Id, Status = "Out for Delivery", Timestamp = order1.OrderDate.AddDays(7).AddHours(9), UpdatedBy = "Chilexpress", Notes = "Segundo intento de entrega", Location = "Providencia" },
            new() { OrderId = order1.Id, Status = "Delivered", Timestamp = order1.OrderDate.AddDays(6).AddHours(14), UpdatedBy = "Repartidor", Notes = "Entregado al destinatario - Firmado por R. Silva", Location = "Av. Providencia 1234, Depto 801" }
        };
        context.StatusHistories.AddRange(order1StatusHistory);
        
        var order1Shipping = new ShippingInfo
        {
            OrderId = order1.Id,
            Carrier = "Chilexpress",
            ServiceType = "Express",
            TrackingNumber = "CHX-99887766554",
            TrackingUrl = "https://www.chilexpress.cl/seguimiento/CHX-99887766554",
            ShippingWeight = 12.5m,
            PackageCount = "3",
            ShippingAddressStreet = "Av. Providencia 1234, Depto 801",
            ShippingAddressCity = "Providencia",
            ShippingAddressState = "Región Metropolitana",
            ShippingAddressPostalCode = "7500000",
            ShippingAddressCountry = "Chile",
            ShippedDate = order1.OrderDate.AddDays(1).AddHours(10),
            DeliveredDate = order1.OrderDate.AddDays(6).AddHours(14),
            DeliverySignature = "R. Silva",
            DeliveryInstructions = "Llamar 30 min antes. Código acceso: 4521#"
        };
        context.ShippingInfos.Add(order1Shipping);
        
        var order1Payment = new PaymentDetail
        {
            OrderId = order1.Id,
            PaymentMethod = "CreditCard",
            PaymentStatus = "Captured",
            TransactionId = "TXN-2024-ABCD1234",
            CardLastFour = "4532",
            CardBrand = "Visa",
            PaymentDate = order1.OrderDate.AddMinutes(5),
            AmountPaid = 5046601m,
            BillingAddressStreet = "Av. Providencia 1234, Depto 801",
            BillingAddressCity = "Providencia",
            BillingAddressState = "Región Metropolitana",
            BillingAddressPostalCode = "7500000",
            BillingAddressCountry = "Chile"
        };
        context.PaymentDetails.Add(order1Payment);
        
        var order1Notes = new List<CustomerServiceNote>
        {
            new() { OrderId = order1.Id, AgentName = "María González", NoteType = "General", Content = "Cliente VIP - priorizar atención", CreatedAt = order1.OrderDate.AddHours(1), IsInternal = true, Priority = "High" },
            new() { OrderId = order1.Id, AgentName = "María González", NoteType = "Inquiry", Content = "Cliente llamó para confirmar fecha de entrega. Se explicó tiempo de despacho Express.", CreatedAt = order1.OrderDate.AddDays(2), IsInternal = false, Priority = "Normal" },
            new() { OrderId = order1.Id, AgentName = "Carlos Rodríguez", NoteType = "Complaint", Content = "Cliente molesto por intento de entrega fallido. Se pidieron disculpas y se confirmó reintento para el día siguiente.", CreatedAt = order1.OrderDate.AddDays(6).AddHours(16), IsInternal = false, Priority = "High" },
            new() { OrderId = order1.Id, AgentName = "Carlos Rodríguez", NoteType = "Resolution", Content = "Entrega completada exitosamente en segundo intento. Cliente satisfecho. Se ofreció 5% de descuento en próxima compra como gesto de buena voluntad.", CreatedAt = order1.OrderDate.AddDays(7), IsInternal = false, Priority = "Normal" },
            new() { OrderId = order1.Id, AgentName = "María González", NoteType = "Followup", Content = "Se llamó al cliente para confirmar satisfacción con los productos. Todo funcionando bien. Cliente mencionó interés en silla para home office.", CreatedAt = order1.OrderDate.AddDays(10), IsInternal = false, Priority = "Low", RequiresFollowup = true, FollowupDate = DateTime.UtcNow.AddDays(30) }
        };
        context.CustomerServiceNotes.AddRange(order1Notes);
        
        var order2 = new Order
        {
            OrderNumber = "ORD-2024-79123",
            CustomerId = customers[1].Id,
            OrderDate = DateTime.UtcNow.AddDays(-3),
            EstimatedDeliveryDate = DateTime.UtcNow.AddDays(4),
            CurrentStatus = "Processing",
            Subtotal = 15899850m,
            TaxAmount = 3020972m,
            ShippingCost = 150000m,
            DiscountAmount = 2384978m,
            TotalAmount = 16685844m,
            PaymentMethod = "BankTransfer",
            OrderSource = "Phone",
            SpecialInstructions = "Pedido mayorista para TechCorp Chile. Entregar en dock de carga. Requiere grúa horquilla.",
            IsGift = false
        };
        context.Orders.Add(order2);
        context.SaveChanges();
        
        var order2Items = new List<OrderItem>
        {
            new() { OrderId = order2.Id, ProductId = products[0].Id, Quantity = 5, UnitPrice = 1299990m, DiscountPercent = 15, LineTotal = 5524958m, Status = "Picking" },
            new() { OrderId = order2.Id, ProductId = products[2].Id, Quantity = 10, UnitPrice = 79990m, DiscountPercent = 15, LineTotal = 679915m, Status = "Picked" },
            new() { OrderId = order2.Id, ProductId = products[3].Id, Quantity = 5, UnitPrice = 149990m, DiscountPercent = 15, LineTotal = 637458m, Status = "Picked" },
            new() { OrderId = order2.Id, ProductId = products[5].Id, Quantity = 5, UnitPrice = 349990m, DiscountPercent = 15, LineTotal = 1487458m, Status = "Pending" },
            new() { OrderId = order2.Id, ProductId = products[7].Id, Quantity = 5, UnitPrice = 299990m, DiscountPercent = 15, LineTotal = 1274958m, Status = "Pending" },
            new() { OrderId = order2.Id, ProductId = products[8].Id, Quantity = 5, UnitPrice = 599990m, DiscountPercent = 15, LineTotal = 2549958m, Status = "Pending" },
            new() { OrderId = order2.Id, ProductId = products[9].Id, Quantity = 5, UnitPrice = 799990m, DiscountPercent = 15, LineTotal = 3399958m, Status = "Pending" }
        };
        context.OrderItems.AddRange(order2Items);
        
        var order2StatusHistory = new List<StatusHistory>
        {
            new() { OrderId = order2.Id, Status = "Pending", Timestamp = order2.OrderDate, UpdatedBy = "Carlos Rodríguez", Notes = "Pedido telefónico de TechCorp Chile - Ana Martínez", Location = "Call Center" },
            new() { OrderId = order2.Id, Status = "Awaiting Payment", Timestamp = order2.OrderDate.AddHours(1), UpdatedBy = "Finanzas", Notes = "Transferencia bancaria iniciada - Pago a 30 días", Location = "Santiago, Chile" },
            new() { OrderId = order2.Id, Status = "Payment Confirmed", Timestamp = order2.OrderDate.AddDays(1), UpdatedBy = "Finanzas", Notes = "Transferencia recibida - Banco Estado ref: 987654321", Location = "Santiago, Chile" },
            new() { OrderId = order2.Id, Status = "Processing", Timestamp = order2.OrderDate.AddDays(1).AddHours(2), UpdatedBy = "Sistema Bodega", Notes = "Pedido aprobado para preparación - cola de pedidos mayoristas", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order2.Id, Status = "Picking", Timestamp = order2.OrderDate.AddDays(2), UpdatedBy = "Equipo Bodega", Notes = "Picking parcial en progreso - sillas en backorder del proveedor, llegan en 2 días", Location = "Centro de Distribución Quilicura" }
        };
        context.StatusHistories.AddRange(order2StatusHistory);
        
        var order2Shipping = new ShippingInfo
        {
            OrderId = order2.Id,
            Carrier = "BluExpress Cargo",
            ServiceType = "Carga",
            TrackingNumber = "BLU-PENDIENTE",
            ShippingWeight = 185.0m,
            PackageCount = "2 pallets",
            ShippingAddressStreet = "Av. Andrés Bello 2711 - Dock de Carga",
            ShippingAddressCity = "Las Condes",
            ShippingAddressState = "Región Metropolitana",
            ShippingAddressPostalCode = "7550611",
            ShippingAddressCountry = "Chile",
            DeliveryInstructions = "Entregar en dock de carga B. Contacto: Ana Martínez anexo 4521. Requiere grúa horquilla."
        };
        context.ShippingInfos.Add(order2Shipping);
        
        var order2Payment = new PaymentDetail
        {
            OrderId = order2.Id,
            PaymentMethod = "BankTransfer",
            PaymentStatus = "Captured",
            TransactionId = "BT-2024-987654321",
            PaymentDate = order2.OrderDate.AddDays(1),
            AmountPaid = 16685844m,
            BillingAddressStreet = "Av. Andrés Bello 2711, Piso 12",
            BillingAddressCity = "Las Condes",
            BillingAddressState = "Región Metropolitana",
            BillingAddressPostalCode = "7550611",
            BillingAddressCountry = "Chile"
        };
        context.PaymentDetails.Add(order2Payment);
        
        var order2Notes = new List<CustomerServiceNote>
        {
            new() { OrderId = order2.Id, AgentName = "Carlos Rodríguez", NoteType = "General", Content = "Pedido mayorista - TechCorp Chile renovación trimestral. Ana Martínez (Jefa de Adquisiciones) realizó el pedido.", CreatedAt = order2.OrderDate, IsInternal = true, Priority = "Normal" },
            new() { OrderId = order2.Id, AgentName = "Carlos Rodríguez", NoteType = "General", Content = "Cliente solicitó factura detallada para efectos tributarios y aduaneros.", CreatedAt = order2.OrderDate.AddHours(2), IsInternal = false, Priority = "Normal" },
            new() { OrderId = order2.Id, AgentName = "María González", NoteType = "Inquiry", Content = "Cliente llamó para consultar estado. Se informó sobre backorder parcial de sillas. Cliente aceptó el retraso.", CreatedAt = order2.OrderDate.AddDays(2), IsInternal = false, Priority = "Normal" },
            new() { OrderId = order2.Id, AgentName = "Equipo Bodega", NoteType = "General", Content = "Proveedor de sillas confirma entrega a bodega según calendario. Se completará embalaje del pedido una vez recibidas.", CreatedAt = order2.OrderDate.AddDays(2).AddHours(5), IsInternal = true, Priority = "Normal", RequiresFollowup = true, FollowupDate = DateTime.UtcNow.AddDays(2) }
        };
        context.CustomerServiceNotes.AddRange(order2Notes);
        
        var order3 = new Order
        {
            OrderNumber = "ORD-2024-79456",
            CustomerId = customers[2].Id,
            OrderDate = DateTime.UtcNow.AddDays(-7),
            EstimatedDeliveryDate = DateTime.UtcNow.AddDays(-2),
            CurrentStatus = "Issue - Return Requested",
            Subtotal = 1749970m,
            TaxAmount = 332494m,
            ShippingCost = 15000m,
            DiscountAmount = 0m,
            TotalAmount = 2097464m,
            PaymentMethod = "PayPal",
            OrderSource = "Mobile",
            SpecialInstructions = "Dejar en portería si no hay nadie",
            IsGift = true,
            GiftMessage = "¡Feliz cumpleaños! Con cariño de toda la familia."
        };
        context.Orders.Add(order3);
        context.SaveChanges();
        
        var order3Items = new List<OrderItem>
        {
            new() { OrderId = order3.Id, ProductId = products[10].Id, Quantity = 1, UnitPrice = 1099990m, DiscountPercent = 0, LineTotal = 1099990m, Status = "Return Requested", ReturnReason = "Defecto de pantalla - píxeles muertos en esquina" },
            new() { OrderId = order3.Id, ProductId = products[11].Id, Quantity = 1, UnitPrice = 49990m, DiscountPercent = 0, LineTotal = 49990m, Status = "Delivered" },
            new() { OrderId = order3.Id, ProductId = products[12].Id, Quantity = 1, UnitPrice = 89990m, DiscountPercent = 0, LineTotal = 89990m, Status = "Delivered" },
            new() { OrderId = order3.Id, ProductId = products[13].Id, Quantity = 1, UnitPrice = 249990m, DiscountPercent = 0, LineTotal = 249990m, Status = "Delivered" },
            new() { OrderId = order3.Id, ProductId = products[14].Id, Quantity = 1, UnitPrice = 189990m, DiscountPercent = 0, LineTotal = 189990m, Status = "Delivered" }
        };
        context.OrderItems.AddRange(order3Items);
        
        var order3StatusHistory = new List<StatusHistory>
        {
            new() { OrderId = order3.Id, Status = "Pending", Timestamp = order3.OrderDate, UpdatedBy = "Sistema", Notes = "Pedido desde app móvil", Location = "Santiago, Chile" },
            new() { OrderId = order3.Id, Status = "Payment Confirmed", Timestamp = order3.OrderDate.AddMinutes(2), UpdatedBy = "PayPal", Notes = "Pago PayPal confirmado - ID: PP-8877665544", Location = "Santiago, Chile" },
            new() { OrderId = order3.Id, Status = "Processing", Timestamp = order3.OrderDate.AddHours(1), UpdatedBy = "Sistema Bodega", Notes = "Pedido en cola de preparación", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order3.Id, Status = "Picking", Timestamp = order3.OrderDate.AddHours(3), UpdatedBy = "Pedro Soto", Notes = "Productos recolectados", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order3.Id, Status = "Gift Wrapping", Timestamp = order3.OrderDate.AddHours(4), UpdatedBy = "Servicios Regalo", Notes = "Envoltorio de regalo aplicado con tarjeta de mensaje", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order3.Id, Status = "Packed", Timestamp = order3.OrderDate.AddHours(5), UpdatedBy = "Equipo Embalaje", Notes = "1 bulto - 1.5kg", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order3.Id, Status = "Shipped", Timestamp = order3.OrderDate.AddDays(1), UpdatedBy = "Starken", Notes = "Retirado por transportista", Location = "Centro de Distribución Quilicura" },
            new() { OrderId = order3.Id, Status = "In Transit", Timestamp = order3.OrderDate.AddDays(2), UpdatedBy = "Starken", Notes = "En tránsito", Location = "Hub Starken Santiago" },
            new() { OrderId = order3.Id, Status = "Delivered", Timestamp = order3.OrderDate.AddDays(3), UpdatedBy = "Starken", Notes = "Dejado en portería según instrucciones", Location = "Calle Los Aromos 567, Ñuñoa" },
            new() { OrderId = order3.Id, Status = "Issue Reported", Timestamp = order3.OrderDate.AddDays(4), UpdatedBy = "Cliente", Notes = "Cliente reportó defecto en pantalla de tablet vía app", Location = "Santiago, Chile" },
            new() { OrderId = order3.Id, Status = "Issue - Return Requested", Timestamp = order3.OrderDate.AddDays(4).AddHours(2), UpdatedBy = "María González", Notes = "Devolución autorizada - producto defectuoso. Enviando etiqueta de retorno prepagada.", Location = "Call Center" }
        };
        context.StatusHistories.AddRange(order3StatusHistory);
        
        var order3Shipping = new ShippingInfo
        {
            OrderId = order3.Id,
            Carrier = "Starken",
            ServiceType = "Estándar",
            TrackingNumber = "STK-112233445566",
            TrackingUrl = "https://www.starken.cl/seguimiento/STK-112233445566",
            ShippingWeight = 1.5m,
            PackageCount = "1",
            ShippingAddressStreet = "Calle Los Aromos 567",
            ShippingAddressCity = "Ñuñoa",
            ShippingAddressState = "Región Metropolitana",
            ShippingAddressPostalCode = "7750000",
            ShippingAddressCountry = "Chile",
            ShippedDate = order3.OrderDate.AddDays(1),
            DeliveredDate = order3.OrderDate.AddDays(3),
            DeliverySignature = "Dejado en portería",
            DeliveryInstructions = "Dejar en portería si no hay nadie"
        };
        context.ShippingInfos.Add(order3Shipping);
        
        var order3Payment = new PaymentDetail
        {
            OrderId = order3.Id,
            PaymentMethod = "PayPal",
            PaymentStatus = "Captured",
            TransactionId = "PP-8877665544",
            PaymentDate = order3.OrderDate.AddMinutes(2),
            AmountPaid = 2097464m,
            BillingAddressStreet = "Calle Los Aromos 567",
            BillingAddressCity = "Ñuñoa",
            BillingAddressState = "Región Metropolitana",
            BillingAddressPostalCode = "7750000",
            BillingAddressCountry = "Chile"
        };
        context.PaymentDetails.Add(order3Payment);
        
        var order3Notes = new List<CustomerServiceNote>
        {
            new() { OrderId = order3.Id, AgentName = "Sistema", NoteType = "General", Content = "Pedido regalo - asegurar envoltorio de regalo y tarjeta con mensaje", CreatedAt = order3.OrderDate, IsInternal = true, Priority = "Normal" },
            new() { OrderId = order3.Id, AgentName = "María González", NoteType = "Complaint", Content = "Cliente reportó que la tablet tiene píxeles muertos visibles en esquina superior derecha. Fotos recibidas por correo confirman el defecto.", CreatedAt = order3.OrderDate.AddDays(4), IsInternal = false, Priority = "High" },
            new() { OrderId = order3.Id, AgentName = "María González", NoteType = "Resolution", Content = "Devolución autorizada bajo garantía. Etiqueta de retorno Starken prepagada enviada al correo del cliente. Se enviará reemplazo una vez recibida la devolución.", CreatedAt = order3.OrderDate.AddDays(4).AddHours(2), IsInternal = false, Priority = "High" },
            new() { OrderId = order3.Id, AgentName = "María González", NoteType = "Followup", Content = "Se envió correo al cliente con instrucciones de devolución y etiqueta de seguimiento. Esperando envío de retorno.", CreatedAt = order3.OrderDate.AddDays(4).AddHours(3), IsInternal = false, Priority = "Normal", RequiresFollowup = true, FollowupDate = DateTime.UtcNow.AddDays(3) },
            new() { OrderId = order3.Id, AgentName = "Equipo QA", NoteType = "General", Content = "INTERNO: Esta es la 3ra devolución de ProTab 12.9 este mes por defectos de pantalla. Marcado para revisión de calidad con proveedor.", CreatedAt = order3.OrderDate.AddDays(4).AddHours(5), IsInternal = true, Priority = "Urgent" }
        };
        context.CustomerServiceNotes.AddRange(order3Notes);

        context.SaveChanges();
    }
}