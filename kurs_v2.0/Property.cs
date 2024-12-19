using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class Property
{
    public int PropertyId { get; set; }

    public string AddressProperty { get; set; } = null!;

    public string Class { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Condition { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
