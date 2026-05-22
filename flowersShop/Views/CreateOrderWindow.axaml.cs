using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using flowersShop.Models;
using flowersShop.ViewModels;

namespace flowersShop.Views;

public partial class CreateOrderWindow : Window
{
    public CreateOrderWindow(
        ObservableCollection<CartItem> cartItems)
    {
        InitializeComponent();
        
        DataContext = new CreateOrderWindowViewModel(cartItems);
    }
    
    private void GoToMainWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CreateOrderWindowViewModel vm)
        {
            vm.GoToMainWindow();
        }
    }
    
    private void CreateOrder_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CreateOrderWindowViewModel vm)
        {
            vm.CreateOrder();
        }
    }
}