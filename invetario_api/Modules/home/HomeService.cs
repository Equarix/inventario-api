using System;
using invetario_api.database;
using invetario_api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace invetario_api.Modules.home;

public class HomeService : IHomeService
{
    private Database _db;

    public HomeService(Database db)
    {
        _db = db;
    }


    public async Task<List<object>> getCategoriesTop()
    {
        using (var con = _db.Database.GetDbConnection())
        {
            await con.OpenAsync();

            using var cmd = con.CreateCommand();

            cmd.CommandText = "exec sp_Top5CategoriasPorPrecioSell";

            using var reader = await cmd.ExecuteReaderAsync();

            var categoriesTop = new List<object>();

            while (await reader.ReadAsync())
            {
                var position = reader.GetInt64(0);
                var name = reader.GetString(1);
                var value = reader.GetDecimal(2);
                var color = reader.GetString(3);
                var totalPriceSell = reader.GetDecimal(4);
                categoriesTop.Add(new { position, name, value, color, totalPriceSell });
            }
            await con.CloseAsync();
            return categoriesTop;
        }
    }

    public async Task<List<object>> getCriticalProducts()
    {
        using (var con = _db.Database.GetDbConnection())
        {
            await con.OpenAsync();

            using var cmd = con.CreateCommand();

            cmd.CommandText = "exec getCriticalProducts";

            using var reader = await cmd.ExecuteReaderAsync();

            var criticalProducts = new List<object>();

            while (await reader.ReadAsync())
            {
                var name = reader.GetString(0);
                var stock = reader.GetInt32(1);
                var min = reader.GetInt32(2);
                var status = reader.GetString(3);
                var store = reader.GetString(4);
                criticalProducts.Add(new { name, stock, min, status, store });
            }
            await con.CloseAsync();
            return criticalProducts;
        }
    }

    public async Task<object> getKpi()
    {
        using (var con = _db.Database.GetDbConnection())
        {
            await con.OpenAsync();

            using var cmd = con.CreateCommand();

            cmd.CommandText = "exec getKpiInventory";

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new
                {
                    total_inventory_value = reader.GetDouble(0),
                    out_of_stock_products = reader.GetInt32(1),
                    entry_order_pending = reader.GetInt32(2),
                    sales_this_month = reader.GetInt32(3),
                };
            }
            await con.CloseAsync();
            throw new HttpException(404, "No se pudieron obtener los KPI");
        }
    }

    public async Task<List<object>> getProductsTop()
    {
        using (var con = _db.Database.GetDbConnection())
        {
            await con.OpenAsync();

            using var cmd = con.CreateCommand();

            cmd.CommandText = "exec getTopProducts";

            using var reader = await cmd.ExecuteReaderAsync();

            var productsTop = new List<object>();

            while (await reader.ReadAsync())
            {
                var name = reader.GetString(0);
                var value = reader.GetInt32(1);
                productsTop.Add(new { name, value });
            }
            await con.CloseAsync();
            return productsTop;
        }
    }

    public async Task<List<object>> getTrend()
    {
        var trend = new List<object>();

        using (var con = _db.Database.GetDbConnection())
        {
            await con.OpenAsync();

            using var cmd = con.CreateCommand();

            cmd.CommandText = "exec salesTrendData";

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var name = reader.GetString(0);
                var sales = reader.GetInt32(1);
                var revenue = reader.GetDouble(2);
                trend.Add(new { name, sales, revenue });
            }
            await con.CloseAsync();
        }

        return trend;
    }
}
