using invetario_api.Modules.boxMove.dto;
using invetario_api.Modules.boxMove.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.boxMove
{
    public interface IBoxmoveService
    {
        Task<List<Boxmove>> getBoxmoves();

        Task<Boxmove?> getBoxmoveById(int boxMoveId);
        
        Task<Boxmove> createBoxmove(BoxmoveDto data);

        Task<Boxmove?> updateBoxmove(int boxMoveId, UpdateBoxmoveDto data);

        Task<Boxmove?> deleteBoxmove(int boxMoveId);
    }
}
