using System;
using System.ComponentModel.DataAnnotations;
using invetario_api.Utils;

namespace invetario_api.Modules.dayBox.dto;

public class QueryDayBox : PaginateDto
{
    public DateTime? date { get; set; }
}

public class QueryDayBoxByDate
{
    [Required]
    public DateTime? date { get; set; }

    [Required]
    public int? boxId { get; set; }
}