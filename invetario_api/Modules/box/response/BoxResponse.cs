using System;
using invetario_api.Modules.store.response;

namespace invetario_api.Modules.box.response;

public class BoxResponse
{
    public int boxId { get; set; }
    public string boxName { get; set; }

    public string serie { get; set; }

    public string serieProforma { get; set; }

    public int storeId { get; set; }

    public StoreSingleResponse store { get; set; }

    public bool status { get; set; }

    public static BoxResponse fromEntity(entity.Box box)
    {
        return new BoxResponse
        {
            boxId = box.boxId,
            boxName = box.boxName,
            serie = box.serie,
            serieProforma = box.serieProforma,
            storeId = box.storeId,
            store = StoreSingleResponse.fromEntity(box.store),
            status = box.status
        };
    }

    public static List<BoxResponse> fromEntityList(List<entity.Box> boxs)
    {
        return boxs.Select(box => fromEntity(box)).ToList();
    }
}
