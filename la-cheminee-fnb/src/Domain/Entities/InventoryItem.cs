namespace LaCheminee.FnB.Domain.Entities;

public sealed class InventoryItem(string menuCode, int quantity)
{
    public string MenuCode { get; } = menuCode;
    public int Quantity { get; private set; } = quantity;

    public void Deduct(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (Quantity < amount)
        {
            throw new InvalidOperationException($"Insufficient inventory for menu item '{MenuCode}'.");
        }

        Quantity -= amount;
    }

    public void Restock(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Quantity += amount;
    }
}
