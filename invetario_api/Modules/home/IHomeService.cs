using System;

namespace invetario_api.Modules.home;

public interface IHomeService
{
    Task<List<object>> getTrend();

    Task<object> getKpi();

    Task<List<object>> getProductsTop();

    Task<List<object>> getCriticalProducts();

    Task<List<object>> getCategoriesTop();
}
