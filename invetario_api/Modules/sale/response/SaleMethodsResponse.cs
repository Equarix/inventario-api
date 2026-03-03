using System;
using invetario_api.Modules.payMethod.entity;
using invetario_api.Modules.sale.entity;

namespace invetario_api.Modules.sale.response;

public class SaleMethodsResponse
{
    public int saleMethodId { get; set; }
    public string methodPayment { get; set; }
    public float amount { get; set; }
    public Paymethod paymethod { get; set; }


    public static SaleMethodsResponse FromEntity(SaleMethods saleMethod)
    {
        return new SaleMethodsResponse
        {
            saleMethodId = saleMethod.saleMethodId,
            methodPayment = saleMethod.methodPayment,
            amount = saleMethod.amount,
            paymethod = saleMethod.payMethod
        };
    }

    public static List<SaleMethodsResponse> FromEntityList(List<SaleMethods> saleMethods)
    {
        return saleMethods.Select(FromEntity).ToList();
    }
}
