using CommunicationShared;
using MassTransit;

namespace BorrowingManagement.Consumers;

public class OrderCreatedConsumer : IConsumer<BookOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<BookOrderCreatedEvent> context)
    {
        var message = context.Message;

        //process the OrderCreatedEvent
        Console.WriteLine($"Order Received: {message.OrderId}");
        Console.WriteLine($"Customer: {message.CustomerName}");
        Console.WriteLine($"Status: {message.Status}");

        //add logic to update inventory or other business rules here

        await Task.CompletedTask;
    }
}