using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class AveragePriceByPropertyType
{
    public string PropertyType { get; set; } = null!;

    public decimal? AveragePrice { get; set; }
}
