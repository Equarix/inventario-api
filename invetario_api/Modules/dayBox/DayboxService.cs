using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.dayBox.dto;
using invetario_api.Modules.dayBox.entity;
using invetario_api.Modules.dayBox.response;
using invetario_api.Modules.users.current_user;
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
        private readonly ICurrentUserService _currentUserService;

        public DayboxService(Database db, ICurrentUserService currentUserService)
        {
            _db = db;
            _currentUserService = currentUserService;

        }

        public async Task<PageResult<List<DayBoxResponse>>> getDayboxs(QueryDayBox paginate)
        {
            var query = _db.dayBoxs.AsQueryable();

            if (paginate.date != null)
            {
                query = query.Where(d => d.date.Date == paginate.date);
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Include(d => d.box)
                .Include(d => d.user)
                .Skip((paginate.page - 1) * paginate.limit)
                .Take(paginate.limit)
            .ToListAsync();

            var dayBoxResponses = DayBoxResponse.fromEntityList(items);

            return new PageResult<List<DayBoxResponse>>
            {
                items = dayBoxResponses,
                totalItems = totalItems,
                limit = paginate.limit,
                page = paginate.page
            };
        }

        public async Task<DayBoxResponse> createDaybox(DayboxDto data)
        {
            var box = await _db.boxs.FindAsync(data.boxId);

            if (box == null)
            {
                throw new HttpException(404, "Box not found");
            }

            var currentUser = await _currentUserService.GetCurrentUser();

            var daybox = new Daybox
            {
                boxId = data.boxId.Value,
                totalefectivo = data.totalefectivo.Value,
                totalTarjeta = data.totalTarjeta.Value,
                observations = data.observations,
                date = data.date.Value,
                box = box,
                userId = currentUser.userId,
                user = currentUser
            };

            _db.dayBoxs.Add(daybox);
            await _db.SaveChangesAsync();

            return DayBoxResponse.fromEntity(daybox);
        }

        public async Task<DayBoxResponse?> deleteDaybox(int dayBoxId)
        {
            var daybox = await _db.dayBoxs.FindAsync(dayBoxId);

            if (daybox == null)
            {
                throw new HttpException(404, "Daybox not found");
            }

            daybox.status = false;
            await _db.SaveChangesAsync();

            return DayBoxResponse.fromEntity(daybox);
        }

        public async Task<DayBoxResponse?> getDayboxByDate(QueryDayBoxByDate query)
        {
            var daybox = await _db.dayBoxs
                .Include(d => d.box)
                .Include(d => d.user)
                .FirstOrDefaultAsync(d => d.date.Date == query.date.Value.Date && d.boxId == query.boxId.Value);

            if (daybox == null)
            {
                throw new HttpException(404, "Daybox not found");
            }

            return DayBoxResponse.fromEntity(daybox);
        }

        public async Task<object> isCreateSales(int boxId)
        {
            var today = DateTime.Now.Date;

            var dayboxtoday = await _db.dayBoxs
                .FirstOrDefaultAsync(d => d.date.Date == today && d.boxId == boxId);

            if (dayboxtoday != null)
            {
                return new { isCreateSales = false, message = "Ya se ha cerrado la caja del dia " + today };
            }

            var ayer = DateTime.Now.AddDays(-1).Date;

            var dayboxayer = await _db.dayBoxs
                .FirstOrDefaultAsync(d => d.date.Date == ayer && d.boxId == boxId);

            if (dayboxayer == null)
            {
                return new { isCreateSales = false, message = "Tiene que Cerrar Caja del dia " + ayer };
            }

            return new { isCreateSales = true, message = "Se puede crear la caja del dia " + today };
        }
    }
}
