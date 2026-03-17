using System;
using invetario_api.Modules.products.response;
using invetario_api.Modules.proforma.entity;

namespace invetario_api.Modules.proforma.response;

public class ProformaDetailsResponse
{
    public int proformaDetailsId { get; set; }

    public ProductSingleResponse product { get; set; }

    public int quantity { get; set; }

    public string productName { get; set; }

    public decimal price { get; set; }


    public static ProformaDetailsResponse fromProformaDetails(ProformaDetails details)
    {
        return new ProformaDetailsResponse()
        {
            proformaDetailsId = details.proformaDetailsId,
            product = ProductSingleResponse.fromEntity(details.product),
            quantity = details.quantity,
            productName = details.productName,
            price = details.price
        };
    }

    public static List<ProformaDetailsResponse> fromProformaDetailsList(ICollection<ProformaDetails> details)
    {
        return details.Select(d => fromProformaDetails(d)).ToList();
    }
}
