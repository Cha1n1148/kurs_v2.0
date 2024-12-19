using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class TransactionsDetail
{
    public int TransactionsId { get; set; }

    public DateOnly TransactionDate { get; set; }

    public decimal AmountTransaction { get; set; }

    public string AddressProperty { get; set; } = null!;

    public string PropertyClass { get; set; } = null!;

    public string ClientFirstName { get; set; } = null!;

    public string ClientMiddleName { get; set; } = null!;

    public string ClientLastName { get; set; } = null!;

    public string NameAgency { get; set; } = null!;
}
