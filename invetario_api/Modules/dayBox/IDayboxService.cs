using invetario_api.Modules.dayBox.dto;
using invetario_api.Modules.dayBox.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using invetario_api.Utils;
using invetario_api.Modules.dayBox.response;

namespace invetario_api.Modules.dayBox
{
    public interface IDayboxService
    {
        Task<PageResult<List<DayBoxResponse>>> getDayboxs(QueryDayBox paginate);

        Task<DayBoxResponse> createDaybox(DayboxDto data);

        Task<DayBoxResponse?> deleteDaybox(int dayBoxId);

        Task<DayBoxResponse?> getDayboxByDate(QueryDayBoxByDate query);

        Task<object> isCreateSales(int boxId);
    }
}
