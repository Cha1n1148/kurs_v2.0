using System;
using System.Collections.Generic;

namespace kurs_v2._0;

public partial class Agency
{
    public int AgencyId { get; set; }

    public string NameAgency { get; set; } = null!;

    public string AddressAgency { get; set; } = null!;

    public string PhoneAgency { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
