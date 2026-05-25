using Avalonia.Controls;
using flowersShop.ViewModels;
using flowersShop.Views;

namespace flowersShop.Models;

public class StaticFields
{
    public static FlowersShopContext context = new FlowersShopContext();
    public static Window? previousWindow;
    public static Window? mainWindow;
    public static Window? window;
    public static User? user;
    public static Flower? flower;
}