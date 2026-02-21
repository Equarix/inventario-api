using System;
using invetario_api.Modules.provider.entity;

namespace invetario_api.Modules.provider.response;

public class ProviderResponseSingle
{
    public int providerId { get; set; }
    public string code { get; set; }
    public string companyName { get; set; }
    public string publicName { get; set; }
    public string typeDocument { get; set; }
    public string documentNumber { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public string mainContact { get; set; }
    public string contactPhone { get; set; }
    public string payCondition { get; set; }
    public PayCondition payConditionId { get; set; }
    public string typeMoney { get; set; }
    public int daysDelivery { get; set; }
    public bool status { get; set; } = true;
    public DateTime createdAt { get; set; }

    public static ProviderResponseSingle fromEntity(Provider provider)
    {
        return new ProviderResponseSingle
        {
            providerId = provider.providerId,
            code = provider.code,
            companyName = provider.companyName,
            publicName = provider.publicName,
            typeDocument = provider.typeDocument,
            documentNumber = provider.documentNumber,
            address = provider.address,
            phone = provider.phone,
            email = provider.email,
            mainContact = provider.mainContact,
            contactPhone = provider.contactPhone,
            payCondition = provider.payCondition.ToString(),
            payConditionId = provider.payCondition,
            typeMoney = provider.typeMoney,
            daysDelivery = provider.daysDelivery,
            status = provider.status,
            createdAt = provider.createdAt
        };
    }

    public static List<ProviderResponseSingle> fromEntityList(List<Provider> providers)
    {
        List<ProviderResponseSingle> responseList = new List<ProviderResponseSingle>();
        foreach (var provider in providers)
        {
            responseList.Add(fromEntity(provider));
        }
        return responseList;
    }
}
