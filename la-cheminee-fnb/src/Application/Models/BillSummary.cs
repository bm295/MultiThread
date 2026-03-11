namespace LaCheminee.FnB.Application.Models;

public sealed record BillSummary(decimal Subtotal, decimal ServiceCharge, decimal Vat, decimal Total);
