using System;

namespace invetario_api.Utils;

public class PageResult<T>
{
    public T items { get; set; }

    public int totalItems { get; set; }

    public double totalPages => Math.Ceiling((double)totalItems / limit);

    public int page { get; set; }

    public int limit { get; set; }
}
