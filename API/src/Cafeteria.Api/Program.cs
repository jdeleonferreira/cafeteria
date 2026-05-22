using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cafeteria.Application.Orders;
using Cafeteria.Infrastructure;
using Cafeteria.Application.Payments;

var builder = WebApplication.CreateBuilder(args);

// Add application services
builder.Services.AddInfrastructure();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PaymentService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("Cafeteria API"));

app.MapPost("/orders", async (OrderService service, CreateOrderRequest request) =>
{
    var id = await service.PlaceOrderAsync(request);
    return Results.Created($"/orders/{id}", id);
});

app.MapGet("/orders/{id}", async (OrderService service, Guid id) =>
{
    var dto = await service.GetByIdAsync(id);
    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

app.MapGet("/orders", async (OrderService service) =>
{
    var list = await service.ListAsync();
    return Results.Ok(list);
});

app.MapPost("/orders/{id}/cancel", async (OrderService service, Guid id) =>
{
    try
    {
        await service.CancelOrderAsync(id);
        return Results.NoContent();
    }
    catch (InvalidOperationException)
    {
        return Results.NotFound();
    }
});

// Payments
app.MapPost("/payments", async (PaymentService svc, CreatePaymentRequest req) =>
{
    var id = await svc.RegisterPaymentAsync(req);
    return Results.Created($"/payments/{id}", id);
});

app.MapPost("/payments/{id}/complete", async (PaymentService svc, Guid id, string? transactionId) =>
{
    try
    {
        await svc.MarkCompletedAsync(id, transactionId, DateTime.UtcNow);
        return Results.NoContent();
    }
    catch (InvalidOperationException)
    {
        return Results.NotFound();
    }
});

app.MapPost("/payments/{id}/fail", async (PaymentService svc, Guid id, string reason) =>
{
    try
    {
        await svc.MarkFailedAsync(id, reason);
        return Results.NoContent();
    }
    catch (InvalidOperationException)
    {
        return Results.NotFound();
    }
});

app.MapGet("/payments/{id}", async (PaymentService svc, Guid id) =>
{
    var dto = await svc.GetByIdAsync(id);
    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

app.MapGet("/payments", async (PaymentService svc) =>
{
    var list = await svc.ListAsync();
    return Results.Ok(list);
});

app.Run();
