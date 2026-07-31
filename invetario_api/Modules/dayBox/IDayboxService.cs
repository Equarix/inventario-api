using invetario_api.Modules.dayBox.dto;
using invetario_api.Modules.dayBox.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using invetario_api.Utils;

namespace invetario_api.Modules.dayBox
{
    public interface IDayboxService
    {
        Task<PageResult<List<Daybox>>> getDayboxs(PaginateDto paginate);

        Task<Daybox?> getDayboxById(int dayBoxId);
        
        Task<Daybox> createDaybox(DayboxDto data);

        Task<Daybox?> updateDaybox(int dayBoxId, UpdateDayboxDto data);

        Task<Daybox?> deleteDaybox(int dayBoxId);
    }
}
