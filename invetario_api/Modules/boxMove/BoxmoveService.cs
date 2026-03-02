using invetario_api.database;
using invetario_api.Modules.boxMove.dto;
using invetario_api.Modules.boxMove.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.boxMove
{
    public class BoxmoveService : IBoxmoveService
    {
        private Database _db;

        public BoxmoveService(Database db) { 
            _db = db;
        }

        public async Task<List<Boxmove>> getBoxmoves()
        {
            throw new NotImplementedException();
        }

        public async Task<Boxmove> createBoxmove(BoxmoveDto data)
        {   
            throw new NotImplementedException();
        }

        public async Task<Boxmove?> deleteBoxmove(int boxMoveId)
        {
            throw new NotImplementedException();
        }

        public async Task<Boxmove?> getBoxmoveById(int boxMoveId)
        {
            throw new NotImplementedException();
        }

        public async Task<Boxmove?> updateBoxmove(int boxMoveId, UpdateBoxmoveDto data)
        {
            throw new NotImplementedException();
        }
    }
}
