using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using flowersShop.Models;
using flowersShop.ViewModels;

namespace flowersShop.Views;

public partial class AllOrdersWindow : Window
{
    public AllOrdersWindow()
    {
        InitializeComponent();

        DataContext = new AllOrdersWindowViewModel();
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
        if (DataContext is AllOrdersWindowViewModel vm)
        {
            vm.GoToMainWindow();
        }
    }
    
    private void GoToChangeOrderStatus_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.DataContext is Order order &&
            DataContext is AllOrdersWindowViewModel vm)
        {
            vm.GoToChangeOrderStatus(order);
        }
    }
}