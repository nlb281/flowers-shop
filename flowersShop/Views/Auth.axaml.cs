using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using flowersShop.Models;
using flowersShop.ViewModels;

namespace flowersShop.Views;

public partial class Auth : Window
{
    AuthViewModel vm =  new AuthViewModel();
    
    public Auth()
    {
        InitializeComponent();
        DataContext = vm;
        StaticFields.oldWindow = this;
    }
}