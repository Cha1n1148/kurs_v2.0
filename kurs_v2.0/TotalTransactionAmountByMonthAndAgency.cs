using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class TotalTransactionAmountByMonthAndAgency
{
    public string NameAgency { get; set; } = null!;

    public int? Month { get; set; }

    public int? Year { get; set; }

    public decimal? TotalAmount { get; set; }
}
