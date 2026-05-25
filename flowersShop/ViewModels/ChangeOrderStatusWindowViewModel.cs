using System.Collections.ObjectModel;
using System.Linq;
using flowersShop.Models;
using flowersShop.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace flowersShop.ViewModels;

public class ChangeOrderStatusWindowViewModel
    : ViewModelBase
{
    public Order Order { get; set; }

    public ObservableCollection<string> Statuses { get; set; } =
    [
        "Новый",
        "В сборке",
        "Передан курьеру",
        "Доставлен"
    ];

    private string? _selectedStatus;

    public string? SelectedStatus
    {
        get => _selectedStatus;
        set => this.RaiseAndSetIfChanged(
            ref _selectedStatus,
            value);
    }

    public ChangeOrderStatusWindowViewModel(Order order)
    {
        Order = order;

        SelectedStatus = order.Status;
    }

    public async void SaveStatus()
    {
        if (string.IsNullOrWhiteSpace(
                SelectedStatus))
        {
            return;
        }
        
        var courier = StaticFields.context.Couriers.FirstOrDefault(x => x.Id == Order.CourierId);
        
        if (courier == null)
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Курьер не найден",
                    ButtonEnum.Ok)
                .ShowWindowAsync();
            return;
        }
        
        if (SelectedStatus == "Доставлен")
        {
            courier.IsFree = "true";
        }
        else
        {
            courier.IsFree = "false";
        }
        
        Order.Status = SelectedStatus;

        StaticFields.context.Couriers.Update(courier);
        StaticFields.context.Orders.Update(Order);

        StaticFields.context.SaveChanges();

        GoBack();
        
        await MessageBoxManager
            .GetMessageBoxStandard(
                "Успех",
                "Статус заказа изменен",
                ButtonEnum.Ok)
            .ShowWindowAsync();
    }

    public void GoBack()
    {
        StaticFields.window?.Hide();

        StaticFields.window =
            StaticFields.previousWindow;
        
        if (StaticFields.window?.DataContext
            is AllOrdersWindowViewModel vm)
        {
            vm.ReloadOrders();
        }

        StaticFields.window?.Show();
    }
}