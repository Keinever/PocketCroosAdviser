using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PocketCroosAdviser.ViewModels;
using ReactiveUI;

namespace PocketCroosAdviser.Views;

public partial class MarkersWindowView : ReactiveUserControl<MarkersWindowViewModel>
{
    public MarkersWindowView()
    {
        // Вызов WhenActivated используется для выполнения некоторого 
        // кода в момент активации и деактивации модели представления.
        this.WhenActivated((CompositeDisposable disposable) => { });
        AvaloniaXamlLoader.Load(this);
    }
    protected async override void OnInitialized()
    {
        var markersWindowViewModel = DataContext as MarkersWindowViewModel;
        if (markersWindowViewModel == null) throw new ArgumentNullException(nameof(markersWindowViewModel));
        await LoadData(markersWindowViewModel);
        base.OnInitialized();
    }

    private async Task LoadData(MarkersWindowViewModel markersWindowViewModel)
    {
        await markersWindowViewModel.LoadData().ContinueWith( t =>
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