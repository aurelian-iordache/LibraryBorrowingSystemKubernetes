namespace CommunicationShared
{
    public record BookOrderCreatedEvent
    {
        public Guid OrderId { get; init; }
        public Guid CustomerId { get; init; }
        public string CustomerName { get; init; }
        public DateTime OrderDate { get; init; }
        public string Status { get; init; }
        public bool OrderConfirmed { get; init; }
        public List<BookDto> Books { get; init; }

        // Parameterless constructor required by MassTransit
        public BookOrderCreatedEvent() { }

        // Constructor for record immutability
        public BookOrderCreatedEvent(
            Guid orderId,
            Guid customerId,
            string customerName,
            DateTime orderDate,
            string status,
            List<BookDto> books)
        {
            OrderId = orderId;
            CustomerId = customerId;
            CustomerName = customerName;
            OrderDate = orderDate;
            Status = status;
            Books = books;
        }
    }

    public record BookDto(
        Guid BookId,
        string BookTitle,
        bool IsInStock
    );
}