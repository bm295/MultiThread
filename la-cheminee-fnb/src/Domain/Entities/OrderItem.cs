namespace LaCheminee.FnB.Domain.Entities;

public sealed record OrderItem(MenuItem Item, int Quantity, string Note)
{
    public decimal Amount => Item.Price * Quantity;
}
