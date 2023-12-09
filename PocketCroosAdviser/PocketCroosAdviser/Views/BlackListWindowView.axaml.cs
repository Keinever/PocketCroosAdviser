using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PocketCroosAdviser.ViewModels;
using ReactiveUI;

namespace PocketCroosAdviser.Views;

public partial class BlackListWindowView : ReactiveUserControl<BlackListWindowViewModel>
{
    public BlackListWindowView()
    {
        // Вызов WhenActivated используется для выполнения некоторого 
        // кода в момент активации и деактивации модели представления.
        this.WhenActivated((CompositeDisposable disposable) => { });
        AvaloniaXamlLoader.Load(this);
    }

    protected async override void OnInitialized()
    {
        var blackListWindowViewModel = DataContext as BlackListWindowViewModel;
        if (blackListWindowViewModel == null) throw new ArgumentNullException(nameof(blackListWindowViewModel));
        await LoadData(blackListWindowViewModel);
        base.OnInitialized();
    }

    private async Task LoadData(BlackListWindowViewModel blackListWindowViewModel)
    {
        await blackListWindowViewModel.LoadData().ContinueWith( t =>
        {
            if (t.IsFaulted) // Проверка на наличие исключения
            {
                Console.WriteLine("An exception occurred: " + t.Exception);

            }
            else
            {
                Console.WriteLine("Continuing after handling the exception");
            }
        });
    }

}