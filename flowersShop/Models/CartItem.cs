using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;

namespace flowersShop.Models;

public class CartItem : ReactiveObject
{
    private int _quantity;
    private decimal _price;

    public int Id { get; set; }

    public string Name { get; set; }
    
    public string ImagePath { get; set; }
    
    public Bitmap ImageBitmap =>
        new Bitmap(
            AssetLoader.Open(
                new Uri(ImagePath)
            )
        );

    public int Quantity
    {
        get => _quantity;
        set => this.RaiseAndSetIfChanged(ref _quantity, value);
    }
    
    public decimal Price
    {
        get => _price;
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }


    public decimal TotalPrice => Price * Quantity;
}