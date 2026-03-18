using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.payMethod.dto;
using invetario_api.Modules.payMethod.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.payMethod
{
    public class PaymethodService : IPaymethodService
    {
        private Database _db;

        public PaymethodService(Database db)
        {
            _db = db;
        }

        public async Task<List<Paymethod>> getPaymethods()
        {
            return await _db.payMethods.ToListAsync();
        }

        public async Task<Paymethod> createPaymethod(PaymethodDto data)
        {
            var newPaymethod = new Paymethod
            {
                name = data.name,
                turned = data.turned
            };

            _db.payMethods.Add(newPaymethod);
            await _db.SaveChangesAsync();

            return newPaymethod;
        }

        public async Task<Paymethod?> deletePaymethod(int payMethodId)
        {
            var findPaymethod = await _db.payMethods.Where(p => p.paymethodId == payMethodId).FirstOrDefaultAsync();
            if (findPaymethod == null)
            {
                throw new HttpException(404, "Pay method not found");
            }

            findPaymethod.status = false;
            await _db.SaveChangesAsync();
            return findPaymethod;
        }

        public async Task<Paymethod?> getPaymethodById(int payMethodId)
        {
            var findPaymethod = await _db.payMethods.Where(p => p.paymethodId == payMethodId).FirstOrDefaultAsync();
            if (findPaymethod == null)
            {
                throw new HttpException(404, "Pay method not found");
            }

            return findPaymethod;
        }

        public async Task<Paymethod?> updatePaymethod(int payMethodId, UpdatePaymethodDto data)
        {
            var findPaymethod = await _db.payMethods.Where(p => p.paymethodId == payMethodId).FirstOrDefaultAsync();
            if (findPaymethod == null)
            {
                throw new HttpException(404, "Pay method not found");
            }

            findPaymethod.name = data.name;
            findPaymethod.turned = data.turned;
            findPaymethod.status = data.status!.Value;
            await _db.SaveChangesAsync();
            return findPaymethod;
        }

        public Task<List<Paymethod>> getPaymethodsActive()
        {
            var paymethods = _db.payMethods.Where(p => p.status == true).ToListAsync();
            return paymethods;
        }
    }
}
