using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using flowersShop.Models;
using flowersShop.ViewModels;

namespace flowersShop.Views;

public partial class ChangeOrderStatusWindow : Window
{
    public ChangeOrderStatusWindow()
    {
        InitializeComponent();
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

    public ChangeOrderStatusWindow(Order order)
    {
        InitializeComponent();

        DataContext =
            new ChangeOrderStatusWindowViewModel(order);
    }
}