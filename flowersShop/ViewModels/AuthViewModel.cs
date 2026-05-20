using System.Linq;
using flowersShop.Models;
using flowersShop.Views;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace flowersShop.ViewModels;

public class AuthViewModel : ViewModelBase
{
    private string login, password;

    public string Login
    {
        get => login;
        set => login = value;
    }

    public string Password
    {
        get => password;
        set => password = value;
    }

    public void LogIn()
    {
        StaticFields.user = StaticFields.context.Users.Include(x => x.Role)
            .FirstOrDefault(x => x.Login == Login && x.Password == Password);

        if (StaticFields.user == null)
        {
            MessageBoxManager.GetMessageBoxStandard("Information", "Invalid login or password", ButtonEnum.Ok)
                .ShowWindowAsync();
        }
        else
        {
            StaticFields.window = StaticFields.oldWindow;
            (new MainWindow()).Show();
            StaticFields.window.Close();
        }
    }
}