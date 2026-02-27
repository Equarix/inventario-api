using invetario_api.Modules.entryorder.dto;
using invetario_api.Modules.entryorder.entity;
using invetario_api.Modules.entryorder.response;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.entryorder
{
    public interface IEntryorderService
    {
        Task<List<EntryOrderResponse>> getEntryorders();

        Task<EntryOrderResponse?> getEntryorderById(int entryorderId);

        Task<EntryOrderResponse> createEntryorder(EntryorderDto data);

        Task<EntryOrderResponse?> completeEntryorder(int entryorderId);
        Task<EntryOrderResponse?> cancelEntryorder(int entryorderId);

        Task<List<EntryOrderResponse>> getPendingEntryorder();
    }
}
