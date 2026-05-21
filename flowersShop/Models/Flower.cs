using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace flowersShop.Models;

public partial class Flower
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImagePath { get; set; } = null!;
    
    public Bitmap ImageBitmap =>
        new Bitmap(
            AssetLoader.Open(
                new Uri(ImagePath)
            )
        );

    public string Availability { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
