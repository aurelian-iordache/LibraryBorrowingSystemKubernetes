using CommunicationShared;
using MassTransit;

namespace InventoryManagement.Consumers;

public class BookInventoryCheckConsumer:  IConsumer<BookInventoryCheckEvent>
{
    public async Task Consume(ConsumeContext<BookInventoryCheckEvent> context)
    {
        Console.WriteLine($"Check inventory for order {context.Message.OrderId}");
        await context.Publish<BookInventoryCheckedEvent>(new BookInventoryCheckedEvent(context.Message.OrderId));
    }
}