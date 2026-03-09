using System.Globalization;

namespace LaCheminee.FnB;

public enum TableStatus
{
    Available,
    Occupied,
    Reserved
}

public enum OrderStatus
{
    Open,
    SentToKitchen,
    Completed,
    Paid
}

public sealed record MenuItem(string Code, string Name, string Category, decimal Price);

public sealed class Table(int number, string area, int capacity)
{
    public int Number { get; } = number;
    public string Area { get; } = area;
    public int Capacity { get; } = capacity;
    public TableStatus Status { get; set; } = TableStatus.Available;
}

public sealed record OrderItem(MenuItem Item, int Quantity, string Note)
{
    public decimal Amount => Item.Price * Quantity;
}

public sealed class Order(int id, int tableNumber, string staff)
{
    public int Id { get; } = id;
    public int TableNumber { get; } = tableNumber;
    public string Staff { get; } = staff;
    public DateTime OpenedAt { get; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Open;
    public List<OrderItem> Items { get; } = [];

    public decimal Subtotal => Items.Sum(i => i.Amount);

    public void AddItem(MenuItem menuItem, int quantity, string note = "")
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Số lượng phải lớn hơn 0.");
        }

        Items.Add(new OrderItem(menuItem, quantity, note));
    }
}

public sealed record BillSummary(decimal Subtotal, decimal ServiceCharge, decimal Vat, decimal Total);

public sealed class FnbSystem
{
    private readonly Dictionary<int, Table> _tables = [];
    private readonly Dictionary<string, MenuItem> _menu = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<int, Order> _orders = [];
    private int _nextOrderId = 1;

    public IReadOnlyDictionary<int, Table> Tables => _tables;
    public IReadOnlyDictionary<string, MenuItem> Menu => _menu;
    public IReadOnlyDictionary<int, Order> Orders => _orders;

    public void Seed150SeatsLayout()
    {
        var distribution = new Dictionary<string, int[]>
        {
            ["Indoor"] = Enumerable.Repeat(4, 16).ToArray(),
            ["Terrace"] = Enumerable.Repeat(2, 10).ToArray(),
            ["Private"] = Enumerable.Repeat(6, 11).ToArray()
        };

        _tables.Clear();

        var tableNo = 1;
        foreach (var (area, capacities) in distribution)
        {
            foreach (var cap in capacities)
            {
                _tables[tableNo] = new Table(tableNo, area, cap);
                tableNo++;
            }
        }
    }

    public void AddMenuItem(string code, string name, string category, decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Giá món phải lớn hơn 0.");
        }

        _menu[code] = new MenuItem(code, name, category, price);
    }

    public Order OpenOrder(int tableNumber, string staff)
    {
        var table = GetTable(tableNumber);
        if (table.Status is not (TableStatus.Available or TableStatus.Reserved))
        {
            throw new InvalidOperationException($"Bàn {tableNumber} không sẵn sàng.");
        }

        table.Status = TableStatus.Occupied;

        var order = new Order(_nextOrderId++, tableNumber, staff);
        _orders[order.Id] = order;

        return order;
    }

    public void AddItemToOrder(int orderId, string menuCode, int quantity, string note = "")
    {
        var order = GetOrder(orderId);
        if (order.Status is OrderStatus.Completed or OrderStatus.Paid)
        {
            throw new InvalidOperationException("Không thể thêm món khi order đã hoàn tất hoặc thanh toán.");
        }

        var menuItem = GetMenuItem(menuCode);
        order.AddItem(menuItem, quantity, note);
    }

    public void SendToKitchen(int orderId)
    {
        var order = GetOrder(orderId);
        if (order.Items.Count == 0)
        {
            throw new InvalidOperationException("Order chưa có món.");
        }

        order.Status = OrderStatus.SentToKitchen;
    }

    public BillSummary CloseAndPay(int orderId, decimal serviceChargeRate = 0.05m, decimal vatRate = 0.10m)
    {
        var order = GetOrder(orderId);
        if (order.Status == OrderStatus.Paid)
        {
            throw new InvalidOperationException("Order đã thanh toán.");
        }

        var subtotal = order.Subtotal;
        var serviceCharge = subtotal * serviceChargeRate;
        var vat = (subtotal + serviceCharge) * vatRate;
        var total = subtotal + serviceCharge + vat;

        order.Status = OrderStatus.Paid;
        _tables[order.TableNumber].Status = TableStatus.Available;

        return new BillSummary(
            decimal.Round(subtotal, 2),
            decimal.Round(serviceCharge, 2),
            decimal.Round(vat, 2),
            decimal.Round(total, 2));
    }

    public (decimal OccupiedTableRate, decimal OccupiedSeatRate) OccupancyReport()
    {
        var totalTables = _tables.Count;
        var occupiedTables = _tables.Values.Count(t => t.Status == TableStatus.Occupied);

        var totalSeats = _tables.Values.Sum(t => t.Capacity);
        var occupiedSeats = _tables.Values.Where(t => t.Status == TableStatus.Occupied).Sum(t => t.Capacity);

        var occupiedTableRate = totalTables == 0 ? 0 : decimal.Round((decimal)occupiedTables / totalTables * 100, 2);
        var occupiedSeatRate = totalSeats == 0 ? 0 : decimal.Round((decimal)occupiedSeats / totalSeats * 100, 2);

        return (occupiedTableRate, occupiedSeatRate);
    }

    private Table GetTable(int tableNumber)
    {
        return _tables.TryGetValue(tableNumber, out var table)
            ? table
            : throw new KeyNotFoundException($"Không tìm thấy bàn {tableNumber}.");
    }

    private MenuItem GetMenuItem(string menuCode)
    {
        return _menu.TryGetValue(menuCode, out var item)
            ? item
            : throw new KeyNotFoundException($"Không tìm thấy món có mã '{menuCode}'.");
    }

    private Order GetOrder(int orderId)
    {
        return _orders.TryGetValue(orderId, out var order)
            ? order
            : throw new KeyNotFoundException($"Không tìm thấy order {orderId}.");
    }
}

public static class Program
{
    public static void Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        var system = new FnbSystem();
        system.Seed150SeatsLayout();

        system.AddMenuItem("A01", "Soupe à l'oignon", "Starter", 9.5m);
        system.AddMenuItem("M01", "Steak Frites", "Main", 24.0m);
        system.AddMenuItem("D01", "Crème brûlée", "Dessert", 8.0m);

        var order = system.OpenOrder(3, "Marie");
        system.AddItemToOrder(order.Id, "A01", 2);
        system.AddItemToOrder(order.Id, "M01", 2, "Medium rare");
        system.AddItemToOrder(order.Id, "D01", 1);

        system.SendToKitchen(order.Id);
        var bill = system.CloseAndPay(order.Id);

        var (occupiedTableRate, occupiedSeatRate) = system.OccupancyReport();

        Console.WriteLine("=== La Cheminée F&B (.NET 10 / C#) ===");
        Console.WriteLine($"Order #{order.Id} - Table {order.TableNumber}");
        Console.WriteLine($"Subtotal: {bill.Subtotal}");
        Console.WriteLine($"Service Charge: {bill.ServiceCharge}");
        Console.WriteLine($"VAT: {bill.Vat}");
        Console.WriteLine($"Total: {bill.Total}");
        Console.WriteLine($"Occupied Table Rate: {occupiedTableRate}%");
        Console.WriteLine($"Occupied Seat Rate: {occupiedSeatRate}%");
    }
}
