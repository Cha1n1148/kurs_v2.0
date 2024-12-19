using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class TypeTransaction
{
    public int TransactionTypeId { get; set; }

    public int TransactionsId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual Transaction Transactions { get; set; } = null!;
}
