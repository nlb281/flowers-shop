using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
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
    
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        if (Application.Current?.ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
    
    private void GoToMainWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CreateOrderWindowViewModel vm)
        {
            vm.GoToMainWindow();
        }
    }
    
    private void GoToAddNewClientWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is CreateOrderWindowViewModel vm)
        {
            vm.GoToAddNewClientWindow();
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