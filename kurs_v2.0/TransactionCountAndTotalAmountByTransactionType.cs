using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class TransactionCountAndTotalAmountByTransactionType
{
    public string TransactionType { get; set; } = null!;

    public int? TransactionCount { get; set; }

    public decimal? TotalAmount { get; set; }
}
