using System;

namespace invetario_api.Modules.box.response;

public class BoxSingleResponse
{
    public int boxId { get; set; }
    public string boxName { get; set; }

    public string serie { get; set; }

    public string serieProforma { get; set; }

    public int storeId { get; set; }


    public bool status { get; set; }

    public static BoxSingleResponse fromEntity(entity.Box box)
    {
        return new BoxSingleResponse
        {
            boxId = box.boxId,
            boxName = box.boxName,
            serie = box.serie,
            serieProforma = box.serieProforma,
            storeId = box.storeId,
            status = box.status
        };
    }

    public static List<BoxSingleResponse> fromEntityList(List<entity.Box> boxs)
    {
        return boxs.Select(box => fromEntity(box)).ToList();
    }
}
