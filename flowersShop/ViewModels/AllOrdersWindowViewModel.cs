using System.Collections.ObjectModel;
using System.Linq;
using flowersShop.Models;
using flowersShop.Views;
using Microsoft.EntityFrameworkCore;

namespace flowersShop.ViewModels;

public class AllOrdersWindowViewModel
{
    public ObservableCollection<Order> Orders { get; set; } = new();

    public AllOrdersWindowViewModel()
    {
        LoadOrders();
    }
    
    public void ReloadOrders()
    {
        LoadOrders();
    }
    
    private void LoadOrders()
    {
        Orders.Clear();

        var orders = StaticFields.context.Orders.AsQueryable().Include(x => x.Client).OrderByDescending(x => x.Id);

        foreach (var order in orders)
        {
            Orders.Add(order);
        }
    }

    public void GoToChangeOrderStatus(Order order)
    {
        StaticFields.previousWindow =
            StaticFields.window;

        StaticFields.window?.Hide();

        StaticFields.window =
            new ChangeOrderStatusWindow(order);

        StaticFields.window.Show();
    }
    
    public void GoToMainWindow()
    {
        StaticFields.window?.Hide();

        StaticFields.window =
            StaticFields.mainWindow;

        StaticFields.window?.Show();
    }
}