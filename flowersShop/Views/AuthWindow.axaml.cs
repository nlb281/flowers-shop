using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using flowersShop.Models;
using flowersShop.ViewModels;

namespace flowersShop.Views;

public partial class AuthWindow : Window
{
    AuthWindowViewModel vm =  new AuthWindowViewModel();
    
    public AuthWindow()
    {
        InitializeComponent();
        DataContext = vm;
        StaticFields.oldWindow = this;
    }
}