using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class Transaction
{
    public int TransactionsId { get; set; }

    public int PropertyId { get; set; }

    public int ClientsId { get; set; }

    public int AgencyId { get; set; }

    public DateOnly TransactionDate { get; set; }

    public decimal AmountTransaction { get; set; }

    public virtual Agency Agency { get; set; } = null!;

    public virtual Client Clients { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;

    public virtual ICollection<TypeTransaction> TypeTransactions { get; set; } = new List<TypeTransaction>();
}
