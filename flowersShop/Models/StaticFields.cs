using Avalonia.Controls;

namespace flowersShop.Models;

public class StaticFields
{
    public static FlowersShopContext context = new FlowersShopContext();
    public static Window? oldWindow, window;
    public static User? user;
    public static Flower? flower;
}