using LaCheminee.FnB.Domain.Enums;

namespace LaCheminee.FnB.Domain.Entities;

public sealed class Table(int number, string area, int capacity)
{
    public int Number { get; } = number;
    public string Area { get; } = area;
    public int Capacity { get; } = capacity;
    public TableStatus Status { get; private set; } = TableStatus.Available;

    public void Reserve() => Status = TableStatus.Reserved;
    public void Occupy() => Status = TableStatus.Occupied;
    public void Release() => Status = TableStatus.Available;
}
