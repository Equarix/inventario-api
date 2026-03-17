using invetario_api.Modules.proforma.dto;
using invetario_api.Modules.proforma.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using invetario_api.Utils;
using invetario_api.Modules.proforma.response;

namespace invetario_api.Modules.proforma
{
    public interface IProformaService
    {
        Task<PageResult<List<ProformaResponse>>> getProformas(QueryDto paginate);

        Task<ProformaResponse?> getProformaById(int proformaId);

        Task<ProformaResponse> createProforma(ProformaDto data);
    }
}
