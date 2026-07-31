using invetario_api.Modules.box.dto;
using invetario_api.Modules.box.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using invetario_api.Utils;
using invetario_api.Modules.box.response;

namespace invetario_api.Modules.box
{
    public interface IBoxService
    {
        Task<PageResult<List<BoxResponse>>> getBoxs(PaginateDto paginate);

        Task<BoxResponse?> getBoxById(int boxId);

        Task<BoxResponse> createBox(BoxDto data);

        Task<BoxResponse?> updateBox(int boxId, UpdateBoxDto data);

        Task<BoxResponse?> deleteBox(int boxId);

    }
}
