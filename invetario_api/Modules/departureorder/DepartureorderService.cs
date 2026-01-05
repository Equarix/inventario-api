using invetario_api.database;
using invetario_api.Modules.departureorder.dto;
using invetario_api.Modules.departureorder.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.departureorder
{
    public class DepartureorderService : IDepartureorderService
    {
        private Database _db;

        public DepartureorderService(Database db) { 
            _db = db;
        }

        public async Task<List<Departureorder>> getDepartureorders()
        {
            throw new NotImplementedException();
        }

        public async Task<Departureorder> createDepartureorder(DepartureorderDto data)
        {   
            throw new NotImplementedException();
        }

        public async Task<Departureorder?> deleteDepartureorder(int departureorderId)
        {
            throw new NotImplementedException();
        }

        public async Task<Departureorder?> getDepartureorderById(int departureorderId)
        {
            throw new NotImplementedException();
        }

        public async Task<Departureorder?> updateDepartureorder(int departureorderId, UpdateDepartureorderDto data)
        {
            throw new NotImplementedException();
        }
    }
}
