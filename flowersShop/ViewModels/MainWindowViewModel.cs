using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;  
using System.Reactive;
using flowersShop.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace flowersShop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private bool _onlyAvailable;
    private decimal _totalPrice;
    private string _addButtonText = "Добавить";

    public bool OnlyAvailable
    {
        get => _onlyAvailable;
        set
        {
            this.RaiseAndSetIfChanged(ref _onlyAvailable, value);
            LoadFlowers();
        }
    }

    public decimal TotalPrice
    {
        get => _totalPrice;
        set => this.RaiseAndSetIfChanged(ref _totalPrice, value);
    }
    
    public string AddButtonText
    {
        get => _addButtonText;
        set => this.RaiseAndSetIfChanged(ref _addButtonText, value);
    }

    public ObservableCollection<Flower> Flowers { get; set; } = new();

    public ObservableCollection<CartItem> CartItems { get; set; } = new();

    public ReactiveCommand<Unit, Unit> CreateOrderCommand { get; }

    public ReactiveCommand<Unit, Unit> OpenOrdersCommand { get; }

    public MainWindowViewModel()
    {
        LoadFlowers();

        CreateOrderCommand = ReactiveCommand.Create(CreateOrder);

        OpenOrdersCommand = ReactiveCommand.Create(OpenOrders);

        this.WhenAnyValue(x => x.CartItems.Count)
            .Subscribe(_ => UpdateTotal());
    }

    private void LoadFlowers()
    {
        Flowers.Clear();

        var flowers = StaticFields.context.Flowers.AsQueryable();

        if (OnlyAvailable)
        {
            flowers = flowers.Where(x => x.Availability == "true");
        }

        foreach (var flower in flowers)
        {
            Flowers.Add(flower);
        }
    }

    public async void AddToCart(Flower flower)
    {
        if (flower.Quantity <= 0)
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Information",
                    "Product is out of stock",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        var existingItem = CartItems
            .FirstOrDefault(x => x.Id == flower.Id);

        if (existingItem != null) return;

        CartItems.Add(new CartItem
        {
            Id = flower.Id,
            Name = flower.Name,
            ImagePath = flower.ImagePath,
            Price = flower.Price,
            Quantity = 1
        });
        
        flower.IsInCart = true;
        
        flower.Quantity--;

        LoadFlowers();

        UpdateTotal();
    }

    public void AddOneToCart(CartItem cartItem)
    {
        var flower = Flowers.FirstOrDefault(x => x.Id == cartItem.Id);
        
        if (flower != null && flower.Quantity > 0)
        {
            cartItem.Quantity++;
            flower.Quantity--;
            
            LoadFlowers();
            
            UpdateTotal();
        }
    }

    public void RemoveOneFromCart(CartItem cartItem)
    {
        var flower = Flowers.FirstOrDefault(x => x.Id == cartItem.Id);

        if (flower != null)
        {
            cartItem.Quantity--;
            flower.Quantity++;
            
            LoadFlowers();
            
            UpdateTotal();
        }
        
        if (cartItem.Quantity == 0)
        {
            CartItems.Remove(CartItems.First(x => x.Id == cartItem.Id));
            
            flower.IsInCart = false;
        }
    }
    
    public void RemoveFromCart(CartItem cartItem)
    {
        var flower = Flowers.FirstOrDefault(x => x.Id == cartItem.Id);

        if (flower != null)
        {
            CartItems.Remove(CartItems.First(x => x.Id == cartItem.Id));
               
            flower.IsInCart = false;
            
            flower.Quantity += cartItem.Quantity;
            
            LoadFlowers();
            
            UpdateTotal();
        }
    }

    private void UpdateTotal()
    {
        TotalPrice = CartItems.Sum(x => x.TotalPrice);
    }

    private void CreateOrder()
    {
        // позже
    }

    private void OpenOrders()
    {
        // позже
    }
}