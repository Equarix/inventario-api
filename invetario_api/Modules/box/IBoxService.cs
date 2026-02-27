using invetario_api.Modules.box.dto;
using invetario_api.Modules.box.entity;
using invetario_api.Modules.box.response;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.box
{
    public interface IBoxService
    {
        Task<List<BoxResponse>> getBoxs();

        Task<BoxResponse?> getBoxById(int boxId);

        Task<BoxResponse> openBox(BoxDto data);

        Task<BoxResponse?> closeBox(int boxId);
    }
}
