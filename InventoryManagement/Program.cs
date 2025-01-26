using InventoryManagement.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        cfg.Host(configuration["RabbitMQ:HostName"], "/", h =>
        {
            h.Username(configuration["RabbitMQ:UserName"]);
            h.Password(configuration["RabbitMQ:Password"]);
        });

        cfg.ReceiveEndpoint("order-created-queue-inventory", e =>
        {
            e.Consumer<BookOrderCreatedConsumer>();
        });

        cfg.ReceiveEndpoint("inventory-check-queue", e =>
        {
            e.Consumer<BookInventoryCheckConsumer>();
        });

    });

});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.Run();