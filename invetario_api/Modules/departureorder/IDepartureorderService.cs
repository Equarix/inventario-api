using invetario_api.Modules.departureorder.dto;
using invetario_api.Modules.departureorder.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.departureorder
{
    public interface IDepartureorderService
    {
        Task<List<Departureorder>> getDepartureorders();

        Task<Departureorder?> getDepartureorderById(int departureorderId);
        
        Task<Departureorder> createDepartureorder(DepartureorderDto data);

        Task<Departureorder?> updateDepartureorder(int departureorderId, UpdateDepartureorderDto data);

        Task<Departureorder?> deleteDepartureorder(int departureorderId);
    }
}
