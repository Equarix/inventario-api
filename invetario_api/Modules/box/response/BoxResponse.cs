using System;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.box.response;

public class BoxResponse
{
    public int boxId { get; set; }
    public decimal amountOpening { get; set; }
    public decimal? amountClosing { get; set; }
    public DateTime dateOpening { get; set; }
    public DateTime? dateClosing { get; set; }

    public UserSingleResponse userOpening { get; set; }

    public UserSingleResponse? userClosing { get; set; }

    public UserSingleResponse userActual { get; set; }

    public bool isOpen { get; set; }


    public static BoxResponse FromBoxEntity(entity.Box box)
    {
        return new BoxResponse
        {
            boxId = box.boxId,
            amountOpening = box.amountOpening,
            amountClosing = box.amountClosing,
            dateOpening = box.dateOpening,
            dateClosing = box.dateClosing,
            userOpening = UserSingleResponse.fromEntity(box.userOpening),
            userClosing = box.userClosing != null ? UserSingleResponse.fromEntity(box.userClosing) : null,
            userActual = UserSingleResponse.fromEntity(box.userActual),
            isOpen = box.isOpen
        };
    }

    public static List<BoxResponse> FromBoxEntityList(List<entity.Box> boxs)
    {
        return boxs.Select(box => FromBoxEntity(box)).ToList();
    }
}
