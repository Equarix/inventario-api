using invetario_api.Modules.sale.dto;
using invetario_api.Modules.sale.entity;
using invetario_api.Modules.sale.response;
using invetario_api.utils;
using invetario_api.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.sale
{
    public interface ISaleService
    {
        Task<PageResult<List<SaleResponse>>> getSales(PaginateDto paginate);

        Task<SaleResponse?> getSaleById(int saleId);

        Task<SaleResponse> createSale(SaleDto data);

        Task<SaleResponse?> deleteSale(int saleId);
    }
}
