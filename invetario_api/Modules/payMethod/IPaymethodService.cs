using invetario_api.Modules.payMethod.dto;
using invetario_api.Modules.payMethod.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.payMethod
{
    public interface IPaymethodService
    {
        Task<List<Paymethod>> getPaymethods();

        Task<Paymethod?> getPaymethodById(int payMethodId);
        
        Task<Paymethod> createPaymethod(PaymethodDto data);

        Task<Paymethod?> updatePaymethod(int payMethodId, UpdatePaymethodDto data);

        Task<Paymethod?> deletePaymethod(int payMethodId);
    }
}
