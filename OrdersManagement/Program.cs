using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Application.Dtos;
using OrdersManagement.Application.Sagas;
using OrdersManagement.Database;
using System.Reflection;
using CommunicationShared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<OrderSaga, OrderSagaState>()
     .EntityFrameworkRepository(r =>
     {
         r.ConcurrencyMode = ConcurrencyMode.Pessimistic;

         r.AddDbContext<DbContext, OrderStateDbContext>((provider, options) =>
         {
             var configuration = provider.GetRequiredService<IConfiguration>();
             var connectionString = configuration.GetConnectionString("DefaultConnection");

             options.UseSqlServer(connectionString, sqlOptions =>
             {
                 sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                 sqlOptions.MigrationsHistoryTable($"__{nameof(OrderStateDbContext)}");
             });
         });
     });

    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        cfg.Host(configuration["RabbitMQ:HostName"], "/", h =>
        {
            h.Username(configuration["RabbitMQ:UserName"]);
            h.Password(configuration["RabbitMQ:Password"]);
        });

        cfg.ReceiveEndpoint("order-saga-queue", e =>
        {
            e.StateMachineSaga<OrderSagaState>(context); //attach the saga
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/orders", async (CreateBookOrderDto newOrder, IPublishEndpoint publishEndpoint) =>
{
    var orderCreatedEvent = new BookOrderCreatedEvent(newOrder.OrderId, newOrder.CustomerId, newOrder.CustomerName, DateTime.UtcNow, newOrder.Status, newOrder.Books);
    await publishEndpoint.Publish(orderCreatedEvent);
    return Results.Created($"/orders/{newOrder.OrderId}", newOrder);
});

app.Run();