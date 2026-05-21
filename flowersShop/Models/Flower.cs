using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using System.ComponentModel.DataAnnotations.Schema;

namespace flowersShop.Models;

public partial class Flower : ReactiveObject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImagePath { get; set; } = null!;
    
    [NotMapped]
    public Bitmap ImageBitmap =>
        new Bitmap(
            AssetLoader.Open(
                new Uri(ImagePath)
            )
        );

    public string Availability { get; set; } = null!;

    public int Quantity { get; set; }

    private bool _isInCart;
    
    [NotMapped]
    public bool IsInCart
    {
        get => _isInCart;
        set => this.RaiseAndSetIfChanged(ref _isInCart, value);
    }
    
    [NotMapped]
    public string AddButtonText =>
        IsInCart
            ? "Товар в корзине"
            : "Добавить в корзину";
    
    [NotMapped]
    public bool CanAddToCart => !IsInCart;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
