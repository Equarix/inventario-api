using System;
using invetario_api.Modules.products.response;

namespace invetario_api.Modules.proforma.response.builder;

public class ProformaDetailsResponseBuilder
{
    private int _proformaDetailsId;
    private ProductSingleResponse _product;
    private int _quantity;
    private string? _productName;
    private decimal _price;


    public ProformaDetailsResponseBuilder setProformaDetailsId(int proformaDetailsId)
    {
        _proformaDetailsId = proformaDetailsId;
        return this;
    }

    public ProformaDetailsResponseBuilder setProduct(ProductSingleResponse product)
    {
        _product = product;
        return this;
    }

    public ProformaDetailsResponseBuilder setQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }

    public ProformaDetailsResponseBuilder setProductName(string productName)
    {
        _productName = productName;
        return this;
    }

    public ProformaDetailsResponseBuilder setPrice(decimal price)
    {
        _price = price;
        return this;
    }

    public ProformaDetailsResponse build()
    {
        return new ProformaDetailsResponse()
        {
            proformaDetailsId = _proformaDetailsId,
            product = _product,
            quantity = _quantity,
            productName = _productName!,
            price = _price
        };
    }
}
