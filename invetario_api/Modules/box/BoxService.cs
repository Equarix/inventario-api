using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.box.dto;
using invetario_api.Modules.box.entity;
using invetario_api.Modules.box.response;
using invetario_api.utils;
using invetario_api.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.box
{
    public class BoxService : IBoxService
    {
        private Database _db;

        public BoxService(Database db)
        {
            _db = db;
        }

        public async Task<PageResult<List<BoxResponse>>> getBoxs(PaginateDto paginate)
        {

            var query = _db.boxs.AsQueryable();

            var totalCount = await query.CountAsync();

            var boxs = await query
                .Include(b => b.store)
                .Skip((paginate.page - 1) * paginate.limit)
                .Take(paginate.limit)
                .ToListAsync();

            return new PageResult<List<BoxResponse>>
            {
                items = BoxResponse.fromEntityList(boxs),
                limit = paginate.limit,
                page = paginate.page,
                totalItems = totalCount
            };
        }

        public async Task<BoxResponse> createBox(BoxDto data)
        {

            var store = await _db.stores.FindAsync(data.storeId);

            if (store == null)
            {
                throw new HttpException(404, "Store not found");
            }

            var box = new Box
            {
                boxName = data.boxName,
                serie = data.serie,
                serieProforma = data.serieProforma,
                storeId = data.storeId.Value,
                store = store
            };

            _db.boxs.Add(box);
            await _db.SaveChangesAsync();

            return BoxResponse.fromEntity(box);
        }

        public async Task<BoxResponse?> deleteBox(int boxId)
        {
            var box = await _db.boxs.FindAsync(boxId);

            if (box == null)
            {
                throw new HttpException(404, "Box not found");
            }

            box.status = false;

            await _db.SaveChangesAsync();

            return BoxResponse.fromEntity(box);
        }

        public async Task<BoxResponse?> getBoxById(int boxId)
        {
            var box = await _db.boxs.FindAsync(boxId);

            if (box == null)
            {
                throw new HttpException(404, "Box not found");
            }

            return BoxResponse.fromEntity(box);
        }

        public async Task<BoxResponse?> updateBox(int boxId, UpdateBoxDto data)
        {
            var box = await _db.boxs.FindAsync(boxId);

            if (box == null)
            {
                throw new HttpException(404, "Box not found");
            }

            var store = await _db.stores.FindAsync(data.storeId);

            if (store == null)
            {
                throw new HttpException(404, "Store not found");
            }

            box.boxName = data.boxName;
            box.serie = data.serie;
            box.serieProforma = data.serieProforma;
            box.storeId = data.storeId.Value;
            box.store = store;

            await _db.SaveChangesAsync();

            return BoxResponse.fromEntity(box);
        }
    }
}
