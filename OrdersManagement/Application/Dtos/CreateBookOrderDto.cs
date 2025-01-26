using CommunicationShared;

namespace OrdersManagement.Application.Dtos;

public record CreateBookOrderDto(
    Guid OrderId,
    DateTime OrderDate,
    string Status, // "Created", "Checked", "Confirmed", "Cancelled", "Final"
    Guid CustomerId,
    string CustomerName,
    string CustomerEmail,
    string CustomerPhone,
    string ShippingAddress,
    string City,
    string State,
    string Country,
    string PostalCode,
    DateTime? EstimatedDeliveryDate,
    List<BookDto> Books,
    string Notes
);