using flowersShop.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace flowersShop.ViewModels;

public class AddNewClientWindowViewModel
    : ViewModelBase
{
    private string? _fio;
    private string? _phone;
    private string? _mail;

    public string? Fio
    {
        get => _fio;
        set => this.RaiseAndSetIfChanged(
            ref _fio,
            value);
    }

    public string? Phone
    {
        get => _phone;
        set => this.RaiseAndSetIfChanged(
            ref _phone,
            value);
    }

    public string? Mail
    {
        get => _mail;
        set => this.RaiseAndSetIfChanged(
            ref _mail,
            value);
    }

    public async void AddClient()
    {
        if (string.IsNullOrWhiteSpace(Fio))
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Введите ФИО",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        if (string.IsNullOrWhiteSpace(Phone))
        {
            await MessageBoxManager
                .GetMessageBoxStandard(
                    "Ошибка",
                    "Введите телефон",
                    ButtonEnum.Ok)
                .ShowWindowAsync();

            return;
        }

        var client = new Client
        {
            Fio = Fio,
            Phone = Phone,
            Mail = Mail
        };

        StaticFields.context.Clients.Add(client);

        StaticFields.context.SaveChanges();

        await MessageBoxManager
            .GetMessageBoxStandard(
                "Успех",
                "Клиент успешно добавлен",
                ButtonEnum.Ok)
            .ShowWindowAsync();

        if (StaticFields.previousWindow?.DataContext
            is CreateOrderWindowViewModel vm)
        {
            vm.ReloadClients();
        }
        
        CloseWindow();
    }

    public void CloseWindow()
    {
        StaticFields.window?.Close();

        StaticFields.window = StaticFields.previousWindow;

        StaticFields.window?.Show();
    }
}