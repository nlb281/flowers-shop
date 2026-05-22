using System;
using System.Collections.ObjectModel;
using System.Linq;
using flowersShop.Models;
using flowersShop.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using Tmds.DBus.Protocol;

namespace flowersShop.ViewModels;

public class CreateOrderWindowViewModel : ViewModelBase
{
    private decimal _totalPrice;
    private string? _address;
    private DateTimeOffset? _deliveryDate;
    private Client? _selectedClient;
    private Courier? _selectedCourier;
    

    public ObservableCollection<CartItem> CartItems { get; set; }

    public ObservableCollection<Client> Clients { get; set; } = new();

    public ObservableCollection<Courier> FreeCouriers { get; set; } = new();
    
    public CreateOrderWindowViewModel(ObservableCollection<CartItem> cartItems)
    {
        CartItems = cartItems;

        LoadClients();
        LoadFreeCouriers();
        
        UpdateTotal();
    }
    
    public decimal TotalPrice
    {
        get => _totalPrice;
        set => this.RaiseAndSetIfChanged(ref _totalPrice, value);
    }
    
    public string? Address
    {
        get => _address;
        set => this.RaiseAndSetIfChanged(ref _address, value);
    }
    
    public DateTimeOffset? DeliveryDate
    {
        get => _deliveryDate;
        set => this.RaiseAndSetIfChanged(ref _deliveryDate, value);
    }
    
    public Client? SelectedClient
    {
        get => _selectedClient;
        set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
    }
    
    public Courier? SelectedCourier
    {
        get => _selectedCourier;
        set => this.RaiseAndSetIfChanged(ref _selectedCourier, value);
    }
    
    private void LoadClients()
    {
        Clients.Clear();
        
        var clients = StaticFields.context.Clients.AsQueryable();
        
        foreach (var client in clients)
        {
            Clients.Add(client);
        }
    }
    
    private void LoadFreeCouriers()
    {
        FreeCouriers.Clear();
        
        var freeCouriers = StaticFields.context.Couriers.AsQueryable().Where(x => x.IsFree == "true");
        
        foreach (var courier in freeCouriers)
        {
            FreeCouriers.Add(courier);
        }
    }
    
    private void UpdateTotal()
    {
        TotalPrice = CartItems.Sum(x => x.TotalPrice);
    }

    public async void CreateOrder()
    {
        if (SelectedClient == null)
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Выберите клиента",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        if (string.IsNullOrWhiteSpace(Address))
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Введите адрес доставки",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        if (DeliveryDate == null)
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Выберите дату доставки",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        if (DeliveryDate <= DateTime.Today)
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Дата доставки должна быть не раньше завтра",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        if (SelectedCourier == null)
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Выберите курьера",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        var order = new Order
        {
            OrderDate = DateOnly.FromDateTime(DateTime.Now),

            DeliveryDate = DateOnly.FromDateTime(
                DeliveryDate.Value.Date),

            Address = Address,

            Status = "Новый",

            ClientId = SelectedClient.Id,

            CourierId = SelectedCourier.Id,

            TotalPrice = TotalPrice
        };

        StaticFields.context.Orders.Add(order);

        await StaticFields.context.SaveChangesAsync();

        foreach (var cartItem in CartItems)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.Id,

                FlowerId = cartItem.Id,

                Quantity = cartItem.Quantity,

                Price = cartItem.Price
            };

            StaticFields.context.OrderItems.Add(orderItem);
        }

        SelectedCourier.IsFree = "false";

        StaticFields.context.SaveChanges();
        
        foreach (var cartItem in CartItems)
        {
            var flower = StaticFields.context.Flowers
                .FirstOrDefault(x => x.Id == cartItem.Id);

            if (flower != null)
            {
                flower.IsInCart = false;
            }
        }

        await MessageBoxManager
            .GetMessageBoxStandard(
                "Успех",
                "Заказ успешно оформлен",
                ButtonEnum.Ok)
            .ShowWindowAsync();

        CartItems.Clear();

        GoToMainWindow();
    }
    
    public void GoToMainWindow()
    {
        StaticFields.oldWindow?.Show();
        StaticFields.window?.Close();
    }
}