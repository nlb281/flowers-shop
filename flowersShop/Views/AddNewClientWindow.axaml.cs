using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using flowersShop.ViewModels;

namespace flowersShop.Views;

public partial class AddNewClientWindow : Window
{
    public AddNewClientWindow()
    {
        InitializeComponent();

        DataContext =
            new AddNewClientWindowViewModel();
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

    private void AddClient_OnClick(
        object? sender,
        RoutedEventArgs e)
    {
        if (DataContext is
            AddNewClientWindowViewModel vm)
        {
            vm.AddClient();
        }
    }

    private void Cancel_OnClick(
        object? sender,
        RoutedEventArgs e)
    {
        if (DataContext is
            AddNewClientWindowViewModel vm)
        {
            vm.CloseWindow();
        }
    }
}