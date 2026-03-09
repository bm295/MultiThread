# La Cheminée Restaurant - Phần mềm F&B (.NET 10 / C# 14)

Đã chuyển sang **C# (LangVersion preview cho C# 14)** và target **.NET 10** theo yêu cầu.

## Chức năng MVP
- Quản lý sơ đồ bàn khoảng **150 chỗ** theo khu vực (Indoor, Terrace, Private).
- Quản lý menu theo mã món.
- Mở order theo bàn.
- Thêm món vào order.
- Gửi bếp.
- Đóng order và thanh toán (service charge + VAT).
- Báo cáo tỷ lệ lấp đầy bàn/ghế.

## Cấu trúc
- `LaCheminee.FnB.csproj`: project console target `net10.0`.
- `Program.cs`: chứa model, service `FnbSystem`, và luồng demo.

## Cách chạy
```bash
dotnet run --project la-cheminee-fnb/LaCheminee.FnB.csproj
```

## Gợi ý mở rộng
- Tách domain/application/infrastructure theo Clean Architecture.
- Thêm API ASP.NET Core để phục vụ POS/KDS/web đặt bàn.
- Tích hợp phân quyền theo vai trò (waiter/cashier/manager/kitchen).
- Tích hợp thanh toán và in hoá đơn.
- Thêm persistence (SQL Server/PostgreSQL) và migration.
