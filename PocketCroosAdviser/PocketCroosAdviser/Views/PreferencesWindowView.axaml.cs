using System;
using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PocketCroosAdviser.ViewModels;
using ReactiveUI;

namespace PocketCroosAdviser.Views;

public partial class PreferencesWindowView : ReactiveUserControl<PreferencesWindowViewModel>
{
    public PreferencesWindowView()
    {
        // Вызов WhenActivated используется для выполнения некоторого 
        // кода в момент активации и деактивации модели представления.
        this.WhenActivated((CompositeDisposable disposable) => { });
        AvaloniaXamlLoader.Load(this);
    }
    protected async override void OnInitialized()
    {
        var blackListWindowViewModel = this.DataContext as PreferencesWindowViewModel;
        if (blackListWindowViewModel == null) throw new ArgumentNullException(nameof(blackListWindowViewModel));
        await blackListWindowViewModel.LoadData();
        base.OnInitialized();
    }
}