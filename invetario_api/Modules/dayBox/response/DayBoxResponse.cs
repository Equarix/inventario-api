using System;
using invetario_api.Modules.box.response;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.dayBox.response;

public class DayBoxResponse
{
    public int dayBoxId { get; set; }
    public int boxId { get; set; }
    public float totalefectivo { get; set; }
    public float totalTarjeta { get; set; }
    public string observations { get; set; }
    public DateTime date { get; set; }

    public BoxSingleResponse box { get; set; }

    public UserSingleResponse user { get; set; }

    public static DayBoxResponse fromEntity(entity.Daybox daybox)
    {
        return new DayBoxResponse
        {
            dayBoxId = daybox.dayboxId,
            boxId = daybox.boxId,
            totalefectivo = daybox.totalefectivo,
            totalTarjeta = daybox.totalTarjeta,
            observations = daybox.observations,
            date = daybox.date,
            box = BoxSingleResponse.fromEntity(daybox.box),
            user = UserSingleResponse.fromEntity(daybox.user)
        };
    }

    public static List<DayBoxResponse> fromEntityList(List<entity.Daybox> dayboxes)
    {
        return dayboxes.Select(d => fromEntity(d)).ToList();
    }
}
