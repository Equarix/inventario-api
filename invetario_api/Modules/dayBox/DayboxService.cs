using invetario_api.database;
using invetario_api.Modules.dayBox.dto;
using invetario_api.Modules.dayBox.entity;
using invetario_api.utils;
using invetario_api.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.dayBox
{
    public class DayboxService : IDayboxService
    {
        private Database _db;

        public DayboxService(Database db) { 
            _db = db;
        }

        public async Task<PageResult<List<Daybox>>> getDayboxs(PaginateDto paginate)
        {
            throw new NotImplementedException();
        }

        public async Task<Daybox> createDaybox(DayboxDto data)
        {   
            throw new NotImplementedException();
        }

        public async Task<Daybox?> deleteDaybox(int dayBoxId)
        {
            throw new NotImplementedException();
        }

        public async Task<Daybox?> getDayboxById(int dayBoxId)
        {
            throw new NotImplementedException();
        }

        public async Task<Daybox?> updateDaybox(int dayBoxId, UpdateDayboxDto data)
        {
            throw new NotImplementedException();
        }
    }
}
