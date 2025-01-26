using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersManagement.Application.Sagas;

namespace OrdersManagement.Database;

public class OrderStateMap : SagaClassMap<OrderSagaState>
{
    protected override void Configure(EntityTypeBuilder<OrderSagaState> entity, ModelBuilder model)
    {
        entity.Property(x => x.CurrentState).HasMaxLength(64);
        entity.Property(x => x.CorrelationId);
        entity.Property(x => x.OrderCreatedDate);
        entity.Property(x => x.OrderProcessedDate);
        entity.Property(x => x.AllBooksInStock);
    }
}