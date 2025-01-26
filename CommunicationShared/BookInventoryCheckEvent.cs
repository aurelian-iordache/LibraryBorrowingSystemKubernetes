namespace CommunicationShared;

public record BookInventoryCheckEvent(Guid OrderId, Guid ProductId, bool OrderConfirmed);