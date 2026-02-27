using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.box.dto;
using invetario_api.Modules.box.entity;
using invetario_api.Modules.box.response;
using invetario_api.Modules.users.current_user;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace invetario_api.Modules.box
{
    public class BoxService : IBoxService
    {
        private Database _db;
        private readonly ICurrentUserService _currentUser;

        public BoxService(Database db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<List<BoxResponse>> getBoxs()
        {
            var boxs = await _db.boxs.Include(b => b.userActual).Include(b => b.userOpening).Include(b => b.userClosing).ToListAsync();
            return BoxResponse.FromBoxEntityList(boxs);
        }

        public async Task<BoxResponse> openBox(BoxDto data)
        {

            var userId = this._currentUser.UserId;

            var findUser = await _db.users.Where(u => u.userId == userId).FirstOrDefaultAsync();

            if (findUser == null)
            {
                throw new HttpException(404, "User not found");
            }

            var findOpenBoxUser = await _db.users.Where(u => u.userId == data.userActualId).FirstOrDefaultAsync();

            if (findOpenBoxUser == null)
            {
                throw new HttpException(404, "User not found");
            }

            var newBox = new Box
            {
                amountOpening = data.amountOpening,
                userActualId = data.userActualId,
                userActual = findOpenBoxUser,
                userOpeningId = userId!.Value,
                userOpening = findUser,
            };

            _db.boxs.Add(newBox);
            await _db.SaveChangesAsync();
            return BoxResponse.FromBoxEntity(newBox);
        }

        public async Task<BoxResponse?> closeBox(int boxId)
        {
            var userId = this._currentUser.UserId;

            var findUser = await _db.users.Where(u => u.userId == userId).FirstOrDefaultAsync();

            if (findUser == null)
            {
                throw new HttpException(404, "User not found");
            }

            var findBox = await _db.boxs.Where(b => b.boxId == boxId).FirstOrDefaultAsync();

            if (findBox == null)
            {
                throw new HttpException(404, "Box not found");
            }

            throw new NotImplementedException();
        }

        public async Task<BoxResponse?> getBoxById(int boxId)
        {
            throw new NotImplementedException();
        }
    }
}
