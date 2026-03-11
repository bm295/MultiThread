using LaCheminee.FnB.Application.UseCases;

namespace LaCheminee.FnB.Adapters.Api;

public sealed class ConsoleDemoRunner(RestaurantService service)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await service.Seed150SeatsLayoutAsync(cancellationToken);

        await service.AddMenuItemAsync("A01", "Soupe à l'oignon", "Starter", 9.5m, cancellationToken);
        await service.AddMenuItemAsync("M01", "Steak Frites", "Main", 24.0m, cancellationToken);
        await service.AddMenuItemAsync("D01", "Crème brûlée", "Dessert", 8.0m, cancellationToken);

        var order = await service.OpenOrderAsync(3, "Marie", cancellationToken);
        await service.AddItemToOrderAsync(order.Id, "A01", 2, cancellationToken: cancellationToken);
        await service.AddItemToOrderAsync(order.Id, "M01", 2, "Medium rare", cancellationToken);
        await service.AddItemToOrderAsync(order.Id, "D01", 1, cancellationToken: cancellationToken);
        await service.RemoveItemFromOrderAsync(order.Id, "D01", 1, cancellationToken);
        await service.AddItemToOrderAsync(order.Id, "D01", 1, cancellationToken: cancellationToken);

        await service.SendOrderToKitchenAsync(order.Id, cancellationToken);
        var bill = await service.CloseAndPayAsync(order.Id, "Card", cancellationToken: cancellationToken);

        var occupancy = await service.GetOccupancyReportAsync(cancellationToken);

        Console.WriteLine("=== La Cheminée F&B (.NET 10 / C#) ===");
        Console.WriteLine($"Order #{order.Id} - Table {order.TableNumber}");
        Console.WriteLine($"Subtotal: {bill.Subtotal:0.00}");
        Console.WriteLine($"Service Charge: {bill.ServiceCharge:0.00}");
        Console.WriteLine($"VAT: {bill.Vat:0.00}");
        Console.WriteLine($"Total: {bill.Total:0.00}");
        Console.WriteLine($"Occupied Table Rate: {occupancy.OccupiedTableRate}%");
        Console.WriteLine($"Occupied Seat Rate: {occupancy.OccupiedSeatRate}%");
    }
}
