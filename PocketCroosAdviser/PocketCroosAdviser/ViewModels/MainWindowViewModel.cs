using System;
using PocketCroosAdviser.Data;
using ReactiveUI;

namespace PocketCroosAdviser.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private static ViewModelBase? _content;
    private static PocketContext _db = new();
    
    public MainWindowViewModel()
    {
        _db.Database.EnsureCreated();
        Content = new  GreetingWindowViewModel(_db);
    }
    
    public ViewModelBase? Content
    {
        get => _content;
        private set => this.RaiseAndSetIfChanged(ref _content, value);
    }
    
    public void OpenProfile()
    {
        Content = new ProfileWindowViewModel(_db);
    }
    
    public void OpenGreeting()
    {
        Content = new GreetingWindowViewModel(_db);
    }

    public void OpenPreferences()
    {
        Content = new PreferencesWindowViewModel(_db);
    }
    
    public void OpenMarkers()
    {
        Content = new MarkersWindowViewModel(_db);
    }
    
    public void OpenBlackList()
    {
        Content = new BlackListWindowViewModel(_db);
    }
    
}