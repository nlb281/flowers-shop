using System;
using System.Collections.Generic;

namespace flowersShop.Models;

public partial class Courier
{
    public int Id { get; set; }

    public string Fio { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string IsFree { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
