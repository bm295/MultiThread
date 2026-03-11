using LaCheminee.FnB.Domain.Enums;

namespace LaCheminee.FnB.Domain.Entities;

public sealed class Order(int id, int tableNumber, string staff)
{
    public int Id { get; } = id;
    public int TableNumber { get; } = tableNumber;
    public string Staff { get; } = staff;
    public DateTimeOffset OpenedAt { get; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Open;
    public List<OrderItem> Items { get; } = [];

    public decimal Subtotal => Items.Sum(i => i.Amount);

    public void AddItem(MenuItem menuItem, int quantity, string note)
    {
        EnsureMutable();
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        Items.Add(new OrderItem(menuItem, quantity, note));
    }

    public void RemoveItem(string menuCode, int quantity)
    {
        EnsureMutable();
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        var existing = Items.LastOrDefault(x => string.Equals(x.Item.Code, menuCode, StringComparison.OrdinalIgnoreCase));
        if (existing is null)
        {
            throw new KeyNotFoundException($"Menu item '{menuCode}' does not exist in order.");
        }

        Items.Remove(existing);
        var remaining = existing.Quantity - quantity;
        if (remaining > 0)
        {
            Items.Add(existing with { Quantity = remaining });
        }
    }

    public void SendToKitchen()
    {
        if (Items.Count == 0)
        {
            throw new InvalidOperationException("Cannot send an empty order to kitchen.");
        }

        if (Status != OrderStatus.Open)
        {
            throw new InvalidOperationException("Only open orders can be sent to kitchen.");
        }

        Status = OrderStatus.SentToKitchen;
    }

    public void Close()
    {
        if (Status != OrderStatus.SentToKitchen && Status != OrderStatus.Open)
        {
            throw new InvalidOperationException("Order is not in a closable state.");
        }

        Status = OrderStatus.Closed;
    }

    public void MarkPaid()
    {
        if (Status != OrderStatus.Closed)
        {
            throw new InvalidOperationException("Order must be closed before payment.");
        }

        Status = OrderStatus.Paid;
    }

    private void EnsureMutable()
    {
        if (Status is OrderStatus.Closed or OrderStatus.Paid)
        {
            throw new InvalidOperationException("Cannot modify a closed/paid order.");
        }
    }
}
