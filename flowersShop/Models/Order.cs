using System;
using System.Collections.Generic;

namespace flowersShop.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public string Address { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int ClientId { get; set; }

    public int CourierId { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Courier Courier { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
