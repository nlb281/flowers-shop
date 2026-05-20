using Avalonia.Controls;
using Avalonia.Interactivity;
using flowersShop.Models;
using flowersShop.ViewModels;

namespace flowersShop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        DataContext = new MainWindowViewModel();
    }
    
    private void AddToCart_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.DataContext is Flower flower &&
            DataContext is MainWindowViewModel vm)
        {
            vm.AddToCart(flower);
        }
    }
}