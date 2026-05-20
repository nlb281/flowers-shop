using System;
using System.Collections.Generic;

namespace flowersShop.Models;

public partial class Flower
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImagePath { get; set; } = null!;

    public string Availability { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
